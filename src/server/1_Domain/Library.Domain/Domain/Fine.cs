namespace Library.Domain.Domain;

public class Fine
{
    internal Fine()
    { }

    internal Fine(decimal amount, TimeProvider timeProvider)
    {
        Amount = amount;
        Status = "Unpaid";
        DateIssued = timeProvider.GetLocalNow().DateTime;
    }
    
    public Guid FineId { get; internal set; }

    public decimal Amount { get; internal set; }

    public DateTime DateIssued { get; internal set; }

    public string Status { get; internal set; }

    public Guid UserId { get; internal set; }

    public User User { get; internal set; }

    public Guid CheckoutId { get; internal set; }

    public Checkout Checkout { get; internal set; }
    
    public static Fine Create(decimal amount, TimeProvider timeProvider)
    {
        return new Fine(amount, timeProvider);
    }
}