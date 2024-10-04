using CarmenSchool.Core.Configurations;
using CarmenSchool.Core.DTOs;
using CarmenSchool.Core.DTOs.EnrollmentsDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
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

    public async override Task<PaginatedList<Enrollment>> FindAsync(BaseQueryFilter filters)
    {
      if (filters is not EnrollmentQueryFilter enrollmentQueryFilters)
        return await base.FindAsync(filters);

      IQueryable<Enrollment> entityQuery =
        GetBaseQueryFilter(enrollmentQueryFilters,
        e => e.Student,
        e => e.Period,
        e => e.Course);

      if (enrollmentQueryFilters.CourseId.HasValue)
        entityQuery = entityQuery.Where(e => e.CourseId == enrollmentQueryFilters.CourseId);

      if (!string.IsNullOrEmpty(enrollmentQueryFilters.CourseName))
        entityQuery = entityQuery.Where(e => e.Course.Name.ToUpper().StartsWith(enrollmentQueryFilters.CourseName.ToUpper()));

      if (enrollmentQueryFilters.StudentId.HasValue)
        entityQuery = entityQuery.Where(e => e.StudentId == enrollmentQueryFilters.StudentId);

      if (!string.IsNullOrEmpty(enrollmentQueryFilters.StudentDNI))
        entityQuery = entityQuery.Where(e => e.Student.DNI.ToUpper() == enrollmentQueryFilters.StudentDNI.ToUpper());

      if (!string.IsNullOrEmpty(enrollmentQueryFilters.StudentFullName))
        entityQuery = entityQuery.Where(e => e.Student.FullName.ToUpper().StartsWith(enrollmentQueryFilters.StudentFullName.ToUpper()));

      if (enrollmentQueryFilters.PeriodId.HasValue)
        entityQuery = entityQuery.Where(e => e.PeriodId == enrollmentQueryFilters.PeriodId);

      if (!string.IsNullOrEmpty(enrollmentQueryFilters.PeriodStartDateFrom))
        entityQuery = entityQuery.Where(s => s.Period.StartDate >= DateTimeUtils.ToDateOnly(enrollmentQueryFilters.PeriodStartDateFrom));

      if (!string.IsNullOrEmpty(enrollmentQueryFilters.PeriodStartDateTo))
        entityQuery = entityQuery.Where(s => s.Period.StartDate <= DateTimeUtils.ToDateOnly(enrollmentQueryFilters.PeriodStartDateTo));

      if (!string.IsNullOrEmpty(enrollmentQueryFilters.PeriodEndDateFrom))
        entityQuery = entityQuery.Where(s => s.Period.EndDate >= DateTimeUtils.ToDateOnly(enrollmentQueryFilters.PeriodEndDateFrom));

      if (!string.IsNullOrEmpty(enrollmentQueryFilters.PeriodEndDateTo))
        entityQuery = entityQuery.Where(s => s.Period.EndDate <= DateTimeUtils.ToDateOnly(enrollmentQueryFilters.PeriodEndDateTo));

      return await SortAndPaginate(filters, entityQuery);
    }
  }
}
