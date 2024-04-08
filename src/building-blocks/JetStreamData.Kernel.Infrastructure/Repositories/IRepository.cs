using Ardalis.Specification;
using JetStreamData.Kernel.Domain.Interfaces;
using JetStreamData.Kernel.Infrastructure.Data;

namespace JetStreamData.Kernel.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec);
    IQueryable<TEntity> ApplySpecificationInMemory(ISpecification<TEntity> spec);
    Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    Task<TEntity> FirstOrDefaultWithInMemoryAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
    Task<object> ExecuteScalarAsync(string sql, CancellationToken cancellationToken = default);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entity);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void Delete(IEnumerable<TEntity> entities);
}
