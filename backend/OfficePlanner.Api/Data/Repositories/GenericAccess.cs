using OfficePlanner.Api.Data.Interfaces;
using Npgsql;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OfficePlanner.Api.Data.Repositories
{
    // TODO: Implement generic data access methods, using the new database? (sqlite? MySQL?)
    // TODO: Implement specific repositories like UserAccess for user database table (if we go with sql)

    public abstract class GenericAccess<TModel, TKey> : IGenericAccess<TModel, TKey> where TModel : class
    {
        protected abstract string Table { get; }
        protected abstract string PrimaryKey { get; }
        private readonly string _connectionString;
        protected GenericAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public virtual async Task<TModel?> GetByIdAsync(TKey id)
        {
            using var con = GetConnection();
            await con.OpenAsync();
            var query = $"SELECT * FROM {Table} WHERE {PrimaryKey} = @Id";
            var parameters = new { Id = id };
            return await con.QuerySingleOrDefaultAsync<TModel>(query, parameters);
        }

        public virtual async Task<List<TModel>?> GetAllAsync()
        {
            using var con = GetConnection();
            await con.OpenAsync();
            var query = $"SELECT * FROM {Table}";
            var result = await con.QueryAsync<TModel>(query);
            return result.AsList();
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            using var con = GetConnection();
            await con.OpenAsync();
            var query = $"DELETE FROM {Table} WHERE {PrimaryKey} = @Id";
            var parameters = new { Id = id };
            await con.ExecuteAsync(query, parameters);
        }

        public abstract Task InsertAsync(TModel item);
        public abstract Task UpdateAsync(TModel item);
        // Implementatin of these are too specific, will have to be done in the specific access classes

    }
}