using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.Extensions.Logging;

namespace CarmenSchool.Infrastructure.Repositories
{
  public class CourseRepository(ApplicationDbContext context, ILogger<CourseRepository> logger) 
    : BaseRepository<Course>(context, logger), ICourseRepository
  {
  }
}
