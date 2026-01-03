using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.DTOs;
using OnlineLearningPlatform.Services;
using System.Security.Claims;

namespace OnlineLearningPlatform.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CourseDto>>> GetAllCourses()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<List<CourseDto>>> GetCoursesByCategory(int categoryId)
    {
        var courses = await _courseService.GetCoursesByCategoryAsync(categoryId);
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDetailDto>> GetCourseById(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
            return NotFound(new { message = "Course not found" });

        return Ok(course);
    }

    [Authorize(Roles = "Instructor")]
    [HttpPost]
    public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CourseCreateDto dto)
    {
        var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(instructorId))
            return Unauthorized();

        var course = await _courseService.CreateCourseAsync(dto, instructorId);
        return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
    }

    [Authorize(Roles = "Instructor")]
    [HttpPut("{id}")]
    public async Task<ActionResult<CourseDto>> UpdateCourse(int id, [FromBody] CourseUpdateDto dto)
    {
        var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(instructorId))
            return Unauthorized();

        var course = await _courseService.UpdateCourseAsync(id, dto, instructorId);
        if (course == null)
            return NotFound(new { message = "Course not found or you don't have permission" });

        return Ok(course);
    }

    [Authorize(Roles = "Instructor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(instructorId))
            return Unauthorized();

        var result = await _courseService.DeleteCourseAsync(id, instructorId);
        if (!result)
            return NotFound(new { message = "Course not found or you don't have permission" });

        return NoContent();
    }

    [Authorize(Roles = "Instructor")]
    [HttpGet("instructor/my-courses")]
    public async Task<ActionResult<List<CourseDto>>> GetInstructorCourses()
    {
        var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(instructorId))
            return Unauthorized();

        var courses = await _courseService.GetInstructorCoursesAsync(instructorId);
        return Ok(courses);
    }
}
