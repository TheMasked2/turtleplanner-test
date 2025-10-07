using Microsoft.AspNetCore.Mvc;
using OfficePlanner.Api.Dtos;
using OfficePlanner.Api.Services.Interfaces;


namespace OfficePlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;

    // Inject the interface, not the concrete class
    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }   


    [HttpGet("test-connection")]
    public IActionResult TestDatabaseConnection()
    {
        bool isConnected = _roomService.TestConnection();
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