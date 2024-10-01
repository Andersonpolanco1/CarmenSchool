using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace CarmenSchool.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MockController(
      IStudentService studentService, 
      ICourseService courseService, 
      IHostEnvironment hostEnvironment, 
      IConfiguration configuration
      ) : ControllerBase
  {
    [HttpGet("load-students")]
    public async Task<IActionResult> Students()
    {
      try
      {
        var configurationKey = "MockJsonFilePaths:Students";
        var result = await LoadDataFromJsonAsync<StudentCreateRequest>(configurationKey, studentService.AddAsync);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return Problem(detail:ex.Message);
      }
    }

    [HttpGet("load-courses")]
    public async Task<IActionResult> Courses()
    {
      try
      {
        var configurationKey = "MockJsonFilePaths:Courses";
        var result = await LoadDataFromJsonAsync<CourseCreateRequest>(configurationKey, courseService.AddAsync);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return Problem(detail: ex.Message);
      }
    }

    private async Task<object> LoadDataFromJsonAsync<T>(string configKey, Func<T, Task> addAsyncMethod)
    {
      List<T>? data;

      try
      {
        var MockJsonFileRelativePath = configuration.GetValue<string>(configKey);
        var MockJsonFileFullPath = GetFullPath(MockJsonFileRelativePath);
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
