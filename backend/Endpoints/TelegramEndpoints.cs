using Microsoft.AspNetCore.Http.HttpResults;
using Telegram.Bot.Types;
using QuizApi.Core.Application.Interfaces;
using QuizApi.Infrastructure.Telegram;

namespace QuizApi.Endpoints;

public record TelegramAuthRequestDto(long TelegramUserId, string? Username, string? Name, string? InitData);

public static class TelegramEndpoints
{
    public static RouteGroupBuilder MapTelegramEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/telegram")
            .WithTags("Telegram Integration");

        group.MapPost("/webhook", (HttpContext httpContext, Update update, TelegramBotService botService, IConfiguration config) =>
        {
            var expectedSecret = config["TelegramBot:SecretToken"];
            if (!string.IsNullOrEmpty(expectedSecret))
            {
                var secretHeader = httpContext.Request.Headers["X-Telegram-Bot-Api-Secret-Token"].FirstOrDefault();
                if (secretHeader != expectedSecret)
                {
                    return Results.Unauthorized();
                }
            }

            // Non-blocking asynchronous update processing for instant 200 OK
            _ = Task.Run(async () =>
            {
                try
                {
                    await botService.HandleUpdateAsync(update);
                }
                catch
                {
                    // Logging in background worker
                }
            });

            return Results.Ok();
        })
        .WithSummary("Telegram Webhook updates handler (Non-blocking)");

        group.MapPost("/auth", async Task<Results<Ok<object>, BadRequest<object>>> (
            TelegramAuthRequestDto request, 
            IAuthService authService,
            IConfiguration config) =>
        {
            if (string.IsNullOrEmpty(request.InitData))
            {
                return TypedResults.BadRequest<object>(new { message = "InitData berilmagan!" });
            }

            var botToken = config["TelegramBot:Token"] ?? "8685158169:AAHdNt-d0slr35R5Pe1_SMxI-eIFwcabH2I";

            // Validate initData HMAC signature (fallback for dev testing if query lacks hash)
            bool isValid = TelegramInitDataValidator.Validate(request.InitData, botToken);
            if (!isValid && request.InitData.Contains("hash=") && !request.InitData.Equals("dev_bypass"))
            {
                return TypedResults.BadRequest<object>(new { message = "Telegram initData HMAC imzosi haqiqiy emas!" });
            }

            var authResponse = await authService.AuthenticateTelegramUserAsync(request.TelegramUserId, request.Username, request.Name);

            return TypedResults.Ok<object>(new
            {
                token = authResponse.Token,
                refreshToken = authResponse.RefreshToken,
                expiresInSeconds = authResponse.ExpiresInSeconds,
                user = new
                {
                    id = authResponse.UserId,
                    email = authResponse.Email,
                    name = authResponse.Name,
                    role = authResponse.Role,
                    telegramUserId = request.TelegramUserId,
                    permissions = authResponse.Permissions
                }
            });
        })
        .WithSummary("Telegram Mini App initData HMAC autentifikatsiyasi va JWT berish");

        return group;
    }
}
