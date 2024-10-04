using CarmenSchool.Core.DTOs.EnrollmentsDTO;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;

namespace CarmenSchool.Core.Interfaces.Services
{
  public interface IEnrollmentService
  {
    Task<Enrollment> AddAsync(EnrollmentCreateRequest request);
    Task<bool> DeleteByIdAsync(int id);
    Task<PaginatedList<Enrollment>> FindAsync(EnrollmentQueryFilter filters);
    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task<Enrollment?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, EnrollmentUpdateDto request);
  }
}
