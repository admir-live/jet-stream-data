using JetStreamData.Kernel.Domain.Interfaces;

namespace JetStreamData.Kernel.Domain.Entities;

public abstract class BaseGlobalEvent : IGlobalEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
