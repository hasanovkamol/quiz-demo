# Keycloak OpenID Connect & 5-Minute Token Refresh

Documentation of authentication and keycloak identity provider configuration.

---

## 🔑 Authentication Architecture

- **Identity Provider (IAM)**: Keycloak 24.0 OpenID Connect (`quay.io/keycloak/keycloak:24.0`).
- **Access Token Expiration**: 5 Minutes (300 seconds).
- **Refresh Token Endpoint**: `POST /api/auth/refresh`.

### Refresh Request DTO
```csharp
public record RefreshTokenRequestDto(
    string RefreshToken,
    string? UserId
);
```

### JWT Token Claims
- `ClaimTypes.NameIdentifier`: User GUID.
- `ClaimTypes.Email`: Email address.
- `ClaimTypes.Name`: Display name.
- `ClaimTypes.Role`: "User" | "Admin".
