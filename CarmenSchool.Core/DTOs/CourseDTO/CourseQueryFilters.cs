namespace CarmenSchool.Core.DTOs.CourseDTO
{
  public class CourseQueryFilters : BaseQueryFilter
  {
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
  }
}
