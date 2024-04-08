namespace JetStreamData.Kernel.Domain.Entities;

public abstract class Entity<TKey> : BaseEntity where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
}
