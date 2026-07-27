using QuizApi.Core.Application.Dtos;
using QuizApi.Core.Domain.Entities;

namespace QuizApi.Core.Application.Interfaces;

public interface ISemanticKernelQuizService
{
    Task<Quiz> GenerateQuizAsync(AiQuizGenerationRequest request);
}
