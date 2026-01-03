using System.ComponentModel.DataAnnotations;

namespace OnlineLearningPlatform.DTOs;

public class QuizCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public int CourseId { get; set; }

    public int PassingScore { get; set; } = 70;

    public int TimeLimit { get; set; }

    public List<QuizQuestionCreateDto> Questions { get; set; } = new();
}

public class QuizQuestionCreateDto
{
    [Required]
    public string QuestionText { get; set; } = string.Empty;

    public string QuestionType { get; set; } = "MultipleChoice";

    public int Points { get; set; } = 1;

    public int OrderIndex { get; set; }

    public List<QuizAnswerCreateDto> Answers { get; set; } = new();
}

public class QuizAnswerCreateDto
{
    [Required]
    public string AnswerText { get; set; } = string.Empty;

    public bool IsCorrect { get; set; }
}

public class QuizDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int PassingScore { get; set; }
    public int TimeLimit { get; set; }
    public int CourseId { get; set; }
    public int QuestionCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class QuizDetailDto : QuizDto
{
    public List<QuizQuestionDto> Questions { get; set; } = new();
}

public class QuizQuestionDto
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string QuestionType { get; set; } = string.Empty;
    public int Points { get; set; }
    public int OrderIndex { get; set; }
    public List<QuizAnswerDto> Answers { get; set; } = new();
}

public class QuizAnswerDto
{
    public int Id { get; set; }
    public string AnswerText { get; set; } = string.Empty;
    // Note: IsCorrect is NOT included for students taking quiz
}

public class QuizSubmissionDto
{
    public int QuizId { get; set; }
    public List<QuizAnswerSubmissionDto> Answers { get; set; } = new();
    public int TimeSpentInSeconds { get; set; }
}

public class QuizAnswerSubmissionDto
{
    public int QuestionId { get; set; }
    public List<int> SelectedAnswerIds { get; set; } = new();
}

public class QuizResultDto
{
    public int Id { get; set; }
    public int Score { get; set; }
    public int TotalPoints { get; set; }
    public double Percentage { get; set; }
    public bool Passed { get; set; }
    public DateTime CompletedAt { get; set; }
    public int TimeSpentInSeconds { get; set; }
    public string QuizTitle { get; set; } = string.Empty;
}
