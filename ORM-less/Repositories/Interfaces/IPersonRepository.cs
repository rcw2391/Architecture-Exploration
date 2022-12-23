using Models;

namespace Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> GetByIDAsync(int id);
        Task<IEnumerable<Person>> GetAllWithChildren();
    }
}
