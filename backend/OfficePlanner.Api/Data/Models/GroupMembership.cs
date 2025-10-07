namespace OfficePlanner.Api.Data.Models;

public class GroupMembership
{
    public int UserId {get; set;} // FK
    public int GroupId {get; set;} // FK
}