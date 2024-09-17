using Library.Models.Contracts;
using Library.Models.Users;
using MediatR;

namespace Library.Queries.Users.GetUsers;

public record GetUsersRequest : IRequest<List<UserViewModel>>;

public class GetUsersHandler(IUserRepository userRepository) : IRequestHandler<GetUsersRequest, List<UserViewModel>>
{
    public Task<List<UserViewModel>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        return userRepository.GetUsers(cancellationToken);
    }
}