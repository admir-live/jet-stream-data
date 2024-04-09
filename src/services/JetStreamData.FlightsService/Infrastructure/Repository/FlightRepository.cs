using JetStreamData.FlightsService.Domain.Entities;
using JetStreamData.Kernel.Infrastructure.Repositories;

namespace JetStreamData.FlightsService.Infrastructure.Repository;

public sealed class FlightRepository(FlightDbContext context) : Repository<FlightInformation>(context), IFlightRepository;
