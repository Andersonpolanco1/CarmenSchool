using Microsoft.EntityFrameworkCore;

namespace CarmenSchool.Core.Utils
{
  public class PaginatedList<T>
  {
    private int MaxPageSize { get; set; }
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    public int TotalRows { get; set; }
    public int TotalPages { get; private set; }
    public List<T> Data { get; set; } = [];

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, int maxPageSize)
    {
      MaxPageSize = maxPageSize;
      TotalRows = count;
      PageIndex = pageIndex;
      PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
      TotalPages = (int)Math.Ceiling(TotalRows / (double)PageSize);
      Data.AddRange(items);
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, int maxPageSize)
    {
      var count = await source.CountAsync();
      pageSize = pageSize > maxPageSize ? maxPageSize : pageSize;
      var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
      return new PaginatedList<T>(items, count, pageIndex, pageSize, maxPageSize);
    }

    //Método para mapear la lista a otro tipo 
    public PaginatedList<U> Map<U>(Func<T, U> mapFunction)
    {
      var mappedData = Data.Select(mapFunction).ToList();
      return new PaginatedList<U>(mappedData, TotalRows, PageIndex, PageSize, MaxPageSize);
    }
  }
}
