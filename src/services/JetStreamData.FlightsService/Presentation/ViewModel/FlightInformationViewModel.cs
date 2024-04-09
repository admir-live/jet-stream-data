using JetStreamData.Kernel.Application.ViewModel;

namespace JetStreamData.FlightsService.Presentation.ViewModel;

public class FlightInformationViewModel : IdViewModel<Guid>
{
    public FlightViewModel Flight { get; set; }
    public AirportViewModel DepartureAirport { get; set; }
    public AirportViewModel ArrivalAirport { get; set; }
    public AirlineViewModel Airline { get; set; }
    public ScheduleViewModel DepartureSchedule { get; set; }
    public ScheduleViewModel ArrivalSchedule { get; set; }
}
