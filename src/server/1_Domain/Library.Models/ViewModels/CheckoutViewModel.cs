using Library.Domain.Domain;
using Mapster;

namespace Library.Models.ViewModels;

public class CheckoutViewModel : IRegister
{
    public Guid CheckoutId { get; set; }
    
    public DateTime CheckoutTime { get; set; }

    public DateTime DueDate { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Checkout, CheckoutViewModel>()
            .Map(x => x.Name, src => src.User.Name)
            .GenerateMapper(MapType.Projection | MapType.Map);
    }
}