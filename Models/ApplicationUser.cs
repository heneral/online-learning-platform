using Microsoft.AspNetCore.Identity;

namespace OnlineLearningPlatform.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
    
    // Navigation properties
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<Course> CoursesCreated { get; set; } = new List<Course>();
    public ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
    public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
}
