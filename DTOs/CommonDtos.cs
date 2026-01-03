namespace OnlineLearningPlatform.DTOs;

public class CertificateDto
{
    public int Id { get; set; }
    public string CertificateNumber { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public string? PdfUrl { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
}

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public int CourseCount { get; set; }
}

public class CourseResourceDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSizeInBytes { get; set; }
    public DateTime UploadedAt { get; set; }
}

public class AnalyticsDto
{
    public int TotalStudents { get; set; }
    public int TotalInstructors { get; set; }
    public int TotalCourses { get; set; }
    public int TotalEnrollments { get; set; }
    public int CompletedCourses { get; set; }
    public List<CategoryStatDto> CategoryStats { get; set; } = new();
    public List<PopularCourseDto> PopularCourses { get; set; } = new();
}

public class CategoryStatDto
{
    public string CategoryName { get; set; } = string.Empty;
    public int CourseCount { get; set; }
    public int EnrollmentCount { get; set; }
}

public class PopularCourseDto
{
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public int EnrollmentCount { get; set; }
    public double AverageCompletion { get; set; }
}
