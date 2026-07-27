namespace QuizApi.Core.Application.Interfaces;

public interface IKeycloakService
{
    Task SyncUserToKeycloakAsync(string email, string name, string role);
}
