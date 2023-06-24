using Dapper;
using OCA.API.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OCA.API.Services
{
    public class DapperWrapper : IDapperWrapper
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperWrapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection") ?? string.Empty;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public Task<T> QuerySinglOrDefaultAsync<T>(IDbConnection connection, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
