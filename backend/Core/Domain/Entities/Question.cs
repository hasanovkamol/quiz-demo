using System.Text.Json.Serialization;

namespace QuizApi.Core.Domain.Entities;

public class Question
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid QuizId { get; set; }

    [JsonIgnore]
    public Quiz Quiz { get; set; } = null!;

    public required string Text { get; set; }

    public string? CodeSnippet { get; set; }

    public required string CorrectOptionId { get; set; }

    public required string Explanation { get; set; }

    public List<QuestionOption> Options { get; set; } = [];
}
