namespace Library.Models.Contracts;

public interface IReadonlyStore
{
    Task Index<T>(T document, CancellationToken token) where T : class;
}