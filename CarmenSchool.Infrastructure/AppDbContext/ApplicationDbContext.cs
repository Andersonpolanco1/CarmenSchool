using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarmenSchool.Infrastructure.AppDbContext
{
  internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Period> Periods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new CourseConfiguration());
      modelBuilder.ApplyConfiguration(new StudentConfiguration());
      modelBuilder.ApplyConfiguration(new PeriodConfiguration());
      modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
    }
  }
}
