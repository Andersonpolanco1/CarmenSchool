using CarmenSchool.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarmenSchool.Web.Controllers
{
    [Route("api/[controller]")]
  [ApiController]
  public class MockController(IStudentService studentService, IHostEnvironment hostEnvironment, IConfiguration configuration) : ControllerBase
  {
    [HttpGet("load-students")]
    public async Task<IActionResult> Students()
    {
      var MockJsonFilePathStudents = configuration.GetValue<string>("MockJsonFilePaths:Students");
      var studentJsonFileFullPath = GetFullPath(MockJsonFilePathStudents);
      var newRecords = await studentService.InsertFromJsonFile(studentJsonFileFullPath);
      var response = new { TotalNewStudents = newRecords};
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
