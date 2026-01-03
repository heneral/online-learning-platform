using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningPlatform.Models;

public class LessonProgress
{
    [Key]
    public int Id { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public int WatchTimeInSeconds { get; set; }
    
    public DateTime? CompletedAt { get; set; }
    
    public DateTime LastWatchedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign Keys
    [Required]
    public int EnrollmentId { get; set; }
    
    [Required]
    public int LessonId { get; set; }
    
    // Navigation properties
    [ForeignKey("EnrollmentId")]
    public Enrollment Enrollment { get; set; } = null!;
    
    [ForeignKey("LessonId")]
    public Lesson Lesson { get; set; } = null!;
}
