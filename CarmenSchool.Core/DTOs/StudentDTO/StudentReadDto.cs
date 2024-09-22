namespace CarmenSchool.Core.DTOs.StudentDTO
{
  public class StudentReadDto
  {
    public int Id { get; set; }
    public required string DNI { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
  }
}
