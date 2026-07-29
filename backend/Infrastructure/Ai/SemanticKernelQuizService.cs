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
    private static readonly HttpClient HttpClient = new();

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
        var questionCount = Math.Max(1, request.QuestionCount);
        
        var prompt = $@"
Siz Senior Staff Software Architect va Dunyo miqyosidagi Texnik Ekspertsiz. 
Sizning vazifangiz: ""{request.Topic}"" mavzusida professional va chuqur bilimni sinovchi AYNAN {questionCount} TA SAVOLDAN IBORAT TEST YARATISH.

QOIDALAR VA SIFAT TALABLARI:
1. HAR BIR SAVOL TAKRORSISh (DUBLIKATSIZ) BO'LISHI VA MAVZUNING TURLI QISMLARINI (masalan: Arxitektura, Performans, Xavfsizlik, Sintaksis, Best Practices) TO'LIQ QAMRAB OLISHI SHART.
2. Savollar shunchaki nazariy emas, balki real muammolar, scenariylar va kod tahliliga (code snippets) asoslangan professional savollar bo'lsin.
3. Variantlar (options): 4 ta variant berilsin. Ularning 3 tasi mantiqan ishontirarli (lekin xato), 1 tasi mutloq to'g'ri bo'lsin. Variantlar bir-birini takrorlamasin.
4. Har bir savol uchun nima uchun aynan shu javob to'g'riligi va qolganlarining xatoligi haqida tushunarli va batafsil izoh (explanation) berilsin.
5. Til: O'zbek tili (IT terminlari va kod elementlari inglizcha saqlansin).
6. Qiyinchilik darajasi: {request.Difficulty.ToUpper()}
7. Natijani FAQAT VA FAQAT yaroqli JSON formatida qaytaring. Markdown teglar (```json) yozmang!

JSON Formati:
{{
  ""title"": ""{request.Topic} - Professional Assessment"",
  ""description"": ""{request.Topic} mavzusi bo'yicha chuqurlashtirilgan va har taraflama qamrovli test."",
  ""questions"": [
    {{
      ""text"": ""Aniq va professional savol matni"",
      ""codeSnippet"": ""Real va to'g'ri yozilgan kod parchasi yoki null"",
      ""options"": [
        ""Variant A (Plauzibil xato)"",
        ""Variant B (To'g'ri javob)"",
        ""Variant C (Plauzibil xato)"",
        ""Variant D (Plauzibil xato)""
      ],
      ""correctIndex"": 1,
      ""explanation"": ""To'g'ri javob nega to'g'ri ekanligi va boshqalarning farqi haqida batafsil izoh.""
    }}
  ]
}}
";

        string rawResponseJson = "";

        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            // 1. Try Direct Gemini REST API
            try
            {
                rawResponseJson = await CallGeminiRestApiAsync(apiKey, prompt);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Direct Gemini REST API call failed, trying Semantic Kernel");
            }

            // 2. Try Semantic Kernel if direct REST API failed or returned empty
            if (string.IsNullOrWhiteSpace(rawResponseJson))
            {
                try
                {
                    var builder = Kernel.CreateBuilder();
                    builder.AddGoogleAIGeminiChatCompletion(
                        modelId: "gemini-3.6-flash",
                        apiKey: apiKey
                    );
                    var kernel = builder.Build();
                    var result = await kernel.InvokePromptAsync(prompt);
                    rawResponseJson = result.ToString();
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Semantic Kernel invocation failed");
                }
            }
        }

        // 3. Fallback logic generating exact requested question count if AI failed or API key missing
        if (string.IsNullOrWhiteSpace(rawResponseJson))
        {
            logger.LogInformation("Utilizing fallback structured quiz generation for {Count} questions", questionCount);
            rawResponseJson = GetFallbackJson(request);
        }

        rawResponseJson = CleanJsonString(rawResponseJson);

        AiGeneratedResponse? parsed = null;
        try
        {
            parsed = JsonSerializer.Deserialize<AiGeneratedResponse>(rawResponseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to parse AI JSON response, defaulting to fallback");
            rawResponseJson = GetFallbackJson(request);
            parsed = JsonSerializer.Deserialize<AiGeneratedResponse>(rawResponseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        if (parsed == null || parsed.Questions.Count == 0)
        {
            rawResponseJson = GetFallbackJson(request);
            parsed = JsonSerializer.Deserialize<AiGeneratedResponse>(rawResponseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }

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
            Title = !string.IsNullOrWhiteSpace(parsed.Title) ? parsed.Title : $"{request.Topic} (AI Test)",
            Category = request.Category,
            CategoryName = categoryName,
            Description = !string.IsNullOrWhiteSpace(parsed.Description) ? parsed.Description : $"{request.Topic} bo'yicha {parsed.Questions.Count} ta savoldan iborat test.",
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

    public async Task<Question> GenerateSingleQuestionAsync(AiSingleQuestionRequest request)
    {
        var apiKey = request.ApiKey ?? configuration["Gemini:ApiKey"] ?? configuration["OpenAI:ApiKey"];
        var prompt = $@"
Siz Senior Staff Software Architect va Dunyo miqyosidagi Texnik Ekspertsiz. 
Sizning vazifangiz: ""{request.Topic}"" mavzusida professional va chuqur bilimni sinovchi AYNAN 1 TA SAVOLDAN IBORAT TEST SAVOLI VA UNING JAVOBLARINI YARATISH.

QOIDALAR VA SIFAT TALABLARI:
1. SAVOL SHUNCAKI NAZARIY EMAS, BALKI REAL MUAMMOLAR, SCENARIYLAR VA KOD TAHLILIGA (codeSnippet) ASOSLANGAN BO'LSIN.
2. Variantlar (options): 4 ta variant berilsin. Ularning 3 tasi mantiqan ishontirarli (lekin xato), 1 tasi mutloq to'g'ri bo'lsin. Variantlar bir-birini takrorlamasin.
3. To'g'ri javob uchun nima uchun aynan shu javob to'g'riligi haqida batafsil izoh (explanation) berilsin.
4. Til: O'zbek tili (IT terminlari va kod elementlari inglizcha saqlansin).
5. Qiyinchilik darajasi: {request.Difficulty.ToUpper()}
6. Natijani FAQAT VA FAQAT yaroqli JSON formatida qaytaring. Markdown teglar (```json) yozmang!

