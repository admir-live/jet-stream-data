using Ardalis.Specification;
using JetStreamData.FlightsService.Application.Queries.Models;
using JetStreamData.FlightsService.Domain.Entities;

namespace JetStreamData.FlightsService.Infrastructure.Specifications;

public sealed class SearchFlightSpecification : Specification<FlightInformation>
{
    public SearchFlightSpecification(SearchFlights.Query query)
    {
        Query
            .AsNoTracking();

        if (query.Keyword.IsNotNullOrWhiteSpace())
        {
            Query.Where(information => information.Flight.Number.Contains(query.Keyword));
        }
    }
}
