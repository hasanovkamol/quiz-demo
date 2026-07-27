using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using QuizApi.Core.Application.Dtos;
using QuizApi.Core.Application.Interfaces;
using QuizApi.Infrastructure.Persistence;

namespace QuizApi.Endpoints;

public static class AdminEndpoints
{
    public static RouteGroupBuilder MapAdminEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/admin")
            .WithTags("Admin");

        group.MapPost("/generate-ai-quiz", async Task<Results<Ok<object>, BadRequest<object>, ProblemHttpResult>> (
            AiQuizGenerationRequest request, 
            ISemanticKernelQuizService aiQuizService) =>
        {
            if (string.IsNullOrWhiteSpace(request.Topic))
            {
                return TypedResults.BadRequest<object>(new { message = "Iltimos, mavzu nomini kiriting!" });
            }

            try
            {
                var quiz = await aiQuizService.GenerateQuizAsync(request);
                return TypedResults.Ok<object>(quiz);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("AI Generation error: " + ex.Message);
            }
        })
        .WithSummary("Semantic Kernel AI yordamida avtomatik test savollari yaratish");

        group.MapPost("/seed-720-questions", async (QuizDbContext dbContext) =>
        {
            await DbInitializer.SeedComprehensiveQuizzesAsync(dbContext);
            var totalQuizzes = await dbContext.Quizzes.CountAsync();
            var totalQuestions = await dbContext.Questions.CountAsync();

            return TypedResults.Ok(new
            {
                message = "720 ta professional test savollari ma'lumotlar bazasiga muvaffaqiyatli kiritildi!",
                totalQuizzes,
                totalQuestions
            });
        })
        .WithSummary("PDF va ekspert manbalari asosida 720 ta savolni bazaga qayta yuklash");

        group.MapGet("/stats", async (QuizDbContext dbContext) =>
        {
            var totalQuizzes = await dbContext.Quizzes.CountAsync();
            var totalQuestions = await dbContext.Questions.CountAsync();
            var totalAttempts = await dbContext.QuizAttempts.CountAsync();
            var avgScore = totalAttempts > 0 ? await dbContext.QuizAttempts.AverageAsync(a => a.ScorePercentage) : 0;
            var uniqueUsersCount = await dbContext.QuizAttempts.Select(a => a.UserName).Distinct().CountAsync();

            return TypedResults.Ok(new
            {
                totalQuizzes,
                totalQuestions,
                totalAttempts,
                avgScore = Math.Round(avgScore, 1),
                uniqueUsersCount
            });
        })
        .WithSummary("Admin statistikalarini olish");

        return group;
    }
}
