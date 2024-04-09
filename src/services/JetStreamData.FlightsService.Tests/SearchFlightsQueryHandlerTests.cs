using AutoMapper;
using FluentAssertions;
using JetStreamData.FlightsService.Application.Queries.Handlers;
using JetStreamData.FlightsService.Application.Queries.Models;
using JetStreamData.FlightsService.Infrastructure.Repository;
using JetStreamData.FlightsService.Infrastructure.Specifications;
using JetStreamData.FlightsService.Tests.Fixtures;
using JetStreamData.Kernel.Dispatcher;
using JetStreamData.Kernel.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace JetStreamData.FlightsService.Tests;

public class SearchFlightsQueryHandlerTests(SearchFlightHandlersUnitTestFixture fixture) : IClassFixture<SearchFlightHandlersUnitTestFixture>
{
    [Fact]
    public async Task NoResultsFound_ReturnsEmptyFlightSummaryViewModel()
    {
        // Arrange
        var query = new SearchFlights.Query { Keyword = "NonExistentFlightCode" };

        // Act
        var result = await fixture.Dispatcher.DispatchAsync(query, CancellationToken.None);
        var expected = await fixture.FlightDbContext.FlightInformations.Where(information => information.Flight.Number.Contains(query.Keyword)).CountAsync();

        // Assert 
        result.IsSuccess.Should().BeTrue();
        result.Value.Items.Should().BeEmpty();
        result.Value.TotalCount.Should().Be(expected);
        result.Value.TotalPages.Should().Be(0);
    }

    [Fact]
    public async Task PaginationParameters_ReturnsCorrectPage()
    {
        // Arrange
        var query = new SearchFlights.Query { Page = 2, PageSize = 10 };

        // Act
        var result = await fixture.Dispatcher.DispatchAsync(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.PageIndex.Should().Be(2);
        result.Value.PageSize.Should().Be(10);
        result.Value.Items.Count.Should().BeLessOrEqualTo(10);
    }

    [Fact]
    public async Task TotalPagesCalculation_IsCorrect()
    {
        // Arrange
        var query = new SearchFlights.Query { Page = 1, PageSize = 5 };

        // Act
        var result = await fixture.Dispatcher.DispatchAsync(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.TotalPages.Should().Be(4);
    }

    [Fact]
    public async Task FlightInformationMapping_IsCorrect()
    {
        // Arrange
        var query = new SearchFlights.Query { Keyword = "AF1380" };

        // Act
        var result = await fixture.Dispatcher.DispatchAsync(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Items.Should().Contain(model => model.Flight.Number.Contains(query.Keyword));
    }

    [Fact]
    public async Task DatabaseError_ReturnsFailure()
    {
        // Arrange
        var mockRepository = new Mock<IFlightRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockDispatcher = new Mock<IDispatcher>();

        mockRepository.Setup(x => x.ApplySpecification(It.IsAny<SearchFlightSpecification>()))
            .Throws(new DbUpdateException("Simulated database error"));

        var handler = new SearchFlightsQueryHandler(
            mockRepository.Object,
            fixture.Provider.GetService<ICacheManager>(),
            mockMapper.Object,
            mockDispatcher.Object);

        mockDispatcher.Setup(x => x.DispatchAsync(It.IsAny<SearchFlights.Query>(), It.IsAny<CancellationToken>()))
            .Returns((SearchFlights.Query query, CancellationToken token) => handler.Handle(query, token));

        var query = new SearchFlights.Query();

        // Act
        Func<Task> result = async () => await mockDispatcher.Object.DispatchAsync(query, CancellationToken.None);

        // Assert
        await result.Should().ThrowAsync<DbUpdateException>();
    }
}
