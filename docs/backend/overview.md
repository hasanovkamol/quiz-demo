# Backend Overview: ASP.NET Core 10 Minimal APIs & Clean Architecture

Detailed breakdown of the ASP.NET Core 10 Web API architecture.

---

## 📐 Clean Architecture & .NET 10.0 Target Framework

```
backend/
├── Core/
│   ├── Domain/               # Entities (Quiz, Question, User, QuizAttempt)
│   └── Application/          # DTOs & Interfaces (IAuthService, ISemanticKernelQuizService)
├── Infrastructure/           # Persistence (QuizDbContext, DbInitializer), AI, Identity
└── Endpoints/                     # Minimal API Endpoint Group Mappers
```

- **Target Framework**: `.NET 10.0` (`<TargetFramework>net10.0</TargetFramework>`).
- **Docker Image**: `mcr.microsoft.com/dotnet/sdk:10.0` and `mcr.microsoft.com/dotnet/aspnet:10.0`.

---

## 🔌 Minimal API Endpoints Summary

| Endpoint | Method | Access | Description |
|---|---|---|---|
| `/api/quizzes` | `GET` | Public | List all available quizzes with questions & options |
| `/api/quizzes/{id}` | `GET` | Public | Get single quiz by GUID |
| `/api/quizzes` | `POST` | User/Admin | Create a new custom quiz |
| `/api/quizzes/{id}` | `DELETE` | Admin | Delete a quiz by GUID |
| `/api/quizattempts` | `GET` | Admin | List all user quiz attempt records |
| `/api/quizattempts/{id}` | `GET` | Public | Get attempt scorecard by GUID (for LAN share link) |
| `/api/quizattempts` | `POST` | Public | Submit a completed quiz attempt |
| `/api/admin/generate-ai-quiz` | `POST` | Admin | Trigger Semantic Kernel full AI quiz generation |
| `/api/admin/generate-ai-question` | `POST` | Admin | Generate single AI question & options |
| `/api/admin/categories` | `GET` | Public/Admin | List all system & custom categories |
| `/api/admin/categories` | `POST` | Admin | Create a new custom category |
| `/api/admin/quizzes/{quizId}/questions` | `POST` | Admin | Direct 1-Click insert of a question into a quiz |
| `/api/admin/stats` | `GET` | Admin | Get system metrics (total quizzes, attempts, avg score) |
| `/api/auth/google-login` | `POST` | Public | Authenticate with Google ID Token & issue 5-min JWT |
| `/api/auth/refresh` | `POST` | Public | Refresh 5-minute Access Token using Refresh Token |
| `/api/auth/me` | `GET` | `[Authorize]` | Fetch profile of authenticated user |

