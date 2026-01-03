using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningPlatform.Models;

public class CourseResource
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string FileUrl { get; set; } = string.Empty;
    
    public string FileType { get; set; } = string.Empty; // PDF, ZIP, etc.
    
    public long FileSizeInBytes { get; set; }
    
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign Key
    [Required]
    public int CourseId { get; set; }
    
    // Navigation properties
    [ForeignKey("CourseId")]
    public Course Course { get; set; } = null!;
}
