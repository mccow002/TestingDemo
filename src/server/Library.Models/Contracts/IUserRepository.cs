using Library.Domain.Domain;
using Library.Models.Users;

namespace Library.Models.Contracts;

public interface IUserRepository
{
    Task<List<UserViewModel>> GetUsers(CancellationToken token);
    Task<User?> GetUserByLibraryCard(string cardNumber, CancellationToken token);
}