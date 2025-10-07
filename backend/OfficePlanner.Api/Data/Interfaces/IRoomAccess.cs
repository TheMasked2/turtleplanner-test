using OfficePlanner.Api.Data.Models;

namespace OfficePlanner.Api.Data.Interfaces;
public interface IRoomAccess : IGenericAccess<Room, int>
{
    bool TestConnection();
}