using JetStreamData.Kernel.Dispatcher;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JetStreamData.FlightsService.Infrastructure;

public class FlightDbContextDesignTimeFactory : IDesignTimeDbContextFactory<FlightDbContext>
{
    public FlightDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", true)
            .Build();

        var connectionString = configuration.GetConnectionString("FlightDb");

        var optionsBuilder = new DbContextOptionsBuilder<FlightDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        return new FlightDbContext(optionsBuilder.Options, new NullDispatcher());
    }
}
