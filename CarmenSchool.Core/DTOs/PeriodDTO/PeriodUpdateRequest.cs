using CarmenSchool.Core.Utils;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CarmenSchool.Core.DTOs.PeriodDTO
{
  public class PeriodUpdateRequest
  {
    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[-/](0[1-9]|1[0-2])[-/](\d{4})$",
        ErrorMessage = "El formato de la fecha de inicio debe ser dd-MM-yyyy o dd/MM/yyyy.")]
    public string? StartDate { get; set; }

    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[-/](0[1-9]|1[0-2])[-/](\d{4})$",
        ErrorMessage = "El formato de la fecha de fin debe ser dd-MM-yyyy o dd/MM/yyyy.")]
    public string? EndDate { get; set; }

    public DateOnly? GetStartDateAsDateOnly() 
    {

      if (StartDate == null)
        return null; 

      return DateOnly.ParseExact(StartDate, DateTimeUtils.PERIOD_STRING_DATE_FORMATS, CultureInfo.InvariantCulture);
    }
    public DateOnly? GetEndDateAsDateOnly() 
    {
      if (EndDate == null)
        return null;

      return DateOnly.ParseExact(EndDate, DateTimeUtils.PERIOD_STRING_DATE_FORMATS, CultureInfo.InvariantCulture); 
    }
  }
}
