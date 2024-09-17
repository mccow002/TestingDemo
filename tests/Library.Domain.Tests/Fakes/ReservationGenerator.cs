using Bogus;
using Library.Domain.Domain;

namespace Library.Domain.Tests.Fakes;

public sealed class ReservationGenerator : Faker<Reservation>
{
    public static ReservationGenerator Instance => new();
    
    public ReservationGenerator()
    {
        CustomInstantiator(_ => new Reservation());
        RuleFor(x => x.ReservationId, Guid.NewGuid);
        RuleFor(x => x.HoldDate, f => f.Date.Past());
        RuleFor(x => x.UserId, Guid.NewGuid);
        RuleFor(x => x.ReservationStatusId, ReservationStatus.Reserved.ReservationStatusId);
    }
}