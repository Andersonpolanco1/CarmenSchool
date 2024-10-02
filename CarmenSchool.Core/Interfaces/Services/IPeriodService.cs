using CarmenSchool.Core.DTOs.PeriodDTO;
using CarmenSchool.Core.Models;
using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Services
{
  public interface IPeriodService 
  {
    Task<Period> AddAsync(PeriodCreateRequest request);
    Task<bool> DeleteByIdAsync(int id);
    Task<IEnumerable<Period>> GetAllAsync();
    Task<Period?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, PeriodUpdateRequest request);
    Task<IEnumerable<Period>> FindAsync(Expression<Func<Period, bool>> expression);
  }
}
