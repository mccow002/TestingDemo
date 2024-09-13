using Library.Domain.Domain;
using MediatR;

namespace Library.Domain.DomainEvents.Books;

public record BookCreatedEvent(Book Book) : INotification;