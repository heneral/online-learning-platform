using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.DTOs;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Services;

public interface ICourseService
{
    Task<List<CourseDto>> GetAllCoursesAsync();
    Task<List<CourseDto>> GetCoursesByCategoryAsync(int categoryId);
    Task<CourseDetailDto?> GetCourseByIdAsync(int id);
    Task<CourseDto> CreateCourseAsync(CourseCreateDto dto, string instructorId);
    Task<CourseDto?> UpdateCourseAsync(int id, CourseUpdateDto dto, string instructorId);
    Task<bool> DeleteCourseAsync(int id, string instructorId);
    Task<List<CourseDto>> GetInstructorCoursesAsync(string instructorId);
}

public class CourseService : ICourseService
{
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CourseDto>> GetAllCoursesAsync()
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Category)
            .Include(c => c.Lessons)
            .Include(c => c.Enrollments)
            .Where(c => c.IsPublished)
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ThumbnailUrl = c.ThumbnailUrl,
                Price = c.Price,
                IsFree = c.IsFree,
                Level = c.Level,
                Language = c.Language,
                DurationInMinutes = c.DurationInMinutes,
                IsPublished = c.IsPublished,
                CreatedAt = c.CreatedAt,
                InstructorName = $"{c.Instructor.FirstName} {c.Instructor.LastName}",
                CategoryName = c.Category.Name,
                EnrollmentCount = c.Enrollments.Count,
                LessonCount = c.Lessons.Count
            })
            .ToListAsync();
    }

    public async Task<List<CourseDto>> GetCoursesByCategoryAsync(int categoryId)
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Category)
            .Include(c => c.Lessons)
            .Include(c => c.Enrollments)
            .Where(c => c.CategoryId == categoryId && c.IsPublished)
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ThumbnailUrl = c.ThumbnailUrl,
                Price = c.Price,
                IsFree = c.IsFree,
                Level = c.Level,
                Language = c.Language,
                DurationInMinutes = c.DurationInMinutes,
                IsPublished = c.IsPublished,
                CreatedAt = c.CreatedAt,
                InstructorName = $"{c.Instructor.FirstName} {c.Instructor.LastName}",
                CategoryName = c.Category.Name,
                EnrollmentCount = c.Enrollments.Count,
                LessonCount = c.Lessons.Count
            })
            .ToListAsync();
    }

    public async Task<CourseDetailDto?> GetCourseByIdAsync(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Category)
            .Include(c => c.Lessons)
            .Include(c => c.Quizzes)
            .Include(c => c.Resources)
            .Include(c => c.Enrollments)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null) return null;

        return new CourseDetailDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            ThumbnailUrl = course.ThumbnailUrl,
            Price = course.Price,
            IsFree = course.IsFree,
            Level = course.Level,
            Language = course.Language,
            DurationInMinutes = course.DurationInMinutes,
            IsPublished = course.IsPublished,
            CreatedAt = course.CreatedAt,
            InstructorName = $"{course.Instructor.FirstName} {course.Instructor.LastName}",
            CategoryName = course.Category.Name,
            EnrollmentCount = course.Enrollments.Count,
            LessonCount = course.Lessons.Count,
            Lessons = course.Lessons.OrderBy(l => l.OrderIndex).Select(l => new LessonDto
            {
                Id = l.Id,
                Title = l.Title,
                Description = l.Description,
                OrderIndex = l.OrderIndex,
                VideoUrl = l.VideoUrl,
                DurationInSeconds = l.DurationInSeconds,
                IsFree = l.IsFree,
                CourseId = l.CourseId,
                CreatedAt = l.CreatedAt
            }).ToList(),
            Quizzes = course.Quizzes.Select(q => new QuizDto
            {
                Id = q.Id,
                Title = q.Title,
                Description = q.Description,
                PassingScore = q.PassingScore,
                TimeLimit = q.TimeLimit,
                CourseId = q.CourseId,
                QuestionCount = q.Questions.Count,
                CreatedAt = q.CreatedAt
            }).ToList(),
            Resources = course.Resources.Select(r => new CourseResourceDto
            {
                Id = r.Id,
                Title = r.Title,
                FileUrl = r.FileUrl,
                FileType = r.FileType,
                FileSizeInBytes = r.FileSizeInBytes,
                UploadedAt = r.UploadedAt
            }).ToList()
        };
    }

    public async Task<CourseDto> CreateCourseAsync(CourseCreateDto dto, string instructorId)
    {
        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            ThumbnailUrl = dto.ThumbnailUrl,
            Price = dto.Price,
            IsFree = dto.IsFree,
            Level = dto.Level,
            Language = dto.Language,
            CategoryId = dto.CategoryId,
            InstructorId = instructorId,
            IsPublished = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var instructor = await _context.Users.FindAsync(instructorId);
        var category = await _context.Categories.FindAsync(dto.CategoryId);

        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            ThumbnailUrl = course.ThumbnailUrl,
            Price = course.Price,
            IsFree = course.IsFree,
            Level = course.Level,
            Language = course.Language,
            DurationInMinutes = course.DurationInMinutes,
            IsPublished = course.IsPublished,
            CreatedAt = course.CreatedAt,
            InstructorName = $"{instructor?.FirstName} {instructor?.LastName}",
            CategoryName = category?.Name ?? "",
            EnrollmentCount = 0,
            LessonCount = 0
        };
    }

    public async Task<CourseDto?> UpdateCourseAsync(int id, CourseUpdateDto dto, string instructorId)
    {
        var course = await _context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Category)
            .Include(c => c.Lessons)
            .Include(c => c.Enrollments)
            .FirstOrDefaultAsync(c => c.Id == id && c.InstructorId == instructorId);

        if (course == null) return null;

        if (dto.Title != null) course.Title = dto.Title;
        if (dto.Description != null) course.Description = dto.Description;
        if (dto.ThumbnailUrl != null) course.ThumbnailUrl = dto.ThumbnailUrl;
        if (dto.Price.HasValue) course.Price = dto.Price.Value;
        if (dto.IsFree.HasValue) course.IsFree = dto.IsFree.Value;
        if (dto.Level != null) course.Level = dto.Level;
        if (dto.Language != null) course.Language = dto.Language;
        if (dto.CategoryId.HasValue) course.CategoryId = dto.CategoryId.Value;
        if (dto.IsPublished.HasValue) course.IsPublished = dto.IsPublished.Value;

        course.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            ThumbnailUrl = course.ThumbnailUrl,
            Price = course.Price,
            IsFree = course.IsFree,
            Level = course.Level,
            Language = course.Language,
            DurationInMinutes = course.DurationInMinutes,
            IsPublished = course.IsPublished,
            CreatedAt = course.CreatedAt,
            InstructorName = $"{course.Instructor.FirstName} {course.Instructor.LastName}",
            CategoryName = course.Category.Name,
            EnrollmentCount = course.Enrollments.Count,
            LessonCount = course.Lessons.Count
        };
    }

    public async Task<bool> DeleteCourseAsync(int id, string instructorId)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == id && c.InstructorId == instructorId);

        if (course == null) return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<CourseDto>> GetInstructorCoursesAsync(string instructorId)
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Category)
            .Include(c => c.Lessons)
            .Include(c => c.Enrollments)
            .Where(c => c.InstructorId == instructorId)
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ThumbnailUrl = c.ThumbnailUrl,
                Price = c.Price,
                IsFree = c.IsFree,
                Level = c.Level,
                Language = c.Language,
                DurationInMinutes = c.DurationInMinutes,
                IsPublished = c.IsPublished,
                CreatedAt = c.CreatedAt,
                InstructorName = $"{c.Instructor.FirstName} {c.Instructor.LastName}",
                CategoryName = c.Category.Name,
                EnrollmentCount = c.Enrollments.Count,
                LessonCount = c.Lessons.Count
            })
            .ToListAsync();
    }
}
