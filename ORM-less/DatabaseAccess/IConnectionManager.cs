using System.Data.SqlClient;

namespace DatabaseAccess
{
    public interface IConnectionManager
    {
        Task<bool> InitAsync(string connectionString);
        Task<SqlConnection> GetConnectionAsync();
    }
}