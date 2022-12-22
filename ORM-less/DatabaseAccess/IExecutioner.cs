using Models;
using System.Data.SqlClient;

namespace DatabaseAccess
{
    public interface IExecutioner
    {
        Task<IEnumerable<T>> ExecuteFetchAsync<T>(string queryString, SqlParameter[] parameters) where T : DbObject;
    }
}
