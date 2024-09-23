using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarmenSchool.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController(IStudentService studentService) : ControllerBase
  {

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var students = await studentService.GetAllAsync();
      return Ok(students);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var student = await studentService.GetByIdAsync(id);
      return student == null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] StudentCreateRequest request)
    {
      try
      {
        var newStudent = await studentService.AddAsync(request);
        return CreatedAtAction(nameof(Get), new { id = newStudent.Id }, newStudent);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] StudentUpdateRequest request)
    {
      try
      {
        var success  = await studentService.UpdateAsync(id, request);
        return success ? NoContent() : NotFound();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await studentService.DeleteByIdAsync(id);
        return success ? NoContent() : NotFound();
    }
  }
}

