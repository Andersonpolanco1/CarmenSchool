using CarmenSchool.Core;
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
  internal class StudentRepository(ApplicationDbContext context, ILogger<StudentRepository> logger, IOptions<ConfigurationsOptions> options) 
    : BaseRepository<Student>(context, logger, options), IStudentRepository
  {
    public async Task<Student?> GetByDNIAsync(string dni)
    {
      return await context.Students.FirstOrDefaultAsync(s => s.DNI.ToUpper() == dni.ToUpper());
    }

    public override async Task<PaginatedList<Student>> FindAsync(BaseQueryFilter filters)
    {
      if (filters is not StudentQueryFilters studentFilter)
        return  await base.FindAsync(filters);

      var studentQueryFilter = (StudentQueryFilters)filters;
      IQueryable<Student> entityQuery = GetBaseQueryFilter(studentFilter);

      if (!string.IsNullOrEmpty(studentQueryFilter.DNI))
        entityQuery = entityQuery.Where(s => s.DNI.ToUpper() == studentQueryFilter.DNI.ToUpper());

      if (!string.IsNullOrEmpty(studentQueryFilter.FullName))
        entityQuery = entityQuery.Where(s => s.FullName.ToUpper().StartsWith(studentQueryFilter.FullName.ToUpper()));

      if (!string.IsNullOrEmpty(studentQueryFilter.Email))
        entityQuery = entityQuery.Where(s => s.Email.ToUpper().StartsWith(studentQueryFilter.Email.ToUpper()));

      if (!string.IsNullOrEmpty(studentQueryFilter.PhoneNumber))
        entityQuery = entityQuery.Where(s => s.PhoneNumber == studentQueryFilter.PhoneNumber);

      //Si no pasaron el campo de ordenamiento o si el campo de ordenamiento pasado no existe en la clase, se agrega ordenamiento por Id por defecto
      if (string.IsNullOrEmpty(filters.OrderByField) || !ValidationUtils.TryGetProperty<StudentQueryFilters>(filters.OrderByField, out string foundPropertyName))
      {
        entityQuery = entityQuery.OrderBy(u => u.Id);
      }
      else
      {
        entityQuery = filters.SortOrder == SortOrder.Ascending
            ? entityQuery.OrderBy(e => EF.Property<object>(e, foundPropertyName))
            : entityQuery.OrderByDescending(e => EF.Property<object>(e, foundPropertyName));
      }

      var data = await PaginatedList<Student>.CreateAsync(entityQuery, filters.PageIndex, filters.PageSize, options.MaxPageSize);

      return data;
    }
  }
}
