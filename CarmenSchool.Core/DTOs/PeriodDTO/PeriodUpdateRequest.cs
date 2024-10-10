using CarmenSchool.Core.Atributes;
using CarmenSchool.Core.Utils;

namespace CarmenSchool.Core.DTOs.PeriodDTO
{
  public class PeriodUpdateRequest
  {
    [LocalDateString]
    public string? StartDate { get; set; }

    [LocalDateString]
    public string? EndDate { get; set; }

    public DateOnly? GetStartDateAsDateOnly() 
    {
      if (StartDate == null)
        return null; 

      return DateTimeUtils.ToDateOnly(StartDate);
    }
    public DateOnly? GetEndDateAsDateOnly() 
    {
      if (EndDate == null)
        return null;

      return DateTimeUtils.ToDateOnly(EndDate);
    }
  }
}
