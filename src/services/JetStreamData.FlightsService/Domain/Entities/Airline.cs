using JetStreamData.Kernel.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Entities;

public class Airline(
    string name,
    string iata,
    string icao) : Entity<Guid>
{
    private Airline() : this("N/A", "N/A", "N/A")
    {
    }

    public string Name { get; private set; } = name ?? throw new ArgumentNullException(nameof(name));
    public string Iata { get; private set; } = iata ?? throw new ArgumentNullException(nameof(iata));
    public string Icao { get; private set; } = icao ?? throw new ArgumentNullException(nameof(icao));
}
