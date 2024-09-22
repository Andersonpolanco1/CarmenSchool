using CarmenSchool.Core.Models;


namespace CarmenSchool.Core.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetByDNIAsync(string dni);
    }
}
