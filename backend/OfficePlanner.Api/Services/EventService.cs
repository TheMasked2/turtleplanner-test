using System.Threading.Tasks;
using OfficePlanner.Api.Data.Interfaces;
using OfficePlanner.Api.Dtos;
using OfficePlanner.Api.Services.Interfaces;

namespace OfficePlanner.Api.Services;
public class EventService : IEventService
{
    private readonly IEventAccess _eventAccess;

    public EventService(IEventAccess eventAccess)
    {
        _eventAccess = eventAccess;
    }

    public bool TestConnection()
    {
        try
        {
            bool test = _eventAccess.TestConnection();
            return true;
        }
        catch
        {
            return false;
        }
    }
}