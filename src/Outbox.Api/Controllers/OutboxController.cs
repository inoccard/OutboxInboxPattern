using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Outbox.Api.Domain.Models.PersonAggregate.Dtos;
using Outbox.Api.Domain.Models.PersonAggregate.Entities;
using Outbox.Api.Domain.Models.PersonAggregate.Enums;
using Outbox.Api.Domain.Repository;

namespace Outbox.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OutboxController : ControllerBase
{
    private readonly ILogger<OutboxController> _logger;
    private readonly IRepository _repository;

    public OutboxController(IRepository repository, ILogger<OutboxController> logger)
    {
        _logger = logger;
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

            if (!saved) return BadRequest();

            return Ok($"Pessoa cadastrada com sucesso. Id: {person.Id}");
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message} {ex.InnerException.Message}");
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(Person person)
    {
        try
        {
            var personOld = _repository.Query<Person>(p => p.Id == person.Id);

            bool saved;
            if (personOld is null) return NotFound();

            _repository.Update(person);
            saved =  await _repository.CommitAsync();

            if (!saved) return BadRequest();

            return Ok($"Pessoa cadastrada com sucesso. Id: {person.Id}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetAsync(Guid id)
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