using JetStreamData.Kernel.Domain.Entities;

namespace JetStreamData.Kernel.Domain.Interfaces;

public interface IAggregateRoot
{
    IReadOnlyList<BaseDomainEvent> DomainEvents { get; }
}
