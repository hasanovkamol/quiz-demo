# Anti-Cheating Safeguards & Interactive Code Sandbox

Documentation of anti-cheating protection and code sandbox execution engine in QuizMaster PRO.

---

## 🛡 Anti-Cheating Safeguards (`QuizPlayComponent`)

Located in [`ui/src/app/components/quiz-play/quiz-play.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/quiz-play/quiz-play.component.ts):

### 1. Copy, Cut & Right-Click Interception
- Intercepts `document:copy`, `document:cut`, and `document:contextmenu` (right click) events.
- Displays an animated warning toast modal: *"Haqqoniy baholash uchun savol matnini nusxalash (Copy) taqiqlangan!"*.

### 2. Tab Switching & Focus Loss Tracking
- Monitors `window:visibilitychange` and `window:blur` events during active test attempts.
- Displays a warning banner: *"Diqqat! Test vaqtida brauzer oynasini almashtirish taqiqlangan. Ogohlantirish: X / 3"*.
- Reaching **3 warnings** automatically finishes the test attempt and flags it for review.

---

## 💻 Interactive Code Execution Sandbox (`CodeEditorComponent`)

Located in [`ui/src/app/components/code-editor/code-editor.component.ts`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/code-editor/code-editor.component.ts):

### Features:
1. **Glassmorphic Editor View**: Line numbers, tab indentation handling, and syntax container.
2. **Paste Interception**: Attempts to paste code from external clipboard triggers paste warning toast: *"Test davomida kodingizni tashqaridan nusxalash/qo'yish (Paste) mumkin emas! Kodingizni qo'lda yozing."*.
3. **"Kodni Tekshirish" (Run Code)**: Executes live JavaScript / TypeScript snippets against expected test case outputs and prints console logs in the Console drawer.
