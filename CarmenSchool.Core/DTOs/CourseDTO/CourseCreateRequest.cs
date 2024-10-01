using CarmenSchool.Core.Helpers;
using CarmenSchool.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace CarmenSchool.Core.DTOs.CourseDTO
{
  public class CourseCreateRequest 
  {
    [StringLength(50, ErrorMessage = "El nombre del curso debe tener un máximo de {1} caracteres.")]
    public required string Name { get; set; }

    [StringLength(200, ErrorMessage = "La descripción curso debe tener un máximo de {1} caracteres.")]
    public required string Description { get; set; }

    public Course ToEntity()
    {
      return new Course { Name = Name.CapitalizeWords(), Description = Description };
    }
  }
}
