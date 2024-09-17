using Library.Commands.Books.BookSyncReadonly;
using MassTransit;
using MediatR;

namespace Library.MessageProcessor.Consumers;

public class BookSyncReadonlyConsumer(IMediator mediator) : IConsumer<BookSyncReadonlyRequest>
{
    public async Task Consume(ConsumeContext<BookSyncReadonlyRequest> context)
    {
        await mediator.Send(context.Message, context.CancellationToken);
    }
}