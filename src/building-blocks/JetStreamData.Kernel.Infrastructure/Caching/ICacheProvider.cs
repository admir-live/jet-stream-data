namespace JetStreamData.Kernel.Infrastructure.Caching;

public interface ICacheProvider
{
    string Name { get; }
    void Set<T>(string cacheKey, T cacheValue, TimeSpan expiration);
    Task SetAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration);
    CacheValue<T> Get<T>(string cacheKey);
    Task<CacheValue<T>> GetAsync<T>(string cacheKey);
    void Remove(string cacheKey);
    Task RemoveAsync(string cacheKey);
    Task<bool> ExistsAsync(string cacheKey);
    bool Exists(string cacheKey);
    void RemoveAll(IEnumerable<string> cacheKeys);
    Task RemoveAllAsync(IEnumerable<string> cacheKeys);
}
