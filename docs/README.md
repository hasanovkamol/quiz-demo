# QuizMaster PRO Master Documentation Index

Welcome to the official modular documentation for the **QuizMaster PRO** Full-Stack application.

---

## 📑 Master Documentation Directory Map

### 1. 🚀 General System & Developer Overview
- 📄 **[walkthrough.md](./walkthrough.md)**: Master System Walkthrough & Developer Overview

---

### 2. ⚙️ Backend Services (`./docs/backend/`)
- 📄 **[backend/overview.md](./backend/overview.md)**: ASP.NET Core 10 Minimal APIs & Clean Architecture
- 📄 **[backend/permissions-architecture.md](./backend/permissions-architecture.md)**: Permission-Based Authorization (PBAC / Claims) Architecture
- 📄 **[backend/keycloak-permission-management.md](./backend/keycloak-permission-management.md)**: Keycloak Admin Panel Permission Access/Reject Management Guide
- 📄 **[backend/database-schema.md](./backend/database-schema.md)**: EF Core PostgreSQL Schema & Entity Models
- 📄 **[backend/ai-semantic-kernel.md](./backend/ai-semantic-kernel.md)**: Microsoft Semantic Kernel AI Question Generator
- 📄 **[backend/auth-and-keycloak.md](./backend/auth-and-keycloak.md)**: Keycloak OIDC & 5-Minute Access Token Expiration
- 📄 **[backend/integration-tests.md](./backend/integration-tests.md)**: xUnit Integration Test Suite (7/7 Passed)

---

### 3. 🎨 Frontend Single Page Application (`./docs/ui/`)
- 📄 **[ui/overview.md](./ui/overview.md)**: Angular 18+ Architecture & Signals Reactivity
- 📄 **[ui/anti-cheating-and-sandbox.md](./ui/anti-cheating-and-sandbox.md)**: Anti-Cheating Safeguards (Copy/Paste & Tab Switch Protection) & Interactive Code Sandbox
- 📄 **[ui/components.md](./ui/components.md)**: Standalone Component Library Reference
- 📄 **[ui/refresh-token-flow.md](./ui/refresh-token-flow.md)**: 5-Minute Silent Background Token Refresh Timer
- 📄 **[ui/unit-tests.md](./ui/unit-tests.md)**: Jasmine/Karma Component & Service Spec Suites (31/31 Passed across 12 files)

---

### 4. 🐳 Infrastructure & Operations (`./docs/deployment/`)
- 📄 **[deployment/github-deployment-guide.md](./deployment/github-deployment-guide.md)**: GitHub Native Deployment Guide (GitHub Pages & GHCR)
- 📄 **[deployment/infisical-config-management.md](./deployment/infisical-config-management.md)**: Infisical Centralized Secret & Configuration Management Guide
- 📄 **[deployment/ci-cd-pipeline.md](./deployment/ci-cd-pipeline.md)**: GitHub Actions Automated CI/CD Workflow
- 📄 **[deployment/docker-and-gateway.md](./deployment/docker-and-gateway.md)**: Multi-Container Docker Compose Setup
- 📄 **[deployment/quiz-net-topology.md](./deployment/quiz-net-topology.md)**: `quiz-net` Isolated Bridge Network Topology
- 📄 **[deployment/lan-sharing-guide.md](./deployment/lan-sharing-guide.md)**: Local Network (LAN) Wi-Fi Access Guide

---

### 5. 🧠 Question Data Refinement & AI Optimization (`./docs/`)
- 📄 **[question-refinement-log.md](./question-refinement-log.md)**: AI Question Batch Refinement, Correct Option Fixes & Code Snippet Enrichment Log
- 📄 **[graphify-optimization.md](./graphify-optimization.md)**: Graphify AST Knowledge Graph & LLM Token Reduction Indexing

---

## 🚀 Quick Execution Commands

### Run Full-Stack Test Suite (40/40 Passed)
```bash
npm run test
```

### Launch Multi-Container Infrastructure
```bash
docker compose up --build
```
Open `http://localhost` in your browser.
