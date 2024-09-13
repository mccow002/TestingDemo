﻿using Library.Commands.Books.CheckoutBook;
using Library.Queries.Books.GetAllBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Endpoints;

public static class CatalogueEndpoints
{
    public static IEndpointRouteBuilder MapCatalogueEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var catalogue = endpoints.MapGroup("catalogue");
        
        catalogue.MapGet("/books", async (IMediator mediator, CancellationToken token) =>
        {
            var books = await mediator.Send(new GetAllBooksRequest(), token);
            return Results.Ok(books);
        });
        
        catalogue.MapPost("/checkout", async ([FromBody]CheckoutBookRequest request, IMediator mediator, CancellationToken token) => 
        Results.Ok((object?)await mediator.Send(request, token)));
        
        return endpoints;
    }
}