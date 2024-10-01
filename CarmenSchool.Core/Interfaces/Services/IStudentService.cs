using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Models;
using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Services
{
    public interface IStudentService
    {
        Task<StudentReadDto> AddAsync(StudentCreateRequest request);
        Task<bool> DeleteByIdAsync(int id);
        Task<IEnumerable<StudentReadDto>> GetAllAsync();
        Task<StudentReadDto?> GetByIdAsync(int id);
        Task<StudentReadDto?> GetByDNIAsync(string dni);
        Task<bool> UpdateAsync(int id, StudentUpdateRequest request);
        Task<IEnumerable<StudentReadDto>> FindAsync(Expression<Func<Student, bool>> expression);
    }
}