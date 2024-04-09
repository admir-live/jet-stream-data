using Asp.Versioning.ApiExplorer;
using JetStreamData.Kernel.AspNet.Configurations;
using JetStreamData.Kernel.AspNet.Extensions;
using JetStreamData.Kernel.AspNet.Filters;
using JetStreamData.Kernel.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JetStreamData.Kernel.AspNet;

public abstract class JetStreamDataStartup
{
    private JetStreamDataGlobalConfiguration JetStreamDataGlobalConfiguration { get; } = new();
    protected abstract Action<JetStreamDataGlobalConfiguration> GlobalConfiguration { get; }
    protected abstract Action<IServiceCollection> ServiceCollection { get; }
    protected abstract Action<IApplicationBuilder> ConfigureApplicationBuilder { get; }
    protected abstract Action<IApplicationBuilder> PreAction { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        GlobalConfiguration(JetStreamDataGlobalConfiguration);

        RegisterJetStreamDataKernelModule(services);
        RegisterJetStreamDataControllersModule(services);
        RegisterJetStreamDataOpenApiModule(services);
        ServiceCollection(services);
    }

    private void RegisterJetStreamDataControllersModule(IServiceCollection services)
    {
        services
            .AddMvc(options =>
            {
                if (!JetStreamDataGlobalConfiguration.AddHttpGlobalExceptionFilter)
                {
                    return;
                }

                services.AddScoped<HttpGlobalExceptionFilter>();
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });
    }

    private void RegisterJetStreamDataOpenApiModule(IServiceCollection services)
    {
        if (JetStreamDataGlobalConfiguration.OpenApi.AddSwagger)
        {
            services
                .AddSwagger("Jet Stream Data Rest API v1.0.0");
        }
    }

    private void RegisterJetStreamDataKernelModule(IServiceCollection services)
    {
        services
            .AddKernel(
                JetStreamDataGlobalConfiguration.Configuration,
                JetStreamDataGlobalConfiguration.Modules.AssembliesModules.ToArray());
    }

    public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider versionDescription)
    {
        if (JetStreamDataGlobalConfiguration.AddHttpGlobalExceptionFilter)
        {
            app.UseMiddleware<HttpGlobalOrderProcessingExceptionMiddleware>();
        }

        if (JetStreamDataGlobalConfiguration.OpenApi.AddSwagger)
        {
            app.UseSwaggerWithUi(versionDescription);
        }

        app.UseKernel(PreAction);
        ConfigureApplicationBuilder?.Invoke(app);
    }
}
