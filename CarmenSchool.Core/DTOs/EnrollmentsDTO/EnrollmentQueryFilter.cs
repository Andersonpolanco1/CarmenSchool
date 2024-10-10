using CarmenSchool.Core.Atributes;

namespace CarmenSchool.Core.DTOs.EnrollmentsDTO
{
  public class EnrollmentQueryFilter : BaseQueryFilter
  {
    public int? CourseId { get; set; }
    public string? CourseName { get; set; }
    public int? StudentId { get; set; }
    public string? StudentDNI { get; set; }
    public string? StudentFullName { get; set; }
    public int? PeriodId { get; set; }

    [LocalDateString]
    public string? PeriodStartDateFrom { get; set; }

    [LocalDateString]
    public string? PeriodStartDateTo { get; set; }

    [LocalDateString]
    public string? PeriodEndDateFrom { get; set; }
    [LocalDateString]
    public string? PeriodEndDateTo { get; set; }
  }
}
