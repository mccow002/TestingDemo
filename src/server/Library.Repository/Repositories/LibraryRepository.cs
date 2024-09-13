using Library.Domain;
using Library.Domain.Contracts;
using Library.Domain.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Repositories;

public class LibraryRepository(LibraryContext context, IMediator mediator) : ILibraryRepository
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
        var entities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();
        
        var events = entities.SelectMany(x => x.Entity.DomainEvents).ToList();
        
        var results = await context.SaveChangesAsync(cancellationToken);

        foreach (var @event in events)
        {
            await mediator.Publish(@event, cancellationToken);
        }

        return results;
    }
}