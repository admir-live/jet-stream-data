using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using JetStreamData.Kernel.Domain.Interfaces;
using JetStreamData.Kernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace JetStreamData.Kernel.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    private readonly JetStreamDataBaseDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    protected Repository(JetStreamDataBaseDbContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }

    public IUnitOfWork UnitOfWork => _dbContext;

    public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var keyValues = new object[] { id };
        return await _dbSet.FindAsync(keyValues, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        var specificationResult = ApplySpecification(spec);
        return await specificationResult.ToListAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        var specificationResult = ApplySpecification(spec);
        return await specificationResult.CountAsync(cancellationToken);
    }

    public virtual async Task<object> ExecuteScalarAsync(string sql, CancellationToken cancellationToken = default)
    {
        var connection = _dbContext.Database.GetDbConnection();
        var command = connection.CreateCommand();
        if (_dbContext.Database.CurrentTransaction != null)
        {
            command.Transaction = _dbContext.Database.CurrentTransaction.GetDbTransaction();
        }

        command.CommandText = sql;

        return await command.ExecuteScalarAsync(cancellationToken);
    }

    public virtual void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        var specificationResult = ApplySpecification(spec);
        return await specificationResult.FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TEntity> FirstOrDefaultWithInMemoryAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        var elementInDatabase = await FirstOrDefaultAsync(spec, cancellationToken);

        if (elementInDatabase != null)
        {
            return elementInDatabase;
        }

        var specificationInMemoryResult = ApplySpecificationInMemory(spec);
        return specificationInMemoryResult.FirstOrDefault();
    }

    public virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator.Default.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), spec);
    }

    public virtual IQueryable<TEntity> ApplySpecificationInMemory(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator.Default.GetQuery(_dbContext.Set<TEntity>().Local.AsQueryable(), spec);
    }

    public void Delete(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }
}
