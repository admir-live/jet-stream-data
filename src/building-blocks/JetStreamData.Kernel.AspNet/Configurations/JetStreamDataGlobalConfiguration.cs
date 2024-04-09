using Microsoft.Extensions.Configuration;

namespace JetStreamData.Kernel.AspNet.Configurations;

public sealed class JetStreamDataGlobalConfiguration
{
    public JetStreamDataOpenApiConfiguration OpenApi { get; set; } = new();
    public JetStreamDataModulesConfiguration Modules { get; set; } = new();
    public IConfiguration Configuration { get; set; }
    public bool AddHttpGlobalExceptionFilter { get; set; }

    public JetStreamDataGlobalConfiguration UseHttpGlobalExceptionFilter()
    {
        AddHttpGlobalExceptionFilter = true;
        return this;
    }

    public JetStreamDataGlobalConfiguration UseAppSettingsConfiguration(IConfiguration configuration)
    {
        Configuration = configuration;
        return this;
    }
}
