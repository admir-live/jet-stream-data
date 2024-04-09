using Microsoft.Extensions.Hosting;

namespace JetStreamData.Kernel.AspNet;

public abstract class JetStreamDataGenericProgram
{
    protected static IHostBuilder FactoryHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args);
    }
}
