
using CarmenSchool.Core.Interfaces;

namespace CarmenSchool.Core.Models
{
    public class Course : IBaseEntity
  {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; } 
        public virtual List<Enrollment> Enrollments { get; set; } = [];
    }
}
