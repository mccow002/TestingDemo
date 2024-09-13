using Refit;

namespace Library.OpenLibraryApi.Models;

public class SearchParams
{
    [AliasAs("q")]
    public string Query { get; set; }

    [AliasAs("sort")]
    public string Sort => "new";
}