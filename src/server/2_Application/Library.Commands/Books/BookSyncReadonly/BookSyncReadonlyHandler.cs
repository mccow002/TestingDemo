using CRA.Domain.Mappers;
using Library.GoogleBooks.Models;
using Library.Models.Contracts;
using MediatR;

namespace Library.Commands.Books.BookSyncReadonly;

public record BookSyncReadonlyRequest(Guid BookId) : IRequest;

public class BookSyncReadonlyHandler(
    IGoogleBooksApiClient api, 
    IBookRepository bookRepository,
    IReadonlyStore readonlyStore) : IRequestHandler<BookSyncReadonlyRequest>
{
    public async Task Handle(BookSyncReadonlyRequest request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetById(request.BookId, cancellationToken);
        var result = await api.GetBookById(book.VolumeId, "AIzaSyCC478lljstyd4uqJQo-Kudqeddx5Osx2o");
        
        var viewModel = result.AdaptToBookViewModel();
        viewModel.BookId = book.BookId;
        
        await readonlyStore.Index(viewModel, cancellationToken);
    }
}