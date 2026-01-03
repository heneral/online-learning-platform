using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningPlatform.Models;

public class Certificate
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string CertificateNumber { get; set; } = string.Empty;
    
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    
    public string? PdfUrl { get; set; }
    
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
}
