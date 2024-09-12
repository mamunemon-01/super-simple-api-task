using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSimpleAPITask.Models;
using SuperSimpleAPITask.Repositories.Interfaces;

public interface IEmployeeRepository : IGenericRepository<Employee> {
    //Task<IEnumerable<Employee>> GetAllAsync();
    //Task<Employee> GetByIdAsync(Guid id);
    //Task<int> CreateAsync(Employee employee);
    //Task<int> UpdateAsync(Employee employee);
    //Task<int> DeleteAsync(Guid id);
    Task<IEnumerable<Employee>> SearchByNameAsync(string name);
    Task<IEnumerable<Employee>> SearchByPhoneNoAsync(string phoneNo);
    Task<IEnumerable<Employee>> SearchByDeptAsync(List<Guid> deptIds);
}