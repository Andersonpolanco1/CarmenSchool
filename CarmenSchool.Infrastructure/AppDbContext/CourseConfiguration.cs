using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarmenSchool.Infrastructure.AppDbContext
{
  internal class CourseConfiguration : IEntityTypeConfiguration<Course>
  {
    public void Configure(EntityTypeBuilder<Course> builder)
    {
      builder.HasKey(e => e.Id);

      builder.Property(e => e.Name)
          .IsRequired()
          .HasMaxLength(120);

      builder.Property(e => e.CreatedDate)
          .IsRequired();

      builder.Property(e => e.Description)
          .IsRequired()
          .HasMaxLength(500);

      builder.HasMany(e => e.Enrollments)
          .WithOne()
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
