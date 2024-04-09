using JetStreamData.Kernel.Infrastructure.Caching.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace JetStreamData.Kernel.Infrastructure.Caching.IoC;

public static class DependencyInjectionCachingExtensions
{
    public static IServiceCollection AddCache(this IServiceCollection serviceCollection, Action<CacheConfiguration> configuration)
    {
        serviceCollection.AddMemoryCache();
        serviceCollection.AddScoped<ICacheManager, HybridCacheManager>();
        var cacheConfiguration = new CacheConfiguration(serviceCollection);
        configuration(cacheConfiguration);
        serviceCollection.AddSingleton(cacheConfiguration);
        return serviceCollection;
    }
}
