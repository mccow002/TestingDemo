namespace Library.Domain.Domain;

public class Fine
{
    public Guid FineId { get; set; }

    public decimal Amount { get; set; }

    public DateTime DateIssued { get; set; }

    public string Status { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public Guid CheckoutId { get; set; }

    public Checkout Checkout { get; set; }
}