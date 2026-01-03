using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizQuestion> QuizQuestions { get; set; }
    public DbSet<QuizAnswer> QuizAnswers { get; set; }
    public DbSet<QuizResult> QuizResults { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<LessonProgress> LessonProgresses { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<CourseResource> CourseResources { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure relationships
        builder.Entity<Course>()
            .HasOne(c => c.Instructor)
            .WithMany(u => u.CoursesCreated)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Unique constraint for enrollment
        builder.Entity<Enrollment>()
            .HasIndex(e => new { e.StudentId, e.CourseId })
            .IsUnique();

        // Unique constraint for lesson progress
        builder.Entity<LessonProgress>()
            .HasIndex(lp => new { lp.EnrollmentId, lp.LessonId })
            .IsUnique();

        // Configure Certificate number as unique
        builder.Entity<Certificate>()
            .HasIndex(c => c.CertificateNumber)
            .IsUnique();

        // Seed initial data
        SeedData(builder);
    }

    private void SeedData(ModelBuilder builder)
    {
        // Seed Categories
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Programming", Description = "Software development and coding courses" },
            new Category { Id = 2, Name = "Design", Description = "Graphic design and UI/UX courses" },
            new Category { Id = 3, Name = "Business", Description = "Business management and entrepreneurship" },
            new Category { Id = 4, Name = "Marketing", Description = "Digital marketing and sales courses" },
            new Category { Id = 5, Name = "Data Science", Description = "Data analysis and machine learning" }
        );
    }
}
