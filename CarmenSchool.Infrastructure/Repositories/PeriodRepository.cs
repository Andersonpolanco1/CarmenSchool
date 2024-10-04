using CarmenSchool.Core.Configurations;
using CarmenSchool.Core.DTOs;
using CarmenSchool.Core.DTOs.PeriodDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using CarmenSchool.Infrastructure.AppDbContext;
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
      if (filters is not PeriodQueryFilter periodQueryFilters)
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

      return await SortAndPaginate(filters, entityQuery);
    }
  }
}