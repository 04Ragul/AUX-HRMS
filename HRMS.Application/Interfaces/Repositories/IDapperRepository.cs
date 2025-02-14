using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace HRMS.Application.Interfaces.Repositories
{
    public interface IDapperRepository
    {
            Task<GridReader> QueryMultipleAsync(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

            Task<List<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class;
            Task<List<T>> NonSpQueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class;
            Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class;
            Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default) where T : class;
            Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
     
    }
}
