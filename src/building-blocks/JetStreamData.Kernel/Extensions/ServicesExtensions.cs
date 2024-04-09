using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace JetStreamData.Kernel.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServicesAsImplementedInterface(this IServiceCollection services, ServiceLifetime lifetime, Assembly[] assemblies, Func<Type, bool> filter)
    {
        if (filter is null)
        {
            throw new ArgumentNullException(nameof(filter), @"Please specify the filter argument before using method AddServicesAsImplementedInterface at Penzle.Kernel.Extensions.");
        }

        var assemblyServices = assemblies
            .SelectMany(assembly => assembly.GetTypes().Where(type => type.GetInterfaces().Any(filter)))
            .Select(t => new Tuple<Type, Type>(t, t.GetInterfaces().Where(x => !x.IsGenericType).FirstOrDefault(filter)))
            .Where(tuple => tuple.Item2 != typeof(IServiceProvider));

        foreach (var (assemblyServiceAbstraction, assemblyServicesImplementation) in assemblyServices)
        {
            if (assemblyServicesImplementation != null)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(assemblyServicesImplementation, assemblyServiceAbstraction);
                        continue;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(assemblyServicesImplementation, assemblyServiceAbstraction);
                        continue;
                    case ServiceLifetime.Transient:
                        services.AddTransient(assemblyServicesImplementation, assemblyServiceAbstraction);
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
                }
            }
        }

        return services;
    }
}
