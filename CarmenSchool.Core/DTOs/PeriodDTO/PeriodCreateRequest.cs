using CarmenSchool.Core.Atributes;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;

public class PeriodCreateRequest
{
  [LocalDateString]
  public required string StartDate { get; set; }

  [LocalDateString]
  public required string EndDate { get; set; }

  public DateOnly GetStartDateAsDateOnly() => DateTimeUtils.ToDateOnly(StartDate);
  public DateOnly GetEndDateAsDateOnly() => DateTimeUtils.ToDateOnly(EndDate);
  public Period ToEntity()
  {
    return new Period { EndDate = GetEndDateAsDateOnly(), StartDate = GetStartDateAsDateOnly() };
  }
}