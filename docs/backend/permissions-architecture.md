# Permission-Based Authorization Architecture (PBAC / Claims)

Documentation of Permission-Based Authorization in ASP.NET Core Web API.

---

## 🔒 Defined Permissions Constant List (`Permissions.cs`)

Located in [`Core/Domain/Constants/Permissions.cs`](file:///home/user02/Projects/AI%20Projects/Qiuz/backend/Core/Domain/Constants/Permissions.cs):

| Permission Constant | Value | Description |
|---|---|---|
| `QuizzesRead` | `"quizzes:read"` | View and search quizzes |
| `QuizzesCreate` | `"quizzes:create"` | Create custom quizzes |
| `QuizzesDelete` | `"quizzes:delete"` | Delete custom quizzes |
| `AttemptsRead` | `"attempts:read"` | View quiz attempt records |
| `AttemptsSubmit` | `"attempts:submit"` | Submit completed quiz attempts |
| `AiGenerate` | `"ai:generate"` | Trigger Semantic Kernel AI question generation |
| `AdminStats` | `"admin:stats"` | Access admin dashboard statistics |
| `UsersManage` | `"users:manage"` | Manage user roles and permission policies |

---

## 👥 Role to Permission Mappings

- **Admin Role**: Possesses `Permissions.AllPermissions` (All 8 permissions).
- **User Role**: Possesses `Permissions.UserPermissions` (`quizzes:read`, `quizzes:create`, `attempts:submit`, `attempts:read`).

---

## 🛡 Permission Evaluation Handler

Implemented via [`PermissionAuthorizationHandler`](file:///home/user02/Projects/AI%20Projects/Qiuz/backend/Infrastructure/Identity/PermissionAuthorizationHandler.cs):

```csharp
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissionClaims = context.User.FindAll("permission").Select(c => c.Value);
        if (permissionClaims.Contains(requirement.Permission, StringComparer.OrdinalIgnoreCase))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
```
