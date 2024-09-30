using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Helpers;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using System.Linq.Expressions;

namespace CarmenSchool.Services.Internal
{
  public class CourseService(ICourseRepository courseRepository) : ICourseService
  {
    public async Task<CourseReadDto> AddAsync(CourseCreateDto request)
    {
      var course = await courseRepository.FindAsync(c => c.Name.ToUpper() == request.Name.ToUpper());

      if (course != null && course.Any())
          throw new InvalidOperationException("Ya existe un curso registrado con ese nombre");

      var newCourse = request.ToEntity();
      newCourse.CreatedDate = DateTime.Now;
      await courseRepository.AddAsync(newCourse);
      return newCourse.ToRead();
    }


    public async Task<bool> DeleteByIdAsync(int id)
    {
      var studentDb = await courseRepository.GetByIdAsync(id);
      return studentDb != null && await courseRepository.DeleteAsync(studentDb);
    }

    public async Task<IEnumerable<CourseReadDto>> GetAllAsync()
    {
      var courses = await courseRepository.GetAllAsync();
      return courses is null ?
        [] : courses.Select(c => c.ToRead()).ToList();
    }

    public async Task<CourseReadDto?> GetByIdAsync(int id)
    {
      var course = await courseRepository.GetByIdAsync(id);
      return course?.ToRead() ?? null;
    }

    public async Task<bool> UpdateAsync(int id, CourseUpdateDto request)
    {
      var course = await courseRepository.GetByIdAsync(id);

      if (course == null)
        return false;

      course.Description = request.Description;
      course.Name = request.Name.CapitalizeWords();

      return await courseRepository.UpdateAsync(course);
    }

    public async Task<IEnumerable<CourseReadDto>> FindAsync(Expression<Func<Course, bool>> expression)
    {
      var courses = await courseRepository.FindAsync(expression);
      return courses.Select(s => s.ToRead());
    }

    public async Task<int> InsertFromJsonFile(string jsonPath)
    {
      int totalNewRecords = 0;

      try
      {
        var courses = await JsonUtils.GetObjectFromJsonAsync<CourseCreateDto>(jsonPath);

        foreach (var course in courses)
        {
          await AddAsync(course);
          totalNewRecords++;
        }
      }
      catch (Exception)
      {
      }

      return totalNewRecords;
    }
  }
}
