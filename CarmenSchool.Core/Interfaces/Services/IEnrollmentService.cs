using CarmenSchool.Core.DTOs.EnrollmentsDTO;
using CarmenSchool.Core.Models;

namespace CarmenSchool.Core.Interfaces.Services
{
  public interface IEnrollmentService
  {
    Task<Enrollment> AddAsync(EnrollmentCreateRequest request);
    Task<bool> DeleteByIdAsync(int id);
    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task<Enrollment?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, EnrollmentUpdateDto request);
  }
}
