using Microsoft.Extensions.Caching.Memory;

namespace JetStreamData.Kernel.Infrastructure.Caching.Providers.Memory;

public sealed class MemoryCacheProvider(IMemoryCache memoryCache) : ICacheProvider
{
    public string Name => nameof(MemoryCacheProvider);

    public void Set<T>(string cacheKey, T cacheValue, TimeSpan expiration)
    {
        memoryCache.Set(cacheKey, cacheValue, expiration);
    }

    public Task SetAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration)
    {
        Set(cacheKey, cacheValue, expiration);
        return Task.CompletedTask;
    }

    public CacheValue<T> Get<T>(string cacheKey)
    {
        var success = memoryCache.TryGetValue(cacheKey, out T value);
        return new CacheValue<T>(value, success);
    }

    public Task<CacheValue<T>> GetAsync<T>(string cacheKey)
    {
        return Task.FromResult(Get<T>(cacheKey));
    }

    public void Remove(string cacheKey)
    {
        memoryCache.Remove(cacheKey);
    }

    public Task RemoveAsync(string cacheKey)
    {
        Remove(cacheKey);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string cacheKey)
    {
        return Task.FromResult(Exists(cacheKey));
    }

    public bool Exists(string cacheKey)
    {
        var value = Get<object>(cacheKey);
        return value.HasValue;
    }

    public void RemoveAll(IEnumerable<string> cacheKeys)
    {
        foreach (var cacheKey in cacheKeys)
        {
            Remove(cacheKey);
        }
    }

    public Task RemoveAllAsync(IEnumerable<string> cacheKeys)
    {
        RemoveAll(cacheKeys);
        return Task.CompletedTask;
    }
}
