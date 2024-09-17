using Library.IntegrationTests.Core.Data;
using Library.Repository;

namespace Library.IntegrationTests.Core;

public static class DbInitializer
{
    public static async Task Initialize(LibraryContext context)
    {
        await context.Database.EnsureCreatedAsync();
        
        Users.Seed(context);
        await context.SaveChangesAsync();
    }
}