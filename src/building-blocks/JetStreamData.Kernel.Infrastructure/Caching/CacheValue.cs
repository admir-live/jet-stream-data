using CSharpFunctionalExtensions;

namespace JetStreamData.Kernel.Infrastructure.Caching;

public class CacheValue<TValue>(TValue value, bool hasValue) : ValueObject
{
    public bool HasValue { get; } = hasValue;
    public bool IsNull => Value == null;
    public TValue Value { get; } = value;
    public static CacheValue<TValue> Null { get; } = new(default, true);
    public static CacheValue<TValue> NoValue { get; } = new(default, false);

    public override string ToString()
    {
        return Value?.ToString() ?? "<null>";
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        return null;
    }
}
