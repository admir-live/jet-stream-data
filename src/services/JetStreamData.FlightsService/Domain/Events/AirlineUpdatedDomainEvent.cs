using JetStreamData.FlightsService.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Events;

public sealed class AirlineUpdatedDomainEvent(FlightInformation flightInformation) : FlightInformationDomainEvent(flightInformation);
