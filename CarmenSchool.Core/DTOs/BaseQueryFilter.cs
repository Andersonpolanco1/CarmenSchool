using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CarmenSchool.Core.DTOs
{
  public abstract class BaseQueryFilter 
  {
    private const int DEFAULT_PAGE_SIZE = 15;
    private const int DEFAULT_PAGE_INDEX = 1;

    public int? Id { get; set; }

    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[-/](0[1-9]|1[0-2])[-/](\d{4})$",
    ErrorMessage = "El formato de la fecha de creacion desde, debe ser dd-MM-yyyy o dd/MM/yyyy.")]
    public string? CreatedDateFrom { get; set; }

    [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[-/](0[1-9]|1[0-2])[-/](\d{4})$",
    ErrorMessage = "El formato de la fecha de creacion hasta, debe ser dd-MM-yyyy o dd/MM/yyyy.")]
    public string? CreatedDateTo { get; set; }


    [Range(1, int.MaxValue, ErrorMessage = "PageIndex debe ser mayor o igual a 1.")]
    public int PageIndex { get; set; }


    [Range(1, int.MaxValue, ErrorMessage = "PageSize debe ser mayor o igual a 1")]
    public int PageSize { get; set; }

    public string? SortFieldName { get; set; }

    public SortOrder? SortOrder { get; set; }

    protected BaseQueryFilter()
    {
      PageIndex = DEFAULT_PAGE_INDEX;
      PageSize = DEFAULT_PAGE_SIZE;
      SortOrder = DTOs.SortOrder.Ascending;
    }
  }

  public enum SortOrder
  {
    [EnumMember(Value = "Ascending Order")]
    Ascending = 0,

    [EnumMember(Value = "Descending Order")]
    Descending = 1
  }
}
