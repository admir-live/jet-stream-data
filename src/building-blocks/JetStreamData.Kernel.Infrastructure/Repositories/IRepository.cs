using Ardalis.Specification;
using JetStreamData.Kernel.Domain.Interfaces;
using JetStreamData.Kernel.Infrastructure.Data;

namespace JetStreamData.Kernel.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : IAggregateRoot
{
    IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec);
    IQueryable<TEntity> ApplySpecificationInMemory(ISpecification<TEntity> spec);
}
