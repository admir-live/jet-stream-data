using JetStreamData.FlightsService.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Events;

public sealed class DepartureAirportUpdatedDomainEvent(FlightInformation flightInformation) : FlightInformationDomainEvent(flightInformation);
