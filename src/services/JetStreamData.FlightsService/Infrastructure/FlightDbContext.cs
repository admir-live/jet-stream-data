using JetStreamData.FlightsService.Domain.Entities;
using JetStreamData.Kernel.Dispatcher;
using JetStreamData.Kernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JetStreamData.FlightsService.Infrastructure;

public sealed class FlightDbContext(DbContextOptions dbContextOptions, IDispatcher dispatcher) : JetStreamDataBaseDbContext(dbContextOptions, dispatcher)
{
    public override string Schema => "flights";

    public DbSet<FlightInformation> FlightInformations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FlightsServiceAssemblyName).Assembly);
    }
}
