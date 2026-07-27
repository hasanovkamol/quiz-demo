# Microsoft Semantic Kernel AI Quiz Generator

Documentation of the AI question generator powering QuizMaster PRO.

---

## 🤖 Overview

The [`SemanticKernelQuizService`](file:///home/user02/Projects/AI%20Projects/Qiuz/backend/Infrastructure/Ai/SemanticKernelQuizService.cs) leverages Microsoft Semantic Kernel to dynamically generate technical quizzes based on user prompts.

### Supported LLM Providers
- Google Gemini 1.5 Flash (`Microsoft.SemanticKernel.Connectors.Google`)
- OpenAI GPT-4 / GPT-3.5 Turbo

### Request DTO
```csharp
public record AiQuizGenerationRequest(
    string Topic,
    string Category,
    string Difficulty,
    int QuestionCount,
    int TimeLimitMinutes,
    string? ApiKey
);
```

### JSON Response Schema Contract
```json
{
  "title": "Topic Quiz Title",
  "description": "Short overview description",
  "questions": [
    {
      "text": "Question text in Uzbek",
      "codeSnippet": "Optional formatted code snippet",
      "options": ["Variant A", "Variant B", "Variant C", "Variant D"],
      "correctIndex": 0,
      "explanation": "Detailed explanation of correct answer"
    }
  ]
}
```
