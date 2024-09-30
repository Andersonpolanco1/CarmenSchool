namespace CarmenSchool.Core.DTOs.CourseDTO
{
  public class CourseReadDto
  {
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
  }
}
