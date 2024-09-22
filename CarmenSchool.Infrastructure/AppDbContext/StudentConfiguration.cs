using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
  public void Configure(EntityTypeBuilder<Student> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.DNI)
        .IsRequired()
        .HasMaxLength(20);

    builder.Property(e => e.FullName)
        .IsRequired()
        .HasMaxLength(150);

    builder.Property(e => e.Email)
        .IsRequired()
        .HasMaxLength(100);

    builder.Property(e => e.PhoneNumber)
        .HasMaxLength(15);

    builder.Property(e => e.CreatedDate)
        .IsRequired();

    builder.HasMany(e => e.Enrollments)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);
  }
}
