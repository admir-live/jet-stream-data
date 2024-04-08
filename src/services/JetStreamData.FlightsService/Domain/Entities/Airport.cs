using JetStreamData.Kernel.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Entities;

public sealed class Airport(
    string name,
    string iataCode,
    string icaoCode,
    string timezone)
    : Entity<Guid>
{
    private Airport() : this("N/A", "N/A", "N/A", "N/A")
    {
    }

    public string Name { get; private set; } = name ?? throw new ArgumentNullException(nameof(name));
    public string IataCode { get; private set; } = iataCode ?? throw new ArgumentNullException(nameof(iataCode));
    public string IcaoCode { get; private set; } = icaoCode ?? throw new ArgumentNullException(nameof(icaoCode));
    public string Timezone { get; private set; } = timezone ?? throw new ArgumentNullException(nameof(timezone));
}
