using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.DTOs;

public class CourseCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public string? ThumbnailUrl { get; set; }

    public decimal Price { get; set; }

    public bool IsFree { get; set; }

    [Required]
    public string Level { get; set; } = "Beginner";

    public string Language { get; set; } = "English";

    [Required]
    public int CategoryId { get; set; }
}

public class CourseUpdateDto
{
    [MaxLength(200)]
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public decimal? Price { get; set; }

    public bool? IsFree { get; set; }

    public string? Level { get; set; }

    public string? Language { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsPublished { get; set; }
}

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
    public string Level { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public string InstructorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public int EnrollmentCount { get; set; }
    public int LessonCount { get; set; }
}

public class CourseDetailDto : CourseDto
{
    public List<LessonDto> Lessons { get; set; } = new();
    public List<QuizDto> Quizzes { get; set; } = new();
    public List<CourseResourceDto> Resources { get; set; } = new();
}
