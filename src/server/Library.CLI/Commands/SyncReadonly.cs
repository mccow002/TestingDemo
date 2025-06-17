using System.CommandLine;
using System.CommandLine.Invocation;
using CRA.Domain.Mappers;
using Library.GoogleBooks.Models;
using Library.Models.Contracts;

namespace Library.CLI.Commands;

public class SyncReadonly : Command
{
    public SyncReadonly() : base("sync-readonly")
    { }

    public new class Handler(
        IBookReadonlyRepository repo,
        IGoogleBooksApiClient api,
        IReadonlyStore readonlyStore) : ICommandHandler
    {
        public int Invoke(InvocationContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            var books = await repo.GetAllBooks(context.GetCancellationToken());
            foreach (var book in books)
            {
                var result = await api.GetBookById(book.VolumeId, context.GetCancellationToken());
                var viewModel = result.AdaptToBookViewModel();
                viewModel.BookId = book.BookId;
        
                await readonlyStore.Index(viewModel, context.GetCancellationToken());
            }

            return 0;
        }
    }
}