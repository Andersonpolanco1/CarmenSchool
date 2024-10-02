using CarmenSchool.Core.Interfaces;

namespace CarmenSchool.Core.Models
{
  public class Enrollment : IBaseEntity
  {
    public int Id { get; set; }

    public required int CourseId { get; set; }
    public virtual required Course Course { get; set; }

    public required int StudentId { get; set; }
    public virtual required Student Student { get; set; }

    public int PeriodId { get; set; }
    public virtual required Period Period { get; set; }

    public DateTime CreatedDate { get; set; }
  }
}
