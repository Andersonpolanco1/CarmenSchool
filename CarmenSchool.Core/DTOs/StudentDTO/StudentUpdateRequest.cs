using System.ComponentModel.DataAnnotations;

namespace CarmenSchool.Core.DTOs.StudentDTO
{
  public class StudentUpdateRequest
  {
    [EmailAddress]
    public required string Email { get; set; }


    [Phone(ErrorMessage = "El número de teléfono no es válido.")]
    [StringLength(15, ErrorMessage = "El número de teléfono debe tener un máximo de {1} caracteres.")]
    public string? PhoneNumber { get; set; }
  }
}
