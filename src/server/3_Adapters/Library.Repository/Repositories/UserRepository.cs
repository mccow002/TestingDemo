using CRA.Domain.Mappers;
using Library.Domain.Domain;
using Library.Models.Contracts;
using Library.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Repositories;

public class UserRepository(LibraryContext context) : IUserRepository
{
    public async Task<List<UserViewModel>> GetUsers(CancellationToken token)
    {
        return await context.Set<User>()
            .Select(UserMapper.ProjectToViewModel)
            .ToListAsync(token);
    }
    
    public async Task<User?> GetUserByLibraryCard(string cardNumber, CancellationToken token)
    {
        return await context.Set<User>()
            .FirstOrDefaultAsync(x => x.CardNumber == cardNumber, token);
    }
}