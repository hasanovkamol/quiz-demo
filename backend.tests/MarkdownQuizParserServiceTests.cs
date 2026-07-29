using QuizApi.Infrastructure.Services;
using Xunit;

namespace QuizApi.Tests;

public class MarkdownQuizParserServiceTests
{
    [Fact]
    public void ParseMarkdownToQuiz_ShouldParseQuestionsAndOptionsCorrectly()
    {
        // Arrange
        var parser = new MarkdownQuizParserService();
        var sampleMd = @"# 🌐 ASP.NET Core & Web API — Test Savollari

## 🟢 EASY (1–30)

**1. ASP.NET Core qanday turdagi freymvork?**
A) Faqat Windows uchun
B) Cross-platform (Windows, Linux, macOS)
C) Faqat mobil ilovalar uchun
D) Faqat desktop ilovalar uchun
**To'g'ri javob: B**

**2. ASP.NET Core loyihasida ilova ishga tushish nuqtasi (entry point) qaysi fayl hisoblanadi?**
```csharp
public static void Main(string[] args)
{
    CreateHostBuilder(args).Build().Run();
}
```
A) Startup.cs
B) appsettings.json
C) Program.cs
D) web.config
**To'g'ri javob: C**
[Izoh: Program.cs faylidagi Main metodi ilova kirish nuqtasidir.]
";

        // Act
        var quiz = parser.ParseMarkdownToQuiz(sampleMd, defaultTitle: "ASP.NET Core Custom Test", category: "dotnet", categoryName: "C# & .NET Core", difficulty: "Oson");

        // Assert
        Assert.NotNull(quiz);
        Assert.Equal("ASP.NET Core Custom Test", quiz.Title);
        Assert.Equal("dotnet", quiz.Category);
        Assert.Equal("C# & .NET Core", quiz.CategoryName);
        Assert.Equal("Oson", quiz.Difficulty);
        Assert.Equal(2, quiz.Questions.Count);

        var q1 = quiz.Questions[0];
        Assert.Equal("ASP.NET Core qanday turdagi freymvork?", q1.Text);
        Assert.Equal(4, q1.Options.Count);
        Assert.Equal("Cross-platform (Windows, Linux, macOS)", q1.Options[1].Text);
        Assert.Equal(q1.Options[1].Id.ToString(), q1.CorrectOptionId);

        var q2 = quiz.Questions[1];
        Assert.Equal("ASP.NET Core loyihasida ilova ishga tushish nuqtasi (entry point) qaysi fayl hisoblanadi?", q2.Text);
        Assert.Contains("CreateHostBuilder", q2.CodeSnippet);
        Assert.Equal(4, q2.Options.Count);
        Assert.Equal("Program.cs", q2.Options[2].Text);
        Assert.Equal(q2.Options[2].Id.ToString(), q2.CorrectOptionId);
        Assert.Equal("Program.cs faylidagi Main metodi ilova kirish nuqtasidir.", q2.Explanation);
    }

    [Fact]
    public void ParseMarkdownToQuiz_WithoutUiTitle_ShouldFallbackToHeaderTitle()
    {
        var parser = new MarkdownQuizParserService();
        var md = @"# 🌐 ASP.NET Core & Web API — Test Savollari

**1. Test Savol?**
A) Variant A
B) Variant B
**To'g'ri javob: A**
";
        var quiz = parser.ParseMarkdownToQuiz(md);
        Assert.Equal("🌐 ASP.NET Core & Web API — Test Savollari", quiz.Title);
    }
}
