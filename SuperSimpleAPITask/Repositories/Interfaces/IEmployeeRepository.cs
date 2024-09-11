using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSimpleAPITask.Models;

public interface IEmployeeRepository{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee> GetByIdAsync(Guid id);
    Task<int> CreateAsync(Employee employee);
    Task<int> UpdateAsync(Employee employee);
    Task<int> DeleteAsync(Guid id);
    Task<IEnumerable<Employee>> SearchByName(string name);
    Task<IEnumerable<Employee>> SearchByPhoneNo(string phoneNo);
    Task<IEnumerable<Employee>> SearchByDept(List<Guid> ids);
}