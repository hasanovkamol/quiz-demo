namespace QuizApi.Core.Domain.Entities;

public class QuizAttempt
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public string QuizTitle { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    public int TotalQuestions { get; set; }
    public int CorrectAnswersCount { get; set; }
    public double ScorePercentage { get; set; }
    public int TotalTimeSpentSeconds { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    public int CheatingWarningsCount { get; set; }
    public bool CheatingDetected { get; set; }

    public List<UserAnswer> UserAnswers { get; set; } = [];
}
