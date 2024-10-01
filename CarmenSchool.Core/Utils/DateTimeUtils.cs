using System.Text;

namespace CarmenSchool.Core.Utils
{
  public static class DateTimeUtils
  {
    public static string[] PERIOD_STRING_DATE_FORMATS =
    [
      "dd-MM-yyyy",
      "dd/MM/yyyy"
    ];

    public static string CalculateYearsAndMonthsBetweenDates(DateOnly startDate, DateOnly endDate)
    {
      if (startDate > endDate)
        throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha de fin.");

      int years = endDate.Year - startDate.Year;
      int months = endDate.Month - startDate.Month;

      if (months < 0)
      {
        years--;
        months += 12; 
      }

      if (startDate.Day > endDate.Day)
      {
        months--; 
        if (months < 0) 
        {
          years--;
          months += 12;
        }
      }

      var durationSB = new StringBuilder();

      if (years > 0)
        durationSB.Append($"{years} año{(years > 1 ? "s" : string.Empty)}");

      if (years > 0 && months > 0)
        durationSB.Append($" y ");

      if (months > 0)
        durationSB.Append($"{months} mes{(months > 1 ? "es" : string.Empty)}");

      return durationSB.ToString();

    }
  }
}
