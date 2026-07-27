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
        return new List<Question>
        {
            CreateQuestion("EF Core-da AsNoTracking() metodidan foydalanishning asosiy afzalligi nimada va u qaysi holatlarda ishlatiladi?",
                new List<string> {
                    "DbContext ChangeTracker snapshot saqlamaydi va ob'ektlarni kuzatmaydi; Faqat o'qish (read-only) so'rovlarida xotira va tezlikni sezilarli oshiradi",
                    "Ma'lumotlar bazasida jadvalga avtomatik ravishda write-lock qo'yadi",
                    "Tranzaksiyani bekor qiladi va ma'lumotlarni o'chirib tashlaydi",
                    "LINQ so'rovini SQL ga o'girmasdan xotirada bajaradi"
                },
                "AsNoTracking() EF Core ga ob'ektlarni ChangeTracker snapshot-larida saqlamaslikni aytadi, bu faqat o'qish (read-only) so'rovlarida xotira va unumdorlikni sezilarli oshiradi."),

            CreateQuestion("EF Core-da N+1 so'rovlar muammosi (N+1 query problem) qanday kelib chiqadi va uni oldini olishning to'g'ri usuli qaysi?",
                new List<string> {
                    "Include() yoki ThenInclude() orqali Eager Loading qo'llash yoki Projection (.Select) yozish",
                    "Barcha jadvallarni bitta katta In-Memory List ga yuklab olish",
                    "DbContext obyektini har bir loop ichida qayta yaratish",
                    "AsNoTracking() ni o'chirish va SaveChangesAsync() ni chaqirish"
                },
                "N+1 muammosi bog'langan ma'lumotlar tsiklda har safar alohida SQL so'rovi bilan o'qilganda kelib chiqadi. Uni Include() yoki explicit Projection (.Select) orqali 1 ta SQL ga birlashtirish kerak."),

            CreateQuestion("EF Core-da migratsiya yaratish va ma'lumotlar bazasini dastur ishga tushganda avtomatik yangilash (migration apply) qanday bajariladi?",
                new List<string> {
                    "Database.MigrateAsync() metodi bajarilmagan migratsiyalarni aniqlab bazaga avtomatik SQL sifatida qo'llaydi",
                    "EnsureCreatedAsync() va MigrateAsync() bir vaqtda chaqirilishi shart",
                    "Migratsiyalar faqat visual studio oynasidan bajariladi",
                    "Database.EnsureDeletedAsync() chaqiriladi"
                },
                "Database.MigrateAsync() bajarilmagan EF Core migratsiyalarini aniqlaydi va ma'lumotlar bazasiga xavfsiz tatbiq etadi."),

            CreateQuestion("EF Core Fluent API-da OnModelCreating metodi nima uchun ishlatiladi?",
                new List<string> {
                    "Entity munosabatlarini (1:N, N:M), indekslarni, kalitlarni va jadval nomlarini aniq konfiguratsiya qilish uchun",
                    "Faqat ma'lumotlar bazasi parolini saqlash uchun",
                    "Faqat Controller marshrutlarini sozlash uchun",
                    "Faqat brauzer keshini tozalash uchun"
                },
                "OnModelCreating Fluent API orqali ma'lumotlar bazasi sxemasi, indekslar va munosabatlarni moslashtirish imkonini beradi."),

            CreateQuestion("EF Core-da DbSet<T>.FindAsync(id) va FirstOrDefaultAsync(x => x.Id == id) o'rtasidagi asosiy farq nima?",
                new List<string> {
                    "FindAsync avval DbContext ChangeTracker keshidan qidiradi; Agar topilsa SQL so'rov yubormaydi. FirstOrDefaultAsync esa har doim bazaga SQL yuboradi",
                    "FirstOrDefaultAsync keshdan qidiradi, FindAsync esa har doim bazaga so'rov yuboradi",
                    "FindAsync faqat string ID-lar bilan ishlaydi",
                    "Ikkala metod ham bir xil ishlaydi"
                },
                "FindAsync() birlamchi kalit bo'yicha avval ChangeTracker lokal keshini tekshiradi, topilsa SQL so'rovini tejaydi."),

            CreateQuestion("EF Core-da Cascading Delete (Kaskadli o'chirish) sozlamasi qanday vazifa bajaradi?",
                new List<string> {
                    "Ota ob'ekt (masalan Quiz) o'chirilganda, unga bog'liq barcha bola ob'ektlar (Questions) avtomatik ravishda o'chiriladi",
                    "Ota ob'ekt o'chirilganda bola ob'ektlar o'zgarmasdan qoladi",
                    "Ota ob'ektni o'chirishni taqiqlaydi va exception beradi",
                    "Bola ob'ektlarni boshqa jadvalga ko'chiradi"
                },
                "DeleteBehavior.Cascade ota entity o'chganda unga bog'liq barcha child entitiylarni avtomatik bazadan o'chiradi."),

            CreateQuestion("EF Core-da Shadow Properties (soya xususiyatlar) nimani anglatadi?",
                new List<string> {
                    "C# entity sinfida prop sifatida mavjud bo'lmagan, lekin EF Core modelida va ma'lumotlar bazasi jadvalida saqlanadigan ustunlar",
                    "Faqat o'chirilgan obyektlar keshda saqlanadigan joy",
                    "Faqat SQL Server-da ishlaydigan vaqtinchalik jadval",
                    "Entity-ning maxfiy paroli"
                },
                "Shadow Property C# sinfida aniqlanmagan, ammo EF Core va DB jadvalida mavjud bo'lgan ustundir."),

            CreateQuestion("EF Core-da Value Converters (HasConversion) nima uchun ishlatiladi?",
                new List<string> {
                    "C# tiplarini (masalan Enum, Custom Class) ma'lumotlar bazasi tiplariga (masalan string, int, JSON) o'girib saqlash va qayta o'qish uchun",
                    "SQL so'rovlarini avtomatik shifrlash uchun",
                    "Faqat DateTime formatini o'zgartirish uchun",
                    "DbContext pooling-ni yoqish uchun"
                },
                "Value Converters C# property qiymatini bazaga saqlash va o'qish vaqtida tip shaklini o'zgartiradi (masalan Enum -> string)."),

            CreateQuestion("EF Core-da Keyless Entity Types (HasNoKey) qaysi holatlarda qo'llaniladi?",
                new List<string> {
                    "Birlamchi kaliti (Primary Key) bo'lmagan SQL View-lar yoki Stored Procedure natijalarini mapping qilish uchun",
                    "Faqat Primary Key bo'lgan jadvallarda",
                    "Faqat In-Memory test bazalarida",
                    "EF Core migratsiyalarini o'chirish uchun"
                },
                "HasNoKey() Primary Key ga ega bo'lmagan SQL View yoki raw SQL natijaviy strukturalarini xartagrafiya qilishda qo'llaniladi."),

            CreateQuestion("EF Core-da Explicit Loading (Aniq yuklash) qanday amalga oshiriladi?",
                new List<string> {
                    "Allaqachon yuklangan ota ob'ekt uchun bog'liq kolleksiya yoki navigatsiya xossasini keyinchalik talabga ko'ra .LoadAsync() bilan o'qish",
                    "Include() metodi bilan bir xil avtomatik yuklash",
                    "DbContext-ni qayta yaratish orqali yuklash",
                    "AsNoTracking() bilan birga ishlatiladigan avtomatik o'qish"
                },
                "Explicit Loading allaqachon xotiradagi entity uchun `Entry(entity).Collection(...).LoadAsync()` orqali bog me'langan ma'lumotni alohida yuklaydi."),

            CreateQuestion("ASP.NET Core DI konteynerida AddDbContext<T>() metodining standart umr ko'rish davomiyligi (Lifetime) qanday?",
                new List<string> {
                    "Scoped (Har bir HTTP so'rovi uchun 1 ta DbContext instansiyasi)",
                    "Singleton (Butun dastur uchun 1 ta umumiy instansiya)",
                    "Transient (Har bir chaqirilganda yangi instansiya)",
                    "Global static instansiya"
                },
                "AddDbContext DbContext-ni Scoped lifetimeda ro'yxatdan o'tkazadi. Singleton qilish multithread toqnashuvi va xotira sizishiga olib keladi."),

            CreateQuestion("EF Core-da Data Annotations ([Key], [Required], [MaxLength]) va Fluent API o'rtasidagi farq nima?",
                new List<string> {
                    "Data Annotations atributlar orqali Entity sinfida yoziladi; Fluent API esa OnModelCreating metodida ajratilgan holda ko me'proq imkoniyat beradi",
                    "Data Annotations har doim Fluent API-dan ustun turadi",
                    "Fluent API faqat SQL Server bilan ishlaydi",
                    "Ular o'rtasida imkoniyat bo'yicha farq yo'q"
                },
                "Fluent API barcha murakkab konfiguratsiyalarni qo'llaydi va Domain Entity-ni ortiqcha atributlardan toza (Clean Code) saqlashga imkon beradi."),

            CreateQuestion("EF Core-da Navigation Properties (Collection va Reference Navigation) qanday rol o'ynaydi?",
                new List<string> {
                    "Jadvallar o'rtasidagi xorijiy kalit (FK) bog'liqliklarini C# kodida ob'ektlar havolasi (masalan Order.User yoki User.Orders) sifatida bog'laydi",
                    "Faqat HTML ranglarini o'zgartiradi",
                    "Faqat LINQ so'rovlarini bekor qiladi",
                    "Faqat Primary Key turini beradi"
                },
                "Navigation properties relational bazadagi Foreign Key bog me me'liqligini C# ob'ektlar modelida ifodalash uchun xizmat qiladi."),

            CreateQuestion("EF Core-da DbSet.AddAsync() va oddiy DbSet.Add() o'rtasidagi farq va qachon AddAsync ishlatilishi kerak?",
                new List<string> {
                    "AddAsync faqat ma me'lumotlar bazasida maxsus async Value Generator (masalan HiLo sequence) ishlatilganda kerak; Oddiy holatda Add() yetarli",
                    "AddAsync har doim ishlatilishi majburiy",
                    "DbSet.Add() xotirani tozalab yuboradi",
                    "AddAsync SQL so'rovini darhol bazaga yuboradi"
                },
                "AddAsync faqat async hi-lo value generator bo'lganda kerak. Odatda memory tracker-ga qo'shish uchun sinxron `Add()` ishlatilishi tavsiya etiladi."),

            CreateQuestion("EF Core-da SaveChangesAsync() chaqirilganda qanday jarayon yuz beradi?",
                new List<string> {
                    "ChangeTracker-dagi barcha Added, Modified, Deleted ob'ektlar uchun avtomatik SQL INSERT/UPDATE/DELETE so'rovlari shakllantirilib tranzaksiyada bajariladi",
                    "Faqat kesh tozalanadi, bazaga yozilmaydi",
                    "Barcha jadvallar drop qilinadi",
                    "Faqat SELECT so me me'rovi yuboriladi"
                },
                "SaveChangesAsync ChangeTracker topgan barcha o'zgarishlarni bitta SQL transaction ichida bazaga saqlaydi va vaqtinchalik ID-larni yangilaydi."),

            CreateQuestion("EF Core-da Entity State (Detached, Unchanged, Added, Modified, Deleted) nimani anglatadi?",
                new List<string> {
                    "Obyektning DbContext ChangeTracker bilan aloqasi va u ustidan SaveChangesAsync-da qanday SQL amali bajarilishini belgilovchi holat",
                    "Faqat C# o'zgaruvchi turini",
                    "Faqat database jadvalining nomini",
                    "Faqat LINQ so'rov natijasini"
                },
                "EntityState ChangeTracker har bir ob'ektni kuzatish holatini ifodalaydi va qaysi SQL DML buyrug'i bajarilishini aniqlaydi."),

            CreateQuestion("EF Core-da Database Providers (Npgsql, SqlServer, InMemory) qanday vazifa bajaradi?",
                new List<string> {
                    "EF Core-ning abstrakt LINQ va ChangeTracker so'rovlarini muayyan ma'lumotlar bazasining (PostgreSQL, MS SQL, SQLite) o'ziga xos SQL dialektiga o'giradi",
                    "Faqat C# kompilyatsiyasini bajaradi",
                    "Faqat HTML render qiladi",
                    "Faqat JSON fayllarni siqadi"
                },
                "Database Provider EF Core va muayyan SQL ma'lumotlar bazasi o'rtasida ko'prik bo'lib LINQ so me me'rovlarini tegishli SQL dialektiga o'g'iradi."),

            CreateQuestion("EF Core-da Database.ExecuteSqlRawAsync() va DbSet.FromSqlRaw() o me me'rtasidagi farq nima?",
                new List<string> {
                    "FromSqlRaw SQL query natijasini Entity-larga map qilib IQueryable qaytaradi; ExecuteSqlRawAsync esa UPDATE/DELETE kabi DML buyruqlarini bajarib ta'sirlangan qatorlar sonini beradi",
                    "FromSqlRaw faqat UPDATE bajaradi",
                    "ExecuteSqlRawAsync entity to me'plamini qaytaradi",
                    "Ikkala metod bir xil ishlaydi"
                },
                "FromSqlRaw SQL-dan entity o'qish uchun, ExecuteSqlRawAsync esa INSERT/UPDATE/DELETE buyruqlarini to me me me'g'ridan-to'g'ri bajarish uchun ishlatiladi."),

            CreateQuestion("EF Core-da Soft Delete pattern (Yumshoq o me'chirish) Global Query Filter bilan qanday amalga oshiriladi?",
                new List<string> {
                    "Entity-da IsDeleted bo'yicha HasQueryFilter(x => !x.IsDeleted) qo'yiladi va bazadan jismonan o me me me'chirish o me'rniga IsDeleted = true qilinadi",
                    "Database.EnsureDeletedAsync chaqiriladi",
                    "ChangeTracker mutlaqo o me me'chiriladi",
                    "Faqat SQL Server-da ishlaydi"
                },
                "Soft Delete-da fiziki DELETE o'rniga `IsDeleted = true` qilinadi va Global Query Filter avtomatik barcha SELECT so me'rovlarda `WHERE IsDeleted = false` qo me'shadi."),

            CreateQuestion("EF Core DbContext ChangeTracker-ning Entries() va HasChanges() metodlari nima uchun kerak?",
                new List<string> {
                    "DbContext tomonidan kuzatilayotgan barcha ob'ektlar holatini (State) ko'rish va saqlanmagan o'zgarishlar borligini tekshirish uchun",
                    "Faqat keshni tozalash uchun",
                    "Faqat parollarni shifrlash uchun",
                    "Faqat migratsiya fayllarini yaratish uchun"
                },
                "ChangeTracker.Entries() kuzatuvdagi entitiylar ro me me'yxatini, HasChanges() esa saqlanmagan o'zgarishlar bor-yo me'qligini tekshiradi."),

            CreateQuestion("EF Core-da Composite Primary Key (Ko'p ustunli birlamchi kalit) Fluent API-da qanday ko'rsatiladi?",
                new List<string> {
                    "modelBuilder.Entity<OrderItem>().HasKey(x => new { x.OrderId, x.ProductId });",
                    "modelBuilder.Entity<OrderItem>().HasPrimaryKey(\"OrderId\", \"ProductId\");",
                    "[Key] atributini ikkala property-ga qo'yish orqali",
                    "Composite key-larni yaratib bo'lmaydi"
                },
                "Composite Primary Key-lar Fluent API-da `HasKey(x => new { x.Key1, x.Key2 })` anonim ob'ekti orqali beriladi."),

            CreateQuestion("EF Core-da HasDefaultValue() va HasDefaultValueSql() o'rtasidagi farq nima?",
                new List<string> {
                    "HasDefaultValue qat'iy konstant qiymat (masalan 10) beradi; HasDefaultValueSql esa SQL funksiyasini (masalan GETDATE() yoki CURRENT_TIMESTAMP) biriktiradi",
                    "HasDefaultValueSql faqat C# funksiyasini chaqiradi",
                    "HasDefaultValue faqat string-lar uchun",
                    "Ikkala metod ham bir xil"
                },
                "HasDefaultValue C# qiymatini beradi. HasDefaultValueSql esa bazaning uzviy funksiyalarini (CURRENT_TIMESTAMP) SQL darajasida biriktiradi."),

            CreateQuestion("EF Core-da Table Splitting nimani anglatadi?",
                new List<string> {
                    "Bir nechta har xil C# Entity sinflarini ma'lumotlar bazasidagi bitta umumiy SQL jadvaliga mapping qilish",
                    "Bitta Entity-ni 10 ta jadvalga bo me me me'lish",
                    "Jadvalni har kuni o me me me'chirish",
                    "Faqat In-Memory bazada ishlaydi"
                },
                "Table Splitting o'rtasida 1:1 bog'liqlik bo'lgan bir nechta C# entity-larni (masalan Order va OrderDetail) bitta fiziki DB jadvaliga mapping qiladi."),

            CreateQuestion("EF Core-da Owned Entity Types (OwnsOne / OwnsMany) qaysi arxitekturaviy konsept uchun ishlatiladi?",
                new List<string> {
                    "Domain-Driven Design (DDD) da o'z unikal ID-siga ega bo'lmagan Value Object-larni ota Entity jadvaliga ustunlar sifatda saqlash uchun",
                    "Faqat Primary Key yaratish uchun",
                    "Faqat SQL View-lar uchun",
                    "Faqat migratsiyalarni o'chirish uchun"
                },
                "OwnsOne/OwnsMany Value Object-larni (masalan Address, Money) alohida ID siz, ota entity jadvaliga o me me me'rnatilgan (embedded) ustunlar sifatida mapping qiladi."),

            CreateQuestion("EF Core Inheritance Mapping strategiyalarida Table-per-Hierarchy (TPH) qanday ishlaydi?",
                new List<string> {
                    "Vorislik ierarxiyasidagi barcha sinflarni bitta jadvalga saqlaydi va qaysi sinf ekanligini Discriminator ustuni orqali ajratadi",
                    "Har bir voris sinf uchun alohida jadval ochadi",
                    "Vorislikni taqiqlaydi",
                    "Faqat abstract class-larda ishlaydi"
                },
                "TPH (EF Core default) butun vorislik ierarxiyasini 1 ta jadvalga saqlaydi va `Discriminator` ustuni orqali tipni ajratadi."),

            CreateQuestion("EF Core SaveChangesAsync metodini override qilib avtomatik Audit Logging (CreatedAt, UpdatedAt) yaratish qanday bajariladi?",
                new List<string> {
                    "ChangeTracker.Entries<IAuditableEntity>() orqali Added va Modified state-dagi entitiylarni ushlab, ularning vaqt ustunlarini avtomatik yangilash",
                    "Faqat SQL Trigger yaratish orqali",
                    "Faqat Controller-da qo'lda berish orqali",
                    "Faqat brauzer taymerini ishlatish orqali"
                },
                "DbContext.SaveChangesAsync ni override qilib `ChangeTracker.Entries<IAuditableEntity>()` orqali `CreatedAt` va `UpdatedAt` atributlarini avtomatik to'ldirish mumkin."),

            CreateQuestion("EF Core-da IEntityTypeConfiguration<T> interfeysining afzalligi nimada?",
                new List<string> {
                    "Har bir Entity Fluent API konfiguratsiyasini alohida toza sinflarga (Clean Code) ajratib, OnModelCreating-ni ixcham tutish imkonini beradi",
                    "Faqat migratsiya SQL faylini o'chiradi",
                    "DbContext pooling-ni o me me'chiradi",
                    "Faqat SQLite bilan ishlaydi"
                },
                "IEntityTypeConfiguration<T> har bir entity konfiguratsiyasini alohida faylga ajratib `modelBuilder.ApplyConfigurationsFromAssembly()` orqali yuklashga imkon beradi."),

            CreateQuestion("EF Core-da Database.BeginTransactionAsync() yordamida eksplitsit tranzaksiya boshqarish qachon kerak bo'ladi?",
                new List<string> {
                    "Bir nechta SaveChangesAsync chaqiruvlarini yoki raw SQL operatsiyalarini bitta umumiy baza tranzaksiyasiga birlashtirish zarur bo'lganda",
                    "AsNoTracking so'rovlarida",
                    "Faqat 1 ta SELECT so'rovida",
                    "DbContext barpo etilayotganda"
                },
                "Agar operatsiya bir nechta SaveChangesAsync yoki raw SQL so me me'rovlaridan iborat bo'lsa, eksplitsit transaction va Commit/Rollback kerak bo'ladi."),

            CreateQuestion("EF Core-da AsNoTrackingWithIdentityResolution() va oddiy AsNoTracking() o'rtasidagi farq nima?",
                new List<string> {
                    "AsNoTrackingWithIdentityResolution so'rov natijasida takrorlanayotgan bir xil entity ob'ektlarini xotirada duplikatsiya qilmasdan yagona reference qilib beradi",
                    "AsNoTrackingWithIdentityResolution har doim sekinroq",
                    "AsNoTracking ob'ektlarni keshlaydi",
                    "Ular bir xil ishlaydi"
                },
                "AsNoTrackingWithIdentityResolution ChangeTracker saqlamaydi, lekin N:M yoki 1:N so'rovlarda takroriy entity-larning xotiradagi nusxasini birlashtiradi."),

            CreateQuestion("EF Core Fluent API-da HasIndex() va IsUnique() yordamida indeks yaratish kodi qanday yoziladi?",
                new List<string> {
                    "modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();",
                    "modelBuilder.Entity<User>().CreateIndex(u => u.Email);",
                    "modelBuilder.Entity<User>().Property(u => u.Email).SetUniqueIndex();",
                    "Indekslarni Fluent API-da yaratib bo'lmaydi"
                },
                "EF Core Fluent API-da `HasIndex(...)` va `.IsUnique()` metodlari unikal yoki oddiy DB indekslarini yaratish uchun qo me'llaniladi.")
        };
    }

    private static List<Question> GenerateEfCoreMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("EF Core Query Splitting (AsSplitQuery() vs AsSingleQuery()) qaysi muammoni hal qiladi?",
                new List<string> {
                    "Bir nechta Include() bo'lganda SQL-dagi Cartesian Explosion (ma'lumotlar ko'payib ketishi) ni oldini olish uchun so'rovni bir nechta bog'liq SQL ga bo'lib o'qiydi",
                    "DbContext-ni 2 ga bo'lib beradi",
                    "AsNoTracking-ni o'chirib qo'yadi",
                    "Faqat In-Memory bazada ishlaydi"
                },
                "AsSplitQuery() ko'plab Include-lar bo'lganda bitta ulkan Cartesian product SQL o'rniga har bir bog me me'liq jadval uchun alohida SQL so me'rov yuboradi."),

            CreateQuestion("EF Core-da Optimistic Concurrency Control (Bir vaqtda ma'lumot o'zgartirish) [ConcurrencyCheck] yoki IsConcurrencyToken() bilan qanday ishlaydi?",
                new List<string> {
                    "UPDATE bajarilayotganda WHERE shartiga eski qiymat (RowVersion) qo'shiladi; Agar boshqa kishi o'zgartirgan bo'lsa DbUpdateConcurrencyException otiladi",
                    "Jadvalga to'liq Exclusive Lock qo'yadi",
                    "So'rovni bekor qilib dasturni yopadi",
                    "Faqat PostgreSQL-da ishlaydi"
                },
                "Optimistic Concurrency `WHERE Version = oldVersion` sharti orqali boshqa foydalanuvchi ma'lumotni o me'zgartirganini aniqlaydi va Exception beradi."),

            CreateQuestion("EF Core Global Query Filters (HasQueryFilter) nima uchun kerak va uni vaqtincha qanday o'chirish mumkin?",
                new List<string> {
                    "Soft Delete (IsDeleted) va Multi-Tenancy filtrlarni avtomatik barcha query-larga qo'shish uchun; IgnoreQueryFilters() orqali vaqtincha o'chiriladi",
                    "IgnoreQueryFilters() bazani tozalaydi",
                    "HasQueryFilter faqat Primary Key-ni filtrlaydi",
                    "Global filters-ni o'chirib bo'lmaydi"
                },
                "HasQueryFilter barcha so me me me'rovlarga avtomatik shart qo me'shadi. Admin so me me'rovlarida `IgnoreQueryFilters()` bilan o'chirish mumkin."),

            CreateQuestion("EF Core Interceptors (DbCommandInterceptor, SaveChangesInterceptor) qanday vazifalarni bajaradi?",
                new List<string> {
                    "SQL buyruqlari bazaga yuborilishidan oldin yoki SaveChanges chaqirilganda ularni ushlab loglash, audit yuritish yoki SQL-ni o'zgartirish",
                    "Faqat migratsiyani bajarish uchun",
                    "Faqat Controller-larni ulash uchun",
                    "Faqat CSS stillarini berish uchun"
                },
                "Interceptors EF Core quvurlarida (pipeline) SQL buyruqlarini va SaveChanges amallarini ushlab profilaktika va audit yuritish imkonini beradi."),

            CreateQuestion("EF Core-da DbUpdateConcurrencyException yuz berganda xatolikni bartaraf etish (Concurrency Resolution) qanday bajariladi?",
                new List<string> {
                    "Exception ichidagi EntityEntry orqali DatabaseValues (bazadagi yangi qiymat) ni olib, ClientValues bilan solishtirish va qayta SaveChangesAsync qilish",
                    "Dasturni majburiy qayta tushirish",
                    "Barcha ma'lumotlarni o'chirib tashlash",
                    "Concurrency resolution-ni ilojisi yo'q"
                },
                "DbUpdateConcurrencyException ushlangach, `entry.GetDatabaseValuesAsync()` orqali bazadagi yangi holat olinib mojaro hal etiladi (Client Wins / Database Wins)."),

            CreateQuestion("EF Core-da Value Comparers (HasConversion bilan ValueComparer<T>) qachon talab etiladi?",
                new List<string> {
                    "O'zgaruvchan (Mutable) kolleksiyalar yoki class-lar (masalan List<string> yoki Array) ChangeTracker tomonidan to'g'ri solishtirilishi uchun",
                    "Faqat int va string turlarida",
                    "Faqat Primary Key bo'lganda",
                    "Value Comparers EF Core-da mavjud emas"
                },
                "Mutable turlarda (masalan List<T>) EF Core tarkibiy o me'zgarishlarni sezishi uchun moslashtirilgan `ValueComparer` kerak bo'ladi."),

            CreateQuestion("EF Core Inheritance Mapping strategiyalarida TPT (Table-per-Type) va TPC (Table-per-Concrete-Type) o'rtasidagi farq nima?",
                new List<string> {
                    "TPT — har bir sinf alohida jadval va JOIN bilan bog'lanadi; TPC — har bir konkret sinf barcha ustunlarni saqlovchi mustaqil alohida jadvalga ega bo'ladi",
                    "TPT faqat 1 ta jadval saqlaydi",
                    "TPC har doim JOIN ishlatadi",
                    "Ular TPH bilan bir xil"
                },
                "TPT ota va bola sinflarni alohida jadvallarga bo'lib JOIN qiladi. TPC esa har bir konkret voris sinfga to'liq mustaqil jadval beradi."),

            CreateQuestion("EF Core 7+ da kiritilgan ExecuteUpdateAsync va ExecuteDeleteAsync (Bulk Operations) metodlarining ChangeTracker-ga ta'siri qanday?",
                new List<string> {
                    "Entitiylarni xotiraga yuklamasdan to'g'ridan-to'g'ri SQL UPDATE/DELETE yuboradi; ChangeTracker xotiradagi holatni yangilamaydi",
                    "ChangeTracker-dagi barcha ob'ektlarni avtomatik yangilaydi",
                    "Faqat 1 ta qatorni o'chira oladi",
                    "ExecuteDeleteAsync tranzaksiyani taqiqlaydi"
                },
                "ExecuteUpdateAsync/ExecuteDeleteAsync obyektlarni RAM-ga yuklamasdan 1 ta tezkor SQL yuboradi. Biroq ChangeTracker keshini o'zi yangilamaydi."),

            CreateQuestion("EF Core-da EF.Functions.Like() (yoki ILike) va C# String.Contains() o'rtasidagi SQL translation farqi nima?",
                new List<string> {
                    "EF.Functions.Like() to'g'ridan-to'g'ri SQL LIKE wildcards (%, _) bilan tarjima qilinadi; String.Contains esa wildcards-ni escapelaydi",
                    "String.Contains har doim tezroq",
                    "EF.Functions.Like faqat C# xotirasida ishlaydi",
                    "Ular bir xil SQL chiqaradi"
                },
                "EF.Functions.Like SQL dialektining o'ziga xos wildcard simvollarini (%, _) to me'g'ridan-to me'g'ri SQL so me me'rovga tarjima qiladi."),

            CreateQuestion("EF Core Client vs Server Evaluation (So'rovni serverda yoki klientda bajarish) ogohlantirishi nima va u qanday xavf tug'diradi?",
                new List<string> {
                    "LINQ so'rovi SQL-ga tarjima bo'la olmaganda C# metodlari sababli barcha ma'lumot xotiraga yuklanib (Client eval) unumdorlikni pasaytirishi",
                    "Server evaluation dasturni to'xtatadi",
                    "Client evaluation har doim tezroq",
                    "EF Core-da bunday holat bo'lmaydi"
                },
                "Agar LINQ so me'rovida custom C# metodi ishlatilsa, EF Core so'rovni SQL ga tarjima qilolmay ma'lumotlarni RAM-ga yuklashga majbur bo'ladi."),

            CreateQuestion("EF Core-da FromSqlInterpolated va FromSqlRaw o'rtasidagi SQL Injection xavfsizligi bo'yicha farq nima?",
                new List<string> {
                    "FromSqlInterpolation String Interpolation ($) parametrlarini avtomatik DbParameter shaklida xavfsiz o me'tkazadi; FromSqlRaw esa ehtiyotsizlikda SQL Injection xavfini berishi mumkin",
                    "FromSqlRaw har doim parametrli ishlaydi",
                    "FromSqlInterpolated SQL Injection-ga zaif",
                    "Ikkala metod ham parametrsiz ishlaydi"
                },
                "FromSqlInterpolated '$'{var}'' parametrlarni avtomatik xavfsiz SQL DbParameter shakliga o'tkazib SQL Injection xavfini yo'qotadi."),

            CreateQuestion("EF Core 8 da kiritilgan Complex Types (ComplexType) Owned Entity Types-dan nimasi bilan ajralib turadi?",
                new List<string> {
                    "Complex Types unikal ID talab qilmaydi, har doim immutable Value Object hisoblanadi va shadow key hamda alohida table mapping-ga muhtoj emas",
                    "Complex Types alohida DB jadvali ochadi",
                    "Owned Entity Types ID talab qilmaydi",
                    "Ular o'rtasida farq yo'q"
                },
                "EF Core 8 Complex Types toza Value Object-lar bo'lib, unikal kalitsiz va ortiqcha shadow FK-larsiz ota entity jadvaliga mapping bo'ladi."),

            CreateQuestion("EF Core-da transaction.CreateSavepointAsync() nima beradi?",
                new List<string> {
                    "Mavjud baza tranzaksiyasi ichida oraliq saqlash nuqtasi (Savepoint) o'rnatib, xatolik bo'lganda faqat shu nuqtagacha ROLLBACK qilish imkonini beradi",
                    "Tranzaksiyani to'liq commit qiladi",
                    "DbContext pooling-ni o me me'chiradi",
                    "Savepoint-larni EF Core qo'llamaydi"
                },
                "CreateSavepointAsync murakkab tranzaksiyalar ichida oraliq nuqtalar berib, qisman bekor qilish imkonini beradi."),

            CreateQuestion("EF Core Multi-Tenant ilovalarda Tenant izolyatsiyasini Global Query Filter va Scoped Tenant Service bilan ta'minlash qanday ishlaydi?",
                new List<string> {
                    "DbContext-ga Scoped ITenantService inject qilinadi va HasQueryFilter(x => x.TenantId == _tenantService.TenantId) orqali avtomatik ajratiladi",
                    "Tenant ID har safar hand-code yozilishi shart",
                    "Multi-tenancy faqat alohida bazalarda bo'ladi",
                    "Global filter tenant-ni ajrata olmaydi"
                },
                "Scoped ITenantService joriy so'rov TenantId-sini beradi va Global Query Filter avtomatik barcha query-larga `TenantId` filtrini uradi."),

            CreateQuestion("EF Core-da DbContext.Attach() va DbContext.Update() o'rtasidagi me me'moriy farq nima?",
                new List<string> {
                    "Attach ob'ektni Unchanged holatida ChangeTracker-ga ulaydi; Update esa ob'ekt va uning barcha navigatsiya a'zolarini Modified deb belgilaydi",
                    "Attach barcha property-larni Modified qiladi",
                    "Update faqat 1 ta ustunni yangilaydi",
                    "Ikkala metod bir xil ishlaydi"
                },
                "Update() butun ob me me'ekt grafigini Modified deydi. Attach() esa Unchanged deb ulaydi va faqat o'zgargan property-ni hand-code Modified qilish imkonini beradi."),

            CreateQuestion("EF Core Migration Bundles (dotnet ef migrations bundle) CI/CD deployment jarayonida nima uchun ishlatiladi?",
                new List<string> {
                    "Migratsiyalarni barcha bog'liqliklari bilan yagona mustaqil (Self-contained executable) faylga yig me me'ib, prod bazaga .NET SDK siz tatbiq etish uchun",
                    "Faqat C# kodini siqish uchun",
                    "Faqat In-Memory bazani yaratish uchun",
                    "Faqat Visual Studio oynasida ishlaydi"
                },
                "Migration Bundles CI/CD pipeline-da .NET SDK yoki manba kodlarisiz prod bazaga migratsiyani ijro etuvchi alohida binary executable fayl beradi."),

            CreateQuestion("PostgreSQL JSONB ustunlarini EF Core 7/8 da ToJson() orqali mapping qilish va LINQ so'rov yozish qanday bajariladi?",
                new List<string> {
                    "modelBuilder.Entity<User>().OwnsOne(u => u.Address, a => a.ToJson()); deb ko'rsatiladi va LINQ orqali u.Address.City deb to'g'ridan-to'g'ri so'rov beriladi",
                    "ToJson() faqat string saqlaydi va LINQ ishlamaydi",
                    "PostgreSQL JSONB-ni EF Core qo'llamaydi",
                    "ToJson() faqat XML formatda ishlaydi"
                },
                "EF Core ToJson() Fluent API orqali C# ob'ektlarini PostgreSQL JSONB ustuniga o me'raydi va LINQ so me'rovlarini to'g'ridan-to me'g'ri JSON ichiga tarjima qiladi."),

            CreateQuestion("EF Core-da Connection Resiliency va Execution Strategies (EnableRetryOnFailure) qaysi muammoni hal qiladi?",
                new List<string> {
                    "Tarmoq uzilishi yoki bazadagi vaqtinchalik xatolar (Transient errors) yuz berganda so'rovni eksponentsial kechikish bilan avtomatik qayta urinadi (Retry)",
                    "DbContext-ni qayta yaratadi",
                    "Faqat SQL sintaktik xatolarni tuzatadi",
                    "EnableRetryOnFailure tranzaksiyani taqiqlaydi"
                },
                "EnableRetryOnFailure cloud muhitlardagi vaqtinchalik tarmoq va baza uzilishlarida so'rovlarni avtomatik qayta yuboradi."),

            CreateQuestion("EF Core-da Self-Referencing Entity (O'z-o'ziga bog'langan ierarxiya, masalan Category.Parent) mapping-i qanday sozlanadi?",
                new List<string> {
                    "modelBuilder.Entity<Category>().HasOne(c => c.Parent).WithMany(c => c.Children).HasForeignKey(c => c.ParentId);",
                    "modelBuilder.Entity<Category>().HasSelfReference();",
                    "Self-referencing entity-larni yaratib bo me'lmaydi",
                    "Faqat N:M orqali yaratiladi"
                },
                "O'ziga bog'langan entity-lar `HasOne(c => c.Parent).WithMany(c => c.Children)` orqali 1:N ota-bola ierarxiyasida mapping qilinadi."),

            CreateQuestion("Unit Testing uchun EF Core In-Memory Database va SQLite In-Memory o me'rtasidagi tanlov mezoni nima?",
                new List<string> {
                    "EF Core In-Memory relatsion baza emas (FK/Constraints tekshirmaydi); SQLite In-Memory esa haqiqiy relatsion SQL baza sifatda ko'proq ishonchli",
                    "In-Memory har doim relatsion qoidalarga amal qiladi",
                    "SQLite In-Memory faqat Windows-da ishlaydi",
                    "Ular o'rtasida farq yo'q"
                },
                "EF Core In-Memory relatsion DB qoidalarini (FK constraint, raw SQL) qo me'llamaydi. SQLite In-Memory esa haqiqiy relatsion test muhiti beradi.")
        };
    }

    private static List<Question> GenerateEfCoreHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("EF Core-da DbContext Pooling (AddDbContextPool<T>()) ichki ishlash mexanizmi va unumdorlikka ta'siri nimada?",
                new List<string> {
                    "DbContext instansiyalarini har bir HTTP so'rovda qayta yaratmay, pool-da saqlab qayta ishlatadi (Reuse); O'ta yuqori so'rovlar oqimida allocations va GC overhead-ni keskin kamaytiradi",
                    "DbContext-ni har doim Singleton qilib qo'yadi",
                    "DbContext Pooling faqat In-Memory bazada ishlaydi",
                    "DbContext Pooling so'rovlarni sekinlashtiradi"
                },
                "AddDbContextPool DbContext obyektlarini qayta ishlatib GC allocations xarajatini kamaytiradi. Biroq DbContext ichida scoped state saqlamaslik lozim."),

            CreateQuestion("EF Core Compiled Queries (EF.CompileAsyncQuery()) qanday ishlaydi va qaysi holatlarda qo'llaniladi?",
                new List<string> {
                    "LINQ so'rovining Expression Tree-sini SQL-ga kompilyatsiya qilish vaqtini keshlab, bir xil SQL so me'rovi bot-bot chaqirilganda overhead-ni yo me me'qotadi",
                    "Compiled Queries so me'rovni C# xotirasida bajaradi",
                    "Compiled Queries so me'rovni faqat 1 marta bajaradi",
                    "EF Core-da Compiled queries mavjud emas"
                },
                "EF.CompileAsyncQuery LINQ expression tree-ni SQL parsing bosqichini aylanib o'tib, yuqori chastotali so'rovlarda tezlik beradi."),

            CreateQuestion("EF Core ExecuteUpdateAsync metodining DbContext ChangeTracker va SaveChangesAsync-dan me'moriy va unumdorlik farqi nimada?",
                new List<string> {
                    "ExecuteUpdateAsync entitiylarni RAM-ga yuklamasdan va ChangeTracker-dan o'tkazmasdan to'g'ridan-to'g'ri 1 ta SQL UPDATE yuboradi (Sub-millisecond bulk update)",
                    "ExecuteUpdateAsync har bir qator uchun alohida UPDATE yuboradi",
                    "SaveChangesAsync har doim ExecuteUpdateAsync-dan tezroq",
                    "ExecuteUpdateAsync faqat 1 ta entity bilan ishlaydi"
                },
                "ExecuteUpdateAsync ChangeTracker va entity loading overhead-ini yo'qotib, to'g'ridan-to'g'ri bazaga 1 ta massiv SQL UPDATE beradi."),

            CreateQuestion("EF Core Query Translation Engine va custom IMethodCallTranslator yozish nima uchun kerak bo'ladi?",
                new List<string> {
                    "Maxsus C# metodlarini (masalan Custom Math yoki String funksiyalar) EF Core LINQ so'rovi ichida SQL dialekt funksiyasiga avtomatik tarjima qilish uchun",
                    "Faqat Controller marshrutini tarjima qilish uchun",
                    "Faqat JSON formatlash uchun",
                    "Faqat migratsiyalarni o me'chirish uchun"
                },
                "IMethodCallTranslator EF Core SQL kompilyatoriga maxsus C# metodlarini muayyan SQL dialekt funksiyasiga o'girish qoidasini o me'rgatadi."),

            CreateQuestion("EF Core-da kutilmagan Memory Leak manbalari (masalan Dynamic LINQ Expression compilation) qanday yuzaga keladi?",
                new List<string> {
                    "Har safar unikal strukturalar bilan dinamik Expression Tree qurilib EF Core-ga berilganda, EF Core ichki Query Cache to'lib kesh tozalanmaydi",
                    "AsNoTracking ishlatilganda",
                    "DbContext pool ishlatilganda",
                    "Faqat PostgreSQL bazasida"
                },
                "EF Core LINQ so'rov parametrlarini keshlaydi. Agar parametr o'rniga dinamik ConstantExpression ishlatilsa Query Cache cheksiz o'sib xotira to'ladi."),

            CreateQuestion("EF Core-da Entity Constructor Injection va Backing Fields ([BackingField]) qanday ishlaydi?",
                new List<string> {
                    "EF Core ma'lumotlarni bazadan o'qiganda property setter-larini emas, backing field-larni to'g'ridan-to'g'ri to'ldiradi va encapsulate qilingan Rich Domain Model beradi",
                    "Constructor Injection faqat Controller-da ishlaydi",
                    "Backing Fields entity-ni o'chirib yuboradi",
                    "EF Core constructor-larni ishlatmaydi"
                },
                "Backing Fields EF Core-ga ma'lumotni o'qiganda private field-ga to'g'ridan-to'g'ri yozishni aytadi, bu Rich Domain Model encapsulation-ni saqlaydi."),

            CreateQuestion("EF Core ChangeTracker Snapshot Change Tracking va Notification Entities (INotifyPropertyChanged) o me'rtasidagi farq nima?",
                new List<string> {
                    "Snapshot tracking DetectChanges chaqirilganda xotiradagi nusxalar bilan solishtiradi; Notification Entities esa INotifyPropertyChanged orqali o'zgarishni lahzada xabar qiladi",
                    "Notification Entities so'rovlarni sekinlashtiradi",
                    "Snapshot tracking xotira sarflamaydi",
                    "Ular bir xil ishlaydi"
                },
                "Notification Entities DetectChanges snapshot overhead-ini yo'qotib, holat o'zgarishini real-vaqtda ChangeTracker-ga ma'lum qiladi."),

            CreateQuestion("EF Core-da o'ta yirik ma'lumotlar to'plamini (100k+ records) bazaga tezkor yozishda EFCore.BulkExtensions yoki NpgsqlBinaryImporter yondashuvi nima uchun kerak?",
                new List<string> {
                    "Standard SaveChangesAsync har bir qator uchun INSERT yoki parametrlash bajarib sekinlashadi; BulkExtensions / BinaryImporter esa PostgreSQL COPY stream orqali 100x tez yozadi",
                    "SaveChangesAsync har doim eng tezkor",
                    "BinaryImporter ma me'lumotlarni o'chirib yuboradi",
                    "EF Core-da bulk import qilib bo'lmaydi"
                },
                "EFCore.BulkExtensions va Npgsql Binary COPY streaming standart SaveChanges-ga qaraganda 100,000+ qatorlarni 100 marta tezroq yozadi."),

            CreateQuestion("EF Core-da Multi-Region Read Replica routing (Read-Write Splitting) custom IDbContextFactory<T> bilan qanday tashkil etiladi?",
                new List<string> {
                    "DbContext yaratilayotganda so'rov rejimiga ko'ra (Read-only vs Write) Master DB ulanish simini yoki Read Replica simini dinamik tanlab berish orqali",
                    "Faqat 1 ta baza ulanishidan foydalanish orqali",
                    "Faqat In-Memory bazaga o'tkazish orqali",
                    "Read Replica routing-ni ilojisi yo me'q"
                },
                "IDbContextFactory orqali AsNoTracking read-only so me'rovlar uchun Read Replica DB ulanishi, Write so'rovlar uchun Master DB ulanishi dinamik beriladi."),

            CreateQuestion("EF Core-da Custom Database Provider yozishda IRelationalCommandBuilder va IQuerySqlGeneratorFactory qanday rol o'ynaydi?",
                new List<string> {
                    "EF Core LINQ AST (Abstract Syntax Tree) daraxtini muayyan ma'lumotlar bazasining nativ SQL buyruq matniga kompilyatsiya qiluvchi dvigatel vazifasini bajaradi",
                    "Faqat JSON formatlashni bajaradi",
                    "Faqat migratsiyani o'chiradi",
                    "Faqat In-Memory keshni boshqaradi"
                },
                "IQuerySqlGeneratorFactory EF Core so me'rov daraxtidan tegishli ma'lumotlar bazasi uchun yakuniy SQL buyruq matnini generatsiya qiladi.")
        };
    }
}
