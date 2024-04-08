using JetStreamData.FlightsService.Domain.Entities;
using JetStreamData.Kernel.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Events;

public abstract class FlightInformationDomainEvent(FlightInformation flightInformation) : BaseDomainEvent
{
    public FlightInformation FlightInformation { get; } = flightInformation;
}
