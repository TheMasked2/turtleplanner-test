using Microsoft.AspNetCore.Mvc;
using OfficePlanner.Api.Dtos;
using OfficePlanner.Api.Services.Interfaces;
using OfficePlanner.Api.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace OfficePlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService employeeService) => _employeeService = employeeService;


    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<EmployeeResponse>> Login([FromBody] LoginRequest req)
    {
        try
        {
            var employeeResponse = await _employeeService.LoginAsync(req);
            return Ok(employeeResponse);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Employee>>> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    [HttpGet("test-connection")]
    public IActionResult TestDatabaseConnection()
    {
        bool isConnected = _employeeService.TestConnection();
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
