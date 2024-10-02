using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Infrastructure.AppDbContext;
using CarmenSchool.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CarmenSchool.Infrastructure
{
  public static class ConfigureServices
  {
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
      {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
          throw new InvalidOperationException("No se ha configurado la cadena de conexión.");

        options.UseSqlServer(connectionString);
      });

      services.AddScoped<IStudentRepository, StudentRepository>();
      services.AddScoped<ICourseRepository, CourseRepository>();
      services.AddScoped<IPeriodRepository, PeriodRepository>();
      services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

      return services;
    }
  }
}
