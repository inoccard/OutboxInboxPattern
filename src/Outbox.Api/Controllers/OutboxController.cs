using Microsoft.AspNetCore.Mvc;
using Outbox.Api.Domain.Models.PersonAggregate.Entities;
using Outbox.Api.Domain.Models.PersonAggregate.Enums;
using Outbox.Api.Domain.Repository;

namespace Outbox.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OutboxController : ControllerBase
{
    private readonly ILogger<OutboxController> _logger;
    private readonly IMongoRepository<Person> _repository;

    public OutboxController(IMongoRepository<Person> repository, ILogger<OutboxController> logger)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpPost()]
    public async Task<IActionResult> SaveAsync(Person person)
    {
        try
        {
            var personOld = _repository.FindOneAsync(p => p.Id == person.Id);
            
            bool saved;
            if (personOld == null)
                saved = await _repository.InsertAsync(person);
            else
                saved = await _repository.ReplaceOneAsync(person);
            
            if (!saved) return BadRequest();

            return Ok();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpGet()]
    public async Task<IActionResult> GetAsync()
    {
        try
        {
            var persons = await _repository.FilterByAsync(null);

            if (!persons.Any()) return NotFound();

            return Ok(persons);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    [HttpGet("id")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var person = await _repository.FindOneAsync(p => p.Id == id);

            if (person == null) return NotFound();

            return Ok(person);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}