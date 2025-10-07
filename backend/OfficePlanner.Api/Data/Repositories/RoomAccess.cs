using Npgsql;
using Dapper;
using OfficePlanner.Api.Data.Models;
using OfficePlanner.Api.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OfficePlanner.Api.Data.Repositories;

public class RoomAccess : GenericAccess<Room, int>, IRoomAccess
{
    protected override string Table => "rooms";
    protected override string PrimaryKey => "room_id";
    public RoomAccess(IConfiguration config) : base(config.GetConnectionString("DefaultConnection"))
    { }

    public override async Task InsertAsync(Room room)
    {
        using var con = GetConnection();
        await con.OpenAsync();
        var query = $@"
            INSERT INTO {Table} (name, capacity, location)
            VALUES (@Name, @Capacity, @Location)
            RETURNING room_id;
        ";
        room.RoomId = await con.ExecuteScalarAsync<int>(query, room);
    }

    public override async Task UpdateAsync(Room room)
    {
        using var con = GetConnection();
        await con.OpenAsync();
        var query = $@"
            UPDATE {Table}
            SET 
                name = @Name,
                capacity = @Capacity,
                location = @Location
            WHERE {PrimaryKey} = @RoomId;
        ";
        await con.ExecuteAsync(query, room);
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