using System.Reflection;
using JetStreamData.Kernel.Behaviors;
using JetStreamData.Kernel.Dispatcher;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JetStreamData.Kernel.Extensions;

public static class DispatcherExtensions
{
    public static IServiceCollection AddDispatcher(this IServiceCollection services)
    {
        return services.AddScoped<IDispatcher, MediatorDispatcher>();
    }

    public static IServiceCollection AddPipelines(this IServiceCollection services)
    {
        return services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
    }

    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly[] assembly)
    {
        return services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assembly));
    }

    public static IServiceCollection AddMediatRDispatcher(this IServiceCollection services, Assembly[] assemblies)
    {
        return services
            .AddMediator(assemblies)
            .AddDispatcher();
    }
}
