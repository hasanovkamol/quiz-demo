using System.Net;
using System.Net.Http.Json;
using QuizApi.Core.Domain.Entities;
using Xunit;

namespace QuizApi.Tests;

public class AttemptEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AttemptEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostAttempt_SavesUserAttemptAndReturnsCreated()
    {
        // Arrange
        var attempt = new QuizAttempt
        {
            QuizId = Guid.NewGuid(),
            QuizTitle = "Angular Test",
            CategoryName = "Angular Framework",
            UserName = "Test User Integration",
            TotalQuestions = 5,
            CorrectAnswersCount = 4,
            ScorePercentage = 80.0,
            TotalTimeSpentSeconds = 120,
            UserAnswers = new List<UserAnswer>
            {
                new UserAnswer
                {
                    QuestionId = Guid.NewGuid(),
                    SelectedOptionId = "opt-1",
                    IsCorrect = true,
                    TimeSpentSeconds = 20
                }
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/quizattempts", attempt);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdAttempt = await response.Content.ReadFromJsonAsync<QuizAttempt>();
        Assert.NotNull(createdAttempt);
        Assert.Equal("Test User Integration", createdAttempt.UserName);
        Assert.Equal(80.0, createdAttempt.ScorePercentage);
    }

    [Fact]
    public async Task GetAttempts_ReturnsSuccess()
    {
        // Act
        var response = await _client.GetAsync("/api/quizattempts");

        // Assert
        response.EnsureSuccessStatusCode();
        var attempts = await response.Content.ReadFromJsonAsync<List<QuizAttempt>>();
        Assert.NotNull(attempts);
    }
}
