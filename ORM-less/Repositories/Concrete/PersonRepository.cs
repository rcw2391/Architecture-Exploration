using DatabaseAccess;
using Models;
using Repositories.Interfaces;
using System.Data.SqlClient;

namespace Repositories.Concrete
{
    public class PersonRepository : IPersonRepository
    {
        private IExecutioner _executioner;
        
        public PersonRepository(IExecutioner executioner)
        {
            _executioner = executioner;
        }

        public async Task<Person> GetByIDAsync(int id)
        {
            string queryString = @"exec GetPersonByID @ID = @id";
            SqlParameter[] parameters = new SqlParameter[1] { new SqlParameter("id", id) };

            return (await _executioner.ExecuteFetchAsync<Person>(queryString, parameters)).First();
        }
    }
}
