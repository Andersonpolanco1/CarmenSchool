using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using System.Linq.Expressions;

namespace CarmenSchool.Core.Interfaces.Services
{
    public interface ICourseService
    {
        Task<Course> AddAsync(CourseCreateRequest request);
        Task<bool> DeleteByIdAsync(int id);
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id, params Expression<Func<Course, object>>[]? includes);
        Task<bool> UpdateAsync(int id, CourseUpdateDto request);
        Task<PaginatedList<Course>> FindAsync(CourseQueryFilters filters);
        Task<IEnumerable<Course>> FindAsync(Expression<Func<Course, bool>> expression);
    }
}