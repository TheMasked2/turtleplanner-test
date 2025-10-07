namespace OfficePlanner.Api.Data.Models;

public class Admin : Employee
{
    public int? AdminId { get; set; } // Primary Key
    public int? UserId { get; set; } // FK 
    public string? Permissions { get; set; }
}