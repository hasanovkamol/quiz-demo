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
- `categories`: Array signal (`CategoryItem[]`) of dynamic system & custom categories.
- `activeCategory`: Category filter signal.
- `currentQuiz`: Active selected quiz signal.
- `currentQuestionIndex`: Active question pointer index.
- `userAnswers`: `Map<string, UserAnswer>` storing selected answers.
- `timerSecondsLeft`: Live countdown timer signal.
- `filteredQuizzes`: `computed()` signal deriving quizzes by active category.
- `progressPercentage`: `computed()` signal computing progress percentage (`Question X of Y`).
- `formattedTimer`: `computed()` signal formatting seconds into `MM:SS`.

---

## 🎨 Admin Portal & Features

1. **Category Management (`categories` tab)**: Admin creates new categories dynamically (`id`, `name`, `iconName`, `description`). New categories automatically appear in all filter pills and dropdowns across the application.
2. **AI Single Question & 1-Click Insert (`ai-single` tab)**: Generates 1 targeted AI question with options & explanation, presents a live preview card, and inserts directly into a selected Quiz with 1-click (`addQuestionToQuiz`).
3. **Quiz Creator Line-Item AI Suggestion**: In [`QuizCreatorComponent`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/quiz-creator/quiz-creator.component.ts), each question item features an `"⚡ AI Taklifi Bilan To'ldirish"` button to auto-fill questions on the fly.

