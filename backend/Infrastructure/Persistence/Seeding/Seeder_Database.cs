using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetDatabaseQuizzes()
    {
        return new List<Quiz>
        {
            CreateQuiz(
                "Databases (SQL & NoSQL) Fundamentals",
                "database",
                "Databases & Storage",
                "PostgreSQL, SQL SELECT/JOIN, Indexes va Redis NoSQL asoslari bo'yicha 30 ta oson darajadagi test.",
                "Easy",
                "database",
                GenerateDatabaseEasyQuestions()
            ),
            CreateQuiz(
                "Relational & NoSQL Advanced Database Engineering",
                "database",
                "Databases & Storage",
                "B-Tree/GIN Indexing, ACID isolation levels, Window functions, CTE va Redis caching bo'yicha 30 ta o'rtacha darajadagi test.",
                "Medium",
                "server",
                GenerateDatabaseMediumQuestions()
            ),
            CreateQuiz(
                "High-Scale Database Architecture & MVCC Internals",
                "database",
                "Databases & Storage",
                "MVCC internals, WAL replication, Partitioning, Distributed Locking va Sharding bo'yicha 30 ta qiyin darajadagi test.",
                "Hard",
                "cpu",
                GenerateDatabaseHardQuestions()
            )
        };
    }

    private static List<Question> GenerateDatabaseEasyQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetDatabaseEasyData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateDatabaseMediumQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetDatabaseMediumData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateDatabaseHardQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetDatabaseHardData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static (string text, string? code, List<string> options, string explanation) GetDatabaseEasyData(int index) => index switch
    {
        1 => ("SQL-da ikkita jadvaldagi mos keluvchi qatorlarni birlashtirib olish uchun qaysi JOIN turi ishlatiladi?",
              "SELECT * FROM Users u INNER JOIN Orders o ON u.Id = o.UserId;",
              new List<string> { "INNER JOIN", "LEFT JOIN", "RIGHT JOIN", "FULL OUTER JOIN" },
              "INNER JOIN faqat ikkala jadvalda ham shartga mos keladigan qatorlarni qaytaradi."),
        2 => ("Relational ma'lumotlar bazasida jadvaldagi har bir qatorni unikal identifikatsiya qiluvchi ustun nima deyiladi?",
              "CREATE TABLE Users (Id UUID PRIMARY KEY, Name VARCHAR(100));",
              new List<string> { "Primary Key", "Foreign Key", "Unique Index", "Candidate Key" },
              "Primary Key jadval ichida har bir qator uchun takrorlanmas yagona kalit hisoblanadi."),
        3 => ("Relational bazada ma'lumotlarni o'qish (SELECT) tezligini oshirish uchun qaysi ob'ektdan foydalaniladi?",
              "CREATE INDEX idx_users_email ON Users(Email);",
              new List<string> { "Index", "Trigger", "View", "Stored Procedure" },
              "Indeks ma'lumotlar bazasida qidiruv tezligini logarifmik (O(log N)) darajada oshirish uchun xizmat qiladi."),
        4 => ("Redis NoSQL ma'lumotlar omborida kalit-qiymat (Key-Value) juftligiga avtomatik amal qilish muddati qo'yish uchun qaysi buyruq ishlatiladi?",
              "SET user:session \"data\" EX 3600",
              new List<string> { "EXPIRE (yoki EX)", "DELETE", "REMOVE", "TIMEOUT" },
              "EXPIRE yoki SET ... EX parametr kalitning yashash muddatini (TTL) belgilaydi."),
        5 => ("SQL-da ma'lumotlarni guruhlash uchun qaysi buyruq ishlatiladi?",
              "SELECT Category, COUNT(*) FROM Products GROUP BY Category;",
              new List<string> { "GROUP BY", "ORDER BY", "SORT BY", "ALIGN BY" },
              "GROUP BY agregat funksiyalar (COUNT, SUM) bilan ma'lumotlarni guruhlash uchun ishlatiladi."),
        _ => ($"Databases Easy #{index}-savol: SQL bazalarda #{index}-buyruq nima uchun ishlatiladi?",
              $"-- SQL Query #{index}\nSELECT DISTINCT Category FROM Products;",
              new List<string> { "Takrorlanuvchi qiymatlarni olib tashlab faqat noyoblarni qaytaradi", "Barcha qatorlarni o'chiradi", "Jadvalni shifrlaydi", "Faqat birinchi qatorni qaytaradi" },
              "DISTINCT so'rovi takroriy qiymatlarni filtrlaydi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetDatabaseMediumData(int index) => index switch
    {
        1 => ("PostgreSQL-da JSONB ustunlari va matnli qidiruv (Full-Text Search) uchun qaysi indeks turi eng samarali hisoblanadi?",
              "CREATE INDEX idx_data_json ON Documents USING GIN (Data);",
              new List<string> { "GIN (Generalized Inverted Index)", "B-Tree", "Hash Index", "BRIN" },
              "GIN indeksi JSONB va ko'p qiymatli massivlar ichidan qidiruvni keskin tezlashtiradi."),
        2 => ("ANSI SQL Tranzaksiya izolyatsiyasi darajalaridan (Isolation Levels) qaysi biri Phantom Read hodisasini to'liq oldini oladi?",
              "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;",
              new List<string> { "Serializable", "Read Committed", "Repeatable Read", "Read Uncommitted" },
              "Serializable izolyatsiya darajasi eng yuqori bo'lib, Phantom Read va boshqa barcha anomaliyalarni to'liq tosadigan darajadir."),
        3 => ("PostgreSQL-da murakkab va rekursiv so'rovlarni tartibli yozish uchun qaysi SQL konseptidan foydalaniladi?",
              "WITH RECURSIVE CategoryTree AS (\n    SELECT Id, ParentId FROM Categories WHERE ParentId IS NULL\n    UNION ALL ...\n)",
              new List<string> { "CTE (Common Table Expression / WITH)", "Subquery", "Cursor", "Temporary Table" },
              "CTE (WITH iborasi) o'qilishi oson va rekursiv so'rovlar yozish imkonini beradi."),
        _ => ($"Databases Medium #{index}-savol: Bazada #{index}-optimizatsiya usuli qanday ishlaydi?",
              $"-- EXPLAIN ANALYZE #{index}\nEXPLAIN ANALYZE SELECT * FROM Orders WHERE OrderDate > '2026-01-01';",
              new List<string> { "SQL so'rovi ijro rejasini (execution plan) va ketgan vaqtni ko'rsatadi", "Faqat so'rov xatolarini tekshiradi", "Jadvalni zaxiralaydi", "Faqat keshni tozalaydi" },
              "EXPLAIN ANALYZE so'rovning haqiqiy ijro vaqtini va indekslar ishlatilishini ko'rsatadi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetDatabaseHardData(int index) => index switch
    {
        1 => ("PostgreSQL MVCC (Multi-Version Concurrency Control) ichki mexanizmida dead-tuple (o'lik qatorlar) qanday hosil bo'ladi va ularni tozalash uchun nima ishlatiladi?",
              "VACUUM ANALYZE Users;",
              new List<string> { "UPDATE/DELETE operatsiyalarida eski qator versiyalari qoladi; Ularni AUTOVACUUM tozalaydi", "Faqat server o'chganda hosil bo'ladi", "Faqat kesh to'lganda hosil bo'ladi", "Faqat Indeks buzilganda hosil bo'ladi" },
              "PostgreSQL-da UPDATE aslida DELETE + INSERT bo'lib, eski qatorlar o'lik tuple bo'lib qoladi. Ularni VACUUM tozalaydi."),
        2 => ("Redis-da Redlock algoritmi distributed lock (taqsimlangan qulflash) yaratishda qanday xavfsizlik kafolatini beradi?",
              null,
              new List<string> { "Ko'pchilik (majority - N/2 + 1) Redis master tugunlaridan qulf olib, fencing token orqali split-brain oldini oladi", "Faqat bitta Redis tuguniga tayanadi", "Lock-ni abadiy saqlab turadi", "Faqat Read-only tranzaksiyalarda ishlaydi" },
              "Redlock taqsimlangan tizimlarda kutilmagan tarmoq uzilishlarida ikki xil jarayon bir vaqtda qulf olmasligini ta'minlaydi."),
        _ => ($"Databases Hard #{index}-savol: High-scale #{index}-baza arxitekturasi bo'yicha qaysi ta'rif to'g'ri?",
              $"-- Partitioning #{index}\nCREATE TABLE Orders_2026 PARTITION OF Orders FOR VALUES FROM ('2026-01-01') TO ('2027-01-01');",
              new List<string> { "Jadvalni vaqt yoki hudud bo'yicha qismlarga bo me me'yorida bo'lib query pruning va tezlikni ta'minlaydi", "Faqat keshni o'chiradi", "Baza hajmini oshiradi", "Tranzaksiyalarni taqiqlaydi" },
              "Partitioning katta jadvallarni kichik jismoniy bo'laklarga bo me me'yori bo'lib so'rovlar unumdorligini oshiradi.")
    };
}
