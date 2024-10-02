using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.Extensions.Logging;

namespace CarmenSchool.Infrastructure.Repositories
{
  internal class PeriodRepository(ApplicationDbContext context, ILogger<PeriodRepository> logger)
    : BaseRepository<Period>(context, logger), IPeriodRepository
  {
  }
}
