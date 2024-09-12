using Refit;

namespace Library.OpenLibraryApi;

public class SearchParams
{
    [AliasAs("q")]
    public string Query { get; set; }

    [AliasAs("sort")]
    public string Sort => "new";
}