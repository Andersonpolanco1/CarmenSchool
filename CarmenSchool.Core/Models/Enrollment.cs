namespace CarmenSchool.Core.Models
{
  public class Enrollment
  {
    public required int CourseId { get; set; }
    public virtual required Course Course { get; set; }

    public required int StudentId { get; set; }
    public virtual required Student Student { get; set; }

    public DateTime EnrollmentDate { get; set; }
  }
}
