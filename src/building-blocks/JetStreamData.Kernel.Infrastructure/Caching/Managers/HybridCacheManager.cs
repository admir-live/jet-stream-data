namespace JetStreamData.Kernel.Infrastructure.Caching.Managers;

internal class HybridCacheManager : ICacheManager
{
    public HybridCacheManager(IEnumerable<ICacheProvider> cacheProviders, CacheConfiguration cacheConfiguration)
    {
        var providers = cacheProviders.ToList();
        if (providers is null || providers.Count == 0)
        {
            throw new ArgumentNullException(nameof(cacheProviders), @"Please add at least one cache provider.");
        }

        if (cacheConfiguration is null)
        {
            throw new ArgumentNullException(nameof(cacheConfiguration), @"Please configure cache manager.");
        }

        CacheProviders = providers.ToList();
        Default = providers.FirstOrDefault(provider => provider.GetType() == cacheConfiguration.DefaultCacheProvider) ??
                  throw new EntryPointNotFoundException("The default cache type missing in cache provider collection.");
    }

    public IReadOnlyCollection<ICacheProvider> CacheProviders { get; }
    public ICacheProvider Default { get; }
}
