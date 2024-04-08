namespace JetStreamData.Kernel.Domain.ValueObjects;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public virtual bool Equals(ValueObject other)
    {
        return other is not null && ValuesAreEqual(other);
    }

    public static bool operator ==(
        ValueObject a,
        ValueObject b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(
        ValueObject a,
        ValueObject b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        return obj is ValueObject valueObject && ValuesAreEqual(valueObject);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues().Aggregate(
            default(int),
            (
                    hashcode,
                    value) =>
                HashCode.Combine(hashcode, value.GetHashCode()));
    }

    protected abstract IEnumerable<object> GetAtomicValues();

    private bool ValuesAreEqual(ValueObject valueObject)
    {
        return GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
    }
}
