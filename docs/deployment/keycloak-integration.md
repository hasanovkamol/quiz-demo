# Keycloak Integratsiya Yo'riqnomasi

## Umumiy Ma'lumot

QuizMaster PRO ilovasida Keycloak OIDC autentifikatsiyasi quyidagi arxitektura bo'yicha ishlaydi:

```
GitHub Pages (Angular)
        │
        │  OIDC redirect / token request
        ▼
Cloudflare Tunnel ──► Nginx Gateway /auth/* ──► Keycloak:8080 (KC_HTTP_RELATIVE_PATH=/auth)
                                   /api/*  ──► ASP.NET Core Backend
```

---

## Tarkibiy qismlar

### 1. Keycloak Docker (docker-compose.yml)

```yaml
keycloak:
  image: quay.io/keycloak/keycloak:24.0
  command: start-dev --import-realm
  environment:
    KEYCLOAK_ADMIN: admin
    KEYCLOAK_ADMIN_PASSWORD: admin
    KC_HTTP_RELATIVE_PATH: /auth        # /auth prefix bilan ishlaydi
    KC_PROXY: edge                      # Nginx reverse proxy orqasida
    KC_HOSTNAME_STRICT: "false"
    KC_HOSTNAME_STRICT_HTTPS: "false"
```

**Muhim:** `KC_HTTP_RELATIVE_PATH: /auth` — bu orqali Keycloak barcha endpointlari `/auth/...` prefiksida ishlaydi va Nginx gateway orqali tashqariga chiqariladi.

### 2. Nginx Gateway (gateway/nginx.conf)

```nginx
location /auth/ {
    proxy_pass http://keycloak:8080/auth/;
    proxy_set_header X-Forwarded-Proto $scheme;
    # ...
}
```

GitHub Pages'dan Cloudflare tunnel URL ga `/auth/` so'rovlari Keycloak'ga yo'naltiriladi.

### 3. Angular KeycloakService (ui/src/app/services/keycloak.service.ts)

`keycloak-js` kutubxonasi orqali OIDC PKCE flow amalga oshirilgan:

```typescript
await kc.init({
  onLoad: 'check-sso',           // Sahifa yuklanganda silent SSO tekshirish
  silentCheckSsoRedirectUri: '...'/silent-check-sso.html',
  pkceMethod: 'S256',            // Xavfsiz PKCE
  checkLoginIframe: false,
});
```

**Signals asosida reaktiv holat:**
- `isAuthenticated` — kirgan/kirmaganligi
- `roles` — realm rollari
- `permissions` — `permission` claim (quizzes:read, admin:stats, ...)
- `isAdmin` — Admin ekanligini computed signal orqali tekshiradi

### 4. config.json (ui/public/config.json)

Runtime konfiguratsiya — deploy vaqtida Cloudflare URL o'zgarsa faqat shu faylni yangilash yetarli:

```json
{
  "apiUrl": "https://<cloudflare-tunnel-url>/api",
  "keycloak": {
    "url": "https://<cloudflare-tunnel-url>/auth",
    "realm": "quizmaster-realm",
    "clientId": "quizmaster-app"
  }
}
```

### 5. Keycloak Realm (keycloak/realm-export.json)

`quizmaster-app` client konfiguratsiyasi:
- `publicClient: true` — frontend PKCE flow uchun
- `redirectUris` — GitHub Pages URL qo'shilgan:
  ```json
  "redirectUris": [
    "http://localhost/*",
    "http://127.0.0.1/*",
    "https://hasanovkamol.github.io/*"
  ]
  ```
- `webOrigins` — CORS uchun GitHub Pages origin qo'shilgan
- `permission` claim mapper — JWT tokeniga rollar qo'shiladi

---

## Foydalanish — Komponentda Login/Logout

```typescript
import { KeycloakService } from '../services/keycloak.service';

@Component({ ... })
export class MyComponent {
  private readonly kc = inject(KeycloakService);

  readonly isLoggedIn = this.kc.isAuthenticated;
  readonly username = this.kc.username;
  readonly isAdmin = this.kc.isAdmin;

  login() {
    this.kc.login(); // Keycloak login sahifasiga redirect
  }

  logout() {
    this.kc.logout(); // Session o'chiriladi
  }

  canCreate() {
    return this.kc.hasPermission('quizzes:create');
  }
}
```

---

## Auth Interceptor — Dual Token Support

`auth.interceptor.ts` quyidagi tartibda token tanlaydi:

1. **Keycloak autentifikatsiya bo'lgan bo'lsa** → Keycloak JWT token (avtomatik yangilanadi)
2. **Google OAuth bo'lgan bo'lsa** → Google JWT token (fallback)

---

## Docker qayta ishga tushirish

Keycloak va gateway konfiguratsiyasi o'zgargandan keyin:

```bash
docker compose down keycloak gateway
docker compose up -d keycloak gateway
```

Realm o'zgarganda Keycloak Admin Console'ga kirib qo'lda import qiling yoki:
```bash
docker compose down keycloak
docker volume rm quiz_keycloak_data  # agar volume bo'lsa
docker compose up -d keycloak
```

---

## Muhim URL lar (Docker ichida)

| Xizmat | URL |
|--------|-----|
| Keycloak Admin | http://localhost:8080/auth/admin |
| Keycloak Realm | http://localhost:8080/auth/realms/quizmaster-realm |
| OIDC Discovery | http://localhost:8080/auth/realms/quizmaster-realm/.well-known/openid-configuration |
| Backend Auth | Keycloak__Authority = http://keycloak:8080/auth/realms/quizmaster-realm |
