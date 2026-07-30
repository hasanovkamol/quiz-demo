---
name: graphify-navigation
description: Drastically reduce token usage when planning, searching, or debugging QuizMaster PRO by consulting the Graphify knowledge graph in graphify-out/GRAPH_REPORT.md and graphify-out/graph.json.
---

# 🕸️ Graphify Knowledge Graph Agent Skill

This skill instructs AI agents (Gemini, Claude, Antigravity, Cursor) to leverage the pre-indexed **Graphify Knowledge Graph** for ultra-efficient token navigation.

---

## 📌 Agent Workflow Rules

### 1. 🔍 Consult Graph Before Searching Code
Before reading raw source files or using wide `grep` searches:
* Inspect `graphify-out/GRAPH_REPORT.md` to identify relevant **Community Hubs** and **God Nodes** (`Question`, `QuizService`, `AuthService`, `TelegramBotService`).
* Use `graphify-out/graph.json` to trace exact module dependencies and function call trees.

### 2. ⚡ Keep Graph Updated
After adding or modifying files/services:
```bash
~/.local/bin/graphify . --code-only
~/.local/bin/graphify cluster-only .
```
*(No LLM API tokens consumed for AST code updates).*

### 3. 🎯 High-Value Token Savings
* **Architecture Mapping:** Query graph edges to locate where services connect.
* **Refactoring & Impact Analysis:** Check incoming/outgoing edges before changing core abstractions.
