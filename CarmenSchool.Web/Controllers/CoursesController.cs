using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces;
using CarmenSchool.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarmenSchool.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CoursesController(ICourseService CourseService) : ControllerBase
  {

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var students = await CourseService.GetAllAsync();
        return Ok(students);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        var student = await CourseService.GetByIdAsync(id);
        return student == null ? NotFound() : Ok(student);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] StudentCreateRequest request)
    {
      try
      {
        var newStudent = await CourseService.AddAsync(request);
        return CreatedAtAction(nameof(Get), new { id = newStudent.Id }, newStudent);
      }
      catch (InvalidOperationException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] StudentUpdateRequest request)
    {
      try
      {
        var success  = await CourseService.UpdateAsync(id, request);
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
        var success = await CourseService.DeleteByIdAsync(id);
        return success ? NoContent() : NotFound();
    }
  }
}

