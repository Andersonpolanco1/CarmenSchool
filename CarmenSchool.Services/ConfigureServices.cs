using CarmenSchool.Core.Interfaces;
using CarmenSchool.Services.Internal;
using Microsoft.Extensions.DependencyInjection;


namespace CarmenSchool.Services
{
  public static class ConfigureServices
  {
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
      services.AddScoped<IStudentService, StudentService>();
      return services;
    }
  }
}
