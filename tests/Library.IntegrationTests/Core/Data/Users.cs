using Library.Domain.Domain;
using Library.Repository;

namespace Library.IntegrationTests.Core.Data;

public static class Users
{
    public static void Seed(LibraryContext context)
    {
        context.Add(User1);
        context.Add(User2);
    }
    
    public static User User1 = User.Create("User 1", "test1@test.com", "1234");
    
    public static User User2 = User.Create("User 2", "test2@test.com", "5678");
}