using CarmenSchool.Core.DTOs.EnrollmentsDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Core.Models;
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
      var studentEnrollments = await enrollmentRepository.GetByUnikeIdAsync(request.StudentId, request.CourseId, request.PeriodId);

      if (studentEnrollments != null)
        throw new InvalidOperationException("El estudiante ya esta inscrito en este curso en este período.");

      var student = await GetValidStudent(request.StudentId);
      var course = await GetValidCourse(request.CourseId);
      var period = await GetValidPeriod(request.PeriodId);
      
      var newEnrollment = request.ToEntity(course,student,period);
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

      if (request.StudentId.HasValue && enrollmentDb.StudentId != request.StudentId)
      {
        var student = await GetValidStudent(request.StudentId!.Value);
        enrollmentDb.Student = student;
      }

      if (request.CourseId.HasValue && enrollmentDb.CourseId != request.CourseId)
      {
        var course = await GetValidCourse(request.CourseId!.Value);
        enrollmentDb.Course = course;
      }

      if (request.PeriodId.HasValue && enrollmentDb.PeriodId != request.PeriodId)
      {
        var period = await GetValidPeriod(request.PeriodId!.Value);
        enrollmentDb.Period = period;
      }

      return enrollmentRepository.IsModified(enrollmentDb) ?
        await enrollmentRepository.UpdateAsync(enrollmentDb)
        : true;
    }

    public async Task<IEnumerable<Enrollment>> FindAsync(Expression<Func<Enrollment, bool>> expression)
    {
      var courses = await enrollmentRepository.FindAsync(expression);
      return courses;
    }

    private async Task<Period> GetValidPeriod(int periodId)
    {
      return await periodService.GetByIdAsync(periodId) ?? throw new InvalidOperationException("Período no existe.");
    }

    private async Task<Course> GetValidCourse(int courseId)
    {
      return await courseService.GetByIdAsync(courseId) ?? throw new InvalidOperationException("Curso no existe.");
    }

    private async Task<Student> GetValidStudent(int studentId)
    {
      return await studentService.GetByIdAsync(studentId) ?? throw new InvalidOperationException("Estudiante no existe.");
    }
  }
}
