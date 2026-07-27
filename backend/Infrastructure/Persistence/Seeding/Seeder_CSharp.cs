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
                "Stack vs Heap, Value vs Reference types, Boxing/Unboxing, ref/out/in va IDisposable bo'yicha 30 ta oson darajadagi test.",
                "Easy",
                "code-2",
                GenerateCSharpEasyQuestions()
            ),
            CreateQuiz(
                "C# Advanced Memory, CLR Internals & Async Deep Dive",
                "csharp",
                "C# Dasturlash Tili",
                "GC Generations (Gen 0-2, LOH, POH), Span<T> vs Memory<T>, Async State Machine va Record Types bo'yicha 30 ta o'rtacha darajadagi test.",
                "Medium",
                "cpu",
                GenerateCSharpMediumQuestions()
            ),
            CreateQuiz(
                "C# High-Performance, Unmanaged Memory & Native CLR Architecture",
                "csharp",
                "C# Dasturlash Tili",
                "System.IO.Pipelines, Native AOT, SIMD Intrinsics, ThreadPool Starvation va Lock-Free Concurrency bo'yicha 30 ta qiyin darajadagi test.",
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
            CreateQuestion("C# dasturlash tilida Stack va Heap xotira tuzilmalari o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "Stack LIFO bo'lib qiymat turlarini (Value Types) saqlaydi va tez tozalanadi; Heap dinamik bo'lib havolali turlarni (Reference Types) saqlaydi va GC tomonidan tozalanadi",
                    "Stack faqat matnli ma'lumotlarni, Heap esa raqamlarni saqlaydi",
                    "Heap xotira Stack-ga qaraganda 100 marta tezroq ishlaydi",
                    "Stack xotira hech qachon toza bo'lmaydi va xotira sizishiga olib keladi"
                },
                "Stack LIFO (Last In First Out) prinsipi bo'yicha ishlaydi va funksiya lokal o'zgaruvchilarini saqlaydi. Heap dinamik xotira bo'lib, uning tozalanishiga Garbage Collector javobgar."),

            CreateQuestion("C#-da Boxing va Unboxing jarayonlari nima va uning unumdorlikka (performance) ta'siri qanday?",
                new List<string> {
                    "Boxing Value Type-ni Heap-dagi Object-ga o'g'iradi; Unboxing esa uni qaytaradi. Bu Heap-da ortiqcha allocation va GC yukini oshiradi",
                    "Boxing faqat string-larni shifrlash uchun ishlatiladi va perfomansga ta'sir qilmaydi",
                    "Unboxing xotirani zudlik bilan tozalaydi va tezlikni oshiradi",
                    "Boxing va Unboxing faqat multithreading operatsiyalarida ishlaydi"
                },
                "Boxing Value Type-ni Heap-ga joylab ob'ekt yaratadi, bu esa ortiqcha xotira ajratilishi va GC yig'ilishiga sabab bo'ladi."),

            CreateQuestion("C#-da struct va class o'rtasidagi asosiy farqlar nimalardan iborat va qachon struct ishlatish kerak?",
                new List<string> {
                    "Struct — Value Type (Stack-da saqlanadi, vorislikni qo'llamaydi), Class — Reference Type. Hajmi 16 baytdan kichik, immutable obyektlar uchun struct mos",
                    "Class faqat static metodlar uchun, Struct esa faqat interfeyslar uchun ishlatiladi",
                    "Struct Heap-da saqlanadi va vorislikni to'liq qo'llab-quvvatlaydi",
                    "Class va Struct o'rtasida hech qanday xotira farqi yo'q"
                },
                "Struct Value Type hisoblanib Stack-da joylashadi. U vorislikni (inheritance) qo'llamaydi va kichik hajmli ma'lumotlar uchun mos keladi."),

            CreateQuestion("C#-da readonly struct va ref struct turlari xotira optimallashda qanday rol o'ynaydi?",
                new List<string> {
                    "readonly struct defensive copying (himoya nusxalash) ni oldini oladi; ref struct esa obyektni faqat Stack-da joylashishini majburiy qiladi",
                    "readonly struct xotirani shifrlaydi, ref struct esa uni diskka saqlaydi",
                    "ref struct faqat async metodlar ichida ishlatish uchun yaratilgan",
                    "Ikkala struct turi ham ob'ektlarni Heap-ga majburiy ko'chiradi"
                },
                "readonly struct maydonlar o'zgarmasligini kafolatlab defensive copy-ni oldini oladi. ref struct esa ob'ektning Heap-ga o'tib ketmasligini va faqat Stack-da saqlanishini majbur qiladi."),

            CreateQuestion("Metod parametrlarida ref, out va in kalit so'zlarining farqlari nimada?",
                new List<string> {
                    "ref boshlang'ich qiymat talab qiladi; out metod ichida qiymat tayinlanishini shart qiladi; in esa havolani readonly rejimida uzatadi",
                    "out parametrlarga boshlang'ich qiymat berish majburiy",
                    "in parametri qiymatni metod ichida o'zgartirishga ruxsat beradi",
                    "Ushbu uchta kalit so'z ham bir xil vazifani bajaradi"
                },
                "ref boshlang'ich qiymatga ega havolani uzatadi, out metod ichida qiymat tayinlanishini kafolatlaydi, in esa qiymatni o'zgarmas readonly havola sifatida uzatadi."),

            CreateQuestion("IDisposable va Finalizer (~Destructor) o'rtasidagi farq nimada va Dispose Pattern qanday qo'llaniladi?",
                new List<string> {
                    "IDisposable unmanaged resurslarni dasturchi tomonidan aniq vaqtda bo'shatadi; Finalizer esa GC obyektni o'chirayotganda avtomatik chaqiriladi",
                    "Finalizer-ni dasturchi to'g'ridan-to'g'ri kodingizda chaqira oladi",
                    "IDisposable faqat Stack xotirasini tozalash uchun ishlatiladi",
                    "GC.SuppressFinalize(this) Finalizer-ni birinchi navbatda chaqirishni buyuradi"
                },
                "IDisposable deterministik (dasturchi xohlagan vaqtda) resurs tozalash uchun ishlatiladi. GC.SuppressFinalize(this) chaqirilganda GC ushbu ob'ekt Finalizer-ini ortiqcha chaqirib o'tirmaydi."),

            CreateQuestion("C#-da string turi bo'yicha qaysi ta'rif to'g'ri va nima uchun String Manipulation uchun StringBuilder tavsiya etiladi?",
                new List<string> {
                    "String — Immutable Reference Type bo'lib, har bir o'zgarishda yangi obyekt yaratadi; StringBuilder esa bitta bufer ichida o'zgartirish kiritadi",
                    "String Value Type bo'lib Stack-da saqlanadi",
                    "StringBuilder har bir operatsiyada GC tozalashini chaqiradi",
                    "String va StringBuilder o'rtasida unumdorlik farqi yo'q"
                },
                "String immutable bo'lgani uchun har bir tsikldagi += yangi ob'ekt yaratadi. StringBuilder esa bitta bufer (char array) ustida ishlab GC ga yuk tushirmaydi."),

            CreateQuestion("C#-da IEnumerable<T> va IQueryable<T> o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "IEnumerable filtrni xotiraga (In-Memory) yuklab olgandan keyin bajaradi; IQueryable filtrni Expression Tree sifatida ma'lumotlar bazasiga SQL qilib yuboradi",
                    "IQueryable faqat massivlar bilan ishlaydi",
                    "IEnumerable faqat SQL Server bilan ishlaydi",
                    "Ikkala interfeys ham so'rovni bir xil vaqtda ma'lumotlar bazasiga yuboradi"
                },
                "IQueryable Expression Tree hosil qilib so'rovni bazaga SQL sifatida yuboradi (Server-side evaluation). IEnumerable esa barcha ma'lumotlarni o'qib bo'lgach xotirada filtrlaydi."),

            CreateQuestion("C# 9+ da record va class o'rtasidagi asosiy farq va with expression qanday ishlaydi?",
                new List<string> {
                    "Record-lar Value-based equality qo'llaydi va with ifodasi mavjud obyektning nusxasini olib, belgilangan maydonlarini o'zgartirib yangi obyekt beradi",
                    "Record qiymatlarini keyinchalik o'zgartirib bo'lmaydi va with uni o'chirib yuboradi",
                    "Class with ifodasini to'liq qo'llab-quvvatlaydi",
                    "Record va Class xotirada mutlaqo bir xil tenglikni (Reference equality) tekshiradi"
                },
                "Record-larda tenglik obyektlar havolasiga emas, qiymatlariga qarab (Value-based) aniqlanadi va with iborasi unumli nondestructive mutation qiladi."),

            CreateQuestion("C#-da Nullable Reference Types (string?) mexanizmi qanday ishlaydi va u kompilyatsiya bosqichida nimani kafolatlaydi?",
                new List<string> {
                    "Kompilyatsiya vaqtida NullReferenceException xavfini kamaytiradi, ammo CLR runtime darajasida qo'shimcha tur yaratmaydi (static analysis)",
                    "Runtime-da null qiymat tushsa uni avtomatik bo'sh string ga o'zgartiradi",
                    "Nullable reference type-lar qiymat turiga aylanadi (struct)",
                    "Kompilyatordan keyin ilova tezligini 2 marta oshiradi"
                },
                "Nullable Reference Types C# kompilyatori darajasida statik tahlil o'tkazib NRE xatoliklarini oldini olishga yordam beradi, runtime-da yangi tip hosil qilmaydi."),

            CreateQuestion("C#-da const va readonly o'zgaruvchilari o'rtasidagi farq nimada?",
                new List<string> {
                    "const kompilyatsiya vaqtida (Compile-time) baholanadi va qiymati kodga static joylanadi; readonly esa runtime-da (constructor ichida) tayinlanishi mumkin",
                    "const faqat class ichida, readonly faqat metod ichida ishlatiladi",
                    "readonly o'zgaruvchini ilova davomida istalgan joyda o'zgartirish mumkin",
                    "const va readonly bir xil ishlaydi, hech qanday farq yo'q"
                },
                "const qiymatlari kompilyatsiya vaqtida o'rniga qo'yiladi. readonly esa obyekt yaratilayotganda constructor ichida dinamik ravishda tayinlanishi mumkin."),

            CreateQuestion("C# 8+ pattern matching sintaksisida switch expression va property pattern qanday qulaylik beradi?",
                new List<string> {
                    "Kodni ixchamlashtirib, obyekt xususiyatlari (property pattern) va shartlari bo'yicha natijani toza va o'qilishi oson ko'rinishda qaytarish imkonini beradi",
                    "U faqat int turlari bilan ishlaydi",
                    "U switch iborasini sekinlashtiradi",
                    "U faqat Windows OS-da ishlaydi"
                },
                "Switch expression va Pattern Matching koddagi ko'plab if-else zanjirlarini toza va deklarativ funksional usulda yozishga imkon beradi."),

            CreateQuestion("C#-da enum turi ostida qaysi ma'lumot turi yotadi va [Flags] atributi nima beradi?",
                new List<string> {
                    "Standart bo'yicha int yotadi; [Flags] atributi enum qiymatlarini bitwise (OR, AND) operatsiyalar bilan birgalikda saqlash va tekshirish imkonini beradi",
                    "Standart bo'yicha string yotadi",
                    "[Flags] atributi enum-ni struct-ga o'g'iradi",
                    "Enum qiymatlarini runtime-da o'zgartirishga imkon beradi"
                },
                "Enum standart bo'yicha int bo'ladi (byte/long ham bo'lishi mumkin). [Flags] bit-mask (1, 2, 4, 8) mantiqiy amallar bilan ko'p tanlovli bayroqlarni biriktirish imkonini beradi."),

            CreateQuestion("C#-da Exception handling jarayonida catch blokidagi when filtrining afzalligi nimada?",
                new List<string> {
                    "Exception stack trace va xotira holatini buzmasdan (unwinding qilmasdan) shart to'g'ri kelsagina ushbu catch blokiga kirishni ta'minlaydi",
                    "Xatolikni umuman loglamasdan o'chirib yuboradi",
                    "Faqat SQL Server xatolarida ishlaydi",
                    "catch blokini majburiy tezlashtiradi"
                },
                "catch (Exception ex) when (ex.ErrorCode == 404) filtri xatolik stack trace-ni buzmagan holda faqat shart mos kelsa catch ichiga kiradi."),

            CreateQuestion("C#-da Extension Methods (kengaytma metodlar) qanday yaratiladi va ularning cheklovi nimada?",
                new List<string> {
                    "Static sinf ichida birinchi parametr oldiga this kalit so'zi qo'yiladi; Ular kengaytirilayotgan sinfning private maydonlariga kirish huquqiga ega emas",
                    "Extension metodlar private maydonlarni ham o'zgartira oladi",
                    "Ular faqat struct turlari uchun ishlatiladi",
                    "Extension metodlar ilovani 2 marta sekinlashtiradi"
                },
                "Extension metodlar static class va static method shaklida yozilib, this parametri orqali mavjud turlarga yangi metod qo'shadi, lekin private a'zolarga kira olmaydi."),

            CreateQuestion("Generics cheklovlarida (Generic Constraints) where T : class va where T : struct nimani bildiradi?",
                new List<string> {
                    "where T : class tip faqat Reference Type bo'lishini; where T : struct esa faqat Value Type bo'lishini talab qiladi",
                    "where T : class faqat static sinflarni qabul qiladi",
                    "where T : struct faqat string-larni qabul qiladi",
                    "Ular har doim bir xil turdagi obyektlarni qabul qiladi"
                },
                "Generic constraints (where T : class/struct/new()) kompilyatsiya vaqtida umumiy tip parametlariga qat'iy cheklovlar qo'yish uchun ishlatiladi."),

            CreateQuestion("Generic interfeyslarda Covariance (out T) va Contravariance (in T) tushunchalari nimani anglatadi?",
                new List<string> {
                    "out T (Covariance) faqat qaytariluvchi tur sifatida hosilaviy tipni ishlatishga imkon beradi; in T (Contravariance) esa kirish parametri sifatida ishlaydi",
                    "out T faqat int turlari uchun",
                    "in T faqat async metodlarda ishlaydi",
                    "Ular o'zgaruvchilarni Heap-ga o me'tkazadi"
                },
                "Covariance (out T) hosilaviy sinf obyektlarini asosiy sinf interfeysiga tayinlash imkonini beradi (IEnumerable<out T>). Contravariance (in T) esa teskarisi."),

            CreateQuestion("C#-da Anonymous Types va ValueTuple (int, string) o'rtasidagi asosiy farqlar nimada?",
                new List<string> {
                    "Anonymous Types — immutable Reference Type (class); ValueTuple esa mutable Value Type (struct) bo'lib metodlardan bir nechta qiymat qaytarishga qulay",
                    "Anonymous Types faqat SQL-da ishlaydi",
                    "ValueTuple xotirani juda ko'p egallaydi",
                    "Ular o'rtasida hech qanday farq yo'q"
                },
                "ValueTuple Stack-da saqlanadigan va nomlangan elementlarga ega yengil struct hisoblanadi. Anonymous Types esa kompilyator tomonidan yaratiladigan class hisoblanadi."),

            CreateQuestion("Delegates va Events o'rtasidagi arxitekturaviy farq va kapsulatsiya (encapsulation) qanday ishlaydi?",
                new List<string> {
                    "Event — bu Delegate ustiga qurilgan o'rovchi (wrapper) bo me'lib, tashqi sinflarga faqat += (subscribe) va -= (unsubscribe) amallarini bajarishga ruxsat beradi",
                    "Delegate faqat private metodlar uchun ishlaydi",
                    "Event-ni tashqi sinflar to'g'ridan-to'g'ri invoke() qila oladi",
                    "Delegate va Event o'rtasida farq yo'q" 
                },
                "Event kapsulatsiyani ta'minlaydi: tashqi sinflar hodisani to'g me'ridan-to'g me'ri chaqira (invoke) yoki tozalab tashlay (null) olmaydi, faqat obuna bo'lishi mumkin."),

            CreateQuestion("C#-da Indexer (this[int index]) va Range/Index operatorlari (^1, 1..5) qanday qo'llaniladi?",
                new List<string> {
                    "Indexer sinf obyektiga massiv kabi indeks orqali murojaat qilish imkonini beradi; ^1 oxiridan birinchi elementni, 1..5 esa ko'rsatilgan diapazondagi kesmani beradi",
                    "^1 har doim birinchi elementni beradi",
                    "Range operatori ma'lumotlar bazasini o me'chiradi",
                    "Indexer faqat string turlarida ishlaydi"
                },
                "Indexer sinf obyektlarini indekslash imkonini beradi. System.Index (^1) va System.Range (1..5) esa massiv va Span-lardan nusxa olmasdan qulay kesib olishni beradi."),

            CreateQuestion("String Interpolation ($'{var}') va FormattableString o'rtasidagi farq nimada?",
                new List<string> {
                    "FormattableString format shablonini va parametrlarni alohida saqlaydi, bu esa SQL Injection oldini olish va i18n (lokalizatsiya) uchun ishlatiladi",
                    "FormattableString har doim sekinroq ishlaydi",
                    "String Interpolation xotirada 10 marta ko'p joy oladi",
                    "Ular o'rtasida hech qanday farq yo'q"
                },
                "FormattableString format matni va parametrlarni alohida tutadi. U masalan EF Core-dagi FromSqlInterpolated so'rovlarida parametrli SQL tuzish uchun ishlatiladi."),

            CreateQuestion("C# 8+ dagi Default Interface Methods (Interfeysdagi default metodlar) xususiyati nima uchun qo'shilgan?",
                new List<string> {
                    "Mavjud interfeysga yangi metod qo me'shilganda uni implement qilgan o'nlab sinflarni buzmasdan (backward compatibility) standart tana berish uchun",
                    "Interfeyslarni struct-ga o me'girish uchun",
                    "Klassik vorislikni taqiqlash uchun",
                    "Faqat static metodlarni saqlash uchun"
                },
                "Default Interface Methods mavjud kutubxonalarni buzmagan holda interfeyslarni rivojlantirish (API evolution) imkonini beradi."),

            CreateQuestion("C#-da is operatori yordamida pattern matching bilan tur tekshirish va kasting o'rtasidagi farq nima?",
                new List<string> {
                    "if (obj is Person p) shaklida tur tekshiriladi va to'g'ri kelsa kasting qilinib p o'zgaruvchisiga biriktiriladi, InvalidCastException bermaydi",
                    "is operatori har doim exception otadi",
                    "U faqat int turlari uchun ishlaydi",
                    "as operatoridan tezligi 10 baravar sekin"
                },
                "Pattern matching `is` operatori xavfsiz tur tekshiruvi va bir vaqtning o'zida o'zgaruvchiga bog me'lash (type pattern) imkonini beradi."),

            CreateQuestion("C#-da lock iborasi va Monitor (Monitor.Enter / Exit) o'rtasidagi bog'liqlik qanday?",
                new List<string> {
                    "lock iborasi kompilyator tomonidan try-finally bloki va Monitor.Enter / Monitor.Exit metodlariga o me'giriladigan sintaktik qulaylikdir",
                    "lock xotirani tozalash uchun ishlatiladi",
                    "Monitor faqat asinxron metodlarda ishlaydi",
                    "lock va Monitor ikkita alohida protsessda ishlaydi"
                },
                "lock (obj) { ... } kodi kompilyatordan keyin `bool lockTaken = false; try { Monitor.Enter(obj, ref lockTaken); } finally { if (lockTaken) Monitor.Exit(obj); }` ga aylanadi."),

            CreateQuestion("C# auto-properties-da init-only setters (init accessor) nimani kafolatlaydi?",
                new List<string> {
                    "Obyekt yaratilayotganda (Object Initializer) qiymat tayinlashga ruxsat beradi, lekin undan keyin xususiyatni o'zgarmas (immutable) qiladi",
                    "Faqat private metodlardan chaqirishga ruxsat beradi",
                    "Xususiyatni avtomatik null ga o me'zgartiradi",
                    "U faqat struct turlarida ishlaydi"
                },
                "init setter obyekt yaratish obyekt initsializatsiyasi vaqtida qiymat berishga va undan keyin o'zgarmas readonly bo me'lib qolishiga xizmat qiladi."),

            CreateQuestion("C#-da partial classes va partial methods qanday vaziyatda juda qo'l keladi?",
                new List<string> {
                    "Kodni bir nechta fayllarga bo'lish hamda avtomatik generatsiya qilingan kod (Source Generators, EF Designer) bilan foydalanuvchi kodini ajratish uchun",
                    "Faqat keshni tozalash uchun",
                    "Faqat Windows OS-da ishlatish uchun",
                    "Multithreading-ni o me'chirish uchun"
                },
                "partial sinflar bitta sinf kodini turli fayllarda saqlash va auto-generated kodlarni qo me'lda yozilgan kodga aralashtirmaslik uchun ishlatiladi."),

            CreateQuestion("Metod parametrlarida params kalit so'zi va C# 13 dagi ReadOnlySpan<T> params qanday qulaylik beradi?",
                new List<string> {
                    "params o me'zgaruvchan sondagi argumentlarni uzatish imkonini beradi; ReadOnlySpan params esa Heap-da massiv allocation yaratmasdan ishlashga imkon beradi",
                    "params faqat string massivlar uchun ishlaydi",
                    "ReadOnlySpan params ilovani sekinlashtiradi",
                    "Ular o me'rtasida hech qanday xotira farqi yo'q"
                },
                "Klassik `params T[]` har safar yangi massiv yaratadi. C# 13 dagi `params ReadOnlySpan<T>` esa Stack-based allocation-free argument uzatish imkonini beradi."),

            CreateQuestion("Attributes ([AttributeUsage]) va Reflection metadata o me'rtasidagi bog'liqlik qanday?",
                new List<string> {
                    "Atributlar assembly metadata-siga qo'shimcha ma'lumot saqlaydi; Reflection esa runtime-da ushbu atributlarni o'qib mantiq bajaradi",
                    "Atributlar kodingizni avtomatik o me'chirib beradi",
                    "Reflection atributlarni o o me'qiy olmaydi",
                    "Atributlar faqat CSS stillari uchun ishlatiladi"
                },
                "Atributlar sinf, metod va xususiyatlarga deklarativ metadata biriktiradi. Reflection esa `GetCustomAttributes()` orqali ularni runtime-da o'qiydi."),

            CreateQuestion("Type casting jarayonida (T) obj to'g'ridan-to'g'ri kastingi va obj as T operatori o'rtasidagi farq nimada?",
                new List<string> {
                    "To'g'ridan-to'g'ri kasting mos kelmasa InvalidCastException otadi; as operatori esa mos kelmasa null qaytaradi (faqat Reference Types uchun)",
                    "as operatori har doim exception otadi",
                    "Direct cast faqat string turlari uchun ishlaydi",
                    "Ular o'rtasida hech qanday farq yo'q"
                },
                "Direct casting `(Person)obj` turi mos bo me'lmasa InvalidCastException beradi. `obj as Person` esa xavfsiz bo'lib, mos kelmasa null beradi."),

            CreateQuestion("C#-da Operator Overloading (operatorlarni qayta yuklash) sintaksisi qanday va u qanday metodlar bo'lishi shart?",
                new List<string> {
                    "public static Vector operator +(Vector a, Vector b) shaklida bo me'lib, u har doim public static metod bo'lishi shart",
                    "Operator overloading faqat private metodlarda ishlaydi",
                    "U faqat interface ichida yozilishi shart",
                    "U faqat string turlari uchun ishlaydi"
                },
                "C#-da `+`, `-`, `==` kabi operatorlarni qayta belgilash uchun metod albatta `public static` bo'lishi va mos tiplarni qabul qilishi kerak.")
        };
    }

    private static List<Question> GenerateCSharpMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("Garbage Collector (GC) avlodlari (Gen 0, Gen 1, Gen 2, LOH, POH) qanday vazifa bajaradi?",
                new List<string> {
                    "Gen 0 — yangi va qisqa umrli obyektlar; Gen 1 — o'tish buferi; Gen 2 — uzoq yashovchi obyektlar; LOH — 85KB dan katta obyektlar; POH — manzili o'zgarmas (pinned) obyektlar",
                    "Gen 0 faqat static o'zgaruvchilar uchun mo'ljallangan",
                    "LOH obyektlari har bir millisoniyada majburiy tozalanadi",
                    "POH obyektlari Gen 0 va Gen 1 tozalanganda avtomatik Stack-ga ko'chiriladi"
                },
                "GC obyektlarni yashash davri bo'yicha bo'ladi. Gen 0 eng ko'p va tez tozalanadi. LOH (Large Object Heap) 85,000 baytdan katta obyektlarni saqlaydi."),

            CreateQuestion("C#-da Span<T> va Memory<T> o'rtasidagi asosiy cheklov va ishlatilish farqlari nimada?",
                new List<string> {
                    "Span ref struct bo'lgani uchun Stack-only va u async metodlarda, class maydonlarida ishlatilmaydi; Memory esa struct bo'lib Heap-da saqlana oladi va async metodlarga mos keladi",
                    "Span faqat string-lar bilan ishlaydi, Memory esa faqat int massivlar bilan",
                    "Memory async metodlarda ishlatilsa xotira sizishiga olib keladi",
                    "Span va Memory o'rtasida hech qanday sintaktik yoki xotira cheklovi yo'q"
                },
                "Span ref struct bo'lgani sababli Stack-dan Heap-ga ko'chib ketishi taqiqlangan (async/await, class fields). Memory esa bu cheklovga ega emas."),

            CreateQuestion("Async/Await asinxron modelida ConfigureAwait(false) ishlatishning asosiy maqsadi va muhiti nima?",
                new List<string> {
                    "Davom etuvchi kodni (continuation) asl SynchronizationContext-ga qaytishini shart qilmasdan ThreadPool thread-ida bajaradi va deadlock-ni oldini oladi",
                    "So'rov bajarilishini 2 marta tezlashtiradi",
                    "Asinxron metodni sinxron metodga o'zgardi",
                    "Faqat UI hodisalarini ushlash uchun ishlatiladi"
                },
                "ConfigureAwait(false) davomini asl SynchronizationContext-ga majburan qaytarmaydi, bu kutubxonalar va backend servislarida unumdorlikni oshiradi va deadlock-ni oldini oladi."),

            CreateQuestion("Async/Await metodlarida ValueTask<T> va Task<T> o'rtasidagi farq va qachon ValueTask ishlatish kerak?",
                new List<string> {
                    "Agarda metod natijasi ko'pincha sinxron (masalan keshdan) qaytsa, ValueTask Heap-da Task ob'ekti yaratilishini (allocation) oldini oladi",
                    "ValueTask har doim Task-ga qaraganda 10 marta sekinroq ishlaydi",
                    "ValueTask-ni bir necha marta await qilish tavsiya etiladi",
                    "Task faqat void metodlar uchun ishlatiladi"
                },
                "Agarda natija allaqachon tayyor (keshda) bo'lsa, ValueTask Stack-da qaytib Task ob'ekti yaratilishini (allocation) bartaraf etadi."),

            CreateQuestion("C#-da Interlocked operatsiyalarining (masalan Interlocked.Increment) oddiy lock (Monitor) dan afzalligi nimada?",
                new List<string> {
                    "Hardware CPU atomic ko'rsatmalaridan foydalanadi va thread-larni bloklamasdan (Lock-free) va context switch-siz o'ta yuqori tezlik beradi",
                    "Faqat fayllarni o'qish uchun ishlatiladi",
                    "Garbage Collection-ni to'xtatib qo'yadi",
                    "Lock-ga qaraganda sekinroq ishlaydi"
                },
                "Interlocked operatsiyalari atomar CPU yo'riqnomalari bilan ishlaydi, thread-ni bloklamaydi (kernel-level context switch bo'lmaydi)."),

            CreateQuestion("System.Threading.Channels (Channel<T>) kutubxonasining BlockingCollection<T> ga nisbatan asosiy afzalligi nimada?",
                new List<string> {
                    "Asinxron (async/await) Producer-Consumer modelini to'liq qo'llaydi va thread-larni bloklamasdan high-throughput ma'lumot oqimini beradi",
                    "Faqat SQL Server bilan ishlaydi",
                    "Faqat bitta thread bilan ishlay oladi",
                    "Xabarlarni har doim diskka yozadi"
                },
                "Channel<T> asinxron oqim va backpressure-ni qo'llab-quvvatlaydi, thread-larni sinxron bloklamaydi."),

            CreateQuestion("C# Source Generators texnologiyasi qanday ishlaydi va uning an'anaviy Reflection-dan afzalligi nimada?",
                new List<string> {
                    "Kompilyatsiya vaqtida kodingizni tahlil qilib yangi C# kodini generatsiya qiladi; Reflection kabi runtime xarajat va JIT overhead tug'dirmaydi",
                    "Runtime-da kodingizni o'chirib beradi",
                    "Faqat In-Memory bazalarda ishlaydi",
                    "Faqat Windows OS-da ishlaydi"
                },
                "Source Generators kompilyatsiyada ishlaydi va AOT/Zero-reflection kabi yuqori unumdorlikni ta'minlaydi."),

            CreateQuestion("C#-da Yield Return (Iterator State Machine) qanday ishlaydi va uning Lazy Evaluation konseptiga bog'liqligi nimada?",
                new List<string> {
                    "C# kompilyatori state machine sinfini yaratadi; Elementlar faqat foreach yoki MoveNext() chaqirilganda ketma-ket hisoblanadi (Lazy)",
                    "Barcha elementlarni darhol xotiraga massiv qilib yuklaydi",
                    "Faqat bir marta ishlatilishi mumkin",
                    "Metod bajarilayotganda barcha thread-larni to'xtatadi"
                },
                "yield return elementlarni bir yo'la emas, talab qilinganda (on-demand) birma-bir hisoblab beradi."),

            CreateQuestion("C#-da EventHandler ishlatilganda xotira sizishi (Memory Leak) qanday kelib chiqadi va uni oldini olish usuli qaysi?",
                new List<string> {
                    "Publisher uzoq yashasa, Subscriber ob'ektiga kuchli havola (strong reference) saqlab qoladi va GC uni o'chira olmaydi; Unsubscribe qilish yoki WeakReference ishlatish shart",
                    "Subscriber avtomatik tozalanadi",
                    "Event-lar Heap-da emas, Stack-da saqlanadi",
                    "Event-larni ishlatish GC-ni o'chirib qo'yadi"
                },
                "Uzun umrli Publisher ob'ekti Subscriber ob'ektini havola bilan ushlab turadi va GC o'chirishiga tosqinlik qiladi."),

            CreateQuestion("C# Expression Trees (Expression<Func<T, bool>>) va oddiy Delegate (Func<T, bool>) o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "Expression Tree koddagi mantiqni ob'ektlar daraxti (data structure) sifatida saqlaydi; Delegate esa ijro etiluvchi IL kodi",
                    "Delegate-ni SQL so'roviga o'girish mumkin",
                    "Expression Tree faqat string-larni solishtiradi",
                    "Ikkalasi ham mutlaqo bir xil bajariladi"
                },
                "Expression Tree koddagi mantiqni tahlil qilish uchun ob'ektlar ierarxiyasi bo'lib saqlaydi, EF Core uni SQL-ga aylantiradi."),

            CreateQuestion("IAsyncEnumerable<T> va await foreach sintaksisi asinxron ma'lumotlar oqimida nima beradi?",
                new List<string> {
                    "Ma'lumotlarni to'liq xotiraga yuklamasdan, bo'laklab (stream) asinxron ravishda birma-bir o'qish imkonini beradi",
                    "Faqat fayllarni o me me'chirish uchun ishlatiladi",
                    "Metodlarni sinxron holatga o'tkazadi",
                    "Faqat UI hodisalari uchun kerak"
                },
                "IAsyncEnumerable<T> ma'lumotlar to'plamini bir vaqtning o'zida RAM ga yuklamasdan stream shaklida asinxron uzatadi."),

            CreateQuestion("Task.WhenAll va Task.WhenAny metodlari o'rtasidagi farq va Exception handling qanday ishlaydi?",
                new List<string> {
                    "Task.WhenAll barcha task-lar tugashini kutadi va AggregateException qaytaradi; Task.WhenAny esa birinchi tugagan task-ni qaytaradi",
                    "Task.WhenAll birinchi xatoda qolgan task-larni o me'chiradi",
                    "Task.WhenAny har doim sekinroq ishlaydi",
                    "Ular o me'rtasida farq yo'q"
                },
                "Task.WhenAll barcha parallel task-larni kutadi va yuzaga kelgan barcha xatolarni AggregateException ichiga yig'adi."),

            CreateQuestion("AsyncLocal<T> va ThreadLocal<T> o'rtasidagi farq va u asinxron oqimda (Async Context) qanday uzatiladi?",
                new List<string> {
                    "AsyncLocal qiymatni asinxron await zanjirlari bo'ylab (Execution Context) uzatadi; ThreadLocal esa faqat muayyan jismoniy Thread uchun amal qiladi",
                    "ThreadLocal asinxron oqimda qiymatni saqlaydi",
                    "AsyncLocal faqat SQL Server bilan ishlaydi",
                    "Ular o me'rtasida farq yo'q"
                },
                "AsyncLocal<T> await bo'lganda thread o me'zgarsa ham ExecutionContext orqali qiymatni keyingi thread-ga o'tkazadi. ThreadLocal esa thread almashtirilganda qiymatni yo'qotadi."),

            CreateQuestion("CancellationTokenSource va CancellationToken yordamida uzoq davom etuvchi operatsiyalarni bekor qilish qanday qo'llaniladi?",
                new List<string> {
                    "CancellationTokenSource signal yuboradi, CancellationToken esa OperationCanceledException otadi yoki IsCancellationRequested ni tekshiradi",
                    "CancellationToken serverni majburiy resursdan uzadi",
                    "U faqat fayllarni o'chirishda ishlaydi",
                    "CancellationToken har doim thread-ni o'chirib beradi"
                },
                "Cooperative Cancellation standarti bo'yicha token.ThrowIfCancellationRequested() chaqirilib asinxron operatsiya bekor qilinadi."),

            CreateQuestion("IMemoryCache-da PostEvictionCallback va ExpirationEvictionPolicies (Absolute vs Sliding) qanday ishlaydi?",
                new List<string> {
                    "AbsoluteExpiration belgilangan muddatda o'chiradi; SlidingExpiration esa oxirgi murojaatdan keyin vaqt hisoblaydi; PostEvictionCallback esa kesh o'chirilganda ishlaydi",
                    "SlidingExpiration keshni hech qachon o'chirmaydi",
                    "PostEvictionCallback faqat exception yuz berganda ishlaydi",
                    "Ular faqat Redis bo'lganda ishlaydi"
                },
                "SlidingExpiration keshga har murojaat bo'lganda umrini uzaytiradi. PostEvictionCallback esa kesh o'chirilganda sababini (EvictionReason) bildirib xabar beradi."),

            CreateQuestion("System.Text.Json kutubxonasida Custom JsonConverter<T> yozish qaysi vaziyatda zarur bo'ladi?",
                new List<string> {
                    "Nodatiy formatdagi JSON ma'lumotlarini (masalan, string sana formatlarini DateTime-ga) maxsus deserializatsiya qilish kerak bo'lganda",
                    "Faqat fayllarni siqish uchun",
                    "Faqat SQL so'rovlarini tahlil qilish uchun",
                    "JsonConverter faqat XML uchun ishlaydi"
                },
                "Custom JsonConverter Read va Write metodlarini override qilib nodatiy JSON tuzilmalarini C# obyektlariga o'girish imkonini beradi."),

            CreateQuestion("ReaderWriterLockSlim sinfining oddiy lock (Monitor) ga nisbatan ko'p o'qiladigan (Read-Heavy) tizimlardagi afzalligi nimada?",
                new List<string> {
                    "U bir nechta reader thread-larga bir vaqtning o'zida o'qishga ruxsat beradi, yozuvchi (Writer) esa eksklyuziv lock oladi",
                    "U har doim thread-larni bloklaydi",
                    "U faqat diskka yozish uchun kerak",
                    "U lock-dan sekinroq ishlaydi"
                },
                "ReaderWriterLockSlim bir vaqtda yuzlab o'quvchilarga (Multiple Readers) parallel o'qish imkonini berib, faqat yozuvchi kelganda eksklyuziv bloklaydi."),

            CreateQuestion("ArrayPool<T>.Shared rent va return mexanizmining unumdorlikka ta'siri qanday?",
                new List<string> {
                    "Katta bayt massivlarini qayta-qayta yaratmasdan pool-dan ijaraga oladi va qaytaradi, bu GC allocation va LOH fragmentation-ni keskin kamaytiradi",
                    "ArrayPool massivlarni diskka saqlaydi",
                    "ArrayPool faqat string-lar uchun ishlaydi",
                    "U ilovani 10 marta sekinlashtiradi"
                },
                "ArrayPool.Shared massiv buferlarini qayta ishlatib GC LOH allocation va fragmentation-ni bartaraf qiladi."),

            CreateQuestion("Func<T> va Action<T> delegatlarida closure capture (tashqi o'zgaruvchilarni ushlash) xotiraga qanday ta'sir qiladi?",
                new List<string> {
                    "Tashqi o'zgaruvchini ushlaganda (closure) kompilyator yashirin sinf (compiler-generated class) va Heap-da allocation yaratadi",
                    "Closure xotirani avtomatik tozalaydi",
                    "Closure faqat static metodlarda bo me'ladi",
                    "U xotiraga umuman ta'sir qilmaydi"
                },
                "Lambda ifodasi tashqi lokal o'zgaruvchidan foydalansa, kompilyator display class yaratadi va bu Heap allocation-ga olib keladi."),

            CreateQuestion("C# 8+ dagi Default Interface Implementation va ko'p interfeysli vorislikda metod toqnashuvi qanday hal qilinadi?",
                new List<string> {
                    "Obyekt to'g me'ridan-to'g me'ri tegishli interfeys turiga kasting (Explicit Interface Implementation) qilinib chaqiriladi",
                    "Kompilyatsiya xatosi beradi va ilova ishlamaydi",
                    "Metod tasodifiy birini tanlaydi",
                    "Interfeyslar o'z-o'zidan o me'chib ketadi"
                },
                "Default interface metodlari sinf obyektida to'g'ridan-to'g'ri ko'rinmaydi, u faqat interfeys tipiga kasting qilinganda `((IFoo)obj).Bar()` chaqiriladi."),

            CreateQuestion("C# 9+ dagi Native Integers (nint va nuint) turlari qaysi maqsadda ishlatiladi?",
                new List<string> {
                    "Operatsion tizim arxitekturasiga (32-bit yoki 64-bit) qarab mos ravishda pointer-sized int sifatida pointer arifmetikasi va unumdorlik uchun ishlatiladi",
                    "Faqat shakllarni chizish uchun",
                    "Faqat SQL Server parametrlari uchun",
                    "Ular faqat string-larni saqlaydi"
                },
                "nint va nuint platforma ko me'rsatgichi hajmidagi (32-bitda 4 bayt, 64-bitda 8 bayt) butun sonlarni ifodalaydi."),

            CreateQuestion("C#-da Volatile.Read va Volatile.Write metodlarining reordering ga ta me'siri nimada?",
                new List<string> {
                    "CPU va kompilyatordan ko'rsatmalarni o'zgaruvchi atrofida qayta tartiblamaslikni (no reordering) va xotiradan eng yangi qiymatni o me me'qishni kafolatlaydi",
                    "Xotirani shifrlash uchun ishlatiladi",
                    "Garbage Collector-ni chaqiradi",
                    "Faqat fayl o me'qishda ishlaydi"
                },
                "Volatile operatsiyalari Memory Barrier o'rnatadi va CPU keshlaridan yangi qiymat o'qilishini ta'minlaydi."),

            CreateQuestion("C# 12 dagi UnsafeAccessorAttribute nima uchun taqdim etilgan?",
                new List<string> {
                    "Reflection ishlatmasdan, o me'ta yuqori tezlikda sinfning private maydon va metodlariga to'g'ridan-to me'ri kirish imkonini beradi",
                    "Faqat SQL so me'rovlarini tezlashtiradi",
                    "Faqat public metodlarni o me'chiradi",
                    "U faqat AOT-da taqiqlangan"
                },
                "UnsafeAccessor (C# 12) Zero-reflection overhead bilan private a'zolarga kirish uchun kompilyator darajasidagi kasting beradi."),

            CreateQuestion("System.Collections.Immutable kolleksiyalari va ReadOnlyCollection o me'rtasidagi fundamental farq nima?",
                new List<string> {
                    "ReadOnlyCollection asl kolleksiyaga o'rovchi (wrapper) bo'lib asl kolleksiya o'zgarsa u ham o'zgaradi; ImmutableCollection esa mutlaqo o me'zgarmas yangi obyekt beradi",
                    "ReadOnlyCollection-ga yangi element qo me'shib bo me'ladi",
                    "ImmutableCollection har doim sekinroq ishlaydi",
                    "Ular bir xil ishlaydi"
                },
                "ReadOnlyCollection asl kolleksiya o me me'zgarsa o'zgaradi (Read-Only View). Immutable collection esa o me me'zgarmas xotira nusxasi hisoblanadi."),

            CreateQuestion("C#-da dynamic kalit so'zi va Dynamic Language Runtime (DLR) qanday ishlaydi?",
                new List<string> {
                    "Kompilyatsiya vaqtidagi tur tekshiruvini o'chirib, a'zolarni qidirish va chaqirishni runtime-dagi DLR CallSite keshiga yuklaydi",
                    "dynamic turini kompilyator int-ga o me'giradi",
                    "dynamic har doim AOT bilan birga ishlaydi",
                    "U faqat JSON formatini pars qiladi"
                },
                "dynamic tipi kompilyator static typing-ni o'chirib, barcha chaqiruvlarni DLR runtime binding-ga o'tkazishini bildiradi."),

            CreateQuestion("Pattern Matching-dagi Positional va Relational Pattern-lar qanday qulaylik beradi?",
                new List<string> {
                    "Deconstruct metodi orqali obyekt qiymatlarini ajratib, sonli solishtirishlar (>= 18 and <= 65) bilan toza mantiqiy shartlar yozish imkonini beradi",
                    "Faqat matnlarni solishtirish uchun",
                    "SQL so'rovlarini generatsiya qiladi",
                    "Faqat exception handling-da ishlaydi"
                },
                "Relational va Positional pattern-lar murakkab biznes shartlarini qisqa va o me'qilishi oson deklarativ kodga aylantiradi."),

            CreateQuestion("TaskCompletionSource<T> hodisalarni (Events) asinxron Task-ga aylantirishda qanday qo'llaniladi?",
                new List<string> {
                    "Hodisa (event) yuz berganda tcs.SetResult(data) chaqiriladi va ushbu hodisani kutayotgan awaiter-ga natija qaytariladi",
                    "tcs faqat thread-ni to'xtatadi",
                    "tcs faqat exception otadi",
                    "tcs ma me'lumotlar bazasini yangilaydi"
                },
                "TaskCompletionSource<T> callback yoki event bazasidagi koddagi hodisani asinxron Task-ga o me'girib await qilish imkonini beradi."),

            CreateQuestion("Custom EqualityComparer<T> yaratishda GetHashCode() va Equals() kontraktining buzilishi qanday muammoga olib keladi?",
                new List<string> {
                    "Ikkita obyekt Equals bo'lsa ularning GetHashCode() ham teng bo'lishi shart; aks holda Dictionary yoki HashSet-da obyekt topilmay yo'qoladi",
                    "Dictionary har doim crash beradi",
                    "Faqat memory leak yuzaga keladi",
                    "Hech qanday muammo bo me'lmaydi"
                },
                "Dictionary va HashSet avval GetHashCode bo me'yicha bucket topadi, keyin Equals tekshiradi. Kontrakt buzilsa obyekt kolleksiyadan topilmaydi."),

            CreateQuestion("ExceptionDispatchInfo.Capture(ex).Throw() metodining oddiy throw ex ga nisbatan afzalligi nimada?",
                new List<string> {
                    "Original exception-ning barcha Call Stack Trace ma'lumotlarini saqlab qolgan holda xatolikni qayta otish (rethrow) imkonini beradi",
                    "xatolikni o'chirib yuboradi",
                    "xatolikni log faylga yozadi",
                    "throw ex har doim yaxshiroq"
                },
                "throw ex original Call Stack-ni qayta yozadi. ExceptionDispatchInfo.Capture esa aniq original call stack-ni buzmay saqlaydi.")
        };
    }

    private static List<Question> GenerateCSharpHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("System.IO.Pipelines (PipeReader/PipeWriter) yordamida Zero-Allocation High-Throughput I/O qanday amalga oshiriladi?",
                new List<string> {
                    "Sinflar va bayt massivlarini qayta-qayta yaratmasdan, MemoryPool buferlaridan unumli foydalanib, to'g'ridan-to'g'ri xotira bo'laklari bilan ishlaydi",
                    "Faqat fayllarni diskka shifrlab yozish uchun ishlatiladi",
                    "Har bir bayt o'qilganda GC.Collect chaqiriladi",
                    "Kestrel server bilan ishlamaydi"
                },
                "System.IO.Pipelines xotirada buferlarni qayta ishlatadi (MemoryPool) va yuqori tezlikdagi tarmoq operatsiyalarida 0-allocation beradi."),

            CreateQuestion("Native AOT (Ahead-Of-Time) kompilyatsiyasi .NET 8/9 da qanday ishlaydi va uning asosiy cheklovi nimada?",
                new List<string> {
                    "IL kodni to'g'ridan-to'g'ri mashina kodi (machine code) ga o'giradi (JIT-siz), ishga tushish tezligi va xotira hajmi minimal bo'ladi; Lekin dynamic Reflection va Emit cheklanadi",
                    "JIT kompilyatsiyani 10 marta sekinlashtiradi",
                    "Faqat Windows-da ishlaydi va Linux-ni qo'llamaydi",
                    "Ma'lumotlar bazasidan foydalanishni taqiqlaydi"
                },
                "Native AOT IL o'rniga to'g'ridan-to'g'ri mashina kodini beradi, tez ishga tushadi, lekin dinamik Reflection va Code Generation cheklanadi."),

            CreateQuestion("CLR JIT Compilerni Tiered Compilation (Darajali kompilyatsiya) mexanizmi qanday ishlaydi?",
                new List<string> {
                    "Dastlab metodlar tez va optimizatsiyasiz (Tier 0) kompilyatsiya qilinadi; Metod ko'p chaqirilsa, u orqa fonda (Tier 1 / Dynamic PGO) yuqori optimizatsiya bilan qayta JIT qilinadi",
                    "Metodlarni har soniyada o me'chirib qayta tuzadi",
                    "Faqat AOT ilovalarda ishlaydi",
                    "Dastur xotirasini 10 marta oshiradi"
                },
                "Tiered Compilation tezkor start uchun Tier 0 (Quick JIT) va ko me'p chaqiriladigan kodlar uchun Tier 1 (Optimized JIT) ishlatadi."),

            CreateQuestion("C#-da Hardware Intrinsics va SIMD (Single Instruction Multiple Data) yo'nalishi orqali kodni apparat darajasida parallellashtirish qanday ishlaydi?",
                new List<string> {
                    "Protsessorning AVX/SSE ko me me'rsatmalaridan foydalanib bitta instruksiya bilan bir nechta ma me me'lumotlarni parallel hisoblaydi",
                    "Faqat GPU xotirasi bilan ishlaydi",
                    "Faqat string-larni birlashtiradi",
                    "Multithreading-ni o me'chirish uchun ishlaydi"
                },
                "SIMD protsessor apparat buyruqlari yordamida (SSE/AVX) bir paytning o'zida ko me me'plab sonli amallarni parallel bajaradi."),

            CreateQuestion("C#-da Unsafe va fixed kalit so'zlari yordamida ko'rsatgichlar (pointers) bilan ishlashda GC xotira harakati qanday nazorat qilinadi?",
                new List<string> {
                    "fixed kalit so me'zi obyektni GC ko'chirmasligi uchun Pinned (qo'zg'almas) qiladi va uning xotira manzilini ko'rsatgich (pointer) orqali xavfsiz o'qishga imkon beradi",
                    "fixed ob'ektni Heap-dan oshirib Stack-ga ko'chiradi",
                    "fixed xotiradan ob'ektni zudlik bilan o'chirib tashlaydi",
                    "Unsafe faqat SQL so'rovlarida ishlatiladi"
                },
                "fixed iborasi obyektni GC tozalash jarayonida o'rnidan jildirmaslik uchun 'pin' qilib ko me'rsatgich bilan ishlash imkonini beradi."),

            CreateQuestion("Multithreading dasturlashda False Sharing (soxta ulashish) hodisasi nima va u CPU L1/L2 keshiga qanday salbiy ta'sir ko'rsatadi?",
                new List<string> {
                    "Ikki alohida thread bir xil CPU Cache Line (masalan 64 bayt) ichida joylashgan turli o'zgaruvchilarni yangilaganda kesh doimiy inaktivatsiyaga uchrab tezlik tushadi",
                    "Faqat RAM to'lib qolganda yuzaga keladi",
                    "Diskka xabar yozishda paydo bo me'ladi",
                    "Faqat Single-core protsessorlarda bo'ladi"
                },
                "False Sharing bir xil CPU Cache Line ichidagi turli o me'zgaruvchilar har xil iplar tomonidan o'zgartirilganda kesh liniyasining majburiy invalidate bo'lishiga olib keladi."),

            CreateQuestion("TaskCompletionSource<T> va ManualResetEventSlim yordamida asinxron kutish va hodisalar (Signals) qanday muvofiqlashtiriladi?",
                new List<string> {
                    "Callback yoki event-ga asoslangan sinxron koddagi hodisani asinxron Task-ga aylantirib await qilish imkonini beradi",
                    "Faqat fayllarni yuklashda ishlaydi",
                    "Thread-ni abadiy bloklab qo'yadi",
                    "Faqat ASP.NET Core controller-da ishlaydi"
                },
                "TaskCompletionSource<T> hodisa kelganda Task-ni yakunlash (SetResult/SetException) orqali asinxron kutish imkonini beradi."),

            CreateQuestion("C#-da ArrayPool<T> ishlatishning GC LOH (Large Object Heap) fragmentation-ga ta'siri nimada?",
                new List<string> {
                    "Katta bayt massivlarini qayta ishlatish (pool) orqali LOH-da tez-tez yangi massiv yaratilishini va xotira bo me me'yoriy fragmentatsiyasini oldini oladi",
                    "Massivlarni xotirada 2 marta qisqartiradi",
                    "Faqat string turlarida ishlaydi",
                    "ArrayPool massivlarni har safar o'chirib beradi"
                },
                "ArrayPool.Shared massiv buferlarini qayta ishlatib LOH allocation va fragmentation-ni bartaraf qiladi."),

            CreateQuestion("Lock-free dasturlashda Memory Barrier (Thread.MemoryBarrier yoki Volatile.Read) ning vazifasi nimada?",
                new List<string> {
                    "Protsessor va kompilyatorning ko me'rsatmalarni qayta tartiblashi (Instruction reordering) ni oldini oladi va kesh xotira xushxabarini kafolatlaydi",
                    "Faqat faylga yozishni to me'tatadi",
                    "Thread Pool-ni o'chirib qo'yadi",
                    "Garbage Collector-ni chaqiradi"
                },
                "Memory Barrier CPU va Kompilyatordan ko me me'rsatmalarni o'zboshimchalik bilan qayta tartiblamaslikni va xotiradan aniq o me me me'qishni talab qiladi."),

            CreateQuestion("C#-da custom [AsyncMethodBuilder] yaratish orqali asinxron metodning ijro qilish mexanizmini o me'zgartirish qanday maqsadlarda ishlatiladi?",
                new List<string> {
                    "Async/await State Machine va uning natijasini saqlash xarajatini maxsus allocatesiz buferlarga yo me'naltirish va maxsus task turlarini yaratish uchun",
                    "Faqat keshni to me'zalash uchun",
                    "Faqat exception-larni bostirish uchun",
                    "Faqat Visual Studio proyektini qurish uchun"
                },
                "Custom AsyncMethodBuilder kompilyator yaratadigan state machine builder-ini o'zgartirish va nolinchi allocation-li custom async turlarini yaratish imkonini beradi."),

            CreateQuestion("CLR-da obyekt xotira tuzilishida Object Header (SyncBlockIndex va TypeHandle) qanday vazifa bajaradi?",
                new List<string> {
                    "SyncBlockIndex lock synchronization va HashCode saqlaydi; TypeHandle esa metodlar jadvaliga (MethodTable VMT) ishora qiladi",
                    "Ular faqat GC-ni to'xtatish uchun kerak",
                    "Ular faqat string uzunligini saqlaydi",
                    "Ular o me me me'zgaruvchilar sonini hisoblaydi"
                },
                "Reference Type obyekt xotirada 8-bayt SyncBlockIndex va 8-bayt TypeHandle (MethodTable pointer) header ma me'lumotiga ega bo me'ladi."),

            CreateQuestion("C#-da StructLayoutAttribute (LayoutKind.Sequential, Pack=1) nimani boshqaradi?",
                new List<string> {
                    "Struct maydonlarining xotiradagi bayt joylashuvi (padding) va tartibini C++ / Unmanaged interop uchun qat'iy belgilaydi",
                    "Struct-ni Heap-ga o me'tkazadi",
                    "Struct-ni avtomatik string-ga o'g'iradi",
                    "U faqat class-lar uchun ishlaydi"
                },
                "StructLayout(LayoutKind.Sequential, Pack=1) xotiradagi padding alignment-ni o'chirib, struct baytlarini ketma-ket joylashtiradi."),

            CreateQuestion("Garbage Collector rejimlarida Server GC va Workstation GC o'rtasidagi unumdorlik farqi nimada?",
                new List<string> {
                    "Server GC har bir CPU yadrosiga alohida GC thread va heap ajratadi (yuqori throughput); Workstation GC esa kamroq xotira va kamroq latencyni ko'zlaydi",
                    "Workstation GC faqat Linux-da ishlaydi",
                    "Server GC har doim sekinroq ishlaydi",
                    "Ular o me'rtasida farq yo'q"
                },
                "Server GC ko me'p yadroli serverlarda parallel yig'ish va alohida heaps orqali o'ta yuqori throughput beradi."),

            CreateQuestion("P/Invoke (Platform Invoke) va C# 9+ dagi LibraryImport (Source-generated interop) o'rtasidagi farq nimada?",
                new List<string> {
                    "DllImport runtime reflection va DLR marshalling ishlatadi; LibraryImport esa kompilyatsiya vaqtida toza C# marshalling kodini generatsiya qilib Native AOT ga mos tezlik beradi",
                    "LibraryImport faqat C++ da ishlaydi",
                    "DllImport har doim tezroq ishlaydi",
                    "Ular bir xil ishlaydi"
                },
                "LibraryImport C# 11 / .NET 7 da Source Generator orqali interop marshalling kodini kompilyatsiya vaqtida tuzadi."),

            CreateQuestion("CLR SynchronizationContext va TaskScheduler o'rtasidagi mantiqiy bog'liqlik va asinxron ijro tartibi qanday?",
                new List<string> {
                    "SynchronizationContext asinxron davom etuvchi kodni muayyan ipga (masalan UI Thread) yuboradi; TaskScheduler esa Task-lar qanday thread-larda bajarilishini rejalashtiradi",
                    "TaskScheduler faqat fayllarni o me'qiydi",
                    "SynchronizationContext faqat SQL Server bilan ishlaydi",
                    "Ular bir xil vazifani bajaradi"
                },
                "SynchronizationContext message loop muhitlarida (WinForms/WPF) UI ipiga qaytishni ta'minlaydi. TaskScheduler esa ThreadPool-da tasklarni bo me'ladi."),

            CreateQuestion("ConcurrentDictionary<TKey, TValue> ichki tuzilishida Lock Striping va Granular Locking qanday unumdorlik beradi?",
                new List<string> {
                    "Butun lug'atni bitta lock bilan emas, balki alohida bucket segmentlarini (lock arrays) guruhlab lock qiladi va parallel o'qish/yozish tezligini keskin oshiradi",
                    "U faqat bitta thread-ni qo me'llaydi",
                    "U har doim GC yig'ishini o'tkazadi",
                    "U lock-dan foydalanmaydi"
                },
                "ConcurrentDictionary Lock Striping (bir nechta kichik lock-lar) yordamida har xil bucket-larga bir vaqtda parallel yozishga ruxsat beradi."),

            CreateQuestion("Span<T> ichki tuzilishidagi ByReference<T> va Length maydonlari qanday xotira xavfsizligini beradi?",
                new List<string> {
                    "ByReference<T> unmanaged pointer o'rniga GC tushunadigan managed ref pointer tutadi, Length esa massiv chegarasidan chiqib ketmaslikni (Bounds Check) ta me'minlaydi",
                    "Span faqat string-larni saqlaydi",
                    "Span xotirani shifrlab beradi",
                    "ByReference faqat SQL-da ishlaydi"
                },
                "Span<T> managed pointer (`ref T`) va `int length` dan iborat ref struct bo me'lib, GC xavfsiz xotira kesmalarini beradi."),

            CreateQuestion("ThreadPool Starvation muammosi nima va u asinxron koddagi .Result yoki .Wait() chaqiriqlari sababli qanday kelib chiqadi?",
                new List<string> {
                    "Sinxron .Result chaqiruvi ThreadPool worker thread-ini bloklab beradi; yangi task-lar uchun thread-lar yetishmay ThreadPool sekin yangi thread yaratishi sababli tizim osilib qoladi",
                    "ThreadPool avtomatik 1000000 thread yaratadi",
                    "ThreadPool Starvation faqat RAM tugaganda yuz beradi",
                    "U faqat Linux OS-da kelib chiqadi"
                },
                "Sync-over-async (`.Result` yoki `.Wait()`) ThreadPool thread-larini bloklab qo'yadi va Thread Injection Rate soniyasiga 1-2 tadan oshmagani sababli deadlock/starvation beradi."),

            CreateQuestion("C#-da stackalloc yordamida Unmanaged Stack Memory ajratishda StackOverflowException oldini olish uchun qanday cheklov va Span qo'llaniladi?",
                new List<string> {
                    "stackalloc hajmini tekshirib (masalan 1KB dan kichik bo'lsa), natijani Span<T> ga biriktirish va katta hajmda ArrayPool-ga o'tish kerak",
                    "stackalloc avtomatik Heap-ga ko'chadi",
                    "stackalloc hech qanday exception otmaydi",
                    "stackalloc faqat string-larda ishlaydi"
                },
                "stackalloc Stack-da tezkor xotira ajratadi. Katta hajmlarda StackOverflowException bermasligi uchun hajmi tekshirilib `Span<byte>` bilan ishlatiladi."),

            CreateQuestion("C# 11 dagi ref fields in ref structs imkoniyati nima beradi?",
                new List<string> {
                    "ref struct ichida boshqa xotira manziliga ko'rsatgich (ref field) saqlash va murakkab zero-allocation xotira ko'rsatkichlarini tuzish imkonini beradi",
                    "ref struct-ni Heap-ga ko me'chirishga ruxsat beradi",
                    "ref fields faqat string turlarida ishlaydi",
                    "U faqat SQL so me'rovlarini tezlashtiradi"
                },
                "C# 11 ref fields in ref structs xotira buferlarini ko'rsatuvchi murakkab Stack-only ma'lumotlar tuzilmasini yaratishga imkon beradi."),

            CreateQuestion("C#-da WeakReference<T> va GCHandle (Normal, Weak, WeakTrackResurrection, Pinned) turlari qachon kerak bo'ladi?",
                new List<string> {
                    "Obyektni GC o me me'chirishiga tosqinlik qilmagan holda keshda ushlash (Weak) yoki Native C++ ga obyekt manzilini o me'zgarmas qilib uzatish (Pinned) uchun",
                    "Faqat JSON serializatsiya uchun",
                    "Faqat HTTP controller-da ishlaydi",
                    "GCHandle faqat AOT-da taqiqlangan"
                },
                "WeakReference GC yig'ishiga xalaqit bermaydi. GCHandle.Alloc(obj, GCHandleType.Pinned) esa unmanaged C++ koddagi ko'rsatgichlar uchun xotirada qotiradi."),

            CreateQuestion("High-Performance Matnlarni formatlashda ISpanFormattable va Utf8Formatter interfeyslarining afzalligi nimada?",
                new List<string> {
                    "String allocation yaratmasdan to'g'ridan-to'g'ri Span<char> yoki Span<byte> buferiga matn shaklida yozib beradi (Zero-Allocation Formatting)",
                    "Ular faqat fayllarni o me me'chirish uchun ishlaydi",
                    "Ular matnni shifrlab yozadi",
                    "Ular faqat SQL Server bilan ishlaydi"
                },
                "ISpanFormattable `.ToString()` chaqirmasdan to'g'ridan-to'g'ri taqdim etilgan Span buferiga yozib beradi va string allocation-ni yo me'qotadi."),

            CreateQuestion("System.Runtime.CompilerServices.Unsafe sinfi (Unsafe.As, Unsafe.Add) yordamida bajariladigan operatsiyalar qanday xatarlarga ega?",
                new List<string> {
                    "Kompilyator va CLR type safety tekshiruvlarini aylanib o'tadi; Noto'g'ri ishlatilsa xotira korrupsiyasi (Memory Corruption) va Access Violation Crash beradi",
                    "U faqat keshni tozalaydi",
                    "U har doim xavfsiz va xatosiz ishlaydi",
                    "U faqat AOT ilovalarda ishlaydi"
                },
                "Unsafe sinfi C++ ko'rsatgichlari kabi xom xotira amallarini bajaradi va tur xavfsizligini ta'minlamaydi."),

            CreateQuestion("System.Reflection.Emit.DynamicMethod yordamida IL (Intermediate Language) kodini runtime-da generatsiya qilish nimaga tayanadi?",
                new List<string> {
                    "ILGenerator orqali xotirada to'g'ridan-to'g'ri IL bayt-kodini tuzib joriy protsessda dinamik metod sifatida kompilyatsiya va ijro qilish uchun",
                    "U faqat HTML yaratish uchun ishlaydi",
                    "U ma me'lumotlar bazasini o me me'chiradi",
                    "U faqat Windows OS-da ishlaydi"
                },
                "Reflection.Emit runtime-da yuqori unumli dinamik metodlar (IL Bytecode) yaratish uchun IoC container va Serializer-larda qo me'llaniladi."),

            CreateQuestion("CLR Exception Handling ichki mexanizmida SEH (Structured Exception Handling) va 2-Pass Exception Filter qanday bajariladi?",
                new List<string> {
                    "1-Pass: Call Stack bo me'lab kim ushbu xatoni ushlashini (when filter) qidiradi; 2-Pass: Stack-ni orqaga yechib (unwinding) finally va catch bloklarini tartib bilan bajaradi",
                    "Xatolik darhol ilovani o me me'chiradi",
                    "Exception Handling faqat Single-thread-da ishlaydi",
                    "Ular o me'rtasida 2-Pass yo'q"
                },
                "CLR Exception Engine 2 parametrli ko'rib chiqishga ega: 1-Pass yig'ish va filterlash; 2-Pass esa stack unwinding va finally ijrosidir."),

            CreateQuestion("Thread sinxronizatsiya primitivlarida Kernel-mode (Mutex, Semaphore) va User-mode (SpinLock, Interlocked) o'rtasidagi unumdorlik farqi nimada?",
                new List<string> {
                    "Kernel-mode primitivlari OS context switch talab qiladi va sekinroq (mikrosoniyalar); User-mode (SpinLock/Interlocked) esa CPU tsiklida tez bajariladi (nanosoniyalar)",
                    "Kernel-mode har doim tezroq ishlaydi",
                    "User-mode faqat Windows OS-da ishlaydi",
                    "Ular bir xil tezlikka ega"
                },
                "Kernel-mode operatsiyalari OS yadrosiga context switch qiladi va qimmat. User-mode (Interlocked/SpinLock) esa CPU darajasida o me'ta tez ishlaydi."),

            CreateQuestion("Custom MemoryManager<T> yaratish qaysi yuqori unumdorlikdagi (High-performance) ssenariyda qo'llaniladi?",
                new List<string> {
                    "Unmanaged Native xotirani (C++ allocated pointer) yoki Memory Mapped File-ni safe Memory<T> va Span<T> turlariga o'rab (wrap) berish uchun",
                    "Faqat string-larni birlashtirish uchun",
                    "Faqat SQL Server parametri uchun",
                    "U faqat AOT-da taqiqlangan"
                },
                "MemoryManager<T> tashqi unmanaged xotira bo me me'laklarini managed `Memory<T>` va `Span<T>` ko'rinishida xavfsiz boshqarish imkonini beradi."),

            CreateQuestion("C# 12 dagi Inline Arrays ([InlineArray(10)]) atributi va uning xotira joylashuvi bo'yicha afzalligi nimada?",
                new List<string> {
                    "Struct ichida o me'zgarmas o'lchamli massiv elementlarini Heap-da alohida array ob'ekti yaratmasdan, to'g'ridan-to'g'ri struct xotira blokiga inline joylashtiradi",
                    "Inline array-lar har doim LOH-ga tushadi",
                    "Inline array-lar faqat string saqlaydi",
                    "Ular xotira sarfini 10 baravar oshiradi"
                },
                "Inline Arrays (C# 12) struct tanasining o'zida belgilangan sondagi elementlarni contiguous inline xotiraga joylaydi va Heap allocation-ni 0 ga tushiradi."),

            CreateQuestion("CLR Type Loader va MethodTable tuzilishida Virtual Method Table (VMT) dispatch qanday amalga oshiriladi?",
                new List<string> {
                    "Virtual metod chaqirilganda obyektning TypeHandle ko'rsatgichi orqali MethodTable vtable slotlaridan metod xotira adresi topiladi va chaqiriladi",
                    "Virtual metodlar har doim inline bo'ladi",
                    "VMT faqat static metodlar uchun ishlaydi",
                    "VMT faqat struct turlarida bo me'ladi"
                },
                "Virtual dispatch runtime-da obyekt MethodTable vtable slot-laridagi metod ko me me'rsatgichini qidirib topadi (indirection)."),

            CreateQuestion(".NET diagnostics va monitoring ekotizimida EventPipe, EventListener va System.Diagnostics.Metrics nima beradi?",
                new List<string> {
                    "Profilir va monitoring vositalariga (dotnet-trace, dotnet-counters, OpenTelemetry) runtime ichidagi GC, ThreadPool va Custom Metric hodisalarini 0-overhead bilan uzatadi",
                    "Faqat log faylini o me'chirish uchun kerak",
                    "Faqat HTML hesabat yaratadi",
                    "Ular faqat Windows OS-da ishlaydi"
                },
                "EventPipe va System.Diagnostics.Metrics .NET cross-platform diagnostika infratuzilmasi bo me'lib, runtime unumdorlik ko'rsatkichlarini monitoring qilishga yordam beradi.")
        };
    }
}
