# Angular Standalone Component Reference

Component directory structure and reference documentation with full Mobile Responsiveness.

---

## 🧱 Component Hierarchy & Mobile Responsiveness

### 1. `NavbarComponent` ([`navbar.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/navbar/navbar.component.ts))
- **Mobile Navigation Drawer**: Features a sliding mobile navigation menu for small screens (< 768px).
- Branding: `QuizMaster PRO` logo.
- User Name badge & modal trigger.
- Admin Panel toggle button.
- Score History badge counter.
- "Yangi Test" action button.

### 2. `QuizListComponent` ([`quiz-list.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/quiz-list/quiz-list.component.ts))
- Responsive Grid: `grid-cols-1 md:grid-cols-2 lg:grid-cols-3`.
- Scrollable horizontal category filter pills for mobile touch screens.
- Search input and category filtering.

### 3. `QuizPlayComponent` ([`quiz-play.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/quiz-play/quiz-play.component.ts))
- Mobile touch-friendly target areas (`min-h-[48px]`).
- Integrated Anti-Cheating warnings and Interactive Code Execution Sandbox.

### 4. `CodeEditorComponent` ([`code-editor.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/code-editor/code-editor.component.ts))
- Mobile responsive dark-mode code editor with output console drawer.

### 5. `AdminDashboardComponent` ([`admin-dashboard.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/admin/admin-dashboard.component.ts))
- Mobile scrollable attempts table and responsive AI question generator.

### 6. `ResultShareComponent` ([`result-share.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/result-share/result-share.component.ts))
- Mobile scorecard view for LAN sharing.

### 7. `UserModalComponent` ([`user-modal.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/user-modal/user-modal.component.ts))
- Responsive modal dialog for name entry and Google OAuth sign-in.