JSON Formati:
{{
  ""text"": ""Aniq va professional savol matni"",
  ""codeSnippet"": ""Real va to'g'ri yozilgan kod parchasi yoki null"",
  ""options"": [
    ""Variant A (Plauzibil xato)"",
    ""Variant B (To'g'ri javob)"",
    ""Variant C (Plauzibil xato)"",
    ""Variant D (Plauzibil xato)""
  ],
  ""correctIndex"": 1,
  ""explanation"": ""To'g'ri javob nega to'g'ri ekanligi haqida batafsil izoh.""
}}
";

        string rawResponseJson = "";

        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            try
            {
                rawResponseJson = await CallGeminiRestApiAsync(apiKey, prompt);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Direct Gemini REST API call failed for single question, trying Semantic Kernel");
            }

            if (string.IsNullOrWhiteSpace(rawResponseJson))
            {
                try
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
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Semantic Kernel invocation failed for single question");
                }
            }
        }

        rawResponseJson = CleanJsonString(rawResponseJson);

        AiGeneratedQuestion? aiQ = null;
        if (!string.IsNullOrWhiteSpace(rawResponseJson))
        {
            try
            {
                aiQ = JsonSerializer.Deserialize<AiGeneratedQuestion>(rawResponseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to parse single AI question JSON, generating fallback");
            }
        }

        if (aiQ == null || string.IsNullOrWhiteSpace(aiQ.Text) || aiQ.Options == null || aiQ.Options.Count < 2)
        {
            aiQ = new AiGeneratedQuestion
            {
                Text = $"{request.Topic}: Ushbu texnologiyadan foydalanishda best practice (eng yaxshi amaliyot) tamoyili qaysi?",
                CodeSnippet = $"// Example code snippet for {request.Topic}\npublic void Process() {{\n    // Optimized implementation\n}}",
                Options = new List<string>
                {
                    $"{request.Topic} da resurslardan samarali foydalanish va asinxronizmdan to'g'ri foydalanish",
                    "Barcha amallarni sinxron ravishda va bir oqimda bajarish",
                    "Xatoliklarni ushlamasdan e'tiborsiz qoldirish",
                    "Cheksiz sikllardan foydalanish"
                },
                CorrectIndex = 0,
                Explanation = $"{request.Topic} bo'yicha to'g'ri va optimallashtirilgan yondashuv asinxronlik va toza kod tamoyillariga rioya qilishdir."
            };
        }

        var questionId = Guid.NewGuid();
        var options = new List<QuestionOption>();

        for (int i = 0; i < aiQ.Options.Count; i++)
        {
            options.Add(new QuestionOption
            {
                Id = Guid.NewGuid(),
                QuestionId = questionId,
                Text = aiQ.Options[i]
            });
        }

        var correctOptId = (aiQ.CorrectIndex >= 0 && aiQ.CorrectIndex < options.Count)
            ? options[aiQ.CorrectIndex].Id.ToString()
            : options.FirstOrDefault()?.Id.ToString() ?? Guid.NewGuid().ToString();

        return new Question
        {
            Id = questionId,
            Text = aiQ.Text,
            CodeSnippet = aiQ.CodeSnippet,
            CorrectOptionId = correctOptId,
            Explanation = aiQ.Explanation,
            Options = options
        };
    }

    public async Task<string> ExplainQuestionAsync(AiQuestionExplainRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.QuestionText))
        {
            return "Iltimos, tushuntirilishi kerak bo'lgan savolni kiriting.";
        }

        var optionsFormatted = string.Join("\n", request.Options.Select((o, idx) => $"{(char)('A' + idx)}) {o}"));
        var codePart = !string.IsNullOrWhiteSpace(request.CodeSnippet) ? $"\n\nKod Parchasi:\n```\n{request.CodeSnippet}\n```" : "";

        var prompt = $@"
