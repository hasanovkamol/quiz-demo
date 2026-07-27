# Docker Compose & Nginx Gateway Setup

Detailed guide for multi-container orchestration.

---

## 🐳 Docker Services (`docker-compose.yml`)

1. `quiz_postgres_db`: PostgreSQL 16 database engine.
2. `quiz_keycloak`: Keycloak IAM 24.0 OpenID Connect on port 8080.
3. `quiz_aspnet_backend`: ASP.NET Core 9 Web API on port 5000.
4. `quiz_angular_ui`: Angular 18+ Single Page Application.
5. `quiz_nginx_gateway`: Nginx API Gateway exposing port 80.

### Execution Command
```bash
docker compose up --build -d
```
