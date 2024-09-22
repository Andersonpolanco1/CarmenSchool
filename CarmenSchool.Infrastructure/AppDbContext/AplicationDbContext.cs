using CarmenSchool.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarmenSchool.Infrastructure.AppDbContext
{
    public class AplicationDbContext(DbContextOptions<AplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses{ get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Period> Periods { get; set; }
    }
}
