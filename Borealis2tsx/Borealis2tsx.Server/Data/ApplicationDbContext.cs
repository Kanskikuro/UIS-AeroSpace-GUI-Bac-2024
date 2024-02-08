using Borealis2tsx.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Borealis2tsx.Server.Data;

public class PersonDbContext : DbContext
{
    public PersonDbContext(DbContextOptions<PersonDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Person { get; set; }
}

