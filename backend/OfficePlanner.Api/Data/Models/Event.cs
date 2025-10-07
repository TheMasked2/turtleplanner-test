namespace OfficePlanner.Api.Data.Models;

public class Event
{
    public int? EventId { get; set; } // Primary Key
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDate { get; set; }
    public int? CreatedBy { get; set; } // FK UserId
}