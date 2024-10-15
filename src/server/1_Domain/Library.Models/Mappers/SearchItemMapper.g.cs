using Library.GoogleBooks.Models;
using Library.Models.Books;

namespace CRA.Domain.Mappers
{
    public static partial class SearchItemMapper
    {
        public static BookViewModel AdaptToBookViewModel(this SearchItem p1)
        {
            return p1 == null ? null : new BookViewModel()
            {
                AuthorName = p1.VolumeInfo.Authors.Count != 0 ? p1.VolumeInfo.Authors[0] : string.Empty,
                CoverLink = p1.VolumeInfo == null ? null : (p1.VolumeInfo.ImageLinks == null ? null : p1.VolumeInfo.ImageLinks.Thumbnail),
                PublishDate = p1.VolumeInfo == null ? null : p1.VolumeInfo.PublishedDate,
                Description = p1.VolumeInfo == null ? null : p1.VolumeInfo.Description,
                VolumeId = p1.Id,
                Publisher = p1.VolumeInfo == null ? null : p1.VolumeInfo.Publisher,
                Title = p1.VolumeInfo == null ? null : p1.VolumeInfo.Title,
                Subject = p1.VolumeInfo.Categories.Count != 0 ? p1.VolumeInfo.Categories[0] : string.Empty
            };
        }
    }
}