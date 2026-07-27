# `quiz-net` Isolated Network Topology

Diagram and DNS resolution guide for the `quiz-net` Docker bridge network.

---

## 🏛 `quiz-net` Bridge Network Topology

```
 ┌────────────────────────────────────────────────────────────────────────┐
 │                        LOCAL NETWORK (LAN Users)                       │
 └───────────────────────────────────┬────────────────────────────────────┘
                                     │ HTTP Port 80
                                     ▼
 ┌────────────────────────────────────────────────────────────────────────┐
 │                           GATEWAY (Nginx)                              │
 │  - Exposes Port 80 to Local Network (0.0.0.0:80)                      │
 │  - Network: quiz-net                                                   │
 │  - Serves static Angular SPA routes (/) from 'ui' container           │
 │  - Proxies /api/* to 'backend' container (http://backend:5000/api/)    │
 └──────┬────────────────────────────┬────────────────────┬───────────────┘
        │ Network: quiz-net          │ Network: quiz-net  │ Network: quiz-net
        ▼                            ▼                    ▼
 ┌──────────────┐            ┌────────────────┐   ┌──────────────┐
 │ UI CONTAINER │            │ BACKEND (API)  │   │   KEYCLOAK   │
 │ (Angular 18) │            │ (.NET 9 + SK)  │   │  (Port 8080) │
 └──────────────┘            └───────┬────────┘   └──────────────┘
                                     │ Network: quiz-net (PostgreSQL Protocol)
                                     ▼
                             ┌────────────────┐
                             │  DB CONTAINER  │
                             │ (PostgreSQL 16)│
                             └────────────────┘
```
