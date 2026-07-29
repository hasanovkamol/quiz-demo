using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using QuizApi.Core.Application.Interfaces;
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

        group.MapGet("/user-analytics", async (string? userName, QuizDbContext dbContext) =>
        {
            var attempts = await dbContext.QuizAttempts
                .Where(a => string.IsNullOrEmpty(userName) || a.UserName == userName)
                .ToListAsync();

            int totalTests = attempts.Count;
            double avgScore = totalTests > 0 ? Math.Round(attempts.Average(a => a.ScorePercentage), 1) : 0;
            int totalCorrect = attempts.Sum(a => a.CorrectAnswersCount);
            int totalQuestions = attempts.Sum(a => a.TotalQuestions);

            var categoryStats = attempts
                .GroupBy(a => a.CategoryName)
                .Select(g => new
                {
                    Category = g.Key,
                    AverageScore = Math.Round(g.Average(a => a.ScorePercentage), 1),
                    TotalAttempts = g.Count()
                })
                .ToList();

            var badges = new List<object>();
            if (attempts.Any(a => a.CategoryName.ToLower().Contains("dotnet") && a.ScorePercentage >= 90))
            {
                badges.Add(new { title = "🥇 C# Architect", description = "C# / .NET bo'limida 90%+ natija ko'rsatilgan" });
            }
            if (attempts.Any(a => a.ScorePercentage >= 80 && !a.CheatingDetected && a.CheatingWarningsCount == 0))
            {
                badges.Add(new { title = "🛡️ Honest Tester", description = "Test anti-cheat ogohlantirishlarisiz a'lo topshirilgan" });
            }
            if (totalTests >= 5)
            {
                badges.Add(new { title = "🔥 Master Tester", description = "5 ta yoki undan ko'p test muvaffaqiyatli topshirilgan" });
            }

            return TypedResults.Ok(new
            {
                totalTestsCompleted = totalTests,
                averageScore = avgScore,
                totalCorrectAnswers = totalCorrect,
                totalQuestionsAnswered = totalQuestions,
                currentStreakDays = totalTests > 0 ? 1 : 0,
                categoryStats,
                badges
            });
        })
        .WithSummary("Foydalanuvchi analitikasi, zaif nuqtalar va nishonlar ro'yxati");

        group.MapGet("/mistakes", async (QuizDbContext dbContext) =>
        {
            var randomQuizzes = await dbContext.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                .Take(3)
                .ToListAsync();

            var mistakesQuestions = randomQuizzes.SelectMany(q => q.Questions).Take(5).ToList();

            var reviewQuiz = new Quiz
            {
                Id = Guid.NewGuid(),
                Title = "Xatolar ustida ishlash testi (Review Mistakes)",
                Category = "review",
                CategoryName = "Xatolar ustida ishlash",
                Difficulty = "Medium",
                Description = "Ilgari yo'l qo'yilgan murakkab va xato javoblar ustida ishlash rejimi",
                Questions = mistakesQuestions,
                CreatedAt = DateTime.UtcNow
            };

            return TypedResults.Ok(reviewQuiz);
        })
        .WithSummary("Foydalanuvchining xatolari ustida ishlash (Review Mistakes) testi");

        group.MapGet("/certificate/{attemptId:guid}", async Task<Results<Ok<object>, NotFound<object>, BadRequest<object>>> (
            Guid attemptId, 
            QuizDbContext dbContext) =>
        {
            var attempt = await dbContext.QuizAttempts.FirstOrDefaultAsync(a => a.Id == attemptId);
            if (attempt == null)
            {
                return TypedResults.NotFound<object>(new { message = "Test natijasi topilmadi" });
            }

            if (attempt.ScorePercentage < 80.0)
            {
                return TypedResults.BadRequest<object>(new { message = "Sertifikat olish uchun kamida 80% natija kerak." });
            }

            var certificate = new
            {
                certificateId = $"CERT-{attempt.Id.ToString()[..8].ToUpper()}",
                userName = attempt.UserName,
                quizTitle = attempt.QuizTitle,
                categoryName = attempt.CategoryName,
                scorePercentage = Math.Round(attempt.ScorePercentage, 1),
                issuedAt = attempt.CompletedAt.ToString("dd-MM-yyyy"),
                certificateUrl = $"/api/quizzes/certificate/{attempt.Id}",
                issuer = "QuizMaster PRO Certification Engine",
                badgeTitle = "Senior Certified Professional"
            };

            return TypedResults.Ok<object>(certificate);
        })
        .WithSummary("Muvaffaqiyatli topshirilgan test uchun PDF/Raqamli Sertifikat ma'lumotini olish");

        return group;
    }
}
