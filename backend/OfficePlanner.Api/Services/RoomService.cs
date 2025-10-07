using System.Threading.Tasks;
using OfficePlanner.Api.Data.Interfaces;
using OfficePlanner.Api.Dtos;
using OfficePlanner.Api.Services.Interfaces;

namespace OfficePlanner.Api.Services;
public class RoomService : IRoomService
{
    private readonly IRoomAccess _roomAccess;

    public RoomService(IRoomAccess roomAccess)
    {
        _roomAccess = roomAccess;
    }

    public bool TestConnection()
    {
        try
        {
            bool test = _roomAccess.TestConnection();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
