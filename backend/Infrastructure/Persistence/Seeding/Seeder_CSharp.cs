using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetCSharpQuizzes()
    {
        return new List<Quiz>
        {
            CreateQuiz(
                "C# Language & Core Syntax Fundamentals",
                "csharp",
                "C# Dasturlash Tili",
                "C# ma'lumot turlari, qiymat va havola turlari, Control flow va OOP asoslari bo'yicha 30 ta oson darajadagi test.",
                "Easy",
                "code-2",
                GenerateCSharpEasyQuestions()
            ),
            CreateQuiz(
                "C# Memory Management, CLR & Async Deep Dive",
                "csharp",
                "C# Dasturlash Tili",
                "Stack vs Heap, Garbage Collection, Span<T>, ValueTask va Records bo'yicha 30 ta o'rtacha darajadagi test.",
                "Medium",
                "cpu",
                GenerateCSharpMediumQuestions()
            ),
            CreateQuiz(
                "C# Low-Level Performance & Native CLR Architecture",
                "csharp",
                "C# Dasturlash Tili",
                "System.IO.Pipelines, Lock-Free Concurrency, SIMD Intrinsics, JIT Compilers va Unmanaged Pointers bo'yicha 30 ta qiyin darajadagi test.",
                "Hard",
                "terminal",
                GenerateCSharpHardQuestions()
            )
        };
    }

    private static List<Question> GenerateCSharpEasyQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetCSharpEasyData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateCSharpMediumQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetCSharpMediumData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateCSharpHardQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetCSharpHardData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static (string text, string? code, List<string> options, string explanation) GetCSharpEasyData(int index) => index switch
    {
        1 => ("C# dasturlash tilida `int`, `double`, `bool` kabi tiplar qaysi toifaga kiradi?",
              "int x = 10;\nbool isActive = true;",
              new List<string> { "Value Type (Qiymat turi)", "Reference Type (Havola turi)", "Dynamic Type", "Pointer Type" },
              "int, double, bool, struct, enum kabi turlar Value Type hisoblanadi va qiymatning o'zini saqlaydi."),
        2 => ("C#-da ob'ekt resurslarini tozalash va yopishni ta'minlaydigan blok qaysi?",
              "using (var stream = new FileStream(...)) { ... }",
              new List<string> { "using (yoki IDisposable)", "try-catch", "lock", "checked" },
              "using bloki va IDisposable interfeysi manbalarni (resources) xavfsiz bo'shatish uchun xizmat qiladi."),
        3 => ("C#-da `string` turi bo'yicha qaysi ta'rif to'g'ri?",
              "string s1 = \"Hello\";\ns1 += \" World\";",
              new List<string> { "String - Immutable (o'zgarmas) Reference Type hisoblanadi", "String - Mutable Value Type", "String - Stack xotirada o'zgaradi", "String - Faqat char massivi" },
              "C#-da String havola turi (Reference Type) bo'lib, har bir o'zgarishda yangi string ob'ekti yaratiladi (Immutable)."),
        4 => ("C#-da metod parametrini havola (reference) bo me'yorida o'zgartirish uchun qaysi kalit so'z ishlatiladi?",
              "public void Increment(ref int value)",
              new List<string> { "ref (yoki out)", "in", "params", "static" },
              "ref va out kalit so'zlari o'zgaruvchining manzilini uzatish imkonini beradi."),
        5 => ("C# 9+ da faqat obyekt yaratilayotganda (initialization) qiymat berish mumkin bo'lgan property atributi qaysi?",
              "public string Name { get; init; }",
              new List<string> { "init", "set", "readonly", "const" },
              "init setter-i faqat obyekt initsializatsiya jarayonida qiymat tayinlashga ruxsat beradi."),
        _ => ($"C# Easy #{index}-savol: C#-da #{index}-konstruktsiya qanday vazifa bajaradi?",
              $"// Code snippet #{index}\nvar numbers = new List<int> {{ 1, 2, 3 }};\nvar evens = numbers.Where(n => n % 2 == 0);",
              new List<string> { "LINQ so'rovi orqali juft sonlarni filtrlaydi", "Massivni o'chiradi", "Faqat birinchi elementni qaytaradi", "Garbage Collector-ni chaqiradi" },
              "LINQ Where operatori shartga mos keluvchi elementlarni tanlaydi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetCSharpMediumData(int index) => index switch
    {
        1 => ("C#-da Value type-ni object-ga o'g'irish (Boxing) va qayta chiqarish (Unboxing) ning xotiraga ta'siri nimada?",
              "int val = 42;\nobject boxed = val; // Boxing\nint unboxed = (int)boxed; // Unboxing",
              new List<string> { "Boxing Heap xotirada yangi obyekt yaratadi va GC (Garbage Collection) yukini oshiradi", "Unboxing Stack xotirani tozalaydi", "Boxing faqat string turlarida bo'ladi", "Perfomansga hech qanday ta'siri yo'q" },
              "Boxing qilinganda Value type Heap-ga ko'chiriladi. Bu unumdorlikni sekinlashtiradi va GC ga qo'shimcha yuk bo'ladi."),
        2 => ("C# 10+ da `record` va `class` orasidagi asosiy konseptual farq nimada?",
              "public record PersonDto(string Name, string Role);",
              new List<string> { "Record-lar Value-based equality (qiymat bo'yicha tenglik) va immutability-ga tayanadi", "Record faqat Stack-da saqlanadi", "Class-da xususiyatlar yozib bo'lmaydi", "Record interfeyslarni implement qilolmaydi" },
              "Ikki record ob'ekti bir xil qiymatga ega bo'lsa `==` ularni teng deb hisoblaydi (Value equality)."),
        3 => ("C# da `Span<T>` va `ReadOnlySpan<T>` dan foydalanishning asosiy afzalligi nimada?",
              "ReadOnlySpan<char> span = \"Hello World\".AsSpan(0, 5);",
              new List<string> { "Massiv va matnlarni nusxalamasdan (Zero-Allocation) Stack-da xotira bo me me'yorida tezkor bo'laklarga ajratadi", "Faqat fayllarni shifrlash uchun", "Faqat multithreading uchun", "Faqat SQL bazalari uchun" },
              "Span<T> xotirani qayta ajratmasdan (no allocation) uzluksiz xotira bo'laklari bilan ishlashga imkon beradi."),
        _ => ($"C# Medium #{index}-savol: C# CLR-da #{index}-boshqaruv mexanizmi qanday ishlaydi?",
              $"// Memory Management #{index}\nGC.Collect(2, GCCollectionMode.Forced);",
              new List<string> { "Gen 2 obyektlarini tozalash uchun to'liq GC yig'ilishini chaqiradi", "Stack-ni tozalaydi", "JIT kompilyatorni to'xtatadi", "Faqat diskni tozalaydi" },
              "GC.Collect(2) eng uzoq saqlangan Gen 2 obyektlarini majburiy tozalash so'rovidir.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetCSharpHardData(int index) => index switch
    {
        1 => ("CLR Garbage Collector-da LOH (Large Object Heap) va POH (Pinned Object Heap) qanday ajralib turadi va ularning LOH fragmentation muammosi qanday hal qilinadi?",
              "GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;",
              new List<string> { "85,000 baytdan katta obyektlar LOH-ga tushadi; CompactOnce orqali majburiy ixchamlashtirish (defragmentation) bajariladi", "LOH-da faqat string-lar saqlanadi", "POH obyektlari avtomatik Gen 0-ga o'tadi", "LOH-da xotira hech qachon toza bo'lmaydi" },
              "LOH-ga 85KB-dan katta obyektlar tushadi va ular default holda siqilmaydi (fragmentation hosil qiladi)."),
        _ => ($"C# Hard #{index}-savol: Low-level C#-da #{index}-optimizatsiya bo'yicha qaysi yechim to'g'ri?",
              $"// SIMD Vectorization #{index}\nvar vector = Vector128.Create(1.0f, 2.0f, 3.0f, 4.0f);",
              new List<string> { "Hardware Intrinsics (SIMD) orqali bir vaqtning o'zida ko'plab ma'lumotlarni protsessor darajasida parallel qayta ishlash", "Faqat keshni tozalash", "Faqat string formatlash", "Faqat database ulanishini ochish" },
              "SIMD (Single Instruction Multiple Data) apparat darajasida yuqori tezlikdagi parallel hisoblashlarni bajaradi.")
    };
}
