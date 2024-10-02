using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.Interfaces;
using System.Text.Json.Serialization;

namespace CarmenSchool.Core.Models
{
  public class Course : IBaseEntity
  {
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual List<Enrollment>? Enrollments { get; set; } = new List<Enrollment>();
    public CourseReadDto ToRead()
    {
      return new CourseReadDto { Id = Id, Description = Description, Name = Name };
    }
  }
}
