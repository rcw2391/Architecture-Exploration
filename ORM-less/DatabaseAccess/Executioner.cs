using ModelFactory;
using Models;
using System.Data.SqlClient;

namespace DatabaseAccess
{
    // Just having fun, should probably be named something else xD
    public class Executioner : IExecutioner
    {
        private IConnectionManager _connectionManager;
        
        public Executioner(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        
        public async Task<IEnumerable<T>> ExecuteFetchAsync<T>(string queryString, SqlParameter[] parameters) where T : DbObject
        {
            SqlConnection connection = await _connectionManager.GetConnectionAsync();
            SqlCommand command = new(queryString, connection);
            command.Parameters.AddRange(parameters);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            return Factory.ReadResults<T>(reader);
        }
    }
}
