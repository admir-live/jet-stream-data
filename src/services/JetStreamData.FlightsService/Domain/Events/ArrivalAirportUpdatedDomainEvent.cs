using JetStreamData.FlightsService.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Events;

public sealed class ArrivalAirportUpdatedDomainEvent(FlightInformation flightInformation) : FlightInformationDomainEvent(flightInformation);
