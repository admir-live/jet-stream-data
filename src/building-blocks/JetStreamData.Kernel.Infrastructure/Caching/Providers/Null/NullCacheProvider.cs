namespace JetStreamData.Kernel.Infrastructure.Caching.Providers.Null;

public sealed class NullCacheProvider : ICacheProvider
{
    public string Name => nameof(NullCacheProvider);

    public void Set<T>(string cacheKey, T cacheValue, TimeSpan expiration)
    {
    }

    public Task SetAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration)
    {
        return Task.CompletedTask;
    }

    public CacheValue<T> Get<T>(string cacheKey)
    {
        return new CacheValue<T>(default, false);
    }

    public Task<CacheValue<T>> GetAsync<T>(string cacheKey)
    {
        return Task.FromResult(Get<T>(cacheKey));
    }

    public void Remove(string cacheKey)
    {
    }

    public Task RemoveAsync(string cacheKey)
    {
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string cacheKey)
    {
        return Task.FromResult(Exists(cacheKey));
    }

    public bool Exists(string cacheKey)
    {
        return false;
    }

    public void RemoveAll(IEnumerable<string> cacheKeys)
    {
    }

    public Task RemoveAllAsync(IEnumerable<string> cacheKeys)
    {
        return Task.CompletedTask;
    }
}
