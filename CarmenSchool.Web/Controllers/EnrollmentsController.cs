using CarmenSchool.Core.DTOs.EnrollmentsDTO;
using CarmenSchool.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarmenSchool.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EnrollmentsController(IEnrollmentService enrollmentService) : ControllerBase
  {
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var enrollments = await enrollmentService.GetAllAsync();
        return Ok(enrollments.Select(e => e.ToRead()));
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        var enrollment = await enrollmentService.GetByIdAsync(id);
        return enrollment == null ? NotFound() : Ok(enrollment.ToRead());
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EnrollmentCreateRequest request)
    {
      try
      {
        var newEnrollment = await enrollmentService.AddAsync(request);
        return CreatedAtAction(nameof(Get), new { id = newEnrollment.Id }, newEnrollment.ToRead());
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
    public async Task<IActionResult> Put(int id, [FromBody] EnrollmentUpdateDto request)
    {
      try
      {
        var success = await enrollmentService.UpdateAsync(id, request);
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
      var success = await enrollmentService.DeleteByIdAsync(id);
      return success ? NoContent() : NotFound();
    }
  }
}
