using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using JetStreamData.FlightsService.Application.Queries.Models;
using JetStreamData.FlightsService.Application.Services;
using JetStreamData.FlightsService.Infrastructure.Repository;
using JetStreamData.FlightsService.Infrastructure.Specifications;
using JetStreamData.FlightsService.Presentation.ViewModel;
using JetStreamData.Kernel.Dispatcher;
using JetStreamData.Kernel.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;

namespace JetStreamData.FlightsService.Application.Queries.Handlers;

public sealed class SearchFlightsQueryHandler(
    IFlightRepository repository,
    ICacheManager cacheManager,
    IMapper mapper,
    IDispatcher dispatcher)
    : BaseHandler<SearchFlights.Query, Result<FlightSummaryViewModel>>(mapper, dispatcher)
{
    private readonly IFlightRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly ICacheManager _cacheManager = cacheManager ?? throw new ArgumentNullException(nameof(cacheManager));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public override async Task<Result<FlightSummaryViewModel>> Handle(SearchFlights.Query request, CancellationToken cancellationToken)
    {
        var cacheKey = GenerateCacheKey(request);
        var cachedResult = TryGetFromCache(cacheKey);
        if (cachedResult.IsSuccess)
        {
            return cachedResult.Value;
        }

        var flightSummary = await CreateFlightSummaryAsync(request, cancellationToken);
        return await CacheAndReturnAsync(cacheKey, flightSummary);
    }

    private string GenerateCacheKey(SearchFlights.Query request)
    {
        return QueryHashService.GenerateUniqueName(request);
    }

    private Result<FlightSummaryViewModel> TryGetFromCache(string cacheKey)
    {
        var cacheValue = _cacheManager.Default.Get<Result<FlightSummaryViewModel>>(cacheKey);
        return cacheValue.HasValue ? cacheValue.Value : Result.Failure<FlightSummaryViewModel>("Cache miss");
    }

    private async Task<FlightSummaryViewModel> CreateFlightSummaryAsync(SearchFlights.Query request, CancellationToken cancellationToken)
    {
        var query = _repository.ApplySpecification(new SearchFlightSpecification(request));

        var items = await query
            .ApplyPaginationQuery(request)
            .ProjectTo<FlightInformationViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var total = await query.CountAsync(cancellationToken);

        return new FlightSummaryViewModel
        {
            Items = items,
            PageIndex = request.Page,
            PageSize = request.PageSize,
            TotalCount = total,
            TotalPages = CalculateTotalPages(total, request.PageSize)
        };
    }

    private static int CalculateTotalPages(int total, int pageSize)
    {
        return (int)Math.Ceiling(total / (double)pageSize);
    }

    private async Task<Result<FlightSummaryViewModel>> CacheAndReturnAsync(string cacheKey, FlightSummaryViewModel flightSummary)
    {
        var result = Result.Success(flightSummary);
        await _cacheManager.Default.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));
        return result;
    }
}
