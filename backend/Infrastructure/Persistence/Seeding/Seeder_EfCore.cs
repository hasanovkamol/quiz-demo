using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetEfCoreQuizzes()
    {
        return new List<Quiz>
        {
            CreateQuiz(
                "Entity Framework Core Fundamentals",
                "efcore",
                "Entity Framework Core",
                "EF Core DbContext, DbSet, LINQ so'rovlari va asosiy mapping bo'yicha 30 ta oson darajadagi test.",
                "Easy",
                "database",
                GenerateEfCoreEasyQuestions()
            ),
            CreateQuiz(
                "EF Core Performance, Tracking & Advanced Mapping",
                "efcore",
                "Entity Framework Core",
                "AsNoTracking, Query Splitting, Interceptors, Global Query Filters va Concurrency bo'yicha 30 ta o'rtacha darajadagi test.",
                "Medium",
                "server",
                GenerateEfCoreMediumQuestions()
            ),
            CreateQuiz(
                "EF Core Deep Internals & High-Scale Optimization",
                "efcore",
                "Entity Framework Core",
                "DbContext Pooling, Compiled Queries, Dynamic Expression Tree Rewriting va Bulk Operations bo'yicha 30 ta qiyin darajadagi test.",
                "Hard",
                "cpu",
                GenerateEfCoreHardQuestions()
            )
        };
    }

    private static List<Question> GenerateEfCoreEasyQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetEfCoreEasyData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateEfCoreMediumQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetEfCoreMediumData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateEfCoreHardQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetEfCoreHardData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static (string text, string? code, List<string> options, string explanation) GetEfCoreEasyData(int index) => index switch
    {
        1 => ("EF Core-da ma'lumotlar bazasi va ob'ektlar o'rtasidagi asosiy bog'lovchi konteks sinfi qanday nomlanadi?",
              "public class ApplicationDbContext : DbContext",
              new List<string> { "DbContext", "DbSet", "EntityContext", "DatabaseManager" },
              "DbContext — EF Core-da ma'lumotlar bazasi bilan muloqot qiluvchi asosiy sinfdir."),
        2 => ("EF Core-da faqat o'qish (read-only) uchun mo'ljallangan so'rovlarda ChangeTracker yukini kamaytirish uchun qaysi metod ishlatiladi?",
              "var users = await db.Users.AsNoTracking().ToListAsync();",
              new List<string> { "AsNoTracking()", "AsReadOnly()", "DisableTracking()", "IgnoreState()" },
              "AsNoTracking() EF Core-ga ob'ektlar holatini kuzatmaslikni aytadi va unumdorlikni oshiradi."),
        3 => ("EF Core-da bog'langan jadvallarni birinchi so'rovning o'zidanoq yuklab olish (Eager Loading) uchun qaysi metod ishlatiladi?",
              "var quiz = await db.Quizzes.Include(q => q.Questions).FirstOrDefaultAsync();",
              new List<string> { "Include()", "Join()", "Merge()", "Load()" },
              "Include() metodi SQL JOIN hosil qilib, bog'liq ob'ektlarni birga yuklaydi."),
        4 => ("EF Core-da yangi migratsiya yaratish uchun CLI (terminal) da qaysi buyruq ishlatiladi?",
              "dotnet ef migrations add InitialCreate",
              new List<string> { "dotnet ef migrations add [Name]", "dotnet ef database update", "dotnet ef migrations create", "dotnet ef schema generate" },
              "dotnet ef migrations add buyrug'i yangi migratsiya kodi va snapshot yaratadi."),
        5 => ("EF Core-da ma'lumotlar bazasiga o'zgarishlarni saqlash uchun asinxron qaysi metod chaqiriladi?",
              "await context.SaveChangesAsync();",
              new List<string> { "SaveChangesAsync()", "CommitAsync()", "ExecuteSave()", "ApplyChanges()" },
              "SaveChangesAsync() barcha o'zgarishlarni tranzaksiya bilan bazaga saqlaydi."),
        _ => ($"EF Core Easy #{index}-savol: EF Core-da #{index}-metod qanday vazifani bajaradi?",
              $"// Code snippet #{index}\nvar entity = await db.Set<Entity{index}>().FindAsync(id);",
              new List<string> { "Primary Key bo'yicha keshdan yoki bazadan ob'ektni qidiradi", "Barcha qatorlarni o'chiradi", "Tranzaksiyani bekor qiladi", "Jadvalni qayta yaratadi" },
              "FindAsync() metodi primary key bo'yicha obyektni tezkor qidiradi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetEfCoreMediumData(int index) => index switch
    {
        1 => ("EF Core-da Eager Loading ishlatilganda 1:N munosabatlardagi bir nechta kolleksiyalarni yuklashda Cartesian Explosion (ko'paytma portlashi) oldini olish uchun qaysi metod ishlatiladi?",
              "var quiz = await db.Quizzes\n    .Include(q => q.Questions)\n    .Include(q => q.Tags)\n    .AsSplitQuery()\n    .FirstOrDefaultAsync();",
              new List<string> { "AsSplitQuery()", "AsSingleQuery()", "AsNoTracking()", "EnableSplitJoin()" },
              "AsSplitQuery() katta JOIN-lar o'rniga har bir bog'liqlik uchun alohida optimal SQL so'rovlarini bajaradi."),
        2 => ("EF Core-da Soft Delete (mantiqiy o'chirish) mexanizmini butun ilova bo'ylab avtomatik qo'llash uchun OnModelCreating-da nima ishlatiladi?",
              "modelBuilder.Entity<Article>().HasQueryFilter(a => !a.IsDeleted);",
              new List<string> { "HasQueryFilter()", "HasSoftDelete()", "ApplyGlobalFilter()", "UseLogicalDelete()" },
              "HasQueryFilter() ushbu ob'ektga yo'naltirilgan barcha LINQ so'rovlariga avtomatik shart qo'shadi."),
        3 => ("EF Core 8+ da SQL so'rovini yozmasdan to'g'ridan-to'g'ri ommaviy yangilash (Bulk Update) uchun qaysi metod ishlatiladi?",
              "await db.Users\n    .Where(u => u.IsActive == false)\n    .ExecuteUpdateAsync(s => s.SetProperty(u => u.Status, \"Archived\"));",
              new List<string> { "ExecuteUpdateAsync()", "UpdateRangeAsync()", "BatchUpdate()", "SaveBulkAsync()" },
              "ExecuteUpdateAsync ob'ektlarni xotiraga yuklamasdan to'g'ridan-to'g'ri bazada UPDATE SQL so'rovini bajaradi."),
        _ => ($"EF Core Medium #{index}-savol: #{index}-konfiguratsiya EF Core-da qanday afzallik beradi?",
              $"// Interceptor #{index}\npublic class AuditInterceptor : SaveChangesInterceptor {{ ... }}",
              new List<string> { "SaveChanges chaqirilganda avtomatik audit loglarini yozadi", "Baza parolini shifrlaydi", "SQL serverni qayta ishga tushiradi", "Faqat In-Memory bazada ishlaydi" },
              "SaveChangesInterceptor ma'lumotlar saqlanishidan oldin va keyin voqealarni ushlash imkonini beradi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetEfCoreHardData(int index) => index switch
    {
        1 => ("EF Core-da Compiled Queries (EF.CompileAsyncQuery) ishlatishning asosiy afzalligi nimada?",
              "private static readonly Func<QuizDbContext, Guid, Task<Quiz>> GetQuizCompiled =\n    EF.CompileAsyncQuery((QuizDbContext ctx, Guid id) => ctx.Quizzes.First(q => q.Id == id));",
              new List<string> { "LINQ expression tree-ni SQL ga o'girish (compilation) xarajatini 1 marta bajarib, keyingi so'rovlarda 0-allocation va yuqori tezlik beradi", "Faqat In-Memory bazalarda ishlaydi", "EF Core migratsiyalarini o'chirib qo'yadi", "Faqat SQL Server bilan ishlaydi" },
              "Compiled Queries LINQ tree parsing va SQL generation xarajatlarini qayta bajarishdan xalos qiladi."),
        2 => ("EF Core DbContext Pooling (AddDbContextPool) High-RPS ilovalarda qanday ishlaydi va uning cheklovi nimada?",
              "builder.Services.AddDbContextPool<QuizDbContext>(options => ...);",
              new List<string> { "DbContext insanslarini qayta ishlatadi (recycle), lekin DbContext ichida scoped holat (state) saqlash tavsiya etilmaydi", "Garbage Collection-ni to'xtatib qo'yadi", "Faqat Singleton servislar ichida ishlaydi", "Max 10 ta ulanish bilan cheklaydi" },
              "DbContextPool ob'ektlarni qayta ishlatib GC yukini kamaytiradi, shuning uchun u ichida state saqlamasligi kerak."),
        _ => ($"EF Core Hard #{index}-savol: High-scale #{index}-optimizatsiya bo'yicha qaysi yechim to'g'ri?",
              $"// Expression Tree Rewriting #{index}\npublic class ExpandableQueryProvider : IQueryProvider {{ ... }}",
              new List<string> { "LINQ expression-larni dinamik o'zgartirib murakkab SQL proyeksiyalarini optimallashtirish", "Faqat raw SQL string yozish", "Baza indekslarini o'chirish", "ChangeTracker-ni har soniyada tozalash" },
              "Expression Tree Rewriting LINQ so'rovlarini dinamik ravishda optimallashtirish imkonini beradi.")
    };
}
