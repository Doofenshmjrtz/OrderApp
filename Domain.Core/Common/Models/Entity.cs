using Domain.Core.Common.Abstractions;

namespace Domain.Core.Common.Models;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public static Entity Create() => throw new NotImplementedException("This method should be overridden in a child class.");
    
    public List<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}