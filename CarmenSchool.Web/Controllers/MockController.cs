using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using CarmenSchool.Core.DTOs.EnrollmentsDTO;
using CarmenSchool.Core.Configurations;
using Microsoft.Extensions.Options;

namespace CarmenSchool.Web.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class MockController(
      IStudentService studentService, 
      ICourseService courseService, 
      IPeriodService periodService, 
      IEnrollmentService enrollmentService, 
      IHostEnvironment hostEnvironment,
      IOptions<ConfigurationsOptions> options
      ) : ControllerBase
  {
    private readonly MockJsonFilePathsOptions mockJsonFilePaths = options.Value.MockJsonFilePaths;

    [HttpGet]
    public async Task<IActionResult> Students()
    {
      try
      {
        var result = await LoadDataFromJsonAsync<StudentCreateRequest>(mockJsonFilePaths.Students, studentService.AddAsync);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return Problem(detail:ex.Message);
      }
    }

    [HttpGet]
    public async Task<IActionResult> Courses()
    {
      try
      {
        var result = await LoadDataFromJsonAsync<CourseCreateRequest>(mockJsonFilePaths.Courses, courseService.AddAsync);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return Problem(detail: ex.Message);
      }
    }

    [HttpGet]
    public async Task<IActionResult> Periods()
    {
      try
      {
        var result = await LoadDataFromJsonAsync<PeriodCreateRequest>(mockJsonFilePaths.Periods, periodService.AddAsync);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return Problem(detail: ex.Message);
      }
    }

    [HttpGet]
    public async Task<IActionResult> Enrollments()
    {
      try
      {
        var result = await LoadDataFromJsonAsync<EnrollmentCreateRequest>(mockJsonFilePaths.Enrollments, enrollmentService.AddAsync);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return Problem(detail: ex.Message);
      }
    }

    private async Task<object> LoadDataFromJsonAsync<T>(string relativeFilePath, Func<T, Task> addAsyncMethod)
    {
      List<T>? data;

      try
      {
        var MockJsonFileFullPath = GetFullPath(relativeFilePath);
        var json = await System.IO.File.ReadAllTextAsync(MockJsonFileFullPath, Encoding.UTF8);
        data = JsonSerializer.Deserialize<List<T>>(json);
      }
      catch (Exception)
      {
        throw;
      }

      int totalNewRecords = 0;
      int totalErrorRecords = 0;

      if (data != null && data.Count != 0)
      {
        foreach (var record in data)
        {
          try
          {
            await addAsyncMethod(record);
            totalNewRecords++;
          }
          catch (InvalidOperationException)
          {
            totalErrorRecords++;
          }
        }
      }
      return new { TotalNewRecords = totalNewRecords, TotalErrorRecords = totalErrorRecords };
    }

    private string GetFullPath(string? relativePath)
    {
      if (string.IsNullOrEmpty(relativePath)) return string.Empty;

      var basePath = GetRootPath();
      return Path.Combine(basePath ?? string.Empty, relativePath);
    }

    private string? GetRootPath()
    {
      var webPath = hostEnvironment.ContentRootPath;
      return new DirectoryInfo(webPath)?.Parent?.FullName;
    }
  }
}
