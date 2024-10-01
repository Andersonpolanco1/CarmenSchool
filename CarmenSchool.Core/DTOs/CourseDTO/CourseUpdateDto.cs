using System.ComponentModel.DataAnnotations;

namespace CarmenSchool.Core.DTOs.CourseDTO
{
  public class CourseUpdateDto
  {
    [StringLength(50, ErrorMessage = "El nombre del curso debe tener un máximo de {1} caracteres.")]
    public string? Name { get; set; }


    [StringLength(200, ErrorMessage = "La descripción curso debe tener un máximo de {1} caracteres.")]
    public string? Description { get; set; }
  }
}
