using Domain.Models.OutboxAggregrate.Services;
using Domain.Models.PersonAggregate.Dtos;
using Domain.Models.PersonAggregate.Entities;
using Domain.Models.PersonAggregate.Notifications;
using Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Outbox.Api.Controllers;

[ApiController]
[Route("[controller]/person")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IMediator _mediator;
    private readonly IOutboxEventService _outboxEventService;
    private readonly IRepository _repository;

    public PersonController(
        IRepository repository,
        ILogger<PersonController> logger,
        IOutboxEventService outboxEventService,
        IMediator mediator)
    {
        _logger = logger;
        _outboxEventService = outboxEventService;
        _mediator = mediator;
        _repository = repository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> SaveAsync(PersonDto personDto)
    {
        try
        {
            var person = new Person(personDto.Name, personDto.Document, personDto.Doctype);
            await _repository.AddAsync(person);
            var saved = await _repository.CommitAsync();

            // Salva o evento logo após salvar os dados
            var @event = new PersonCreatedNotification(person.Id, person.Name, person.Document, person.DocumentType);
            await _outboxEventService.SaveEvent(@event);

            await SendPersonCreated(person);

            if (!saved) return BadRequest();

            return Ok($"Pessoa cadastrada com sucesso. Id: {person.Id}");
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message} {ex.InnerException.Message}");
        }
    }

    private async Task SendPersonCreated(Person person)
    {
        try
        {
            var personEvent =
                new PersonCreatedNotification(person.Id, person.Name, person.Document, person.DocumentType);
            await _mediator.Publish(personEvent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, PersonDto personDto)
    {
        try
        {
            var personOld = await _repository.Query<Person>(p => p.Id == id).FirstOrDefaultAsync();

            bool saved;
            if (personOld is null) return NotFound();

            personOld.Update(personDto.Name, personDto.Document, personDto.Doctype);

            _repository.Update(personOld);
            saved = await _repository.CommitAsync();

            if (!saved) return BadRequest();

            return Ok($"Pessoa cadastrada com sucesso. Id: {personOld.Id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var person = await _repository.QueryAsNoTracking<Person>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null) return NotFound();

            return Ok(person);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}