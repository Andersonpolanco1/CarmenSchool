using CarmenSchool.Core.DTOs.PeriodDTO;
using CarmenSchool.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarmenSchool.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PeriodsController(IPeriodService periodService) : ControllerBase
  {
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var periods = await periodService.GetAllAsync();
        return Ok(periods);
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
        var period = await periodService.GetByIdAsync(id);
        return period == null ? NotFound() : Ok(period);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PeriodCreateRequest request)
    {
      try
      {
        var newPeriod = await periodService.AddAsync(request);
        return CreatedAtAction(nameof(Get), new { id = newPeriod.Id }, newPeriod);
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
    public async Task<IActionResult> Put(int id, [FromBody] PeriodUpdateRequest request)
    {
      try
      {
        var success = await periodService.UpdateAsync(id, request);
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
      var success = await periodService.DeleteByIdAsync(id);
      return success ? NoContent() : NotFound();
    }
  }
}
