using System.Net;
using System.Net.Http.Json;
using QuizApi.Core.Application.Dtos;
using Xunit;

namespace QuizApi.Tests;

public class AuthEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GoogleLogin_ValidFallbackInput_ReturnsJwtTokenAndPermissions()
    {
        // Arrange
        var req = new GoogleLoginRequestDto(
            IdToken: "",
            FallbackName: "Integration Test Auth User",
            FallbackEmail: "testuser@quizmaster.local"
        );

        // Act
        var response = await _client.PostAsJsonAsync("/api/auth/google-login", req);

        // Assert
        response.EnsureSuccessStatusCode();
        var authRes = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        Assert.NotNull(authRes);
        Assert.False(string.IsNullOrWhiteSpace(authRes.Token));
        Assert.False(string.IsNullOrWhiteSpace(authRes.RefreshToken));
        Assert.Equal(300, authRes.ExpiresInSeconds);
        Assert.Equal("Integration Test Auth User", authRes.Name);
        Assert.NotEmpty(authRes.Permissions);
        Assert.Contains("quizzes:read", authRes.Permissions);
    }

    [Fact]
    public async Task RefreshToken_ValidUser_ReturnsNewTokenAndPermissions()
    {
        // 1. Initial Login
        var loginReq = new GoogleLoginRequestDto("", "Refresh Test User", "refuser@quizmaster.local");
        var loginRes = await _client.PostAsJsonAsync("/api/auth/google-login", loginReq);
        var authRes = await loginRes.Content.ReadFromJsonAsync<AuthResponseDto>();
        Assert.NotNull(authRes);

        // 2. Refresh Token Call
        var refreshReq = new RefreshTokenRequestDto(authRes.RefreshToken, authRes.UserId.ToString());
        var refreshResponse = await _client.PostAsJsonAsync("/api/auth/refresh", refreshReq);

        // Assert
        refreshResponse.EnsureSuccessStatusCode();
        var newAuthRes = await refreshResponse.Content.ReadFromJsonAsync<AuthResponseDto>();
        Assert.NotNull(newAuthRes);
        Assert.False(string.IsNullOrWhiteSpace(newAuthRes.Token));
        Assert.NotEmpty(newAuthRes.Permissions);
    }
}
