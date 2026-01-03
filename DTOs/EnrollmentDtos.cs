namespace OnlineLearningPlatform.DTOs;

public class EnrollmentDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string? CourseThumbnailUrl { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public DateTime EnrolledAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int ProgressPercentage { get; set; }
    public bool IsCompleted { get; set; }
}

public class EnrollCourseDto
{
    public int CourseId { get; set; }
}

public class ProgressDto
{
    public int EnrollmentId { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public int ProgressPercentage { get; set; }
    public int CompletedLessons { get; set; }
    public int TotalLessons { get; set; }
    public int CompletedQuizzes { get; set; }
    public int TotalQuizzes { get; set; }
    public bool IsCompleted { get; set; }
}

public class LessonProgressDto
{
    public int LessonId { get; set; }
    public string LessonTitle { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public int WatchTimeInSeconds { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime LastWatchedAt { get; set; }
}

public class UpdateLessonProgressDto
{
    public int EnrollmentId { get; set; }
    public int LessonId { get; set; }
    public int WatchTimeInSeconds { get; set; }
    public bool IsCompleted { get; set; }
}
