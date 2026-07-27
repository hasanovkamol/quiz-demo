# Keycloak Admin Panel Permission Management Guide

Guide for managing granular application permissions directly inside Keycloak Admin UI (`http://localhost:8080`).

---

## 🔑 Keycloak Realm Export & Import Configuration

The [`keycloak/realm-export.json`](file:///home/user02/Projects/AI%20Projects/Qiuz/keycloak/realm-export.json) automatically pre-configures Keycloak Realm Roles and OIDC Mapper during Docker initialization (`command: start-dev --import-realm`).

### Keycloak Pre-Configured Permission Roles:
1. `quizzes:read`
2. `quizzes:create`
3. `quizzes:delete`
4. `attempts:read`
5. `attempts:submit`
6. `ai:generate`
7. `admin:stats`
8. `users:manage`

---

## 🛠 Managing User Access/Reject via Keycloak Admin Console

1. **Access Keycloak Admin UI**:
   - Open browser: `http://localhost:8080` (or `http://<LAN-IP>:8080`).
   - Credentials: Username: `admin`, Password: `admin`.
   - Select Realm: **`quizmaster-realm`**.

2. **Granting Access (Assign Permission to User)**:
   - Navigate to **Users** -> Select Target User.
   - Go to **Role mapping** tab -> Click **Assign role**.
   - Select specific permission roles (e.g. `quizzes:create`, `ai:generate`).
   - Click **Save**. Keycloak includes the granted permissions in the user's JWT token, allowing ASP.NET Core API access immediately.

3. **Revoking / Rejecting Access (Remove Permission)**:
   - Go to **Users** -> Target User -> **Role mapping**.
   - Select the permission role to revoke (e.g. `quizzes:delete` or `ai:generate`).
   - Click **Unassign**. Next time the user calls the API, ASP.NET Core [`PermissionAuthorizationHandler`](file:///home/user02/Projects/AI%20Projects/Qiuz/backend/Infrastructure/Identity/PermissionAuthorizationHandler.cs) blocks access with **`HTTP 403 Forbidden`**!
