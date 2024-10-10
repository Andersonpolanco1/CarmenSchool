using CarmenSchool.Core.DTOs.PeriodDTO;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Services
{
  public interface IPeriodService 
  {
    Task<Period> AddAsync(PeriodCreateRequest request);
    Task<bool> DeleteByIdAsync(int id);
    Task<IEnumerable<Period>> GetAllAsync();
    Task<Period?> GetByIdAsync(int id, params Expression<Func<Period, object>>[]? includes);
    Task<bool> UpdateAsync(int id, PeriodUpdateRequest request);
    Task<IEnumerable<Period>> FindAsync(Expression<Func<Period, bool>> expression);
    Task<PaginatedList<Period>> FindAsync(PeriodQueryFilter filters);
  }
}
