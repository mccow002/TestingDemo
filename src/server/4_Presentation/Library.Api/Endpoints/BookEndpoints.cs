using Library.Commands.Books.AddBook;
using Library.Queries.Books.BookSearch;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints;

public static class BookEndpoints
{
    public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder builder)
    {
        var book = builder.MapGroup("books");
        
        book.MapPost("/", async (AddBookRequest request, IMediator mediator, CancellationToken token) =>
        {
            await mediator.Send(request, token);
            return Results.Ok();
        });
        
        book.MapGet("search", async ([FromQuery]string searchTerm, IMediator mediator, CancellationToken token) =>
        {
            var request = new BookSearchRequest(searchTerm);
            var response = await mediator.Send(request, token);
            return Results.Ok(response);
        });

        return builder;
    }
}