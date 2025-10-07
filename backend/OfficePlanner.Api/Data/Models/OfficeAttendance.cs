namespace OfficePlanner.Api.Data.Models;

public class OfficeAttendance
{
    public int AttendanceId { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public string? Status { get; set; }
}