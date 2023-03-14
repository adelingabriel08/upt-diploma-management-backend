using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UPT.Diploma.Management.Domain.Models.Base;
using UPT.Diploma.Management.Persistence.Contracts;
using UPT.Diploma.Management.Persistence.Database;

namespace UPT.Diploma.Management.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase
{
    private readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(TEntity entity, bool skipCommit = false)
    {
        entity.CreatedTimeUtc = DateTime.UtcNow;
        entity.UpdatedTimeUtc = DateTime.UtcNow;
        await _dbContext.Set<TEntity>().AddAsync(entity);
        if (!skipCommit)
            await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity, bool skipCommit = false)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        if (!skipCommit)
            await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<TEntity>> GetAsync()
    {
        return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetAsync(int id, bool asNoTracking = true)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ICollection<TEntity>> GetAsync(int skip, int take)
    {
        return await _dbContext.Set<TEntity>().Skip(skip).Take(take).ToListAsync();
    }

    public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().AsNoTracking().Where(expression).ToListAsync();
    }
    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression);
    }
    
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().AsNoTracking().AnyAsync(expression);
    }

    public async Task CommitDbTransactionAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}