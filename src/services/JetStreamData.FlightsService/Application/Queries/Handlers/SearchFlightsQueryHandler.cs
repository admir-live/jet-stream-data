using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using JetStreamData.FlightsService.Application.Queries.Models;
using JetStreamData.FlightsService.Application.Services;
using JetStreamData.FlightsService.Infrastructure.Repository;
using JetStreamData.FlightsService.Infrastructure.Specifications;
using JetStreamData.FlightsService.Presentation.ViewModel;
using JetStreamData.Kernel.Dispatcher;
using Microsoft.EntityFrameworkCore;

namespace JetStreamData.FlightsService.Application.Queries.Handlers;

public sealed class SearchFlightsQueryHandler(IFlightRepository repository, IMapper mapper, IDispatcher dispatcher)
    : BaseHandler<SearchFlights.Query, Result<FlightSummaryViewModel>>(mapper, dispatcher)
{
    public override async Task<Result<FlightSummaryViewModel>> Handle(SearchFlights.Query request, CancellationToken cancellationToken)
    {
        var query = repository
            .ApplySpecification(new SearchFlightSpecification(request));

        var collection = await query
            .ApplyPaginationQuery(request)
            .ProjectTo<FlightInformationViewModel>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var total = await query.CountAsync(cancellationToken);

        var flightSummary = new FlightSummaryViewModel
        {
            Items = collection,
            PageIndex = request.Page,
            PageSize = request.PageSize,
            TotalCount = total,
            TotalPages = (int)Math.Ceiling(total / (double)request.PageSize)
        };

        return Result.Success(flightSummary);
    }
}
