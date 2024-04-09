namespace JetStreamData.Kernel.Infrastructure.Caching;

public interface ICacheManager
{
    IReadOnlyCollection<ICacheProvider> CacheProviders { get; }
    ICacheProvider Default { get; }
}
