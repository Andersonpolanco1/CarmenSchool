using CarmenSchool.Core;
using CarmenSchool.Core.DTOs;
using CarmenSchool.Core.DTOs.PeriodDTO;
using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarmenSchool.Infrastructure.Repositories
{
  internal class PeriodRepository(
    ApplicationDbContext context,
    ILogger<PeriodRepository> logger,
    IOptions<ConfigurationsOptions> options) 
    : BaseRepository<Period>(context, logger, options), IPeriodRepository
  {
    public override async Task<PaginatedList<Period>> FindAsync(BaseQueryFilter filters)
    {
      if (filters is not PeriodQueryFilters periodQueryFilters)
        return await base.FindAsync(filters);

      IQueryable<Period> entityQuery = GetBaseQueryFilter(periodQueryFilters);

      if (!string.IsNullOrEmpty(periodQueryFilters.StartDateFrom))
        entityQuery = entityQuery.Where(s => s.StartDate >= DateTimeUtils.ToDateOnly(periodQueryFilters.StartDateFrom));

      if (!string.IsNullOrEmpty(periodQueryFilters.StartDateTo))
        entityQuery = entityQuery.Where(s => s.StartDate <= DateTimeUtils.ToDateOnly(periodQueryFilters.StartDateTo));

      if (!string.IsNullOrEmpty(periodQueryFilters.EndDateFrom))
        entityQuery = entityQuery.Where(s => s.EndDate >= DateTimeUtils.ToDateOnly(periodQueryFilters.EndDateFrom));

      if (!string.IsNullOrEmpty(periodQueryFilters.EndDateTo))
        entityQuery = entityQuery.Where(s => s.EndDate <= DateTimeUtils.ToDateOnly(periodQueryFilters.EndDateTo));

      //Si no pasaron el campo de ordenamiento o si el campo de ordenamiento pasado no existe en la clase, se agrega ordenamiento por Id por defecto
      if (string.IsNullOrEmpty(filters.SortFieldName) || !ValidationUtils.TryGetProperty<StudentQueryFilters>(filters.SortFieldName, out string foundPropertyName))
      {
        entityQuery = entityQuery.OrderBy(u => u.Id);
      }
      else
      {
        entityQuery = filters.SortOrder == SortOrder.Ascending
            ? entityQuery.OrderBy(e => EF.Property<object>(e, foundPropertyName))
            : entityQuery.OrderByDescending(e => EF.Property<object>(e, foundPropertyName));
      }

      var data = await PaginatedList<Period>.CreateAsync(entityQuery, filters.PageIndex, filters.PageSize, options.MaxPageSize);

      return data;
    }
  }
}
