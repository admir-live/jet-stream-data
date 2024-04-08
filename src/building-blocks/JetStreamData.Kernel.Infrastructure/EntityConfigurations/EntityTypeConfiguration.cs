using JetStreamData.Kernel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JetStreamData.Kernel.Infrastructure.EntityConfigurations;

public abstract class EntityTypeConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : Entity<TKey> where TKey : IEquatable<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .Property(p => p.CreatedAt)
            .IsRequired();

        builder
            .Property(p => p.ModifiedAt)
            .IsRequired();
    }
}
