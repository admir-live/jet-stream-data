using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JetStreamData.Kernel.Domain.Interfaces;

namespace JetStreamData.Kernel.Domain.Entities;

public abstract class AggregateRoot<TEntityKey> : Entity<TEntityKey>, IAggregateRoot, IHasGlobalEvents, IHasDomainEvents where TEntityKey : IEquatable<TEntityKey>
{
    [JsonIgnore] private readonly List<BaseDomainEvent> _domainEvents = [];
    [JsonIgnore] private readonly List<BaseGlobalEvent> _globalEvents = [];

    [Timestamp] public byte[] RowVersion { get; set; }

    public virtual IReadOnlyList<BaseDomainEvent> DomainEvents => _domainEvents;

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }


    IReadOnlyList<BaseDomainEvent> IHasDomainEvents.DomainEvents => _domainEvents;

    public void ClearGlobalEvents()
    {
        _globalEvents.Clear();
    }

    public virtual IReadOnlyList<BaseGlobalEvent> GlobalEvents => _globalEvents;

    public void AddGlobalEvent(BaseGlobalEvent eventItem)
    {
        _globalEvents?.Add(eventItem);
    }

    public void RemoveGlobalEvent(BaseGlobalEvent eventItem)
    {
        _globalEvents?.Remove(eventItem);
    }

    public void RemoveDomainEvent(BaseDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void AddDomainEvent(BaseDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }
}
