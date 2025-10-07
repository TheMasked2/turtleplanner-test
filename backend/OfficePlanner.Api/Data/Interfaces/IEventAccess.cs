using OfficePlanner.Api.Data.Models;

namespace OfficePlanner.Api.Data.Interfaces;
public interface IEventAccess : IGenericAccess<Event, int>
{
    bool TestConnection(); // Test method
}