using CarmenSchool.Core.DTOs.EnrollmentsDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using System.Linq.Expressions;

namespace CarmenSchool.Services.Internal
{
  internal class EnrollmentService(
    IEnrollmentRepository enrollmentRepository,
    IStudentService studentService,
    ICourseService courseService,
    IPeriodService periodService) : IEnrollmentService
  {
    public async Task<Enrollment> AddAsync(EnrollmentCreateRequest request)
    {
      var student = await GetValidStudent(request.StudentId);
      var course = await GetValidCourse(request.CourseId);
      var period = await GetValidPeriod(request.PeriodId);

      await ValidateEnrollment(request.StudentId, request.CourseId, request.PeriodId);

      var newEnrollment = request.ToEntity(course, student, period);
      newEnrollment.CreatedDate = DateTime.Now;
      await enrollmentRepository.AddAsync(newEnrollment);
      return newEnrollment;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
      var enrollmentDb = await enrollmentRepository.GetByIdAsync(id);
      return enrollmentDb != null && await enrollmentRepository.DeleteAsync(enrollmentDb);
    }

    public async Task<IEnumerable<Enrollment>> GetAllAsync()
    {
      var enrollment = await enrollmentRepository.GetAllAsync(e => e.Student, e => e.Period, e => e.Course);
      return enrollment is null ?
        [] : enrollment.ToList();
    }

    public async Task<Enrollment?> GetByIdAsync(int id)
    {
      return await enrollmentRepository.GetByIdAsync(id, e => e.Student, e => e.Period, e => e.Course);
    }

    public async Task<bool> UpdateAsync(int id, EnrollmentUpdateDto request)
    {
      var enrollmentDb = await enrollmentRepository.GetByIdAsync(id);

      if (enrollmentDb == null)
        return false;

      if (ValidationUtils.FieldHasChanged(request.StudentId, enrollmentDb.StudentId))
        enrollmentDb.Student = await GetValidStudent(request.StudentId!.Value);

      if (ValidationUtils.FieldHasChanged(request.CourseId, enrollmentDb.CourseId))
        enrollmentDb.Course = await GetValidCourse(request.CourseId!.Value);

      if (ValidationUtils.FieldHasChanged(request.PeriodId, enrollmentDb.PeriodId))
        enrollmentDb.Period = await GetValidPeriod(request.PeriodId!.Value);

      if (enrollmentRepository.IsModified(enrollmentDb))
      {
        await ValidateEnrollment(enrollmentDb.Student.Id, enrollmentDb.Course.Id, enrollmentDb.Period.Id);
        await enrollmentRepository.UpdateAsync(enrollmentDb);
      }

      return true;
    }

    public async Task<IEnumerable<Enrollment>> FindAsync(Expression<Func<Enrollment, bool>> expression)
    {
      var courses = await enrollmentRepository.FindAsync(expression);
      return courses;
    }

    private async Task ValidateEnrollment(int studentId, int courseId, int periodId)
    {
      var studentEnrollments = await enrollmentRepository.GetByUnikeIdAsync(studentId, courseId, periodId);

      if (studentEnrollments != null)
        throw new InvalidOperationException("El estudiante ya esta inscrito en este curso en este período.");
    }

    private async Task<Period> GetValidPeriod(int periodId) =>
      await periodService.GetByIdAsync(periodId) ?? throw new InvalidOperationException("Período no existe.");

    private async Task<Course> GetValidCourse(int courseId) =>
       await courseService.GetByIdAsync(courseId) ?? throw new InvalidOperationException("Curso no existe.");

    private async Task<Student> GetValidStudent(int studentId) =>
       await studentService.GetByIdAsync(studentId) ?? throw new InvalidOperationException("Estudiante no existe.");
  }
}
