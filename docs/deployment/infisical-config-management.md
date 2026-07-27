# Infisical Centralized Secret & Config Management

Documentation of Infisical Vault secret management setup.

---

## 🔑 Infisical Architecture & Port Configuration

```
┌────────────────────────────────────────────────────────────────────────┐
│                   INFISICAL SECRET VAULT CONTAINER                     │
│ Image: infisical/infisical:latest                                      │
│ Port Mapping: 8000:8080 (Host Port 8000 -> Container Port 8080)        │
│ Environment: PORT=8080, SITE_URL=http://localhost:8000                │
└────────────────────────────────────────────────────────────────────────┘
```

- **Infisical Web UI URL**: `http://localhost:8000`
- **Internal Port**: `8080` (mapped to host port `8000`).
- **PostgreSQL Database**: `infisical_postgres_db` (Port 5432).
- **Redis Cache**: `infisical_redis_cache` (Port 6379).

---

## 🚀 Accessing Infisical Web Dashboard

1. Open `http://localhost:8000` in your web browser.
2. Complete the initial Admin Signup / Setup.
3. Manage secrets, database connection strings, JWT keys, and Gemini AI API Keys centrally!
