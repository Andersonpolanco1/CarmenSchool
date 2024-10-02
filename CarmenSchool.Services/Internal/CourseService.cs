using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.Helpers;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Core.Models;
using System.Linq.Expressions;

namespace CarmenSchool.Services.Internal
{
  internal class CourseService(ICourseRepository courseRepository) : ICourseService
  {
    public async Task<Course> AddAsync(CourseCreateRequest request)
    {
      var course = await courseRepository.FindAsync(c => c.Name.ToUpper() == request.Name.ToUpper());

      if (course != null && course.Any())
          throw new InvalidOperationException("Ya existe un curso registrado con ese nombre");

      var newCourse = request.ToEntity();
      newCourse.CreatedDate = DateTime.Now;
      await courseRepository.AddAsync(newCourse);
      return newCourse;
    }


    public async Task<bool> DeleteByIdAsync(int id)
    {
      var studentDb = await courseRepository.GetByIdAsync(id);
      return studentDb != null && await courseRepository.DeleteAsync(studentDb);
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
      var courses = await courseRepository.GetAllAsync();
      return courses is null ?
        [] : courses.ToList();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
      var course = await courseRepository.GetByIdAsync(id);
      return course ?? null;
    }

    public async Task<bool> UpdateAsync(int id, CourseUpdateDto request)
    {
      var courseDb = await courseRepository.GetByIdAsync(id);

      if (courseDb == null)
        return false;

      if(request.Name != null)
        courseDb.Name = request.Name.CapitalizeWords();

      if (request.Description != null)
        courseDb.Description = request.Description;

      return courseRepository.IsModified(courseDb) ?
        await courseRepository.UpdateAsync(courseDb)
        : false;
    }

    public async Task<IEnumerable<Course>> FindAsync(Expression<Func<Course, bool>> expression)
    {
      var courses = await courseRepository.FindAsync(expression);
      return courses ?? [];
    }
  }
}
