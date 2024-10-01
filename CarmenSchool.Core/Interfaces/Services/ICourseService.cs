using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.Models;
using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Services
{
    public interface ICourseService
    {
        Task<CourseReadDto> AddAsync(CourseCreateRequest request);
        Task<bool> DeleteByIdAsync(int id);
        Task<IEnumerable<CourseReadDto>> GetAllAsync();
        Task<CourseReadDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, CourseUpdateDto request);
        Task<IEnumerable<CourseReadDto>> FindAsync(Expression<Func<Course, bool>> expression);
    }
}