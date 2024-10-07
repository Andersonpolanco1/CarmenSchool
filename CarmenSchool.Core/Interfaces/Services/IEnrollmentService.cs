using CarmenSchool.Core.DTOs.EnrollmentsDTO;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Services
{
  public interface IEnrollmentService
  {
    Task<Enrollment> AddAsync(EnrollmentCreateRequest request);
    Task<bool> DeleteByIdAsync(int id);
    Task<PaginatedList<Enrollment>> FindAsync(EnrollmentQueryFilter filters);
    Task<IEnumerable<Enrollment>> FindAsync(Expression<Func<Enrollment, bool>> expression, params Expression<Func<Enrollment, object>>[]? includes);
    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task<Enrollment?> GetByIdAsync(int id, params Expression<Func<Enrollment, object>>[]? includes);
    Task<bool> UpdateAsync(int id, EnrollmentUpdateDto request);
  }
}
