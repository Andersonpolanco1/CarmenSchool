
using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces;

namespace CarmenSchool.Core.Models
{
  public class Course : IBaseEntity
  {
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public virtual List<Enrollment> Enrollments { get; set; } = [];

    public CourseReadDto ToRead()
    {
      return new CourseReadDto { Id = Id, Description = Description, Name = Name, CreatedDate = CreatedDate };
    }
  }
}
