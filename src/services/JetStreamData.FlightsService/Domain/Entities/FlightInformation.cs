using JetStreamData.FlightsService.Domain.Events;
using JetStreamData.Kernel.Domain.Entities;

namespace JetStreamData.FlightsService.Domain.Entities;

public sealed class FlightInformation : AggregateRoot<Guid>
{
    private FlightInformation() { }

    public FlightInformation(
        Guid id,
        Flight flight,
        Airport departureAirport,
        Airport arrivalAirport,
        Airline airline,
        Schedule departureSchedule,
        Schedule arrivalSchedule)
    {
        Id = id;
        SetFlight(flight);
        SetDepartureAirport(departureAirport);
        SetArrivalAirport(arrivalAirport);
        SetAirline(airline);
        UpdateDepartureSchedule(departureSchedule.Scheduled, departureSchedule.Estimated, departureSchedule.Actual);
        UpdateArrivalSchedule(arrivalSchedule.Scheduled, arrivalSchedule.Estimated, arrivalSchedule.Actual);
    }

    public Flight Flight { get; private set; } = null!;
    public Airport DepartureAirport { get; private set; } = null!;
    public Airport ArrivalAirport { get; private set; } = null!;
    public Airline Airline { get; private set; } = null!;
    public Schedule DepartureSchedule { get; private set; } = null!;
    public Schedule ArrivalSchedule { get; private set; } = null!;

    public void SetFlight(Flight flight)
    {
        Flight = flight ?? throw new ArgumentNullException(nameof(flight));
        AddDomainEvent(new FlightUpdatedDomainEvent(this));
    }

    public void SetDepartureAirport(Airport airport)
    {
        DepartureAirport = airport ?? throw new ArgumentNullException(nameof(airport));
        AddDomainEvent(new DepartureAirportUpdatedDomainEvent(this));
    }

    public void SetArrivalAirport(Airport airport)
    {
        ArrivalAirport = airport ?? throw new ArgumentNullException(nameof(airport));
        AddDomainEvent(new ArrivalAirportUpdatedDomainEvent(this));
    }

    public void SetAirline(Airline airline)
    {
        Airline = airline ?? throw new ArgumentNullException(nameof(airline));
        AddDomainEvent(new AirlineUpdatedDomainEvent(this));
    }

    public void UpdateDepartureSchedule(
        DateTime newScheduledDeparture,
        DateTime? newEstimatedDeparture = null,
        DateTime? newActualDeparture = null)
    {
        DepartureSchedule = new Schedule(newScheduledDeparture, newEstimatedDeparture, newActualDeparture);
        AddDomainEvent(new DepartureScheduleUpdatedDomainEvent(this));
    }

    public void UpdateArrivalSchedule(
        DateTime newScheduledArrival,
        DateTime? newEstimatedArrival = null,
        DateTime? newActualArrival = null)
    {
        ArrivalSchedule = new Schedule(newScheduledArrival, newEstimatedArrival, newActualArrival);
        AddDomainEvent(new ArrivalScheduleUpdatedDomainEvent(this));
    }
}
