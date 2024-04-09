using JetStreamData.Kernel.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace JetStreamData.FlightsService.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddFlightsDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FlightDb");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ConfigurationMissingException("FlightDb");
        }

        services.AddDbContext<FlightDbContext>(builder =>
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        return services;
    }
}
