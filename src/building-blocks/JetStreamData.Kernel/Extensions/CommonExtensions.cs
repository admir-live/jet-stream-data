using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JetStreamData.Kernel.Extensions;

public static class CommonExtensions
{
    public static IServiceCollection AddKernel(this IServiceCollection serviceCollection,
        IConfiguration configuration, Assembly[] assemblies)
    {
        serviceCollection
            .AddAutoMapper(assemblies)
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblies(assemblies)
            .AddLogging()
            .AddMediatRDispatcher(assemblies)
            .AddPipelines()
            .AddHttpContextAccessor()
            .AddCors(o =>
                o.AddPolicy("DefaultPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }))
            .AddApiVersioning()
            .AddApiExplorer(o => o.GroupNameFormat = "'v'VVV");

        serviceCollection
            .AddDataRepository(assemblies, new List<string>());

        return serviceCollection;
    }

    public static IApplicationBuilder UseKernel(this IApplicationBuilder app, Action<IApplicationBuilder> preAction)
    {
        app
            .Map("/version",
                builder => builder.Run(async context =>
                {
                    await context?.Response.WriteAsJsonAsync(Assembly.GetExecutingAssembly().GetName().Version
                        ?.ToString());
                })).UseCors("DefaultPolicy")
            .UseStaticFiles()
            .UseRouting();

        preAction?.Invoke(app);
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        return app;
    }
}
