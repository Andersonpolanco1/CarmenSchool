using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces;
using CarmenSchool.Core.Models;
using System.Linq.Expressions;

namespace CarmenSchool.Services.Internal
{
  public class StudentService(IStudentRepository studentRepository) : IStudentService
  {
    public async Task<StudentReadDto> AddAsync(StudentCreateRequest request)
    {
      var student = await studentRepository.FindAsync(s => s.DNI == request.DNI || s.Email == request.Email);

      if (student != null && student.Any())
      {
        var existingEmail = student.FirstOrDefault(s => s.Email.ToUpper() == request.Email.ToUpper());
        var existingDNI = student.FirstOrDefault(s => s.DNI.ToUpper() == request.DNI.ToUpper());

        if (existingEmail != null)
          throw new InvalidOperationException($"Ya existe un estudiante registrado con el Email {request.Email}");

        if (existingDNI != null)
          throw new InvalidOperationException($"Ya existe un estudiante registrado con el DNI {request.DNI}");
      }

      var newStudent = request.ToEntity();
      newStudent.CreatedDate = DateTime.Now;
      await studentRepository.AddAsync(newStudent);
      return newStudent.ToStudentReadDto();
    }


    public async Task<bool> DeleteByIdAsync(int id)
    {
      var studentDb = await studentRepository.GetByIdAsync(id);
      return studentDb != null && await studentRepository.DeleteAsync(studentDb);
    }

    public async Task<IEnumerable<StudentReadDto>> GetAllAsync()
    {
      var students = await studentRepository.GetAllAsync();
      return students is null ?
        [] :  students.Select(s => s.ToStudentReadDto()).ToList();
    }

    public async Task<StudentReadDto?> GetByIdAsync(int id)
    {
      var student = await studentRepository.GetByIdAsync(id);
      return student?.ToStudentReadDto() ?? null;
    }

    public async Task<StudentReadDto?> GetByDNIAsync(string dni)
    {
      var student = await studentRepository.GetByDNIAsync(dni);
      return student?.ToStudentReadDto() ?? null;
    }

    public async Task<bool> UpdateAsync(int id,StudentUpdateRequest request)
    {
      var studentDb = await studentRepository.GetByIdAsync(id);

      if (studentDb == null) 
        return false;

      studentDb.Email = request.Email.ToLower();
      studentDb.PhoneNumber = request.PhoneNumber;

      return await studentRepository.UpdateAsync(studentDb);
    }

    public async Task<IEnumerable<StudentReadDto>> FindAsync(Expression<Func<Student, bool>> expression)
    {
      var students = await studentRepository.FindAsync(expression);
      return students.Select(s => s.ToStudentReadDto());
    }
  }
}
