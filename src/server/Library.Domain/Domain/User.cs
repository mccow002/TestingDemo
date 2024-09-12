namespace Library.Domain.Domain;

public class User
{
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string CardNumber { get; set; }

    public ICollection<Book> Books { get; set; } = [];

    public ICollection<Reservation> Reservations { get; set; } = [];

    public ICollection<Fine> Fines { get; set; } = [];
}