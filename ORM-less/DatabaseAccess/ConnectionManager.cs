using System.Data;
using System.Data.SqlClient;

namespace DatabaseAccess
{
    // To ensure multiple connections aren't created, use a singleton
    public class ConnectionManager : IConnectionManager
    {
        private static readonly ConnectionManager _instance = new ConnectionManager();

        static ConnectionManager()
        {

        }

        private ConnectionManager() { }

        public static ConnectionManager Instance => _instance;

        private string _connection;
        private bool _isInit;

        public async Task<bool> InitAsync(string connectionString)
        {
            if (!_isInit)
            {
                using (SqlConnection connection = new(connectionString))
                {
                    _connection = connectionString;
                    await connection.OpenAsync();
                    _isInit = connection.State == ConnectionState.Open;
                }

            }

            return _isInit;
        }

        /// <summary>
        /// Returns open db connection.
        /// </summary>
        public async Task<SqlConnection> GetConnectionAsync()
        {
            if (_isInit)
            {
                SqlConnection connection = new SqlConnection(_connection);
                await connection.OpenAsync();
                return connection;
            }

            throw new InvalidOperationException("ConnectionManager is not initialized.");
        }
    }
}
