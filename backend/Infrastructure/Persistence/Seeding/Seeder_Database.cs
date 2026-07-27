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
        return new List<Question>
        {
            CreateQuestion("SQL-da INNER JOIN va LEFT JOIN o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "INNER JOIN faqat ikkala jadvalda ham shartga mos kelgan qatorlarni qaytaradi; LEFT JOIN chap jadvaldagi barcha qatorlarni va mos o'ng jadval qatorlarini (bo'lmasa NULL) qaytaradi",
                    "LEFT JOIN faqat o'ng jadvaldagi ma'lumotlarni qaytaradi",
                    "INNER JOIN barcha qatorlarni nusxalaydi",
                    "Ikkala JOIN turi ham mutlaqo bir xil natija beradi"
                },
                "INNER JOIN faqat kesishgan (matching) qatorlarni oladi. LEFT JOIN esa chap jadvalning barcha qatorlarini saqlab qoladi."),

            CreateQuestion("Relational ma'lumotlar bazasida B-Tree indeksi qanday ishlaydi va u so'rov tezligiga qanday ta'sir qiladi?",
                new List<string> {
                    "So'rov qidiruvini to'liq jadvalni o'qishdan (Full Table Scan O(N)) darajadan logarifmik (O(log N)) darajaga tushirib beradi",
                    "SELECT so'rovlarini 100 marta sekinlashtiradi",
                    "Faqat INSERT operatsiyalarini tezlashtiradi",
                    "Jadval hajmini 50% ga qisqartiradi"
                },
                "B-Tree (Balanced Tree) indeksi saralangan daraxt hosil qilib, qidiruvni O(log N) tezlikka olib keladi."),

            CreateQuestion("Relational bazalarda WHERE va HAVING shart iboralari orasidagi asosiy farq nimada?",
                new List<string> {
                    "WHERE guruhlashdan (GROUP BY) oldin individual qatorlarni filtrlaydi; HAVING esa guruhlangan agregat natijalarni (COUNT, SUM) filtrlaydi",
                    "HAVING guruhlashdan oldin ishlaydi, WHERE esa guruhlashdan keyin",
                    "WHERE faqat string-lar bilan ishlaydi, HAVING faqat int-lar bilan",
                    "WHERE va HAVING bir xil vaqtda ishlaydi"
                },
                "WHERE har bir qatarga guruhlashdan oldin qo'llaniladi. HAVING esa GROUP BY va agregatsiyadan (COUNT/SUM) keyin filtrlaydi."),

            CreateQuestion("Redis NoSQL ma'lumotlar bazasining asosiy xususiyatlari va xotirada saqlash mexanizmi haqida qaysi ta'rif to'g'ri?",
                new List<string> {
                    "In-Memory (RAM-da ishlovchi) o'ta yuqori tezlikka ega Key-Value va murakkab ma'lumotlar strukturasini saqlovchi ma'lumotlar ombori",
                    "Faqat relational SQL jadvallarni saqlaydi",
                    "Faqat diskda ishlaydi va RAM-dan foydalanmaydi",
                    "Faqat HTML fayllarni keshlaydi"
                },
                "Redis in-memory (RAM) ma'lumotlar ombori bo'lib sub-millisecond tezlikda ishlaydi."),

            CreateQuestion("SQL-da PRIMARY KEY va UNIQUE KEY o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "Primary Key jadvalda faqat 1 ta bo'lishi mumkin va NULL qabul qilmaydi; Unique Key esa bir nechta bo'lishi mumkin va NULL qabul qilishi mumkin",
                    "Unique Key har doim 10 ta NULL qabul qilishi shart",
                    "Primary Key vorislikni beradi, Unique Key esa bermaydi",
                    "Ikkala kalit ham bir xil cheklovga ega"
                },
                "Jadvalda faqat 1 ta Primary Key bo'ladi va u NULL bo'lmaydi. Unique Key esa bir nechta bo'lishi mumkin."),

            CreateQuestion("MongoDB NoSQL ma'lumotlar bazasi qanday ma'lumotlar modeliga tayanadi?",
                new List<string> {
                    "BSON (Binary JSON) formatidagi Hujjatlarga (Document-based) va moslashuvchan sxemaga (Schemaless)",
                    "Faqat qat'iy munosabatli SQL jadvallariga",
                    "Faqat ustunli (Columnar) jadvallarga",
                    "Faqat matnli fayllarga"
                },
                "MongoDB dokumentga yo'naltirilgan NoSQL baza bo'lib ma'lumotlarni BSON (JSON) formatida saqlaydi."),

            CreateQuestion("Relational ma'lumotlar bazasida Database Normalization (1NF, 2NF, 3NF) ning asosiy maqsadi nima?",
                new List<string> {
                    "Ma'lumotlar duplikasiyasini (redundancy) yo'qotish va ma'lumotlar anomaliyalarining oldini olish",
                    "So'rovlarni sekinlashtirish",
                    "Indekslarni avtomatik o'chirib tashlash",
                    "Faqat NoSQL bazalarga o'tish"
                },
                "Normalizatsiya ma'lumotlar takrorlanishini qisqartiradi va izchillikni ta'minlaydi."),

            CreateQuestion("SQL-da UNION va UNION ALL o'rtasidagi asosiy farq nima?",
                new List<string> {
                    "UNION takrorlanuvchi qatorlarni olib tashlaydi (DISTINCT qiladi); UNION ALL esa barcha qatorlarni tezkor birlashtirib beradi",
                    "UNION ALL takrorlarni o'chiradi, UNION esa saqlaydi",
                    "UNION faqat 2 ta jadval bilan ishlaydi",
                    "UNION ALL faqat NoSQL-da ishlaydi"
                },
                "UNION takrorlanishlarni olib tashlash uchun saralaydi va sekinroq. UNION ALL esa filtrsiz hamma qatorlarni tezkor birlashtiradi."),

            CreateQuestion("PostgreSQL-da SERIAL yoki BIGSERIAL ustun turi nima uchun ishlatiladi?",
                new List<string> {
                    "Avtomatik 1 ga oshib boruvchi (Auto-Incrementing Sequence) raqamli kalit yaratish uchun",
                    "Faqat matnli ma'lumotlarni saqlash uchun",
                    "Faqat JSON formatni saqlash uchun",
                    "Faqat IP manzillarni saqlash uchun"
                },
                "SERIAL va BIGSERIAL PostgreSQL-da avtomatik oshuvchi sequence yaratadi."),

            CreateQuestion("Database Transactions-da COMMIT va ROLLBACK buyruqlari nimani bajaradi?",
                new List<string> {
                    "COMMIT barcha o'zgarishlarni bazaga yakuniy saqlaydi; ROLLBACK esa xatolik bo'lganda barcha o'zgarishlarni bekor qilib avvalgi holatga qaytaradi",
                    "ROLLBACK ma'lumotlarni bazadan o'chirib tashlaydi",
                    "COMMIT faqat 1 ta qatorni saqlaydi",
                    "Ikkala buyruq ham bir xil vazifani bajaradi"
                },
                "COMMIT tranzaksiyani muvaffaqiyatli saqlaydi. ROLLBACK esa o'zgarishlarni bekor qiladi (Undo)."),

            CreateQuestion("SQL-da DELETE, TRUNCATE va DROP buyruqlari o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "DELETE — qatorlarni shart bo'yicha o'chiradi va ROLLBACK bo'ladi; TRUNCATE — jadvalni to'liq tezkor tozalaydi; DROP — jadval strukturasini o'chiradi",
                    "TRUNCATE faqat 1 ta qatorni o'chiradi",
                    "DROP jadvalni saqlab qoladi",
                    "DELETE rollbacb bo'lmaydi"
                },
                "DELETE DML buyrug'i bo'lib log yuritadi, TRUNCATE DDL buyrug'i bo'lib jadval ma'lumotlarini tezkor bo'shatadi, DROP esa jadvalni mutlaqo yo'q qiladi."),

            CreateQuestion("Relational bazalarda FOREIGN KEY cheklovi (Constraint) qanday vazifa bajaradi?",
                new List<string> {
                    "Ikki jadval o'rtasida moslik izchilligini (Referential Integrity) ta'minlaydi va mavjud bo'lmagan ID yozilishini tosadigan cheklov o'rnatadi",
                    "Faqat jadval nomini o'zgartiradi",
                    "Faqat parollarni shifrlaydi",
                    "FOREIGN KEY faqat NoSQL-da bo'ladi"
                },
                "Foreign Key bog'liq jadvallar o'rtasida ma'lumotlar yaxlitligi va referentsial izchillikni kafolatlaydi."),

            CreateQuestion("SQL GROUP BY iborasi bilan COUNT, SUM, AVG, MIN, MAX agregat funksiyalari qanday ishlaydi?",
                new List<string> {
                    "Jadval qatorlarini belgilangan ustunlar bo'yicha guruhlarga bo'lib, har bir guruh uchun yagona agregat natija kisoblaydi",
                    "Har bir qatorni alohida ko'paytiradi",
                    "Jadvalni o'chirib beradi",
                    "Faqat string formatlaydi"
                },
                "GROUP BY ko'plab qatorlarni ko'rsatilgan ustun bo'yicha bir guruhga yig'adi va statistik ma'lumot chiqaradi."),

            CreateQuestion("SQL Views (CREATE VIEW) va Materialized Views o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "View — virtual so'rov bo'lib diskda ma'lumot saqlamaydi; Materialized View esa so'rov natijasini diskka jismonan saqlab keshlaydi",
                    "View har doim diskka saqlaydi",
                    "Materialized View faqat In-Memory ishlaydi",
                    "Ular o'rtasida farq yo'q"
                },
                "View faqat saqlangan SQL so'rovi (Virtual Table), Materialized View esa jismonan saqlanadigan va yangilab turiladigan ma'lumotlar to'plamidir."),

            CreateQuestion("SQL Composite Index (Ko'p ustunli indeks) da Leftmost Prefix Rule nimani anglatadi?",
                new List<string> {
                    "(A, B, C) indeksida so'rov WHERE shartida eng chapdagi A ustunini qatnashtirsa indeks ishlaydi; faqat B yoki C bo'lsa indeks ishlamaydi",
                    "Indeks faqat chap tomondagi fayllarga saqlanadi",
                    "Indeks har doim ishlaydi",
                    "U faqat NoSQL-da mavjud"
                },
                "Composite Index chapdan o'ngga saralanadi. Shuning uchun so'rov eng chapdagi birinchi ustunni qatnashtirishi kerak."),

            CreateQuestion("Ma'lumotlar bazasi Connection Pooling (masalan Npgsql / HikariCP) nima uchun kerak?",
                new List<string> {
                    "Har bir so'rovda baza ulanishini qayta yangidan yaratish xarajatini kamaytirish uchun ulanishlar pulini tayyor saqlab beradi",
                    "Faqat ma me'lumotlarni shifrlaydi",
                    "Faqat fayllarni keshlaydi",
                    "Baza parolini saqlaydi"
                },
                "Connection Pool tayyor ulanishlarni RAM-da saqlab turadi, bu TCP handshake va auth vaqtini tejab so'rov tezligini oshiradi."),

            CreateQuestion("Relational bazalarda One-to-Many va Many-to-Many munosabatlari qanday modellashtiriladi?",
                new List<string> {
                    "One-to-Many — ko'p tomondagi jadvalga Foreign Key qo'shiladi; Many-to-Many — ikkala jadval kalitlarini saqlovchi oraliq Junction Table yaratiladi",
                    "Many-to-Many uchun har doim 1 ta jadval yetarli",
                    "One-to-Many taqiqlangan",
                    "Ular faqat MongoDB-da bo'ladi"
                },
                "Many-to-Many munosabati 2 ta Primary Key-ni bog'lovchi oraliq bog'lovchi jadval (Junction/Bridge table) orqali amalga oshiriladi."),

            CreateQuestion("SQL-da LIKE 'A%' va LIKE '%A%' va Full-Text Search o'rtasidagi unumdorlik farqi nima?",
                new List<string> {
                    "LIKE 'A%' B-Tree indeksidan foydalanishi mumkin; LIKE '%A%' esa har doim Full Table Scan qiladi (sekin); Full-Text Search GIN indeks ishlatadi",
                    "LIKE '%A%' har doim tezroq ishlaydi",
                    "Full-Text Search indeks ishlatmaydi",
                    "Ular mutlaqo bir xil"
                },
                "B-Tree indeksi faqat so'z boshidan ('A%') qidirganda ishlaydi. So'z o'rtasidan ('%A%') qidirganda GIN va Full-Text Search kerak."),

            CreateQuestion("SQL-da Subquery (Ichki so'rov) va Correlated Subquery o'rtasidagi farq nima?",
                new List<string> {
                    "Correlated Subquery tashqi so'rovning har bir qatori uchun qayta ijro etiladi (sekinroq); Oddiy Subquery esa 1 marta bajarilib natija beradi",
                    "Correlated Subquery har doim 100 marta tezroq",
                    "Oddiy Subquery faqat NoSQL-da ishlaydi",
                    "Ikkala subquery ham bir marta ishlaydi"
                },
                "Correlated Subquery tashqi jadval qatoriga bog'liq bo'ladi va har bir qator uchun qayta chaqirilib unumdorlikni tushirishi mumkin."),

            CreateQuestion("SQL-da EXISTS va IN shart iboralari katta ma'lumotlarda qanday ishlaydi?",
                new List<string> {
                    "EXISTS birinchi mos kelgan qatorni topishi bilan qidiruvni to'xtatadi (Short-circuit check); IN esa ichki so'rov natijalar to'plamini yig'adi",
                    "IN har doim EXISTS-dan tezroq",
                    "EXISTS faqat NoSQL-da ishlaydi",
                    "Ular o me me'rtasida farq yo'q"
                },
                "EXISTS shart bajarilishi bilan True qaytarib to'xtaydi (Short-circuiting), bu esa katta jadvallarda samaraliroq."),

            CreateQuestion("Redis NoSQL-da SET, GET, INCR va EXPIRE buyruqlari nimani bajaradi?",
                new List<string> {
                    "SET — kalit-qiymat yozadi; GET — o'qiydi; INCR — sonni atomik 1 ga oshiradi; EXPIRE — kalitga yashash muddatini (TTL) o'rnatadi",
                    "INCR qiymatni o me'chiradi",
                    "EXPIRE kalitni abadiy saqlaydi",
                    "SET faqat SQL-da ishlaydi"
                },
                "Redis-da INCR buyrug'i atomik ravishda sonni oshiradi, EXPIRE esa avtomatik keshni tozalash uchun TTL beradi."),

            CreateQuestion("Redis Lists buyruqlaridan LPUSH va RPOP bilan Message Queue qanday tashkil etiladi?",
                new List<string> {
                    "LPUSH ro'yxat chap tomoniga xabar qo'shadi, RPOP esa o'ng tomondan xabarni sug'urib oladi (FIFO Queue)",
                    "LPUSH xabarlarni o'chiradi",
                    "RPOP faqat keshni tozalaydi",
                    "Redis-da navbat tashkil etib bo'lmaydi"
                },
                "LPUSH va RPOP (yoki BLPOP blocking pop) birgalikda In-Memory FIFO (First-In, First-Out) xabarlar navbatini beradi."),

            CreateQuestion("Redis Sets (SADD, SMEMBERS, SINTER) ma'lumotlar strukturasi nimasi bilan ajralib turadi?",
                new List<string> {
                    "Takrorlanmaydigan (Unique) tartibsiz elementlar to'plamini saqlaydi hamda to'plamlar kesishmasi (SINTER) kabi amallarni bajaradi",
                    "Sets takroriy qiymat saqlaydi",
                    "Sets faqat 1 ta element saqlaydi",
                    "SADD buyrug'i to'plamni o'chiradi"
                },
                "Redis Sets unikal elementlar to me'plami bo'lib, kesishma (intersection) va birlashma (union) amallarini sub-millisecond beradi."),

            CreateQuestion("Redis Hashes (HSET, HGET, HGETALL) qaysi holatlarda String-ga qaraganda mos keladi?",
                new List<string> {
                    "Obyektlarni (masalan User profile) ichki maydonlari bo'yicha alohida saqlash va faqat 1 ta maydonini (HSET/HGET) o'qish/o'zgartirish uchun",
                    "Hashes faqat 10KB fayllarni saqlaydi",
                    "Hashes keshni tezroq o'chiradi",
                    "String har doim obyektlar uchun mos"
                },
                "Redis Hashes obyekt maydonlarini RAM xotirasida o me'ta ixcham saqlash va alohida field-larni o'qish imkonini beradi."),

            CreateQuestion("Relational bazalarda NULL qiymatlar va Three-Valued Logic (TRUE, FALSE, UNKNOWN) qanday ishlaydi?",
                new List<string> {
                    "NULL — nomalum qiymat; NULL = NULL tenglik har doim UNKNOWN/FALSE beradi; Shuning uchun IS NULL yoki IS NOT NULL ishlatiladi",
                    "NULL har doim 0 ga teng",
                    "NULL = NULL har doim TRUE beradi",
                    "NULL qiymat bilan amallar taqiqlangan"
                },
                "SQL-da NULL mavjud bo'lmagan qiymat demakdir. `NULL = NULL` har doim FALSE/UNKNOWN beradi."),

            CreateQuestion("SQL-da Stringlarni birlashtirish (Concatenation) turlicha bazalarda (PostgreSQL, SQL Server) qanday amalga oshiriladi?",
                new List<string> {
                    "PostgreSQL-da || operatori yoki CONCAT(); SQL Server-da + operatori yoki CONCAT() funksiyasi",
                    "Faqat JOIN iborasi orqali",
                    "Faqat SUM() funksiyasi orqali",
                    "String-larni birlashtirib bo'lmaydi"
                },
                "PostgreSQL-da `FirstName || ' ' || LastName`, SQL Server-da esa `FirstName + ' ' + LastName` ishlatiladi."),

            CreateQuestion("SQL-da OFFSET/LIMIT pagination va Keyset Pagination (Seek Method) o'rtasidagi unumdorlik farqi nima?",
                new List<string> {
                    "OFFSET n ma'lumot o'tganda dastlabki n ta qatorni o me'qib tashlaydi (sekin); Keyset Pagination (WHERE id > last_id) esa darhol indeksdan O(1) ko me'taradi",
                    "OFFSET har doim Keyset pagination-dan tezroq",
                    "Keyset pagination faqat NoSQL-da bo'ladi",
                    "Ular mutlaqo bir xil"
                },
                "OFFSET 100000 bo'lganda baza 100000 ta qatorni o me'qib o'chiradi. Keyset pagination (Seek method) esa indeksdan darhol ko'taradi."),

            CreateQuestion("Database Logical Backup (pg_dump) va Physical Backup (File-level copy) o'rtasidagi farq nima?",
                new List<string> {
                    "Logical Backup SQL skriptlarini (CREATE, INSERT) beradi va versiyalararo ko'chirishga mos; Physical Backup esa baza data fayllarini nusxalaydi va o'ta tez tiklanadi",
                    "Logical backup faqat rasmlarni saqlaydi",
                    "Physical backup SQL tayyorlaydi",
                    "Ular bir xil backup turi"
                },
                "Logical Backup (pg_dump) SQL skriptlari shaklida portal beradi. Physical Backup esa fayl bloklarini nusxalab tezkor tiklanadi."),

            CreateQuestion("SQL Transactions-da BEGIN TRANSACTION va SAVEPOINT nimani beradi?",
                new List<string> {
                    "BEGIN — tranzaksiyani boshlaydi; SAVEPOINT — tranzaksiya ichida oraliq nuqta o'rnatib, xato bo'lganda butun emas faqat ushbu nuqtagacha ROLLBACK qilish imkonini beradi",
                    "SAVEPOINT bazani o me'chirish uchun kerak",
                    "BEGIN TRANSACTION keshni tozalaydi",
                    "SAVEPOINT faqat NoSQL-da bo me'ladi"
                },
                "SAVEPOINT tranzaksiya ichida oraliq nuqtalar yaratib, qisman bekor qilish (ROLLBACK TO SAVEPOINT) imkoniyatini beradi."),

            CreateQuestion("NoSQL va Relational (SQL) bazalarni tanlashdagi asosiy me'moriy mezon nima?",
                new List<string> {
                    "SQL — qat'iy schema, murakkab JOIN-lar va ACID izchillik talab etilganda; NoSQL — moslashuvchan schema, high throughput va horizontal scaling kerak bo'lganda",
                    "NoSQL har doim SQL-dan yaxshiroq",
                    "SQL hech qachon scale bo me'lmaydi",
                    "Ular o'rtasida farq yo me'q"
                },
                "Loyihaning izchillik (ACID vs BASE) va schema moslashuvchanligi talablaridan kelib chiqib SQL yoki NoSQL tanlanadi.")
        };
    }

    private static List<Question> GenerateDatabaseMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("ACID prinsiplari (Atomicity, Consistency, Isolation, Durability) ma'lumotlar bazasida nimani kafolatlaydi?",
                new List<string> {
                    "Atomicity — hammasi bajariladi yoki hech biri; Consistency — barcha qoidalar saqlanadi; Isolation — tranzaksiyalar alohida; Durability — elektr o'chsa ham saqlanadi",
                    "Atomicity — faqat 1 ta ulanishni qo'yadi",
                    "Consistency — keshni tozalaydi",
                    "Durability — har soniyada nusxa oladi"
                },
                "ACID relatsion ma'lumotlar bazalarida tranzaksiyalarning 100% ishonchli va xavfsiz bajarilishini ta'minlaydi."),

            CreateQuestion("PostgreSQL-da GIN (Generalized Inverted Index) indeksi qaysi holatlarda B-Tree indeksiga qaraganda mos keladi?",
                new List<string> {
                    "JSONB ob'ektlari, Massivlar va Full-Text Search (matnli qidiruv) bo'yicha ichki elementlarni qidirishda",
                    "Faqat raqamli Primary Key bo'yicha qidirganda",
                    "Faqat Date va Time formatlarida",
                    "Faqat bir xil qiymatli bo'lganda"
                },
                "GIN (Inverted Index) massivlar, matnlar va JSONB ichidagi kalit-qiymatlarni tezkor qidirish uchun ideal."),

            CreateQuestion("ANSI SQL Tranzaksiya izolyatsiyasi darajalarida (Isolation Levels) Phantom Read hodisasi nima va u qaysi darajada oldi olinadi?",
                new List<string> {
                    "Bir tranzaksiya davomida qayta query yuborilganda boshqa tranzaksiya qo'shgan yangi qatorlar (phantom) paydo bo'lishi; Serializable darajasida oldi olinadi",
                    "O'chirilgan qator qaytib qolishi",
                    "Faqat Read Uncommitted-da oldi olinadi",
                    "Bazadagi ma'lumotlar buzilishi"
                },
                "Phantom Read bitta tranzaksiya ichida qayta o'qiganda boshqa tranzaksiya insert qilgan yangi qatorlar ko'rinib qolishidir. Serializable buni to'liq tosadigan darajadir."),

            CreateQuestion("PostgreSQL-da Window Functions (ROW_NUMBER(), RANK(), DENSE_RANK(), LEAD(), LAG()) oddiy GROUP BY dan nimasi bilan farq qiladi?",
                new List<string> {
                    "Window Functions qatorlarni guruhlarga yig'ib yo'qotib yubormasdan, har bir qator uchun alohida hisoblangan agregat va reyting qiymatlarini qaytaradi",
                    "GROUP BY qatorlarni saqlab qoladi, Window Functions esa guruhlaydi",
                    "Window Functions faqat NoSQL bazada ishlaydi",
                    "Ikkalasi ham bir xil bajariladi"
                },
                "Window Functions qatorlar sonini kamaytirmasdan, OVER (PARTITION BY ...) orqali har bir qatorga qo'shimcha tahliliy ma'lumot beradi."),

            CreateQuestion("Redis persistence (ma'lumotlarni diskda saqlash) usullaridan RDB (Snapshotting) va AOF (Append Only File) o'rtasidagi farq nimada?",
                new List<string> {
                    "RDB vaqti-vaqti bilan butun xotira snapshot-ini diskka yozadi (tezkor recovery, lekin ma'lumot yo'qolish xavfi bor); AOF har bir yozuv buyrug'ini jurnallaydi (ishonchliroq)",
                    "AOF faqat In-Memory ishlaydi",
                    "RDB faqat log fayl yozadi",
                    "Ikkalasi ham bir xil ishlaydi"
                },
                "RDB ma'lum vaqt oralig'ida ixcham snapshot oladi. AOF esa har bir yozish operatsiyasini ketma-ket jurnalga yozib boradi."),

            CreateQuestion("Redis Eviction Policies (xotira to'lganda ma'lumot o'chirish siyosatlari) da LRU (Least Recently Used) va LFU (Least Frequently Used) farqi nima?",
                new List<string> {
                    "LRU eng uzoq vaqt ishlatilmagan (vaqt bo'yicha) kalitni o'chiradi; LFU esa eng kam marta chaqirilgan (chastota bo'yicha) kalitni o'chiradi",
                    "LFU eng oxirgi kirgan kalitni o'chiradi",
                    "LRU faqat 10KB kalitlarni o'chiradi",
                    "Ikkala siyosat ham bir xil kalitlarni tanlaydi"
                },
                "LRU oxirgi foydalanilgan vaqtiga (recency) qaraydi. LFU esa kalitning umumiy ishlatilish soniga (frequency) qaraydi."),

            CreateQuestion("Database Locks (Deadlock - berk ko'cha) qanday kelib chiqadi va u ma'lumotlar bazasida qanday bartaraf etiladi?",
                new List<string> {
                    "Ikki tranzaksiya bir-biri qulflagan resurslarni bir vaqtda kutganda kelib chiqadi; Baza avtomatik Deadlock Detector orqali birini victim qilib tranzaksiyasini revert qiladi",
                    "Baza barcha ulanishlarni o'chiradi",
                    "Server avtomatik qayta tushadi",
                    "Deadlock hech qachon kelib chiqmaydi"
                },
                "Deadlock doiraviy kutish hosil bo'lganda yuzaga keladi. Bazaning Deadlock Detector mexanizmi bir tranzaksiyani abort (rollback) qilib yechadi."),

            CreateQuestion("PostgreSQL-da JSONB va oddiy JSON ustun turlari o'rtasidagi farq nima va qaysi biri ko'proq ishlatiladi?",
                new List<string> {
                    "JSONB ma'lumotni ikkilik (binary) formatda saralab saqlaydi va indekslashni (GIN) qo'llaydi; JSON esa matn (raw text) sifatida saqlaydi va sekinroq",
                    "JSONB sekinroq ishlaydi",
                    "JSON indeklashni qo'llaydi",
                    "JSONB faqat raqamlarni saqlaydi"
                },
                "JSONB parslash va indekslash (GIN) uchun ikkilik formatda saqlanadi. Shuning uchun qidiruv va ko'p operatsiyalarda JSONB ishlatiladi."),

            CreateQuestion("MongoDB-da Index va Aggregation Pipeline ($match, $group, $project) qanday ishlaydi?",
                new List<string> {
                    "Aggregation Pipeline hujjatlarni bosqichma-bosqich (stage) qayta ishlab guruhlaydi; $match bosqichi indekslardan unumli foydalanadi",
                    "Pipeline faqat 1 ta hujjat qaytaradi",
                    "MongoDB-da indekslar ishlamaydi",
                    "Pipeline faqat SQL Server-da ishlaydi"
                },
                "Aggregation Pipeline ma'lumotlarni bosqichma-bosqich filtrlaydi va transformatsiya qiladi. $match va $sort birinchi bosqichda indekslardan foydalanadi."),

            CreateQuestion("PostgreSQL-da CTE (Common Table Expression - WITH iborasi) va RECURSIVE CTE ishlatilishi haqida qaysi ta'rif to'g'ri?",
                new List<string> {
                    "RECURSIVE CTE daraxtsimon (tree/graph) va ierarxik ma'lumotlarni (masalan kategoriyalar, tashkilot strukturasi) bir so'rovda o'qish uchun ishlatiladi",
                    "CTE so'rovlarni 10 marta sekinlashtiradi",
                    "CTE faqat o'chirish uchun ishlatiladi",
                    "RECURSIVE CTE cheksiz siklga kirib bazani buzadi"
                },
                "Recursive CTE ota-bola (parent-child) ierarxik jadvallarni (kategoriyalar, izohlar zanjiri) o'qish uchun juda qulay."),

            CreateQuestion("PostgreSQL Index Scan, Index Only Scan va Bitmap Index Scan o'rtasidagi unumdorlik farqi nima?",
                new List<string> {
                    "Index Only Scan — barcha kerakli ustunlar indeksda mavjud (Heap-ga kirmaydi); Index Scan — Heap-dan qatorni o'qiydi; Bitmap Scan — ko'plab sahifalarni ketma-ket tartiblab o me me'qiydi",
                    "Index Scan har doim eng tezkor",
                    "Bitmap Scan faqat 1 ta qatorda ishlaydi",
                    "Ular o'rtasida farq yo me'q"
                },
                "Index Only Scan eng tezkor hisoblanadi chunki u jadval disk sahifalariga (Heap page) kirmasdan indeksning o'zidan javob beradi."),

            CreateQuestion("PostgreSQL EXPLAIN ANALYZE buyrug'ida Planning Time, Execution Time va Cost ko'rsatkichlari nimani beradi?",
                new List<string> {
                    "Planning Time — so'rov rejasini tuzish vaqti; Execution Time — so'rovning amaldagi bajarilish vaqti; Cost — rejalashtirgichning taxminiy I/O va CPU resurs bahosi",
                    "Execution Time faqat kesh vaqtini ko me'rsatadi",
                    "Cost real pul sarfini bildiradi",
                    "EXPLAIN ANALYZE so'rovni bajarmaydi"
                },
                "EXPLAIN ANALYZE so'rovni amalda ijro etadi va har bir tugun bajarilish vaqti hamda resurs xarajatini ko'rsatadi."),

            CreateQuestion("Database Lock turlaridan Shared Lock (S) va Exclusive Lock (X) o me me'rtasidagi farq nima?",
                new List<string> {
                    "Shared Lock (S) — bir vaqtda ko'plab o'qishlarga ruxsat beradi; Exclusive Lock (X) — yozish/yangilash uchun boshqa barcha o'qish va yozishlarni bloklaydi",
                    "Shared Lock yozishga ruxsat beradi",
                    "Exclusive Lock o'qishni taqiqlamaydi",
                    "Ular bir xil qulf turi"
                },
                "Shared Lock o'qish uchun ko'plab tranzaksiyalarga beriladi. Exclusive Lock esa faqat 1 ta yozuvchi tranzaksiyaga berilib boshqalarni bloklaydi."),

            CreateQuestion("Database Locking strategiyalarida Pessimistic Locking va Optimistic Locking o'rtasidagi tanlov mezoni nima?",
                new List<string> {
                    "Pessimistic Locking (SELECT ... FOR UPDATE) — toqnashuv yuqori bo'lganda qatorni darhol qulflaydi; Optimistic Locking — Version kolonka orqali toqnashuv kam bo'lganda ishlatiladi",
                    "Optimistic Locking har doim bazani qulflaydi",
                    "Pessimistic Locking faqat NoSQL-da bo me'ladi",
                    "Ular mutlaqo bir xil"
                },
                "Optimistic Locking toqnashuv kam bo'lgan tizimlarda qulflash xarajatini kamaytiradi. Pessimistic Locking esa toqnashuv ko'p joyda to me me'g'ridan-to'g'ri lock beradi."),

            CreateQuestion("PostgreSQL Partial Index (CREATE INDEX ... WHERE condition) ishlatishning afzalligi nimada?",
                new List<string> {
                    "Faqat muayyan shartga mos keladigan qatorlarni indekslab (masalan WHERE status = 'ACTIVE'), indeks hajmini keskin qisqartiradi va unumdorlikni oshiradi",
                    "Partial Index jadvalni 2 ga bo'ladi",
                    "Partial Index so'rovlarni sekinlashtiradi",
                    "U faqat NoSQL-da bo'ladi"
                },
                "Partial Index faqat kerakli qatorlar (masalan faqat aktiv foydalanuvchilar) uchun indeks yaratib RAM xotirasini tejaydi."),

            CreateQuestion("PostgreSQL Expression Index (CREATE INDEX ... ON Users(LOWER(Email))) qachon kerak bo'ladi?",
                new List<string> {
                    "So'rovda ustunga funksiya qo'llanilganda (masalan WHERE LOWER(Email) = '...'), oddiy indeks ishlamay qolishini oldini olib funksiya natijasini indekslash uchun",
                    "Expression Index faqat int ustunlarda ishlaydi",
                    "Oddiy B-Tree funksiyalarda ham har doim ishlaydi",
                    "U faqat NoSQL bazalarda ishlaydi"
                },
                "Agar so me'rov shartida ustun funksiyaga o'ralsa (SARGable bo'lmasa), oddiy indeks ishlamaydi va Expression Index kerak bo'ladi."),

            CreateQuestion("Redis Pub/Sub (PUBLISH/SUBSCRIBE) va Redis Streams (XADD/XREADGROUP) o'rtasidagi me'moriy farq nima?",
                new List<string> {
                    "Pub/Sub xabarni tinglovchi bo'lmasa yo me'qotadi (Fire-and-forget); Redis Streams esa xabarlarni saqlaydi va Consumer Group, Offset va Ack beradi",
                    "Pub/Sub xabarlarni diskda saqlaydi",
                    "Redis Streams xabarni saqlamaydi",
                    "Ular bir xil xizmat"
                },
                "Redis Pub/Sub xabarlarni saqlamaydi va obunachi bo'lmasa yo'qoladi. Redis Streams esa Kafka kabi append-only log va consumer groups beradi."),

            CreateQuestion("Database Materialized Views uchun REFRESH MATERIALIZED VIEW CONCURRENTLY buyrug'ining afzalligi nima?",
                new List<string> {
                    "Materialized View yangilanayotgan vaqtda o me me'qish so'rovlarini bloklamasdan (Exclusive Lock-siz) fon rejimida yangilash imkonini beradi",
                    "CONCURRENTLY jadvalni o me'chirib yuboradi",
                    "CONCURRENTLY faqat 1 ta qatorni yangilaydi",
                    "U faqat NoSQL-da bo'ladi"
                },
                "CONCURRENTLY parometri yangilanish vaqtida jadvalni bloklamaydi, lekin uning uchun unikal indeks mavjud bo'lishi shart."),

            CreateQuestion("PostgreSQL-da Database Triggers va Stored Procedures ishlatishning foydasi va arxitekturaviy tavakkalchiligi nima?",
                new List<string> {
                    "Foydasi — baza darajasida avtomatik biznes mantiq va audit log; Tavakkalchiligi — yashirin yon ta me me me'sirlar, debug qilish qiyinligi va CPU yuklamasi",
                    "Triggers har doim dasturni tezlashtiradi",
                    "Stored Procedures testlashni osonlashtiradi",
                    "Ular o'rtasida tavakkalchilik yo'q"
                },
                "Triggers yashirin holda bazada bajarilib, debugging-ni qiyinlashtirishi va kutilmagan yon ta'sirlar berishi mumkin."),

            CreateQuestion("PostgreSQL-da UPSERT operatsiyasi (INSERT ... ON CONFLICT (id) DO UPDATE) qanday ishlaydi?",
                new List<string> {
                    "Qator mavjud bo'lmasa INSERT qiladi, agar unikal kalit bo'yicha toqnashuv (Conflict) bo'lsa uni UPDATE qilib o'zgartiradi",
                    "ON CONFLICT xatolik otib dasturni to'xtatadi",
                    "UPSERT faqat NoSQL bazalarda ishlaydi",
                    "UPSERT har doim jadvalni tozalaydi"
                },
                "PostgreSQL-da `ON CONFLICT DO UPDATE` yordamida bitta atomik so'rovda foydalanuvchi bor bo'lsa update, yo'q bo'lsa insert qilinadi.")
        };
    }

    private static List<Question> GenerateDatabaseHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("PostgreSQL MVCC (Multi-Version Concurrency Control) ichki tuzilishi va xmin/xmax yashirin ustunlari qanday ishlaydi?",
                new List<string> {
                    "Har bir qator versiyasida xmin (yaratgan tranzaksiya ID) va xmax (o'chirgan/yangilagan tranzaksiya ID) saqlanadi; Bu o'qish va yozish operatsiyalarining alohida parallel ishlashini ta'minlaydi",
                    "xmin va xmax faqat Primary Key qiymatlarini saqlaydi",
                    "MVCC faqat NoSQL bazalarda bo'ladi",
                    "xmin faqat server soatini saqlaydi"
                },
                "MVCC-da qatorlar ustidan to'g'ridan-to'g'ri yozilmaydi. Yangi versiya yaratilib xmin/xmax orqali ko'rinuvchanlik (visibility) boshqariladi."),

            CreateQuestion("PostgreSQL VACUUM va AUTOVACUUM mexanizmlari Dead Tuples (o'lik qatorlar) va Bloat management-ni qanday amalga oshiradi?",
                new List<string> {
                    "MVCC natijasida qolgan o'lik qatorlarni (dead tuples) tozalab bo'sh joyni qayta ishlatish uchun belgilaydi; VACUUM FULL esa jadvalni qayta qurib diskka joyni qaytaradi (Exclusive Lock)",
                    "VACUUM barcha indekslarni o'chirib tashlaydi",
                    "AUTOVACUUM har bir soniyada serverni qayta tushiradi",
                    "VACUUM faqat Readonly bazalarda ishlaydi"
                },
                "AUTOVACUUM eskirgan va o'chirilgan tuple-larni bo'sh joy sifatida belgilaydi. VACUUM FULL esa jadvalni qayta yozib disk joyini OS ga qaytaradi."),

            CreateQuestion("PostgreSQL Logical Replication va Write-Ahead Logging (WAL) internals qanday ishlaydi?",
                new List<string> {
                    "WAL fayllaridan SQL o'zgarishlar oqimi (Logical Decoding) ajratib olinib, jadval darajasida boshqa PostgreSQL tugunlariga nusxalanadi",
                    "Logical Replication faqat fayllarni nusxalaydi",
                    "WAL fayllari faqat In-Memory bo'ladi",
                    "Replication tranzaksiyalarni o'chirib yuboradi"
                },
                "Logical replication WAL oqimidan jadval o'zgarishlarini (INSERT/UPDATE/DELETE) mantiqiy dekodlab boshqa serverga uzatadi."),

            CreateQuestion("PostgreSQL Partitioning (Range, List, Hash) va Partition Pruning optimizatsiyasi qanday ishlaydi?",
                new List<string> {
                    "So'rovdagi WHERE shartiga qarab, PostgreSQL query planner faqat kerakli bo'lim (partition) jadvalini o'qiydi va qolganlarini (Partition Pruning) chetlab o'tadi",
                    "Partitioning jadvalni o'chirib beradi",
                    "Pruning so'rovni sekinlashtiradi",
                    "Partitioning faqat 100 ta qatorda ishlaydi"
                },
                "Partition Pruning so'rov shartiga mos kelmaydigan bo'lim jadvallarni rejalashtiruvchi darajasida o'qishdan chiqarib tashlaydi."),

            CreateQuestion("Distributed Databases-da CAP Teoremasi (Consistency, Availability, Partition Tolerance) va PACELC kengaytmasi nimani ta'kidlaydi?",
                new List<string> {
                    "Tarmoq uzilishi (Partition) bo'lganda tizim yo Izchillikni (Consistency) yo Mavjudlikni (Availability) tanlashi shart; PACELC esa oddiy holatda Latency va Consistency tanlovini qo'shadi",
                    "CAP teoremasi barcha 3 ta sifatni 100% ta'minlashni talab qiladi",
                    "Partition Tolerance faqat bitta kompyuterda bo'ladi",
                    "PACELC faqat SQL Server-da ishlaydi"
                },
                "CAP teoremasiga ko'ra taqsimlangan tizim tarmoq bo'linganda (Partition) bir vaqtning o'zida ham 100% Consistency, ham 100% Availability berolmaydi."),

            CreateQuestion("PostgreSQL Serializable Snapshot Isolation (SSI) va Write Skew Anomaly qanday hal etiladi?",
                new List<string> {
                    "SSI tranzaksiyalar o'rtasidagi SIREAD lock-lar va graflarni tahlil qilib, Write Skew sodir bo'lishi bilan birini abort qiladi va 100% ketma-ketlikni kafolatlaydi",
                    "SSI faqat jadvalni to'liq qulflaydi (Table Lock)",
                    "Write Skew anomaliyasi hesh qachon kelib chiqmaydi",
                    "SSI faqat Read Uncommitted-da ishlaydi"
                },
                "PostgreSQL SSI mexanizmi hech qanday og'ir lock qo'ymasdan SIREAD lock-lar orqali Write Skew anomaliyasini aniqlab abort qiladi."),

            CreateQuestion("PostgreSQL-da Advisory Locks (pg_advisory_lock) qaysi holatlarda va qanday ishlatiladi?",
                new List<string> {
                    "Jadval yoki qatorga bog'liq bo'lmagan, dasturiy mantiq darajasidagi (Application-level) taqsimlangan qulflashlarni bajarish uchun",
                    "Faqat jadvalni o'chirish uchun",
                    "Faqat foydalanuvchi parolini tekshirish uchun",
                    "Faqat backup olish uchun"
                },
                "Advisory Locks PostgreSQL tomonidan taqdim etiladigan, jadval va qatorlarga bog'liq bo'lmagan dasturiy ma'no berilgan qulflardir."),

            CreateQuestion("MongoDB WiredTiger saqlash dvigateli (Storage Engine) xotira keshini va Concurrency control-ni qanday boshqaradi?",
                new List<string> {
                    "Document-level Concurrency Control (hujjat darajasidagi qulflash) va Ticket-based execution hamda Snappy/Zlib siqishni beradi",
                    "WiredTiger to'liq jadval darajasida qulflaydi (Table Lock)",
                    "WiredTiger faqat RAM-da ishlaydi",
                    "WiredTiger tranzaksiyalarni taqiqlaydi"
                },
                "WiredTiger MongoDB uchun yuqori unumdorlikdagi dvigatel bo'lib, hujjat darajasidagi qulflash va kesh siqishni ta'minlaydi."),

            CreateQuestion("Redis Cluster-da Hash Slots (16384 ta slot) va Hash Tags ({user100}.orders) qanday ishlaydi?",
                new List<string> {
                    "Hash Tags {...} jingalak qavs ichidagi kalit qismini hashing qilib, tegishli ma'lumotlarni aynan bitta Redis master tuguniga (node) tushishini kafolatlaydi",
                    "Hash Slots faqat 10 ta kalit saqlaydi",
                    "Hash Tags kalitlarni o'chirish uchun ishlatiladi",
                    "Redis Cluster-da kalitlarni taqsimlab bo'lmaydi"
                },
                "Hash Tags {...} bir necha bog'liq kalitlarning aynan bitta Redis tuguniga tushishini ta'minlaydi (Multi-key operations uchun)."),

            CreateQuestion("PostgreSQL-da B-Tree indekslarida Page Splitting va FillFactor (masalan WITH (fillfactor = 70)) sozlamasi qanday rol o'ynaydi?",
                new List<string> {
                    "Indeks sahifalarida yangi UPDATE/INSERT uchun bo'sh joy qoldiradi, bu esa og'ir Page Splitting va indeks fragmentatsiyasini kamaytiradi",
                    "FillFactor jadvalni o'chirib beradi",
                    "Page Splitting so'rovlarni tezlashtiradi",
                    "FillFactor faqat NoSQL-da bo'ladi"
                },
                "FillFactor indeks varaq sahifalarida joy qoldirib, tez-tez o'zgaradigan jadvallarda Page Splitting hosil bo'lishini kamaytiradi."),

            CreateQuestion("PostgreSQL Write-Ahead Logging (WAL) va ARIES algoritmi bazaning halokatdan tiklanishini (Crash Recovery) qanday beradi?",
                new List<string> {
                    "Ma me me me'lumotlar diskka yozilishidan oldin WAL fayliga append qilinadi; Server yiqilganda WAL redolog-lari qayta o'qilib (Redo/Undo) baza holati tiklanadi",
                    "WAL fayllari faqat RAM-da saqlanadi",
                    "ARIES algoritmi barcha ma me'lumotni o'chiradi",
                    "Crash Recovery-ni amalda ilojisi yo'q"
                },
                "WAL (Write-Ahead Logging) va ARIES algoritmi relatsion bazalarga Durability va Crash Recovery kafolatini beradi."),

            CreateQuestion("PostgreSQL-da High-Availability topologies va Patroni / PgBouncer arxitekturasi qanday ishlaydi?",
                new List<string> {
                    "Patroni ZooKeeper/etcd orqali PostgreSQL cluster master-ini kuzatadi va failover bajaradi; PgBouncer esa ulanishlar pulini (Transaction pooling) samarali boshqaradi",
                    "Patroni faqat NoSQL-da bo me'ladi",
                    "PgBouncer bazani sekinlashtiradi",
                    "Ular o'rtasida bog me'liqlik yo'q"
                },
                "Patroni PostgreSQL avtomatik Failover va Leader Election beradi, PgBouncer esa minglab mijoz ulanishlarini boshqaradi."),

            CreateQuestion("Taqsimlangan ma'lumotlar bazalarida Two-Phase Commit (2PC) va Blocking Problem zaifligi nimada?",
                new List<string> {
                    "2PC Coordinator va Participants tayyorlik (Prepare) vaqtida lock tutadi; Coordinator yiqilsa barcha bazalar bloklanib osilib qoladi",
                    "2PC hech qachon lock tutmaydi",
                    "2PC har doim tez ishlaydi",
                    "U faqat MongoDB-da bo me'ladi"
                },
                "2PC tranzaksiyada Coordinator tuguni yiqilganda barcha bazalar resurslarni bloklangan holatda saqlab qoladi (Blocking Vulnerability)."),

            CreateQuestion("PostgreSQL pg_stat_statements kengaytmasi so'rovlar unumdorligini profillashda nima beradi?",
                new List<string> {
                    "Bazaga tushgan barcha SQL so me'rovlarining o me'rtacha bajarilish vaqti, I/O yuklamasi, necha marta chaqirilgani va sekin so me'rovlar statistikasini yig'adi",
                    "pg_stat_statements parollarni shifrlaydi",
                    "U faqat HTML fayllarni keshlaydi",
                    "pg_stat_statements bazani o me'chiradi"
                },
                "pg_stat_statements PostgreSQL-da eng ko'p resurs yeyotgan (Slow queries) so'rovlarni aniqlash uchun asosiy instrument hisoblanadi."),

            CreateQuestion("Redis Sentinel arxitekturasi Master Failover va Quorum o'tkazishni qanday bajaradi?",
                new List<string> {
                    "Sentinel node-lar Master tugun nosozligini ovoz berish (Quorum) orqali aniqlab, avtomatik ravishda Replica-ni yangi Master qilib tayinlaydi",
                    "Sentinel xabarlarni o'chirib beradi",
                    "Sentinel faqat Single node-da ishlaydi",
                    "Sentinel-da failover bo me'lmaydi"
                },
                "Redis Sentinel Quorum ovoz berishi orqali High Availability va avtomatik Master Failover beradi."),

            CreateQuestion("Distributed Databases-da Consistent Hashing va Hash Ring qanday ishlaydi?",
                new List<string> {
                    "Kalit va Node-larni 2^32 halqada joylashtirib, yangi node qo'shilganda yoki o'chganda ma'lumotlarning faqat K/N qismini ko'chirish imkonini beradi",
                    "Consistent Hashing barcha ma me'lumotni o me'chiradi",
                    "Consistent Hashing faqat bitta serverda ishlaydi",
                    "U faqat SQL Server-da bo me'ladi"
                },
                "Consistent Hashing taqsimlangan kesh va NoSQL bazalarda serverlar soni o'zgarganda ma'lumotlarni qayta taqsimlash xarajatini minimal qiladi."),

            CreateQuestion("PostgreSQL-da REINDEX CONCURRENTLY buyrug'ining afzalligi nimada?",
                new List<string> {
                    "B-Tree indeksi shikastlanganda yoki bloat bo'lganda, jadvaldagi yozish operatsiyalarini bloklamasdan (Exclusive Lock-siz) indeksni qayta quradi",
                    "REINDEX jadvalni o'chirib tashlaydi",
                    "CONCURRENTLY sekinroq lekin jadvalni qulflaydi",
                    "REINDEX faqat NoSQL-da bo'ladi"
                },
                "REINDEX CONCURRENTLY production muhitida o'qish va yozishni to'xtatmagan holda buzilgan/shishgan indekslarni qayta qurish imkonini beradi."),

            CreateQuestion("PgBouncer ulanishlar pulida Session Pooling va Transaction Pooling o'rtasidagi farq nima?",
                new List<string> {
                    "Session Pooling ulanishni mijoz sessiyasi tugaguncha beradi; Transaction Pooling esa ulanishni faqat 1 ta SQL tranzaksiyasi davomida berib uzadi (yuqori scalability)",
                    "Transaction Pooling tranzaksiyalarni taqiqlaydi",
                    "Session Pooling ulanishni darhol uzadi",
                    "Ular bir xil pooling turi"
                },
                "Transaction Pooling PgBouncer-ga minimal server ulanishi bilan o'n minglab mijoz so me'rovlariga xizmat ko me'rsatish imkonini beradi."),

            CreateQuestion("MongoDB Change Streams texnologiyasi qanday ishlaydi?",
                new List<string> {
                    "Oplog (Operations Log) orqali bazadagi barcha real-vaqt Hujjat o'zgarishlarini (INSERT/UPDATE/DELETE) dasturiy oqimda tinglash imkonini beradi",
                    "Change Streams bazani o'chirib beradi",
                    "Change Streams faqat fayllarni yuklaydi",
                    "U faqat SQL Server-da bo'ladi"
                },
                "MongoDB Change Streams Oplog-ni tinglab, real-vaqtda hodisalarga javob qaytarish (Event-driven Architecture) imkonini beradi."),

            CreateQuestion("PostgreSQL Query Planner-da Join algoritmlaridan Nested Loop, Hash Join va Merge Join qachon tanlanadi?",
                new List<string> {
                    "Nested Loop — kichik va indekslangan jadvallarda; Hash Join — indekslanmagan katta jadvallarda; Merge Join — ikkala jadval ham saralangan bo'lganda",
                    "Nested Loop har doim eng sekin",
                    "Hash Join faqat 1 ta qatorda ishlaydi",
                    "Query planner har doim bir xil algoritm tanlaydi"
                },
                "PostgreSQL Query Planner ma'lumotlar hajmi va indekslar mavjudligiga qarab eng maqbul Join algoritmini (Nested Loop, Hash Join, Merge Join) tanlaydi.")
        };
    }
}
