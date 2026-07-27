using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using QuizApi.Infrastructure.Persistence;
using QuizApi.Core.Domain.Entities;
using UserEntity = QuizApi.Core.Domain.Entities.User;
using TelegramUser = global::Telegram.Bot.Types.User;

namespace QuizApi.Infrastructure.Telegram;

public class TelegramBotService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TelegramBotService> _logger;

    public TelegramBotService(
        ITelegramBotClient botClient,
        IServiceProvider serviceProvider,
        ILogger<TelegramBotService> logger)
    {
        _botClient = botClient;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task HandleUpdateAsync(Update update)
    {
        try
        {
            if (update.Type == UpdateType.Message && update.Message?.Text != null)
            {
                await HandleMessageAsync(update.Message);
            }
            else if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery != null)
            {
                await HandleCallbackQueryAsync(update.CallbackQuery);
            }
            else if (update.Type == UpdateType.PollAnswer && update.PollAnswer != null)
            {
                await HandlePollAnswerAsync(update.PollAnswer);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling Telegram update");
        }
    }

    private async Task HandleMessageAsync(Message message)
    {
        var chatId = message.Chat.Id;
        var text = message.Text?.Trim() ?? "";

        if (text.StartsWith("/start"))
        {
            await SendWelcomeMessageAsync(chatId, message.From?.FirstName ?? "Dasturchi");
        }
        else if (text.StartsWith("/quiz"))
        {
            await SendCategorySelectionAsync(chatId);
        }
        else if (text.StartsWith("/stats"))
        {
            await SendUserStatsAsync(chatId, message.From);
        }
        else if (text.StartsWith("/leaderboard"))
        {
            await SendLeaderboardAsync(chatId);
        }
        else
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "📌 Buyruqlar ro'yxati:\n\n/quiz — Test ishlashni boshlash\n/stats — Shaxsiy statistikangiz\n/leaderboard — Top dasturchilar reytingi",
                parseMode: ParseMode.Markdown
            );
        }
    }

    private async Task SendWelcomeMessageAsync(long chatId, string name)
    {
        var webAppUrl = Environment.GetEnvironmentVariable("TELEGRAM_WEBAPP_URL") ?? "http://localhost:8081";

        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithWebApp("📱 Mini App-ni Ochish", new WebAppInfo { Url = webAppUrl }),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("⚡ ASP.NET Core", "cat:dotnet"),
                InlineKeyboardButton.WithCallbackData("🗄️ EF Core", "cat:efcore"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("💾 Databases", "cat:database"),
                InlineKeyboardButton.WithCallbackData("🅰️ Angular", "cat:angular"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("💻 C# & CLR", "cat:csharp"),
                InlineKeyboardButton.WithCallbackData("🏛️ Architecture", "cat:architecture"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("📬 Messaging", "cat:messaging"),
                InlineKeyboardButton.WithCallbackData("🐳 DevOps", "cat:devops"),
            }
        });

        string welcomeText = $"<b>Assalomu alaykum, {name}!</b> 🇺🇿\n\n" +
                             $"<b>QuizMaster PRO</b> botiga xush kelibsiz!\n" +
                             $"Ushbu bot orqali siz IT sohasidagi 720 ta senior darajadagi professional testlarni ishlashingiz mumkin.\n\n" +
                             $"👇 <b>Kategoriyani tanlang yoki Mini App-ni oching:</b>";

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: welcomeText,
            parseMode: ParseMode.Html,
            replyMarkup: inlineKeyboard
        );
    }

    private async Task SendCategorySelectionAsync(long chatId)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("⚡ ASP.NET Core", "cat:dotnet"),
                InlineKeyboardButton.WithCallbackData("🗄️ EF Core", "cat:efcore"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("💾 Databases", "cat:database"),
                InlineKeyboardButton.WithCallbackData("🅰️ Angular", "cat:angular"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("💻 C# & CLR", "cat:csharp"),
                InlineKeyboardButton.WithCallbackData("🏛️ Architecture", "cat:architecture"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("📬 Messaging", "cat:messaging"),
                InlineKeyboardButton.WithCallbackData("🐳 DevOps", "cat:devops"),
            }
        });

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "🎯 **Qaysi bo'lim bo'yicha test topshirmoqchisiz?**",
            parseMode: ParseMode.Markdown,
            replyMarkup: inlineKeyboard
        );
    }

    private async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
    {
        if (callbackQuery.Message == null || string.IsNullOrEmpty(callbackQuery.Data)) return;

        var chatId = callbackQuery.Message.Chat.Id;
        var data = callbackQuery.Data;

        if (data.StartsWith("cat:"))
        {
            var category = data.Substring(4);
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("🟢 Oson (Easy)", $"startquiz:{category}:Easy"),
                    InlineKeyboardButton.WithCallbackData("🟡 O'rtacha (Medium)", $"startquiz:{category}:Medium"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("🔴 Qiyin (Hard)", $"startquiz:{category}:Hard"),
                }
            });

            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"📊 **{category.ToUpper()}** bo'limi uchun qiyinchilik darajasini tanlang:",
                parseMode: ParseMode.Markdown,
                replyMarkup: inlineKeyboard
            );
        }
        else if (data.StartsWith("startquiz:"))
        {
            var parts = data.Split(':');
            if (parts.Length >= 3)
            {
                var category = parts[1];
                var difficulty = parts[2];
                await SendRandomQuizPollAsync(chatId, category, difficulty);
            }
        }

        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
    }

    private async Task SendRandomQuizPollAsync(long chatId, string category, string difficulty)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

        var quiz = await dbContext.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(q => q.Options)
            .Where(q => q.Category == category && q.Difficulty == difficulty)
            .FirstOrDefaultAsync();

        if (quiz == null || !quiz.Questions.Any())
        {
            await _botClient.SendTextMessageAsync(chatId, "❌ Ushbu bo'lim bo'yicha savollar topilmadi.");
            return;
        }

        var random = new Random();
        var question = quiz.Questions[random.Next(quiz.Questions.Count)];

        var optionsTexts = question.Options.Select(o => o.Text).ToList();
        int correctIndex = 0;

        for (int i = 0; i < question.Options.Count; i++)
        {
            if (question.Options[i].Id.ToString() == question.CorrectOptionId || i == 0)
            {
                correctIndex = i;
                break;
            }
        }

        // Telegram limit for explanation is 200 chars
        string explanation = question.Explanation;
        if (explanation.Length > 195)
        {
            explanation = explanation.Substring(0, 192) + "...";
        }

        await _botClient.SendPollAsync(
            chatId: chatId,
            question: question.Text.Length > 300 ? question.Text.Substring(0, 295) + "..." : question.Text,
            options: optionsTexts,
            type: PollType.Quiz,
            correctOptionId: correctIndex,
            explanation: explanation,
            isAnonymous: false
        );
    }

    private async Task HandlePollAnswerAsync(PollAnswer pollAnswer)
    {
        if (pollAnswer.User == null) return;

        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

        var tgUser = pollAnswer.User;
        var dbUser = await dbContext.Users.FirstOrDefaultAsync(u => u.TelegramUserId == tgUser.Id);

        if (dbUser == null)
        {
            dbUser = new UserEntity
            {
                TelegramUserId = tgUser.Id,
                TelegramUsername = tgUser.Username,
                Name = $"{tgUser.FirstName} {tgUser.LastName}".Trim(),
                Email = $"{tgUser.Id}@telegram.user",
                Role = "User"
            };
            dbContext.Users.Add(dbUser);
            await dbContext.SaveChangesAsync();
        }

        _logger.LogInformation("Poll answer received from Telegram User: {Name} ({Id})", dbUser.Name, tgUser.Id);
    }

    private async Task SendUserStatsAsync(long chatId, TelegramUser? tgUser)
    {
        if (tgUser == null) return;

        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.TelegramUserId == tgUser.Id);
        var attempts = user != null 
            ? await dbContext.QuizAttempts.Where(a => a.UserName == user.Name).ToListAsync()
            : new List<QuizAttempt>();

        int totalAttempts = attempts.Count;
        double avgScore = totalAttempts > 0 ? attempts.Average(a => a.ScorePercentage) : 0;

        string statsText = $"📊 <b>Shaxsiy Statistikangiz:</b>\n\n" +
                           $"👤 <b>Foydalanuvchi:</b> {tgUser.FirstName}\n" +
                           $"📝 <b>Jami topshirilgan quizlar:</b> {totalAttempts} ta\n" +
                           $"🎯 <b>O'rtacha natija:</b> {Math.Round(avgScore, 1)}%\n";

        await _botClient.SendTextMessageAsync(chatId, statsText, parseMode: ParseMode.Html);
    }

    private async Task SendLeaderboardAsync(long chatId)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

        var topUsers = await dbContext.QuizAttempts
            .GroupBy(a => a.UserName)
            .Select(g => new { Name = g.Key, AvgScore = g.Average(a => a.ScorePercentage), Total = g.Count() })
            .OrderByDescending(u => u.AvgScore)
            .Take(10)
            .ToListAsync();

        string text = "🏆 <b>TOP Dasturchilar Reytingi:</b>\n\n";
        for (int i = 0; i < topUsers.Count; i++)
        {
            var u = topUsers[i];
            string medal = i switch { 0 => "🥇", 1 => "🥈", 2 => "🥉", _ => $"{i + 1}." };
            text += $"{medal} <b>{u.Name}</b> — {Math.Round(u.AvgScore, 1)}% ({u.Total} test)\n";
        }

        await _botClient.SendTextMessageAsync(chatId, text, parseMode: ParseMode.Html);
    }
}
