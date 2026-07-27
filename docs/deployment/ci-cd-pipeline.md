# GitHub Actions CI/CD Pipeline Guide

Documentation of the automated Continuous Integration and Continuous Deployment (CI/CD) workflow.

---

## ⚙️ GitHub Actions Workflow (`.github/workflows/ci-cd.yml`)

The CI/CD pipeline triggers automatically on any `push` or `pull_request` to `main` or `master` branches.

```
 ┌────────────────────────────────────────────────────────────────────────┐
 │                           GITHUB ACTIONS TRIGGER                       │
 │                    (Push or PR to main / master)                       │
 └───────────────────┬────────────────────────────────┬───────────────────┘
                     │                                │
                     ▼                                ▼
 ┌────────────────────────────────────┐ ┌─────────────────────────────────┐
 │        JOB 1: backend-ci           │ │       JOB 2: frontend-ci        │
 │ - Setup .NET 9 SDK                 │ │ - Setup Node.js 20.x            │
 │ - Restore & Build Web API          │ │ - Install npm packages          │
 │ - Run Integration Tests (7/7 Pass) │ │ - Run Unit Tests (30/30 Pass)   │
 │                                    │ │ - Build Angular Production Bundle│
 └───────────────────┬────────────────┘ └────────────────┬────────────────┘
                     │                                   │
                     └─────────────────┬─────────────────┘
                                       │ (Needs Job 1 & Job 2)
                                       ▼
                     ┌───────────────────────────────────┐
                     │   JOB 3: docker-verification      │
                     │ - Validate docker-compose config  │
                     │ - Build all multi-container images│
                     └───────────────────────────────────┘
```

---

## 🚀 Pipeline Execution Jobs

1. **`backend-ci`**: Restores NuGet dependencies, builds C# Web API, and executes all 7 `xUnit` integration tests against `InMemoryDatabase`.
2. **`frontend-ci`**: Installs npm dependencies, runs all 30 Angular Karma/Jasmine unit tests, and verifies `ng build` production bundle generation.
3. **`docker-verification`**: Ensures `docker-compose.yml` syntax integrity and builds container images (`db`, `keycloak`, `backend`, `ui`, `gateway`).
