using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
  public void Configure(EntityTypeBuilder<Enrollment> builder)
  {
    builder.HasKey(e => e.Id); 

    builder.HasIndex(e => new { e.CourseId, e.StudentId, e.PeriodId })
      .IsUnique();

    builder.HasOne(e => e.Course)
        .WithMany(c => c.Enrollments)
        .HasForeignKey(e => e.CourseId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Student)
        .WithMany(s => s.Enrollments)
        .HasForeignKey(e => e.StudentId)
        .OnDelete(DeleteBehavior.Cascade);

    builder
        .HasOne(e => e.Period)
        .WithMany(p => p.Enrollments)
        .HasForeignKey(e => e.PeriodId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
