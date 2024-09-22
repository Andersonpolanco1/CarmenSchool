namespace CarmenSchool.Core.DTOs.StudentDTO
{
  public class StudentUpdateRequest
  {
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
  }
}
