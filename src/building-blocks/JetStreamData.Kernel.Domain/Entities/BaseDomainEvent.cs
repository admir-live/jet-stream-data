using JetStreamData.Kernel.Domain.Interfaces;

namespace JetStreamData.Kernel.Domain.Entities;

public abstract class BaseDomainEvent : IDomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
