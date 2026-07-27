using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetCSharpQuizzes()
    {
        return new List<Quiz>
        {
            CreateQuiz(
                "C# Language & Core Memory Fundamentals",
                "csharp",
                "C# Dasturlash Tili",
                "Stack vs Heap, Value vs Reference types, Boxing/Unboxing, ref/out/in va IDisposable bo'yicha chuqur savollar.",
                "Easy",
                "code-2",
                GenerateCSharpEasyQuestions()
            ),
            CreateQuiz(
                "C# Advanced Memory, CLR Internals & Async Deep Dive",
                "csharp",
                "C# Dasturlash Tili",
                "GC Generations (Gen 0-2, LOH, POH), Span<T> vs Memory<T>, Async State Machine va Record Types bo'yicha senior savollar.",
                "Medium",
                "cpu",
                GenerateCSharpMediumQuestions()
            ),
            CreateQuiz(
                "C# High-Performance, Unmanaged Memory & Native CLR Architecture",
                "csharp",
                "C# Dasturlash Tili",
                "System.IO.Pipelines, Native AOT, SIMD Intrinsics, ThreadPool Starvation va Lock-Free Concurrency bo'yicha principal savollar.",
                "Hard",
                "terminal",
                GenerateCSharpHardQuestions()
            )
        };
    }

    private static List<Question> GenerateCSharpEasyQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "C# dasturlash tilida Stack va Heap xotira tuzilmalari o'rtasidagi asosiy farqlar nimada?",
                "int age = 25; // Stack\nstring name = \"Alisher\"; // Heap",
                new List<string> {
                    "Stack LIFO bo'lib qiymat turlarini saqlaydi va tez tozalanadi; Heap dinamik bo'lib havolali turlarni saqlaydi va GC tomonidan tozalanadi",
                    "Stack faqat matnli ma'lumotlarni, Heap esa raqamlarni saqlaydi",
                    "Heap xotira Stack-ga qaraganda 100 marta tezroq ishlaydi",
                    "Stack xotira hech qachon toza bo'lmaydi va xotira sizishiga olib keladi"
                },
                "Stack LIFO (Last In First Out) prinsipi bo'yicha ishlaydi va funksiya lokal o'zgaruvchilarini saqlaydi. Heap dinamik xotira bo'lib, uning tozalanishiga Garbage Collector javobgar."
            ),
            CreateQuestion(
                "C#-da Boxing va Unboxing jarayonlari nima va uning perfomansga ta'siri qanday?",
                "int val = 100;\nobject boxed = val; // Boxing\nint unboxed = (int)boxed; // Unboxing",
                new List<string> {
                    "Boxing Value Type-ni Heap-dagi Object-ga o'g'iradi (yangi obyekt yaratadi); Unboxing esa uni qaytaradi. Bu Heap-da ortiqcha allocation va GC yukini oshiradi",
                    "Boxing faqat string-larni shifrlash uchun ishlatiladi va perfomansga ta'sir qilmaydi",
                    "Unboxing xotirani zudlik bilan tozalaydi va tezlikni oshiradi",
                    "Boxing va Unboxing faqat multithreading operatsiyalarida ishlaydi"
                },
                "Boxing Value Type-ni Heap-ga joylab ob'ekt yaratadi, bu esa ortiqcha xotira ajratilishi va GC yig'ilishiga sabab bo'ladi."
            ),
            CreateQuestion(
                "C#-da `struct` va `class` o'rtasidagi asosiy farqlar nimalardan iborat va qachon `struct` ishlatish kerak?",
                "public struct Point { public int X; public int Y; }",
                new List<string> {
                    "Struct — Value Type (Stack-da saqlanadi, vorislikni qo'llamaydi), Class — Reference Type. Hajmi 16 baytdan kichik, immutable obyektlar uchun struct mos",
                    "Class faqat static metodlar uchun, Struct esa faqat interfeyslar uchun ishlatiladi",
                    "Struct Heap-da saqlanadi va vorislikni to'liq qo'llab-quvvatlaydi",
                    "Class va Struct o'rtasida hech qanday xotira farqi yo'q"
                },
                "Struct Value Type hisoblanib Stack-da joylashadi. U vorislikni (inheritance) qo'llamaydi va kichik hajmli ma'lumotlar uchun mos keladi."
            ),
            CreateQuestion(
                "C#-da `readonly struct` va `ref struct` turlari xotira optimallashda qanday rol o'ynaydi?",
                "public readonly ref struct CustomSpan { ... }",
                new List<string> {
                    "readonly struct defensive copying (himoya nusxalash) ni oldini oladi; ref struct esa obyektni faqat Stack-da joylashishini majburiy qiladi",
                    "readonly struct xotirani shifrlaydi, ref struct esa uni diskka saqlaydi",
                    "ref struct faqat async metodlar ichida ishlatish uchun yaratilgan",
                    "Ikkala struct turi ham ob'ektlarni Heap-ga majburiy ko'chiradi"
                },
                "readonly struct maydonlar o'zgarmasligini kafolatlab defensive copy-ni oldini oladi. ref struct esa ob'ektning Heap-ga o'tib ketmasligini va faqat Stack-da saqlanishini majbur qiladi."
            ),
            CreateQuestion(
                "Metod parametrlarida `ref`, `out` va `in` kalit so'zlarining farqlari nimada?",
                "public void Process(in int id, ref int count, out string status)",
                new List<string> {
                    "ref boshlang'ich qiymat talab qiladi; out metod ichida qiymat tayinlanishini shart qiladi; in esa havolani readonly rejimida uzatadi",
                    "out parametrlarga boshlang'ich qiymat berish majburiy",
                    "in parametri qiymatni metod ichida o'zgartirishga ruxsat beradi",
                    "Ushbu uchta kalit so'z ham bir xil vazifani bajaradi"
                },
                "ref boshlang'ich qiymatga ega havolani uzatadi, out metod ichida qiymat tayinlanishini kafolatlaydi, in esa qiymatni o'zgarmas readonly havola sifatida uzatadi."
            ),
            CreateQuestion(
                "IDisposable va Finalizer (~Destructor) o'rtasidagi farq nimada va Dispose Pattern qanday qo'llaniladi?",
                "public void Dispose() {\n    Dispose(true);\n    GC.SuppressFinalize(this);\n}",
                new List<string> {
                    "IDisposable unmanaged resurslarni dasturchi tomonidan aniq vaqtda bo'shatadi; Finalizer esa GC obyektni o'chirayotganda avtomatik chaqiriladi",
                    "Finalizer-ni dasturchi to'g'ridan-to'g'ri kodingizda chaqira oladi",
                    "IDisposable faqat Stack xotirasini tozalash uchun ishlatiladi",
                    "GC.SuppressFinalize(this) Finalizer-ni birinchi navbatda chaqirishni buyuradi"
                },
                "IDisposable deterministik (dasturchi xohlagan vaqtda) resurs tozalash uchun ishlatiladi. GC.SuppressFinalize(this) chaqirilganda GC ushbu ob'ekt Finalizer-ini ortiqcha chaqirib o'tirmaydi."
            ),
            CreateQuestion(
                "C#-da `string` turi bo'yicha qaysi ta'rif to'g'ri va nima uchun String Manipulation uchun `StringBuilder` tavsiya etiladi?",
                "string str = \"A\";\nfor(int i=0; i<1000; i++) str += i; // Bad!",
                new List<string> {
                    "String — Immutable Reference Type bo'lib, har bir o'zgarishda yangi obyekt yaratadi; StringBuilder esa bitta bufer ichida o'zgartirish kiritadi",
                    "String Value Type bo'lib Stack-da saqlanadi",
                    "StringBuilder har bir operatsiyada GC tozalashini chaqiradi",
                    "String va StringBuilder o'rtasida unumdorlik farqi yo'q"
                },
                "String immutable bo'lgani uchun har bir tsikldagi `+=` yangi ob'ekt yaratadi. StringBuilder esa bitta bufer (char array) ustida ishlab GC ga yuk tushirmaydi."
            ),
            CreateQuestion(
                "C#-da `IEnumerable<T>` va `IQueryable<T>` o'rtasidagi asosiy farq nimada?",
                "IEnumerable<User> list = db.Users; // In-Memory\nIQueryable<User> query = db.Users; // SQL Translation",
                new List<string> {
                    "IEnumerable filtrni xotiraga (In-Memory) yuklab olgandan keyin bajaradi; IQueryable filtrni Expression Tree sifatida ma'lumotlar bazasiga SQL qilib yuboradi",
                    "IQueryable faqat massivlar bilan ishlaydi",
                    "IEnumerable faqat SQL Server bilan ishlaydi",
                    "Ikkala interfeys ham so'rovni bir xil vaqtda ma'lumotlar bazasiga yuboradi"
                },
                "IQueryable Expression Tree hosil qilib so'rovni bazaga SQL sifatida yuboradi (Server-side evaluation). IEnumerable esa barcha ma'lumotlarni o'qib bo'lgach xotirada filtrlaydi."
            ),
            CreateQuestion(
                "C# 9+ da `record` va `class` o'rtasidagi asosiy farq va `with` expression qanday ishlaydi?",
                "var p1 = new Person(\"Ali\", 25);\nvar p2 = p1 with { Age = 26 };",
                new List<string> {
                    "Record-lar Value-based equality qo'llaydi va `with` ifodasi mavjud obyektning nusxasini olib, belgilangan maydonlarini o'zgartirib yangi obyekt beradi",
                    "Record qiymatlarini keyinchalik o'zgartirib bo'lmaydi va `with` uni o'chirib yuboradi",
                    "Class `with` ifodasini to'liq qo'llab-quvvatlaydi",
                    "Record va Class xotirada mutlaqo bir xil tenglikni (Reference equality) tekshiradi"
                },
                "Record-larda tenglik obyektlar havolasiga emas, qiymatlariga qarab (Value-based) aniqlanadi va `with` iborasi unumli nondestructive mutation qiladi."
            ),
            CreateQuestion(
                "C#-da Nullable Reference Types (`string?`) mexanizmi qanday ishlaydi va u kompilyatsiya bosqichida nimani kafolatlaydi?",
                "string? nullableName = null;\nstring name = nullableName!; // Null-forgiving",
                new List<string> {
                    "Kompilyatsiya vaqtida NullReferenceException xavfini kamaytiradi, ammo CLR runtime darajasida qo'shimcha tur yaratmaydi (static analysis)",
                    "Runtime-da null qiymat tushsa uni avtomatik bo'sh string ga o'zgartiradi",
                    "Nullable reference type-lar qiymat turiga aylanadi (struct)",
                    "Kompilyatordan keyin ilova tezligini 2 marta oshiradi"
                },
                "Nullable Reference Types C# kompilyatori darajasida statik tahlil o'tkazib NRE xatoliklarini oldini olishga yordam beradi, runtime-da yangi tip hosil qilmaydi."
            )
        };
    }

    private static List<Question> GenerateCSharpMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "Garbage Collector (GC) avlodlari (Gen 0, Gen 1, Gen 2, LOH, POH) qanday vazifa bajaradi?",
                "GC.Collect(2, GCCollectionMode.Forced);",
                new List<string> {
                    "Gen 0 — yangi va qisqa umrli obyektlar; Gen 1 — o'tish buferi; Gen 2 — uzoq yashovchi obyektlar; LOH — 85KB dan katta obyektlar; POH — manzili o'zgarmas (pinned) obyektlar",
                    "Gen 0 faqat static o'zgaruvchilar uchun mo'ljallangan",
                    "LOH obyektlari har bir millisoniyada majburiy tozalanadi",
                    "POH obyektlari Gen 0 va Gen 1 tozalanganda avtomatik Stack-ga ko'chiriladi"
                },
                "GC obyektlarni yashash davri bo'yicha bo'ladi. Gen 0 eng ko'p va tez tozalanadi. LOH (Large Object Heap) 85,000 baytdan katta obyektlarni saqlaydi."
            ),
            CreateQuestion(
                "C#-da `Span<T>` va `Memory<T>` o'rtasidagi asosiy cheklov va ishlatilish farqlari nimada?",
                "public async Task ProcessAsync(Memory<byte> memory) // OK\n// public async Task ProcessAsync(Span<byte> span) // COMPILE ERROR!",
                new List<string> {
                    "Span ref struct bo'lgani uchun Stack-only va u async metodlarda, class maydonlarida ishlatilmaydi; Memory esa struct bo'lib Heap-da saqlana oladi va async metodlarga mos keladi",
                    "Span faqat string-lar bilan ishlaydi, Memory esa faqat int massivlar bilan",
                    "Memory async metodlarda ishlatilsa xotira sizishiga olib keladi",
                    "Span va Memory o'rtasida hech qanday sintaktik yoki xotira cheklovi yo'q"
                },
                "Span `ref struct` bo'lgani sababli Stack-dan Heap-ga ko'chib ketishi taqiqlangan (async/await, class fields). Memory esa bu cheklovga ega emas."
            ),
            CreateQuestion(
                "Async/Await asinxron modelida `ConfigureAwait(false)` ishlatishning asosiy maqsadi va muhiti nima?",
                "await client.GetStringAsync(url).ConfigureAwait(false);",
                new List<string> {
                    "Davom etuvchi kodni (continuation) asl SynchronizationContext-ga qaytishini shart qilmasdan ThreadPool thread-ida bajaradi va deadlock-ni oldini oladi",
                    "So'rov bajarilishini 2 marta tezlashtiradi",
                    "Asinxron metodni sinxron metodga o'zgardi",
                    "Faqat UI hodisalarini ushlash uchun ishlatiladi"
                },
                "ConfigureAwait(false) davomini asl SynchronizationContext-ga majburan qaytarmaydi, bu kutubxonalar va backend servislarida unumdorlikni oshiradi va deadlock-ni oldini oladi."
            ),
            CreateQuestion(
                "Async/Await metodlarida `ValueTask<T>` va `Task<T>` o'rtasidagi farq va qachon `ValueTask` ishlatish kerak?",
                "public ValueTask<int> GetCachedValueAsync(string key)",
                new List<string> {
                    "Agarda metod natijasi ko'pincha sinxron (masalan keshdan) qaytsa, ValueTask Heap-da Task ob'ekti yaratilishini (allocation) oldini oladi",
                    "ValueTask har doim Task-ga qaraganda 10 marta sekinroq ishlaydi",
                    "ValueTask-ni bir necha marta await qilish tavsiya etiladi",
                    "Task faqat void metodlar uchun ishlatiladi"
                },
                "Agarda natija allaqachon tayyor (keshda) bo'lsa, ValueTask Stack-da qaytib Task ob'ekti yaratilishini (allocation) bartaraf etadi."
            ),
            CreateQuestion(
                "C#-da `Interlocked` operatsiyalarining (masalan Interlocked.Increment) oddiy `lock` (Monitor) dan afzalligi nimada?",
                "Interlocked.Increment(ref _counter);",
                new List<string> {
                    "Hardware CPU atomic ko'rsatmalaridan foydalanadi va thread-larni bloklamasdan (Lock-free) va context switch-siz o'ta yuqori tezlik beradi",
                    "Faqat fayllarni o'qish uchun ishlatiladi",
                    "Garbage Collection-ni to'xtatib qo'yadi",
                    "Lock-ga qaraganda sekinroq ishlaydi"
                },
                "Interlocked operatsiyalari atomar CPU yo'riqnomalari bilan ishlaydi, thread-ni bloklamaydi (kernel-level context switch bo'lmaydi)."
            ),
            CreateQuestion(
                "System.Threading.Channels (Channel<T>) kutubxonasining `BlockingCollection<T>` ga nisbatan asosiy afzalligi nimada?",
                "var channel = Channel.CreateBounded<WorkItem>(100);",
                new List<string> {
                    "Asinxron (async/await) Producer-Consumer modelini to'liq qo'llaydi va thread-larni bloklamasdan high-throughput ma'lumot oqimini beradi",
                    "Faqat SQL Server bilan ishlaydi",
                    "Faqat bitta thread bilan ishlay oladi",
                    "Xabarlarni har doim diskka yozadi"
                },
                "Channel<T> asinxron oqim va backpressure-ni qo'llab-quvvatlaydi, thread-larni sinxron bloklamaydi."
            ),
            CreateQuestion(
                "C# Source Generators texnologiyasi qanday ishlaydi va uning an'anaviy Reflection-dan afzalligi nimada?",
                "[JsonSerializable(typeof(UserDto))]\npublic partial class AppJsonContext : JsonSerializerContext { }",
                new List<string> {
                    "Kompilyatsiya vaqtida kodingizni tahlil qilib yangi C# kodini generatsiya qiladi; Reflection kabi runtime xarajat va JIT overhead tug'dirmaydi",
                    "Runtime-da kodingizni o'chirib beradi",
                    "Faqat In-Memory bazalarda ishlaydi",
                    "Faqat Windows OS-da ishlaydi"
                },
                "Source Generators kompilyatsiyada ishlaydi va AOT/Zero-reflection kabi yuqori unumdorlikni ta'minlaydi."
            ),
            CreateQuestion(
                "C#-da `Yield Return` (Iterator State Machine) qanday ishlaydi va uning `Lazy Evaluation` konseptiga bog'liqligi nimada?",
                "public IEnumerable<int> GetNumbers() {\n    yield return 1;\n    yield return 2;\n}",
                new List<string> { "C# kompilyatori state machine sinfini yaratadi; Elementlar faqat foreach yoki MoveNext() chaqirilganda ketma-ket hisoblanadi (Lazy)", "Barcha elementlarni darhol xotiraga massiv qilib yuklaydi", "Faqat bir marta ishlatilishi mumkin", "Metod bajarilayotganda barcha thread-larni to'xtatadi" },
                "yield return elementlarni bir yo'la emas, talab qilinganda (on-demand) birma-bir hisoblab beradi."
            ),
            CreateQuestion(
                "C#-da `EventHandler` ishlatilganda xotira sizishi (Memory Leak) qanday kelib chiqadi va uni oldini olish usuli qaysi?",
                "publisher.OnDataChanged += subscriber.HandleData; // Potential leak!",
                new List<string> { "Publisher uzoq yashasa, Subscriber ob'ektiga kuchli havola (strong reference) saqlab qoladi va GC uni o'chira olmaydi; Unsubscribe qilish yoki WeakReference ishlatish shart", "Subscriber avtomatik tozalanadi", "Event-lar Heap-da emas, Stack-da saqlanadi", "Event-larni ishlatish GC-ni o'chirib qo me me me'yor qo'yadi" },
                "Uzun umrli Publisher ob'ekti Subscriber ob'ektini havola bilan ushlab turadi va GC o'chirishiga tosqinlik qiladi."
            ),
            CreateQuestion(
                "C# Expression Trees (`Expression<Func<T, bool>>`) va oddiy Delegate (`Func<T, bool>`) o'rtasidagi asosiy farq nimada?",
                "Expression<Func<User, bool>> expr = u => u.Age > 18; // Data structure\nFunc<User, bool> func = u => u.Age > 18; // Executable code",
                new List<string> { "Expression Tree koddagi mantiqni ob'ektlar daraxti (data structure) sifatida saqlaydi (masalan SQL ga o'girish uchun); Delegate esa ijro etiluvchi IL kodi", "Delegate-ni SQL so'roviga o'girish mumkin", "Expression Tree faqat string-larni solishtiradi", "Ikkalasi ham mutlaqo bir xil bajariladi" },
                "Expression Tree koddagi mantiqni tahlil qilish uchun ob'ektlar ierarxiyasi bo'lib saqlaydi, EF Core uni SQL-ga aylantiradi."
            )
        };
    }

    private static List<Question> GenerateCSharpHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "System.IO.Pipelines (PipeReader/PipeWriter) yordamida Zero-Allocation High-Throughput I/O qanday amalga oshiriladi?",
                "ReadResult result = await reader.ReadAsync();\nReadOnlySequence<byte> buffer = result.Buffer;",
                new List<string> {
                    "Sinflar va bayt massivlarini qayta-qayta yaratmasdan, MemoryPool buferlaridan unumli foydalanib, to'g'ridan-to'g'ri xotira bo'laklari bilan ishlaydi",
                    "Faqat fayllarni diskka shifrlab yozish uchun ishlatiladi",
                    "Har bir bayt o'qilganda GC.Collect chaqiriladi",
                    "Kestrel server bilan ishlamaydi"
                },
                "System.IO.Pipelines xotirada buferlarni qayta ishlatadi (MemoryPool) va yuqori tezlikdagi tarmog' operatsiyalarida 0-allocation beradi."
            ),
            CreateQuestion(
                "Native AOT (Ahead-Of-Time) kompilyatsiyasi .NET 8/9 da qanday ishlaydi va uning asosiy cheklovi nimada?",
                "// PublishAot = true",
                new List<string> {
                    "IL kodni to'g'ridan-to'g me'yorida mashina kodi (machine code) ga o'giradi (JIT-siz), ishga tushish tezligi va xotira hajmi minimal bo'ladi; Lekin dynamic Reflection va Emit cheklanadi",
                    "JIT kompilyatsiyani 10 marta sekinlashtiradi",
                    "Faqat Windows-da ishlaydi va Linux-ni qo'llamaydi",
                    "Ma'lumotlar bazasidan foydalanishni taqiqlaydi"
                },
                "Native AOT IL o'rniga to'g'ridan-to'g'ri mashina kodini beradi, tez ishga tushadi, lekin dinamik Reflection va Code Generation cheklanadi."
            ),
            CreateQuestion(
                "CLR JIT Compilerni Tiered Compilation (Darajali kompilyatsiya) mexanizmi qanday ishlaydi?",
                "// Tier 0 (Quick JIT) -> Tier 1 (Optimized JIT / Dynamic PGO)",
                new List<string> {
                    "Dastlab metodlar tez va optimizatsiyasiz (Tier 0) kompilyatsiya qilinadi; Metod ko'p chaqirilsa, u orqa fonda (Tier 1) yuqori optimizatsiya bilan qayta JIT qilinadi",
                    "Metodlarni har soniyada o'chirib qayta tuzadi",
                    "Faqat AOT ilovalarda ishlaydi",
                    "Dastur xotirasini 10 marta oshiradi"
                },
                "Tiered Compilation tezkor start uchun Tier 0 (Quick JIT) va ko'p chaqiriladigan ko'p takrorlanadigan kodlar uchun Tier 1 (Optimized JIT) ishlatadi."
            ),
            CreateQuestion(
                "C#-da Hardware Intrinsics va SIMD (Single Instruction Multiple Data) yo me me'yori orqali kodni apparat darajasida parallellashtirish qanday ishlaydi?",
                "Vector128<float> v1 = Vector128.Create(1.0f, 2.0f, 3.0f, 4.0f);",
                new List<string> {
                    "Protsessorning AVX/SSE ko'rsatmalaridan foydalanib bitta instruksiya bilan bir nechta ma'lumotlarni parallel hisoblaydi",
                    "Faqat GPU xotirasi bilan ishlaydi",
                    "Faqat string-larni birlashtiradi",
                    "Multithreading-ni o'chirib qo'yadi"
                },
                "SIMD protsessor apparat buyruqlari yordamida (SSE/AVX) bir paytning o'zida ko'plab sonli amallarni parallel bajaradi."
            ),
            CreateQuestion(
                "C#-da `Unsafe` va `fixed` kalit so'zlari yordamida ko'rsatgichlar (pointers) bilan ishlashda GC xotira harakati qanday nazorat qilinadi?",
                "fixed (byte* p = byteArray) { /* use p */ }",
                new List<string> {
                    "fixed kalit so'zi obyektni GC ko'chirmasligi uchun Pinned (qo'zg'almas) qiladi va uning xotira manzilini ko'rsatgich (pointer) orqali xavfsiz o'qishga imkon beradi",
                    "fixed ob'ektni Heap-dan oshirib Stack-ga ko'chiradi",
                    "fixed xotiradan ob'ektni zudlik bilan o'chirib tashlaydi",
                    "Unsafe faqat SQL so'rovlarida ishlatiladi"
                },
                "fixed iborasi obyektni GC tozalash jarayonida o'rnidan jildirmaslik uchun 'pin' qilib ko'rsatgich bilan ishlash imkonini beradi."
            ),
            CreateQuestion(
                "Multithreading dasturlashda False Sharing (soxta ulashish) hodisasi nima va u CPU L1/L2 keshiga qanday salbiy ta'sir ko'rsatadi?",
                "[StructLayout(LayoutKind.Explicit)]\npublic struct CacheAligned { [FieldOffset(0)] public long Counter1; [FieldOffset(64)] public long Counter2; }",
                new List<string> {
                    "Ikki alohida thread bir xil CPU Cache Line (masalan 64 bayt) ichida joylashgan turli o'zgaruvchilarni yangilaganda kesh doimiy inaktivatsiyaga uchrab tezlik tushadi",
                    "Faqat RAM to'lib qolganda yuzaga keladi",
                    "Diskka xabar yozishda paydo bo'ladi",
                    "Faqat Single-core protsessorlarda bo'ladi"
                },
                "False Sharing bir xil CPU Cache Line ichidagi turli o'zgaruvchilar har xil iplar tomonidan o'zgartirilganda kesh liniyasining majburiy invalidate bo'lishiga olib keladi."
            ),
            CreateQuestion(
                "TaskCompletionSource<T> va ManualResetEventSlim yordamida asinxron kutish va hodisalar (Signals) qanday muvofiqlashtiriladi?",
                "var tcs = new TaskCompletionSource<bool>();\n// Later in event handler:\ntcs.SetResult(true);",
                new List<string> {
                    "Callback yoki event-ga asoslangan sinxron koddagi hodisani asinxron Task-ga aylantirib `await` qilish imkonini beradi",
                    "Faqat fayllarni yuklashda ishlaydi",
                    "Thread-ni abadiy bloklab qo'yadi",
                    "Faqat ASP.NET Core controller-da ishlaydi"
                },
                "TaskCompletionSource<T> hodisa kelganda Task-ni yakunlash (SetResult/SetException) orqali asinxron kutish imkonini beradi."
            ),
            CreateQuestion(
                "C#-da `ArrayPool<T>` ishlatishning GC LOH (Large Object Heap) fragmentation-ga ta'siri nimada?",
                "byte[] buffer = ArrayPool<byte>.Shared.Rent(1024);\ntry { /* use buffer */ } finally { ArrayPool<byte>.Shared.Return(buffer); }",
                new List<string> {
                    "Katta bayt massivlarini qayta ishlatish (pool) orqali LOH-da tez-tez yangi massiv yaratilishini va xotira bo me me'yoriy fragmentatsiyasini oldini oladi",
                    "Massivlarni xotirada 2 marta qisqartiradi",
                    "Faqat string turlarida ishlaydi",
                    "ArrayPool massivlarni har safar o'chirib beradi"
                },
                "ArrayPool.Shared massiv buferlarini qayta ishlatib LOH allocation va fragmentation-ni bartaraf qiladi."
            ),
            CreateQuestion(
                "Lock-free dasturlashda Memory Barrier (`Thread.MemoryBarrier` yoki `Volatile.Read`) ning vazifasi nimada?",
                "int val = Volatile.Read(ref _flag);",
                new List<string> {
                    "Protsessor va kompilyatorning ko'rsatmalarni qayta tartiblashi (Instruction reordering) ni oldini oladi va kesh xotira xushxabarini kafolatlaydi",
                    "Faqat faylga yozishni to'xtatadi",
                    "Thread Pool-ni o'chirib qo'yadi",
                    "Garbage Collector-ni chaqiradi"
                },
                "Memory Barrier CPU va Kompilyatordan ko'rsatmalarni o'zboshimchalik bilan qayta tartiblamaslikni va xotiradan aniq o'qishni talab qiladi."
            ),
            CreateQuestion(
                "C#-da custom `[AsyncMethodBuilder]` yaratish orqali asinxron metodning ijro qilish mexanizmini o'zgartirish qanday maqsadlarda ishlatiladi?",
                "[AsyncMethodBuilder(typeof(CustomValueTaskMethodBuilder<>))]\npublic async ValueTask<int> ExecuteCustomAsync()",
                new List<string> {
                    "Async/await State Machine va uning natijasini saqlash xarajatini maxsus maxsus allocatesiz buferlarga yo'naltirish va maxsus task turlarini yaratish uchun",
                    "Faqat keshni tozalash uchun",
                    "Faqat exception-larni bostirish uchun",
                    "Faqat Visual Studio proyektini qurish uchun"
                },
                "Custom AsyncMethodBuilder kompilyator yaratadigan state machine builder-ini o'zgartirish va nolinchi allocation-li custom async turlarini yaratish imkonini beradi."
            )
        };
    }
}
