using System.Threading.Tasks;
using OfficePlanner.Api.Dtos;
using OfficePlanner.Api.Data.Models;

namespace OfficePlanner.Api.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResponse?> LoginAsync(LoginRequest req);
    Task<List<Employee>> GetAllEmployeesAsync();
    bool TestConnection(); // Test method
}
