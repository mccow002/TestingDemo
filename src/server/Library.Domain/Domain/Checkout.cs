namespace Library.Domain.Domain;

public class Checkout
{
    public Guid CheckoutId { get; set; }

    public DateTime CheckoutTime { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public Guid BookId { get; set; }

    public Book Book { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }
}