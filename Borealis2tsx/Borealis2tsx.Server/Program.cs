using Borealis2tsx.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IReadDataPortService, ReadDataPortService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// DB connectionstring
var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<PersonDbContext>(options =>
    options.UseSqlServer(connection));


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");


// test person table in DB
app.MapGet("/Person", (PersonDbContext context) =>
{
    return context.Person.ToList();
})
.WithName("GetPersons");

app.MapPost("/Person", (Person person, PersonDbContext context) =>
{
    context.Add(person);
    context.SaveChanges();
})
.WithName("CreatePerson");

app.Run();


public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class PersonDbContext : DbContext
{
    public PersonDbContext(DbContextOptions<PersonDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Person { get; set; }
}
