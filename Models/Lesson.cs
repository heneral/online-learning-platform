using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningPlatform.Models;

public class Lesson
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public int OrderIndex { get; set; }
    
    public string? VideoUrl { get; set; }
    
    public int DurationInSeconds { get; set; }
    
    public bool IsFree { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign Key
    [Required]
    public int CourseId { get; set; }
    
    // Navigation properties
    [ForeignKey("CourseId")]
    public Course Course { get; set; } = null!;
    
    public ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();
}
