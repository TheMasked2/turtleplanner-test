using OfficePlanner.Api.Data.Models;

namespace OfficePlanner.Api.Data.Interfaces;

public interface IEmployeeAccess : IGenericAccess<Employee, int>
{
    Task<Employee?> FindByEmailAsync(string email);
    public bool TestConnection(); // Test method
}