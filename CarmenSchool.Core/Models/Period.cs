using CarmenSchool.Core.DTOs.PeriodDTO;
using CarmenSchool.Core.Interfaces;
using CarmenSchool.Core.Utils;

namespace CarmenSchool.Core.Models
{
  public class Period : IBaseEntity
  {
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual List<Enrollment>? Enrollments { get; set; }

    public PeriodReadDto ToRead()
    {
      return new PeriodReadDto 
      {
        Id = Id, 
        StartDate = StartDate.ToLocalDateString(), 
        EndDate = EndDate.ToLocalDateString() 
      };
    }
  }
}
