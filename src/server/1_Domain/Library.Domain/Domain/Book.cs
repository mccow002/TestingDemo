using Library.Domain.DomainEvents.Books;

namespace Library.Domain.Domain;

public class Book : Entity
{
    internal Book()
    {}
    
    internal Book(string volumeId)
    {
        VolumeId = volumeId;
        AddDomainEvent(new BookCreatedEvent(this));
    }
    
    public Guid BookId { get; internal set; }

    public string VolumeId { get; internal set; }

    public ICollection<Checkout> Checkouts { get; internal set; } = [];

    public ICollection<Reservation> Reservations { get; internal set; } = [];
    
    public static Book Create(string volumeId)
    {
        return new Book(volumeId);
    }
}