using QuizApi.Core.Domain.Entities;

namespace QuizApi.Core.Application.Interfaces;

public interface IMarkdownQuizParserService
{
    Quiz ParseMarkdownToQuiz(string markdownText, string? defaultTitle = null, string? category = null, string? categoryName = null, string? difficulty = null);
}
