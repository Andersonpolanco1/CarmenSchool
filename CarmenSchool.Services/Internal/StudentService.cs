using CarmenSchool.Core.DTOs.Student;
using CarmenSchool.Core.Interfaces;
using CarmenSchool.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmenSchool.Services.Internal
{
    public class StudentService(IStudentRepository studentRepository)
    {
        public async Task<Student> AddAsync(StudentCreateRequest entity)
        {
            var student = studentRepository.GetByIdAsync

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
