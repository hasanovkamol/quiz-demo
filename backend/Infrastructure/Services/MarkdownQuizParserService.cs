using System.Text.RegularExpressions;
using QuizApi.Core.Application.Interfaces;
using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Services;

public class MarkdownQuizParserService : IMarkdownQuizParserService
{
    public Quiz ParseMarkdownToQuiz(string markdownText, string? defaultTitle = null, string? category = null, string? categoryName = null, string? difficulty = null)
    {
        if (string.IsNullOrWhiteSpace(markdownText))
        {
            throw new ArgumentException("Markdown matni bo'sh bo'lishi mumkin emas!", nameof(markdownText));
        }

        var lines = markdownText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        
        string parsedTitle = "";
        string catKey = category ?? "custom";
        string diffLevel = difficulty ?? "O'rta";

        var questions = new List<Question>();

        Question? currentQuestion = null;
        var currentOptions = new List<string>();
        var codeBlockLines = new List<string>();
        bool inCodeBlock = false;
        string? currentCorrectLetter = null;
        string? currentExplanation = null;

        var questionHeaderRegex = new Regex(@"^\s*\*\*\s*(\d+\.?)?\s*(.*?)\s*\*\*\s*$", RegexOptions.Compiled);
        var optionRegex = new Regex(@"^\s*([A-Da-d1-4])[\)\.]\s*(.*)$", RegexOptions.Compiled);
        var correctAnswerRegex = new Regex(@"^\s*\*?\*?\s*To['’`]?g['’`]?ri\s+javob\s*:\s*([A-Da-d1-4])\s*\*?\*?\s*(.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        void FinalizeCurrentQuestion()
        {
            if (currentQuestion != null && !string.IsNullOrWhiteSpace(currentQuestion.Text))
            {
                if (codeBlockLines.Count > 0)
                {
                    currentQuestion.CodeSnippet = string.Join("\n", codeBlockLines).Trim();
                }

                var optionsList = new List<QuestionOption>();
                Guid correctOptId = Guid.Empty;

                for (int i = 0; i < currentOptions.Count; i++)
                {
                    var optId = Guid.NewGuid();
                    var optText = currentOptions[i].Trim();
                    optionsList.Add(new QuestionOption
                    {
                        Id = optId,
                        Text = optText
                    });

                    if (!string.IsNullOrWhiteSpace(currentCorrectLetter))
                    {
                        var letterUpper = currentCorrectLetter.Trim().ToUpper();
                        int targetIdx = letterUpper switch
                        {
                            "A" or "1" => 0,
                            "B" or "2" => 1,
                            "C" or "3" => 2,
                            "D" or "4" => 3,
                            _ => 0
                        };
                        if (i == targetIdx)
                        {
                            correctOptId = optId;
                        }
                    }
                }

                if (correctOptId == Guid.Empty && optionsList.Count > 0)
                {
                    correctOptId = optionsList[0].Id;
                }

                currentQuestion.Id = Guid.NewGuid();
                currentQuestion.Options = optionsList;
                currentQuestion.CorrectOptionId = correctOptId.ToString();
                currentQuestion.Explanation = currentExplanation ?? "";

                questions.Add(currentQuestion);
            }

            currentQuestion = null;
            currentOptions.Clear();
            codeBlockLines.Clear();
            inCodeBlock = false;
            currentCorrectLetter = null;
            currentExplanation = null;
        }

        foreach (var rawLine in lines)
        {
            var trimmed = rawLine.Trim();

            if (string.IsNullOrWhiteSpace(parsedTitle) && trimmed.StartsWith("# ") && !trimmed.StartsWith("## "))
            {
                parsedTitle = trimmed[2..].Trim();
                continue;
            }

            if (trimmed.StartsWith("## "))
            {
                if (trimmed.Contains("EASY", StringComparison.OrdinalIgnoreCase) || trimmed.Contains("Oson", StringComparison.OrdinalIgnoreCase))
                {
                    diffLevel = "Oson";
                }
                else if (trimmed.Contains("MEDIUM", StringComparison.OrdinalIgnoreCase) || trimmed.Contains("O'rta", StringComparison.OrdinalIgnoreCase))
                {
                    diffLevel = "O'rta";
                }
                else if (trimmed.Contains("HARD", StringComparison.OrdinalIgnoreCase) || trimmed.Contains("Qiyin", StringComparison.OrdinalIgnoreCase))
                {
                    diffLevel = "Qiyin";
                }
            }

            if (trimmed.StartsWith("```"))
            {
                inCodeBlock = !inCodeBlock;
                continue;
            }

            if (inCodeBlock)
            {
                codeBlockLines.Add(rawLine);
                continue;
            }

            var matchCorrect = correctAnswerRegex.Match(trimmed);
            if (matchCorrect.Success)
            {
                currentCorrectLetter = matchCorrect.Groups[1].Value;
                var extraExplanation = matchCorrect.Groups[2].Value.Trim();
                if (!string.IsNullOrWhiteSpace(extraExplanation))
                {
                    currentExplanation = extraExplanation.TrimStart(':', '-', ' ', '[', ']').TrimEnd(']');
                }
                continue;
            }

            if (trimmed.StartsWith("[Izoh:", StringComparison.OrdinalIgnoreCase) || trimmed.StartsWith("Izoh:", StringComparison.OrdinalIgnoreCase))
            {
                currentExplanation = trimmed.Replace("[Izoh:", "", StringComparison.OrdinalIgnoreCase)
                                             .Replace("Izoh:", "", StringComparison.OrdinalIgnoreCase)
                                             .TrimEnd(']').Trim();
                continue;
            }

            var matchOption = optionRegex.Match(trimmed);
            if (matchOption.Success && currentQuestion != null)
            {
                currentOptions.Add(matchOption.Groups[2].Value.Trim());
                continue;
            }

            var matchHeader = questionHeaderRegex.Match(trimmed);
            if (matchHeader.Success && !trimmed.Contains("To'g'ri javob") && !trimmed.StartsWith("|"))
            {
                FinalizeCurrentQuestion();
                var qText = matchHeader.Groups[2].Value.Trim();
                if (!string.IsNullOrWhiteSpace(qText))
                {
                    currentQuestion = new Question
                    {
                        Text = qText
                    };
                }
                continue;
            }
        }

        FinalizeCurrentQuestion();

        var finalTitle = !string.IsNullOrWhiteSpace(defaultTitle) 
            ? defaultTitle.Trim() 
            : (!string.IsNullOrWhiteSpace(parsedTitle) ? parsedTitle.Trim() : "Import qilingan Markdown Test");

        var finalCatKey = !string.IsNullOrWhiteSpace(category) ? category.ToLower().Trim() : catKey;

        var finalCategoryName = !string.IsNullOrWhiteSpace(categoryName)
            ? categoryName.Trim()
            : QuizApi.Endpoints.AdminEndpoints.GetCategoryNameById(finalCatKey);

        var finalDifficulty = !string.IsNullOrWhiteSpace(difficulty) ? difficulty.Trim() : diffLevel;

        var quizId = Guid.NewGuid();
        foreach (var q in questions)
        {
            q.QuizId = quizId;
            foreach (var opt in q.Options)
            {
                opt.QuestionId = q.Id;
            }
        }

        return new Quiz
        {
            Id = quizId,
            Title = finalTitle,
            Category = finalCatKey,
            CategoryName = finalCategoryName,
            Description = $"Markdown faylidan import qilingan {questions.Count} ta savoldan iborat test.",
            Difficulty = finalDifficulty,
            IconName = "file-text",
            TimeLimitSeconds = Math.Max(300, questions.Count * 60),
            IsCustom = true,
            CreatedAt = DateTime.UtcNow,
            Questions = questions
        };
    }
}
