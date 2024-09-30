
using CarmenSchool.Core.Helpers;
using CarmenSchool.Core.Models;

namespace CarmenSchool.Core.DTOs.CourseDTO
{
  public class CourseCreateDto 
  {
    public required string Name { get; set; }
    public required string Description { get; set; }

    public Course ToEntity()
    {
      return new Course { Name = Name.CapitalizeWords(), Description = Description };
    }
  }
}
