# Angular Standalone Component Reference

Component directory structure and reference documentation.

---

## 🧱 Component Hierarchy

### 1. `NavbarComponent` ([`navbar.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/navbar/navbar.component.ts))
- Branding: `QuizMaster PRO` logo.
- User Name badge & modal trigger.
- Admin Panel toggle button.
- Score History badge counter.
- "Yangi Test" action button.

### 2. `QuizListComponent` ([`quiz-list.component.spec.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/quiz-list/quiz-list.component.spec.ts))
- Hero Banner with real-time search input.
- "Qanday Ishlaydi?" (How it works) 3-step quick guide cards.
- Category filter pills.
- Quiz Cards grid showing difficulty badges (Oson: Emerald, O'rta: Amber, Qiyin: Rose), question count, time limit, and "Testni Boshlash" button.

### 3. `QuizPlayComponent` ([`quiz-play.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/quiz-play/quiz-play.component.ts))
- Active quiz screen with step indicator buttons highlighting answered questions in green.
- Live countdown timer turning red when < 60s remain.
- Question card rendering formatted code snippets in editor style.
- Option cards with A, B, C, D badges and selected checkmark indicator.

### 4. `QuizResultComponent` ([`quiz-result.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/quiz-result/quiz-result.component.ts))
- Circular SVG progress indicator displaying percentage score.
- Grade verdict badge ("A'lo daraja!", "Barakalla!", etc.).
- Question-by-question review section with explanations for each answer.

### 5. `AdminDashboardComponent` ([`admin-dashboard.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/admin/admin-dashboard.component.ts))
- System metrics cards (Total attempts, Unique users, Avg score, Total quizzes).
- User Attempts table displaying `UserName`, `QuizTitle`, `CategoryName`, `ScorePercentage`, `CompletedAt`.
- Semantic Kernel AI Question Generator tab.

### 6. `ResultShareComponent` ([`result-share.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/result-share/result-share.component.ts))
- Dedicated route view `/result/:id` displaying attempt scorecards for local network sharing.

### 7. `UserModalComponent` ([`user-modal.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/user-modal/user-modal.component.ts))
- Prompting modal for `Ism va Familiya` before starting quiz & Google OAuth Sign-In.
