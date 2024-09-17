using Library.Domain.Domain;
using Library.Models.Books;
using Mapster;

namespace Library.Models.ViewModels;

public class CatalogueItemViewModel : IRegister
{
    public Guid BookId { get; set; }
    
    public BookViewModel Book { get; set; }

    public CheckoutViewModel? Checkout { get; set; }

    public List<ReservationViewModel> Reservations { get; set; } = [];
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, CatalogueItemViewModel>()
            .Map(x => x.Checkout, src => src.Checkouts.FirstOrDefault(x => x.ReturnDate == null))
            .Map(x => x.Reservations, src => src.Reservations.Where(x => x.ReservationStatusId == ReservationStatus.Reserved.ReservationStatusId))
            .Ignore(x => x.Book)
            .GenerateMapper(MapType.Projection);
    }
}