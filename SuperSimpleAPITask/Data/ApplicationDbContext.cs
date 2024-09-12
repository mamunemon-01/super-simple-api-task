using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SuperSimpleAPITask.Models;

public class ApplicationDbContext : DbContext {

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optoins) : base(optoins){}

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
}