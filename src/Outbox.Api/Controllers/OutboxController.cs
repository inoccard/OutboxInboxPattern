using Microsoft.AspNetCore.Mvc;
using Outbox.Api.Domain.Models.OutboxAggregrate.Services;

namespace Outbox.Api.Controllers;

[ApiController]
[Route("[controller]/outbox")]
public class OutboxController(OutboxEventService outboxEventService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(outboxEventService.GetEvents());
    }
}