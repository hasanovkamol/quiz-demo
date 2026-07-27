using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace QuizApi.Infrastructure.Telegram;

public class TelegramBotPollingService : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly TelegramBotService _botService;
    private readonly ILogger<TelegramBotPollingService> _logger;

    public TelegramBotPollingService(
        ITelegramBotClient botClient,
        TelegramBotService botService,
        ILogger<TelegramBotPollingService> logger)
    {
        _botClient = botClient;
        _botService = botService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Telegram Bot Polling Service starting...");

        try
        {
            await _botClient.SetMyCommandsAsync(new[]
            {
                new BotCommand { Command = "quiz", Description = "Test topshirish (Ketma-ket savollar)" },
                new BotCommand { Command = "results", Description = "Test natijalaringiz tarixi (Pagination)" },
                new BotCommand { Command = "stats", Description = "Shaxsiy statistikangiz" },
                new BotCommand { Command = "leaderboard", Description = "Top dasturchilar reytingi" }
            }, cancellationToken: stoppingToken);

            // Clean up and overwrite any old/spam description on the bot profile
            await _botClient.SetMyDescriptionAsync(
                "QuizMaster PRO — Senior .NET va IT dasturchilar uchun 720 ta interaktiv testlar va bilimni baholash bot-platformasi. 🇺🇿",
                cancellationToken: stoppingToken);

            await _botClient.SetMyShortDescriptionAsync(
                "QuizMaster PRO — Senior .NET & IT Quiz Bot",
                cancellationToken: stoppingToken);

            var webAppUrl = Environment.GetEnvironmentVariable("TELEGRAM_WEBAPP_URL");
            if (!string.IsNullOrWhiteSpace(webAppUrl) && webAppUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                await _botClient.SetChatMenuButtonAsync(menuButton: new MenuButtonWebApp
                {
                    Text = "📱 Mini App",
                    WebApp = new WebAppInfo { Url = webAppUrl }
                }, cancellationToken: stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not update Bot profile metadata / commands");
        }

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _botClient.StartReceiving(
            updateHandler: async (bot, update, ct) => await _botService.HandleUpdateAsync(update),
            pollingErrorHandler: (bot, ex, ct) =>
            {
                _logger.LogError(ex, "Telegram Bot Polling Error");
                return Task.CompletedTask;
            },
            receiverOptions: receiverOptions,
            cancellationToken: stoppingToken
        );
    }
}
