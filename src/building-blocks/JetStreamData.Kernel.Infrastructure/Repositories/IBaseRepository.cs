using System.Linq.Expressions;

namespace JetStreamData.Kernel.Infrastructure.Repositories;

public interface IBaseRepository<TEntity>
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default, string includeProperties = "");
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default, string includeProperties = "");
    Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
    Task<IList<TEntity>> Take(int count);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveAsync();
}
