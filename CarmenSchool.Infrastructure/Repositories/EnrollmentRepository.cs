﻿using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarmenSchool.Infrastructure.Repositories
{
  public class EnrollmentRepository(ApplicationDbContext context, ILogger<EnrollmentRepository> logger)
    : BaseRepository<Enrollment>(context, logger), IEnrollmentRepository
  {
    public async Task<Enrollment?> GetByIdAsync(int studentId, int courseId, int periodId)
    {
      return await context.Enrollments.FirstOrDefaultAsync(e =>
      e.StudentId == studentId &&
      e.CourseId == courseId &&
      e.PeriodId == periodId);
    }
  }
}
