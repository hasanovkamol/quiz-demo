# Multi-Container Docker Compose Setup & Nginx Gateway

Documentation of container topology and port mapping configuration.

---

## 🐳 Container Port Mapping Topology

| Service | Container Name | Host Port | Internal Container Port | Description |
|---|---|---|---|---|
| **gateway** | `quiz_nginx_gateway` | **`80`** | `80` | Main Reverse Proxy Entrypoint (`http://localhost`) |
| **ui** | `quiz_angular_ui` | **`8081`** | `80` | Angular 18+ Direct SPA Access (`http://localhost:8081`) |
| **adminer** | `quiz_adminer_ui` | **`8082`** | `8080` | PostgreSQL Database Web UI (`http://localhost:8082`) |
| **backend** | `quiz_aspnet_backend` | **`5005`** | `5000` | ASP.NET Core 10 Web API (`http://localhost:5005`) |
| **keycloak** | `quiz_keycloak` | **`8080`** | `8080` | Keycloak Identity Realm (`http://localhost:8080`) |
| **infisical** | `quiz_infisical_secrets` | **`8000`** | `8000` | Infisical Secret Manager (`http://localhost:8000`) |
| **db** | `quiz_postgres_db` | **`5432`** | `5432` | PostgreSQL Database Container |

---

## 🚀 Execution & Port Access

```bash
docker compose up -d
```
- Direct Angular UI: `http://localhost:8081`
- Adminer Database UI: `http://localhost:8082`
- ASP.NET Core 10 API: `http://localhost:5005`
- Full Gateway Entrypoint: `http://localhost`
