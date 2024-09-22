
using CarmenSchool.Core.DTOs.StudentDTO;

namespace CarmenSchool.Core.Models
{
  public class Student
  {
    public int Id { get; set; }
    public required string DNI { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual List<Enrollment> Enrollments { get; set; } = [];


    public StudentReadDto ToStudentReadDto()
    {
      return new StudentReadDto { DNI = DNI, FullName = FullName, Email = Email, PhoneNumber = PhoneNumber, Id=Id };
    }
  }
}
