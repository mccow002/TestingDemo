using Library.Domain.Domain;
using Mapster;

namespace Library.Models.Users;

public class UserViewModel : IRegister
{
    public Guid UserId { get; set; }
    
    public string Name { get; set; }

    public string Email { get; set; }

    public string CardNumber { get; set; }
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserViewModel>()
            .GenerateMapper(MapType.Projection | MapType.Map);
    }
}