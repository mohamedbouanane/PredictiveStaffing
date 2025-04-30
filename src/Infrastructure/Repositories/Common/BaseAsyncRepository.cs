namespace Infrastructure.Repositories.Common;

using Application.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities.Common;
using System;

public class BaseAsyncRepository<T>(DbContext dbContext) : IBaseAsyncRepository<T> where T : BaseEntity
{
    protected readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this._dbSet.FindAsync([id], cancellationToken: cancellationToken);
    }

    public virtual async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await this._dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await this._dbSet.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await this._dbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        _ = await this._dbSet.AddAsync(entity: entity, cancellationToken: cancellationToken);

        return entity;
    }

    public virtual async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await this.GetByIdAsync(id: id, cancellationToken: cancellationToken);
        if (entity != null)
        {
            _ = this._dbSet.Remove(entity);
        }
    }

    public virtual async Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(entity.Id);

        var dbEntity = await this.GetByIdAsync(id: entity.Id.Value, cancellationToken: cancellationToken);
        ArgumentNullException.ThrowIfNull(dbEntity);

        dbContext.Entry(entity).State = EntityState.Modified;

        return dbEntity;
    }

    public virtual async Task<T?> UpdatePartialAsync(Guid id, Action<T> updateAction, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(updateAction);

        var dbEntity = await this.GetByIdAsync(id: id, cancellationToken: cancellationToken);
        ArgumentNullException.ThrowIfNull(dbEntity);

        updateAction(dbEntity);
        dbContext.Entry(dbEntity).State = EntityState.Modified;

        return dbEntity;
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this._dbSet.AnyAsync(e => e.Id == id, cancellationToken: cancellationToken);
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
    }
}
