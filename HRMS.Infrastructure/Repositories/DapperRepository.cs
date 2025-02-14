using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using HRMS.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace HRMS.Infrastructure.Repositories
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IDbConnection connection;
        public DapperRepository(IConfiguration configuration)
        {
            connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await connection.ExecuteAsync(sql, param, transaction);
        }

        public async Task<List<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class
        {
            return (await connection.QueryAsync<T>(sql, param, transaction, commandType: CommandType.StoredProcedure)).AsList();
        }

		public async Task<List<T>> NonSpQueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class
		{
			return (await connection.QueryAsync<T>(sql, param, transaction)).AsList();
		}
		public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class
        {
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class
        {
            return await connection.QuerySingleAsync<T>(sql, param, transaction);
        }
        public async Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await connection.ExecuteReaderAsync(sql, param, transaction);
        }
        public async Task<GridReader> QueryMultipleAsync(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await connection.QueryMultipleAsync(sql,param,transaction,commandTimeout,commandType);
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
