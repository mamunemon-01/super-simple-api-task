using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSimpleAPITask.Models;
using SuperSimpleAPITask.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository{
    private readonly ApplicationDbContext _context;
    public EmployeeRepository(ApplicationDbContext context) : base(context){
        _context = context;
    }

    //public async Task<IEnumerable<Employee>> GetAllAsync(){
    //    using(var connection = _connectionFactory.CreateConnection()){
    //        return await connection.QueryAsync<Employee>("SELECT * FROM Employees");
    //    }
    //}

    //public async Task<Employee> GetByIdAsync(Guid id){
    //    using(var connection = _connectionFactory.CreateConnection()){
    //        return await connection.QuerySingleOrDefaultAsync<Employee>(
    //            "SELECT * FROM Employees WHERE Id=@Id", new {Id = id}
    //        );
    //    }
    //}

    //public async Task<int> CreateAsync(Employee employee){
    //    using(var connection = _connectionFactory.CreateConnection()){
    //        var query = "INSERT INTO Employees (Id, Name, PhoneNo, DeptId) VALUES (@Id, @Name, @PhoneNo, @DeptId)";
    //        return await connection.ExecuteAsync(query, employee);
    //    }
    //}

    //public async Task<int> UpdateAsync(Employee employee){
    //    using(var connection = _connectionFactory.CreateConnection()){
    //        var query = "UPDATE Employees SET Name=@Name, PhoneNo=@PhoneNo, DeptId=@DeptId WHERE Id=@Id";
    //        return await connection.ExecuteAsync(query, employee);
    //    }
    //}

    //public async Task<int> DeleteAsync(Guid id){
    //    using(var connection = _connectionFactory.CreateConnection()){
    //        var query = "DELETE FROM Employees WHERE Id=@Id";
    //        return await connection.ExecuteAsync(query, new {Id = id});
    //    }
    //}

    public async Task<IEnumerable<Employee>> SearchByNameAsync(string name){
        return await _context.Employees
            .FromSqlRaw($"SELECT * FROM Employees WHERE Name LIKE '%{name}%'")
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> SearchByPhoneNoAsync(string phoneNo){
        return await _context.Employees
            .FromSqlRaw($"SELECT * FROM Employees WHERE PhoneNo LIKE '%{phoneNo}%'")
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> SearchByDeptAsync(List<Guid> deptIds){
        if (deptIds == null || deptIds.Count == 0)
        {
            return Enumerable.Empty<Employee>();
        }

        var sqlParams = deptIds.Select((id, index) => new SqlParameter($"@id{index}", id)).ToArray();
        //var setOfParams = string.Join(",", sqlParams.Select(p => p.ParameterName));
        var placeholders = string.Join(",", Enumerable.Range(0, deptIds.Count).Select(i => $"@id{i}"));

        string query = $"SELECT * FROM Employees WHERE DeptId IN ({placeholders})";
        return await _context.Employees
            .FromSqlRaw(query, sqlParams)
            .ToListAsync();
    }
}