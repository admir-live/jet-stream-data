using System.Reflection;
using JetStreamData.FlightsService.Infrastructure;
using JetStreamData.FlightsService.Infrastructure.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace JetStreamData.FlightsService.Tests.Fixtures;

public sealed class SearchFlightHandlersUnitTestFixture : BaseFlightHandlersUnitTestFixture
{
    public SearchFlightHandlersUnitTestFixture()
    {
        ConfigureServiceCollection();
        ConfigureMembers();
        ConfigureTestData();
    }

    private void ConfigureTestData()
    {
        using var scope = Provider.CreateScope();
        using var dbContext = scope.ServiceProvider.GetService<FlightDbContext>()!;
        dbContext.FlightInformations.AddRange(Seed.Get);
        dbContext.SaveChanges();
    }

    protected override IServiceCollection ConfigureServiceCollection(params Assembly[] assemblies)
    {
        var serviceCollection = base.ConfigureServiceCollection(assemblies);
        return serviceCollection;
    }
}
