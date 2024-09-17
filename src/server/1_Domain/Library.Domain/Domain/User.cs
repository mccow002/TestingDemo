namespace Library.Domain.Domain;

public class User
{
    internal User()
    {}
    
    internal User(string name, string email, string cardNumber)
    {
        Name = name;
        Email = email;
        CardNumber = cardNumber;
    }
    
    public Guid UserId { get; internal set; }

    public string Name { get; internal set; }

    public string Email { get; internal set; }

    public string CardNumber { get; internal set; }

    public ICollection<Book> Books { get; internal set; } = [];

    public ICollection<Reservation> Reservations { get; internal set; } = [];

    public ICollection<Fine> Fines { get; internal set; } = [];
    
    public static User Create(string name, string email, string cardNumber)
    {
        return new User(name, email, cardNumber);
    }
}