using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.DTOs;
using OnlineLearningPlatform.Services;
using System.Security.Claims;

namespace OnlineLearningPlatform.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Student")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public async Task<ActionResult<EnrollmentDto>> EnrollInCourse([FromBody] EnrollCourseDto dto)
    {
        var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(studentId))
            return Unauthorized();

        try
        {
            var enrollment = await _enrollmentService.EnrollInCourseAsync(dto.CourseId, studentId);
            return CreatedAtAction(nameof(GetStudentEnrollments), null, enrollment);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<EnrollmentDto>>> GetStudentEnrollments()
    {
        var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(studentId))
            return Unauthorized();

        var enrollments = await _enrollmentService.GetStudentEnrollmentsAsync(studentId);
        return Ok(enrollments);
    }

    [HttpGet("progress/{courseId}")]
    public async Task<ActionResult<ProgressDto>> GetCourseProgress(int courseId)
    {
        var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(studentId))
            return Unauthorized();

        var progress = await _enrollmentService.GetCourseProgressAsync(courseId, studentId);
        if (progress == null)
            return NotFound(new { message = "Enrollment not found" });

        return Ok(progress);
    }

    [HttpPost("lesson-progress")]
    public async Task<IActionResult> UpdateLessonProgress([FromBody] UpdateLessonProgressDto dto)
    {
        var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(studentId))
            return Unauthorized();

        var result = await _enrollmentService.UpdateLessonProgressAsync(dto, studentId);
        if (!result)
            return NotFound(new { message = "Enrollment or lesson not found" });

        return NoContent();
    }
}
