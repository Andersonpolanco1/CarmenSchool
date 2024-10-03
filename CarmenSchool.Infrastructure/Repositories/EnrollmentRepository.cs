using CarmenSchool.Core;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarmenSchool.Infrastructure.Repositories
{
  internal class EnrollmentRepository
    (
      ApplicationDbContext context,
      ILogger<EnrollmentRepository> logger,
      IOptions<ConfigurationsOptions> options) 
    : BaseRepository<Enrollment>(context, logger, options), IEnrollmentRepository
  {
    public async Task<Enrollment?> GetByUnikeIdAsync(int studentId, int courseId, int periodId)
    {
      return await context.Enrollments.FirstOrDefaultAsync(e =>
      e.StudentId == studentId &&
      e.CourseId == courseId &&
      e.PeriodId == periodId);
    }
  }
}
