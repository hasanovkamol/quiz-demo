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
                "PostgreSQL, SQL SELECT/JOIN, Indexes va Redis NoSQL asoslari bo'yicha professional savollar.",
                "Easy",
                "database",
                GenerateDatabaseEasyQuestions()
            ),
            CreateQuiz(
                "Relational & NoSQL Advanced Database Engineering",
                "database",
                "Databases & Storage",
                "B-Tree/GIN Indexing, ACID isolation levels, Window functions, CTE va Redis caching bo'yicha senior savollar.",
                "Medium",
                "server",
                GenerateDatabaseMediumQuestions()
            ),
            CreateQuiz(
                "High-Scale Database Architecture & MVCC Internals",
                "database",
                "Databases & Storage",
                "MVCC internals, WAL replication, Partitioning, Distributed Locking va Sharding bo'yicha principal savollar.",
                "Hard",
                "cpu",
                GenerateDatabaseHardQuestions()
            )
        };
    }

    private static List<Question> GenerateDatabaseEasyQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "SQL-da `INNER JOIN` va `LEFT JOIN` o'rtasidagi asosiy farq nimada?",
                "SELECT * FROM Users u LEFT JOIN Orders o ON u.Id = o.UserId;",
                new List<string> {
                    "INNER JOIN faqat ikkala jadvalda ham shartga mos kelgan qatorlarni qaytaradi; LEFT JOIN chap jadvaldagi barcha qatorlarni va mos o'ng jadval qatorlarini (bo'lmasa NULL) qaytaradi",
                    "LEFT JOIN faqat o'ng jadvaldagi ma'lumotlarni qaytaradi",
                    "INNER JOIN barcha qatorlarni nusxalaydi",
                    "Ikkala JOIN turi ham mutlaqo bir xil natija beradi"
                },
                "INNER JOIN faqat kesishgan (matching) qatorlarni oladi. LEFT JOIN esa chap jadvalning barcha qatorlarini saqlab qoladi."
            ),
            CreateQuestion(
                "Relational ma'lumotlar bazasida B-Tree indeksi qanday ishlaydi va u so'rov tezligiga qanday ta'sir qiladi?",
                "CREATE INDEX idx_users_email ON Users(Email);",
                new List<string> {
                    "So'rov qidiruvini to'liq jadvalni o'qishdan (Full Table Scan O(N)) darajadan logarifmik (O(log N)) darajaga tushirib beradi",
                    "SELECT so'rovlarini 100 marta sekinlashtiradi",
                    "Faqat INSERT operatsiyalarini tezlashtiradi",
                    "Jadval hajmini 50% ga qisqartiradi"
                },
                "B-Tree (Balanced Tree) indeksi saralangan daraxt hosil qilib, qidiruvni O(log N) tezlikka olib keladi."
            ),
            CreateQuestion(
                "Relational bazalarda `WHERE` va `HAVING` shart iboralari orasidagi asosiy farq nimada?",
                "SELECT Category, COUNT(*) FROM Products WHERE Price > 10 GROUP BY Category HAVING COUNT(*) > 5;",
                new List<string> {
                    "WHERE guruhlashdan (GROUP BY) oldin individual qatorlarni filtrlaydi; HAVING esa guruhlangan agregat natijalarni (COUNT, SUM) filtrlaydi",
                    "HAVING guruhlashdan oldin ishlaydi, WHERE esa guruhlashdan keyin",
                    "WHERE faqat string-lar bilan ishlaydi, HAVING faqat int-lar bilan",
                    "WHERE va HAVING bir xil vaqtda ishlaydi"
                },
                "WHERE har bir qatarga guruhlashdan oldin qo'llaniladi. HAVING esa GROUP BY va agregatsiyadan (COUNT/SUM) keyin filtrlaydi."
            ),
            CreateQuestion(
                "Redis NoSQL ma'lumotlar bazasining asosiy xususiyatlari va xotirada saqlash mexanizmi haqida qaysi ta'rif to'g'ri?",
                "SET user:100 \"Alisher\" EX 3600",
                new List<string> {
                    "In-Memory (RAM-da ishlovchi) o'ta yuqori tezlikka ega Key-Value va murakkab ma'lumotlar strukturasini saqlovchi ma'lumotlar ombori",
                    "Faqat relational SQL jadvallarni saqlaydi",
                    "Faqat diskda ishlaydi va RAM-dan foydalanmaydi",
                    "Faqat HTML fayllarni keshlaydi"
                },
                "Redis in-memory (RAM) ma'lumotlar ombori bo'lib sub-millisecond tezlikda ishlaydi."
            ),
            CreateQuestion(
                "SQL-da `PRIMARY KEY` va `UNIQUE KEY` o'rtasidagi asosiy farq nimada?",
                "CREATE TABLE Users (Id INT PRIMARY KEY, Email VARCHAR(100) UNIQUE);",
                new List<string> {
                    "Primary Key jadvalda faqat 1 ta bo'lishi mumkin va NULL qabul qilmaydi; Unique Key esa bir nechta bo'lishi mumkin va NULL qabul qilishi mumkin",
                    "Unique Key har doim 10 ta NULL qabul qilishi shart",
                    "Primary Key vorislikni beradi, Unique Key esa bermaydi",
                    "Ikkala kalit ham bir xil cheklovga ega"
                },
                "Jadvalda faqat 1 ta Primary Key bo'ladi va u NULL bo'lmaydi. Unique Key esa bir nechta bo'lishi mumkin."
            ),
            CreateQuestion(
                "MongoDB NoSQL ma'lumotlar bazasi qanday ma'lumotlar modeliga tayanadi?",
                "db.users.insertOne({ name: \"Ali\", age: 30, skills: [\"C#\", \"Angular\"] });",
                new List<string> {
                    "BSON (Binary JSON) formatidagi Hujjatlarga (Document-based) va moslashuvchan sxemaga (Schemaless)",
                    "Faqat qat'iy munosabatli SQL jadvallariga",
                    "Faqat ustunli (Columnar) jadvallarga",
                    "Faqat matnli fayllarga"
                },
                "MongoDB dokumentga yo'naltirilgan NoSQL baza bo'lib ma'lumotlarni BSON (JSON) formatida saqlaydi."
            ),
            CreateQuestion(
                "Relational ma'lumotlar bazasida Database Normalization (1NF, 2NF, 3NF) ning asosiy maqsadi nima?",
                null,
                new List<string> {
                    "Ma'lumotlar duplikasiyasini (redundancy) yo'qotish va ma'lumotlar anomaliyalarining oldini olish",
                    "So'rovlarni sekinlashtirish",
                    "Indekslarni avtomatik o'chirib tashlash",
                    "Faqat NoSQL bazalarga o'tish"
                },
                "Normalizatsiya ma'lumotlar takrorlanishini qisqartiradi va izchillikni ta'minlaydi."
            ),
            CreateQuestion(
                "SQL-da `UNION` va `UNION ALL` o'rtasidagi asosiy farq nima?",
                "SELECT Name FROM Customers UNION ALL SELECT Name FROM Suppliers;",
                new List<string> {
                    "UNION takrorlanuvchi qatorlarni olib tashlaydi (DISTINCT qiladi); UNION ALL esa barcha qatorlarni tezkor birlashtirib beradi",
                    "UNION ALL takrorlarni o'chiradi, UNION esa saqlaydi",
                    "UNION faqat 2 ta jadval bilan ishlaydi",
                    "UNION ALL faqat NoSQL-da ishlaydi"
                },
                "UNION takrorlanishlarni olib tashlash uchun saralaydi va sekinroq. UNION ALL esa filtrsiz hamma qatorlarni tezkor birlashtiradi."
            ),
            CreateQuestion(
                "PostgreSQL-da `SERIAL` yoki `BIGSERIAL` ustun turi nima uchun ishlatiladi?",
                "CREATE TABLE Orders (Id BIGSERIAL PRIMARY KEY, Total DECIMAL);",
                new List<string> {
                    "Avtomatik 1 ga oshib boruvchi (Auto-Incrementing Sequence) raqamli kalit yaratish uchun",
                    "Faqat matnli ma'lumotlarni saqlash uchun",
                    "Faqat JSON formatni saqlash uchun",
                    "Faqat IP manzillarni saqlash uchun"
                },
                "SERIAL va BIGSERIAL PostgreSQL-da avtomatik oshuvchi sequence yaratadi."
            ),
            CreateQuestion(
                "Database Transactions-da `COMMIT` va `ROLLBACK` buyruqlari nimani bajaradi?",
                "BEGIN TRANSACTION;\nUPDATE Accounts SET Balance = Balance - 100 WHERE Id = 1;\nCOMMIT;",
                new List<string> {
                    "COMMIT barcha o'zgarishlarni bazaga yakuniy saqlaydi; ROLLBACK esa xatolik bo'lganda barcha o'zgarishlarni bekor qilib avvalgi holatga qaytaradi",
                    "ROLLBACK ma'lumotlarni bazadan o'chirib tashlaydi",
                    "COMMIT faqat 1 ta qatorni saqlaydi",
                    "Ikkala buyruq ham bir xil vazifani bajaradi"
                },
                "COMMIT tranzaksiyani muvaffaqiyatli saqlaydi. ROLLBACK esa o'zgarishlarni bekor qiladi (Undo)."
            )
        };
    }

    private static List<Question> GenerateDatabaseMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "ACID prinsiplari (Atomicity, Consistency, Isolation, Durability) ma'lumotlar bazasida nimani kafolatlaydi?",
                "BEGIN TRANSACTION;\n-- Transfer money\nCOMMIT;",
                new List<string> {
                    "Atomicity — hammasi bajariladi yoki hech biri; Consistency — barcha qoidalar saqlanadi; Isolation — tranzaksiyalar alohida; Durability — elektr o'chsa ham saqlanadi",
                    "Atomicity — faqat 1 ta ulanishni qo'yadi",
                    "Consistency — keshni tozalaydi",
                    "Durability — har soniyada nusxa oladi"
                },
                "ACID relatsion ma'lumotlar bazalarida tranzaksiyalarning 100% ishonchli va xavfsiz bajarilishini ta'minlaydi."
            ),
            CreateQuestion(
                "PostgreSQL-da GIN (Generalized Inverted Index) indeksi qaysi holatlarda B-Tree indeksiga qaraganda mos keladi?",
                "CREATE INDEX idx_docs ON Documents USING GIN(tags);",
                new List<string> {
                    "JSONB ob'ektlari, Massivlar va Full-Text Search (matnli qidiruv) bo'yicha ichki elementlarni qidirishda",
                    "Faqat raqamli Primary Key bo'yicha qidirganda",
                    "Faqat Date va Time formatlarida",
                    "Faqat bir xil qiymatli bo'lganda"
                },
                "GIN (Inverted Index) massivlar, matnlar va JSONB ichidagi kalit-qiymatlarni tezkor qidirish uchun ideal."
            ),
            CreateQuestion(
                "ANSI SQL Tranzaksiya izolyatsiyasi darajalarida (Isolation Levels) 'Phantom Read' hodisasi nima va u qaysi darajada oldi olinadi?",
                "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;",
                new List<string> {
                    "Bir tranzaksiya davomida qayta query yuborilganda boshqa tranzaksiya qo'shgan yangi qatorlar (phantom) paydo bo'lishi; Serializable darajasida oldi olinadi",
                    "O'chirilgan qator qaytib qolishi",
                    "Faqat Read Uncommitted-da oldi olinadi",
                    "Bazadagi ma'lumotlar buzilishi"
                },
                "Phantom Read bitta tranzaksiya ichida qayta o'qiganda boshqa tranzaksiya insert qilgan yangi qatorlar ko'rinib qolishidir. Serializable buni to'liq tosadigan darajadir."
            ),
            CreateQuestion(
                "PostgreSQL-da Window Functions (`ROW_NUMBER()`, `RANK()`, `DENSE_RANK()`, `LEAD()`, `LAG()`) oddiy `GROUP BY` dan nimasi bilan farq qiladi?",
                "SELECT Name, Department, Salary, ROW_NUMBER() OVER (PARTITION BY Department ORDER BY Salary DESC) FROM Employees;",
                new List<string> {
                    "Window Functions qatorlarni guruhlarga yig'ib yo'qotib yubormasdan, har bir qator uchun alohida hisoblangan agregat va reyting qiymatlarini qaytaradi",
                    "GROUP BY qatorlarni saqlab qoladi, Window Functions esa guruhlaydi",
                    "Window Functions faqat NoSQL bazada ishlaydi",
                    "Ikkalasi ham bir xil bajariladi"
                },
                "Window Functions qatorlar sonini kamaytirmasdan, `OVER (PARTITION BY ...)` orqali har bir qatorga qo'shimcha tahliliy ma'lumot beradi."
            ),
            CreateQuestion(
                "Redis persistence (ma'lumotlarni diskda saqlash) usullaridan RDB (Snapshotting) va AOF (Append Only File) o'rtasidagi farq nimada?",
                "// redis.conf: appendonly yes",
                new List<string> {
                    "RDB vaqti-vaqti bilan butun xotira snapshot-ini diskka yozadi (tezkor recovery, lekin ma'lumot yo'qolish xavfi bor); AOF har bir yozuv buyrug'ini jurnallaydi (ishonchliroq)",
                    "AOF faqat In-Memory ishlaydi",
                    "RDB faqat log fayl yozadi",
                    "Ikkalasi ham bir xil ishlaydi"
                },
                "RDB ma'lum vaqt oralig'ida ixcham snapshot oladi. AOF esa har bir yozish operatsiyasini ketma-ket jurnalga yozib boradi."
            ),
            CreateQuestion(
                "Redis Eviction Policies (xotira to'lganda ma'lumot o'chirish siyosatlari) da LRU (Least Recently Used) va LFU (Least Frequently Used) farqi nima?",
                "maxmemory-policy allkeys-lru",
                new List<string> {
                    "LRU eng uzoq vaqt ishlatilmagan (vaqt bo'yicha) kalitni o'chiradi; LFU esa eng kam marta chaqirilgan (chastota bo'yicha) kalitni o'chiradi",
                    "LFU eng oxirgi kirgan kalitni o'chiradi",
                    "LRU faqat 10KB kalitlarni o'chiradi",
                    "Ikkala siyosat ham bir xil kalitlarni tanlaydi"
                },
                "LRU oxirgi foydalanilgan vaqtiga (recency) qaraydi. LFU esa kalitning umumiy ishlatilish soniga (frequency) qaraydi."
            ),
            CreateQuestion(
                "Database Locks (Deadlock - berk ko me me'cha) qanday kelib chiqadi va u ma'lumotlar bazasida qanday bartaraf etiladi?",
                "-- Transaction 1 locks A then wants B; Transaction 2 locks B then wants A",
                new List<string> {
                    "Ikki tranzaksiya bir-biri qulflagan resurslarni bir vaqtda kutganda kelib chiqadi; Baza avtomatik Deadlock Detector orqali birini victim qilib tranzaksiyasini revert qiladi",
                    "Baza barcha ulanishlarni o me me'chiradi",
                    "Server avtomatik qayta tushadi",
                    "Deadlock hech qachon kelib chiqmaydi"
                },
                "Deadlock doiraviy kutish hosil bo'lganda yuzaga keladi. Bazaning Deadlock Detector mexanizmi bir tranzaksiyani abort (rollback) qilib yechadi."
            ),
            CreateQuestion(
                "PostgreSQL-da `JSONB` va oddiy `JSON` ustun turlari o'rtasidagi farq nima va qaysi biri ko'proq ishlatiladi?",
                "CREATE TABLE Logs (Id INT, Payload JSONB);",
                new List<string> {
                    "JSONB ma'lumotni ikkilik (binary) formatda saralab saqlaydi va indekslashni (GIN) qo'llaydi; JSON esa matn (raw text) sifatida saqlaydi va sekinroq",
                    "JSONB sekinroq ishlaydi",
                    "JSON indeklashni qo'llaydi",
                    "JSONB faqat raqamlarni saqlaydi"
                },
                "JSONB parslash va indekslash (GIN) uchun ikkilik formatda saqlanadi. Shuning uchun qidiruv va ko'p operatsiyalarda JSONB ishlatiladi."
            ),
            CreateQuestion(
                "MongoDB-da `Index` va `Aggregation Pipeline` (`$match`, `$group`, `$project`) qanday ishlaydi?",
                "db.orders.aggregate([{ $match: { status: \"A\" } }, { $group: { _id: \"$cust_id\", total: { $sum: \"$amount\" } } }]);",
                new List<string> {
                    "Aggregation Pipeline hujjatlarni bosqichma-bosqich (stage) qayta ishlab guruhlaydi; `$match` bosqichi indekslardan unumli foydalanadi",
                    "Pipeline faqat 1 ta hujjat qaytaradi",
                    "MongoDB-da indekslar ishlamaydi",
                    "Pipeline faqat SQL Server-da ishlaydi"
                },
                "Aggregation Pipeline ma'lumotlarni bosqichma-bosqich filtrlaydi va transformatsiya qiladi. `$match` va `$sort` birinchi bosqichda indekslardan foydalanadi."
            ),
            CreateQuestion(
                "PostgreSQL-da CTE (Common Table Expression - `WITH` iborasi) va `RECURSIVE` CTE ishlatilishi haqida qaysi ta'rif to'g'ri?",
                "WITH RECURSIVE Tree AS (\n    SELECT Id, ParentId FROM Node WHERE ParentId IS NULL\n    UNION ALL\n    SELECT n.Id, n.ParentId FROM Node n JOIN Tree t ON n.ParentId = t.Id\n)",
                new List<string> {
                    "RECURSIVE CTE daraxtsimon (tree/graph) va ierarxik ma'lumotlarni (masalan kategoriyalar, tashkilot strukturasi) bir so'rovda o'qish uchun ishlatiladi",
                    "CTE so'rovlarni 10 marta sekinlashtiradi",
                    "CTE faqat o'chirish uchun ishlatiladi",
                    "RECURSIVE CTE cheksiz siklga kirib bazani buzadi"
                },
                "Recursive CTE ota-bola (parent-child) ierarxik jadvallarni (kategoriyalar, izohlar zanjiri) o'qish uchun juda qulay."
            )
        };
    }

    private static List<Question> GenerateDatabaseHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "PostgreSQL MVCC (Multi-Version Concurrency Control) ichki tuzilishi va `xmin`/`xmax` yashirin ustunlari qanday ishlaydi?",
                "SELECT xmin, xmax, * FROM Users;",
                new List<string> {
                    "Har bir qator versiyasida xmin (yaratgan tranzaksiya ID) va xmax (o'chirgan/yangilagan tranzaksiya ID) saqlanadi; Bu o'qish va yozish operatsiyalarining alohida parallel ishlashini ta'minlaydi",
                    "xmin va xmax faqat Primary Key qiymatlarini saqlaydi",
                    "MVCC faqat NoSQL bazalarda bo'ladi",
                    "xmin faqat server soatini saqlaydi"
                },
                "MVCC-da qatorlar ustidan to'g'ridan-to'g'ri yozilmaydi. Yangi versiya yaratilib xmin/xmax orqali ko'rinuvchanlik (visibility) boshqariladi."
            ),
            CreateQuestion(
                "PostgreSQL `VACUUM` va `AUTOVACUUM` mexanizmlari Dead Tuples (o'lik qatorlar) va Bloat management-ni qanday amalga oshiradi?",
                "VACUUM FULL Users; -- Heavy lock!",
                new List<string> {
                    "MVCC natijasida qolgan o'lik qatorlarni (dead tuples) tozalab bo'sh joyni qayta ishlatish uchun belgilaydi; VACUUM FULL esa jadvalni qayta qurib diskka joyni qaytaradi (Exclusive Lock)",
                    "VACUUM barcha indekslarni o'chirib tashlaydi",
                    "AUTOVACUUM har bir soniyada serverni qayta tushiradi",
                    "VACUUM faqat Readonly bazalarda ishlaydi"
                },
                "AUTOVACUUM eskirgan va o'chirilgan tuple-larni bo'sh joy sifatida belgilaydi. VACUUM FULL esa jadvalni qayta yozib disk joyini OS ga qaytaradi."
            ),
            CreateQuestion(
                "PostgreSQL Logical Replication va Write-Ahead Logging (WAL) internals qanday ishlaydi?",
                "CREATE PUBLICATION my_pub FOR TABLE Users;\nCREATE SUBSCRIPTION my_sub CONNECTION '...' PUBLICATION my_pub;",
                new List<string> {
                    "WAL fayllaridan SQL o'zgarishlar oqimi (Logical Decoding) ajratib olinib, jadval darajasida boshqa PostgreSQL tugunlariga nusxalanadi",
                    "Logical Replication faqat fayllarni nusxalaydi",
                    "WAL fayllari faqat In-Memory bo'ladi",
                    "Replication tranzaksiyalarni o'chirib yuboradi"
                },
                "Logical replication WAL oqimidan jadval o'zgarishlarini (INSERT/UPDATE/DELETE) mantiqiy dekodlab boshqa serverga uzatadi."
            ),
            CreateQuestion(
                "PostgreSQL Partitioning (Range, List, Hash) va Partition Pruning optimizatsiyasi qanday ishlaydi?",
                "CREATE TABLE Orders_2026 PARTITION OF Orders FOR VALUES FROM ('2026-01-01') TO ('2027-01-01');",
                new List<string> {
                    "So'rovdagi WHERE shartiga qarab, PostgreSQL query planner faqat kerakli bo'lim (partition) jadvalini o'qiydi va qolganlarini (Partition Pruning) chetlab o'tadi",
                    "Partitioning jadvalni o'chirib beradi",
                    "Pruning so'rovni sekinlashtiradi",
                    "Partitioning faqat 100 ta qatorda ishlaydi"
                },
                "Partition Pruning so'rov shartiga mos kelmaydigan bo'lim jadvallarni rejalashtiruvchi darajasida o'qishdan chiqarib tashlaydi."
            ),
            CreateQuestion(
                "Distributed Databases-da CAP Teoremasi (Consistency, Availability, Partition Tolerance) va PACELC kengaytmasi nimani ta'kidlaydi?",
                "// CAP Theorem: Choose 2 out of 3 under network partition",
                new List<string> {
                    "Tarmoq uzilishi (Partition) bo'lganda tizim yo Izchillikni (Consistency) yo Mavjudlikni (Availability) tanlashi shart; PACELC esa oddiy holatda Latency va Consistency tanlovini qo'shadi",
                    "CAP teoremasi barcha 3 ta sifatni 100% ta'minlashni talab qiladi",
                    "Partition Tolerance faqat bitta kompyuterda bo'ladi",
                    "PACELC faqat SQL Server-da ishlaydi"
                },
                "CAP teoremasiga ko'ra taqsimlangan tizim tarmoq bo'linganda (Partition) bir vaqtning o'zida ham 100% Consistency, ham 100% Availability berolmaydi."
            ),
            CreateQuestion(
                "PostgreSQL Serializable Snapshot Isolation (SSI) va Write Skew Anomaly qanday hal etiladi?",
                "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;",
                new List<string> {
                    "SSI tranzaksiyalar o'rtasidagi SIREAD lock-lar va graflarni tahlil qilib, Write Skew sodir bo'lishi bilan birini abort qiladi va 100% ketma-ketlikni kafolatlaydi",
                    "SSI faqat jadvalni to'liq qulflaydi (Table Lock)",
                    "Write Skew anomaliyasi hesh qachon kelib chiqmaydi",
                    "SSI faqat Read Uncommitted-da ishlaydi"
                },
                "PostgreSQL SSI mexanizmi hech qanday og'ir lock qo'ymasdan SIREAD lock-lar orqali Write Skew anomaliyasini aniqlab abort qiladi."
            ),
            CreateQuestion(
                "PostgreSQL-da `Advisory Locks` (`pg_advisory_lock`) qaysi holatlarda va qanday ishlatiladi?",
                "SELECT pg_advisory_lock(123456);",
                new List<string> { "Jadval yoki qatorga bog me'liq bo'lmagan, dasturiy mantiq darajasidagi (Application-level) taqsimlangan qulflashlarni bajarish uchun", "Faqat jadvalni o'chirish uchun", "Faqat foydalanuvchi parolini tekshirish uchun", "Faqat backup olish uchun" },
                "Advisory Locks PostgreSQL tomonidan taqdim etiladigan, jadval va qatorlarga bog'liq bo'lmagan dasturiy ma'no berilgan qulflardir."
            ),
            CreateQuestion(
                "MongoDB WiredTiger saqlash dvigateli (Storage Engine) xotira keshini va Concurrency control-ni qanday boshqaradi?",
                "// WiredTiger cache configuration",
                new List<string> {
                    "Document-level Concurrency Control (hujjat darajasidagi qulflash) va Ticket-based execution hamda Snappy/Zlib siqishni beradi",
                    "WiredTiger to'liq jadval darajasida qulflaydi (Table Lock)",
                    "WiredTiger faqat RAM-da ishlaydi",
                    "WiredTiger tranzaksiyalarni taqiqlaydi"
                },
                "WiredTiger MongoDB uchun yuqori unumdorlikdagi dvigatel bo'lib, hujjat darajasidagi qulflash va kesh siqishni ta'minlaydi."
            ),
            CreateQuestion(
                "Redis Cluster-da Hash Slots (16384 ta slot) va Hash Tags (`{user100}.orders`) qanday ishlaydi?",
                "GET {user:100}:profile\nGET {user:100}:orders",
                new List<string> {
                    "Hash Tags `{...}` jingalak qavs ichidagi kalit qismini hashing qilib, tegishli ma'lumotlarni aynan bitta Redis master tuguniga (node) tushishini kafolatlaydi",
                    "Hash Slots faqat 10 ta kalit saqlaydi",
                    "Hash Tags kalitlarni o me me me me'chirish uchun ishlatiladi",
                    "Redis Cluster-da kalitlarni taqsimlab bo'lmaydi"
                },
                "Hash Tags `{...}` bir necha bog me me'liq kalitlarning aynan bitta Redis tuguniga tushishini ta me'minlaydi (Multi-key operations uchun)."
            ),
            CreateQuestion(
                "PostgreSQL-da B-Tree indekslarida Page Splitting va FillFactor (masalan `WITH (fillfactor = 70)`) sozlamasi qanday rol o'ynaydi?",
                "CREATE INDEX idx_test ON Users(Name) WITH (fillfactor = 70);",
                new List<string> {
                    "Indeks sahifalarida yangi UPDATE/INSERT uchun bo'sh joy qoldiradi, bu esa og'ir Page Splitting va indeks fragmentatsiyasini kamaytiradi",
                    "FillFactor jadvalni o'chirib beradi",
                    "Page Splitting so'rovlarni tezlashtiradi",
                    "FillFactor faqat NoSQL-da bo'ladi"
                },
                "FillFactor indeks varaq sahifalarida joy qoldirib, tez-tez o'zgaradigan jadvallarda Page Splitting hosil bo'lishini kamaytiradi."
            )
        };
    }
}
