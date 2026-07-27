using System.Collections.Concurrent;
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

    // Track active quiz sessions per user
    private static readonly ConcurrentDictionary<long, ActiveQuizSession> _activeSessions = new();
    
    // Track poll IDs to their session and correct answer index
    private static readonly ConcurrentDictionary<string, PollInfo> _activePolls = new();

    public TelegramBotService(
        ITelegramBotClient botClient,
        IServiceProvider serviceProvider,
        ILogger<TelegramBotService> logger)
    {
        _botClient = botClient;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public class ActiveQuizSession
    {
        public long UserId { get; set; }
        public long ChatId { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public List<Question> Questions { get; set; } = new();
        public int CurrentIndex { get; set; } = 0;
        public int CorrectAnswersCount { get; set; } = 0;
    }

    public class PollInfo
    {
        public long UserId { get; set; }
        public int CorrectOptionId { get; set; }
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
                text: "📌 **Buyruqlar ro'yxati:**\n\n/quiz — Test ishlashni boshlash (Ketma-ket savollar)\n/stats — Shaxsiy statistikangiz\n/leaderboard — Top dasturchilar reytingi",
                parseMode: ParseMode.Markdown
            );
        }
    }

    private async Task SendWelcomeMessageAsync(long chatId, string name)
    {
        var webAppUrl = Environment.GetEnvironmentVariable("TELEGRAM_WEBAPP_URL") ?? "";

        var buttons = new List<List<InlineKeyboardButton>>();

        if (!string.IsNullOrWhiteSpace(webAppUrl) && webAppUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            buttons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithWebApp("📱 Mini App-ni Ochish", new WebAppInfo { Url = webAppUrl })
            });
        }

        buttons.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("⚡ ASP.NET Core", "cat:dotnet"),
            InlineKeyboardButton.WithCallbackData("🗄️ EF Core", "cat:efcore"),
        });
        buttons.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("💾 Databases", "cat:database"),
            InlineKeyboardButton.WithCallbackData("🅰️ Angular", "cat:angular"),
        });
        buttons.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("💻 C# & CLR", "cat:csharp"),
            InlineKeyboardButton.WithCallbackData("🏛️ Architecture", "cat:architecture"),
        });
        buttons.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("📬 Messaging", "cat:messaging"),
            InlineKeyboardButton.WithCallbackData("🐳 DevOps", "cat:devops"),
        });

        var inlineKeyboard = new InlineKeyboardMarkup(buttons);

        string welcomeText = $"<b>Assalomu alaykum, {name}!</b> 🇺🇿\n\n" +
                             $"<b>QuizMaster PRO</b> botiga xush kelibsiz!\n" +
                             $"Ushbu bot orqali siz IT sohasidagi 720 ta senior darajadagi professional testlarni ketma-ket topshirishingiz mumkin.\n\n" +
                             $"👇 <b>Kategoriyani tanlang:</b>";

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
        var userId = callbackQuery.From.Id;
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
                await StartSequentialQuizSessionAsync(userId, chatId, category, difficulty);
            }
        }

        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
    }

    private async Task StartSequentialQuizSessionAsync(long userId, long chatId, string category, string difficulty)
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

        // Take 5 random questions for a quick interactive session
        var random = new Random();
        var selectedQuestions = quiz.Questions.OrderBy(_ => random.Next()).Take(5).ToList();

        var session = new ActiveQuizSession
        {
            UserId = userId,
            ChatId = chatId,
            Category = category,
            Difficulty = difficulty,
            Questions = selectedQuestions,
            CurrentIndex = 0,
            CorrectAnswersCount = 0
        };

        _activeSessions[userId] = session;

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"🚀 **{category.ToUpper()} ({difficulty})** bo'yicha 5 ta ketma-ket savoldan iborat test boshlandi!\n\nBirinchi savol yuborilmoqda...",
            parseMode: ParseMode.Markdown
        );

        await SendNextPollInSessionAsync(session);
    }

    private async Task SendNextPollInSessionAsync(ActiveQuizSession session)
    {
        if (session.CurrentIndex >= session.Questions.Count)
        {
            await FinishQuizSessionAsync(session);
            return;
        }

        var question = session.Questions[session.CurrentIndex];
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

        string explanation = question.Explanation;
        if (explanation.Length > 195)
        {
            explanation = explanation.Substring(0, 192) + "...";
        }

        string questionTitle = $"[{session.CurrentIndex + 1}/{session.Questions.Count}] {question.Text}";
        if (questionTitle.Length > 300)
        {
            questionTitle = questionTitle.Substring(0, 295) + "...";
        }

        var message = await _botClient.SendPollAsync(
            chatId: session.ChatId,
            question: questionTitle,
            options: optionsTexts,
            type: PollType.Quiz,
            correctOptionId: correctIndex,
            explanation: explanation,
            isAnonymous: false
        );

        if (message.Poll != null)
        {
            _activePolls[message.Poll.Id] = new PollInfo
            {
                UserId = session.UserId,
                CorrectOptionId = correctIndex
            };
        }
    }

    private async Task HandlePollAnswerAsync(PollAnswer pollAnswer)
    {
        if (pollAnswer.User == null) return;

        var userId = pollAnswer.User.Id;
        var pollId = pollAnswer.PollId;

        if (_activePolls.TryRemove(pollId, out var pollInfo))
        {
            if (_activeSessions.TryGetValue(userId, out var session))
            {
                // Check if user chose correct option
                if (pollAnswer.OptionIds.Contains(pollInfo.CorrectOptionId))
                {
                    session.CorrectAnswersCount++;
                }

                session.CurrentIndex++;

                // Wait 1.5 seconds so user can see poll green/red animation
                await Task.Delay(1500);

                // Send next poll or finish
                await SendNextPollInSessionAsync(session);
            }
        }

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
    }

    private async Task FinishQuizSessionAsync(ActiveQuizSession session)
    {
        _activeSessions.TryRemove(session.UserId, out _);

        int total = session.Questions.Count;
        int correct = session.CorrectAnswersCount;
        double percentage = total > 0 ? (double)correct / total * 100 : 0;

        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

        var dbUser = await dbContext.Users.FirstOrDefaultAsync(u => u.TelegramUserId == session.UserId);
        string userName = dbUser?.Name ?? "Telegram Dasturchi";

        // Save Attempt to Database
        var quiz = await dbContext.Quizzes.FirstOrDefaultAsync(q => q.Category == session.Category && q.Difficulty == session.Difficulty);
        if (quiz != null)
        {
            var attempt = new QuizAttempt
            {
                QuizId = quiz.Id,
                UserName = userName,
                ScorePercentage = percentage,
                CompletedAt = DateTime.UtcNow
            };
            dbContext.QuizAttempts.Add(attempt);
            await dbContext.SaveChangesAsync();
        }

        string resultMsg = $"🎉 <b>TEST YAKUNLANDI!</b>\n\n" +
                           $"👤 <b>Dasturchi:</b> {userName}\n" +
                           $"📚 <b>Bo'lim:</b> {session.Category.ToUpper()} ({session.Difficulty})\n" +
                           $"🎯 <b>Natija:</b> {correct} / {total} ball ({Math.Round(percentage, 1)}%)\n\n" +
                           $"🏆 <i>Natijangiz reyting bazasiga saqlandi!</i>\n\n" +
                           $"Qayta test topshirish uchun /quiz buyrug'ini bosing.";

        await _botClient.SendTextMessageAsync(
            chatId: session.ChatId,
            text: resultMsg,
            parseMode: ParseMode.Html
        );
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
