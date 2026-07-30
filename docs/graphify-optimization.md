# 🕸️ Graphify Token Optimization Guide

Graphify transforms the entire **QuizMaster PRO** codebase into a queryable knowledge graph using local Abstract Syntax Tree (AST) parsing with `tree-sitter`. This drastically reduces LLM token consumption by avoiding brute-force file searches.

---

## 📊 Indeks Xulosasi (Current Corpus Index)

- **Jami Tugunlar (Nodes):** 767
- **Tugunlararo Bog'liqliklar (Edges):** 1369
- **Jamiyatlar (Communities):** 39 ta (Backend, Angular UI, Gateway, Keycloak va b.)
- **Bosh Tugunlar ("God Nodes" - Asosiy Arxitektura):**
  1. `Question` (43 ta bog'liqlik)
  2. `QuizService` (34 ta bog'liqlik)
  3. `QuizApiService` (33 ta bog'liqlik)
  4. `QuizApi.Core.Domain.Entities` (31 ta bog'liqlik)
  5. `AuthService` (28 ta bog'liqlik)
  6. `TelegramBotService` (27 ta bog'liqlik)

---

## ⚙️ Graphify Buyruqlari (Usage Commands)

### 1. Koddagi o'zgarishlardan so'ng indeksni yangilash (Local AST - Zero API Token Cost)
```bash
~/.local/bin/graphify . --code-only
```

### 2. Grafik reportini qayta hisoblash
```bash
~/.local/bin/graphify cluster-only .
```

---

## 📂 Yaratilgan Indeks Fayllari
- `graphify-out/GRAPH_REPORT.md`: Arxitektura va bog'liqliklar hisoboti.
- `graphify-out/graph.json`: Vizual va AI tomonidan o'qiladigan to'liq grafik indeksi.
- `graphify-out/graph.html`: Brauzerda loyiha me'morchiligini vizual interaktiv ko'rish imkoniyati.
