namespace Library.Repository.Repositories;

public class LibraryRepository(LibraryContext context) : ILibraryRepository
{
    
    public void Add<T>(T entity) where T : class
    {
        context.Set<T>().Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        context.Set<T>().Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        context.Set<T>().Remove(entity);
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}