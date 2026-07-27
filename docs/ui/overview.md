# Angular Single Page Application UI Overview

Detailed breakdown of Angular 18+ architecture, Signals state, and Tailwind CSS design system.

---

## ⚡ Reactivity & State Management (Angular Signals)

The application utilizes Angular's latest **Signals** reactivity model (`signal`, `computed`):

### Core Signals in [`AuthService`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/services/auth.service.ts)
- `currentUser`: `User | null` signal representing logged-in profile.
- `token`: `string | null` signal representing active 5-minute JWT Access Token.
- `refreshToken`: `string | null` signal representing active Refresh Token.

### Core Signals in [`QuizService`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/services/quiz.service.ts)
- `currentUserName`: User full name string signal.
- `quizzes`: Array signal of available quizzes.
- `activeCategory`: Category filter signal ('all' | 'angular' | 'dotnet' | 'webdev' | 'custom').
- `currentQuiz`: Active selected quiz signal.
- `currentQuestionIndex`: Active question pointer index.
- `userAnswers`: `Map<string, UserAnswer>` storing selected answers.
- `timerSecondsLeft`: Live countdown timer signal.
- `filteredQuizzes`: `computed()` signal deriving quizzes by active category.
- `progressPercentage`: `computed()` signal computing progress percentage (`Question X of Y`).
- `formattedTimer`: `computed()` signal formatting seconds into `MM:SS`.
