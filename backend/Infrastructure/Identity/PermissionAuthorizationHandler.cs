using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace QuizApi.Infrastructure.Identity;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        // 1. Direct 'permission' claim check
        var directClaims = context.User.FindAll("permission").Select(c => c.Value);
        if (directClaims.Contains(requirement.Permission, StringComparer.OrdinalIgnoreCase))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        // 2. Keycloak 'realm_access.roles' check
        var realmAccessClaim = context.User.FindFirst("realm_access")?.Value;
        if (!string.IsNullOrWhiteSpace(realmAccessClaim))
        {
            try
            {
                using var doc = JsonDocument.Parse(realmAccessClaim);
                if (doc.RootElement.TryGetProperty("roles", out var rolesElem) && rolesElem.ValueKind == JsonValueKind.Array)
                {
                    foreach (var role in rolesElem.EnumerateArray())
                    {
                        if (string.Equals(role.GetString(), requirement.Permission, StringComparison.OrdinalIgnoreCase))
                        {
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }
                    }
                }
            }
            catch
            {
                // Fallthrough if non-JSON string
            }
        }

        // 3. Keycloak Role Claims fallback
        var roleClaims = context.User.FindAll("role").Select(c => c.Value);
        if (roleClaims.Contains(requirement.Permission, StringComparer.OrdinalIgnoreCase))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
