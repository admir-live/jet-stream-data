using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace JetStreamData.Kernel.AspNet;

public abstract class JetStreamDataApiProgram<TStartup> : JetStreamDataGenericProgram where TStartup : JetStreamDataStartup
{
    protected static async Task RunWebHostDefaultsAsync(string[] args, CancellationToken cancellationToken = default)
    {
        var host = FactoryHostBuilder(args)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.ConfigureKestrel(options =>
                {
                    options.Limits.MaxRequestHeadersTotalSize = 302768;
                });
                builder.UseStartup<TStartup>();
            })
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration(builder =>
                builder
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile("appsettings.Development.json", true, true)
                    .AddEnvironmentVariables()
                    .Build());

        await host.RunConsoleAsync(cancellationToken);
    }
}
