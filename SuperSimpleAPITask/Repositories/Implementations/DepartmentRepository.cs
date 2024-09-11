using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SuperSimpleAPITask.Models;

public class DepartmentRepository:IDepartmentRepository{
    private readonly DbConnectionFactory _connectionFactory;
    
    public DepartmentRepository(DbConnectionFactory connectionFactory){
        _connectionFactory=connectionFactory;
    }

    public async Task<IEnumerable<Department>> GetAllAsync(){
        using(var connection = _connectionFactory.CreateConnection()){
            return await connection.QueryAsync<Department>("SELECT * FROM Departments");
        }
    }

    public async Task<Department> GetByIdAsync(Guid id){
        using(var connection = _connectionFactory.CreateConnection()){
            return await connection.QuerySingleOrDefaultAsync<Department>(
                "SELECT * FROM Departments WHERE Id=@Id", new {Id = id}
            );
        }
    }

    public async Task<int> CreateAsync(Department Department){
        using(var connection = _connectionFactory.CreateConnection()){
            var query = "INSERT INTO Departments (Id, Name) VALUES (@Id, @Name)";
            return await connection.ExecuteAsync(query, Department);
        }
    }

    public async Task<int> UpdateAsync(Department Department){
        using(var connection = _connectionFactory.CreateConnection()){
            var query = "UPDATE Departments SET Name=@Name WHERE Id=@Id";
            return await connection.ExecuteAsync(query, Department);
        }
    }

    public async Task<int> DeleteAsync(Guid id){
        using(var connection = _connectionFactory.CreateConnection()){
            var query = "DELETE FROM Departments WHERE Id=@Id";
            return await connection.ExecuteAsync(query, new {Id = id});
        }
    }

    public async Task<IEnumerable<Department>> SearchByName(string name){
        using(var connection = _connectionFactory.CreateConnection()){
            var query = "SELECT * FROM Departments WHERE Name LIKE @Name";
            return await connection.QueryAsync<Department>(query, new {Name = $"%{name}%"});
        }
    }
}