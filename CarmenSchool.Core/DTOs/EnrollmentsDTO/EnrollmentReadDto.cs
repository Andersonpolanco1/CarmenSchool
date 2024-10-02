using CarmenSchool.Core.Utils;

namespace CarmenSchool.Core.DTOs.EnrollmentsDTO
{
  public class EnrollmentReadDto
  {
    //inscripcion
    public required int EnrollmentId { get; set; }
    public required DateTime EnrollmentCreatedDate { get; set; }
    //curso
    public required int CourseId { get; set; }
    public required string CourseName { get; set; }
    //estudiante
    public required int StudentId { get; set; }
    public required string StudentDNI { get; set; }
    public required string StudentFullName { get; set; }
    //periodo
    public required int PeriodId { get; set; }
    public required string PeriodStartDate { get; set; } = string.Empty;
    public required string PeriodEndDate { get; set; } = string.Empty;
    public string PeriodDuration
    {
      get
      {
        return DateTimeUtils.CalculateYearsAndMonthsBetweenDates(PeriodStartDate, PeriodEndDate);
      }
    }
  }
}
