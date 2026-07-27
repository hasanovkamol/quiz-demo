# 5-Minute Silent Refresh Token Mechanism

Documentation of automated background token refresh timer in Angular UI.

---

## 🔑 Refresh Token Flow Details

1. **5-Minute Access Token Lifespan**: Access Tokens issued by the backend expire in 300 seconds (5 minutes).
2. **Automatic Silent Refresh Timer**:
   - `AuthService` tracks token issuance and schedules a silent HTTP request (`POST /api/auth/refresh`) 30 seconds before token expiration (at **4 minutes 30 seconds** / 270 seconds).
   - Occurs silently in the background without logging the user out or interrupting an ongoing quiz attempt.
3. **Session Persistence**:
   - Refresh Token and User Profile are persisted in `localStorage` (`quizmaster_refresh_token`).
   - If a page refresh occurs, the session and silent timer are automatically restored.
