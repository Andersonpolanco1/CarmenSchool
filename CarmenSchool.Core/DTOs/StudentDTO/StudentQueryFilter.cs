namespace CarmenSchool.Core.DTOs.StudentDTO
{
  public class StudentQueryFilter : BaseQueryFilter
  {
    public string? DNI { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
  }
}
