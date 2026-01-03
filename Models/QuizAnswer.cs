using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningPlatform.Models;

public class QuizAnswer
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string AnswerText { get; set; } = string.Empty;
    
    public bool IsCorrect { get; set; }
    
    // Foreign Key
    [Required]
    public int QuestionId { get; set; }
    
    // Navigation properties
    [ForeignKey("QuestionId")]
    public QuizQuestion Question { get; set; } = null!;
}
