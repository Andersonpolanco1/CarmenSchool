﻿using CarmenSchool.Core.Interfaces;
using CarmenSchool.Infrastructure.AppDbContext;
using CarmenSchool.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CarmenSchool.Infrastructure
{
  public static class ConfigureServices
  {
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped<IStudentRepository, StudentRepository>();

      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
      );
      return services;
    }
  }
}
