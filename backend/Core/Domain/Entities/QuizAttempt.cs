namespace QuizApi.Core.Domain.Entities;

public class QuizAttempt
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid QuizId { get; set; }

    public required string QuizTitle { get; set; }

    public required string CategoryName { get; set; }

    public required string UserName { get; set; }

    public int TotalQuestions { get; set; }

    public int CorrectAnswersCount { get; set; }

    public double ScorePercentage { get; set; }

    public int TotalTimeSpentSeconds { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    public List<UserAnswer> UserAnswers { get; set; } = [];
}
