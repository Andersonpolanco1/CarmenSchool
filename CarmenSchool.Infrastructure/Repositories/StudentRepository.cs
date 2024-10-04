using CarmenSchool.Core.Configurations;
using CarmenSchool.Core.DTOs;
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
    internal class StudentRepository(
    ApplicationDbContext context, 
    ILogger<StudentRepository> logger, 
    IOptions<ConfigurationsOptions> options) 
    : BaseRepository<Student>(context, logger, options), IStudentRepository
  {
    public async Task<Student?> GetByDNIAsync(string dni)
    {
      return await context.Students.FirstOrDefaultAsync(s => s.DNI.ToUpper() == dni.ToUpper());
    }

    public override async Task<PaginatedList<Student>> FindAsync(BaseQueryFilter filters)
    {
      if (filters is not StudentQueryFilter studentFilter)
        return  await base.FindAsync(filters);

      IQueryable<Student> entityQuery = GetBaseQueryFilter(studentFilter);

      if (!string.IsNullOrEmpty(studentFilter.DNI))
        entityQuery = entityQuery.Where(s => s.DNI.ToUpper() == studentFilter.DNI.ToUpper());

      if (!string.IsNullOrEmpty(studentFilter.FullName))
        entityQuery = entityQuery.Where(s => s.FullName.ToUpper().StartsWith(studentFilter.FullName.ToUpper()));

      if (!string.IsNullOrEmpty(studentFilter.Email))
        entityQuery = entityQuery.Where(s => s.Email.ToUpper().StartsWith(studentFilter.Email.ToUpper()));

      if (!string.IsNullOrEmpty(studentFilter.PhoneNumber))
        entityQuery = entityQuery.Where(s => s.PhoneNumber == studentFilter.PhoneNumber);

      return await SortAndPaginate(filters, entityQuery);
    }
  }
}
