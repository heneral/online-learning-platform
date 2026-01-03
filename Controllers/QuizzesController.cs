using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.DTOs;
using OnlineLearningPlatform.Services;
using System.Security.Claims;

namespace OnlineLearningPlatform.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class QuizzesController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizzesController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QuizDetailDto>> GetQuizById(int id)
    {
        var quiz = await _quizService.GetQuizByIdAsync(id);
        if (quiz == null)
            return NotFound(new { message = "Quiz not found" });

        return Ok(quiz);
    }

    [Authorize(Roles = "Instructor")]
    [HttpPost]
    public async Task<ActionResult<QuizDto>> CreateQuiz([FromBody] QuizCreateDto dto)
    {
        var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(instructorId))
            return Unauthorized();

        try
        {
            var quiz = await _quizService.CreateQuizAsync(dto, instructorId);
            return CreatedAtAction(nameof(GetQuizById), new { id = quiz.Id }, quiz);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
    }

    [Authorize(Roles = "Student")]
    [HttpPost("submit")]
    public async Task<ActionResult<QuizResultDto>> SubmitQuiz([FromBody] QuizSubmissionDto dto)
    {
        var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(studentId))
            return Unauthorized();

        try
        {
            var result = await _quizService.SubmitQuizAsync(dto, studentId);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Student")]
    [HttpGet("results/course/{courseId}")]
    public async Task<ActionResult<List<QuizResultDto>>> GetQuizResults(int courseId)
    {
        var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(studentId))
            return Unauthorized();

        var results = await _quizService.GetStudentQuizResultsAsync(studentId, courseId);
        return Ok(results);
    }
}
