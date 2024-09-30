using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PeriodConfiguration : IEntityTypeConfiguration<Period>
{
  public void Configure(EntityTypeBuilder<Period> builder)
  {

    builder.HasKey(e => e.Id);

    builder.Property(e => e.StartDate)
        .IsRequired();

    builder.Property(e => e.EndDate)
        .IsRequired();

    builder.Property(e => e.CreatedDate)
    .IsRequired();

    builder.HasMany(e => e.Enrollments)
        .WithOne() 
        .OnDelete(DeleteBehavior.Cascade); 

  }
}
