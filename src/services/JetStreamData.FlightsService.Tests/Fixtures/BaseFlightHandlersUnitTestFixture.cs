using System.Reflection;
using AutoMapper;
using JetStreamData.FlightsService.Infrastructure;
using JetStreamData.Kernel;
using JetStreamData.Kernel.Dispatcher;
using JetStreamData.Kernel.Extensions;
using JetStreamData.Kernel.Infrastructure.Caching.IoC;
using JetStreamData.Kernel.Infrastructure.Caching.Providers.Null;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JetStreamData.FlightsService.Tests.Fixtures;

public abstract class BaseFlightHandlersUnitTestFixture
{
    public IServiceCollection ServiceCollection { get; protected set; }
    public IServiceProvider Provider { get; protected set; }
    public IMapper Mapper { get; protected set; }
    public IDispatcher Dispatcher { get; protected set; }

    public FlightDbContext FlightDbContext { get; protected set; }

    protected virtual IServiceCollection ConfigureServiceCollection(params Assembly[] assemblies)
    {
        ServiceCollection = new ServiceCollection();

        var scanAssemblies = new[] { Assembly.GetAssembly(typeof(FlightsServiceAssemblyName)), Assembly.GetAssembly(typeof(KernelAssembly)) };
        if (assemblies.IsNotNullOrEmpty())
        {
            scanAssemblies.AddRange(assemblies);
        }

        ServiceCollection
            .AddMediatRDispatcher(scanAssemblies)
            .AddAutoMapper(scanAssemblies)
            .AddDataRepository(scanAssemblies)
            .AddSqliteInMemoryContext<FlightDbContext>()
            .AddCache(configuration =>
            {
                configuration.DefaultCacheProvider = typeof(NullCacheProvider);
                configuration.UseNullCache();
            });

        return ServiceCollection;
    }

    protected virtual void ConfigureMembers(params Assembly[] assemblies)
    {
        Provider = BuildServiceProvider();
        Mapper = Provider.GetService<IMapper>();
        Dispatcher = Provider.GetService<IDispatcher>();
        FlightDbContext = Provider.GetService<FlightDbContext>();
        ConfigureDbContextDatabase(FlightDbContext);
    }

    private IServiceProvider BuildServiceProvider()
    {
        return ServiceCollection.BuildServiceProvider();
    }

    private static void ConfigureDbContextDatabase<TDbContext>(TDbContext dbContext) where TDbContext : DbContext
    {
        dbContext.Database.OpenConnection();
        dbContext.Database.EnsureCreated();
    }
}
