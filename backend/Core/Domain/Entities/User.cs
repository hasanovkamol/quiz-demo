namespace QuizApi.Core.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? GoogleId { get; set; }

    public required string Email { get; set; }

    public required string Name { get; set; }

    public string? PictureUrl { get; set; }

    public string Role { get; set; } = "User";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;
}
