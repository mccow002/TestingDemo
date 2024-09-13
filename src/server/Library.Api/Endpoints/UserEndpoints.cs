using Library.Commands.Users.CreateUser;
using Library.Queries.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var users = endpoints.MapGroup("/users");

        users.MapGet("/", async (IMediator mediator, CancellationToken token) =>
            Results.Ok(await mediator.Send(new GetUsersRequest(), token)));

        users.MapPost("/", async ([FromBody]CreateUserRequest request, IMediator mediator, CancellationToken token) =>
            Results.Ok(await mediator.Send(request, token)));

        return endpoints;
    }
}