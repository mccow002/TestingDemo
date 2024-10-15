using Library.Models.ViewModels;
using Refit;

namespace Library.ContractTests;

public interface ILibraryApiClient
{
    [Get("/catalogue/books")]
    Task<List<CatalogueItemViewModel>> GetCatalogue();
}