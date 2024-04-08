using JetStreamData.FlightsService.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Events;

public sealed class DepartureScheduleUpdatedDomainEvent(FlightInformation flightInformation) : FlightInformationDomainEvent(flightInformation);
