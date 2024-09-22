using CarmenSchool.Core.Interfaces;
using CarmenSchool.Core.Models;
using CarmenSchool.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarmenSchool.Infrastructure.Repositories
{
    public class StudentRepository(AplicationDbContext context, ILogger<StudentRepository> logger) : IStudentRepository
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
                logger.LogError(message:ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await context.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await context.Students.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> UpdateAsync(Student entity)
        {
            context.Students.Update(entity);
            var affectedRows = await context.SaveChangesAsync();
            return affectedRows > 0;
        }
    }
}
