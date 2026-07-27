using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using QuizApi.Core.Application.Interfaces;

namespace QuizApi.Infrastructure.Identity;

public class KeycloakService(HttpClient httpClient, IConfiguration configuration, ILogger<KeycloakService> logger) : IKeycloakService
{
    public async Task SyncUserToKeycloakAsync(string email, string name, string role)
    {
        if (string.IsNullOrWhiteSpace(email)) return;

        try
        {
            var keycloakUrl = configuration["Keycloak:AdminUrl"] ?? "http://keycloak:8080";
            var realm = configuration["Keycloak:Realm"] ?? "quizmaster-realm";
            var adminUser = configuration["Keycloak:AdminUsername"] ?? "admin";
            var adminPass = configuration["Keycloak:AdminPassword"] ?? "admin";

            // 1. Get Keycloak Admin Access Token
            using var tokenReq = new HttpRequestMessage(HttpMethod.Post, $"{keycloakUrl}/realms/master/protocol/openid-connect/token")
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", "admin-cli"),
                    new KeyValuePair<string, string>("username", adminUser),
                    new KeyValuePair<string, string>("password", adminPass),
                })
            };

            var tokenRes = await httpClient.SendAsync(tokenReq);
            if (!tokenRes.IsSuccessStatusCode)
            {
                logger.LogWarning("Keycloak Admin Token request returned status: {Status}", tokenRes.StatusCode);
                return;
            }

            var tokenJson = await tokenRes.Content.ReadAsStringAsync();
            var adminAccessToken = JsonNode.Parse(tokenJson)?["access_token"]?.ToString();
            if (string.IsNullOrEmpty(adminAccessToken)) return;

            // 2. Check if user already exists in Keycloak
            using var checkReq = new HttpRequestMessage(HttpMethod.Get, $"{keycloakUrl}/admin/realms/{realm}/users?email={Uri.EscapeDataString(email)}");
            checkReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", adminAccessToken);

            var checkRes = await httpClient.SendAsync(checkReq);
            if (checkRes.IsSuccessStatusCode)
            {
                var checkJson = await checkRes.Content.ReadAsStringAsync();
                var usersArr = JsonNode.Parse(checkJson)?.AsArray();
                if (usersArr != null && usersArr.Count > 0)
                {
                    logger.LogInformation("User {Email} already exists in Keycloak realm {Realm}.", email, realm);
                    return;
                }
            }

            // 3. Create User in Keycloak
            var names = name.Split(' ', 2);
            var firstName = names[0];
            var lastName = names.Length > 1 ? names[1] : "";

            var createUserObj = new
            {
                username = email,
                email = email,
                firstName = firstName,
                lastName = lastName,
                enabled = true,
                emailVerified = true
            };

            using var createReq = new HttpRequestMessage(HttpMethod.Post, $"{keycloakUrl}/admin/realms/{realm}/users")
            {
                Content = new StringContent(JsonSerializer.Serialize(createUserObj), Encoding.UTF8, "application/json")
            };
            createReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", adminAccessToken);

            var createRes = await httpClient.SendAsync(createReq);
            if (createRes.IsSuccessStatusCode || createRes.StatusCode == System.Net.HttpStatusCode.Created)
            {
                logger.LogInformation("Successfully created user {Email} in Keycloak realm {Realm}.", email, realm);
            }
            else
            {
                logger.LogWarning("Keycloak create user API returned {StatusCode}", createRes.StatusCode);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error syncing user {Email} to Keycloak", email);
        }
    }
}
