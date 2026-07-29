using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using QuizApi.Core.Application.Dtos;
using QuizApi.Core.Application.Interfaces;
using QuizApi.Core.Domain.Entities;
using QuizApi.Infrastructure.Persistence;

namespace QuizApi.Endpoints;

public static class AdminEndpoints
{
    private static readonly List<CategoryDto> CategoriesStore =
    [
        new("angular", "Angular Framework", "code-2", "Angular Signals va RxJS texnologiyalari"),
        new("dotnet", "C# & .NET Core", "terminal", "C# 13, EF Core va ASP.NET Core"),
        new("webdev", "Web Infrastructure", "globe", "Web Xavfsizlik va Docker Infratuzilmasi"),
        new("custom", "Maxsus Testlar", "sparkles", "Admin tomonidan yaratilgan maxsus testlar")
    ];

    public static string GetCategoryNameById(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return "Maxsus Markdown Test";
        lock (CategoriesStore)
        {
            var cat = CategoriesStore.FirstOrDefault(c => string.Equals(c.Id, id, StringComparison.OrdinalIgnoreCase));
            return cat?.Name ?? id;
        }
    }

    public static RouteGroupBuilder MapAdminEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/admin")
            .WithTags("Admin");

        // 1. Full AI Quiz Generation
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

        // 2. Single AI Question Generation (Bitta-bitta AI savol yaratish)
        group.MapPost("/generate-ai-question", async Task<Results<Ok<Question>, BadRequest<object>, ProblemHttpResult>> (
            AiSingleQuestionRequest request,
            ISemanticKernelQuizService aiQuizService) =>
        {
            if (string.IsNullOrWhiteSpace(request.Topic))
            {
                return TypedResults.BadRequest<object>(new { message = "Iltimos, mavzu nomini kiriting!" });
            }

            try
            {
                var question = await aiQuizService.GenerateSingleQuestionAsync(request);
                return TypedResults.Ok(question);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("AI Single Question Generation error: " + ex.Message);
            }
        })
        .WithSummary("AI yordamida bitta test savoli va 4 ta javob variantini shakllantirish");

        // 3. Category Endpoints (GET & POST)
        group.MapGet("/categories", () => TypedResults.Ok(CategoriesStore))
        .WithSummary("Barcha kategoriyalar ro'yxatini olish");

        group.MapPost("/categories", IResult (CategoryDto category) =>
        {
            if (string.IsNullOrWhiteSpace(category.Id) || string.IsNullOrWhiteSpace(category.Name))
            {
                return TypedResults.BadRequest(new { message = "Kategoriya ID va Nomi kiritilishi shart!" });
            }

            lock (CategoriesStore)
            {
                var existing = CategoriesStore.FirstOrDefault(c => c.Id.Equals(category.Id, StringComparison.OrdinalIgnoreCase));
                if (existing != null)
                {
                    CategoriesStore.Remove(existing);
                }
                CategoriesStore.Add(category);
            }

            return TypedResults.Created($"/api/admin/categories/{category.Id}", category);
        })
        .WithSummary("Yangi Kategoriya yaratish va ro'yxatga qo'shish");

        // 4. Insert Single Question directly to a Quiz in DB
        group.MapPost("/quizzes/{quizId:guid}/questions", async Task<Results<Created<Question>, NotFound<object>>> (
            Guid quizId,
            Question question,
            QuizDbContext dbContext) =>
        {
            var quiz = await dbContext.Quizzes.Include(q => q.Questions).FirstOrDefaultAsync(q => q.Id == quizId);
            if (quiz == null)
            {
                return TypedResults.NotFound<object>(new { message = "Test topilmadi" });
            }

            question.Id = Guid.NewGuid();
            question.QuizId = quizId;
            foreach (var opt in question.Options)
            {
                opt.Id = Guid.NewGuid();
                opt.QuestionId = question.Id;
            }

            dbContext.Questions.Add(question);
            await dbContext.SaveChangesAsync();

            return TypedResults.Created($"/api/quizzes/{quizId}/questions/{question.Id}", question);
        })
        .WithSummary("Testga bitta savolni to'g'ridan-to'g'ri qo'shish (1-Click Insert)");

        // 5. Parse Markdown Preview
        group.MapPost("/parse-markdown-preview", Results<Ok<Quiz>, BadRequest<object>> (
            ImportMarkdownQuizRequestDto request,
            IMarkdownQuizParserService parserService) =>
        {
            if (string.IsNullOrWhiteSpace(request.MarkdownText))
            {
                return TypedResults.BadRequest<object>(new { message = "Iltimos, Markdown matnini kiriting!" });
            }

            try
            {
                var quiz = parserService.ParseMarkdownToQuiz(request.MarkdownText, request.Title, request.Category, request.CategoryName, request.Difficulty);
                return TypedResults.Ok(quiz);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest<object>(new { message = "Markdown parsing xatosi: " + ex.Message });
            }
        })
        .WithSummary("Markdown textini parse qilib, saqlashdan oldin prevyu olish");

        // 6. Import Markdown Quiz to Database (Optimized Bulk Insert)
        group.MapPost("/import-markdown", async Task<Results<Created<Quiz>, BadRequest<object>>> (
            ImportMarkdownQuizRequestDto request,
            IMarkdownQuizParserService parserService,
            QuizDbContext dbContext) =>
        {
            if (string.IsNullOrWhiteSpace(request.MarkdownText))
            {
                return TypedResults.BadRequest<object>(new { message = "Iltimos, Markdown matnini kiriting!" });
            }

            try
            {
                var quiz = parserService.ParseMarkdownToQuiz(request.MarkdownText, request.Title, request.Category, request.CategoryName, request.Difficulty);

                if (quiz.Questions.Count == 0)
                {
                    return TypedResults.BadRequest<object>(new { message = "Markdown matnidan birorta ham test savoli aniqlanmadi. Formatni tekshiring!" });
                }

                dbContext.Quizzes.Add(quiz);
                await dbContext.SaveChangesAsync();

                return TypedResults.Created($"/api/quizzes/{quiz.Id}", quiz);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest<object>(new { message = "Markdown import xatosi: " + ex.Message });
            }
        })
        .WithSummary("Markdown fayl/matnidan test va savollarni ma'lumotlar bazasiga optimallashtirilgan holatda saqlash (Bulk Insert)");


        // 5. Seed 720 questions
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

        // 6. Stats
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
