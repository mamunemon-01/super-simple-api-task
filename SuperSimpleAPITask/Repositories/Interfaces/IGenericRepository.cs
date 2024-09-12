using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperSimpleAPITask.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        //Task<IEnumerable<T>> SearchByNameAsync(string name);
    }
}
