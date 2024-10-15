using Bogus;
using Library.Domain.Domain;

namespace Library.Domain.Tests.Fakes;

public sealed class BookGenerator : Faker<Book>
{
    public BookGenerator()
    {
        var checkoutGenerator = new CheckoutGenerator();
        
        CustomInstantiator(_ => new Book());
        RuleFor(x => x.VolumeId, f => f.Commerce.Ean13());
        RuleFor(x => x.BookId, Guid.NewGuid);
        RuleFor(x => x.Checkouts, f => [checkoutGenerator.Generate()]);
        RuleFor(x => x.Reservations, f => new List<Reservation>());
    }
}