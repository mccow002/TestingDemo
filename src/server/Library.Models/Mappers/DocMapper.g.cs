using Library.Models.Books;
using Library.OpenLibraryApi.Models;

namespace CRA.Domain.Mappers
{
    public static partial class DocMapper
    {
        public static BookViewModel AdaptToBookViewModel(this Doc p1)
        {
            return p1 == null ? null : new BookViewModel()
            {
                AuthorName = p1.AuthorName.Count != 0 ? p1.AuthorName[0] : string.Empty,
                CoverEditionKey = p1.CoverEditionKey,
                FirstPublishYear = p1.FirstPublishYear,
                FirstSentence = p1.FirstSentence.Count != 0 ? p1.FirstSentence[0] : string.Empty,
                Isbn = p1.Isbn[0],
                Publisher = p1.Publisher.Count != 0 ? p1.Publisher[0] : string.Empty,
                Title = p1.Title,
                Subject = p1.Subject.Count != 0 ? p1.Subject[0] : string.Empty
            };
        }
    }
}