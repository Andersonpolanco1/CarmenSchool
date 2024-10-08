﻿using CarmenSchool.Core.DTOs.CourseDTO;
using CarmenSchool.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarmenSchool.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CoursesController(ICourseService CourseService) : ControllerBase
  {

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CourseQueryFilters filters)
    {
      try
      {
        var courses = await CourseService.FindAsync(filters);
        return Ok(courses.Map(c => c.ToRead()));
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
        var course = await CourseService.GetByIdAsync(id);
        return course == null ? NotFound() : Ok(course.ToRead());
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CourseCreateRequest request)
    {
      try
      {
        var newCourse = await CourseService.AddAsync(request);
        return CreatedAtAction(nameof(Get), new { id = newCourse.Id }, newCourse.ToRead());
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
    public async Task<IActionResult> Put(int id, [FromBody] CourseUpdateDto request)
    {
      try
      {
        var success  = await CourseService.UpdateAsync(id, request);
        return success ? NoContent() : NotFound();
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await CourseService.DeleteByIdAsync(id);
        return success ? NoContent() : NotFound();
    }
  }
}

