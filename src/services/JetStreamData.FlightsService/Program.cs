using JetStreamData.Kernel.AspNet;

namespace JetStreamData.FlightsService;

public class Program : JetStreamDataApiProgram<Startup>
{
    public static async Task Main(string[] args)
    {
        await RunWebHostDefaultsAsync(args, new CancellationToken(false));
    }
}
