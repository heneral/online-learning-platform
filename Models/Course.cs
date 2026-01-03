using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningPlatform.Models;

public class Course
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    public string? ThumbnailUrl { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    public bool IsFree { get; set; }
    
    public string Level { get; set; } = "Beginner"; // Beginner, Intermediate, Advanced
    
    public string Language { get; set; } = "English";
    
    public int DurationInMinutes { get; set; }
    
    public bool IsPublished { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    // Foreign Keys
    [Required]
    public string InstructorId { get; set; } = string.Empty;
    
    [Required]
    public int CategoryId { get; set; }
    
    // Navigation properties
    [ForeignKey("InstructorId")]
    public ApplicationUser Instructor { get; set; } = null!;
    
    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = null!;
    
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<CourseResource> Resources { get; set; } = new List<CourseResource>();
}
