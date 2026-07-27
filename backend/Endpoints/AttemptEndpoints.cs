using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using QuizApi.Core.Domain.Entities;
using QuizApi.Infrastructure.Persistence;

namespace QuizApi.Endpoints;

public static class AttemptEndpoints
{
    public static RouteGroupBuilder MapAttemptEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/quizattempts")
            .WithTags("Quiz Attempts");

        group.MapGet("/", async (QuizDbContext dbContext) =>
        {
            var attempts = await dbContext.QuizAttempts
                .Include(a => a.UserAnswers)
                .OrderByDescending(a => a.CompletedAt)
                .ToListAsync();

            return TypedResults.Ok(attempts);
        })
        .WithSummary("Barcha topshirilgan testlar natijalarini olish");

        group.MapGet("/{id:guid}", async Task<Results<Ok<QuizAttempt>, NotFound<object>>> (Guid id, QuizDbContext dbContext) =>
        {
            var attempt = await dbContext.QuizAttempts
                .Include(a => a.UserAnswers)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (attempt == null) return TypedResults.NotFound<object>(new { message = "Natija topilmadi" });

            return TypedResults.Ok(attempt);
        })
        .WithSummary("ID bo'yicha test natijasini olish (LAN share link uchun)");

        group.MapPost("/", async (QuizAttempt attempt, QuizDbContext dbContext) =>
        {
            attempt.Id = Guid.NewGuid();
            attempt.CompletedAt = DateTime.UtcNow;

            foreach (var ans in attempt.UserAnswers)
            {
                ans.Id = Guid.NewGuid();
                ans.AttemptId = attempt.Id;
            }

            dbContext.QuizAttempts.Add(attempt);
            await dbContext.SaveChangesAsync();

            return TypedResults.Created($"/api/quizattempts/{attempt.Id}", attempt);
        })
        .WithSummary("Test natijasini topshirish va saqlash");

        return group;
    }
}
