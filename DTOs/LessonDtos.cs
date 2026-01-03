using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.DTOs;

public class LessonCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public int CourseId { get; set; }

    public int OrderIndex { get; set; }

    public string? VideoUrl { get; set; }

    public int DurationInSeconds { get; set; }

    public bool IsFree { get; set; }
}

public class LessonUpdateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? OrderIndex { get; set; }
    public string? VideoUrl { get; set; }
    public int? DurationInSeconds { get; set; }
    public bool? IsFree { get; set; }
}

public class LessonDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
    public string? VideoUrl { get; set; }
    public int DurationInSeconds { get; set; }
    public bool IsFree { get; set; }
    public int CourseId { get; set; }
    public DateTime CreatedAt { get; set; }
}
