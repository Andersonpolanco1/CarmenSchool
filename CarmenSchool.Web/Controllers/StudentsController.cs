﻿using CarmenSchool.Core.DTOs.StudentDTO;
using CarmenSchool.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarmenSchool.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController(IStudentService studentService) : ControllerBase
  {

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] StudentQueryFilter filters)
    {
      try
      {
        var students = await studentService.FindAsync(filters);
        return Ok(students.Map(s => s.ToRead()));
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
        var student = await studentService.GetByIdAsync(id);
        return student == null ? NotFound() : Ok(student.ToRead());
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
        var newStudent = await studentService.AddAsync(request);
        return CreatedAtAction(nameof(Get), new { id = newStudent.Id }, newStudent.ToRead());
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
        var success  = await studentService.UpdateAsync(id, request);
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
        var success = await studentService.DeleteByIdAsync(id);
        return success ? NoContent() : NotFound();
    }
  }
}

