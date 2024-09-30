using CarmenSchool.Core.Models;


namespace CarmenSchool.Core.Interfaces.Repositories
{
    public interface IStudentRepository : IRepository<Student> 
    {
        Task<Student?> GetByDNIAsync(string dni);
    }
}
