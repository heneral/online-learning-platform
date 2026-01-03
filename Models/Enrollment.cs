using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningPlatform.Models;

public class Enrollment
{
    [Key]
    public int Id { get; set; }
    
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? CompletedAt { get; set; }
    
    public int ProgressPercentage { get; set; } = 0;
    
    public bool IsCompleted { get; set; }
    
    // Foreign Keys
    [Required]
    public string StudentId { get; set; } = string.Empty;
    
    [Required]
    public int CourseId { get; set; }
    
    // Navigation properties
    [ForeignKey("StudentId")]
    public ApplicationUser Student { get; set; } = null!;
    
    [ForeignKey("CourseId")]
    public Course Course { get; set; } = null!;
    
    public ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();
}