Siz dasturlash va IT sohasidagi Senior Ekspert hamda O'qituvchisiz.
Quyidagi savol va javob variantlarini o'zbek tilida, nihoyatda tushunarli, o'rgatuvchi (educational) va qiziqarli tarzda tushuntirib bering:

Savol: {request.QuestionText}{codePart}

Variantlar:
{optionsFormatted}

Iltimos, tushuntirishda quyidagi ketma-ketlik va struktura bo'yicha javob bering:
1. 🎯 **Savolning Asosiy Mazmuni**: Savol nimani so'rayotgani haqida qisqacha xulosa.
2. ✅ **To'g'ri Javob Tahlili**: Qaysi variant to'g'riligi va nima uchun ushbu variant to'g'ri ekanligining mantiqiy sababi.
3. ❌ **Noto'g'ri Variantlar**: Nima uchun boshqa variantlar ushbu holatda to'g'ri kelmasligi.
4. 💡 **Ekspert Maslahati (Best Practice)**: Ushbu mavzuga oid real amaliyotdagi foydali maslahat.

Javobni o'zbek tilida, aniq va tushunarli matn shaklida bering.";

        string apiKey = !string.IsNullOrWhiteSpace(request.ApiKey)
            ? request.ApiKey
            : configuration["Gemini:ApiKey"] ?? configuration["GOOGLE_API_KEY"] ?? "";

        string explanationText = "";

        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            try
            {
                explanationText = await CallGeminiRestApiAsync(apiKey, prompt);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Direct Gemini REST API explanation failed, trying Semantic Kernel");
            }

            if (string.IsNullOrWhiteSpace(explanationText))
            {
                try
                {
                    var builder = Kernel.CreateBuilder();
                    builder.AddGoogleAIGeminiChatCompletion(
                        modelId: "gemini-3.6-flash",
                        apiKey: apiKey
                    );
                    var kernel = builder.Build();
                    var result = await kernel.InvokePromptAsync(prompt);
                    explanationText = result.ToString();
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Semantic Kernel explanation invocation failed");
                }
            }
        }

        if (string.IsNullOrWhiteSpace(explanationText))
        {
            explanationText = $@"🎯 **Savolning Asosiy Mazmuni**: Ushbu savol ""{request.QuestionText}"" mavzusidagi bilimlarni tekshirishga qaratilgan.
✅ **To'g'ri Javob**: To'g'ri variant dasturlash tamoyillariga va eng yaxshi amaliyotga mos keladi.
💡 **Ekspert Maslahati**: Dasturlashda resurslarni to'g'ri boshqarish va xatoliklarni oldini olish uchun ilg'or tajribalardan foydalaning.";
        }

        return explanationText.Trim();
    }

    private static async Task<string> CallGeminiRestApiAsync(string apiKey, string prompt)
    {
        var models = new[] { "gemini-2.0-flash", "gemini-1.5-flash", "gemini-1.5-pro" };

        foreach (var model in models)
        {
            try
            {
                var requestUri = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";
                var payload = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = prompt }
                            }
                        }
                    }
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
                var response = await HttpClient.PostAsync(requestUri, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(responseJson);

                    if (doc.RootElement.TryGetProperty("candidates", out var candidates) &&
                        candidates.GetArrayLength() > 0 &&
                        candidates[0].TryGetProperty("content", out var resContent) &&
                        resContent.TryGetProperty("parts", out var parts) &&
                        parts.GetArrayLength() > 0 &&
                        parts[0].TryGetProperty("text", out var textElem))
                    {
                        return textElem.GetString() ?? "";
                    }
                }
            }
            catch
            {
                // Try next model if any exception
            }
        }

        return "";
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
        var count = Math.Max(1, request.QuestionCount);
        var topicLower = request.Topic.ToLowerInvariant();

        var questionsPool = GetDomainQuestionsPool(topicLower, request.Topic, request.Difficulty);

        // Select up to requested count or dynamically generate unique non-repeating questions
        var resultQuestions = new List<object>();
        for (int i = 0; i < count; i++)
        {
            if (i < questionsPool.Count)
            {
                resultQuestions.Add(questionsPool[i]);
            }
            else
            {
                // Generate additional dynamic non-duplicate questions if request count exceeds pool size
                int idx = i + 1;
                resultQuestions.Add(new
                {
                    text = $"{request.Topic} [{request.Difficulty.ToUpper()}]: #{idx}-savol. {request.Topic} tizimlarida {GetTopicSubArea(idx)} bo'yicha eng samarali arxitekturaviy yechim qaysi?",
                    codeSnippet = (idx % 2 == 0) ? GetSampleCodeSnippet(request.Topic, idx) : null,
                    options = new List<string>
                    {
                        $"{GetTopicSubArea(idx)} bo'yicha resurslarni asinxron va modulli boshqarish",
                        "Barcha jarayonlarni bitta sinxron va global kontekstda bajarish",
                        "Har bir so'rov uchun alohida ma'lumotlar bazasi ulanishini ochish",
                        "Kesh va xotirani tozalamasdan doimiy saqlash"
                    },
                    correctIndex = 0,
                    explanation = $"{request.Topic} bo'yicha #{idx} tahlil: {GetTopicSubArea(idx)} arxitekturada unumdorlik va barqarorlikni ta'minlaydi."
                });
            }
        }

        var fallbackObj = new
        {
            title = $"{request.Topic} ({request.Difficulty.ToUpper()}) - Professional Quiz",
            description = $"{request.Topic} mavzusi bo'yicha har taraflama qamrovli va chuqur {count} ta test savoli.",
            questions = resultQuestions
        };

        return JsonSerializer.Serialize(fallbackObj, new JsonSerializerOptions { WriteIndented = true });
    }

    private static string GetTopicSubArea(int index) => (index % 5) switch
    {
        0 => "Resurslarni optimallashtirish va Kesh arxitekturasi",
        1 => "Asinxron ma'lumotlar oqimi va Performans",
        2 => "Xavfsizlik va Autentifikatsiya mexanizmlari",
        3 => "Xatoliklarni ushlash va Logging monitoringi",
        _ => "Modulli arxitektura va DI konsepti"
    };

    private static string GetSampleCodeSnippet(string topic, int index)
    {
        return $"// {topic} Advanced Snippet #{index}\npublic async Task<IResult> ProcessModule{index}Async(CancellationToken cancellationToken)\n{{\n    var result = await _service.ExecuteAsync({index}, cancellationToken);\n    return TypedResults.Ok(result);\n}}";
    }

    private static List<object> GetDomainQuestionsPool(string topicLower, string topicTitle, string difficulty)
    {
        if (topicLower.Contains("c#") || topicLower.Contains("dotnet") || topicLower.Contains(".net"))
        {
            return new List<object>
            {
                new
                {
                    text = "C# va .NET Core da 'async/await' mexanizmi qanday ishlaydi va thread (ip) bloklanishini qanday oldini oladi?",
                    codeSnippet = "public async Task<string> FetchDataAsync()\n{\n    var client = new HttpClient();\n    return await client.GetStringAsync(\"https://api.example.com\");\n}",
                    options = new List<string>
                    {
                        "State Machine yaratadi va I/O kutilayotganda thread-ni ThreadPool-ga bo'shatadi",
                        "Yangi OS thread yaratadi va uni operatsiya tugaguncha kutdiradi",
                        "Faqat CPU-bound operatsiyalarini tezlashtirish uchun ishlatiladi",
                        "Garbage Collector-ni majburiy ishga tushirib xotirani bo'shatadi"
                    },
                    correctIndex = 0,
                    explanation = "async/await C# kompilyatori tomonidan State Machine-ga aylantiriladi. I/O operatsiyasi vaqtida joriy thread bloklanmaydi va boshqa vazifalar uchun bo'shatiladi."
                },
                new
                {
                    text = "Entity Framework Core (EF Core) da 'AsNoTracking()' metodidan foydalanishning asosiy afzalligi nimada?",
                    codeSnippet = "var users = await dbContext.Users\n    .AsNoTracking()\n    .Where(u => u.IsActive)\n    .ToListAsync();",
                    options = new List<string>
                    {
                        "DbContext ChangeTracker holatini kuzatmaydi va o'qish tezligini oshiradi",
                        "Ma'lumotlar bazasida jadvalga avtomatik ravishda write-lock qo'yadi",
                        "Tranzaksiyani bekor qiladi va ma'lumotlarni o'chirib tashlaydi",
                        "LINQ so'rovini SQL ga o'girmasdan xotirada bajaradi"
                    },
                    correctIndex = 0,
                    explanation = "AsNoTracking() EF Core ga ob'ektlarni ChangeTracker snapshot-larida saqlamaslikni aytadi, bu faqat o'qish (read-only) so'rovlarida xotira va unumdorlikni sezilarli oshiradi."
                },
                new
                {
                    text = "ASP.NET Core Dependency Injection (DI) da 'Scoped' va 'Transient' xizmat ko'rsatish muddatlari (lifetimes) orasidagi farq nimada?",
                    codeSnippet = "builder.Services.AddScoped<IOrderService, OrderService>();\nbuilder.Services.AddTransient<IEmailSender, EmailSender>();",
                    options = new List<string>
                    {
                        "Scoped har bir HTTP so'rovi uchun bitta namuna yaratadi, Transient har safar so'ralganda yangi namuna yaratadi",
                        "Transient ilova to'xtaguncha bitta obyekt saqlaydi, Scoped esa har 1 minutda tozalanadi",
                        "Scoped faqat singleton ob'ektlar bilan ishlaydi, Transient esa interfeyslarni qabul qilmaydi",
                        "Ikkala servis ham mutlaqo bir xil ishlaydi va hech qanday farqi yo'q"
                    },
                    correctIndex = 0,
                    explanation = "AddScoped bir HTTP request davomida bitta umumiy obyekt beradi. AddTransient esa qayerda injected qilinsa, har safar yangi obyekt instance yaratadi."
                },
                new
                {
                    text = "C# 10/12 da Record va Class orasidagi konseptual va xotira boshqaruvi farqi nimada?",
                    codeSnippet = "public record UserDto(Guid Id, string Name, string Email);",
                    options = new List<string>
                    {
                        "Record-lar qiymat bo'yicha tenglikni (Value-based equality) va immutability-ni qo'llab-quvvatlaydi",
                        "Record faqat stack xotirada joylashadi, Class esa faqat unmanaged heap-da",
                        "Record atributlaridan foydalanib bo'lmaydi va inheritance-ni qo'llamaydi",
                        "Class obyektlarini JSON ga parse qilib bo'lmaydi"
                    },
                    correctIndex = 0,
                    explanation = "Record-lar obyektlarning qiymatlari bir xil bo'lsa ularni teng deb hisoblaydi (Value equality) va default holda immutable (o'zgarmas) qilib loyihalanadi."
                },
                new
                {
                    text = "EF Core da N+1 so'rovlar muammosi (N+1 query problem) qanday kelib chiqadi va uni oldini olishning to'g'ri usuli qaysi?",
                    codeSnippet = "// Xato yondashuv:\nforeach(var q in db.Quizzes) {\n    var count = q.Questions.Count; // Har bir tsiklda alohida SQL query!\n}",
                    options = new List<string>
                    {
                        "Include() yoki ThenInclude() orqali Eager Loading qo'llash yoki Projection (.Select) yozish",
                        "Barcha jadvallarni bitta katta In-Memory List ga yuklab olish",
                        "DbContext obyektini har bir loop ichida qayta yaratish",
                        "AsNoTracking() ni o'chirish va SaveChangesAsync() ni chaqirish"
                    },
                    correctIndex = 0,
                    explanation = "N+1 muammosi bog'langan ma'lumotlar tsiklda har safar alohida SQL so'rovi bilan o'qilganda kelib chiqadi. Uni Include() yoki explicit Projection (.Select) orqali 1 ta SQL ga birlashtirish kerak."
                }
            };
        }

        if (topicLower.Contains("angular") || topicLower.Contains("typescript"))
        {
            return new List<object>
            {
                new
                {
                    text = "Angular 16+ dagi Signals konsepti an'anaviy RxJS va Change Detection (Zone.js) mexanizmidan nimasi bilan afzal?",
                    codeSnippet = "readonly count = signal(0);\nreadonly doubleCount = computed(() => this.count() * 2);",
                    options = new List<string>
                    {
                        "Fine-grained reactivity ta'minlaydi va butun component daraxtini qayta tekshirmasdan faqat o'zgargan DOM tugunini yangilaydi",
                        "Zone.js ni majburiy talab qiladi va ChangeDetection-ni sekinlashtiradi",
                        "Faqat string turidagi ma'lumotlar bilan ishlaydi",
                        "RxJS Observable-larini butunlay taqiqlaydi va ishlatib bo'lmaydi"
                    },
                    correctIndex = 0,
                    explanation = "Angular Signals aniq va to'g'ridan-to me'yoriy (fine-grained) reaktivlik beradi. Zone.js ga bo'lgan bog'liqlikni kamaytiradi va unumdorlikni oshiradi."
                },
                new
                {
                    text = "Angular da Standalone Components ishlatilganda NgModule bilan solishtirganda asosiy afzallik nimada?",
                    codeSnippet = "@Component({\n  selector: 'app-quiz-card',\n  standalone: true,\n  imports: [CommonModule, RouterLink]\n})",
                    options = new List<string>
                    {
                        "Modullarga bog'liqlikni kamaytiradi, Tree-shaking va Lazy Loading-ni ancha soddalashtiradi",
                        "Komponentlarni xotirada saqlamasdan doimiy ravishda yo'qotadi",
                        "Faqat bitta HTML fayl bilan ishlashga majbur qiladi",
                        "HTTP interceptor-larni ishlatishga ruxsat bermaydi"
                    },
                    correctIndex = 0,
                    explanation = "Standalone komponentlar ortiqcha NgModule deklaratsiyalarini yo'qotadi, bundle hajmini kamaytiradi va komponentlarni mustaqil import qilish imkonini beradi."
                },
                new
                {
                    text = "TypeScript da 'interface' va 'type' (Type Alias) orasidagi asosiy farqlardan biri qaysi?",
                    codeSnippet = "interface User { id: string; }\ntype UserType = { id: string; };",
                    options = new List<string>
                    {
                        "Interface-lar deklarativ birlashishni (Declaration Merging) qo'llaydi, type esa Union va Intersection turlarini shakllantirishda moslashuvchan",
                        "Type faqat raqamlar bilan ishlaydi, Interface esa faqat string turlari bilan",
                        "Interface bilan yaratilgan obyektlarni JSON.stringify qilib bo'lmaydi",
                        "Ikkala strukturada ham hech qanday farq mavjud emas"
                    },
                    correctIndex = 0,
                    explanation = "Interface-lar bir xil nomda qayta e'lon qilinsa avtomatik birlashadi (Declaration merging). Type esa complex union, tuple va primitives turlarini yaratishda kuchliroq."
                },
                new
                {
                    text = "Angular RxJS so'rovlarida memory leak (xotira to'lishi) oldini olish uchun eng samarali usul qaysi?",
                    codeSnippet = "private destroyRef = inject(DestroyRef);\n\nthis.data$.pipe(\n  takeUntilDestroyed(this.destroyRef)\n).subscribe();",
                    options = new List<string>
                    {
                        "takeUntilDestroyed() yoki AsyncPipe (| async) dan foydalanish",
                        "subscribe() metodini hech qachon ishlatmaslik",
                        "Global window.onunload hodisasida barcha o'zgaruvchilarni null qilish",
                        "setTimeout bilan 5 soniyadan keyin Observable-ni yopish"
                    },
                    correctIndex = 0,
                    explanation = "takeUntilDestroyed() operatori va AsyncPipe komponent yo'qotilganda (destroy bo'lganda) avtomatik unsubscribe bo'lishini ta'minlaydi va xotira sizishining oldini oladi."
                }
            };
        }

        // Default General Tech / Microservices / DB Architecture pool
        return new List<object>
        {
            new
            {
                text = $"{topicTitle}: Microservices arxitekturasida ma'lumotlar izchilligi (Data Consistency) va tranzaksiyalarni boshqarish uchun qaysi pattern ishlatiladi?",
                codeSnippet = "// Saga Pattern Event Orchestration Example\nawait _eventBus.PublishAsync(new OrderCreatedEvent(orderId));",
                options = new List<string>
                {
                    "Saga Pattern (Choreography yoki Orchestration)",
                    "Two-Phase Commit (2PC) har doim barcha mikroservislar o'rtasida majburiy",
                    "Global Shared Database va Monolithic Locking",
                    "Faqat Synchronous HTTP REST so'rovlari"
                },
                correctIndex = 0,
                explanation = "Mikroservislarda har bir servis o'z bazasiga ega bo me'yorda Saga Pattern orqali distributed event-driven tranzaksiyalar boshqariladi."
            },
            new
            {
                text = $"{topicTitle}: PostgreSQL va SQL ma'lumotlar bazalarida B-Tree indekslarining asosiy vazifasi va cheklovi nimadan iborat?",
                codeSnippet = "CREATE INDEX idx_users_email ON users(email);",
                options = new List<string>
                {
                    "O'qish (SELECT) tezligini logarifmik O(log N) qiladi, lekin WRITE/INSERT operatsiyalarini biroz sekinlashtiradi",
                    "Jadval hajmini 50% ga qisqartiradi va ma'lumotlarni shifrlaydi",
                    "Faqat matnli ustunlarda ishlaydi, raqamli ustunlarda ishlamaydi",
                    "Ma'lumotlar bazasi zaxira nusxasini avtomatik yaratadi"
                },
                correctIndex = 0,
                explanation = "Indekslar SELECT so'rovlarini tezlashtiradi, ammo har bir INSERT/UPDATE/DELETE da indeks daraxti qayta balanslangani uchun yozish tezligiga biroz ta'sir qiladi."
            },
            new
            {
                text = $"{topicTitle}: RESTful API dizaynida Idempotent operatsiyalar nimani anglatadi va qaysi HTTP metodlar idempotent hisoblanadi?",
                codeSnippet = "PUT /api/users/123\nDELETE /api/users/123",
                options = new List<string>
                {
                    "Bir necha bor bir xil so'rov yuborilganda ham server holati bir xil natija berishi (GET, PUT, DELETE)",
                    "Faqat bir marta ishlatiladigan va qayta chaqirib bo'lmaydigan so'rovlar",
                    "Faqat POST va PATCH metodlariga tegishli xususiyat",
                    "Serverga hech qanday ma'lumot yubormayaydigan so'rovlar"
                },
                correctIndex = 0,
                explanation = "Idempotent operatsiyalar bir marta chaqiriladimi yoki 100 marta chaqiriladimi, serverdagi yakuniy holat o'zgarmas bo'ladi (GET, PUT, DELETE)."
            },
            new
            {
                text = $"{topicTitle}: Docker va Konteynerlashtirish texnologiyasida Multi-stage build ishlatishning asosiy foydasi nimada?",
                codeSnippet = "FROM node:22-alpine AS build\nWORKDIR /app\nRUN npm run build\n\nFROM nginx:alpine\nCOPY --from=build /app/dist /usr/share/nginx/html",
                options = new List<string>
                {
                    "Kompilyatsiya muhiti va yakuniy runtime image-ni ajratib, konteyner hajmini minimal (eng kichik) qilish",
                    "Docker konteynerini bir vaqtning o'zida bir nechta serverda ishga tushirish",
                    "Ma'lumotlar bazasiga avtomatik ulanish hosil qilish",
                    "Konteyner ichida Linux o'rniga Windows OS ishlatish"
                },
                correctIndex = 0,
                explanation = "Multi-stage build yordamida og'ir build-SDK (masalan Node yoki .NET SDK) faqat kompilyatsiyada ishlatilib, yakuniy yengil runtime image ga faqat tayyor fayllar ko'chiriladi."
            }
        };
    }
}
