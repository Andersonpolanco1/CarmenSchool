using CarmenSchool.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace CarmenSchool.Core.DTOs.Student
{
    public class StudentCreateRequest
    {
        public required string DNI { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
