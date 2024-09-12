using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSimpleAPITask.Models;
using SuperSimpleAPITask.Repositories.Interfaces;

public interface IDepartmentRepository : IGenericRepository<Department> {
    //Task<IEnumerable<Department>> GetAllAsync();
    //Task<Department> GetByIdAsync(Guid id);
    //Task<int> CreateAsync(Department Department);
    //Task<int> UpdateAsync(Department Department);
    //Task<int> DeleteAsync(Guid id);
    Task<IEnumerable<Department>> SearchByName(string name);
}