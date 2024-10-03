
using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces;
using System.Text.Json.Serialization;

namespace CarmenSchool.Core.Models
{
  public class Student : IBaseEntity
  {
    public int Id { get; set; }
    public required string DNI { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }

    [JsonIgnore]
    public virtual List<Enrollment>? Enrollments { get; set; } = new List<Enrollment>();
    public StudentReadDto ToRead()
    {
      return new StudentReadDto
      {
        DNI = DNI,
        FullName = FullName,
        Email = Email,
        PhoneNumber = PhoneNumber,
        Id = Id
      };
    }
  }
}
