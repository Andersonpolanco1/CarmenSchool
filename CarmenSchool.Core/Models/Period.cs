using CarmenSchool.Core.Interfaces;

namespace CarmenSchool.Core.Models
{
    public class Period : IBaseEntity
    {
        public int Id { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public virtual List<Enrollment>? Enrollments { get; set; }
    }
}
