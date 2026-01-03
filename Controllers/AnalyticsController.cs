using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.DTOs;

namespace OnlineLearningPlatform.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AnalyticsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AnalyticsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<AnalyticsDto>> GetAnalytics()
    {
        var totalStudents = await _context.Users
            .Where(u => u.Enrollments.Any())
            .CountAsync();

        var totalInstructors = await _context.Users
            .Where(u => u.CoursesCreated.Any())
            .CountAsync();

        var totalCourses = await _context.Courses.CountAsync();
        var totalEnrollments = await _context.Enrollments.CountAsync();
        var completedCourses = await _context.Enrollments.Where(e => e.IsCompleted).CountAsync();

        var categoryStats = await _context.Categories
            .Include(c => c.Courses)
                .ThenInclude(course => course.Enrollments)
            .Select(c => new CategoryStatDto
            {
                CategoryName = c.Name,
                CourseCount = c.Courses.Count,
                EnrollmentCount = c.Courses.SelectMany(course => course.Enrollments).Count()
            })
            .ToListAsync();

        var popularCourses = await _context.Courses
            .Include(c => c.Enrollments)
            .OrderByDescending(c => c.Enrollments.Count)
            .Take(10)
            .Select(c => new PopularCourseDto
            {
                CourseId = c.Id,
                CourseTitle = c.Title,
                EnrollmentCount = c.Enrollments.Count,
                AverageCompletion = c.Enrollments.Any() 
                    ? c.Enrollments.Average(e => e.ProgressPercentage) 
                    : 0
            })
            .ToListAsync();

        var analytics = new AnalyticsDto
        {
            TotalStudents = totalStudents,
            TotalInstructors = totalInstructors,
            TotalCourses = totalCourses,
            TotalEnrollments = totalEnrollments,
            CompletedCourses = completedCourses,
            CategoryStats = categoryStats,
            PopularCourses = popularCourses
        };

        return Ok(analytics);
    }
}
