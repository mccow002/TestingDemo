namespace Library.Domain.Domain;

public class Book
{
    public Guid BookId { get; set; }

    public string Isbn { get; set; }

    public ICollection<Checkout> Checkouts { get; set; } = [];

    public ICollection<Reservation> Reservations { get; set; } = [];
}