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
                "ASP.NET Core ilovalarining asosiy konseptlari, Middleware quvuri, Kestrel va Routing bo'yicha professional savollar.",
                "Easy",
                "globe",
                GenerateDotNetEasyQuestions()
            ),
            CreateQuiz(
                "ASP.NET Core Architecture & Web API Deep Dive",
                "dotnet",
                "C# & .NET Core",
                "Dependency Injection lifetimes, Action Filters, JWT Auth, Custom Middleware va Rate Limiting bo'yicha senior savollar.",
                "Medium",
                "layers",
                GenerateDotNetMediumQuestions()
            ),
            CreateQuiz(
                "ASP.NET Core High-Performance & Principal Architecture",
                "dotnet",
                "C# & .NET Core",
                "Kestrel Transports, System.IO.Pipelines, Zero-allocation Formatter va Memory Pool profiling bo'yicha principal savollar.",
                "Hard",
                "cpu",
                GenerateDotNetHardQuestions()
            )
        };
    }

    private static List<Question> GenerateDotNetEasyQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "ASP.NET Core-da So'rovlar quvurida (Request Pipeline) Middleware-lar qanday tartibda va qanday ishlaydi?",
                "app.UseRouting();\napp.UseAuthentication();\napp.UseAuthorization();\napp.MapControllers();",
                new List<string> {
                    "Middleware-lar `Use` chaqirilgan ketma-ketlikda kiruvchi so'rovni qayta ishlaydi va `next()` orqali keyingisiga o me me'yorida uzatadi; Qaytishda teskari tartibda chiqadi",
                    "Middleware-lar har doim tasodifiy tartibda ishlaydi",
                    "Faqat birinchi middleware ishlaydi, qolganlari bajarilmaydi",
                    "Middleware-lar faqat static fayllarni yuklaydi"
                },
                "Middleware-lar rasmda bo'lgani kabi 'Russian Doll' (Matryoshka) zanjiri bo'yicha ishlaydi: kirishda tartib bilan, chiqishda teskari tartibda."
            ),
            CreateQuestion(
                "ASP.NET Core ichki (built-in) Kestrel veb-serveri haqida qaysi ta'rif to'g'ri?",
                "var builder = WebApplication.CreateBuilder(args);",
                new List<string> {
                    "Kestrel — ko'p platformali (cross-platform), asinxron I/O ga asoslangan va o'ta yuqori unumdorlikka ega tarmoq serveridir",
                    "Kestrel faqat Windows IIS serverida ishlaydi",
                    "Kestrel faqat HTML fayllarni ko'rsatish uchun ishlatiladi",
                    "Kestrel ma'lumotlar bazasi hisoblanadi"
                },
                "Kestrel ASP.NET Core uchun standart yuqori unumdorlikdagi asinxron veb-server hisoblanadi."
            ),
            CreateQuestion(
                "Dependency Injection-da `AddTransient` va `AddScoped` o'rtasidagi asosiy farq nimada?",
                "builder.Services.AddTransient<IEmailSender, EmailSender>();\nbuilder.Services.AddScoped<IOrderService, OrderService>();",
                new List<string> {
                    "AddTransient har safar so'ralganda yangi obyekt beradi; AddScoped esa bitta HTTP request (so'rov) doirasida yagona bitta obyektni qaytaradi",
                    "AddScoped butun ilova to'xtaguncha yagona obyekt beradi (Singleton)",
                    "AddTransient faqat interfeysiz sinflar bilan ishlaydi",
                    "Ikkala lifetime ham bir xil ishlaydi va hech qanday farq qilmaydi"
                },
                "AddTransient har bir inject qilinganda yangi instance yaratadi. AddScoped esa joriy HTTP request doirasida bitta obyekt instance saqlaydi."
            ),
            CreateQuestion(
                "ASP.NET Core Minimal API-da `TypedResults` ishlatishning oddiy `Results` sinfiga nisbatan afzalligi nimada?",
                "app.MapGet(\"/api/user/{id}\", (int id) => TypedResults.Ok(new UserDto(id)));",
                new List<string> {
                    "TypedResults strongly-typed qaytarish turini beradi, bu Swagger/OpenAPI hujjatlariga va unit testlarga moslikni ta'minlaydi",
                    "TypedResults so'rovni 10 marta tezlashtiradi",
                    "TypedResults faqat HTML qaytaradi",
                    "TypedResults ma'lumotlar bazasini avtomatik yangilaydi"
                },
                "TypedResults qaytarayotgan HTTP status kodi va obyekt tipini compile-time da aniq beradi va OpenApi metadata uchun juda qulay."
            ),
            CreateQuestion(
                "ASP.NET Core sozlalamalarini (Configuration) o'qishda Options Pattern (`IOptions<T>`) ishlatishning afzalligi nimada?",
                "public class SmtpOptions { public string Host { get; set; } }\n// Inject IOptions<SmtpOptions>",
                new List<string> {
                    "Konfiguratsiyani strongly-typed sinfga bog'laydi, validation beradi va IConfiguration string kalitlariga bog'liqlikni yo'qotadi",
                    "appsettings.json faylini o'chirib yuboradi",
                    "Faqat ma'lumotlar bazasi parolini saqlash uchun ishlatiladi",
                    "Faqat controller-siz Minimal API-larda ishlaydi"
                },
                "Options Pattern strongly-typed konfiguratsiya va validatsiya imkonini beradi."
            ),
            CreateQuestion(
                "ASP.NET Core Web API-da `[ApiController]` atributining asosiy vazifasi va qulayliklari nimalardan iborat?",
                "[ApiController]\n[Route(\"api/[controller]\")]\npublic class UsersController : ControllerBase",
                new List<string> {
                    "Model state validatsiyasini avtomatik tekshirib 400 Bad Request qaytaradi va Body/Route parametrlari bind bo'lishini soddalashtiradi",
                    "Controller-ni avtomatik Singleton qiladi",
                    "Faqat HTML view-larni ko'rsatishga imkon beradi",
                    "Database migration-ni ishga tushiradi"
                },
                "[ApiController] avtomatik 400 validation response va infer binding source-larni (FromQuery, FromBody) ta'minlaydi."
            ),
            CreateQuestion(
                "ASP.NET Core-da Exception Handling Middleware (`app.UseExceptionHandler()`) qanday ishlaydi?",
                "app.UseExceptionHandler(\"/error\");",
                new List<string> {
                    "Quvurdagi istalgan chutilmagan xatolikni (unhandled exception) tutib oladi va uni tayinli error controller yoki ProblemDetails formatida qaytaradi",
                    "Xatoliklarni e'tiborsiz qoldirib so'rovni davom ettiradi",
                    "Faqat 404 Not Found xatolarini ushlaydi",
                    "Database tranzaksiyalarini commit qiladi"
                },
                "UseExceptionHandler so'rovlar quvuridagi barcha tutilmagan exception-larni markazlashgan holda ushlaydi."
            ),
            CreateQuestion(
                "ASP.NET Core-da CORS (Cross-Origin Resource Sharing) siyosati qayerda va qanday tartibda ulanishi shart?",
                "app.UseCors(\"AllowSpecificOrigins\");",
                new List<string> {
                    "app.UseRouting() dan keyin va app.UseAuthorization() dan oldin joylashishi shart",
                    "Pipeline-ning eng oxirida app.MapControllers() dan keyin",
                    "Program.cs faylida eng birinchi bo'lib",
                    "CORS siyosatini qo me me'yorida ulash shart emas"
                },
                "CORS middleware UseRouting-dan keyin va UseAuthorization/UseEndpoints-dan oldin joylashishi lozim."
            ),
            CreateQuestion(
                "ASP.NET Core-da `IHttpClientFactory` yordamida `HttpClient` obyektlarini yaratishning asosiy sababi nima?",
                "builder.Services.AddHttpClient();",
                new List<string> {
                    "HttpClientHandler va underlying socket ulanishlarini qayta ishlatadi hamda Socket Exhaustion va DNS refresh muammolarini hal qiladi",
                    "HTTP so'rovlarini xotirada shifrlaydi",
                    "Faqat GET so'rovlarini bajarishga imkon beradi",
                    "HttpClient yaratilishini taqiqlaydi"
                },
                "IHttpClientFactory ulanishlarni (sockets) samarali boshqaradi va `using(new HttpClient())` keltirib chiqaradigan Socket Exhaustion-ni bartaraf etadi."
            ),
            CreateQuestion(
                "ASP.NET Core Web API-da `ProblemDetails` (RFC 7807) standarti nimani anglatadi?",
                "return TypedResults.Problem(\"Xatolik yuz berdi\", statusCode: 500);",
                new List<string> { "HTTP API-larda xatolik va muammolar haqida bir xillashtirilgan va standartlashgan JSON strukturada javob qaytarish", "Faqat brauzer keshini tozalash", "Faqat database ulanish xatolari uchun", "Kodni kompilyatsiya qilish xatosi" },
                "ProblemDetails (RFC 7807) barcha API xatoliklarini standart JSON formatda (type, title, status, detail) taqdim etadi."
            )
        };
    }

    private static List<Question> GenerateDotNetMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "ASP.NET Core Dependency Injection-da 'Capturing Scoped Service in Singleton' (Scoped servisni Singleton-da ushlab qolish) muammosi va uni tekshirish qanday ishlaydi?",
                "builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = true);",
                new List<string> {
                    "Singleton servis qisqa umrli Scoped servisga bog me'nilsa, Scoped servis ham doimiy saqlanib qolib Memory Leak va Capturing State xatosini beradi; ValidateScopes buni aniqlaydi",
                    "Singleton servis avtomatik Transient-ga aylanadi",
                    "Scoped servis har bir daqiqada o'chib turadi",
                    "Bu holat ASP.NET Core-da tavsiya etilgan eng to'g'ri amaliyotdir"
                },
                "Singleton Scoped servisni o'zida saqlasa (capture qilsa), Scoped obyekt va uning DbContext-i hech qachon Dispose bo'lmaydi va xotira sizadi hamda concurrency xatosi beradi."
            ),
            CreateQuestion(
                "ASP.NET Core-da Rate Limiting Middleware (Fixed, Sliding, Token Bucket, Concurrency) qanday ishlaydi?",
                "options.AddTokenBucketLimiter(\"token-bucket\", opt => { opt.TokenLimit = 100; opt.ReplenishmentPeriod = TimeSpan.FromSeconds(10); });",
                new List<string> {
                    "So'rovlar chastotasini cheklaydi; Token Bucket har ma'lum vaqtda belgilan miqdorda tokenlar qo me'yori bilan so'rovlarni o'tkazadi",
                    "Faqat IP manzillarni bloklaydi",
                    "Faqat static fayllarni yuklashni cheklaydi",
                    "SQL bazadagi ma'lumotlarni o'chiradi"
                },
                "Rate Limiting so'rovlar oqimini tartibga soladi. Token Bucket tokenlar to me me'lishi bo'yicha so'rovlarga ruxsat beradi."
            ),
            CreateQuestion(
                "ASP.NET Core Output Caching (`UseOutputCache`) va Response Caching (`UseResponseCaching`) orasidagi farq nimada?",
                "app.UseOutputCache();",
                new List<string> {
                    "Output Caching (.NET 7+) server xotirasida saqlanadi, Tag-based Invalidation va Custom Cache Policy-ni beradi; Response Caching esa HTTP header-lariga (Cache-Control) tayanadi",
                    "Response Caching faqat server xotirasini tozalaydi",
                    "Output Caching faqat brauzerda saqlanadi",
                    "Ikkalasi ham mutlaqo bir xil bajariladi"
                },
                "Output Caching server-side kesh texnologiyasi bo'lib taglar bo'yicha invalidation va moslashtirilgan siyosatlarni qo'llaydi."
            ),
            CreateQuestion(
                "ASP.NET Core Custom Authorization Policy va AuthorizationHandler<TRequirement> qanday ishlaydi?",
                "public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>",
                new List<string> {
                    "Foydalanuvchi claims-lari yoki ma'lumotlar bazasi bo me me'yorida murakkab biznes qoidalarni tekshiradi (PBAC / ABAC)",
                    "Faqat parolni tekshiradi",
                    "Faqat JWT tokenining muddatini uzaytiradi",
                    "Faqat IP manzilini tekshiradi"
                },
                "AuthorizationHandler yordamida yosh, rollar, permission-lar yoki DB holatiga asoslangan dinamik avtorizatsiya talablari (Requirements) tekshiriladi."
            ),
            CreateQuestion(
                "ASP.NET Core-da `IHostedService` va `BackgroundService` yordamida orqa fonda ishlovchi vazifalar (Background Tasks) qanday boshqariladi?",
                "public class QueueProcessorService : BackgroundService {\n    protected override async Task ExecuteAsync(CancellationToken stoppingToken) { ... }\n}",
                new List<string> {
                    "Ilova ishga tushganda orqa fonda (background thread) vazifani boshlaydi va CancellationToken orqali Graceful Shutdown-ni ta'minlaydi",
                    "Faqat Controller chaqirilganda ishga tushadi",
                    "Faqat HTTP so'rovlarini to'xtatadi",
                    "Faqat ma'lumotlar bazasini formatlaydi"
                },
                "BackgroundService long-running vazifalarni bajaradi va stoppingToken orqali ilova to'xtaganda tartibli yopiladi (Graceful shutdown)."
            ),
            CreateQuestion(
                "ASP.NET Core-da `Polly` kutubxonasi va `IHttpClientFactory` integratsiyasi (Resilience Policies) nima beradi?",
                "builder.Services.AddHttpClient<IMyClient, MyClient>()\n    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500)));",
                new List<string> {
                    "Tashqi API so'rovlarida xatolik bo'lganda avtomatik Retry, Circuit Breaker va Fallback siyosatlarini qo'llaydi",
                    "Faqat JSON parser sifatiga javob beradi",
                    "Faqat fayllarni yuklaydi",
                    "HttpClient obyektini o'chirib yuboradi"
                },
                "Polly va IHttpClientFactory integratsiyasi tarmoq xatolari va transient fail-larda Retry va Circuit Breaker beradi."
            ),
            CreateQuestion(
                "ASP.NET Core Web API-da Custom Model Binder (`IModelBinder`) yaratish qaysi holatlarda talab etiladi?",
                "public async Task BindModelAsync(ModelBindingContext bindingContext)",
                new List<string> {
                    "So'rov parametrlaridan (Query/Header/Body) murakkab va noan'anaviy strukturalarni custom ob'ektlarga o'girish va parsed qilish uchun",
                    "Faqat parolni shifrlash uchun",
                    "Faqat HTML chiqarish uchun",
                    "Faqat database ulanishini ochish uchun"
                },
                "Custom Model Binder kelayotgan string, header yoki qismlarni maxsus obyekt tipiga o'girish uchun ishlatiladi."
            ),
            CreateQuestion(
                "ASP.NET Core SignalR-da Hubs va WebSockets/Server-Sent Events (SSE) muloqoti va Transport Fallback qanday ishlaydi?",
                "public class QuizHub : Hub { public async Task SendScore(...) { ... } }",
                new List<string> {
                    "Real-vaqtda ikki tomonlama (duplex) muloqot beradi; WebSockets qo'llanmasa avtomatik Server-Sent Events yoki Long Polling-ga fallback qiladi",
                    "Faqat HTTP GET so'rovlarini bajaradi",
                    "Faqat static rasmlarni uzatadi",
                    "Faqat SQL Server bilan ishlaydi"
                },
                "SignalR real-time muloqot uchun WebSockets-dan foydalanadi va brauzer qo me me'yoriga qarab SSE yoki Long Polling-ga tushadi."
            ),
            CreateQuestion(
                "ASP.NET Core-da Custom Action Filter va `ActionExecutingContext` orqali so'rovni bekor qilish (Short-circuiting) qanday bajariladi?",
                "context.Result = new BadRequestObjectResult(\"Invalid request\");",
                new List<string> {
                    "context.Result parametriga `IActionResult` obyektini tayinlash orqali so'rovni Action metodiga yetkazmasdan darhol javob bilan qaytarish",
                    "Throw Exception chaqirish majburiy",
                    "HttpResponse-ni null qilish",
                    "Thread Pool-ni o'chirib qo'yish"
                },
                "ActionFilter ichida `context.Result` tayinlansa pipeline short-circuit bo'ladi va so'rov controller action-iga o'tmaydi."
            ),
            CreateQuestion(
                "ASP.NET Core-da Data Protection API (`IDataProtectionProvider`) vazifasi nimadan iborat?",
                "var protector = _provider.CreateProtector(\"QuizSecretPurpose\");\nstring protectedData = protector.Protect(\"MyData\");",
                new List<string> {
                    "Kritik ma'lumotlar, cookie va tokenlarni shifrlash (Protect) va qayta ochish (Unprotect) va kalitlarni saqlashni boshqaradi",
                    "Faqat ma'lumotlar bazasi zaxira nusxasini oladi",
                    "Faqat HTML xavfsizligini ta'minlaydi",
                    "Faqat keshni tozalaydi"
                },
                "Data Protection API shifrlash kalitlarini (Keys) va ma'lumotlar xavfsiz shifrlanishini boshqaradi."
            )
        };
    }

    private static List<Question> GenerateDotNetHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "ASP.NET Core Kestrel-da High-Throughput I/O operatsiyalarida System.IO.Pipelines va PipeReader/PipeWriter ishlatishning GC-ga va perfomansga ta'siri nimada?",
                "public async Task ReadRequestAsync(PipeReader reader)\n{\n    while (true) {\n        ReadResult result = await reader.ReadAsync();\n        ReadOnlySequence<byte> buffer = result.Buffer;\n        // Process without byte[] allocation\n    }\n}",
                new List<string> {
                    "Bayt massivlari (byte[]) ajratilishini yo'qotib, Zero-Allocation I/O va LOH (Large Object Heap) bosimini bartaraf etadi",
                    "Faqat fayllarni shifrlash uchun ishlatiladi",
                    "GC-ni har bir so'rovdan keyin majburiy chaqiradi",
                    "Kestrel unumdorligini 50% ga sekinlashtiradi"
                },
                "System.IO.Pipelines xotirada bayt massivlarini qayta-qayta yaratmasdan (buffer reuse) Zero-Allocation I/O ni ta'minlaydi."
            ),
            CreateQuestion(
                "ASP.NET Core-da Thread Pool Starvation (iplar ochligi) qanday kelib chiqadi va u High-RPS serverda nimaga olib keladi?",
                "// Xato yondashuv (Sync-over-Async):\nvar result = _service.GetDataAsync().Result;",
                new List<string> {
                    "Sinxron kod ichida .Result yoki .Wait() chaqirish ThreadPool worker iplarini bloklaydi va so'rovlar navbatda to'planib latency va 503 xatolariga olib keladi",
                    "GC Gen 2 to'lishiga olib keladi",
                    "Kestrel serverni avtomatik qayta ishga tushiradi",
                    "Faqat CPU haroratini oshiradi"
                },
                "Sync-over-Async (.Result / .Wait()) ThreadPool-dagi barcha mavjud worker thread-larni bloklab qo'yadi."
            ),
            CreateQuestion(
                "ASP.NET Core-da `EndpointDataSource` va Dynamic Endpoint Routing internals qanday ishlaydi?",
                "public class DynamicEndpointDataSource : EndpointDataSource",
                new List<string> {
                    "Kompilyatsiyasiz runtime davomida yangi HTTP endpoint-larni dinamik qo'shish va yo me me'yori routing jadvallarini yangilash imkonini beradi",
                    "Faqat static fayllar keshini tozalaydi",
                    "Faqat SQL query-larni o'zgartiradi",
                    "Faqat IIS serverida ishlaydi"
                },
                "EndpointDataSource ASP.NET Core-da dinamik marshrutlash va endpoint-larni runtime-da boshqarish vositasidir."
            ),
            CreateQuestion(
                "ASP.NET Core-da Custom `AuthenticationHandler<TOptions>` sinfini kengaytirganda `HandleAuthenticateAsync()` nimani qaytarishi kerak?",
                "protected override async Task<AuthenticateResult> HandleAuthenticateAsync()",
                new List<string> {
                    "AuthenticateResult.Success(ticket) yoki AuthenticateResult.Fail(...) / NoResult()",
                    "Faqat true yoki false",
                    "Faqat ClaimsIdentity ob'ekti",
                    "HttpResponseMessage" },
                "HandleAuthenticateAsync AuthenticateResult ob'ektini qaytaradi (Success, Fail, yoki NoResult)."
            ),
            CreateQuestion(
                "High-scale ASP.NET Core Web API-da AsyncLocal<T> va ExecutionContext sizib chiqishi (leak) qanday yuzaga keladi?",
                "private static readonly AsyncLocal<UserSession> _session = new();",
                new List<string> {
                    "AsyncLocal konteksti asinxron oqimlar orqali uzatiladi; Uzoq yashaydigan yoki unmanaged thread-larda tozalanmasa xotirada eskirgan obyektlarni ushlab qoladi",
                    "Faqat static fayllar yuklanganda bo'ladi",
                    "Faqat IIS qayta ishga tushganda bo'ladi",
                    "Faqat GC Gen 0 tozganda bo'ladi"
                },
                "AsyncLocal ExecutionContext bilan birga uzatiladi va unmanaged yoki pooled thread-larda tozalanmasa xotira sizishiga olib keladi."
            ),
            CreateQuestion(
                "ASP.NET Core-da High-Performance Custom Output Formatter (`IOutputFormatter`) qanday yaratiladi va ishlaydi?",
                "public async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)",
                new List<string> {
                    "Stream-ga to'g'ridan-to'g'ri Utf8JsonWriter yoki System.IO.Pipelines orqali 0-allocation bilan obyektni yozadi",
                    "Faqat string.Concat ishlatadi",
                    "Faqat HTML string qaytaradi",
                    "Faqat database-ga saqlaydi"
                },
                "Custom OutputFormatter Response Stream-ga to'g me me'yori yozib intermediate string allocations-ni yo'qotadi."
            ),
            CreateQuestion(
                "ASP.NET Core-da Security Hardening uchun HSTS, CSP (Content Security Policy) va CORS Preflight so'rovlari Nginx bilan birgalikda qanday muvofiqlashtiriladi?",
                "app.UseHsts();",
                new List<string> {
                    "HSTS brauzerga HTTPS majburiyligini bildiradi, CSP ziyonli skriptlarni (XSS) cheklaydi, Nginx esa Preflight OPTIONS so'rovlarini backend-ga yetkazmasdan o me me'yorida qaytarishi mumkin",
                    "Faqat database-ni shifrlaydi",
                    "Faqat Kestrel portini o'zgartiradi",
                    "Faqat SQL injection-ni to'xtatadi"
                },
                "HSTS brauzerni faqat HTTPS ishlatishga majburlaydi, CSP esa faqat ruxsat berilgan resurslarni yuklashga qo'yadi."
            ),
            CreateQuestion(
                "ASP.NET Core-da YARP (Yet Another Reverse Proxy) yordamida gRPC va REST so'rovlarini dinamik routing qilish qanday ishlaydi?",
                "builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection(\"ReverseProxy\"));",
                new List<string> {
                    "C#-da yozilgan yuqori unumdorlikka ega reverse proxy bo'lib, mikroservislar o'rtasida dinamik load balancing va routing ta'minlaydi",
                    "Faqat Windows IIS-da ishlaydi",
                    "Faqat static HTML ko'rsatadi",
                    "Faqat database migration uchun ishlatiladi"
                },
                "YARP .NET-da tayyorlangan reverse proxy kutubxonasi bo'lib High-performance routing va gRPC/HTTP2 proxying beradi."
            ),
            CreateQuestion(
                "High-scale Web API-da Kestrel MaxConcurrentConnections va MaxConcurrentUpgradedConnections sozlamalari qanday rol o'ynaydi?",
                "options.Limits.MaxConcurrentConnections = 10000;",
                new List<string> {
                    "Server qabul qila oladigan maksimal bir vaqtdagi ulanishlar sonini cheklaydi va DOS/Resource Exhaustion hujumlaridan va RAM to'lib qolishidan hisoblaydi",
                    "Faqat SQL bazaga ulanishni cheklaydi",
                    "Faqat CPU haroratini pasaytiradi",
                    "Faqat fayl hajmini cheklaydi"
                },
                "Kestrel ulanish limitsiz bo'lib qolmasligi va server RAM exhaustion bo'lmasligi uchun MaxConcurrentConnections qo me me'yori ishlatiladi."
            ),
            CreateQuestion(
                "ASP.NET Core 9-da yangi kiritilgan `HybridCache` kutubxonasi `IMemoryCache` va `IDistributedCache` ning qaysi muammolarini hal etadi?",
                "await _hybridCache.GetOrCreateAsync(\"key\", async cancel => await FetchDataAsync());",
                new List<string> {
                    "L1 (In-Memory) va L2 (Redis) keshni birlashtiradi va Cache Stampede (bir vaqtda ko'p so'rov tushishi) muammosini L1-da lock orqali avtomatik hal qiladi",
                    "Faqat diskka yozadi",
                    "Faqat SQL Server-da ishlaydi",
                    "MemoryCache-ni o'chirib yuboradi"
                },
                "HybridCache L1 In-Memory va L2 Distributed Caching-ni birlashtiradi hamda stampede protection (lock) beradi."
            )
        };
    }
}
