using CarmenSchool.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarmenSchool.Web.Controllers
{
    [Route("api/[controller]")]
  [ApiController]
  public class MockController(IStudentService studentService, ICourseService courseService, IHostEnvironment hostEnvironment, IConfiguration configuration) : ControllerBase
  {
    [HttpGet("load-students")]
    public async Task<IActionResult> Students()
    {
      var MockJsonFilePath = configuration.GetValue<string>("MockJsonFilePaths:Students");
      var studentJsonFileFullPath = GetFullPath(MockJsonFilePath);
      var newRecords = await studentService.InsertFromJsonFile(studentJsonFileFullPath);
      var response = new { TotalNewRecords = newRecords};
      return Ok(response);
    }

    [HttpGet("load-courses")]
    public async Task<IActionResult> Courses()
    {
      var MockJsonFilePath = configuration.GetValue<string>("MockJsonFilePaths:Courses");
      var studentJsonFileFullPath = GetFullPath(MockJsonFilePath);
      var newRecords = await courseService.InsertFromJsonFile(studentJsonFileFullPath);
      var response = new { TotalNewRecords = newRecords };
      return Ok(response);
    }

    private string GetFullPath(string relativePath)
    {
      if (relativePath == null) return string.Empty;

      var basePath = GetRootPath();
      return Path.Combine(basePath, relativePath);
    }

    private string GetRootPath()
    {
      var webPath = hostEnvironment.ContentRootPath;
      return new DirectoryInfo(webPath).Parent.FullName;
    }
  }
}
