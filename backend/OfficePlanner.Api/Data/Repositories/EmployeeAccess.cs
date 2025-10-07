using System;
using Dapper;
using Npgsql;
using OfficePlanner.Api.Data.Models;
using OfficePlanner.Api.Data.Interfaces;

namespace OfficePlanner.Api.Data.Repositories;

public class EmployeeAccess : GenericAccess<Employee, int>, IEmployeeAccess
{
    protected override string Table => "employees";
    protected override string PrimaryKey => "userid";

    public EmployeeAccess(IConfiguration config)
        : base(config.GetConnectionString("DefaultConnection")) { }


    public async Task<Employee?> FindByEmailAsync(string email)
    {
        using var con = GetConnection();
        const string sql = @"
            SELECT 
                userid  AS UserId,
                name     AS Name,
                email    AS Email,
                role     AS Role,      -- stored as text: 'Admin'/'User'
                password AS Password
            FROM employees
            WHERE email = @email
            LIMIT 1;";
        return await con.QueryFirstOrDefaultAsync<Employee>(sql, new { email });
    }

    public override async Task InsertAsync(Employee employee)
    {
        using var con = GetConnection();
        await con.OpenAsync();
        var query = $@"
            INSERT INTO {Table} (name, email, role, password)
            VALUES (@Name, @Email, @Role, @Password)
            RETURNING user_id;
        ";
        employee.UserId = await con.ExecuteScalarAsync<int>(query, employee);
    }

    public override async Task UpdateAsync(Employee employee)
    {
        using var con = GetConnection();
        await con.OpenAsync();
        var query = $@"
            UPDATE {Table}
            SET name = @Name,
                email = @Email,
                role = @Role,
                password = @Password
            WHERE {PrimaryKey} = @UserId;
        ";
        await con.ExecuteAsync(query, employee);
    }

    // Test method
    public bool TestConnection()
    {
        try
        {
            using var con = GetConnection();
            con.Open();
            var result = con.ExecuteScalar<int>("SELECT 1");
            return result == 1;
        }
        catch
        {
            return false;
        }
    }
}