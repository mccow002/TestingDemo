using Library.GoogleBooks.Models;
using Mapster;

namespace Library.Models.Books;

public class BookViewModel : IRegister
{
    public Guid? BookId { get; set; }
    
    public string AuthorName { get; set; }

    public string CoverLink { get; set; }

    public string PublishDate { get; set; }

    public string Description { get; set; }

    public string VolumeId { get; set; }

    public string Publisher { get; set; }

    public string Title { get; set; }

    public string Subject { get; set; }
    
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SearchItem, BookViewModel>()
            .Map(x => x.VolumeId, src => src.Id)
            .Map(x => x.CoverLink, src => src.VolumeInfo.ImageLinks.Thumbnail)
            .Map(x => x.Title, src => src.VolumeInfo.Title)
            .Map(x => x.PublishDate, src => src.VolumeInfo.PublishedDate)
            .Map(x => x.AuthorName, src => src.VolumeInfo.Authors.Count != 0 ? src.VolumeInfo.Authors[0] : string.Empty)
            .Map(x => x.Description, src => src.VolumeInfo.Description)
            .Map(x => x.Publisher, src => src.VolumeInfo.Publisher)
            .Map(x => x.Subject, src => src.VolumeInfo.Categories.Count != 0 ? src.VolumeInfo.Categories[0] : string.Empty)
            .Ignore(x => x.BookId)
            .GenerateMapper(MapType.Map);
    }
}