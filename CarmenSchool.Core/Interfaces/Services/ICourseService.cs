using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.Models;
using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Services
{
    public interface ICourseService
    {
        Task<Course> AddAsync(CourseCreateRequest request);
        Task<bool> DeleteByIdAsync(int id);
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, CourseUpdateDto request);
        Task<IEnumerable<Course>> FindAsync(Expression<Func<Course, bool>> expression);
    }
}