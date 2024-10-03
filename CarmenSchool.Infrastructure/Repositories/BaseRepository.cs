using CarmenSchool.Core;
using CarmenSchool.Core.DTOs;
using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Helpers;
using CarmenSchool.Core.Interfaces;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;


namespace CarmenSchool.Infrastructure.Repositories
{
  internal abstract class BaseRepository<T>(ApplicationDbContext context, ILogger<BaseRepository<T>> logger, IOptions<ConfigurationsOptions> options) : IRepository<T>
    where T : class, IBaseEntity
  {
    protected readonly ConfigurationsOptions options = options.Value;
    protected readonly ApplicationDbContext context = context;
    protected readonly ILogger<BaseRepository<T>> logger = logger;
    public const string ERROR_OBTENIENDO_REGISTROS = "Se produjo un error interno al intentar recuperar el/los registro(s).";

    public async Task<T> AddAsync(T entity)
    {
      await context.AddAsync(entity);
      await context.SaveChangesAsync();
      return entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
      try
      {
        context.Remove(entity);
        var affectedRows = await context.SaveChangesAsync();
        return affectedRows > 0;

      }
      catch (Exception ex)
      {
        logger.LogError(message: ex.Message);
        return false;
      }
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
      var result = await context.Set<T>().Where(expression).ToListAsync();
      return result ?? [];
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[]? includes)
    {
      try
      {
        IQueryable<T> query = context.Set<T>().AsNoTracking();

        if (includes != null)
        {
          foreach (var include in includes)
            query = query.Include(include);
        }

        return await query.ToListAsync();
      }
      catch (Exception ex)
      {
        logger.LogError(message: ex.Message);
        throw new Exception(ERROR_OBTENIENDO_REGISTROS);
      }
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[]? includes)
    {
      try
      {
        IQueryable<T> query = context.Set<T>();

        if (includes != null)
        {
          foreach (var include in includes)
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(s => s.Id == id);
      }
      catch (Exception ex)
      {
        logger.LogError(ex.Message);
        throw new Exception(ERROR_OBTENIENDO_REGISTROS);
      }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
      try
      {
        context.Set<T>().Update(entity);
        var affectedRows = await context.SaveChangesAsync();
        return affectedRows > 0;
      }
      catch (Exception ex)
      {
        logger.LogError(message: ex.Message);
        throw;
      }
    }

    public bool IsModified(T entity)
    {
      var entry = context.Entry(entity);
      return entry.State == EntityState.Modified;
    }

    public virtual async Task<PaginatedList<T>> FindAsync(BaseQueryFilter filters)
    {
      IQueryable<T> entityQuery = GetBaseQueryFilter(filters);

      //Si no pasaron el campo de ordenamiento o si el campo de ordenamiento pasado no existe en la clase, se agrega ordenamiento por Id por defecto
      if (string.IsNullOrEmpty(filters.OrderByField) || !ValidationUtils.TryGetProperty<BaseQueryFilter>(filters.OrderByField, out string foundProperty))
      {
        entityQuery = entityQuery.OrderBy(u => u.Id);
      }
      else
      {
        entityQuery = filters.SortOrder == SortOrder.Ascending
            ? entityQuery.OrderBy(e => EF.Property<object>(e, foundProperty))
            : entityQuery.OrderByDescending(e => EF.Property<object>(e, foundProperty));
      }

      var data = await PaginatedList<T>.CreateAsync(entityQuery, filters.PageIndex, filters.PageSize, options.MaxPageSize);

      return data;
    }

    public IQueryable<T> GetBaseQueryFilter(BaseQueryFilter filters)
    {
      IQueryable<T> entityQuery = context.Set<T>().AsNoTracking();

      if (filters.Id.HasValue)
        entityQuery = entityQuery.Where(s => s.Id == filters.Id);

      if (!string.IsNullOrEmpty(filters.CreatedDateStart))
        entityQuery = entityQuery.Where(s => DateOnly.FromDateTime(s.CreatedDate) >= DateTimeUtils.ToDateOnly(filters.CreatedDateStart));

      if (!string.IsNullOrEmpty(filters.CreatedDateEnd))
        entityQuery = entityQuery.Where(s => DateOnly.FromDateTime(s.CreatedDate) <= DateTimeUtils.ToDateOnly(filters.CreatedDateEnd));

      return entityQuery;
    }
  }
}
