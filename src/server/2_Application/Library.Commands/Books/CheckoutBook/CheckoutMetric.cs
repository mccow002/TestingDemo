using System.Diagnostics.Metrics;

namespace Library.Commands.Books.CheckoutBook;

public class CheckoutMetric
{
    private readonly Counter<int> _checkoutMeter;
    
    public CheckoutMetric(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("LibraryCheckout");
        _checkoutMeter = meter.CreateCounter<int>("books.checkedout");
    }

    public void Checkout(Guid bookId, Guid userId)
    {
        _checkoutMeter.Add(1, new []
        {
            new KeyValuePair<string, object?>("bookId", bookId),
            new KeyValuePair<string, object?>("userId", userId)
        });
    }
}