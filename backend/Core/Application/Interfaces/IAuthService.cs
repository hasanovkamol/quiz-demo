using QuizApi.Core.Application.Dtos;
using QuizApi.Core.Domain.Entities;

namespace QuizApi.Core.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> AuthenticateGoogleUserAsync(GoogleLoginRequestDto request);
    Task<AuthResponseDto> AuthenticateTelegramUserAsync(long telegramUserId, string? username, string? name);
    Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task<User?> GetUserByIdAsync(Guid userId);
}
