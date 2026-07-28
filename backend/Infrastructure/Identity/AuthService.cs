using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Core.Application.Dtos;
using QuizApi.Core.Application.Interfaces;
using QuizApi.Core.Domain.Constants;
using QuizApi.Core.Domain.Entities;
using QuizApi.Infrastructure.Persistence;

namespace QuizApi.Infrastructure.Identity;

public class AuthService(
    QuizDbContext dbContext, 
    IConfiguration configuration, 
    ILogger<AuthService> logger,
    IKeycloakService keycloakService) : IAuthService
{
    private const int ACCESS_TOKEN_EXPIRATION_SECONDS = 300;

    public async Task<AuthResponseDto> AuthenticateGoogleUserAsync(GoogleLoginRequestDto request)
    {
        string email = "";
        string name = "";
        string googleId = "";
        string pictureUrl = "";

        if (!string.IsNullOrWhiteSpace(request.IdToken))
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
                email = payload.Email;
                name = payload.Name ?? payload.Email.Split('@')[0];
                googleId = payload.Subject;
                pictureUrl = payload.Picture ?? "";
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Google ID Token Validation failed, using fallback identification if provided");
            }
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            email = request.FallbackEmail ?? $"user_{Guid.NewGuid().ToString()[..8]}@quizmaster.local";
            name = request.FallbackName ?? "Foydalanuvchi";
        }

        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        bool isAdmin = string.Equals(email, "khasanovkamol3834@gmail.com", StringComparison.OrdinalIgnoreCase);
        
        if (user == null)
        {
            user = new User
            {
                Id = Guid.NewGuid(),
                GoogleId = googleId,
                Email = email,
                Name = name,
                PictureUrl = pictureUrl,
                Role = isAdmin ? "Admin" : "User",
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            };
            dbContext.Users.Add(user);
        }
        else
        {
            user.LastLoginAt = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(name)) user.Name = name;
            if (!string.IsNullOrWhiteSpace(pictureUrl)) user.PictureUrl = pictureUrl;
            if (isAdmin && user.Role != "Admin") user.Role = "Admin";
        }

        await dbContext.SaveChangesAsync();

        // Sync User to Keycloak Realm asynchronously
        _ = Task.Run(() => keycloakService.SyncUserToKeycloakAsync(user.Email, user.Name, user.Role));

        var permissions = Permissions.GetPermissionsForRole(user.Role);
        var jwtToken = GenerateJwtToken(user, permissions, ACCESS_TOKEN_EXPIRATION_SECONDS);
        var refreshToken = GenerateRefreshToken(user);

        return new AuthResponseDto(
            Token: jwtToken,
            RefreshToken: refreshToken,
            ExpiresInSeconds: ACCESS_TOKEN_EXPIRATION_SECONDS,
            UserId: user.Id,
            Email: user.Email,
            Name: user.Name,
            PictureUrl: user.PictureUrl,
            Role: user.Role,
            Permissions: permissions
        );
    }

    public async Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.UserId) || !Guid.TryParse(request.UserId, out var uId))
        {
            return null;
        }

        var user = await dbContext.Users.FindAsync(uId);
        if (user == null) return null;

        var permissions = Permissions.GetPermissionsForRole(user.Role);
        var jwtToken = GenerateJwtToken(user, permissions, ACCESS_TOKEN_EXPIRATION_SECONDS);
        var refreshToken = GenerateRefreshToken(user);

        return new AuthResponseDto(
            Token: jwtToken,
            RefreshToken: refreshToken,
            ExpiresInSeconds: ACCESS_TOKEN_EXPIRATION_SECONDS,
            UserId: user.Id,
            Email: user.Email,
            Name: user.Name,
            PictureUrl: user.PictureUrl,
            Role: user.Role,
            Permissions: permissions
        );
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await dbContext.Users.FindAsync(userId);
    }

    private string GenerateJwtToken(User user, string[] permissions, int durationSeconds)
    {
        var secretKey = configuration["Jwt:SecretKey"] ?? "QuizMaster_Super_Secret_JWT_Key_2026_Enterprise_Secure!";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claimsList = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Role, user.Role)
        };

        foreach (var perm in permissions)
        {
            claimsList.Add(new Claim("permission", perm));
        }

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"] ?? "QuizMasterAPI",
            audience: configuration["Jwt:Audience"] ?? "QuizMasterApp",
            claims: claimsList,
            expires: DateTime.UtcNow.AddSeconds(durationSeconds),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken(User user)
    {
        return $"ref_{user.Id}_{Guid.NewGuid().ToString().Replace("-", "")}_{DateTime.UtcNow.Ticks}";
    }
}
