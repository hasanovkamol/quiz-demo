# Angular UI Unit & Component Test Suite

Documentation of Angular Jasmine/Karma and Vitest component & service test configuration.

---

## ⚙️ Karma & Jasmine Test Setup (`karma.conf.js`)

The UI project supports standard Angular Karma & Jasmine test execution ([`karma.conf.js`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/karma.conf.js)):

- **Frameworks**: `jasmine`
- **Plugins**: `karma-jasmine`, `karma-chrome-launcher`, `karma-jasmine-html-reporter`, `karma-coverage`
- **Browsers**: `ChromeHeadless`

---

## 🧪 Test Coverage (30/30 Passed across 12 Spec Files)

### Test Execution Commands
```bash
cd ui && npm run test          # Standard single-run test execution
cd ui && npm run test:karma    # Karma test execution
```

### Spec File Breakdown:
1. `app.spec.ts`: Root component creation and hero heading title.
2. `auth.service.spec.ts`: Google OAuth login, Signals state, 5-minute silent refresh, and logout.
3. `quiz.service.spec.ts`: Category filtering, formatted timer calculation (`MM:SS`), and quiz start state.
4. `quiz-api.service.spec.ts`: REST API HTTP request handling (`GET /api/quizzes`, `POST /api/quizattempts`).
5. `auth.interceptor.spec.ts`: HTTP Interceptor JWT `Authorization: Bearer <TOKEN>` header injection.
6. `navbar.component.spec.ts`: Navbar branding and navigation click events.
7. `quiz-list.component.spec.ts`: Category pills and search filtering.
8. `quiz-play.component.spec.ts`: Quiz option selection and exit confirmation modal state.
9. `quiz-result.component.spec.ts`: Quiz result scorecard view.
10. `admin-dashboard.component.spec.ts`: Admin dashboard tab switching ('ai-generator' vs 'attempts').
11. `result-share.component.spec.ts`: LAN scorecard share view (`ActivatedRoute`).
12. `user-modal.component.spec.ts`: User name modal validation and submission.
