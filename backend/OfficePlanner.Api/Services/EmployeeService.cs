using System.Threading.Tasks;
using OfficePlanner.Api.Data.Interfaces;
using OfficePlanner.Api.Data.Models;
using OfficePlanner.Api.Dtos;
using OfficePlanner.Api.Services.Interfaces;

namespace OfficePlanner.Api.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeAccess _employeeAccess;

    public EmployeeService(IEmployeeAccess employeeAccess)
        => _employeeAccess = employeeAccess;

    public async Task<EmployeeResponse?> LoginAsync(LoginRequest req)
    {
        var user = await _employeeAccess.FindByEmailAsync(req.Email);

        if (user is null || user.Password != req.Password)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        return new EmployeeResponse(user.UserId, user.Email!, user.Name!, user.Role.ToString());
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _employeeAccess.GetAllAsync();
    }

    public bool TestConnection()
    {
        try
        {
            bool test = _employeeAccess.TestConnection();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
