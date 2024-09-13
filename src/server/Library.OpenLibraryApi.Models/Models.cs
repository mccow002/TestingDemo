using System.Text.Json.Serialization;

namespace Library.OpenLibraryApi.Models;

public class Doc
{
    [JsonPropertyName("author_alternative_name")]
    public List<string> AuthorAlternativeName { get; set; } = [];

    [JsonPropertyName("author_key")]
    public List<string> AuthorKey { get; set; } = [];

    [JsonPropertyName("author_name")]
    public List<string> AuthorName { get; set; } = [];

    [JsonPropertyName("cover_edition_key")]
    public string CoverEditionKey { get; set; }
    
    [JsonPropertyName("cover_i")]
    public int CoverImage { get; set; }

    [JsonPropertyName("ebook_access")]
    public string EbookAccess { get; set; }

    [JsonPropertyName("ebook_count_i")]
    public int EbookCountI { get; set; }

    [JsonPropertyName("edition_count")]
    public int EditionCount { get; set; }

    [JsonPropertyName("edition_key")]
    public List<string> EditionKey { get; set; } = [];

    [JsonPropertyName("first_publish_year")]
    public int FirstPublishYear { get; set; }

    [JsonPropertyName("first_sentence")]
    public List<string> FirstSentence { get; set; } = [];

    [JsonPropertyName("has_fulltext")]
    public bool HasFulltext { get; set; }

    [JsonPropertyName("isbn")]
    public List<string> Isbn { get; set; } = [];

    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("language")]
    public List<string> Language { get; set; } = [];

    [JsonPropertyName("last_modified_i")]
    public int LastModifiedI { get; set; }

    [JsonPropertyName("lcc")]
    public List<string> Lcc { get; set; } = [];

    [JsonPropertyName("public_scan_b")]
    public bool PublicScanB { get; set; }

    [JsonPropertyName("publish_date")]
    public List<string> PublishDate { get; set; } = [];

    [JsonPropertyName("publish_year")]
    public List<int> PublishYear { get; set; } = [];

    [JsonPropertyName("publisher")]
    public List<string> Publisher { get; set; } = [];

    [JsonPropertyName("seed")]
    public List<string> Seed { get; set; } = [];

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("title_suggest")]
    public string TitleSuggest { get; set; }

    [JsonPropertyName("title_sort")]
    public string TitleSort { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("subject")]
    public List<string> Subject { get; set; } = [];

    [JsonPropertyName("publisher_facet")]
    public List<string> PublisherFacet { get; set; } = [];

    [JsonPropertyName("subject_facet")]
    public List<string> SubjectFacet { get; set; } = [];

    [JsonPropertyName("_version_")]
    public long Version { get; set; }

    [JsonPropertyName("lcc_sort")]
    public string LccSort { get; set; }

    [JsonPropertyName("author_facet")]
    public List<string> AuthorFacet { get; set; } = [];

    [JsonPropertyName("subject_key")]
    public List<string> SubjectKey { get; set; } = [];
}

public class SearchResults
{
    [JsonPropertyName("numFound")]
    public int NumFound { get; set; }

    [JsonPropertyName("start")]
    public int Start { get; set; }

    [JsonPropertyName("numFoundExact")]
    public bool NumFoundExact { get; set; }

    [JsonPropertyName("docs")] 
    public List<Doc> Docs { get; set; } = [];
}