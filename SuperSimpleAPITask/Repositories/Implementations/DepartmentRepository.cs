using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperSimpleAPITask.Repositories.Implementations;
using SuperSimpleAPITask.Models;
using Microsoft.EntityFrameworkCore;

public class DepartmentRepository: GenericRepository<Department>, IDepartmentRepository{
    private readonly ApplicationDbContext _context;
    
    public DepartmentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    //public async Task<IEnumerable<Department>> GetAllAsync(){
    //    using(var connection = _connectionFactory.CreateConnection()){
    //        return await connection.QueryAsync<Department>("SELECT * FROM Departments");
    //    }
    //}

    //public async Task<Department> GetByIdAsync(Guid id){
    //    using(var connection = _connectionFactory.CreateConnection()){
    //        return await connection.QuerySingleOrDefaultAsync<Department>(
    //            "SELECT * FROM Departments WHERE Id=@Id", new {Id = id}
    //        );
    //    }
    //}

    //public async Task<int> CreateAsync(Department Department){
    //    using(var connection = _connectionFactory.CreateConnection()){
    //        var query = "INSERT INTO Departments (Id, Name) VALUES (@Id, @Name)";
    //        return await connection.ExecuteAsync(query, Department);
    //    }
    //}

    //public async Task<int> UpdateAsync(Department Department){
    //    using(var connection = _connectionFactory.CreateConnection()){
    //        var query = "UPDATE Departments SET Name=@Name WHERE Id=@Id";
    //        return await connection.ExecuteAsync(query, Department);
    //    }
    //}

    //public async Task<int> DeleteAsync(Guid id){
    //    using(var connection = _connectionFactory.CreateConnection()){
    //        var query = "DELETE FROM Departments WHERE Id=@Id";
    //        return await connection.ExecuteAsync(query, new {Id = id});
    //    }
    //}

    public async Task<IEnumerable<Department>> SearchByName(string name){
        return await _context.Departments
            .FromSqlRaw($"SELECT * FROM Departments WHERE Name LIKE '%{name}%'")
            .ToListAsync();
    }
}