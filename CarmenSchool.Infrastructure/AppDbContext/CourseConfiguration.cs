using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarmenSchool.Infrastructure.AppDbContext
{
  internal class CourseConfiguration : IEntityTypeConfiguration<Course>
  {
    public void Configure(EntityTypeBuilder<Course> builder)
    {
      builder.HasKey(c => c.Id);

      builder.Property(c => c.Name)
          .IsRequired()
          .HasMaxLength(120);

      builder.Property(c => c.CreatedDate)
          .IsRequired();

      builder.Property(c => c.Description)
          .IsRequired()
          .HasMaxLength(500);

      builder.HasMany(c => c.Enrollments)
          .WithOne()
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
