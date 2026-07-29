using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using QuizApi.Core.Application.Interfaces;
using QuizApi.Core.Domain.Constants;
using QuizApi.Core.Domain.Entities;
using QuizApi.Infrastructure.Persistence;

namespace QuizApi.Endpoints;

public static class QuizEndpoints
{
    public static RouteGroupBuilder MapQuizEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/quizzes")
            .WithTags("Quizzes");

        group.MapGet("/", async (QuizDbContext dbContext) =>
        {
            var quizzes = await dbContext.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();

            return TypedResults.Ok(quizzes);
        })
        .WithSummary("Barcha testlar ro'yxatini olish");

        group.MapGet("/{id:guid}", async Task<Results<Ok<Quiz>, NotFound<object>>> (Guid id, QuizDbContext dbContext) =>
        {
            var quiz = await dbContext.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null) return TypedResults.NotFound<object>(new { message = "Test topilmadi" });

            return TypedResults.Ok(quiz);
        })
        .WithSummary("ID bo'yicha testni olish");

        group.MapPost("/", async (Quiz quiz, QuizDbContext dbContext) =>
        {
            quiz.Id = Guid.NewGuid();
            quiz.CreatedAt = DateTime.UtcNow;

            foreach (var q in quiz.Questions)
            {
                q.Id = Guid.NewGuid();
                q.QuizId = quiz.Id;
                foreach (var opt in q.Options)
                {
                    opt.Id = Guid.NewGuid();
                    opt.QuestionId = q.Id;
                }
            }

            dbContext.Quizzes.Add(quiz);
            await dbContext.SaveChangesAsync();

            return TypedResults.Created($"/api/quizzes/{quiz.Id}", quiz);
        })
        .WithSummary("Yangi test yaratish");

        group.MapDelete("/{id:guid}", async Task<Results<NoContent, NotFound>> (Guid id, QuizDbContext dbContext) =>
        {
            var quiz = await dbContext.Quizzes.FindAsync(id);
            if (quiz == null) return TypedResults.NotFound();

            dbContext.Quizzes.Remove(quiz);
            await dbContext.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithSummary("Testni o'chirish");

        group.MapPost("/explain-question", async Task<Results<Ok<object>, BadRequest<object>>> (
            QuizApi.Core.Application.Dtos.AiQuestionExplainRequest request,
            ISemanticKernelQuizService aiQuizService) =>
        {
            if (string.IsNullOrWhiteSpace(request.QuestionText))
            {
                return TypedResults.BadRequest<object>(new { message = "Iltimos, tushuntirilishi kerak bo'lgan savol matnini yuboring!" });
            }

            var explanation = await aiQuizService.ExplainQuestionAsync(request);
            return TypedResults.Ok<object>(new { explanation });
        })
        .WithSummary("Test yechish jarayonida savol bo'yicha AI dan batafsil tushuntirish va yordam olish");

        return group;
    }
}
