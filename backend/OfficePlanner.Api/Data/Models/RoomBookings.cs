namespace OfficePlanner.Api.Data.Models;

public class RoomBookings
{
    public int RoomId { get; set; } // ForeignKey
    public int UserId { get; set; } // ForeignKey
    public DateTime BookingDate { get; set; } // PrimaryKey
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Purpose { get; set; }
}