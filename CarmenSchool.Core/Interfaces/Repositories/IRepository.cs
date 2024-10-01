using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : IBaseEntity
  {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        bool IsModified(T entity);
    }
}
