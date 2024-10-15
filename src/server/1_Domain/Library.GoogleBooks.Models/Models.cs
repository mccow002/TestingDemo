namespace Library.GoogleBooks.Models;

public class SearchResults
{
    public int TotalItems { get; set; }
    public List<SearchItem> Items { get; set; } = [];
}

public class SearchItem
{
    public string Kind { get; set; }
    public string Id { get; set; }
    public string Etag { get; set; }
    public string SelfLink { get; set; }
    public VolumeInfo VolumeInfo { get; set; }
}

public class VolumeInfo
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public List<string> Authors { get; set; } = [];
    public string Publisher { get; set; }
    public string PublishedDate { get; set; }
    public string Description { get; set; }
    public List<IndustryIdentifier> IndustryIdentifiers { get; set; } = [];
    public int PageCount { get; set; }
    public string PrintType { get; set; }
    public List<string> Categories { get; set; } = [];
    public string MaturityRating { get; set; }
    public bool AllowAnonLogging { get; set; }
    public string ContentVersion { get; set; }
    public ImageLinks ImageLinks { get; set; }
    public string Language { get; set; }
    public string PreviewLink { get; set; }
    public string InfoLink { get; set; }
    public string CanonicalVolumeLink { get; set; }
}

public class ImageLinks
{
    public string SmallThumbnail { get; set; }
    public string Thumbnail { get; set; }
}

public class IndustryIdentifier
{
    public string Type { get; set; }
    public string Identifier { get; set; }
}