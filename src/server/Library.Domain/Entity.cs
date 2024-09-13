using MediatR;

namespace Library.Domain;

public abstract class Entity
{
    private readonly List<INotification> _domainEvents = new();
    
    protected void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public List<INotification> DomainEvents => _domainEvents.AsReadOnly().ToList();
}