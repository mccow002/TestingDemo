using CRA.Domain.Mappers;
using Library.Domain.Contracts;
using Library.Domain.Domain;
using Library.Models.Users;
using MediatR;

namespace Library.Commands.Users.CreateUser;

public record CreateUserRequest(string Name, string Email, string CardNumber) : IRequest<UserViewModel>;

public class CreateUserHandler(ILibraryRepository repository) : IRequestHandler<CreateUserRequest, UserViewModel>
{
    public async Task<UserViewModel> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Name, request.Email, request.CardNumber);
        repository.Add(user);
        await repository.SaveChangesAsync(cancellationToken);

        return user.AdaptToViewModel();
    }
}