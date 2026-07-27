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
                "EF Core DbContext, DbSet, LINQ so'rovlari va asosiy mapping bo'yicha professional savollar.",
                "Easy",
                "database",
                GenerateEfCoreEasyQuestions()
            ),
            CreateQuiz(
                "EF Core Performance, Tracking & Advanced Mapping",
                "efcore",
                "Entity Framework Core",
                "AsNoTracking, Query Splitting, Interceptors, Global Query Filters va Concurrency bo'yicha senior savollar.",
                "Medium",
                "server",
                GenerateEfCoreMediumQuestions()
            ),
            CreateQuiz(
                "EF Core Deep Internals & High-Scale Optimization",
                "efcore",
                "Entity Framework Core",
                "DbContext Pooling, Compiled Queries, Dynamic Expression Tree Rewriting va Bulk Operations bo'yicha principal savollar.",
                "Hard",
                "cpu",
                GenerateEfCoreHardQuestions()
            )
        };
    }

    private static List<Question> GenerateEfCoreEasyQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "EF Core-da `AsNoTracking()` metodidan foydalanishning asosiy afzalligi nimada va u qaysi holatlarda ishlatiladi?",
                "var users = await dbContext.Users.AsNoTracking().Where(u => u.IsActive).ToListAsync();",
                new List<string> {
                    "DbContext ChangeTracker snapshot saqlamaydi va ob'ektlarni kuzatmaydi; Faqat o'qish (read-only) so'rovlarida xotira va tezlikni sezilarli oshiradi",
                    "Ma'lumotlar bazasida jadvalga avtomatik ravishda write-lock qo'yadi",
                    "Tranzaksiyani bekor qiladi va ma'lumotlarni o'chirib tashlaydi",
                    "LINQ so'rovini SQL ga o'girmasdan xotirada bajaradi"
                },
                "AsNoTracking() EF Core ga ob'ektlarni ChangeTracker snapshot-larida saqlamaslikni aytadi, bu faqat o'qish (read-only) so'rovlarida xotira va unumdorlikni sezilarli oshiradi."
            ),
            CreateQuestion(
                "EF Core-da N+1 so'rovlar muammosi (N+1 query problem) qanday kelib chiqadi va uni oldini olishning to'g'ri usuli qaysi?",
                "// Xato yondashuv:\nforeach(var q in db.Quizzes) {\n    var count = q.Questions.Count; // Har bir tsiklda alohida SQL query!\n}",
                new List<string> {
                    "Include() yoki ThenInclude() orqali Eager Loading qo'llash yoki Projection (.Select) yozish",
                    "Barcha jadvallarni bitta katta In-Memory List ga yuklab olish",
                    "DbContext obyektini har bir loop ichida qayta yaratish",
                    "AsNoTracking() ni o'chirish va SaveChangesAsync() ni chaqirish"
                },
                "N+1 muammosi bog'langan ma'lumotlar tsiklda har safar alohida SQL so'rovi bilan o'qilganda kelib chiqadi. Uni Include() yoki explicit Projection (.Select) orqali 1 ta SQL ga birlashtirish kerak."
            ),
            CreateQuestion(
                "EF Core-da migratsiya yaratish va ma'lumotlar bazasini dastur ishga tushganda avtomatik yangilash (migration apply) qanday bajariladi?",
                "await dbContext.Database.MigrateAsync();",
                new List<string> {
                    "Database.MigrateAsync() metodi bajarilmagan migratsiyalarni aniqlab bazaga avtomatik SQL sifatida qo'llaydi",
                    "EnsureCreatedAsync() va MigrateAsync() bir vaqtda chaqirilishi shart",
                    "Migratsiyalar faqat visual studio oynasidan bajariladi",
                    "Database.EnsureDeletedAsync() chaqiriladi"
                },
                "Database.MigrateAsync() bajarilmagan EF Core migratsiyalarini aniqlaydi va ma'lumotlar bazasiga xavfsiz tatbiq etadi."
            ),
            CreateQuestion(
                "EF Core Fluent API-da `OnModelCreating` metodi nima uchun ishlatiladi?",
                "protected override void OnModelCreating(ModelBuilder modelBuilder) {\n    modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();\n}",
                new List<string> {
                    "Entity munosabatlarini (1:N, N:M), indekslarni, kalitlarni va jadval nomlarini aniq konfiguratsiya qilish uchun",
                    "Faqat ma'lumotlar bazasi parolini saqlash uchun",
                    "Faqat Controller marshrutlarini sozlash uchun",
                    "Faqat brauzer keshini tozalash uchun"
                },
                "OnModelCreating Fluent API orqali ma'lumotlar bazasi sxemasi, indekslar va munosabatlarni moslashtirish imkonini beradi."
            ),
            CreateQuestion(
                "EF Core-da `DbSet<T>.FindAsync(id)` va `FirstOrDefaultAsync(x => x.Id == id)` o'rtasidagi asosiy farq nima?",
                "var user = await db.Users.FindAsync(userId);",
                new List<string> {
                    "FindAsync avval DbContext ChangeTracker keshidan qidiradi; Agar topilsa SQL so'rov yubormaydi. FirstOrDefaultAsync esa har doim bazaga SQL yuboradi",
                    "FirstOrDefaultAsync keshdan qidiradi, FindAsync esa har doim bazaga so'rov yuboradi",
                    "FindAsync faqat string ID-lar bilan ishlaydi",
                    "Ikkala metod ham bir xil ishlaydi"
                },
                "FindAsync() birlamchi kalit bo'yicha avval ChangeTracker lokal keshini tekshiradi, topilsa SQL so'rovini tejaydi."
            ),
            CreateQuestion(
                "EF Core-da Cascading Delete (Kaskadli o'chirish) sozlamasi qanday vazifa bajaradi?",
                "modelBuilder.Entity<Quiz>().HasMany(q => q.Questions).WithOne().OnDelete(DeleteBehavior.Cascade);",
                new List<string> {
                    "Ota ob'ekt (masalan Quiz) o'chirilganda, unga bog'liq barcha bola ob'ektlar (Questions) avtomatik ravishda o'chiriladi",
                    "Ota ob'ekt o'chirilganda bola ob'ektlar o'zgarmasdan qoladi",
                    "Ota ob'ektni o'chirishni taqiqlaydi va exception beradi",
                    "Bola ob'ektlarni boshqa jadvalga ko'chiradi"
                },
                "DeleteBehavior.Cascade ota entity o'chganda unga bog'liq barcha child entitiylarni avtomatik bazadan o'chiradi."
            ),
            CreateQuestion(
                "EF Core-da `Shadow Properties` (soya xususiyatlar) nimani anglatadi?",
                "modelBuilder.Entity<Article>().Property<DateTime>(\"LastUpdated\");",
                new List<string> {
                    "C# entity sinfida prop sifatida mavjud bo'lmagan, lekin EF Core modelida va ma'lumotlar bazasi jadvalida saqlanadigan ustunlar",
                    "Faqat o'chirilgan obyektlar keshda saqlanadigan joy",
                    "Faqat SQL Server-da ishlaydigan vaqtinchalik jadval",
                    "Entity-ning maxfiy paroli"
                },
                "Shadow Property C# sinfida aniqlanmagan, ammo EF Core va DB jadvalida mavjud bo'lgan ustundir."
            ),
            CreateQuestion(
                "EF Core-da `Value Converters` (HasConversion) nima uchun ishlatiladi?",
                "modelBuilder.Entity<Order>().Property(o => o.Status).HasConversion<string>();",
                new List<string> {
                    "C# tiplarini (masalan Enum, Custom Class) ma'lumotlar bazasi tiplariga (masalan string, int, JSON) o'girib saqlash va qayta o'qish uchun",
                    "SQL so'rovlarini avtomatik shifrlash uchun",
                    "Faqat DateTime formatini o'zgartirish uchun",
                    "DbContext pooling-ni yoqish uchun"
                },
                "Value Converters C# property qiymatini bazaga saqlash va o'qish vaqtida tip shaklini o'zgartiradi (masalan Enum -> string)."
            ),
            CreateQuestion(
                "EF Core-da `Keyless Entity Types` (HasNoKey) qaysi holatlarda qo'llaniladi?",
                "modelBuilder.Entity<UserStatsView>().HasNoKey().ToView(\"vw_UserStats\");",
                new List<string> {
                    "Birlamchi kaliti (Primary Key) bo'lmagan SQL View-lar yoki Stored Procedure natijalarini mapping qilish uchun",
                    "Faqat Primary Key bo'lgan jadvallarda",
                    "Faqat In-Memory test bazalarida",
                    "EF Core migratsiyalarini o'chirish uchun"
                },
                "HasNoKey() Primary Key ga ega bo'lmagan SQL View yoki raw SQL natijaviy strukturalarini xartagrafiya qilishda qo'llaniladi."
            ),
            CreateQuestion(
                "EF Core-da `Explicit Loading` (Aniq yuklash) qanday amalga oshiriladi?",
                "await db.Entry(quiz).Collection(q => q.Questions).LoadAsync();",
                new List<string> {
                    "Allaqachon yuklangan ota ob'ekt uchun bog'liq kolleksiya yoki navigatsiya xossasini keyinchalik talabga ko'ra `.LoadAsync()` bilan o'qish",
                    "Include() metodi bilan bir xil avtomatik yuklash",
                    "DbContext-ni qayta yaratish orqali yuklash",
                    "Faqat Lazy Loading yoqilganda ishlaydigan metod"
                },
                "Explicit Loading `db.Entry(entity).Reference(...).LoadAsync()` orqali ma'lumotni keyinchalik qo'lda yuklashdir."
            )
        };
    }

    private static List<Question> GenerateEfCoreMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "EF Core-da `AsSplitQuery()` metodi qaysi muammoni hal qiladi va u `AsSingleQuery()` dan nimasi bilan farq qiladi?",
                "var quiz = await db.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Options).AsSplitQuery().FirstOrDefaultAsync();",
                new List<string> {
                    "Bir nechta 1:N munosabatlarni yuklashda Cartesian Explosion (baza ko'paytmasi) hosil bo'lishini oldini olib, alohida optimal SQL so'rovlariga bo'ladi",
                    "Faqat bitta SQL query berishni majburlaydi",
                    "AsNoTracking-ni o'chirib qo'yadi",
                    "Faqat PostgreSQL bazada ishlaydi"
                },
                "AsSplitQuery() ko'plab Include-lar bo'lganda bitta ulkan va sekin SQL JOIN o'rniga bir nechta kichik tezkor SQL so'rovlarini chaqiradi."
            ),
            CreateQuestion(
                "EF Core-da Global Query Filters (`HasQueryFilter`) yordamida Soft Delete (mantiqiy o'chirish) va Multi-Tenancy qanday amalga oshiriladi?",
                "modelBuilder.Entity<Article>().HasQueryFilter(a => !a.IsDeleted && a.TenantId == _tenantId);",
                new List<string> {
                    "Model barcha LINQ so'rovlariga avtomatik filtrlash shartini qo'shadi; `IgnoreQueryFilters()` orqali bu filtrni vaqtincha o'chirish mumkin",
                    "Barcha ma'lumotlarni xotiraga yuklab keyin filtrlaydi",
                    "Faqat Raw SQL so'rovlarida ishlaydi",
                    "DbContext-ni har bir so'rovda qayta yaratadi"
                },
                "HasQueryFilter barcha LINQ query-larga avtomatik shart qo'shadi. IgnoreQueryFilters() bilan uni chetlab o'tish mumkin."
            ),
            CreateQuestion(
                "EF Core 8+ da `ExecuteUpdateAsync` va `ExecuteDeleteAsync` metodlarining oddiy `SaveChanges` ga nisbatan afzalligi nimada?",
                "await db.Users.Where(u => u.IsActive == false).ExecuteUpdateAsync(s => s.SetProperty(u => u.Status, \"Disabled\"));",
                new List<string> {
                    "Obyektlarni xotiraga yuklamasdan va ChangeTracker-ga olmasdan to'g'ridan-to'g'ri bazada ommaviy (Bulk) SQL UPDATE/DELETE bajaradi",
                    "ChangeTracker snapshot-larini Gen 2 xotirasiga o'tkazadi",
                    "Faqat In-Memory test bazalarida ishlaydi",
                    "Faqat 1 ta qatorni yangilay oladi"
                },
                "ExecuteUpdateAsync/ExecuteDeleteAsync ob'ektlarni ChangeTracker ga yuklamasdan darhol bazada to'g'ridan-to'g'ri SQL bajaradi."
            ),
            CreateQuestion(
                "EF Core-da Optimistic Concurrency (Optimistik mos keluvchanlik) nazorati `[Timestamp]` yoki `IsConcurrencyToken` bilan qanday ishlaydi?",
                "public class Product { public int Id; [Timestamp] public byte[] RowVersion; }",
                new List<string> {
                    "Saqlash vaqtida RowVersion bazadagi bilan solishtiriladi; Agar boshqa foydalanuvchi ma'lumotni o'zgartirgan bo'lsa DbUpdateConcurrencyException otiladi",
                    "Baza jadvaliga majburiy write-lock qo'yib boshqalarni bloklaydi",
                    "Tranzaksiyani avtomatik bekor qilib dasturni yopadi",
                    "Faqat SQLite bazasida ishlaydi"
                },
                "Optimistic Concurrency ma'lumot o'zgartirilganda RowVersion/Timestamp tekshirib, DbUpdateConcurrencyException orqali toqnashuvni (conflict) ushlaydi."
            ),
            CreateQuestion(
                "EF Core-da Owned Entity Types (`OwnsOne`, `OwnsMany`) konsepti nimani anglatadi?",
                "modelBuilder.Entity<User>().OwnsOne(u => u.Address);",
                new List<string> {
                    "Value Object-larni alohida primary key-siz, ota entity (User) jadvalining o'zidagi ustunlar sifatida saqlash imkonini beradi",
                    "Faqat boshqa ma'lumotlar bazasiga ulanish uchun",
                    "Faqat identity kalitlarini avto-increment qilish uchun",
                    "Faqat Readonly obyektlar uchun"
                },
                "OwnsOne/OwnsMany DDD Value Object-larini ota jadval ichidagi ustunlar (yoki bog'liq jadval) sifatida mapping qiladi."
            ),
            CreateQuestion(
                "EF Core Interceptors (`DbCommandInterceptor`, `SaveChangesInterceptor`) qanday vazifalarni bajaradi?",
                "public class AuditInterceptor : SaveChangesInterceptor",
                new List<string> {
                    "SQL so'rovlari ijro etilishidan oldin/keyin yoki SaveChanges chaqirilganda SQL-ni o'zgartirish, audit log yozish va taymer o'lchash imkonini beradi",
                    "Faqat In-Memory DB yaratadi",
                    "Faqat Controller parametrlarini tekshiradi",
                    "Faqat migratsiyalarni o'chiradi"
                },
                "Interceptors EF Core-ning SQL bajarish va SaveChanges quvuriga suqilib kirish (interception) va audit/logging qo'shish imkonini beradi."
            ),
            CreateQuestion(
                "EF Core 8+ da `ToJson()` yordamida kompleks ob'ektlar PostgreSQL yoki SQL Server-da JSON ustun sifatiga qanday mapping qilinadi?",
                "modelBuilder.Entity<User>().OwnsOne(u => u.Profile, b => b.ToJson());",
                new List<string> {
                    "Ob'ekt va kolleksiyalarni bitta JSONB/JSON ustuniga avtomatik serializatsiya qilib saqlaydi va LINQ orqali ichki elementlarini filtrlaydi",
                    "JSON faylini diskka saqlaydi",
                    "Faqat string turlarida ishlaydi",
                    "ChangeTracker-ni o'chirib qo me me'yor qo'yadi"
                },
                "ToJson() murakkab obyekt va ierarxiyalarni bazadagi bitta JSON ustuniga moslab LINQ so'rovlariga ruxsat beradi."
            ),
            CreateQuestion(
                "EF Core-da `DbContext` ob'ektining holat saqlash xususiyati (Stateful nature) va uning scoped lifetimes-ga bog me'yorligi nimada?",
                "// DbContext is NOT thread-safe!",
                new List<string> {
                    "DbContext thread-safe emas va bir vaqtning o'zida parallel thread-lar tomonidan chaqirilsa InvalidOperationException va state corruption beradi",
                    "DbContext butun ilova bo'ylab Singleton bo'lishi shart",
                    "DbContext parallel so'rovlarni avtomatik navbatga qo'yadi",
                    "DbContext har doim static o'zgaruvchida saqlanishi kerak"
                },
                "DbContext thread-safe emas. U bir vaqtning o'zida faqat 1 ta thread tomonidan ishlatilishi lozim (Scoped lifetime)."
            ),
            CreateQuestion(
                "EF Core-da Inheritance Mapping (Vorislikni jadvallarga moslash): TPH, TPT va TPC o'rtasidagi farq nimada?",
                "modelBuilder.Entity<Payment>().UseTphMappingStrategy();",
                new List<string> {
                    "TPH — barcha sinflar bitta jadvalda (Discriminator bilan); TPT — har bir sinf alohida jadvalda (JOIN); TPC — har bir konkret sinf alohida to'liq jadvalda",
                    "TPH faqat NoSQL bazalarda ishlaydi",
                    "TPT faqat 1 ta jadval hosil qiladi",
                    "TPC faqat abstract sinflarni saqlaydi"
                },
                "TPH (Table-per-Hierarchy) barcha ierarxiyani 1 jadvalda saqlaydi. TPT (Table-per-Type) har bir sinfga alohida jadval berib JOIN qiladi."
            ),
            CreateQuestion(
                "EF Core-da `ExecutionStrategy` (EnableRetryOnFailure) nima beradi?",
                "builder.Services.AddDbContext<QuizDbContext>(opt => opt.UseNpgsql(..., b => b.EnableRetryOnFailure()));",
                new List<string> {
                    "Tashqi ma'lumotlar bazasi ulanishidagi vaqtinchalik uzilishlarda (Transient faults) so'rovlarni avtomatik qayta urinib ko'radi (Retry)",
                    "Faqat migratsiyalarni qayta yaratadi",
                    "SQL so'rovlarini keshlaydi",
                    "DbContext pooling-ni o'chiradi"
                },
                "EnableRetryOnFailure tarmoq uzilishlari va transient DB error-larda SQL so'rovini avtomatik qayta bajaradi."
            )
        };
    }

    private static List<Question> GenerateEfCoreHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "EF Core-da `Compiled Queries` (`EF.CompileAsyncQuery`) qanday ishlaydi va u High-RPS tizimlarda LINQ overhead-ni qanday yo'qotadi?",
                "private static readonly Func<QuizDbContext, Guid, Task<Quiz>> GetQuizCompiled =\n    EF.CompileAsyncQuery((QuizDbContext ctx, Guid id) => ctx.Quizzes.First(q => q.Id == id));",
                new List<string> {
                    "LINQ Expression Tree-ni SQL-ga aylantirish (compilation) xarajatini 1 marta bajarib keshlaydi; Keyingi barcha so'rovlar 0-allocation bilan to'g'ridan-to'g'ri SQL yuboradi",
                    "EF Core migratsiyalarini o'chirish uchun ishlatiladi",
                    "Faqat In-Memory bazada ishlaydi",
                    "Faqat SQL Server Provider bilan ishlaydi"
                },
                "Compiled Queries LINQ tree parsing va SQL compilation xarajatlarini qayta o me me me'yorida bajarishdan xalos qiladi."
            ),
            CreateQuestion(
                "EF Core DbContext Pooling (`AddDbContextPool`) High-RPS ilovalarda qanday ishlaydi va uning cheklovi nimada?",
                "builder.Services.AddDbContextPool<QuizDbContext>(options => ...);",
                new List<string> {
                    "DbContext instansiyalarini qayta ishlatadi (recycle) va GC yukini kamaytiradi; Lekin DbContext ichida scoped holat (state) saqlash taqiqlanadi",
                    "Garbage Collection-ni to'xtatib qo'yadi",
                    "Faqat Singleton servislar ichida ishlaydi",
                    "Max 10 ta ulanish bilan cheklaydi"
                },
                "DbContextPool ob'ektlarni qayta ishlatib GC yukini kamaytiradi, shuning uchun u ichida state saqlamasligi kerak."
            ),
            CreateQuestion(
                "EF Core Query Compiler Plugin va Expression Tree Rewriting orqali dinamik LINQ optimizatsiyasi qanday amalga oshiriladi?",
                "public class CustomQueryableMethodTranslatingExpressionVisitor : QueryableMethodTranslatingExpressionVisitor",
                new List<string> {
                    "EF Core relatsion so'rovlar generatoriga suqilib kirib, LINQ daraxtini bazaga moslangan maxsus SQL funksiyalariga o'g me'yiradi",
                    "Faqat raw SQL string yozish uchun",
                    "Baza indekslarini o'chirish uchun",
                    "ChangeTracker-ni har soniyada tozalash uchun"
                },
                "Expression Tree Rewriting va Query Compiler plugin-lar LINQ expression-larni dinamik o'zgartirish va optimallashtirish imkonini beradi."
            ),
            CreateQuestion(
                "EF Core ChangeTracker Graph Tracking (Attach vs Update vs Entry.State) va ularning xotira unumdorligiga ta'siri nimada?",
                "dbContext.Attach(entity); // Marks Unchanged\ndbContext.Update(entity); // Marks ALL properties Modified!",
                new List<string> {
                    "Update() barcha xossalarni Modified deb belgilab ulkan UPDATE SQL beradi; Attach() faqat o'zgargan xossalarni kuzatadi va qisman UPDATE beradi",
                    "Attach barcha qatorlarni o'chiradi",
                    "Update faqat Primary Key-ni yangilaydi",
                    "Ikkalasi ham bir xil SQL yuboradi"
                },
                "Update() ob'ektning barcha ustunlarini modified qiladi. Attach() esa faqat haqiqiy o'zgargan property-larni UPDATE qiladi."
            ),
            CreateQuestion(
                "EF Core-da Custom Value Comparers (`HasConversion` bilan birga) qaysi holatda majburiy hisoblanadi?",
                "builder.Property(e => e.Numbers).HasConversion(v => string.Join(',', v), v => v.Split(',').ToList(), new ValueComparer<List<int>>(...));",
                new List<string> {
                    "Mutable kolleksiya yoki massiv kabi Reference Type-larni snapshot qilish va ularning ichki elementlari o'zgarganini (Change Tracking) to'g'ri solishtirish uchun",
                    "Faqat primitive int turlari uchun",
                    "Faqat primary key ustunlari uchun",
                    "Faqat Enum turlari uchun"
                },
                "Mutable reference type-larda EF Core snapshot va change tracking to'g'ri ishlashi uchun ValueComparer yozilishi shart."
            ),
            CreateQuestion(
                "EF Core Multi-Tenant ma'lumotlar bazalarida Dynamic Schema va Dynamic DbContext Interception qanday ishlaydi?",
                "public class TenantDbContext : DbContext { public override void OnConfiguring(...) { options.UseNpgsql(_tenantConnectionString); } }",
                new List<string> {
                    "Har bir tenant uchun so'rov kelganda connection string yoki schema-ni dinamik ravishda kerakli bazaga yo me'yirtiradi",
                    "Faqat bitta baza va jadvalda ishlaydi",
                    "Faqat static fayllar keshini tozalaydi",
                    "EF Core migratsiyalarini o'chiradi"
                },
                "Multi-Tenant DB arxitekturasida DbContext so'rov kelgan tenant ID-siga qarab dynamically kerakli connection string yoki schema-ga ulanadi."
            ),
            CreateQuestion(
                "EF Core-da Memory-mapped Large Binary Objects (BLOB) Streaming va `IDataReader` bilan ishlash qanday bajariladi?",
                "using var reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess);",
                new List<string> {
                    "CommandBehavior.SequentialAccess orqali ulkan fayl va BLOB-larni RAM-ga to'liq yuklamasdan stream sifatida bo'laklab o'qiydi",
                    "Faqat string turlarni o'qiydi",
                    "Faqat In-Memory test bazalarida ishlaydi",
                    "Faqat 1 KB fayllarni o me'qiydi"
                },
                "SequentialAccess ulkan BLOB va fayllarni xotiraga to'liq yuklamay, stream bo me me'yori bo'laklab o'qishga ruxsat beradi."
            ),
            CreateQuestion(
                "EF Core-da High-Scale Transactions & Savepoints (`IDbContextTransaction.CreateSavepointAsync`) qanday ishlaydi?",
                "await transaction.CreateSavepointAsync(\"BeforeUpdate\");\n// ... \nawait transaction.RollbackToSavepointAsync(\"BeforeUpdate\");",
                new List<string> {
                    "Butun tranzaksiyani bekor qilmasdan, faqat ma'lum bir oraliq nuqtaga (Savepoint) bekor qilish (partial rollback) imkonini beradi",
                    "Faqat Read Committed izolyatsiyada ishlaydi",
                    "Faqat SQLite-da ishlaydi",
                    "Tranzaksiyani darhol commit qiladi"
                },
                "Savepoints katta tranzaksiyalar ichida qisman rollback qilish va xatolik bergan joygacha qaytish imkonini beradi."
            ),
            CreateQuestion(
                "EF Core custom Relational Database Provider arxitekturasida `IQuerySqlGeneratorFactory` vazifasi nimadan iborat?",
                "public class CustomNpgsqlQuerySqlGenerator : NpgsqlQuerySqlGenerator",
                new List<string> {
                    "LINQ so'rovlar daraxtini (Query AST) mos ma'lumotlar bazasining o me me me'ziga xos dialekti bo'yicha SQL matniga o'g me'yiradi",
                    "Faqat baza parolini shifrlaydi",
                    "Faqat migratsiyalarni o me'chiradi",
                    "Faqat In-Memory kesh saqlaydi"
                },
                "IQuerySqlGeneratorFactory relatsion provider-da LINQ daraxtini aniq SQL string-ga o'g'irish uchun javobgardir."
            ),
            CreateQuestion(
                "EF Core va Testcontainers (Docker-based Integration Testing) testlash arxitekturasining In-Memory Provider-ga nisbatan afzalligi nimada?",
                "var container = new PostgreSqlBuilder().Build(); await container.StartAsync();",
                new List<string> {
                    "In-Memory Provider haqiqiy relatsion baza (SQL syntax, foreign keys, triggers) ni simulyatsiya qila olmaydi; Testcontainers real PostgreSQL Docker konteynerida 100% haqiqiy test o'tkazadi",
                    "In-Memory Provider real bazadan tezroq ishlaydi",
                    "Testcontainers testlarni o'chirib yuboradi",
                    "Ikkalasi ham mutlaqo bir xil ishlaydi"
                },
                "In-Memory EF provider relatsion bazaga xos munosabat va SQL cheklovlarini tekshirmaydi. Real Docker Testcontainers esa 100% aniq integratsion test beradi."
            )
        };
    }
}
