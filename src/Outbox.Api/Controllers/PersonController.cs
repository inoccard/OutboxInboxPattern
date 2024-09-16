using Application.Outbox;
using Domain.Contracts;
using Domain.Models.OutboxAggregate.Services;
using Domain.Models.PersonAggregate.Dtos;
using Domain.Models.PersonAggregate.Entities;
using Domain.Models.PersonAggregate.Services;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Outbox.Api.Controllers;

[ApiController]
[Route("[controller]/person")]
public class PersonController(
    IRepository repository,
    ILogger<PersonController> logger,
    IOutboxEventService outboxEventService,
    IPersonService personService,
    IMediatorHandlerOutbox mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> SaveAsync(PersonDto personDto)
    {
        try
        {
            var person = new Person(personDto.Name, personDto.Document, personDto.Doctype);

            var saved = await personService.SaveAsync(person);

            if (!saved) return BadRequest("Não foi possível salvar");

            var @event = new PersonCreatedNotification(person.Id, person.Name, person.Document, person.DocumentType);
            await outboxEventService.SaveEvent(@event);

            await SendPersonCreated(@event);

            return Ok($"Pessoa cadastrada com sucesso. Id: {person.Id}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return BadRequest($"{ex.Message} {ex.InnerException.Message}");
        }
    }

    private async Task SendPersonCreated(PersonCreatedNotification personEvent)
    {
        try
        {
            await mediator.Publish(personEvent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, PersonDto personDto)
    {
        try
        {
            var personOld = await repository.Query<Person>(p => p.Id == id).FirstOrDefaultAsync();

            bool saved;
            if (personOld is null) return NotFound();

            personOld.Update(personDto.Name, personDto.Document, personDto.Doctype);

            repository.Update(personOld);
            saved = await repository.CommitAsync();

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
            var person = await repository.QueryAsNoTracking<Person>()
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