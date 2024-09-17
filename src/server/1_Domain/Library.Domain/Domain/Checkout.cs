namespace Library.Domain.Domain;

public class Checkout
{
    internal Checkout()
    { }
    
    internal Checkout(Guid bookId, Guid userId)
    {
        BookId = bookId;
        UserId = userId;
        CheckoutTime = DateTime.Now;
        DueDate = CheckoutTime.AddDays(14);
    }
    
    public Guid CheckoutId { get; internal set; }

    public DateTime CheckoutTime { get; internal set; }

    public DateTime DueDate { get; internal set; }

    public DateTime? ReturnDate { get; internal set; }

    public Guid BookId { get; internal set; }

    public Book Book { get; internal set; }

    public Guid UserId { get; internal set; }

    public User User { get; internal set; }

    public Guid? FineId { get; internal set; }

    public Fine? Fine { get; internal set; }

    public void Return(TimeProvider timeProvider)
    {
        ReturnDate = timeProvider.GetLocalNow().DateTime;
    }

    public void AddFine(decimal fineAmount, TimeProvider timeProvider)
    {
        var fine = Fine.Create(fineAmount, timeProvider);
        Fine = fine;
    }
    
    public static Checkout Create(Guid bookId, Guid userId)
    {
        return new Checkout(bookId, userId);
    }
}