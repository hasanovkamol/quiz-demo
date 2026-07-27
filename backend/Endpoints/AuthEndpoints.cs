using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

using QuizApi.Core.Application.Dtos;
using QuizApi.Core.Application.Interfaces;
using QuizApi.Core.Domain.Entities;

namespace QuizApi.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/auth")
            .WithTags("Auth");

        group.MapPost("/google-login", async (GoogleLoginRequestDto request, IAuthService authService) =>
        {
            var response = await authService.AuthenticateGoogleUserAsync(request);
            return TypedResults.Ok(response);
        })
        .WithSummary("Google OAuth 2.0 ID Token orqali autentifikatsiya");

        group.MapPost("/refresh", async Task<Results<Ok<AuthResponseDto>, UnauthorizedHttpResult>> (
            RefreshTokenRequestDto request, 
            IAuthService authService) =>
        {
            var response = await authService.RefreshTokenAsync(request);
            if (response == null) return TypedResults.Unauthorized();

            return TypedResults.Ok(response);
        })
        .WithSummary("5 daqiqalik Access Token ni Refresh Token orqali yangilash");

        group.MapGet("/me", async Task<Results<Ok<User>, UnauthorizedHttpResult, NotFound>> (
            ClaimsPrincipal userClaims, 
            IAuthService authService) =>
        {
            var userIdClaim = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
            {
                return TypedResults.Unauthorized();
            }

            var user = await authService.GetUserByIdAsync(userId);
            if (user == null) return TypedResults.NotFound();

            return TypedResults.Ok(user);
        })
        .RequireAuthorization()
        .WithSummary("Joriy autentifikatsiyadan o'tgan foydalanuvchi profilini olish");

        return group;
    }
}
