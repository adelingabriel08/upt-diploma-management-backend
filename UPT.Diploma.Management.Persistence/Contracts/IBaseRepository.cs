using System.Linq.Expressions;
using UPT.Diploma.Management.Domain.Models.Base;

namespace UPT.Diploma.Management.Persistence.Contracts;

public interface IBaseRepository<TEntity> where TEntity : EntityBase
{
    Task AddAsync(TEntity entity, bool skipCommit = false);
    Task DeleteAsync(TEntity entity, bool skipCommit = false);
    Task<ICollection<TEntity>> GetAsync();
    Task<TEntity> GetAsync(int id, bool asNoTracking = true);
    Task<ICollection<TEntity>> GetAsync(int skip, int take);
    Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
    Task CommitDbTransactionAsync();
    
}