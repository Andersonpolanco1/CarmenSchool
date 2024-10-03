using CarmenSchool.Core;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarmenSchool.Infrastructure.Repositories
{
  internal class PeriodRepository(ApplicationDbContext context, ILogger<PeriodRepository> logger, IOptions<ConfigurationsOptions> options)
    : BaseRepository<Period>(context, logger,options), IPeriodRepository
  {
  }
}
