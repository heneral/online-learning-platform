using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningPlatform.Models;

public class QuizQuestion
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string QuestionText { get; set; } = string.Empty;
    
    public string QuestionType { get; set; } = "MultipleChoice"; // MultipleChoice, TrueFalse, ShortAnswer
    
    public int Points { get; set; } = 1;
    
    public int OrderIndex { get; set; }
    
    // Foreign Key
    [Required]
    public int QuizId { get; set; }
    
    // Navigation properties
    [ForeignKey("QuizId")]
    public Quiz Quiz { get; set; } = null!;
    
    public ICollection<QuizAnswer> Answers { get; set; } = new List<QuizAnswer>();
}
