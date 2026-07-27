using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetComprehensiveQuizzes()
    {
        var quizzes = new List<Quiz>();

        quizzes.AddRange(GetDotNetQuizzes());
        quizzes.AddRange(GetEfCoreQuizzes());
        quizzes.AddRange(GetDatabaseQuizzes());
        quizzes.AddRange(GetAngularQuizzes());
        quizzes.AddRange(GetCSharpQuizzes());
        quizzes.AddRange(GetArchitectureQuizzes());
        quizzes.AddRange(GetMessagingQuizzes());
        quizzes.AddRange(GetDevOpsQuizzes());

        return quizzes;
    }

    private static Question CreateQuestion(string text, string? code, List<string> options, string explanation)
    {
        var question = new Question
        {
            Text = text,
            CodeSnippet = code,
            Explanation = explanation,
            Options = new List<QuestionOption>()
        };

        for (int i = 0; i < options.Count; i++)
        {
            question.Options.Add(new QuestionOption { Text = options[i] });
        }

        return question;
    }

    private static Question CreateQuestion(string text, List<string> options, string explanation)
    {
        return CreateQuestion(text, null, options, explanation);
    }

    private static Quiz CreateQuiz(string title, string category, string categoryName, string description, string difficulty, string iconName, List<Question> questions)
    {
        var quizId = Guid.NewGuid();
        var timeLimit = difficulty.ToLower() switch
        {
            "easy" => 600,
            "medium" => 900,
            "hard" => 1200,
            _ => 900
        };

        foreach (var q in questions)
        {
            q.Id = Guid.NewGuid();
            q.QuizId = quizId;

            var optionsList = new List<QuestionOption>();

            for (int i = 0; i < q.Options.Count; i++)
            {
                var opt = q.Options[i];
                var optId = Guid.NewGuid();
                opt.Id = optId;
                opt.QuestionId = q.Id;

                if (i == 0) // First option is the correct option in our seeder dataset
                {
                    q.CorrectOptionId = optId.ToString();
                }
                optionsList.Add(opt);
            }
        }

        return new Quiz
        {
            Id = quizId,
            Title = title,
            Category = category,
            CategoryName = categoryName,
            Description = description,
            Difficulty = difficulty,
            IconName = iconName,
            TimeLimitSeconds = timeLimit,
            IsCustom = true,
            CreatedAt = DateTime.UtcNow,
            Questions = questions
        };
    }
}
