using Bogus;
using Library.Domain.Domain;

namespace Library.Domain.Tests.Fakes;

public sealed class CheckoutGenerator : Faker<Checkout>
{
    public CheckoutGenerator()
    {
        CustomInstantiator(_ => new Checkout());
        RuleFor(x => x.CheckoutId, Guid.NewGuid);
        RuleFor(x => x.CheckoutTime, f => f.Date.Past());
        RuleFor(x => x.DueDate, (f, x) => x.CheckoutTime.AddDays(14));
        Ignore(x => x.ReturnDate);
    }
}