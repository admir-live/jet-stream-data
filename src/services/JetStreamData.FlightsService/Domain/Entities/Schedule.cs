using JetStreamData.Kernel.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Entities;

public sealed class Schedule(
    DateTime scheduled,
    DateTime? estimated,
    DateTime? actual) : Entity<Guid>
{
    private Schedule() : this(DateTime.MinValue, null, null)
    {
    }

    public DateTime Scheduled { get; private set; } = scheduled;
    public DateTime? Estimated { get; private set; } = estimated;
    public DateTime? Actual { get; private set; } = actual;
}
