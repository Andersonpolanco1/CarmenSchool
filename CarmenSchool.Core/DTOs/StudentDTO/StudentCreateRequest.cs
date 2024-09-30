using CarmenSchool.Core.Helpers;
using CarmenSchool.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace CarmenSchool.Core.DTOs.StudentDTO
{
  public class StudentCreateRequest
  {
    [StringLength(20, ErrorMessage = "El DNI debe tener un máximo de {1} caracteres.")]
    public required string DNI { get; set; }


    [StringLength(150, MinimumLength = 4,ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
    public required string FullName { get; set; }


    [EmailAddress]
    public required string Email { get; set; }


    [Phone(ErrorMessage = "El número de teléfono no es válido.")]
    [StringLength(15, ErrorMessage = "El número de teléfono debe tener un máximo de {1} caracteres.")]
    public string? PhoneNumber { get; set; }


    public Student ToEntity()
    {
      return new Student 
      {
        DNI = DNI.ToUpper(),
        FullName = FullName.CapitalizeWords(),
        Email = Email.ToLower(),
        PhoneNumber = PhoneNumber
      };
    }
  }
}
