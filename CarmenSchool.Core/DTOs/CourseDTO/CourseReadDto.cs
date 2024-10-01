namespace CarmenSchool.Core.DTOs.CourseDTO
{
  public class CourseReadDto
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
  }
}
