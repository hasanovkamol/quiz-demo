# ASP.NET Core Integration Test Suite (`backend.tests/`)

Documentation of the backend integration test suite.

---

## 🧪 Integration Test Suite (`backend.tests/`)

Located in `./backend.tests/`, the test project executes in-memory HTTP integration testing using `WebApplicationFactory<Program>`:

### Test Execution Command
```bash
cd backend.tests && dotnet test
```

### Test Coverage Summary (7/7 Passed)
1. **`QuizEndpointsTests`**:
   - `GET /api/quizzes`: Returns HTTP 200 OK and seeded quiz array.
   - `POST /api/quizzes`: Creates a custom quiz with options and returns HTTP 201 Created.
   - `GET /api/quizzes/{id}`: Validates HTTP 404 NotFound for non-existent GUIDs.
2. **`AttemptEndpointsTests`**:
   - `POST /api/quizattempts`: Submits user quiz attempt and verifies scorecard persistence.
   - `GET /api/quizattempts`: Retrieves list of submitted quiz attempts.
3. **`AuthEndpointsTests`**:
   - `POST /api/auth/google-login`: Validates user creation, JWT Token, Refresh Token, and 300s expiration.
   - `POST /api/auth/refresh`: Validates 5-minute Access Token refresh mechanism.
