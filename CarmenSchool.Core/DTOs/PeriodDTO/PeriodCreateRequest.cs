using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using System.ComponentModel.DataAnnotations;

public class PeriodCreateRequest
{
  [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[-/](0[1-9]|1[0-2])[-/](\d{4})$",
      ErrorMessage = "El formato de la fecha de inicio debe ser dd-MM-yyyy o dd/MM/yyyy.")]
  public required string StartDate { get; set; }

  [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[-/](0[1-9]|1[0-2])[-/](\d{4})$",
      ErrorMessage = "El formato de la fecha de fin debe ser dd-MM-yyyy o dd/MM/yyyy.")]
  public required string EndDate { get; set; }

  public DateOnly GetStartDateAsDateOnly() => DateTimeUtils.ToDateOnly(StartDate);
  public DateOnly GetEndDateAsDateOnly() => DateTimeUtils.ToDateOnly(EndDate);
  public Period ToEntity()
  {
    return new Period { EndDate = GetEndDateAsDateOnly(), StartDate = GetStartDateAsDateOnly() };
  }
}