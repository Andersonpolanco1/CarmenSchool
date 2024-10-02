using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PeriodConfiguration : IEntityTypeConfiguration<Period>
{
  public void Configure(EntityTypeBuilder<Period> builder)
  {

    builder.HasKey(p => p.Id);

    builder.Property(p => p.StartDate)
        .IsRequired();

    builder.Property(p => p.EndDate)
        .IsRequired();

    builder.Property(p => p.CreatedDate)
    .IsRequired();

    builder.HasMany(p => p.Enrollments)
        .WithOne() 
        .OnDelete(DeleteBehavior.Cascade); 

  }
}
