# QuizMaster PRO: Full System Walkthrough & Developer Guide

Comprehensive system walkthrough and developer guide for the QuizMaster PRO Full-Stack application.

---

## 🌟 Executive Overview

QuizMaster PRO is an enterprise-grade, microservice-ready Full-Stack assessment platform built with **ASP.NET Core 9 Minimal APIs & Clean Architecture**, **Anti-Cheating Safeguards & Code Sandbox Engine**, **Infisical Secret & Config Management**, **Permission-Based Authorization (PBAC)**, **Keycloak Admin Panel Permission Management**, **Angular 18+**, **PostgreSQL 16**, **Microsoft Semantic Kernel AI**, and **Nginx Gateway**.

### Key System Capabilities
1. **Anti-Cheating Safeguards (Copy/Paste & Tab Switch Blocking)**: Intercepts `copy`, `cut`, `paste`, `contextmenu` (right click) events with violation warning modals. Tracks tab switches (`visibilitychange` / `blur`) up to **3 warnings** before automatic test finish.
2. **Interactive Code Execution Sandbox Component**: [`CodeEditorComponent`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/code-editor/code-editor.component.ts) featuring line numbers, dark editor container, paste blocking, and **"Kodni Tekshirish" (Run Code)** execution drawer.
3. **Infisical Centralized Secret & Config Management**: Integrated Infisical container cluster (`quiz_infisical_secrets`, `infisical_postgres_db`, `infisical_redis_cache`) in [`docker-compose.yml`](file:///home/user02/Projects/AI%20Projects/Qiuz/docker-compose.yml). Backend [`Dockerfile`](file:///home/user02/Projects/AI%20Projects/Qiuz/backend/Dockerfile) uses `infisical run -- dotnet QuizApi.dll` to inject connection strings, JWT keys, and AI keys dynamically.
4. **Keycloak Admin Panel Permission Management**: Integrated [`keycloak/realm-export.json`](file:///home/user02/Projects/AI%20Projects/Qiuz/keycloak/realm-export.json) pre-configuring 8 granular permission roles.
5. **Permission-Based Authorization (PBAC / Claims)**: Defined 8 granular permission constants (`quizzes:read`, `quizzes:create`, `quizzes:delete`, `attempts:read`, `attempts:submit`, `ai:generate`, `admin:stats`, `users:manage`).
6. **GitHub Actions CI/CD Automated Workflow**: Integrated `.github/workflows/ci-cd.yml` pipeline (Node.js 22.x + .NET 9.x).
7. **Single Root Command Test Execution**: `npm run test` automatically executes both Angular UI tests (31/31 passed) and Backend Integration tests (7/7 passed).

---

## 🧪 Verification & Build Status

- **Anti-Cheating & Code Sandbox**: `CodeEditorComponent` and event handlers verified with 100% test pass rate.
- **Root Full-Stack Test Command**: `npm run test` -> **Passed! (38/38 Total Tests Passed)**.
- **Frontend Unit & Component Tests**: `npm run test:ui` -> **Passed! (31/31 Passed, 12 Test Spec Files)**.
- **Backend Integration Tests**: `npm run test:backend` -> **Passed! (7/7 Passed, 0 Failed)**.
- **Backend Build**: `cd backend && dotnet build` -> **Build Succeeded (0 Errors, 0 Warnings)**.
- **Frontend Build**: `cd ui && npx ng build` -> **Bundle generation complete (0 Errors)**.
