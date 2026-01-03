using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.DTOs;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Services;

public interface IEnrollmentService
{
    Task<EnrollmentDto> EnrollInCourseAsync(int courseId, string studentId);
    Task<List<EnrollmentDto>> GetStudentEnrollmentsAsync(string studentId);
    Task<ProgressDto?> GetCourseProgressAsync(int courseId, string studentId);
    Task<bool> UpdateLessonProgressAsync(UpdateLessonProgressDto dto, string studentId);
}

public class EnrollmentService : IEnrollmentService
{
    private readonly ApplicationDbContext _context;

    public EnrollmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EnrollmentDto> EnrollInCourseAsync(int courseId, string studentId)
    {
        // Check if already enrolled
        var existingEnrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId);

        if (existingEnrollment != null)
            throw new InvalidOperationException("Already enrolled in this course");

        var course = await _context.Courses
            .Include(c => c.Instructor)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
            throw new ArgumentException("Course not found");

        var enrollment = new Enrollment
        {
            CourseId = courseId,
            StudentId = studentId,
            EnrolledAt = DateTime.UtcNow,
            ProgressPercentage = 0,
            IsCompleted = false
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        var student = await _context.Users.FindAsync(studentId);

        return new EnrollmentDto
        {
            Id = enrollment.Id,
            CourseId = course.Id,
            CourseTitle = course.Title,
            CourseThumbnailUrl = course.ThumbnailUrl,
            StudentName = $"{student?.FirstName} {student?.LastName}",
            EnrolledAt = enrollment.EnrolledAt,
            CompletedAt = enrollment.CompletedAt,
            ProgressPercentage = enrollment.ProgressPercentage,
            IsCompleted = enrollment.IsCompleted
        };
    }

    public async Task<List<EnrollmentDto>> GetStudentEnrollmentsAsync(string studentId)
    {
        return await _context.Enrollments
            .Include(e => e.Course)
            .Include(e => e.Student)
            .Where(e => e.StudentId == studentId)
            .Select(e => new EnrollmentDto
            {
                Id = e.Id,
                CourseId = e.Course.Id,
                CourseTitle = e.Course.Title,
                CourseThumbnailUrl = e.Course.ThumbnailUrl,
                StudentName = $"{e.Student.FirstName} {e.Student.LastName}",
                EnrolledAt = e.EnrolledAt,
                CompletedAt = e.CompletedAt,
                ProgressPercentage = e.ProgressPercentage,
                IsCompleted = e.IsCompleted
            })
            .ToListAsync();
    }

    public async Task<ProgressDto?> GetCourseProgressAsync(int courseId, string studentId)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Course)
                .ThenInclude(c => c.Lessons)
            .Include(e => e.LessonProgresses)
            .FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId);

        if (enrollment == null) return null;

        var totalLessons = enrollment.Course.Lessons.Count;
        var completedLessons = enrollment.LessonProgresses.Count(lp => lp.IsCompleted);

        var quizResults = await _context.QuizResults
            .Include(qr => qr.Quiz)
            .Where(qr => qr.StudentId == studentId && qr.Quiz.CourseId == courseId)
            .ToListAsync();

        var totalQuizzes = await _context.Quizzes
            .Where(q => q.CourseId == courseId)
            .CountAsync();

        var completedQuizzes = quizResults.DistinctBy(qr => qr.QuizId).Count();

        return new ProgressDto
        {
            EnrollmentId = enrollment.Id,
            CourseId = courseId,
            CourseTitle = enrollment.Course.Title,
            ProgressPercentage = enrollment.ProgressPercentage,
            CompletedLessons = completedLessons,
            TotalLessons = totalLessons,
            CompletedQuizzes = completedQuizzes,
            TotalQuizzes = totalQuizzes,
            IsCompleted = enrollment.IsCompleted
        };
    }

    public async Task<bool> UpdateLessonProgressAsync(UpdateLessonProgressDto dto, string studentId)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Course)
                .ThenInclude(c => c.Lessons)
            .Include(e => e.LessonProgresses)
            .FirstOrDefaultAsync(e => e.Id == dto.EnrollmentId && e.StudentId == studentId);

        if (enrollment == null) return false;

        var lessonProgress = await _context.LessonProgresses
            .FirstOrDefaultAsync(lp => lp.EnrollmentId == dto.EnrollmentId && lp.LessonId == dto.LessonId);

        if (lessonProgress == null)
        {
            lessonProgress = new LessonProgress
            {
                EnrollmentId = dto.EnrollmentId,
                LessonId = dto.LessonId,
                WatchTimeInSeconds = dto.WatchTimeInSeconds,
                IsCompleted = dto.IsCompleted,
                LastWatchedAt = DateTime.UtcNow,
                CompletedAt = dto.IsCompleted ? DateTime.UtcNow : null
            };
            _context.LessonProgresses.Add(lessonProgress);
        }
        else
        {
            lessonProgress.WatchTimeInSeconds = dto.WatchTimeInSeconds;
            lessonProgress.LastWatchedAt = DateTime.UtcNow;

            if (dto.IsCompleted && !lessonProgress.IsCompleted)
            {
                lessonProgress.IsCompleted = true;
                lessonProgress.CompletedAt = DateTime.UtcNow;
            }
        }

        await _context.SaveChangesAsync();

        // Update enrollment progress percentage
        var totalLessons = enrollment.Course.Lessons.Count;
        var completedLessons = enrollment.LessonProgresses.Count(lp => lp.IsCompleted);

        if (totalLessons > 0)
        {
            enrollment.ProgressPercentage = (int)((completedLessons * 100.0) / totalLessons);

            if (completedLessons == totalLessons)
            {
                enrollment.IsCompleted = true;
                enrollment.CompletedAt = DateTime.UtcNow;
            }
        }

        await _context.SaveChangesAsync();

        return true;
    }
}
