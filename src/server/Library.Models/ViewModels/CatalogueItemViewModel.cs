using Library.Domain.Domain;
using Library.Models.Books;
using Mapster;

namespace Library.Models.ViewModels;

public class CatalogueItemViewModel : IRegister
{
    public Guid BookId { get; set; }
    
    public BookViewModel Book { get; set; }

    public CheckoutViewModel? Checkout { get; set; }
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, CatalogueItemViewModel>()
            .Map(x => x.Checkout, src => src.Checkouts.FirstOrDefault(x => x.ReturnDate == null))
            .Ignore(x => x.Book)
            .GenerateMapper(MapType.Projection);
    }
}