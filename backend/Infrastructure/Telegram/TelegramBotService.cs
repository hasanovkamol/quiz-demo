using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
    private readonly IMemoryCache _cache;

    private static readonly ConcurrentDictionary<long, DateTime> _lastActionTimes = new();

    // Track active quiz sessions per user
    private static readonly ConcurrentDictionary<long, ActiveQuizSession> _activeSessions = new();
    
    // Track poll IDs to their session and correct answer index
    private static readonly ConcurrentDictionary<string, PollInfo> _activePolls = new();

    public TelegramBotService(
        ITelegramBotClient botClient,
        IServiceProvider serviceProvider,
        ILogger<TelegramBotService> logger,
        IMemoryCache cache)
    {
        _botClient = botClient;
        _serviceProvider = serviceProvider;
        _logger = logger;
        _cache = cache;
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
        var userId = message.From?.Id ?? 0;
        var text = message.Text?.Trim() ?? "";

        if (text.StartsWith("/start"))
        {
            await SendWelcomeMessageAsync(chatId, userId, message.From?.FirstName ?? "Dasturchi");
        }
        else if (text.StartsWith("/quiz"))
        {
            await SendCategorySelectionAsync(chatId, userId);
        }
        else if (text.StartsWith("/results"))
        {
            await SendUserResultsHistoryAsync(chatId, userId, page: 1, selectedCategory: "all");
        }
        else if (text.StartsWith("/stats"))
        {
            await SendUserStatsAsync(chatId, message.From);
        }
        else if (text.StartsWith("/leaderboard"))
        {
            await SendLeaderboardAsync(chatId, message.From);
        }
        else
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "📌 **Buyruqlar ro'yxati:**\n\n/quiz — Test ishlashni boshlash\n/results — Natijalaringiz tarixi (Pagination)\n/stats — Shaxsiy statistikangiz\n/leaderboard — Top dasturchilar reytingi",
                parseMode: ParseMode.Markdown
            );
        }
    }

    private async Task SendWelcomeMessageAsync(long chatId, long userId, string name)
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

        var dynamicKeyboard = await GetDynamicCategoryKeyboardAsync(userId);
        buttons.AddRange(dynamicKeyboard.InlineKeyboard.Select(r => r.ToList()));

        buttons.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("📋 Natijalar Tarixi", "respage:1:all"),
            InlineKeyboardButton.WithCallbackData("🏆 Reyting", "showleaderboard"),
        });

        var inlineKeyboard = new InlineKeyboardMarkup(buttons);

        string welcomeText = $"<b>Assalomu alaykum, {name}!</b> 🇺🇿\n\n" +
                             $"<b>QuizMaster PRO</b> botiga xush kelibsiz!\n" +
                             $"Ushbu bot orqali siz IT sohasidagi barcha senior darajadagi professional testlarni topshirishingiz va o'z natijalaringiz tarixini ko'rishingiz mumkin.\n\n" +
                             $"👇 <b>Kategoriyani tanlang yoki buyruqlardan foydalaning:</b>";

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: welcomeText,
            parseMode: ParseMode.Html,
            replyMarkup: inlineKeyboard
        );
    }

    private async Task SendCategorySelectionAsync(long chatId, long userId)
    {
        var inlineKeyboard = await GetDynamicCategoryKeyboardAsync(userId);

        await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "🎯 **Qaysi bo'lim bo'yicha test topshirmoqchisiz?**",
            parseMode: ParseMode.Markdown,
            replyMarkup: inlineKeyboard
        );
    }

    private async Task<InlineKeyboardMarkup> GetDynamicCategoryKeyboardAsync(long? userId = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

        var categories = await dbContext.Quizzes
            .Select(q => new { q.Category, q.CategoryName })
            .Distinct()
            .ToListAsync();

        List<QuizAttempt> userAttempts = new();
        if (userId.HasValue)
        {
            var dbUser = await dbContext.Users.FirstOrDefaultAsync(u => u.TelegramUserId == userId.Value);
            if (dbUser != null)
            {
                userAttempts = await dbContext.QuizAttempts
                    .Where(a => a.UserName == dbUser.Name)
                    .ToListAsync();
            }
        }

        var buttons = new List<List<InlineKeyboardButton>>();
        var row = new List<InlineKeyboardButton>();

        foreach (var cat in categories)
        {
            var icon = cat.Category switch
            {
                "dotnet" => "⚡",
                "efcore" => "🗄️",
                "database" => "💾",
                "angular" => "🅰️",
                "csharp" => "💻",
                "architecture" => "🏛️",
                "messaging" => "📬",
                "devops" => "🐳",
                "senior-aspnetcore" => "🚀",
                _ => "🔥"
            };

            string starsSuffix = "";
            var catAttempts = userAttempts.Where(a => string.Equals(a.CategoryName, cat.Category, StringComparison.OrdinalIgnoreCase)).ToList();
            if (catAttempts.Any())
            {
                double maxScore = catAttempts.Max(a => a.ScorePercentage);
                int stars = maxScore switch
                {
                    > 80 => 5,
                    > 60 => 4,
                    > 40 => 3,
                    > 20 => 2,
                    > 0 => 1,
                    _ => 0
                };
                if (stars > 0)
                {
                    starsSuffix = $" {new string('⭐', stars)}";
                }
            }

            row.Add(InlineKeyboardButton.WithCallbackData($"{icon} {cat.CategoryName}{starsSuffix}", $"cat:{cat.Category}"));
            if (row.Count == 2)
            {
                buttons.Add(row);
                row = new List<InlineKeyboardButton>();
            }
        }

        if (row.Any())
        {
            buttons.Add(row);
        }

        return new InlineKeyboardMarkup(buttons);
    }

    private async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
    {
        if (callbackQuery.Message == null || string.IsNullOrEmpty(callbackQuery.Data)) return;

        var chatId = callbackQuery.Message.Chat.Id;
        var messageId = callbackQuery.Message.MessageId;
        var userId = callbackQuery.From.Id;
        var data = callbackQuery.Data;

        // Rate limiting / debouncing (400ms)
        var now = DateTime.UtcNow;
        if (_lastActionTimes.TryGetValue(userId, out var lastTime) && (now - lastTime).TotalMilliseconds < 400)
        {
            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
            return;
        }
        _lastActionTimes[userId] = now;

        if (data == "ignore")
        {
            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
            return;
        }
        else if (data == "showleaderboard")
        {
            await SendLeaderboardAsync(chatId, callbackQuery.From);
        }
        else if (data.StartsWith("cat:"))
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
                    InlineKeyboardButton.WithCallbackData("🌟 Barcha Savollar (All)", $"startquiz:{category}:All"),
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
        else if (data.StartsWith("aihelp:"))
        {
            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "💡 AI yordamchi Telegram botda vaqtincha o'chirilgan.", showAlert: true);
        }
        else if (data.StartsWith("ans:"))
        {
            var parts = data.Split(':');
            if (parts.Length >= 3 && int.TryParse(parts[1], out int qIndex) && int.TryParse(parts[2], out int optIndex))
            {
                if (_activeSessions.TryGetValue(userId, out var session))
                {
                    if (qIndex != session.CurrentIndex)
                    {
                        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "⚠️ Bu javob eskirgan.");
                        return;
                    }

                    var question = session.Questions[session.CurrentIndex];
                    if (optIndex >= 0 && optIndex < question.Options.Count)
                    {
                        var chosenOption = question.Options[optIndex];
                        bool isCorrect = chosenOption.Id.ToString() == question.CorrectOptionId;

                        if (isCorrect)
                        {
                            session.CorrectAnswersCount++;
                        }

                        // Record user details in DB
                        try
                        {
                            using var scope = _serviceProvider.CreateScope();
                            var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();
                            await GetOrCreateTelegramUserAsync(dbContext, callbackQuery.From);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Error updating Telegram user");
                        }

                        string feedback = isCorrect
                            ? $"✅ <b>TO'G'RI JAVOB!</b>\n\n💡 <b>Tushuntirish:</b> {HtmlEncode(question.Explanation)}"
                            : $"❌ <b>XATO JAVOB!</b>\n\n💡 <b>Tushuntirish:</b> {HtmlEncode(question.Explanation)}";

                        await _botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: feedback,
                            parseMode: ParseMode.Html
                        );

                        session.CurrentIndex++;
                        await Task.Delay(800);
                        await SendNextPollInSessionAsync(session);
                    }
                }
            }
        }
        else if (data.StartsWith("respage:"))
        {
            var parts = data.Split(':');
            if (parts.Length >= 3 && int.TryParse(parts[1], out int page))
            {
                var selectedCategory = parts[2];
                await SendUserResultsHistoryAsync(chatId, userId, page, selectedCategory, messageId);
            }
        }

        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
    }

    private static string HtmlEncode(string text)
    {
        if (string.IsNullOrEmpty(text)) return "";
        return text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
    }

    private async Task StartSequentialQuizSessionAsync(long userId, long chatId, string category, string difficulty)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

        var query = dbContext.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(q => q.Options)
            .Where(q => q.Category == category);

        if (!string.Equals(difficulty, "All", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(q => q.Difficulty == difficulty);
        }

        var quizzes = await query.ToListAsync();
        var allQuestions = quizzes.SelectMany(q => q.Questions).ToList();

        if (!allQuestions.Any())
        {
            await _botClient.SendTextMessageAsync(chatId, "❌ Ushbu bo'lim bo'yicha savollar topilmadi.");
            return;
        }

        var random = new Random();
        var selectedQuestions = allQuestions.OrderBy(_ => random.Next()).ToList();

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
            text: $"🚀 **{category.ToUpper()} ({difficulty})** bo'yicha {selectedQuestions.Count} ta savoldan iborat to'liq test boshlandi!\n\nBirinchi savol yuborilmoqda...",
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

        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"❓ <b>[{session.CurrentIndex + 1}/{session.Questions.Count}] SAVOL:</b>");
        sb.AppendLine($"<b>{HtmlEncode(question.Text)}</b>\n");

        if (!string.IsNullOrWhiteSpace(question.CodeSnippet))
        {
            sb.AppendLine($"<code>{HtmlEncode(question.CodeSnippet)}</code>\n");
        }

        var optionLetters = new[] { "A", "B", "C", "D", "E", "F", "G", "H" };
        var keyboardButtons = new List<InlineKeyboardButton>();

        for (int i = 0; i < question.Options.Count; i++)
        {
            var letter = i < optionLetters.Length ? optionLetters[i] : $"{i + 1}";
            var optionText = question.Options[i].Text;
            sb.AppendLine($"<b>{letter})</b> {HtmlEncode(optionText)}\n");

            keyboardButtons.Add(InlineKeyboardButton.WithCallbackData($"[ {letter} ]", $"ans:{session.CurrentIndex}:{i}"));
        }

        sb.AppendLine("👇 <i>To'g'ri deb hisoblagan variantingizni bosing:</i>");

        var buttonsLayout = new List<List<InlineKeyboardButton>>();
        var row = new List<InlineKeyboardButton>();
        foreach (var btn in keyboardButtons)
        {
            row.Add(btn);
            if (row.Count == 4)
            {
                buttonsLayout.Add(row);
                row = new List<InlineKeyboardButton>();
            }
        }
        if (row.Any())
        {
            buttonsLayout.Add(row);
        }



        var inlineKeyboard = new InlineKeyboardMarkup(buttonsLayout);

        await _botClient.SendTextMessageAsync(
            chatId: session.ChatId,
            text: sb.ToString(),
            parseMode: ParseMode.Html,
            replyMarkup: inlineKeyboard
        );
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
                if (pollAnswer.OptionIds.Contains(pollInfo.CorrectOptionId))
                {
                    session.CorrectAnswersCount++;
                }

                session.CurrentIndex++;

                await Task.Delay(1200);
                await SendNextPollInSessionAsync(session);
            }
        }

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();
            await GetOrCreateTelegramUserAsync(dbContext, pollAnswer.User);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "User record save warning in Telegram update");
        }
    }

    private async Task FinishQuizSessionAsync(ActiveQuizSession session)
    {
        _activeSessions.TryRemove(session.UserId, out _);

        int total = session.Questions.Count;
        int correct = session.CorrectAnswersCount;
        double percentage = total > 0 ? (double)correct / total * 100 : 0;

        string userName = "Telegram Dasturchi";

        Guid? savedAttemptId = null;
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

            var dbUser = await dbContext.Users.FirstOrDefaultAsync(u => u.TelegramUserId == session.UserId);
            if (dbUser != null)
            {
                userName = dbUser.Name;
            }

            var quiz = await dbContext.Quizzes.FirstOrDefaultAsync(q => q.Category == session.Category && q.Difficulty == session.Difficulty)
                       ?? await dbContext.Quizzes.FirstOrDefaultAsync(q => q.Category == session.Category);

            var attempt = new QuizAttempt
            {
                QuizId = quiz?.Id ?? Guid.NewGuid(),
                QuizTitle = quiz?.Title ?? session.Category,
                CategoryName = session.Category,
                UserName = userName,
                TotalQuestions = total,
                CorrectAnswersCount = correct,
                ScorePercentage = percentage,
                CompletedAt = DateTime.UtcNow
            };
            dbContext.QuizAttempts.Add(attempt);
            await dbContext.SaveChangesAsync();
            savedAttemptId = attempt.Id;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error saving quiz attempt to database during quiz completion");
        }

        int stars = percentage switch
        {
            > 80 => 5,
            > 60 => 4,
            > 40 => 3,
            > 20 => 2,
            > 0 => 1,
            _ => 0
        };

        string starsStr = stars > 0 ? new string('⭐', stars) : "⚪ 0 Yulduz";

        string resultMsg = $"🎉 <b>TEST YAKUNLANDI!</b>\n\n" +
                           $"👤 <b>Dasturchi:</b> {HtmlEncode(userName)}\n" +
                           $"📚 <b>Bo'lim:</b> {HtmlEncode(session.Category.ToUpper())} ({HtmlEncode(session.Difficulty)})\n" +
                           $"🎯 <b>Natija:</b> {correct} / {total} ball ({Math.Round(percentage, 1)}%)\n" +
                           $"⭐ <b>Baho:</b> {starsStr}\n\n";

        if (percentage >= 70.0)
        {
            resultMsg += $"🎓 <b>Tabriklaymiz! Siz Sertifikat oldingiz!</b>\n\n";
        }

        resultMsg += $"Natijalar tarixini ko'rish uchun /results buyrug'ini bosing.";

        var buttons = new List<List<InlineKeyboardButton>>();

        var webAppUrl = Environment.GetEnvironmentVariable("TELEGRAM_WEBAPP_URL") ?? "";
        if (percentage >= 70.0 && savedAttemptId.HasValue && !string.IsNullOrWhiteSpace(webAppUrl) && webAppUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            string certUrl = $"{webAppUrl.TrimEnd('/')}/?certId={savedAttemptId}";
            buttons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithWebApp("🎓 Sertifikatni ko'rish / yuklab olish", new WebAppInfo { Url = certUrl })
            });
        }

        buttons.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("🔄 Qayta yechish", $"startquiz:{session.Category}:{session.Difficulty}"),
            InlineKeyboardButton.WithCallbackData("📋 Natijalar Tarixi", "respage:1:all")
        });

        await _botClient.SendTextMessageAsync(
            chatId: session.ChatId,
            text: resultMsg,
            parseMode: ParseMode.Html,
            replyMarkup: new InlineKeyboardMarkup(buttons)
        );
    }

    public async Task SendUserResultsHistoryAsync(long chatId, long userId, int page = 1, string selectedCategory = "all", int? messageIdToEdit = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

        var dbUser = await dbContext.Users.FirstOrDefaultAsync(u => u.TelegramUserId == userId);
        string userName = dbUser?.Name ?? "";

        var query = dbContext.QuizAttempts.AsQueryable();

        if (!string.IsNullOrEmpty(userName))
        {
            query = query.Where(a => a.UserName == userName);
        }

        if (selectedCategory != "all")
        {
            query = query.Where(a => a.CategoryName.ToLower() == selectedCategory.ToLower());
        }

        int totalItems = await query.CountAsync();

        if (totalItems == 0)
        {
            string emptyMsg = "📝 <b>Sizda hali saqlangan test natijalari mavjud emas.</b>\n\n" +
                               "Test topshirish uchun /quiz buyrug'ini bosing.";
            if (messageIdToEdit.HasValue)
            {
                await _botClient.EditMessageTextAsync(chatId, messageIdToEdit.Value, emptyMsg, parseMode: ParseMode.Html);
            }
            else
            {
                await _botClient.SendTextMessageAsync(chatId, emptyMsg, parseMode: ParseMode.Html);
            }
            return;
        }

        int pageSize = 5;
        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        if (page < 1) page = 1;
        if (page > totalPages) page = totalPages;

        var attempts = await query
            .OrderByDescending(a => a.CompletedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        string categoryHeader = selectedCategory == "all" ? "Barcha bo'limlar" : selectedCategory.ToUpper();
        string text = $"📋 <b>TEST NATIJALARINGIZ TARIXI</b>\n" +
                      $"📂 <b>Kategoriya:</b> {categoryHeader}\n" +
                      $"📄 <b>Sahifa:</b> {page} / {totalPages} (Jami: {totalItems} ta)\n" +
                      $"───────────────────────\n\n";

        for (int i = 0; i < attempts.Count; i++)
        {
            var item = attempts[i];
            int itemNum = ((page - 1) * pageSize) + i + 1;
            string categoryName = !string.IsNullOrEmpty(item.CategoryName) ? item.CategoryName.ToUpper() : "GENERAL";
            double score = Math.Round(item.ScorePercentage, 1);

            string statusBadge = score >= 80 ? "🟢 A'lo" : score >= 50 ? "🟡 Qoniqarli" : "🔴 Zayif";
            string dateStr = item.CompletedAt.ToString("dd-MM-yyyy HH:mm");

            text += $"<b>{itemNum}. {categoryName}</b>\n" +
                    $"🎯 Ball: <b>{score}%</b> ({item.CorrectAnswersCount}/{item.TotalQuestions}) — {statusBadge}\n" +
                    $"📅 Vaqt: <i>{dateStr}</i>\n\n";
        }

        text += $"───────────────────────";

        // Build Inline Navigation & Category Filter Keyboards
        var keyboardRows = new List<List<InlineKeyboardButton>>();

        // Row 1: Category filters
        keyboardRows.Add(new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData(selectedCategory == "all" ? "✅ Barchasi" : "🌐 Barchasi", "respage:1:all"),
            InlineKeyboardButton.WithCallbackData(selectedCategory == "dotnet" ? "✅ ASP.NET" : "⚡ ASP.NET", "respage:1:dotnet"),
            InlineKeyboardButton.WithCallbackData(selectedCategory == "efcore" ? "✅ EF Core" : "🗄️ EF Core", "respage:1:efcore"),
            InlineKeyboardButton.WithCallbackData(selectedCategory == "angular" ? "✅ Angular" : "🅰️ Angular", "respage:1:angular"),
        });

        // Row 2: Pagination buttons
        var prevBtn = page > 1 
            ? InlineKeyboardButton.WithCallbackData("⬅️ Avvalgi", $"respage:{page - 1}:{selectedCategory}")
            : InlineKeyboardButton.WithCallbackData("⏸️", "ignore");

        var pageIndicatorBtn = InlineKeyboardButton.WithCallbackData($"📄 {page} / {totalPages}", "ignore");

        var nextBtn = page < totalPages
            ? InlineKeyboardButton.WithCallbackData("Keyingi ➡️", $"respage:{page + 1}:{selectedCategory}")
            : InlineKeyboardButton.WithCallbackData("⏸️", "ignore");

        keyboardRows.Add(new List<InlineKeyboardButton> { prevBtn, pageIndicatorBtn, nextBtn });

        var inlineKeyboard = new InlineKeyboardMarkup(keyboardRows);

        if (messageIdToEdit.HasValue)
        {
            await _botClient.EditMessageTextAsync(
                chatId: chatId,
                messageId: messageIdToEdit.Value,
                text: text,
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboard
            );
        }
        else
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: text,
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboard
            );
        }
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

    private async Task SendLeaderboardAsync(long chatId, TelegramUser? tgUser)
    {
        if (tgUser == null) return;

        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

        var user = await GetOrCreateTelegramUserAsync(dbContext, tgUser);

        if (user == null || !string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "⛔️ <b>Kechirasiz!</b> Leaderboard (Top dasturchilar reytingi) faqat <b>Admin</b> rolidagi foydalanuvchilar uchun ruxsat etilgan.",
                parseMode: ParseMode.Html
            );
            return;
        }

        var topUsers = await dbContext.QuizAttempts
            .GroupBy(a => a.UserName)
            .Select(g => new { Name = g.Key, AvgScore = g.Average(a => a.ScorePercentage), Total = g.Count() })
            .OrderByDescending(u => u.AvgScore)
            .Take(10)
            .ToListAsync();

        string text = "🏆 <b>TOP Dasturchilar Reytingi (Admin Console):</b>\n\n";
        for (int i = 0; i < topUsers.Count; i++)
        {
            var u = topUsers[i];
            string medal = i switch { 0 => "🥇", 1 => "🥈", 2 => "🥉", _ => $"{i + 1}." };
            text += $"{medal} <b>{HtmlEncode(u.Name)}</b> — {Math.Round(u.AvgScore, 1)}% ({u.Total} test)\n";
        }

        await _botClient.SendTextMessageAsync(chatId, text, parseMode: ParseMode.Html);
    }

    private async Task<UserEntity> GetOrCreateTelegramUserAsync(QuizDbContext dbContext, TelegramUser tgUser)
    {
        string cacheKey = $"tg_user_{tgUser.Id}";
        if (_cache.TryGetValue(cacheKey, out UserEntity? cachedUser) && cachedUser != null)
        {
            return cachedUser;
        }

        var dbUser = await dbContext.Users.FirstOrDefaultAsync(u => u.TelegramUserId == tgUser.Id);
        if (dbUser == null)
        {
            bool isAdmin = string.Equals(tgUser.Username?.TrimStart('@'), "HasanovKamol", StringComparison.OrdinalIgnoreCase);
            dbUser = new UserEntity
            {
                Id = Guid.NewGuid(),
                TelegramUserId = tgUser.Id,
                TelegramUsername = tgUser.Username,
                Name = $"{tgUser.FirstName} {tgUser.LastName}".Trim(),
                Email = $"{tgUser.Id}@telegram.user",
                Role = isAdmin ? "Admin" : "User"
            };
            dbContext.Users.Add(dbUser);
            await dbContext.SaveChangesAsync();
        }
        else if (string.Equals(tgUser.Username?.TrimStart('@'), "HasanovKamol", StringComparison.OrdinalIgnoreCase) && dbUser.Role != "Admin")
        {
            dbUser.Role = "Admin";
            dbUser.TelegramUsername = tgUser.Username;
            await dbContext.SaveChangesAsync();
        }

        _cache.Set(cacheKey, dbUser, TimeSpan.FromMinutes(10));
        return dbUser;
    }
}
