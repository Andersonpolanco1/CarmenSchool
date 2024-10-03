using CarmenSchool.Core;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarmenSchool.Infrastructure.Repositories
{
  internal class CourseRepository(ApplicationDbContext context, ILogger<CourseRepository> logger, IOptions<ConfigurationsOptions> options) 
    : BaseRepository<Course>(context, logger, options), ICourseRepository
  {
  }
}
