using CarmenSchool.Core.DTOs.EnrollmentsDTO;
using CarmenSchool.Core.Interfaces;
using CarmenSchool.Core.Utils;

namespace CarmenSchool.Core.Models
{
  public class Enrollment : IBaseEntity
  {
    public int Id { get; set; }

    public required int CourseId { get; set; }
    public virtual required Course Course { get; set; }

    public required int StudentId { get; set; }
    public virtual required Student Student { get; set; }

    public int PeriodId { get; set; }
    public virtual required Period Period { get; set; }

    public DateTime CreatedDate { get; set; }

    public EnrollmentReadDto ToRead()
    {
      return new EnrollmentReadDto
      {
        EnrollmentId = Id,
        EnrollmentCreatedDate = CreatedDate,
        CourseId = CourseId,
        CourseName = Course.Name,
        StudentDNI = Student.DNI,
        StudentFullName = Student.FullName,
        StudentId = Student.Id,
        PeriodId = Period.Id,
        PeriodEndDate = Period.EndDate.ToLocalDateString(),
        PeriodStartDate = Period.StartDate.ToLocalDateString()
      };
    }
  }
}
