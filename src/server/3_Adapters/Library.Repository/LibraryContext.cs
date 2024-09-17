using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}