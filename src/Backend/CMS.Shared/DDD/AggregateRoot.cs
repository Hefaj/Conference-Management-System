using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Shared.DDD;

public abstract class AggregateRoot
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private readonly List<IDomainEvent> _domainEvent = [];

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvent => _domainEvent;

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvent.Add(domainEvent);
    }

    protected void ClearDomainEvents()
    {
        _domainEvent.Clear();
    }
}
