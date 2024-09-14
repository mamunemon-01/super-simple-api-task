using SuperSimpleAPITask.Repositories.Interfaces;
using SuperSimpleAPITask.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DB cotext with connection string specified
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnString")));

// Add service associated with the unit-of-work pattern
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Allow any origin, any header and any method
builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiTaskDataCors",
    policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ApiTaskDataCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
