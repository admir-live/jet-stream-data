namespace JetStreamData.FlightsService.Presentation.ViewModel;

public class ScheduleViewModel
{
    public DateTime Scheduled { get; set; }
    public DateTime? Estimated { get; set; }
    public DateTime? Actual { get; set; }
}
