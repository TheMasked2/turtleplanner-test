namespace OfficePlanner.Api.Data.Models;

public enum UserRole { User = 0, Admin = 1 }

public class Employee
{
    public int UserId { get; set; } // Primary Key
    public string? Name  { get; set; }
    public string? Email { get; set; }
    public UserRole Role { get; set; } = UserRole.User; // Is still text in db
    public string? Password { get; set; }
}
