using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class StudentConfiguration : IEntityTypeConfiguration<Student>
{
  public void Configure(EntityTypeBuilder<Student> builder)
  {
    builder.HasKey(s => s.Id);

    builder.Property(s => s.DNI)
        .IsRequired()
        .HasMaxLength(20);

    builder.Property(s => s.FullName)
        .IsRequired()
        .HasMaxLength(150);

    builder.Property(s => s.Email)
        .IsRequired()
        .HasMaxLength(100);

    builder.Property(s => s.PhoneNumber)
        .HasMaxLength(15);

    builder.Property(s => s.CreatedDate)
        .IsRequired();

    builder.HasMany(s => s.Enrollments)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);
  }
}
