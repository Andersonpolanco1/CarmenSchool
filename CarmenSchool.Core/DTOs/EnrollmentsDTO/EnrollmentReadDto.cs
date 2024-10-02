using CarmenSchool.Core.Models;

namespace CarmenSchool.Core.DTOs.EnrollmentsDTO
{
  public class EnrollmentReadDto
  {
    public int Id { get; set; }

    public virtual required Course Course { get; set; }

    public virtual required Student Student { get; set; }

    public DateTime CreatedDate { get; set; }
  }
}
