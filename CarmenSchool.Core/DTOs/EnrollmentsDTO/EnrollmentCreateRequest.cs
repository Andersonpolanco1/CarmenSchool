namespace CarmenSchool.Core.DTOs.EnrollmentsDTO
{
  public class EnrollmentCreateRequest
  {
    public required int CourseId { get; set; }
    public required int StudentId { get; set; }
    public required int PeriodId { get; set; }
  }
}
