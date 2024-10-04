using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class PeriodConfiguration : IEntityTypeConfiguration<Period>
{
  public void Configure(EntityTypeBuilder<Period> builder)
  {

    builder.HasKey(p => p.Id);


    builder.HasIndex(e => new { e.StartDate, e.EndDate})
        .IsUnique();

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
