using CarmenSchool.Core.DTOs.PeriodDTO;
using CarmenSchool.Core.Models;
using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Services
{
  public interface IPeriodService 
  {
    Task<PeriodReadDto> AddAsync(PeriodCreateRequest request);
    Task<bool> DeleteByIdAsync(int id);
    Task<IEnumerable<PeriodReadDto>> GetAllAsync();
    Task<PeriodReadDto?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, PeriodUpdateRequest request);
    Task<IEnumerable<PeriodReadDto>> FindAsync(Expression<Func<Period, bool>> expression);
  }
}
