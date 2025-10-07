using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OfficePlanner.Api.Data.Models;
using OfficePlanner.Api.Data.Interfaces;

namespace OfficePlanner.Api.Data.Repositories;

public class EventParticipationAccess : GenericAccess<EventParticipation, int>, IEventParticipationAccess
{
    protected override string Table => "eventparticipation";
    protected override string PrimaryKey => "event_id";

    public EventParticipationAccess(IConfiguration config) : base(config.GetConnectionString("DefaultConnection")) { }

    public override async Task InsertAsync(EventParticipation participation)
    {
        using var con = GetConnection();
        await con.OpenAsync();
        var query = $@"
            INSERT INTO {Table} ({PrimaryKey}, user_id, status)
            VALUES (@EventId, @UserId, @Status);
        ";
        await con.ExecuteAsync(query, participation);
    }

    public override async Task UpdateAsync(EventParticipation participation)
    {
        using var con = GetConnection();
        await con.OpenAsync();
        var query = $@"
            UPDATE {Table}
            SET status = @Status
            WHERE {PrimaryKey} = @EventId;
        ";
        await con.ExecuteAsync(query, participation);
    }
}
