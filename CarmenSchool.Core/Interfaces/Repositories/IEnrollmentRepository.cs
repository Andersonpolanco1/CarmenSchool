using CarmenSchool.Core.Models;

namespace CarmenSchool.Core.Interfaces.Repositories
{
  public interface IEnrollmentRepository : IRepository<Enrollment>
  {
    Task<Enrollment?> GetByIdAsync(int studentId, int courseId, int periodId);
  }
}
