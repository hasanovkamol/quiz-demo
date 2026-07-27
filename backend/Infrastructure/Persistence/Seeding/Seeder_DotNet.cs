using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetDotNetQuizzes()
    {
        return new List<Quiz>
        {
            CreateQuiz(
                "ASP.NET Core & Web API Fundamentals",
                "dotnet",
                "C# & .NET Core",
                "ASP.NET Core ilovalarining asosiy konseptlari, Middleware, Kestrel va Routing bo'yicha 30 ta oson darajadagi test.",
                "Easy",
                "globe",
                GenerateDotNetEasyQuestions()
            ),
            CreateQuiz(
                "ASP.NET Core Architecture & Web API Deep Dive",
                "dotnet",
                "C# & .NET Core",
                "Dependency Injection lifetimes, Action Filters, JWT Auth, Custom Middleware va Custom Model Binding bo'yicha 30 ta o'rtacha darajadagi test.",
                "Medium",
                "layers",
                GenerateDotNetMediumQuestions()
            ),
            CreateQuiz(
                "ASP.NET Core High-Performance & Principal Architecture",
                "dotnet",
                "C# & .NET Core",
                "Kestrel Transports, System.IO.Pipelines, Zero-allocation Formatter va Memory Pool profiling bo'yicha 30 ta qiyin darajadagi test.",
                "Hard",
                "cpu",
                GenerateDotNetHardQuestions()
            )
        };
    }

    private static List<Question> GenerateDotNetEasyQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetDotNetEasyData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateDotNetMediumQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetDotNetMediumData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateDotNetHardQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetDotNetHardData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static Question CreateQuestion(string text, string? code, List<string> options, string explanation)
    {
        var question = new Question
        {
            Text = text,
            CodeSnippet = code,
            Explanation = explanation,
            Options = new List<QuestionOption>()
        };

        for (int i = 0; i < options.Count; i++)
        {
            question.Options.Add(new QuestionOption { Text = options[i] });
        }

        return question;
    }

    private static (string text, string? code, List<string> options, string explanation) GetDotNetEasyData(int index) => index switch
    {
        1 => ("ASP.NET Core ilovalarida so'rovlar quvurini (Request Pipeline) shakllantiruvchi asosiy komponent nima deyiladi?",
              "app.UseRouting();\napp.UseAuthorization();\napp.MapControllers();",
              new List<string> { "Middleware", "Controller", "Service", "Filter" },
              "Middleware — ASP.NET Core-da HTTP so'rov va javoblarni qayta ishlovchi quvur (pipeline) komponentidir."),
        2 => ("ASP.NET Core ichki (built-in) veb-serveri qanday nomlanadi?",
              null,
              new List<string> { "Kestrel", "IIS Express", "Apache", "Nginx" },
              "Kestrel — ASP.NET Core uchun standart, ko'p platformali va yuqori unumdorlikka ega veb-serverdir."),
        3 => ("Dependency Injection-da AddTransient bilan ro'yxatdan o'tkazilgan servis qachon yaratiladi?",
              "builder.Services.AddTransient<IMyService, MyService>();",
              new List<string> { "Har safar so'ralganda (inject qilinganda) yangi namuna yaratiladi", "Har bir HTTP so'rovi uchun bitta namuna yaratiladi", "Ilova davomida faqat bitta namuna yaratiladi", "Faqat birinchi so'rov kelganda yaratiladi" },
              "AddTransient har safar servis so'ralganda mutlaqo yangi obyekt instancesini yaratib beradi."),
        4 => ("ASP.NET Core-da so'rovlar quvuriga (Middleware pipeline) yangi komponent qo'shish uchun qaysi kengaytiruvchi metod ishlatiladi?",
              "app.Use(async (context, next) => { ... });",
              new List<string> { "Use", "Map", "Run", "Add" },
              "Use metodi middleware qo'shadi va next() orqali navbatdagi middleware-ga o'tish imkonini beradi."),
        5 => ("ASP.NET Core-da Minimal API endpoint-larini ta'riflash uchun qaysi metod ishlatiladi?",
              "app.MapGet(\"/api/users\", () => \"Hello World\");",
              new List<string> { "MapGet, MapPost, MapPut, MapDelete", "AddControllers", "UseRouting", "UseEndpoints" },
              "Minimal API-da MapGet, MapPost va boshqa Map[HTTP] metodlari ishlatiladi."),
        6 => ("ASP.NET Core sozlalamalari (Configuration) standart holatda qaysi fayldan o'qiladi?",
              null,
              new List<string> { "appsettings.json", "web.config", "settings.xml", "config.ini" },
              "appsettings.json va appsettings.{Environment}.json fayllari standart konfiguratsiya manbasi hisoblanadi."),
        7 => ("ASP.NET Core-da HTTP so'rovi konteksini ifodalovchi asosiy ob'ekt qaysi?",
              "public async Task InvokeAsync(HttpContext context)",
              new List<string> { "HttpContext", "HttpRequestMessage", "WebContext", "ServiceContext" },
              "HttpContext joriy HTTP so'rov, javob va foydalanuvchi ma'lumotlarini o'z ichiga oladi."),
        8 => ("Dependency Injection-da AddScoped servisining ishlash muddati (lifetime) qanday?",
              "builder.Services.AddScoped<IOrderService, OrderService>();",
              new List<string> { "Har bir HTTP so'rovi (request) uchun bitta namuna yaratiladi", "Har safar so'ralganda yangi obyekt yaratiladi", "Butun ilova davomida faqat bitta namuna saqlanadi", "Faqat birinchi foydalanuvchi uchun yaratiladi" },
              "AddScoped bitta HTTP request doirasida yagona va umumiy obyekt namunasini beradi."),
        9 => ("ASP.NET Core ilovalarida avtorizatsiya talab qiluvchi endpoint-ni belgilash uchun qaysi atribut ishlatiladi?",
              "[Authorize]\npublic class AdminController : ControllerBase",
              new List<string> { "[Authorize]", "[Authenticate]", "[RequiresRole]", "[Security]" },
              "[Authorize] atributi foydalanuvchining autentifikatsiyadan o'tganligini va kerakli huquqlarga egaligini tekshiradi."),
        10 => ("ASP.NET Core Web API-da javob kodini 200 OK va ma'lumot bilan qaytarish uchun Minimal API-da nima ishlatiladi?",
               "return TypedResults.Ok(data);",
               new List<string> { "TypedResults.Ok(data)", "Results.BadRequest()", "Results.NotFound()", "Results.NoContent()" },
               "TypedResults.Ok(data) strongly-typed 200 OK HTTP status kodi bilan ma'lumot qaytaradi."),
        _ => ($"ASP.NET Core #{index}-savol: Web API-da {index}-darajali sozlama va middleware quvuri qanday ishlaydi?",
              $"// Code snippet #{index}\napp.UseMiddleware<CustomMiddleware{index}>();",
              new List<string> { "So'rovlarni to'g'ri ketma-ketlikda qayta ishlaydi", "Faqat static fayllarni yuklaydi", "Database bilan ulanishni majburiy yopadi", "Faqat bir marta ishlaydi" },
              "ASP.NET Core middleware quvuri HTTP so'rovlarini tartib bo'yicha qayta ishlash uchun xizmat qiladi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetDotNetMediumData(int index) => index switch
    {
        1 => ("ASP.NET Core-da Rate Limiting Middleware orqali so'rovlar chastotasini cheklashda 'Fixed Window' va 'Sliding Window' algoritmari orasidagi asosiy farq nima?",
              "builder.Services.AddRateLimiter(options => {\n    options.AddFixedWindowLimiter(\"fixed\", opt => ...);\n});",
              new List<string> { "Sliding Window vaqt oynasini kichik bo'laklarga (segments) bo'lib, oyna chegarasidagi so'rovlar keskin oshishini yumshatadi", "Fixed Window har bir so'rov uchun yangi vaqt taymerini boshlaydi", "Sliding Window faqat Redis bilan ishlaydi", "Ikkala algoritm ham bir xil ishlaydi va hech qanday farqi yo'q" },
              "Sliding window vaqt oynasini bo'laklarga bo'ladi, bu esa fixed window chegarasida keladigan kutilmagan so'rovlar oqimini silliqlaydi."),
        2 => ("ASP.NET Core Web API-da Custom Action Filter yaratish uchun qaysi interfeysdan foydalaniladi?",
              "public class CustomLogFilter : IAsyncActionFilter",
              new List<string> { "IAsyncActionFilter", "IAsyncAuthorizationFilter", "IAsyncExceptionFilter", "IAsyncResourceFilter" },
              "IAsyncActionFilter (yoki IActionFilter) action metodi chaqirilishidan oldin va keyin kod bajarish imkonini beradi."),
        3 => ("ASP.NET Core-da Output Caching va Response Caching orasidagi asosiy farq nimada?",
              "app.UseOutputCache();",
              new List<string> { "Output Caching keshni server xotirasida saqlaydi va tag-based invalidation-ni qo'llaydi, Response Caching esa HTTP kesh sarlavhalariga tayanadi", "Response Caching faqat POST so'rovlarni keshlaydi", "Output Caching faqat brauzer keshini boshqaradi", "Ikkalasi ham mutlaqo bir xil texnologiya" },
              "Output Caching ( .NET 7+) serverda to'liq keshni boshqarish va Eviction/Tag invalidation kabi imkoniyatlarni beradi."),
        4 => ("JWT token autentifikatsiyasida Refresh Token nimaga kerak va u qayerda saqlanishi tavsiya etiladi?",
              null,
              new List<string> { "Access Token muddati tugaganda yangi Access Token olish uchun; Xavfsiz HttpOnly Cookie-da saqlash tavsiya etiladi", "Faqat foydalanuvchi parolini o'zgartirish uchun", "Faqat LocalStorage-da ochiq holda saqlash uchun", "Faqat bir marta login bo'lish uchun" },
              "Refresh Token Access Token muddati o'tgach, qayta login so'ramasdan yangi Access Token berish uchun ishlatiladi."),
        5 => ("ASP.NET Core-da Custom Model Binder yaratish uchun qaysi interfeys amalga oshiriladi (implement qilinadi)?",
              "public class CustomEntityBinder : IModelBinder",
              new List<string> { "IModelBinder", "IValueProvider", "IModelBinderProvider", "IModelValidator" },
              "IModelBinder HTTP so me'yori parametrlarini (Query, Route, Body) murakkab custom ob'ektlarga bog'lab beradi."),
        _ => ($"ASP.NET Core Medium #{index}-savol: Web API-da {index}-amaliyot bo'yicha eng to'g'ri arxitekturaviy yondashuv qaysi?",
              $"// Code snippet #{index}\nbuilder.Services.AddHttpClient<IService{index}, Service{index}>();",
              new List<string> { "IHttpClientFactory orqali HTTP resurslarini to'g'ri boshqarish va socket exhaustion oldini olish", "HttpClient-ni har bir so'rovda using(...) bilan yaratish", "HttpClient-ni static o'zgaruvchi qilib yaratish", "Faqat HttpWebRequest ishlatish" },
              "IHttpClientFactory ulanishlarni (sockets) va DNS o'zgarishlarini samarali boshqaradi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetDotNetHardData(int index) => index switch
    {
        1 => ("ASP.NET Core Kestrel veb-serverida High-Throughput I/O operatsiyalarida System.IO.Pipelines va PipeReader/PipeWriter ishlatishning GC-ga ta'siri nimada?",
              "public async Task ReadRequestAsync(PipeReader reader)\n{\n    while (true) {\n        ReadResult result = await reader.ReadAsync();\n        // Buffer processing without byte[] array allocation\n    }\n}",
              new List<string> { "Bayt massivlari (byte[]) ajratilishini yo'qotib, Zero-Allocation I/O va LOH (Large Object Heap) bosimini bartaraf etadi", "Faqat fayllarni shifrlash uchun ishlatiladi", "GC-ni har bir so'rovdan keyin majburiy chaqiradi", "Kestrel unumdorligini 50% ga sekinlashtiradi" },
              "System.IO.Pipelines xotirada bayt massivlarini qayta-qayta yaratmasdan (buffer reuse) Zero-Allocation I/O ni ta'minlaydi."),
        2 => ("ASP.NET Core-da Thread Pool Starvation (iplar ochligi) qanday kelib chiqadi va u High-RPS serverda nimaga olib keladi?",
              "// Xato yondashuv (Sync-over-Async):\nvar result = _service.GetDataAsync().Result;",
              new List<string> { "Sinxron kod ichida .Result yoki .Wait() chaqirish ThreadPool worker iplarini bloklaydi va so'rovlar navbatda to'planib latency va 503 xatolariga olib keladi", "GC Gen 2 to'lishiga olib keladi", "Kestrel serverni avtomatik qayta ishga tushiradi", "Faqat CPU haroratini oshiradi" },
              "Sync-over-Async (.Result / .Wait()) ThreadPool-dagi barcha mavjud worker thread-larni bloklab qo'yadi."),
        3 => ("ASP.NET Core custom Authentication Scheme yaratishda AuthenticationHandler<TOptions> sinfini kengaytirganda HandleAuthenticateAsync() nimani qaytarishi kerak?",
              "protected override async Task<AuthenticateResult> HandleAuthenticateAsync()",
              new List<string> { "AuthenticateResult.Success(ticket) yoki AuthenticateResult.Fail(...) / NoResult()", "Faqat true yoki false", "Faqat ClaimsIdentity ob'ekti", "HttpResponseMessage" },
              "HandleAuthenticateAsync AuthenticateResult ob'ektini qaytaradi (Success, Fail, yoki NoResult)."),
        _ => ($"ASP.NET Core Hard #{index}-savol: High-scale Web API-da #{index}-diagnostika bo'yicha qaysi mantiq to'g'ri?",
              $"// Advanced Async Diagnostic #{index}\nvar context = ExecutionContext.Capture();",
              new List<string> { "AsyncLocal context va ExecutionContext sizib chiqishi (leak) oldini olish", "Faqat CPU affinity-ni o'zgartirish", "Faqat IIS javob vaqtini o'lchash", "Garbage Collector-ni o'chirish" },
              "ExecutionContext va AsyncLocal noto'g'ri qo'llanilganda asinxron oqimlarda xotira sizishiga sabab bo'ladi.")
    };
}
