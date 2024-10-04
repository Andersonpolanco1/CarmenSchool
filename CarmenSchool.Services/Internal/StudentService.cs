using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using System.Linq.Expressions;

namespace CarmenSchool.Services.Internal
{
  internal class StudentService(IStudentRepository studentRepository) : IStudentService
  {
    public async Task<Student> AddAsync(StudentCreateRequest request)
    {
      await ValidateStudent(request.Email, request.DNI);
      var newStudent = request.ToEntity();
      newStudent.CreatedDate = DateTime.Now;
      await studentRepository.AddAsync(newStudent);
      return newStudent;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
      var studentDb = await studentRepository.GetByIdAsync(id);
      return studentDb != null && await studentRepository.DeleteAsync(studentDb);
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
      var students = await studentRepository.GetAllAsync();
      return students is null ?
        [] :  students.OrderBy(s => s.FullName).ToList();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
      var student = await studentRepository.GetByIdAsync(id);
      return student;
    }

    public async Task<Student?> GetByDNIAsync(string dni)
    {
      var student = await studentRepository.GetByDNIAsync(dni);
      return student;
    }

    public async Task<bool> UpdateAsync(int id,StudentUpdateRequest request)
    {
      var studentDb = await studentRepository.GetByIdAsync(id);

      if (studentDb == null) 
        return false;

      if(ValidationUtils.FieldHasChanged(request.Email, studentDb.Email))
        studentDb.Email = request.Email!.ToLower();

      if (ValidationUtils.FieldHasChanged(request.PhoneNumber, studentDb.PhoneNumber))
        studentDb.PhoneNumber = request.PhoneNumber;

      if (studentRepository.IsModified(studentDb))
      {
        await ValidateStudent(studentDb.Email);
        await studentRepository.UpdateAsync(studentDb);
      }

      return true;
    }

    public async Task<IEnumerable<Student>> FindAsync(Expression<Func<Student, bool>> expression)
    {
      var students = await studentRepository.FindAsync(expression);
      return students.OrderBy(s => s.FullName);
    }

    private async Task ValidateStudent(string email, string? dni = null)
    {
      var emailUpper = email.ToUpper();
      var dniUpper = dni?.ToUpper();

      Expression<Func<Student, bool>> filter = s =>
          s.Email.ToUpper() == emailUpper || (!string.IsNullOrEmpty(dniUpper) && s.DNI.ToUpper() == dniUpper);

      var students = await studentRepository.FindAsync(filter);

      if (students?.Any() == true)
      {
        if (students.Any(s => s.Email.ToUpper() == emailUpper))
          throw new InvalidOperationException($"Ya existe un estudiante registrado con el Email {email}");

        if (!string.IsNullOrEmpty(dniUpper) && students.Any(s => s.DNI.ToUpper() == dniUpper))
          throw new InvalidOperationException($"Ya existe un estudiante registrado con el DNI {dni}");
      }
    }

    public async Task<PaginatedList<Student>> FindAsync(StudentQueryFilter filters)
    {
      return await studentRepository.FindAsync(filters);  
    }
  }
}
