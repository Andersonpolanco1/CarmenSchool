using CarmenSchool.Core.DTOs;
using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Services
{
  public interface IStudentService
  {
    Task<Student> AddAsync(StudentCreateRequest request);
    Task<bool> DeleteByIdAsync(int id);
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<Student?> GetByDNIAsync(string dni);
    Task<bool> UpdateAsync(int id, StudentUpdateRequest request);
    Task<IEnumerable<Student>> FindAsync(Expression<Func<Student, bool>> expression);
    Task<PaginatedList<Student>> FindAsync(StudentQueryFilters filters);
  }
}