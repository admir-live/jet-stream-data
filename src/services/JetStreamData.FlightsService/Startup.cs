using JetStreamData.FlightsService.Infrastructure;
using JetStreamData.FlightsService.Infrastructure.Extensions;
using JetStreamData.FlightsService.Infrastructure.Seed;
using JetStreamData.Kernel.AspNet;
using JetStreamData.Kernel.AspNet.Configurations;
using JetStreamData.Kernel.Extensions;

namespace JetStreamData.FlightsService;

public sealed class Startup : JetStreamDataStartup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override Action<JetStreamDataGlobalConfiguration> GlobalConfiguration => configuration =>
    {
        configuration
            .UseAppSettingsConfiguration(_configuration)
            .UseHttpGlobalExceptionFilter();

        configuration
            .Modules
            .RegisterApplicationAssetsFromAssemblies([typeof(FlightsServiceAssemblyName).Assembly]);

        configuration
            .OpenApi
            .UseSwagger("Jet Stream Data Flights Service API v1.0.0");
    };

    protected override Action<IServiceCollection> ServiceCollection => collection =>
    {
        collection
            .AddFlightsDbContext(_configuration);
    };

    protected override Action<IApplicationBuilder> ConfigureApplicationBuilder => builder =>
    {
        builder
            .TryMigrate<FlightDbContext>(context =>
            {
                if (context.FlightInformations.Any())
                {
                    return;
                }

                context.FlightInformations.AddRange(Seed.Get);
                context.SaveChanges();
            });
    };

    protected override Action<IApplicationBuilder> PreAction => builder =>
    {
    };
}
