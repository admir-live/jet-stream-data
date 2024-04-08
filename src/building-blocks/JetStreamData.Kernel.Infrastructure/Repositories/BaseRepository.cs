using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace JetStreamData.Kernel.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(DbContext context) : IBaseRepository<TEntity>
    where TEntity : class
{
    private readonly DbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet;
    }

    public virtual IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet
            .Where(expression);
    }

    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default, string includeProperties = "")
    {
        var query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate<string, IQueryable<TEntity>>(_dbSet, (current, includeProperty) => current.Include(includeProperty));

        return await query.FirstOrDefaultAsync(filter ?? throw new ArgumentNullException(nameof(filter)), cancellationToken);
    }

    public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default, string includeProperties = "")
    {
        var query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate<string, IQueryable<TEntity>>(_dbSet, (current, includeProperty) => current.Include(includeProperty));

        return await query.SingleOrDefaultAsync(filter ?? throw new ArgumentNullException(nameof(filter)), cancellationToken);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return await _dbSet.CountAsync(filter ?? throw new ArgumentNullException(nameof(filter)));
    }

    public virtual async Task<IList<TEntity>> Take(int count)
    {
        return await _dbSet.Take(count).ToListAsync();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }

        _dbSet.Remove(entityToDelete);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
