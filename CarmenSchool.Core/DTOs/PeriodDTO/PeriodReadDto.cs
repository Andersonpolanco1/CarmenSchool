using CarmenSchool.Core.Utils;

namespace CarmenSchool.Core.DTOs.PeriodDTO
{
  public class PeriodReadDto
  {
    public int Id { get; set; }
    public DateOnly StartDate { get; set; } = DateOnly.MinValue;
    public DateOnly EndDate { get; set; } = DateOnly.MinValue;
    public string Duration
    {
      get
      {
        return DateTimeUtils.CalculateYearsAndMonthsBetweenDates(StartDate, EndDate);
      }
    }

  }
}
