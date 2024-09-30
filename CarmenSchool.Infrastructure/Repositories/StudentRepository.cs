using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace CarmenSchool.Infrastructure.Repositories
{
  public class StudentRepository(ApplicationDbContext context, ILogger<StudentRepository> logger) 
    : BaseRepository<Student>(context, logger), IStudentRepository
  {
    public async Task<Student?> GetByDNIAsync(string dni)
    {
      return await context.Students.FirstOrDefaultAsync(s => s.DNI.ToUpper() == dni.ToUpper());
    }
  }
}
