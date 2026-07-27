using Microsoft.EntityFrameworkCore;
using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task InitializeAsync(QuizDbContext context)
    {
        if (context.Database.IsNpgsql())
        {
            await context.Database.MigrateAsync();
        }
        else
        {
            await context.Database.EnsureCreatedAsync();
        }

        if (await context.Quizzes.AnyAsync())
        {
            return;
        }

        var sampleQuizzes = new List<Quiz>
        {
            new Quiz
            {
                Id = Guid.NewGuid(),
                Title = "Angular 18+ & Signals Mastery",
                Category = "angular",
                CategoryName = "Angular Framework",
                Description = "Angular 18+, Signals, Standalone komponentlar va yangi Control Flow (`@if`, `@for`) bo'yicha bilimlaringizni sinang.",
                IconName = "code-2",
                Difficulty = "O'rta",
                TimeLimitSeconds = 300,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = Guid.NewGuid(),
                        Text = "Angular 16+ versiyasida kiritilgan Signal reaktiv modelida o'zgaruvchini yangilash uchun qaysi metod ishlatiladi?",
                        CodeSnippet = "const count = signal(0);\n// Qiymatga 1 ni qo'shish uchun qaysi biridan foydalaniladi?",
                        CorrectOptionId = "opt-4",
                        Explanation = "Signal qiymatini o'zgartirish uchun ham set(), ham update() metodidan foydalanish mumkin. update() avvalgi qiymatga asoslanib yangilashda qulayroqdir.",
                        Options = new List<QuestionOption>
                        {
                            new QuestionOption { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Text = "count.set(count() + 1)" },
                            new QuestionOption { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Text = "count.update(val => val + 1)" },
                            new QuestionOption { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Text = "count.mutate(val => val + 1)" },
                            new QuestionOption { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Text = "Yuqoridagi 1 va 2 javoblarning ikkalasi ham to'g'ri" }
                        }
                    }
                }
            },
            new Quiz
            {
                Id = Guid.NewGuid(),
                Title = ".NET 8/9 & C# Senior Architecture",
                Category = "dotnet",
                CategoryName = "C# & .NET Core",
                Description = "C# 12/13, EF Core, LINQ optimization, Async/Await va SOLID prinsiplari bo'yicha bilim darajangizni sinang.",
                IconName = "cpu",
                Difficulty = "Qiyin",
                TimeLimitSeconds = 360,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = Guid.NewGuid(),
                        Text = "Entity Framework Core da N+1 muammosini oldini olish uchun bog'liq ob'yektlarni birinchi so'rovning o'zidayoq yuklab olish qanday amalga oshiriladi?",
                        CodeSnippet = "var orders = await context.Orders\n    .???(o => o.OrderItems)\n    .ToListAsync();",
                        CorrectOptionId = "opt-1",
                        Explanation = "EF Core da Eager Loading uchun .Include() metodi ishlatiladi va u so'rovda SQL JOIN ishlatib N+1 muammosini hal qiladi.",
                        Options = new List<QuestionOption>
                        {
                            new QuestionOption { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Text = ".Include(o => o.OrderItems)" },
                            new QuestionOption { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Text = ".Join(o => o.OrderItems)" }
                        }
                    }
                }
            }
        };

        await context.Quizzes.AddRangeAsync(sampleQuizzes);
        await context.SaveChangesAsync();
    }
}
