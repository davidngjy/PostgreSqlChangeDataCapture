using ChangeDataCapture.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChangeDataCapture.WebApi.Persistence;

public class ExampleDbContext : DbContext
{
    public DbSet<Example> Examples => Set<Example>();

    public ExampleDbContext(DbContextOptions<ExampleDbContext> options)
        : base(options) { }
}
