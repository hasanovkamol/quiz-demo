using System.Text.Json.Serialization;

namespace QuizApi.Core.Domain.Entities;

public class UserAnswer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AttemptId { get; set; }

    [JsonIgnore]
    public QuizAttempt Attempt { get; set; } = null!;

    public Guid QuestionId { get; set; }

    public string? SelectedOptionId { get; set; }

    public bool IsCorrect { get; set; }

    public int TimeSpentSeconds { get; set; }
}
