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

                    // Safely ensure Telegram columns and index exist on production Users table
                    await context.Database.ExecuteSqlRawAsync(
                        "ALTER TABLE \"Users\" ADD COLUMN IF NOT EXISTS \"TelegramUserId\" bigint NULL; " +
                        "ALTER TABLE \"Users\" ADD COLUMN IF NOT EXISTS \"TelegramUsername\" text NULL; " +
                        "CREATE INDEX IF NOT EXISTS \"IX_Users_TelegramUserId\" ON \"Users\" (\"TelegramUserId\");");
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

        // Agar bazadagi testlar soni kodimizdagi jami testlar sonidan kam bo'lsa, qayta yozamiz
        var totalExpected = ComprehensiveQuizSeeder.GetComprehensiveQuizzes().Count;
        if (await context.Quizzes.CountAsync() < totalExpected)
        {
            await SeedComprehensiveQuizzesAsync(context);
        }
    }
  
    public static async Task SeedComprehensiveQuizzesAsync(QuizDbContext context)
    {
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
