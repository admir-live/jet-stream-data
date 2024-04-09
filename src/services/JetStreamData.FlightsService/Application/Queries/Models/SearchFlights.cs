using CSharpFunctionalExtensions;
using JetStreamData.FlightsService.Presentation.ViewModel;
using JetStreamData.Kernel.Application.Model;

namespace JetStreamData.FlightsService.Application.Queries.Models;

public sealed class SearchFlights
{
    public class Query(string keyword) : QueryCommand.PaginationQuery<Result<FlightSummaryViewModel>>
    {
        public string Keyword { get; set; } = keyword;

        public Query() : this(string.Empty)
        {
        }
    }
}
