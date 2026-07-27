namespace QuizApi.Core.Domain.Entities;

public class Quiz
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Title { get; set; }

    public required string Category { get; set; }

    public required string CategoryName { get; set; }

    public required string Description { get; set; }

    public string IconName { get; set; } = "code-2";

    public string Difficulty { get; set; } = "O'rta";

    public int TimeLimitSeconds { get; set; } = 300;

    public bool IsCustom { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Question> Questions { get; set; } = [];
}
