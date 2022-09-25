using DatingApp.API.Database;
using Microsoft.EntityFrameworkCore;
 
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var connectionString = builder
    .Configuration.GetConnectionString("Default");
// Add services to the container.
 
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
 
// services.AddDbContext<DataContext>(options =>
//     options.UseSqlServer(connectionString));
 
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
services.AddDbContext<DataContex>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);
 
var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseHttpsRedirection();
 
app.UseAuthorization();
 
app.MapControllers();
 
app.Run();