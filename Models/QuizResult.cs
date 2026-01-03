using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningPlatform.Models;

public class QuizResult
{
    [Key]
    public int Id { get; set; }
    
    public int Score { get; set; }
    
    public int TotalPoints { get; set; }
    
    public bool Passed { get; set; }
    
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    
    public int TimeSpentInSeconds { get; set; }
    
    // Foreign Keys
    [Required]
    public int QuizId { get; set; }
    
    [Required]
    public string StudentId { get; set; } = string.Empty;
    
    // Navigation properties
    [ForeignKey("QuizId")]
    public Quiz Quiz { get; set; } = null!;
    
    [ForeignKey("StudentId")]
    public ApplicationUser Student { get; set; } = null!;
}
