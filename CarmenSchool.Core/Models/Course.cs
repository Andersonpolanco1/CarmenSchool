using System.ComponentModel.DataAnnotations;

namespace CarmenSchool.Core.Models
{
    public class Course
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; } 
        public virtual List<Enrollment> Enrollments { get; set; } = [];
    }
}
