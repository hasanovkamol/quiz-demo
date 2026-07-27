# Angular Standalone Component Reference

Component directory structure and reference documentation with Role Portal Segregation (User Mode vs Admin Console).

---

## 🧱 Role Portal Segregation (User Mode vs Admin Console)

### 1. `NavbarComponent` ([`navbar.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/navbar/navbar.component.ts))
- **Role Switcher**: Features a role toggle pill button (`[ 🎓 User Mode ]` vs `[ ⚙️ Admin Console ]`).
- **User Mode View**: Hides administrative triggers, showing only user profile badge, test history button, and test catalog shortcuts.
- **Admin Console View**: Shows Admin Portal badge, "Yangi Test Yaratish" modal launcher, and system management shortcuts.

### 2. `QuizListComponent` ([`quiz-list.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/quiz-list/quiz-list.component.ts))
- **User Mode Catalog**: Dedicated for test takers to browse, search, and initiate tests across categories.

### 3. `QuizPlayComponent` ([`quiz-play.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/quiz-play/quiz-play.component.ts))
- Dedicated test solver environment with anti-cheating protections and code execution sandbox.

### 4. `AdminDashboardComponent` ([`admin-dashboard.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/admin/admin-dashboard.component.ts))
- Dedicated Admin Console for managing user attempts, viewing analytics stats, generating Semantic Kernel AI quizzes, and creating custom quizzes.
