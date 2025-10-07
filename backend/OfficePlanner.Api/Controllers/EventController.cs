using Microsoft.AspNetCore.Mvc;
using OfficePlanner.Api.Dtos;
using OfficePlanner.Api.Services.Interfaces;


namespace OfficePlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    // Inject the interface, not the concrete class
    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }   


    [HttpGet("test-connection")]
    public IActionResult TestDatabaseConnection()
    {
        bool isConnected = _eventService.TestConnection();
        if (isConnected)
        {
            return Ok(new { status = "success", message = "Database connection successful." });
        }
        else
        {
            return StatusCode(500, new { status = "error", message = "Failed to connect to the database." });
        }
    }
}