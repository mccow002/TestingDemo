using Library.Domain.Domain;
using Mapster;

namespace Library.Models.ViewModels;

public class ReservationViewModel : IRegister
{
    public Guid ReservationId { get; set; }

    public DateTime HoldDate { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Reservation, ReservationViewModel>()
            .Map(x => x.Name, src => src.User.Name)
            .GenerateMapper(MapType.Map | MapType.Projection);
    }
}