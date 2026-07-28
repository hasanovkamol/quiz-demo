# QuizMaster PRO: Full System Walkthrough & Developer Guide

Comprehensive system walkthrough and developer guide for the QuizMaster PRO Full-Stack application.

---

## 🌟 Executive Overview

QuizMaster PRO is an enterprise-grade, microservice-ready Full-Stack assessment platform built with **ASP.NET Core 10 Minimal APIs & Clean Architecture**, **Dedicated User & Admin UI Portals**, **GitHub Pages & GHCR Native Deployment**, **Mobile Responsive Angular 18+ UI**, **Anti-Cheating Safeguards & Code Sandbox Engine**, **Infisical Secret & Config Management**, **Permission-Based Authorization (PBAC)**, **Keycloak Admin Panel Permission Management**, **PostgreSQL 16**, **Microsoft Semantic Kernel AI**, and **Nginx Gateway**.

### Key System Capabilities
1. **Dedicated User vs Admin UI Portals**: Clean role segregation between User Mode (solving quizzes, scorecards, history) and Admin Console (quiz builder, AI generator, user attempt analytics). Role switcher (`[ 🎓 User Mode ]` vs `[ ⚙️ Admin Console ]`) in `NavbarComponent`.
2. **ASP.NET Core 10 Target Framework**: Web API and Integration Test projects target `.NET 10.0` (`<TargetFramework>net10.0</TargetFramework>`) with `mcr.microsoft.com/dotnet/sdk:10.0` Docker images.
3. **Full Mobile Responsiveness**: Responsive mobile layout across all components (`NavbarComponent` sliding mobile drawer, `QuizListComponent` touch-friendly grid, touch-friendly option cards, mobile code editor).
4. **Anti-Cheating Safeguards (Copy/Paste & Tab Switch Blocking)**: Intercepts `copy`, `cut`, `paste`, `contextmenu` (right click) events with violation warning modals. Tracks tab switches (`visibilitychange` / `blur`) up to **3 warnings** before automatic test finish.
5. **Interactive Code Execution Sandbox Component**: [`CodeEditorComponent`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/src/app/components/code-editor/code-editor.component.ts) featuring line numbers, dark editor container, paste blocking, and **"Kodni Tekshirish" (Run Code)** execution drawer.
6. **Infisical Centralized Secret & Config Management**: Integrated Infisical container cluster (`quiz_infisical_secrets`, `infisical_postgres_db`, `infisical_redis_cache`) in [`docker-compose.yml`](file:///home/user02/Projects/AI%20Projects/Qiuz/docker-compose.yml).
7. **Google OAuth 2.0 GIS & User Profile Sign-Out**: Real Google Identity Services (GIS) One Tap integration, User Profile avatar & role display, explicit Sign-Out in `NavbarComponent`.
8. **Nginx API Reverse Proxy for UI**: Dedicated [`ui/nginx.conf`](file:///home/user02/Projects/AI%20Projects/Qiuz/ui/nginx.conf) with `/api/` proxying to `http://backend:5000/api/` enabling both port `8081` and Gateway port `80` access.
9. **EF Core Model Relationships & Migration**: Resolved `FK_QuestionOptions_Questions_QuestionId1` shadow property bug via explicit `.WithOne(o => o.Question)` and `.WithOne(u => u.Attempt)` EF Core mappings and migration `FixShadowPropertiesFK`.
10. **Single Root Command Test Execution**: `npm run test` automatically executes both Angular UI tests (31/31 passed) and Backend Integration tests (7/7 passed).
11. **Senior ASP.NET Core 100 ABCD Quiz Seeder**: [`Seeder_SeniorAspNetCore.cs`](file:///home/user02/Projects/AI%20Projects/Qiuz/backend/Infrastructure/Persistence/Seeding/Seeder_SeniorAspNetCore.cs) — 100 ta senior darajadagi ABCD test savoli 8 ta bo'limga bo'lingan (C# Asoslari, ASP.NET Core, Web API, EF Core, Logging, Xavfsizlik, Arxitektura, Testing/DevOps).

---

## 🧪 Verification & Build Status

- **Role Portal Segregation**: User Mode and Admin Console UI modes verified with clean switcher.
- **Root Full-Stack Test Command**: `npm run test` -> **Passed! (38/38 Total Tests Passed)**.
- **Frontend Unit & Component Tests**: `npm run test:ui` -> **Passed! (31/31 Passed, 12 Test Spec Files)**.
- **Backend Integration Tests**: `npm run test:backend` -> **Passed! (7/7 Passed, 0 Failed)**.
- **Backend Build**: `cd backend && dotnet build` -> **Build Succeeded (0 Errors, 0 Warnings)**.
- **Frontend Build**: `cd ui && npx ng build` -> **Bundle generation complete (0 Errors)**.
