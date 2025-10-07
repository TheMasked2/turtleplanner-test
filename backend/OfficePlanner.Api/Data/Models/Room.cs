namespace OfficePlanner.Api.Data.Models;

public class Room
{
    public int RoomId { get; set; } // ForeignKey
    public string? RoomName { get; set; }
    public int Capacity { get; set; }
    public string? Location { get; set; }   
}