using Microsoft.AspNetCore.Identity;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Data;

public static class DbInitializer
{
    public static async Task SeedTestDataAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Seed Roles
        string[] roles = { "Admin", "Instructor", "Student" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed Admin User
        var adminEmail = "admin@learningplatform.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "System",
                LastName = "Administrator",
                DateOfBirth = new DateTime(1990, 1, 1),
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(admin, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        // Seed Instructor Users
        var instructorEmails = new[]
        {
            ("john.doe@learningplatform.com", "John", "Doe"),
            ("jane.smith@learningplatform.com", "Jane", "Smith")
        };

        foreach (var (email, firstName, lastName) in instructorEmails)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var instructor = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = new DateTime(1985, 5, 15),
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(instructor, "Instructor123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(instructor, "Instructor");
                }
            }
        }

        // Seed Student Users
        var studentEmails = new[]
        {
            ("student1@example.com", "Alice", "Johnson"),
            ("student2@example.com", "Bob", "Williams"),
            ("student3@example.com", "Charlie", "Brown")
        };

        foreach (var (email, firstName, lastName) in studentEmails)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var student = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = new DateTime(2000, 1, 1),
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(student, "Student123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(student, "Student");
                }
            }
        }

        await context.SaveChangesAsync();

        // Check if courses already exist
        if (context.Courses.Any())
        {
            return; // DB has been seeded
        }

        // Get instructors for course creation
        var instructor1 = await userManager.FindByEmailAsync("john.doe@learningplatform.com");
        var instructor2 = await userManager.FindByEmailAsync("jane.smith@learningplatform.com");

        if (instructor1 == null || instructor2 == null) return;

        // Seed Courses
        var courses = new[]
        {
            new Course
            {
                Title = "Complete ASP.NET Core Masterclass",
                Description = "Learn ASP.NET Core from beginner to advanced. Build real-world web applications with the latest .NET technologies.",
                ThumbnailUrl = "https://example.com/aspnet-core.jpg",
                Price = 99.99m,
                IsFree = false,
                Level = "Intermediate",
                Language = "English",
                DurationInMinutes = 1200,
                IsPublished = true,
                CategoryId = 1, // Programming
                InstructorId = instructor1.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new Course
            {
                Title = "Introduction to Web Design",
                Description = "Master the fundamentals of web design including HTML, CSS, and modern design principles.",
                ThumbnailUrl = "https://example.com/web-design.jpg",
                Price = 49.99m,
                IsFree = false,
                Level = "Beginner",
                Language = "English",
                DurationInMinutes = 600,
                IsPublished = true,
                CategoryId = 2, // Design
                InstructorId = instructor2.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-25)
            },
            new Course
            {
                Title = "C# Programming Fundamentals",
                Description = "Learn C# programming from scratch. Perfect for beginners who want to start their coding journey.",
                ThumbnailUrl = "https://example.com/csharp.jpg",
                Price = 0m,
                IsFree = true,
                Level = "Beginner",
                Language = "English",
                DurationInMinutes = 480,
                IsPublished = true,
                CategoryId = 1, // Programming
                InstructorId = instructor1.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new Course
            {
                Title = "Digital Marketing Essentials",
                Description = "Comprehensive guide to digital marketing, SEO, social media, and content marketing strategies.",
                ThumbnailUrl = "https://example.com/digital-marketing.jpg",
                Price = 79.99m,
                IsFree = false,
                Level = "Intermediate",
                Language = "English",
                DurationInMinutes = 900,
                IsPublished = true,
                CategoryId = 4, // Marketing
                InstructorId = instructor2.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Course
            {
                Title = "Python for Data Science",
                Description = "Learn Python programming for data analysis, visualization, and machine learning.",
                ThumbnailUrl = "https://example.com/python-ds.jpg",
                Price = 129.99m,
                IsFree = false,
                Level = "Advanced",
                Language = "English",
                DurationInMinutes = 1500,
                IsPublished = true,
                CategoryId = 5, // Data Science
                InstructorId = instructor1.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            }
        };

        context.Courses.AddRange(courses);
        await context.SaveChangesAsync();

        // Seed Lessons for first course
        var aspNetCourse = courses[0];
        var lessons = new[]
        {
            new Lesson
            {
                Title = "Introduction to ASP.NET Core",
                Description = "Overview of ASP.NET Core framework and its features",
                OrderIndex = 1,
                VideoUrl = "https://example.com/videos/lesson1.mp4",
                DurationInSeconds = 1200,
                IsFree = true,
                CourseId = aspNetCourse.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-29)
            },
            new Lesson
            {
                Title = "Setting Up Development Environment",
                Description = "Install Visual Studio and create your first project",
                OrderIndex = 2,
                VideoUrl = "https://example.com/videos/lesson2.mp4",
                DurationInSeconds = 900,
                IsFree = true,
                CourseId = aspNetCourse.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-28)
            },
            new Lesson
            {
                Title = "Understanding MVC Pattern",
                Description = "Learn about Model-View-Controller architecture",
                OrderIndex = 3,
                VideoUrl = "https://example.com/videos/lesson3.mp4",
                DurationInSeconds = 1500,
                IsFree = false,
                CourseId = aspNetCourse.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-27)
            },
            new Lesson
            {
                Title = "Entity Framework Core Basics",
                Description = "Database access with EF Core",
                OrderIndex = 4,
                VideoUrl = "https://example.com/videos/lesson4.mp4",
                DurationInSeconds = 1800,
                IsFree = false,
                CourseId = aspNetCourse.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-26)
            }
        };

        context.Lessons.AddRange(lessons);
        await context.SaveChangesAsync();

        // Seed a Quiz
        var quiz = new Quiz
        {
            Title = "ASP.NET Core Fundamentals Quiz",
            Description = "Test your knowledge of ASP.NET Core basics",
            CourseId = aspNetCourse.Id,
            PassingScore = 70,
            TimeLimit = 30,
            CreatedAt = DateTime.UtcNow.AddDays(-25)
        };

        context.Quizzes.Add(quiz);
        await context.SaveChangesAsync();

        // Seed Quiz Questions
        var question1 = new QuizQuestion
        {
            QuizId = quiz.Id,
            QuestionText = "What is ASP.NET Core?",
            QuestionType = "MultipleChoice",
            Points = 1,
            OrderIndex = 1
        };

        var question2 = new QuizQuestion
        {
            QuizId = quiz.Id,
            QuestionText = "ASP.NET Core is cross-platform",
            QuestionType = "TrueFalse",
            Points = 1,
            OrderIndex = 2
        };

        context.QuizQuestions.AddRange(question1, question2);
        await context.SaveChangesAsync();

        // Seed Quiz Answers
        var answers = new[]
        {
            new QuizAnswer { QuestionId = question1.Id, AnswerText = "A web framework", IsCorrect = true },
            new QuizAnswer { QuestionId = question1.Id, AnswerText = "A database", IsCorrect = false },
            new QuizAnswer { QuestionId = question1.Id, AnswerText = "An operating system", IsCorrect = false },
            new QuizAnswer { QuestionId = question1.Id, AnswerText = "A programming language", IsCorrect = false },
            new QuizAnswer { QuestionId = question2.Id, AnswerText = "True", IsCorrect = true },
            new QuizAnswer { QuestionId = question2.Id, AnswerText = "False", IsCorrect = false }
        };

        context.QuizAnswers.AddRange(answers);
        await context.SaveChangesAsync();

        Console.WriteLine("✅ Test data seeded successfully!");
    }
}
