using CarmenSchool.Core.Interfaces;
using CarmenSchool.Web.Utils;
using Microsoft.AspNetCore.Http;
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
      var studentJsonFileFullPath = new PathUtils(hostEnvironment).GetFullPath(MockJsonFilePathStudents);
      var newRecords = await studentService.InsertFromJsonFile(studentJsonFileFullPath);
      var response = new { TotalNewStudents = newRecords};
      return Ok(response);
    }
  }
}
