using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.DTOs;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Services;

public interface IQuizService
{
    Task<QuizDetailDto?> GetQuizByIdAsync(int id);
    Task<QuizDto> CreateQuizAsync(QuizCreateDto dto, string instructorId);
    Task<QuizResultDto> SubmitQuizAsync(QuizSubmissionDto dto, string studentId);
    Task<List<QuizResultDto>> GetStudentQuizResultsAsync(string studentId, int courseId);
}

public class QuizService : IQuizService
{
    private readonly ApplicationDbContext _context;

    public QuizService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<QuizDetailDto?> GetQuizByIdAsync(int id)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quiz == null) return null;

        return new QuizDetailDto
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            PassingScore = quiz.PassingScore,
            TimeLimit = quiz.TimeLimit,
            CourseId = quiz.CourseId,
            QuestionCount = quiz.Questions.Count,
            CreatedAt = quiz.CreatedAt,
            Questions = quiz.Questions.OrderBy(q => q.OrderIndex).Select(q => new QuizQuestionDto
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType,
                Points = q.Points,
                OrderIndex = q.OrderIndex,
                Answers = q.Answers.Select(a => new QuizAnswerDto
                {
                    Id = a.Id,
                    AnswerText = a.AnswerText
                    // IsCorrect is intentionally NOT included for students
                }).ToList()
            }).ToList()
        };
    }

    public async Task<QuizDto> CreateQuizAsync(QuizCreateDto dto, string instructorId)
    {
        // Verify the instructor owns the course
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == dto.CourseId && c.InstructorId == instructorId);

        if (course == null)
            throw new UnauthorizedAccessException("You don't have permission to add quizzes to this course");

        var quiz = new Quiz
        {
            Title = dto.Title,
            Description = dto.Description,
            CourseId = dto.CourseId,
            PassingScore = dto.PassingScore,
            TimeLimit = dto.TimeLimit,
            CreatedAt = DateTime.UtcNow
        };

        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();

        // Add questions and answers
        foreach (var questionDto in dto.Questions)
        {
            var question = new QuizQuestion
            {
                QuizId = quiz.Id,
                QuestionText = questionDto.QuestionText,
                QuestionType = questionDto.QuestionType,
                Points = questionDto.Points,
                OrderIndex = questionDto.OrderIndex
            };

            _context.QuizQuestions.Add(question);
            await _context.SaveChangesAsync();

            foreach (var answerDto in questionDto.Answers)
            {
                var answer = new QuizAnswer
                {
                    QuestionId = question.Id,
                    AnswerText = answerDto.AnswerText,
                    IsCorrect = answerDto.IsCorrect
                };

                _context.QuizAnswers.Add(answer);
            }
        }

        await _context.SaveChangesAsync();

        return new QuizDto
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            PassingScore = quiz.PassingScore,
            TimeLimit = quiz.TimeLimit,
            CourseId = quiz.CourseId,
            QuestionCount = dto.Questions.Count,
            CreatedAt = quiz.CreatedAt
        };
    }

    public async Task<QuizResultDto> SubmitQuizAsync(QuizSubmissionDto dto, string studentId)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == dto.QuizId);

        if (quiz == null)
            throw new ArgumentException("Quiz not found");

        // Calculate score
        int totalScore = 0;
        int totalPoints = quiz.Questions.Sum(q => q.Points);

        foreach (var answer in dto.Answers)
        {
            var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
            if (question == null) continue;

            var correctAnswerIds = question.Answers
                .Where(a => a.IsCorrect)
                .Select(a => a.Id)
                .OrderBy(id => id)
                .ToList();

            var submittedAnswerIds = answer.SelectedAnswerIds.OrderBy(id => id).ToList();

            // Check if the submitted answers match the correct answers exactly
            if (correctAnswerIds.SequenceEqual(submittedAnswerIds))
            {
                totalScore += question.Points;
            }
        }

        bool passed = totalPoints > 0 && (totalScore * 100.0 / totalPoints) >= quiz.PassingScore;

        var quizResult = new QuizResult
        {
            QuizId = dto.QuizId,
            StudentId = studentId,
            Score = totalScore,
            TotalPoints = totalPoints,
            Passed = passed,
            CompletedAt = DateTime.UtcNow,
            TimeSpentInSeconds = dto.TimeSpentInSeconds
        };

        _context.QuizResults.Add(quizResult);
        await _context.SaveChangesAsync();

        return new QuizResultDto
        {
            Id = quizResult.Id,
            Score = quizResult.Score,
            TotalPoints = quizResult.TotalPoints,
            Percentage = totalPoints > 0 ? (quizResult.Score * 100.0 / quizResult.TotalPoints) : 0,
            Passed = quizResult.Passed,
            CompletedAt = quizResult.CompletedAt,
            TimeSpentInSeconds = quizResult.TimeSpentInSeconds,
            QuizTitle = quiz.Title
        };
    }

    public async Task<List<QuizResultDto>> GetStudentQuizResultsAsync(string studentId, int courseId)
    {
        return await _context.QuizResults
            .Include(qr => qr.Quiz)
            .Where(qr => qr.StudentId == studentId && qr.Quiz.CourseId == courseId)
            .Select(qr => new QuizResultDto
            {
                Id = qr.Id,
                Score = qr.Score,
                TotalPoints = qr.TotalPoints,
                Percentage = qr.TotalPoints > 0 ? (qr.Score * 100.0 / qr.TotalPoints) : 0,
                Passed = qr.Passed,
                CompletedAt = qr.CompletedAt,
                TimeSpentInSeconds = qr.TimeSpentInSeconds,
                QuizTitle = qr.Quiz.Title
            })
            .ToListAsync();
    }
}
