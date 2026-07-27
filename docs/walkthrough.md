# QuizMaster PRO: Full System Walkthrough & Developer Guide

Comprehensive system walkthrough and developer guide for the QuizMaster PRO Full-Stack application.

---

## 🌟 Executive Overview

QuizMaster PRO is an enterprise-grade, microservice-ready Full-Stack assessment platform built with **ASP.NET Core 9 Minimal APIs & Clean Architecture**, **Mobile Responsive Angular 18+ UI**, **Anti-Cheating Safeguards & Code Sandbox Engine**, **Infisical Secret & Config Management**, **Permission-Based Authorization (PBAC)**, **Keycloak Admin Panel Permission Management**, **PostgreSQL 16**, **Microsoft Semantic Kernel AI**, and **Nginx Gateway**.

### Key System Capabilities
1. **Full Mobile Responsiveness**: Responsive mobile layout across all components (`NavbarComponent` sliding mobile drawer, `QuizListComponent` touch-friendly grid, touch-friendly option cards, mobile code editor).
2. **Anti-Cheating Safeguards (Copy/Paste & Tab Switch Blocking)**: Intercepts `copy`, `cut`, `paste`, `contextmenu` (right click) events with violation warning modals. Tracks tab switches (`visibilitychange` / `blur`) up to **3 warnings** before automatic test finish.
3. **Interactive Code Execution Sandbox Component**: [`CodeEditorComponent`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/code-editor/code-editor.component.ts) featuring line numbers, dark editor container, paste blocking, and **"Kodni Tekshirish" (Run Code)** execution drawer.
4. **Infisical Centralized Secret & Config Management**: Integrated Infisical container cluster (`quiz_infisical_secrets`, `infisical_postgres_db`, `infisical_redis_cache`) in [`docker-compose.yml`](file:///home/user02/Projects/AI%20Projects/Qiuz/docker-compose.yml).
5. **Keycloak Admin Panel Permission Management**: Integrated [`keycloak/realm-export.json`](file:///home/user02/Projects/AI%20Projects/Qiuz/keycloak/realm-export.json) pre-configuring 8 granular permission roles.
6. **Permission-Based Authorization (PBAC / Claims)**: Defined 8 granular permission constants (`quizzes:read`, `quizzes:create`, `quizzes:delete`, `attempts:read`, `attempts:submit`, `ai:generate`, `admin:stats`, `users:manage`).
7. **GitHub Actions CI/CD Automated Workflow**: Integrated `.github/workflows/ci-cd.yml` pipeline (Node.js 22.x + .NET 9.x).
8. **Single Root Command Test Execution**: `npm run test` automatically executes both Angular UI tests (31/31 passed) and Backend Integration tests (7/7 passed).

---

## 🧪 Verification & Build Status

- **Mobile Responsiveness**: Verified mobile navigation drawer and responsive Tailwind layouts.
- **Root Full-Stack Test Command**: `npm run test` -> **Passed! (38/38 Total Tests Passed)**.
- **Frontend Unit & Component Tests**: `npm run test:ui` -> **Passed! (31/31 Passed, 12 Test Spec Files)**.
- **Backend Integration Tests**: `npm run test:backend` -> **Passed! (7/7 Passed, 0 Failed)**.
- **Backend Build**: `cd backend && dotnet build` -> **Build Succeeded (0 Errors, 0 Warnings)**.
- **Frontend Build**: `cd ui && npx ng build` -> **Bundle generation complete (0 Errors)**.
