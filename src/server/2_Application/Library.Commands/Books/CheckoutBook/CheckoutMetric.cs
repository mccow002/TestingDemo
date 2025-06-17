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

    public void Checkout()
    {
        
    }
}