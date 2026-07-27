# Backend Overview: Clean Architecture & Minimal APIs

Detailed breakdown of the ASP.NET Core 9 Web API architecture.

---

## 📐 Clean Architecture Layering

```
backend/
├── Core/
│   ├── Domain/               # Entities (Quiz, Question, User, QuizAttempt)
│   └── Application/          # DTOs & Interfaces (IAuthService, ISemanticKernelQuizService)
├── Infrastructure/           # Persistence (QuizDbContext, DbInitializer), AI, Identity
└── Endpoints/                     # Minimal API Endpoint Group Mappers
```

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
| `/api/admin/generate-ai-quiz` | `POST` | Admin | Trigger Semantic Kernel AI question generation |
| `/api/admin/stats` | `GET` | Admin | Get system metrics (total quizzes, attempts, avg score) |
| `/api/auth/google-login` | `POST` | Public | Authenticate with Google ID Token & issue 5-min JWT |
| `/api/auth/refresh` | `POST` | Public | Refresh 5-minute Access Token using Refresh Token |
| `/api/auth/me` | `GET` | `[Authorize]` | Fetch profile of authenticated user |
