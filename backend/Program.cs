using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Telegram.Bot;
using QuizApi.Core.Application.Interfaces;
using QuizApi.Core.Domain.Constants;
using QuizApi.Endpoints;
using QuizApi.Infrastructure.Ai;
using QuizApi.Infrastructure.Identity;
using QuizApi.Infrastructure.Persistence;
using QuizApi.Infrastructure.Telegram;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS for LAN access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Database context (InMemory for Testing, Npgsql for Production/Development)
if (builder.Environment.EnvironmentName == "Testing")
{
    builder.Services.AddDbContext<QuizDbContext>(options =>
        options.UseInMemoryDatabase("IntegrationTestDb_Testing"));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Host=db;Port=5432;Database=quizdb;Username=postgres;Password=postgres";

    builder.Services.AddDbContext<QuizDbContext>(options =>
        options.UseNpgsql(connectionString));
}

// JWT Authentication
var jwtSecretKey = builder.Configuration["Jwt:SecretKey"] ?? "QuizMaster_Super_Secret_JWT_Key_2026_Enterprise_Secure!";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "QuizMasterAPI",
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? "QuizMasterApp",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
    };
});

// Permission-Based Authorization Registration
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    foreach (var permission in Permissions.AllPermissions)
    {
        options.AddPolicy(permission, policy =>
            policy.Requirements.Add(new PermissionRequirement(permission)));
    }
});

// Register Telegram Bot Client & Service
var telegramBotToken = builder.Configuration["TelegramBot:Token"] ?? "1234567890:DEMO_TELEGRAM_BOT_TOKEN";
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(telegramBotToken));
builder.Services.AddSingleton<TelegramBotService>();

// Register Application & Infrastructure Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISemanticKernelQuizService, SemanticKernelQuizService>();

var app = builder.Build();

// Configure HTTP pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// Map Minimal API Endpoint Groups
app.MapQuizEndpoints();
app.MapAttemptEndpoints();
app.MapAdminEndpoints();
app.MapAuthEndpoints();
app.MapTelegramEndpoints();

// Auto initialize and seed DB
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<QuizDbContext>();
        await DbInitializer.InitializeAsync(dbContext);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Database initialization error");
    }
}

app.Run();
