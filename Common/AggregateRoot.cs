using System.Collections.Generic;

namespace ddd_tutorial_atm.Logic.Common
{
  public abstract class AggregateRoot : Entity
  {
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    protected void AddDomainEvent(IDomainEvent newEvent)
    {
      _domainEvents.Add(newEvent);
    }

    public void ClearDomainEvents()
    {
      _domainEvents.Clear();
    }
  }
}