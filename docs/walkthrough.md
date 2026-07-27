# QuizMaster PRO: Full System Walkthrough & Developer Guide

Comprehensive system walkthrough and developer guide for the QuizMaster PRO Full-Stack application.

---

## 🌟 Executive Overview

QuizMaster PRO is an enterprise-grade, microservice-ready Full-Stack assessment platform built with **ASP.NET Core 9 Minimal APIs & Clean Architecture**, **Permission-Based Authorization (PBAC)**, **Keycloak Admin Panel Permission Management**, **Angular 18+**, **PostgreSQL 16**, **Microsoft Semantic Kernel AI**, and **Nginx Gateway**.

### Key System Capabilities
1. **Keycloak Admin Panel Permission Management**: Integrated [`keycloak/realm-export.json`](file:///home/user02/Projects/AI%20Projects/Qiuz/keycloak/realm-export.json) pre-configuring 8 granular permission roles. Keycloak Admin UI (`http://localhost:8080`) allows granting/revoking user access dynamically with live token claim mapping.
2. **Permission-Based Authorization (PBAC / Claims)**: Defined 8 granular permission constants (`quizzes:read`, `quizzes:create`, `quizzes:delete`, `attempts:read`, `attempts:submit`, `ai:generate`, `admin:stats`, `users:manage`) mapped dynamically to JWT claims and evaluated via [`PermissionAuthorizationHandler`](file:///home/user02/Projects/AI%20Projects/Qiuz/backend/Infrastructure/Identity/PermissionAuthorizationHandler.cs).
3. **GitHub Actions CI/CD Automated Workflow**: Integrated `.github/workflows/ci-cd.yml` pipeline that automatically validates, tests (37/37 tests), builds, and verifies Docker containers on every git push or pull request.
4. **Karma & Jasmine Angular Testing**: Installed and configured `karma`, `karma-jasmine`, `karma-chrome-launcher`, `karma-coverage`, `jasmine-core`, and `@types/jasmine` ([`ui/karma.conf.js`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/karma.conf.js)).
5. **EF Core Migrations Architecture**: Migrations are cleanly integrated into the Infrastructure layer ([`backend/Infrastructure/Persistence/Migrations/`](file:///home/user02/Projects/AI%20Projects/Qiuz/backend/Infrastructure/Persistence/Migrations/)) with `InitialCreate` migration generated.
6. **Single Root Command Test Execution**: `npm run test` automatically executes both Angular UI tests (30/30 passed) and Backend Integration tests (7/7 passed).
7. **Clean Architecture + Minimal APIs**: Low memory overhead, fast startup, TypedResults HTTP responses, and strict layer separation (`Core/Domain`, `Core/Application`, `Infrastructure`, `Endpoints`).
8. **Docker Compose & Gateway Deployment**: Multi-container setup (`db`, `keycloak`, `backend`, `ui`, `gateway`) exposing port 80 to Wi-Fi/LAN networks.

---

## 🧪 Verification & Build Status

- **Keycloak Admin Permission Management**: Configured in `keycloak/realm-export.json` and backend handler.
- **Root Full-Stack Test Command**: `npm run test` -> **Passed! (37/37 Total Tests Passed)**.
- **Frontend Unit & Component Tests**: `npm run test:ui` -> **Passed! (30/30 Passed, 12 Test Spec Files)**.
- **Backend Integration Tests**: `npm run test:backend` -> **Passed! (7/7 Passed, 0 Failed)**.
- **Backend Build**: `cd backend && dotnet build` -> **Build Succeeded (0 Errors, 0 Warnings)**.
- **Frontend Build**: `cd ui && npx ng build` -> **Bundle generation complete (0 Errors)**.
