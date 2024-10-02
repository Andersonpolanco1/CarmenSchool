using CarmenSchool.Core.DTOs.EnrollmentsDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Core.Models;
using System.Linq.Expressions;

namespace CarmenSchool.Services.Internal
{
  public class EnrollmentService(
    IEnrollmentRepository enrollmentRepository, 
    IStudentService studentService,
    ICourseService courseService,
    IPeriodService periodService) 
    : IEnrollmentService
  {
    public async Task<Enrollment> AddAsync(EnrollmentCreateRequest request)
    {
      var studentEnrollments = await enrollmentRepository.GetByIdAsync(request.StudentId, request.CourseId, request.PeriodId);

      if (studentEnrollments != null)
        throw new InvalidOperationException("El estudiante ya esta inscrito en este curso en este periodo.");

      var student = await studentService.GetByIdAsync(request.StudentId) ?? throw new InvalidOperationException("No se pudo realizar la inscripcion, el estudiante no existe.");
      var course = await courseService.GetByIdAsync(request.CourseId) ?? throw new InvalidOperationException("No se pudo realizar la inscripcion, el curso no existe.");
      var period = await periodService.GetByIdAsync(request.PeriodId) ?? throw new InvalidOperationException("No se pudo realizar la inscripcion, el periodo no existe.");
      
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
      var enrollment = await enrollmentRepository.GetByIdAsync(id, e => e.Student, e => e.Period, e => e.Course);
      return enrollment;
    }

    public async Task<bool> UpdateAsync(int id, EnrollmentUpdateDto request)
    {
      var enrollmentDb = await enrollmentRepository.GetByIdAsync(id);

      if (enrollmentDb == null)
        throw new InvalidOperationException("Inscripcion no encontrada");

      if (request.StudentId.HasValue){
        var student = await studentService.GetByIdAsync(request.StudentId!.Value) ?? throw new InvalidOperationException("No se pudo actualizar la inscripcion, el estudiante no existe.");
        enrollmentDb.Student = student;
      }

      if (request.CourseId.HasValue)
      {
        var course = await courseService.GetByIdAsync(request.CourseId!.Value) ?? throw new InvalidOperationException("No se pudo actualizar la inscripcion, el curso no existe.");
        enrollmentDb.Course = course;
      }

      if (request.PeriodId.HasValue)
      {
        var period = await periodService.GetByIdAsync(request.PeriodId!.Value) ?? throw new InvalidOperationException("No se pudo actualizar la inscripcion, el curso no existe.");
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
  }
}
