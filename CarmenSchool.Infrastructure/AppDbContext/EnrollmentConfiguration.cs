using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
  public void Configure(EntityTypeBuilder<Enrollment> builder)
  {
    builder.HasKey(e => new { e.CourseId, e.StudentId });

    builder.Property(e => e.CourseId)
        .IsRequired();

    builder.Property(e => e.StudentId)
        .IsRequired();

    builder.Property(e => e.CreatedDate)
        .IsRequired(); 

    builder.HasOne(e => e.Course)
        .WithMany(c => c.Enrollments)
        .HasForeignKey(e => e.CourseId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.Student)
        .WithMany(s => s.Enrollments)
        .HasForeignKey(e => e.StudentId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
