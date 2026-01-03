using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningPlatform.Models;

public class Quiz
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public int PassingScore { get; set; } = 70;
    
    public int TimeLimit { get; set; } // In minutes, 0 = no limit
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign Key
    [Required]
    public int CourseId { get; set; }
    
    // Navigation properties
    [ForeignKey("CourseId")]
    public Course Course { get; set; } = null!;
    
    public ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
    public ICollection<QuizResult> Results { get; set; } = new List<QuizResult>();
}
