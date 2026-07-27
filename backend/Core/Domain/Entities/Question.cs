namespace QuizApi.Core.Domain.Entities;

public class Question
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? CodeSnippet { get; set; }
    public string CorrectOptionId { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
    public bool? IsCodeQuestion { get; set; }
    public string? InitialCodeTemplate { get; set; }
    public string? ExpectedOutput { get; set; }

    public List<QuestionOption> Options { get; set; } = [];
}
