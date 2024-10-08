﻿using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Infrastructure;
using CarmenSchool.Services.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace CarmenSchool.Services
{
  public static class ConfigureServices
  {
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
      services.AddInfrastructureLayer();

      services.AddScoped<IStudentService, StudentService>();
      services.AddScoped<ICourseService, CourseService>();
      services.AddScoped<IPeriodService, PeriodService>();
      services.AddScoped<IEnrollmentService, EnrollmentService>();
      return services;
    }
  }
}
