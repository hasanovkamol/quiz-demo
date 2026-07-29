namespace QuizApi.Core.Application.Dtos;

public record GoogleLoginRequestDto(
    string IdToken,
    string? FallbackName,
    string? FallbackEmail
);

public record RefreshTokenRequestDto(
    string RefreshToken,
    string? UserId
);

public record AuthResponseDto(
    string Token,
    string RefreshToken,
    int ExpiresInSeconds,
    Guid UserId,
    string Email,
    string Name,
    string? PictureUrl,
    string Role,
    IReadOnlyList<string> Permissions
);

public record AiQuizGenerationRequest(
    string Topic,
    string Category,
    string Difficulty,
    int QuestionCount,
    int TimeLimitMinutes,
    string? ApiKey
);

public record AiSingleQuestionRequest(
    string Topic,
    string? Category,
    string Difficulty,
    string? ApiKey
);

public record CategoryDto(
    string Id,
    string Name,
    string IconName,
    string Description
);

public record ImportMarkdownQuizRequestDto(
    string MarkdownText,
    string? Title,
    string? Category,
    string? CategoryName,
    string? Difficulty
);


