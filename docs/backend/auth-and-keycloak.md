# Keycloak OpenID Connect & Automatic User Sync Service

Documentation of authentication, Keycloak identity provider configuration, and automatic user provisioning via Keycloak Admin REST API.

---

## 🔑 Authentication Architecture

- **Identity Provider (IAM)**: Keycloak 24.0 OpenID Connect (`quay.io/keycloak/keycloak:24.0`).
- **Target Realm**: `quizmaster-realm`
- **Access Token Expiration**: 5 Minutes (300 seconds).
- **Refresh Token Endpoint**: `POST /api/auth/refresh`.

---

## 🔄 Automatic Keycloak User Synchronization Service (`KeycloakService`)

Whenever a user authenticates via **Google OAuth 2.0** or **Telegram Mini App**, the ASP.NET Core backend automatically provisions/syncs the user into Keycloak `quizmaster-realm` via the Keycloak Admin REST API.

### Workflow:
1. User authenticates via Google ID Token or Telegram WebApp.
2. `AuthService.AuthenticateGoogleUserAsync` saves/updates user in EF Core PostgreSQL database (`dbContext.Users`).
3. `IKeycloakService.SyncUserToKeycloakAsync` obtains an `admin-cli` access token from Keycloak (`/realms/master/protocol/openid-connect/token`).
4. Checks if the user email already exists in `quizmaster-realm` (`GET /admin/realms/quizmaster-realm/users?email=...`).
5. If absent, creates the user in Keycloak (`POST /admin/realms/quizmaster-realm/users`) with `enabled: true`, `emailVerified: true`, `firstName`, and `lastName`.
6. The user instantly appears in the Keycloak Admin Console Users dashboard (`http://localhost:8080/admin/master/console/#/quizmaster-realm/users`).

---

### JWT Token Claims
- `ClaimTypes.NameIdentifier`: User GUID.
- `ClaimTypes.Email`: Email address.
- `ClaimTypes.Name`: Display name.
- `ClaimTypes.Role`: "User" | "Admin".
