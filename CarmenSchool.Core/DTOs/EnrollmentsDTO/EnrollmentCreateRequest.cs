using CarmenSchool.Core.Models;

namespace CarmenSchool.Core.DTOs.EnrollmentsDTO
{
  public class EnrollmentCreateRequest
  {
    public required int CourseId { get; set; }
    public required int StudentId { get; set; }
    public required int PeriodId { get; set; }

    public Enrollment ToEntity(Course course, Student student, Period period)
    {
      return new Enrollment
      {
        CourseId = CourseId,
        StudentId = StudentId,
        PeriodId = PeriodId,
        Course = course,
        Student = student,
        Period = period
      };
    }
  }
}
