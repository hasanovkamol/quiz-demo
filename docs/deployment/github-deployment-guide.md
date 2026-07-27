# GitHub Native Deployment Guide (GitHub Pages & GHCR)

Documentation of native deployment capabilities directly within GitHub (`.github/workflows/deploy.yml`).

---

## 🌟 GitHub Deployment Architecture

```
 ┌────────────────────────────────────────────────────────────────────────┐
 │                      GITHUB REPOSITORY (PUSH MAIN)                     │
 └───────────────────┬────────────────────────────────┬───────────────────┘
                     │                                │
                     ▼                                ▼
 ┌────────────────────────────────────┐ ┌─────────────────────────────────┐
 │    JOB 1: deploy-github-pages      │ │    JOB 2: publish-docker-ghcr   │
 │ - Builds Angular SPA (`ng build`)  │ │ - Authenticates with GHCR       │
 │ - Deploys to `gh-pages` branch     │ │ - Builds Docker Images          │
 │ - Live at GitHub Pages URL         │ │ - Publishes to ghcr.io Package  │
 └───────────────────┬────────────────┘ └────────────────┬────────────────┘
                     │                                   │
                     ▼                                   ▼
 ┌────────────────────────────────────┐ ┌─────────────────────────────────┐
 │          GITHUB PAGES URL          │ │   GITHUB CONTAINER REGISTRY     │
 │ https://hasanovkamol.github.io/... │ │ ghcr.io/hasanovkamol/quiz-...   │
 └────────────────────────────────────┘ └─────────────────────────────────┘
```

---

## 🚀 1. GitHub Pages Static Frontend Deployment

- **Automated Workflow**: Job `deploy-github-pages` builds the Angular SPA and pushes static assets to the `gh-pages` branch.
- **Live Access URL**: `https://hasanovkamol.github.io/quiz-demo/`
- **Activation Step**:
  1. Open Repository Settings -> **Pages**.
  2. Select Source: **Deploy from a branch**.
  3. Select Branch: **`gh-pages`** -> `/ (root)` -> Save.

---

## 🐳 2. GitHub Container Registry (GHCR - `ghcr.io`)

- **Automated Docker Image Publishing**: Job `publish-docker-ghcr` compiles and pushes Docker images to GitHub Packages:
  - `ghcr.io/hasanovkamol/quiz-demo/backend:latest`
  - `ghcr.io/hasanovkamol/quiz-demo/gateway:latest`
- **Production Server Deployment Command**:
  ```bash
  docker pull ghcr.io/hasanovkamol/quiz-demo/backend:latest
  docker compose up -d
  ```
