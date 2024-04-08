using JetStreamData.Kernel.Domain.Entities;

namespace JetStreamData.Kernel.Domain.Interfaces;

public interface IHasDomainEvents
{
    IReadOnlyList<BaseDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
