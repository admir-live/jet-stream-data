using JetStreamData.FlightsService.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Events;

public sealed class ArrivalScheduleUpdatedDomainEvent(FlightInformation flightInformation) : FlightInformationDomainEvent(flightInformation);
