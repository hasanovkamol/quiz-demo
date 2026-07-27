# Infisical Centralized Secret & Configuration Management Guide

Guide for managing ASP.NET Core backend configurations and secrets using Infisical (`http://localhost:8000`).

---

## 🔑 Infisical Architecture & Container Setup

Infisical runs as a standalone service in [`docker-compose.yml`](file:///home/user02/Projects/AI%20Projects/Qiuz/docker-compose.yml):

- **Infisical Server (`quiz_infisical_secrets`)**: Accessible at `http://localhost:8000`.
- **Infisical Database (`infisical_postgres_db`)**: Dedicated PostgreSQL 16 database.
- **Infisical Redis Cache (`infisical_redis_cache`)**: Redis 7 cache.

---

## 🚀 How Infisical Injects Secrets into Backend

The backend [`Dockerfile`](file:///home/user02/Projects/AI%20Projects/Qiuz/backend/Dockerfile) installs the Infisical CLI binary and wraps container startup:

```dockerfile
ENTRYPOINT ["infisical", "run", "--", "dotnet", "QuizApi.dll"]
```

When the container boots, `infisical run` dynamically fetches secrets from `http://infisical:8000` and injects them as standard environment variables into ASP.NET Core.

---

## 🛠 Infisical Web Dashboard Secret Setup Guide

1. **Access Dashboard**:
   - Open browser: `http://localhost:8000`.
   - Register root admin account.
2. **Create Project**:
   - Project Name: `QuizMaster PRO`.
3. **Add Managed Secrets**:
   - `ConnectionStrings__DefaultConnection`: `Host=db;Port=5432;Database=quizdb;Username=postgres;Password=postgres`
   - `Jwt__SecretKey`: `QuizMaster_Super_Secret_JWT_Key_2026_Enterprise_Secure!`
   - `Jwt__Issuer`: `QuizMasterAPI`
   - `Jwt__Audience`: `QuizMasterApp`
   - `Keycloak__Authority`: `http://keycloak:8080/realms/quizmaster-realm`
   - `Ai__GeminiApiKey`: `<YOUR_GEMINI_API_KEY>`
