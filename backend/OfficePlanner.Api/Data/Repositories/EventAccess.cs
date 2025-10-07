using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OfficePlanner.Api.Data.Models;
using OfficePlanner.Api.Data.Interfaces;

namespace OfficePlanner.Api.Data.Repositories;

public class EventAccess : GenericAccess<Event, int>, IEventAccess
{
    protected override string Table => "events";
    protected override string PrimaryKey => "event_id";

    public EventAccess(IConfiguration config) : base(config.GetConnectionString("DefaultConnection")) { }

    public override async Task InsertAsync(Event ev)
    {
        using var con = GetConnection();
        await con.OpenAsync();
        var query = $@"
            INSERT INTO {Table} (title, description, event_date, created_by)
            VALUES (@Title, @Description, @EventDate, @CreatedBy)
            RETURNING event_id;
        ";
        ev.EventId = await con.ExecuteScalarAsync<int>(query, ev);
    }

    public override async Task UpdateAsync(Event ev)
    {
        using var con = GetConnection();
        await con.OpenAsync();
        var query = $@"
            UPDATE {Table}
            SET title = @Title,
                description = @Description,
                event_date = @EventDate,
                created_by = @CreatedBy
            WHERE {PrimaryKey} = @EventId;
        ";
        await con.ExecuteAsync(query, ev);
    }

    public bool TestConnection()
    {
        try
        {
            using var con = GetConnection();
            con.Open();
            // Execute a simple query to confirm the connection is live.
            var result = con.QueryFirstOrDefault<int>("SELECT 1");
            return result == 1;
        }
        catch (Exception ex)
        {
            // Log the exception if you have a logging mechanism.
            Console.WriteLine($"Database connection test failed: {ex.Message}");
            return false;
        }
    }

}