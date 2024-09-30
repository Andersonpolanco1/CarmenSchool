using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Core.Models;
using System.Linq.Expressions;

namespace CarmenSchool.Services.Internal
{
  public class CourseService(IStudentRepository studentRepository) : ICourseService
  {
    public Task<StudentReadDto> AddAsync(StudentCreateRequest request)
    {
      throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<StudentReadDto>> FindAsync(Expression<Func<Student, bool>> expression)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<StudentReadDto>> GetAllAsync()
    {
      throw new NotImplementedException();
    }

    public Task<StudentReadDto?> GetByDNIAsync(string dni)
    {
      throw new NotImplementedException();
    }

    public Task<StudentReadDto?> GetByIdAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<int> InsertFromJsonFile(string jsonPath)
    {
      throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, StudentUpdateRequest request)
    {
      throw new NotImplementedException();
    }
  }
}
