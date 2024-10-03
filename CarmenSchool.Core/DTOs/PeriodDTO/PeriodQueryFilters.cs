using CarmenSchool.Core.Atributes;

namespace CarmenSchool.Core.DTOs.PeriodDTO
{
  public class PeriodQueryFilters : BaseQueryFilter
  {
    [LocalDateString]
    public string? StartDateFrom { get; set; }

    [LocalDateString]
    public string? StartDateTo { get; set; }


    [LocalDateString]
    public string? EndDateFrom { get; set; }


    [LocalDateString]
    public string? EndDateTo { get; set; }
  }
}
