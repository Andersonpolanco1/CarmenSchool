namespace CarmenSchool.Core.Models
{
    public class Period
    {
        public int Id { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public virtual List<Enrollment>? Enrollments { get; set; }
    }
}
