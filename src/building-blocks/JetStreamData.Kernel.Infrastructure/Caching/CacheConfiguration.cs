using JetStreamData.Kernel.Infrastructure.Caching.Providers.Memory;
using JetStreamData.Kernel.Infrastructure.Caching.Providers.Null;
using Microsoft.Extensions.DependencyInjection;

namespace JetStreamData.Kernel.Infrastructure.Caching;

public sealed class CacheConfiguration(IServiceCollection serviceCollection)
{
    public Type DefaultCacheProvider { get; set; }

    public void UseNullCache()
    {
        serviceCollection.AddSingleton<ICacheProvider, NullCacheProvider>();
    }

    public void UseInMemoryCache()
    {
        serviceCollection.AddSingleton<ICacheProvider, MemoryCacheProvider>();
    }
}
