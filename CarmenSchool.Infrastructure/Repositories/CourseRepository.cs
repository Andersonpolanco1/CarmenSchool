using CarmenSchool.Core.Configurations;
using CarmenSchool.Core.DTOs;
using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarmenSchool.Infrastructure.Repositories
{
    internal class CourseRepository
    (
      ApplicationDbContext context,
      ILogger<CourseRepository> logger,
      IOptions<ConfigurationsOptions> options) 
    : BaseRepository<Course>(context, logger, options), ICourseRepository
  {
    public override async Task<PaginatedList<Course>> FindAsync(BaseQueryFilter filters)
    {
      if (filters is not CourseQueryFilters courseFilter)
        return await base.FindAsync(filters);

      IQueryable<Course> entityQuery = GetBaseQueryFilter(courseFilter);

      if (!string.IsNullOrEmpty(courseFilter.Name))
        entityQuery = entityQuery.Where(s => s.Name.ToUpper().StartsWith(courseFilter.Name.ToUpper()));

      if (!string.IsNullOrEmpty(courseFilter.Description))
        entityQuery = entityQuery.Where(s => s.Description.ToUpper().Contains(courseFilter.Description.ToUpper()));

      return await SortAndPaginate(filters, entityQuery);
    }
  }
}
