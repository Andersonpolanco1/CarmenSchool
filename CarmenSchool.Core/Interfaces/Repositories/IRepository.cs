using CarmenSchool.Core.DTOs;
using CarmenSchool.Core.Utils;
using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Repositories
{
  public interface IRepository<T> where T : IBaseEntity
  {
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[]? includes);
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[]? includes);
    Task<T> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[]? includes);
    Task<PaginatedList<T>> FindAsync(BaseQueryFilter filters);
    bool IsModified(T entity);
  }
}
