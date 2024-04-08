using JetStreamData.Kernel.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JetStreamData.Kernel.Infrastructure.EntityConfigurations;

public abstract class AggregateRootTypeConfiguration<TEntity, TKey> : EntityTypeConfiguration<TEntity, TKey> where TEntity : AggregateRoot<TKey> where TKey : IEquatable<TKey>
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.Ignore(c => c.DomainEvents);
        builder.Ignore(c => c.GlobalEvents);
    }
}
