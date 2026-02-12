namespace DiarioEntrenamiento.Domain.Abstractions;

public abstract class Entity<TId>
{
    private readonly List<IDomainEvents> _domainEvents = new();
    protected Entity(TId id)
    {
        Id = id;
    }
    public TId Id { get; init; }
    protected void RaiseDomainEvents(IDomainEvents domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    public IReadOnlyCollection<IDomainEvents> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }
}

