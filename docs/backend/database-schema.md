# PostgreSQL Database Schema & EF Core Migrations

Documentation of database entities and EF Core Migrations structure (`Infrastructure/Persistence/Migrations`).

---

## 🏗 EF Core Migrations Location

In Clean Architecture, EF Core Migrations belong to the **Infrastructure Layer** (`backend/Infrastructure/Persistence/Migrations/`):

```
backend/Infrastructure/Persistence/Migrations/
├── 20260727062501_InitialCreate.cs
├── 20260727062501_InitialCreate.Designer.cs
└── QuizDbContextModelSnapshot.cs
```

### Migration Management Command
```bash
dotnet ef migrations add <MigrationName> --output-dir Infrastructure/Persistence/Migrations
```

---

## 🗄 Database Entities & Schema

### 1. `Quiz`
- `Guid Id`: Primary Key.
- `string Title`: Name of the quiz.
- `string Category`: Slug ('angular', 'dotnet', 'webdev', 'custom').
- `string CategoryName`: Human-readable category label.
- `string Description`: Detailed description.
- `string Difficulty`: 'Oson' | 'O\'rta' | 'Qiyin'.
- `int TimeLimitSeconds`: Countdown timer duration in seconds.
- `bool IsCustom`: True if created manually or by AI.
- `List<Question> Questions`: Navigation property.

### 2. `Question`
- `Guid Id`: Primary Key.
- `Guid QuizId`: Foreign Key to `Quiz`.
- `string Text`: Question content.
- `string? CodeSnippet`: Optional code snippet to render in code editor view.
- `string CorrectOptionId`: Option ID matching the correct answer.
- `string Explanation`: Detailed explanation for the answer.
- `List<QuestionOption> Options`: Navigation property.

### 3. `QuestionOption`
- `Guid Id`: Primary Key.
- `Guid QuestionId`: Foreign Key to `Question`.
- `string Text`: Option text (Variant A, B, C, D).

### 4. `QuizAttempt`
- `Guid Id`: Primary Key.
- `Guid QuizId`: Foreign Key to `Quiz`.
- `string QuizTitle`: Saved title snapshot.
- `string CategoryName`: Saved category snapshot.
- `string UserName`: Full name of the user.
- `int TotalQuestions`: Total questions count.
- `int CorrectAnswersCount`: Correctly answered count.
- `double ScorePercentage`: Percentage score (0 - 100).
- `int TotalTimeSpentSeconds`: Total time taken.
- `DateTime CompletedAt`: Timestamp.
- `List<UserAnswer> UserAnswers`: Navigation property.

### 5. `User`
- `Guid Id`: Primary Key.
- `string? GoogleId`: Subject ID from Google OAuth / Keycloak.
- `string Email`: Unique email address.
- `string Name`: Display name.
- `string? PictureUrl`: Avatar URL.
- `string Role`: "User" | "Admin".
- `DateTime CreatedAt` & `LastLoginAt`: Activity timestamps.
