using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SuperSimpleAPITask.Models;

public class EmployeeRepository:IEmployeeRepository{
    private readonly DbConnectionFactory _connectionFactory;
    
    public EmployeeRepository(DbConnectionFactory connectionFactory){
        _connectionFactory=connectionFactory;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(){
        using(var connection = _connectionFactory.CreateConnection()){
            return await connection.QueryAsync<Employee>("SELECT * FROM Employees");
        }
    }

    public async Task<Employee> GetByIdAsync(Guid id){
        using(var connection = _connectionFactory.CreateConnection()){
            return await connection.QuerySingleOrDefaultAsync<Employee>(
                "SELECT * FROM Employees WHERE Id=@Id", new {Id = id}
            );
        }
    }

    public async Task<int> CreateAsync(Employee employee){
        using(var connection = _connectionFactory.CreateConnection()){
            var query = "INSERT INTO Employees (Id, Name, PhoneNo, DeptId) VALUES (@Id, @Name, @PhoneNo, @DeptId)";
            return await connection.ExecuteAsync(query, employee);
        }
    }

    public async Task<int> UpdateAsync(Employee employee){
        using(var connection = _connectionFactory.CreateConnection()){
            var query = "UPDATE Employees SET Name=@Name, PhoneNo=@PhoneNo, DeptId=@DeptId WHERE Id=@Id";
            return await connection.ExecuteAsync(query, employee);
        }
    }

    public async Task<int> DeleteAsync(Guid id){
        using(var connection = _connectionFactory.CreateConnection()){
            var query = "DELETE FROM Employees WHERE Id=@Id";
            return await connection.ExecuteAsync(query, new {Id = id});
        }
    }

    public async Task<IEnumerable<Employee>> SearchByName(string name){
        using(var connection = _connectionFactory.CreateConnection()){
            var query = "SELECT * FROM Employees WHERE Name LIKE @Name";
            return await connection.QueryAsync<Employee>(query, new {Name = $"%{name}%"});
        }
    }

    public async Task<IEnumerable<Employee>> SearchByPhoneNo(string phoneNo){
        using(var connection = _connectionFactory.CreateConnection()){
            var query = "SELECT * FROM Employees WHERE PhoneNo LIKE @PhoneNo";
            return await connection.QueryAsync<Employee>(query, new {PhoneNo= $"%{phoneNo}%"});
        }
    }

    public async Task<IEnumerable<Employee>> SearchByDept(List<Guid> ids){
        using(var connection = _connectionFactory.CreateConnection()){
            var query = "SELECT * FROM Employees WHERE DeptId IN @Ids";
            return await connection.QueryAsync<Employee>(query, new {Ids= ids});
        }
    }
}