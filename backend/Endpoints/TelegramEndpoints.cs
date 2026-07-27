using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using QuizApi.Infrastructure.Persistence;
using QuizApi.Infrastructure.Telegram;
using UserEntity = QuizApi.Core.Domain.Entities.User;

namespace QuizApi.Endpoints;

public record TelegramAuthRequestDto(long TelegramUserId, string? Username, string? Name, string? InitData);

public static class TelegramEndpoints
{
    public static RouteGroupBuilder MapTelegramEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/telegram")
            .WithTags("Telegram Integration");

        group.MapPost("/webhook", async (Update update, TelegramBotService botService) =>
        {
            await botService.HandleUpdateAsync(update);
            return TypedResults.Ok();
        })
        .WithSummary("Telegram Webhook updates handler");

        group.MapPost("/auth", async Task<Results<Ok<object>, BadRequest<object>>> (
            TelegramAuthRequestDto request, 
            QuizDbContext dbContext) =>
        {
            if (string.IsNullOrEmpty(request.InitData))
            {
                return TypedResults.BadRequest<object>(new { message = "InitData berilmagan!" });
            }

            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.TelegramUserId == request.TelegramUserId);
            if (user == null)
            {
                user = new UserEntity
                {
                    TelegramUserId = request.TelegramUserId,
                    TelegramUsername = request.Username,
                    Name = string.IsNullOrWhiteSpace(request.Name) ? "Telegram User" : request.Name,
                    Email = $"{request.TelegramUserId}@telegram.user",
                    Role = "User"
                };
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

            return TypedResults.Ok<object>(new
            {
                token = "demo_telegram_jwt_token",
                user = new
                {
                    user.Id,
                    user.Email,
                    user.Name,
                    user.Role,
                    user.TelegramUserId
                }
            });
        })
        .WithSummary("Telegram Mini App initData avtomatik autentifikatsiyasi");

        return group;
    }
}
