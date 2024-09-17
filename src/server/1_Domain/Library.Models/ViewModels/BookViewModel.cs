using Library.OpenLibraryApi.Models;
using Mapster;

namespace Library.Models.Books;

public class BookViewModel : IRegister
{
    public Guid? BookId { get; set; }
    
    public string AuthorName { get; set; }

    public string CoverEditionKey { get; set; }

    public int FirstPublishYear { get; set; }

    public string FirstSentence { get; set; }

    public string Isbn { get; set; }

    public string Publisher { get; set; }

    public string Title { get; set; }

    public string Subject { get; set; }
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Doc, BookViewModel>()
            .Map(x => x.Isbn, src => src.Isbn[0])
            .Map(x => x.AuthorName, src => src.AuthorName.Count != 0 ? src.AuthorName[0] : string.Empty)
            .Map(x => x.FirstSentence, src => src.FirstSentence.Count != 0 ? src.FirstSentence[0] : string.Empty)
            .Map(x => x.Publisher, src => src.Publisher.Count != 0 ? src.Publisher[0] : string.Empty)
            .Map(x => x.Subject, src => src.Subject.Count != 0 ? src.Subject[0] : string.Empty)
            .Ignore(x => x.BookId)
            .GenerateMapper(MapType.Map);
    }
}