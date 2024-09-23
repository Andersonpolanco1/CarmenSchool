using CarmenSchool.Core.Interfaces;
using CarmenSchool.Core.Models;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CarmenSchool.Infrastructure.Repositories
{
  public class StudentRepository(ApplicationDbContext context, ILogger<StudentRepository> logger) : IStudentRepository
  {
    public async Task<Student> AddAsync(Student entity)
    {
      await context.AddAsync(entity);
      await context.SaveChangesAsync();
      return entity;
    }

    public async Task<bool> DeleteAsync(Student entity)
    {
      try
      {
        context.Students.Remove(entity);
        var affectedRows = await context.SaveChangesAsync();
        return affectedRows > 0;

      }
      catch (Exception ex)
      {
        logger.LogError(message: ex.Message);
        return false;
      }
    }

    public async Task<IEnumerable<Student>> FindAsync(Expression<Func<Student, bool>> expression)
    {
      var result = await context.Students.Where(expression).ToListAsync();
      return result ?? [];
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
      return await context.Students.AsNoTracking().ToListAsync();
    }

    public async Task<Student?> GetByDNIAsync(string dni)
    {
      return await context.Students.FirstOrDefaultAsync(s => s.DNI == dni);
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
      return await context.Students.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> UpdateAsync(Student entity)
    {
      try
      {
        context.Students.Update(entity);
        var affectedRows = await context.SaveChangesAsync();
        return affectedRows > 0;
      }
      catch (Exception ex)
      {
        logger.LogError(message:ex.Message);
        return false;
      }
    }
  }
}
