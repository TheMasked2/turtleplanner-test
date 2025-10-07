namespace OfficePlanner.Api.Data.Models;

public class EventParticipation
{
    public int? EventId { get; set; }
    public int? UserId { get; set; }
    public string? Status { get; set; }
}