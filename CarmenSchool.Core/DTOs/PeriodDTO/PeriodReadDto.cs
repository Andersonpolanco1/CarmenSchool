using CarmenSchool.Core.Utils;

namespace CarmenSchool.Core.DTOs.PeriodDTO
{
  public class PeriodReadDto
  {
    public int Id { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string Duration
    {
      get
      {
        return DateTimeUtils.CalculateYearsAndMonthsBetweenDates(StartDate, EndDate);
      }
    }

  }
}
