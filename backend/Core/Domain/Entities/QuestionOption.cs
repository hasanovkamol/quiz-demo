using System.Text.Json.Serialization;

namespace QuizApi.Core.Domain.Entities;

public class QuestionOption
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid QuestionId { get; set; }

    [JsonIgnore]
    public Question Question { get; set; } = null!;

    public required string Text { get; set; }
}
