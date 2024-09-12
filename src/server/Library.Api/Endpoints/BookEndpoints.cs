namespace Library.Api.Endpoints;

public static class BookEndpoints
{
    public static IEndpointRouteBuilder AddBookEndpoints(this IEndpointRouteBuilder builder)
    {
        var book = builder.MapGroup("books");
        book.MapPost("/", async (IBookService service, CreateBookRequest request) =>
        {
            var book = await service.CreateBookAsync(request);
            return Results.CreatedAt($"/books/{book.Id}", book);
        });
    }
}