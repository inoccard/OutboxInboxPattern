using Domain.Models.OutboxAggregate.Services;
using Microsoft.AspNetCore.Mvc;

namespace Outbox.Api.Controllers;

[ApiController]
[Route("[controller]/outbox")]
public class OutboxController(IOutboxEventService outboxEventService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(outboxEventService.GetEvents());
    }
}