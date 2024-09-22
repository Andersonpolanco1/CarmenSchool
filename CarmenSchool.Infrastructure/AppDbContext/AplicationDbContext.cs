using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace CarmenSchool.Infrastructure.AppDbContext
{
  public class AplicationDbContext(DbContextOptions<AplicationDbContext> options) : DbContext(options)
  {
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Period> Periods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new CourseConfiguration());
      modelBuilder.ApplyConfiguration(new StudentConfiguration());
      modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
      modelBuilder.ApplyConfiguration(new PeriodConfiguration());
    }
  }
}
