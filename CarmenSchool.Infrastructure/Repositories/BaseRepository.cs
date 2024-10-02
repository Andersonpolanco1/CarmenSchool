using CarmenSchool.Core.Interfaces;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;


namespace CarmenSchool.Infrastructure.Repositories
{
  public abstract class BaseRepository<T>(ApplicationDbContext context, ILogger<BaseRepository<T>> logger) : IRepository<T>
    where T : class, IBaseEntity
  {
    protected readonly ApplicationDbContext context = context;
    protected readonly ILogger<BaseRepository<T>> logger = logger;
    public const string ERROR_OBTENIENDO_REGISTROS = "Ha ocurrido un error interno al intentar obtener los registros.";
    public const string ERROR_OBTENIENDO_REGISTRO = "Ha ocurrido un error interno al intentar obtener el registro.";

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
        throw new Exception(ERROR_OBTENIENDO_REGISTRO);
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
        return false;
      }
    }

    public bool IsModified(T entity)
    {
      var entry = context.Entry(entity);
      return entry.State == EntityState.Modified;
    }
  }
}
