using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.SemanticKernel;
using QuizApi.Core.Application.Dtos;
using QuizApi.Core.Application.Interfaces;
using QuizApi.Core.Domain.Entities;
using QuizApi.Infrastructure.Persistence;

namespace QuizApi.Infrastructure.Ai;

public class SemanticKernelQuizService(QuizDbContext dbContext, IConfiguration configuration, ILogger<SemanticKernelQuizService> logger) 
    : ISemanticKernelQuizService
{
    private class AiGeneratedResponse
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("description")]
        public string Description { get; set; } = "";

        [JsonPropertyName("questions")]
        public List<AiGeneratedQuestion> Questions { get; set; } = [];
    }

    private class AiGeneratedQuestion
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = "";

        [JsonPropertyName("codeSnippet")]
        public string? CodeSnippet { get; set; }

        [JsonPropertyName("options")]
        public List<string> Options { get; set; } = [];

        [JsonPropertyName("correctIndex")]
        public int CorrectIndex { get; set; }

        [JsonPropertyName("explanation")]
        public string Explanation { get; set; } = "";
    }

    public async Task<Quiz> GenerateQuizAsync(AiQuizGenerationRequest request)
    {
        var apiKey = request.ApiKey ?? configuration["Gemini:ApiKey"] ?? configuration["OpenAI:ApiKey"];
        
        var prompt = $@"
Siz Senior Software Architect hamda Quiz Mutaxassisisiz. 
Quydagi talablar asosida faqat va faqat yaroqli JSON formatida test tayyorlang. Boshqa hech qanday izoh yoki markdown teglar yozmang (pure raw JSON string).

Mavzu: {request.Topic}
Kategoriya: {request.Category}
Qiyinchilik darajasi: {request.Difficulty}
Savollar soni: {request.QuestionCount}

JSON Formati namunasi:
{{
  ""title"": ""Mavzuga mos sarlavha"",
  ""description"": ""Test haqida qisqacha tavsif"",
  ""questions"": [
    {{
      ""text"": ""Savol matni (Uzbek tilida)"",
      ""codeSnippet"": ""Ixtiyoriy kod parchasi yoki null"",
      ""options"": [
        ""Variant A matni"",
        ""Variant B matni"",
        ""Variant C matni"",
        ""Variant D matni""
      ],
      ""correctIndex"": 0,
      ""explanation"": ""To'g'ri javob uchun batafsil izoh""
    }}
  ]
}}
";

        string rawResponseJson = "";

        try
        {
            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                var builder = Kernel.CreateBuilder();
                builder.AddGoogleAIGeminiChatCompletion(
                    modelId: "gemini-1.5-flash",
                    apiKey: apiKey
                );
                var kernel = builder.Build();
                var result = await kernel.InvokePromptAsync(prompt);
                rawResponseJson = result.ToString();
            }
            else
            {
                throw new InvalidOperationException("API Key ko'rsatilmadi! Iltimos Admin paneldan Gemini API Key kiriting.");
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Semantic Kernel client invocation issue, utilizing fallback structured generation logic");
            rawResponseJson = GetFallbackJson(request);
        }

        rawResponseJson = CleanJsonString(rawResponseJson);

        var parsed = JsonSerializer.Deserialize<AiGeneratedResponse>(rawResponseJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("AI Natijasini PARSE qilib bo'lmadi");

        var categoryName = request.Category switch
        {
            "angular" => "Angular Framework",
            "dotnet" => "C# & .NET Core",
            "webdev" => "Web Infrastructure",
            _ => "Maxsus AI Test"
        };

        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Title = parsed.Title.Length > 0 ? parsed.Title : $"{request.Topic} (AI Generated)",
            Category = request.Category,
            CategoryName = categoryName,
            Description = parsed.Description.Length > 0 ? parsed.Description : $"{request.Topic} bo'yicha AI tomonidan yaratilgan test.",
            Difficulty = request.Difficulty,
            TimeLimitSeconds = Math.Max(1, request.TimeLimitMinutes) * 60,
            IsCustom = true,
            CreatedAt = DateTime.UtcNow,
            Questions = []
        };

        foreach (var q in parsed.Questions)
        {
            var questionId = Guid.NewGuid();
            var options = new List<QuestionOption>();

            for (int i = 0; i < q.Options.Count; i++)
            {
                options.Add(new QuestionOption
                {
                    Id = Guid.NewGuid(),
                    QuestionId = questionId,
                    Text = q.Options[i]
                });
            }

            var correctOptId = (q.CorrectIndex >= 0 && q.CorrectIndex < options.Count)
                ? options[q.CorrectIndex].Id.ToString()
                : options.FirstOrDefault()?.Id.ToString() ?? Guid.NewGuid().ToString();

            quiz.Questions.Add(new Question
            {
                Id = questionId,
                QuizId = quiz.Id,
                Text = q.Text,
                CodeSnippet = q.CodeSnippet,
                CorrectOptionId = correctOptId,
                Explanation = q.Explanation,
                Options = options
            });
        }

        dbContext.Quizzes.Add(quiz);
        await dbContext.SaveChangesAsync();

        return quiz;
    }

    private static string CleanJsonString(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return "{}";
        var cleaned = input.Trim();
        if (cleaned.StartsWith("```json"))
        {
            cleaned = cleaned[7..];
        }
        else if (cleaned.StartsWith("```"))
        {
            cleaned = cleaned[3..];
        }
        if (cleaned.EndsWith("```"))
        {
            cleaned = cleaned[..^3];
        }
        return cleaned.Trim();
    }

    private static string GetFallbackJson(AiQuizGenerationRequest request)
    {
        return $@"
{{
  ""title"": ""{request.Topic} bo'yicha AI Test"",
  ""description"": ""{request.Topic} mavzusidagi savollar to'plami."",
  ""questions"": [
    {{
      ""text"": ""{request.Topic} bo'yicha asosiy tushuncha va uning vazifasi nimadan iborat?"",
      ""codeSnippet"": null,
      ""options"": [
        ""Ma'lumotlar oqimini va unumdorlikni oshirish"",
        ""Tizim xavfsizligini ta'minlash"",
        ""Faqat fayllarni saqlash"",
        ""Yuqoridagilarning barchasi""
      ],
      ""correctIndex"": 0,
      ""explanation"": ""{request.Topic} asosan unumdorlik va to'g'ri arxitekturani ta'minlaydi.""
    }}
  ]
}}
";
    }
}
