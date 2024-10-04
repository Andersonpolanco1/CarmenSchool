using CarmenSchool.Core.Configurations;
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
      var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

      services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
      {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
      });

      services.Configure<ConfigurationsOptions>(options =>
      {
        configuration.GetSection("Configurations").Bind(options);
      });

      services.AddScoped<IStudentRepository, StudentRepository>();
      services.AddScoped<ICourseRepository, CourseRepository>();
      services.AddScoped<IPeriodRepository, PeriodRepository>();
      services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

      return services;
    }
  }
}
