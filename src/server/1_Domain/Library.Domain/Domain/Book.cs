using Library.Domain.DomainEvents.Books;

namespace Library.Domain.Domain;

public class Book : Entity
{
    internal Book()
    {}
    
    internal Book(string isbn)
    {
        Isbn = isbn;
        AddDomainEvent(new BookCreatedEvent(this));
    }
    
    public Guid BookId { get; internal set; }

    public string Isbn { get; internal set; }

    public ICollection<Checkout> Checkouts { get; internal set; } = [];

    public ICollection<Reservation> Reservations { get; internal set; } = [];
    
    public static Book Create(string isbn)
    {
        return new Book(isbn);
    }
}