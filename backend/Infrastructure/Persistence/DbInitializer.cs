using Microsoft.EntityFrameworkCore;
using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task InitializeAsync(QuizDbContext context)
    {
        var retries = 5;
        while (retries > 0)
        {
            try
            {
                if (context.Database.IsNpgsql())
                {
                    await context.Database.MigrateAsync();
                }
                else
                {
                    await context.Database.EnsureCreatedAsync();
                }
                break;
            }
            catch (Exception) when (retries > 1)
            {
                retries--;
                await Task.Delay(2000);
            }
        }

        // Seed 720 comprehensive questions if DB has fewer than 20 quizzes
        if (await context.Quizzes.CountAsync() < 20)
        {
            await SeedComprehensiveQuizzesAsync(context);
        }
    }

    public static async Task SeedComprehensiveQuizzesAsync(QuizDbContext context)
    {
        // Clear existing partial sample quizzes to ensure clean 720 question dataset
        var existingQuizzes = await context.Quizzes.ToListAsync();
        if (existingQuizzes.Any())
        {
            context.Quizzes.RemoveRange(existingQuizzes);
            await context.SaveChangesAsync();
        }

        var comprehensiveQuizzes = ComprehensiveQuizSeeder.GetComprehensiveQuizzes();
        await context.Quizzes.AddRangeAsync(comprehensiveQuizzes);
        await context.SaveChangesAsync();
    }
}
