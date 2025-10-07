using System;
using Dapper;
using Npgsql;
using OfficePlanner.Api.Data.Models;
using OfficePlanner.Api.Data.Interfaces;

namespace OfficePlanner.Api.Data.Repositories;

public class AdminAccess : GenericAccess<Admin, int>, IAdminAccess
{
    protected override string Table => "admins";
    protected override string PrimaryKey => "admin_id";

    public AdminAccess(IConfiguration config) : base(config.GetConnectionString("DefaultConnection"))
    { }

    public override async Task InsertAsync(Admin admin)
    {
        using var con = GetConnection();
        await con.OpenAsync();
        var query = $@"
            INSERT INTO {Table} (user_id, permissions)
            VALUES (@UserId, @Permissions)
            RETURNING admin_id;
        ";
        admin.AdminId = await con.ExecuteScalarAsync<int>(query, admin);
    }

    public override async Task UpdateAsync(Admin admin)
    {
        using var con = GetConnection();
        await con.OpenAsync();
        var query = $@"
            UPDATE {Table}
            SET user_id = @UserId,
                permissions = @Permissions
            WHERE {PrimaryKey} = @AdminId;
        ";
        await con.ExecuteAsync(query, admin);
    }
}
