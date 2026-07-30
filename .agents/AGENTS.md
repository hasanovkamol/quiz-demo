# QuizMaster PRO Project Rules & Auto-Documentation Configuration

## 📝 Automatic Documentation Requirement (`./docs/`)

- **Mandatory Documentation Sync**:
  - Whenever any task, feature request, bug fix, architectural modification, or refactoring is executed in this project, you MUST automatically record and update the corresponding documentation files inside the `./docs/` directory.
  - Always maintain and update the following core documentation files:
    - `docs/README.md`: Master index of all project documentation.
    - `docs/walkthrough.md`: Overall project walkthrough and developer guide.
    - `docs/backend-documentation.md`: ASP.NET Core API, EF Core PostgreSQL schema, Semantic Kernel AI, and endpoints.
    - `docs/ui-documentation.md`: Angular 18+ SPA, Signals reactivity, components, and Tailwind styling.
    - `docs/architecture-and-deployment.md`: Docker Compose multi-container topology and LAN deployment guides.
  - If a new feature or module is created, generate a dedicated `docs/<feature-name>.md` file and register it in `docs/README.md`.

## 🕸️ Graphify Token Optimization & Knowledge Graph Rule

- **Prioritize Graphify Navigation**:
  - AI agents MUST consult `graphify-out/GRAPH_REPORT.md` and `graphify-out/graph.json` before initiating brute-force file scans or broad search queries.
  - After completing major structural code edits, run `~/.local/bin/graphify . --code-only && ~/.local/bin/graphify cluster-only .` to keep the AST graph synchronized.
