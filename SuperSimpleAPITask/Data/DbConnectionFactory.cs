using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class DbConnectionFactory{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DbConnectionFactory(IConfiguration configuration){
        _configuration=configuration;
        _connectionString=configuration.GetConnectionString("DBConnString");
    }

    public IDbConnection CreateConnection(){
        return new SqlConnection(_connectionString);
    }
}