using Library.Commands.Books.BookSyncReadonly;
using Library.Domain.DomainEvents.Books;
using MassTransit;
using MediatR;

namespace Library.DomainEvents.Books.BookCreated;

public class BookCreatedBeginReadonlySync(IPublishEndpoint publishEndpoint) : INotificationHandler<BookCreatedEvent>
{
    public async Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
    {
        await publishEndpoint.Publish(new BookSyncReadonlyRequest(notification.Book.BookId), cancellationToken);
    }
}