using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JetStreamData.Kernel.Extensions;

public static class DataExtensions
{
    public static IServiceCollection AddDataRepository(this IServiceCollection serviceCollection, Assembly[] assemblies,
        IList<string> namespaceMarkers = null)
    {
        namespaceMarkers ??= new List<string>();
        const string marker = "Repository";
        namespaceMarkers.AddIfNotContains(JetStreamDataGlobalConstants.Namespace);
        return serviceCollection.AddServicesAsImplementedInterface(ServiceLifetime.Scoped, assemblies,
            i => i.Namespace != null && i.Name.Contains(marker) && i.Namespace.ContainsAny(namespaceMarkers.ToArray()));
    }

    public static IServiceCollection AddDataRepository(this IServiceCollection serviceCollection)
    {
        return AddDataRepository(serviceCollection, new[] { Assembly.GetExecutingAssembly() }, Array.Empty<string>());
    }

    public static IApplicationBuilder TryMigrate<TContext>(this IApplicationBuilder applicationBuilder,
        Action<TContext> seedAction = null)
        where TContext : DbContext
    {
        var logger = applicationBuilder.ApplicationServices.GetService<ILogger<TContext>>();
        try
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetService<TContext>();
            context.Database.Migrate();
            seedAction?.Invoke(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Failed to migrate database for context ${typeof(TContext).FullName}");
        }

        return applicationBuilder;
    }
}
