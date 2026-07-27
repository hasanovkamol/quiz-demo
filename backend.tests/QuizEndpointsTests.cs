using System.Net;
using System.Net.Http.Json;
using QuizApi.Core.Domain.Entities;
using Xunit;

namespace QuizApi.Tests;

public class QuizEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public QuizEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetQuizzes_ReturnsSuccessAndQuizList()
    {
        // Act
        var response = await _client.GetAsync("/api/quizzes");

        // Assert
        response.EnsureSuccessStatusCode();
        var quizzes = await response.Content.ReadFromJsonAsync<List<Quiz>>();
        Assert.NotNull(quizzes);
    }

    [Fact]
    public async Task PostQuiz_CreatesQuizAndReturnsCreated()
    {
        // Arrange
        var newQuiz = new Quiz
        {
            Title = "Integration Test Quiz",
            Category = "dotnet",
            CategoryName = "C# & .NET Core",
            Description = "Integration Test Description",
            Difficulty = "O'rta",
            TimeLimitSeconds = 300,
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "What is EF Core?",
                    CorrectOptionId = "opt-1",
                    Explanation = "OR Mapper for .NET",
                    Options = new List<QuestionOption>
                    {
                        new QuestionOption { Text = "Object-Relational Mapper" },
                        new QuestionOption { Text = "Web Server" }
                    }
                }
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/quizzes", newQuiz);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdQuiz = await response.Content.ReadFromJsonAsync<Quiz>();
        Assert.NotNull(createdQuiz);
        Assert.Equal("Integration Test Quiz", createdQuiz.Title);
    }

    [Fact]
    public async Task GetQuizById_NonExistentId_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync($"/api/quizzes/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
