namespace QuizApi.Core.Domain.Constants;

public static class Permissions
{
    public const string QuizzesRead = "quizzes:read";
    public const string QuizzesCreate = "quizzes:create";
    public const string QuizzesDelete = "quizzes:delete";

    public const string AttemptsRead = "attempts:read";
    public const string AttemptsSubmit = "attempts:submit";

    public const string AiGenerate = "ai:generate";
    public const string AdminStats = "admin:stats";
    public const string UsersManage = "users:manage";

    public static readonly string[] AllPermissions =
    [
        QuizzesRead,
        QuizzesCreate,
        QuizzesDelete,
        AttemptsRead,
        AttemptsSubmit,
        AiGenerate,
        AdminStats,
        UsersManage
    ];

    public static readonly string[] UserPermissions =
    [
        QuizzesRead,
        QuizzesCreate,
        AttemptsSubmit,
        AttemptsRead
    ];

    public static string[] GetPermissionsForRole(string role)
    {
        return role switch
        {
            "Admin" => AllPermissions,
            "User" => UserPermissions,
            _ => [QuizzesRead, AttemptsSubmit]
        };
    }
}
