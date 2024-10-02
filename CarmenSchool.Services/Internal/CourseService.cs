using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.Helpers;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using System.Linq.Expressions;

namespace CarmenSchool.Services.Internal
{
  internal class CourseService(ICourseRepository courseRepository) : ICourseService
  {
    public async Task<Course> AddAsync(CourseCreateRequest request)
    {
      await ValidateCourseNameIsValid(request.Name);
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

      if (ValidationUtils.FieldHasChanged(request.Name, courseDb.Name))
        courseDb.Name = request.Name!.CapitalizeWords();

      if (ValidationUtils.FieldHasChanged(request.Description, courseDb.Description))
        courseDb.Description = request.Description!;

      if (courseRepository.IsModified(courseDb))
      {
        await ValidateCourseNameIsValid(request.Name!);
        await courseRepository.UpdateAsync(courseDb);
      }

      return true;  
    }

    public async Task<IEnumerable<Course>> FindAsync(Expression<Func<Course, bool>> expression)
    {
      var courses = await courseRepository.FindAsync(expression);
      return courses ?? [];
    }

    private async Task ValidateCourseNameIsValid(string courseName)
    {
      if (string.IsNullOrWhiteSpace(courseName))
        throw new InvalidOperationException("El nombre del curso no puede estar en blanco");

      var course = await courseRepository.FindAsync(c => c.Name.ToUpper() == courseName!.ToUpper());

      if (course != null && course.Any())
        throw new InvalidOperationException("Ya existe un curso registrado con ese nombre");
    }
  }
}
