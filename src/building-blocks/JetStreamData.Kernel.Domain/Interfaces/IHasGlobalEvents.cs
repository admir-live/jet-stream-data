using JetStreamData.Kernel.Domain.Entities;

namespace JetStreamData.Kernel.Domain.Interfaces;

public interface IHasGlobalEvents
{
    IReadOnlyList<BaseGlobalEvent> GlobalEvents { get; }
    void ClearGlobalEvents();
}
