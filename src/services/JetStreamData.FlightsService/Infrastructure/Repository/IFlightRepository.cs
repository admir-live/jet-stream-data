using JetStreamData.FlightsService.Domain.Entities;
using JetStreamData.Kernel.Infrastructure.Repositories;

namespace JetStreamData.FlightsService.Infrastructure.Repository;

public interface IFlightRepository : IRepository<FlightInformation>
{
}
