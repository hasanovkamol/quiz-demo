using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetSeniorAspNetCoreQuizzes()
    {
        return new List<Quiz>
        {
            CreateQuiz(
                "Senior C# Til Asoslari va Ilg'or Mavzular",
                "senior-aspnetcore",
                "Senior ASP.NET Core",
                "Value type, async/await, ValueTask, ConfigureAwait, Predicate, event, generics, IQueryable, yield return, boxing, using, record, nullable, GC Generations bo'yicha 15 ta test.",
                "Hard",
                "terminal",
                GenerateSeniorCSharpBasicsQuestions()
            ),
            CreateQuiz(
                "Senior ASP.NET Core Asoslari",
                "senior-aspnetcore",
                "Senior ASP.NET Core",
                "Middleware, DI Lifetime, BackgroundService, Minimal Hosting, Routing, Filters, Razor Pages, Minimal API, CORS, Options Pattern, Kestrel, Health Checks, Caching, Rate Limiting, SignalR bo'yicha 20 ta test.",
                "Hard",
                "server",
                GenerateSeniorAspNetCoreBasicsQuestions()
            ),
            CreateQuiz(
                "Senior Web API va REST Arxitekturasi",
                "senior-aspnetcore",
                "Senior ASP.NET Core",
                "RESTful tamoyillar, HTTP status kodlari, API versiyalash, DTO, AutoMapper, Idempotency, Pagination, Swagger, gRPC, GraphQL, HATEOAS, Streaming bo'yicha 12 ta test.",
                "Hard",
                "api",
                GenerateSeniorWebApiQuestions()
            ),
            CreateQuiz(
                "Senior Entity Framework Core",
                "senior-aspnetcore",
                "Senior ASP.NET Core",
                "DbContext lifetime, AsNoTracking, Migrations, Lazy Loading, N+1, Deferred Execution, SaveChanges, Concurrency, Fluent API, Many-to-Many, Global Query Filters, Stored Procedures, Compiled Queries, Bulk Operations, DbContext Pooling, Repository Pattern, Split Queries, IgnoreQueryFilters bo'yicha 18 ta test.",
                "Hard",
                "database",
                GenerateSeniorEfCoreQuestions()
            ),
            CreateQuiz(
                "Senior Logging, Monitoring va Tracing",
                "senior-aspnetcore",
                "Senior ASP.NET Core",
                "ILogger, Structured Logging, Serilog, Log Levels, Correlation ID, OpenTelemetry, Distributed Tracing, Application Insights, Sensitive Data, Metrics bo'yicha 10 ta test.",
                "Hard",
                "cpu",
                GenerateSeniorLoggingQuestions()
            ),
            CreateQuiz(
                "Senior Xavfsizlik (Security)",
                "senior-aspnetcore",
                "Senior ASP.NET Core",
                "Authentication vs Authorization, JWT, Refresh Token, OAuth2/OpenID Connect, Role vs Policy, CSRF, XSS, SQL Injection, Password Hashing, HSTS bo'yicha 10 ta test.",
                "Hard",
                "lock",
                GenerateSeniorSecurityQuestions()
            ),
            CreateQuiz(
                "Senior Arxitektura va Dizayn Pattern'lari",
                "senior-aspnetcore",
                "Senior ASP.NET Core",
                "Clean Architecture, CQRS, MediatR, Repository Pattern, SRP, Mikroservislar, Message Brokers, Saga Pattern, Circuit Breaker, DDD Aggregate bo'yicha 10 ta test.",
                "Hard",
                "code-2",
                GenerateSeniorArchitectureQuestions()
            ),
            CreateQuiz(
                "Senior Testing, DevOps va Boshqa Mavzular",
                "senior-aspnetcore",
                "Senior ASP.NET Core",
                "Unit vs Integration Test, WebApplicationFactory, CI/CD, Docker Multi-Stage Build, Trunk-Based Development bo'yicha 5 ta test.",
                "Hard",
                "rocket",
                GenerateSeniorTestingDevOpsQuestions()
            )
        };
    }

    // ==========================================
    // Bo'lim 1: C# Til Asoslari va Ilg'or Mavzular (1-15)
    // ==========================================
    private static List<Question> GenerateSeniorCSharpBasicsQuestions()
    {
        return new List<Question>
        {
            // 1
            CreateQuestion("Mahalliy (local) o'zgaruvchi sifatida e'lon qilingan value type odatda qayerda saqlanadi?",
                new List<string> {
                    "Stack'da",
                    "Har doim Heap'da",
                    "Faqat register'da",
                    "Disk cache'da"
                },
                "Value type lokal o'zgaruvchi sifatida e'lon qilinganda Stack xotirasida saqlanadi. Heap'da faqat boxing yoki reference type ichida joylashganda saqlanadi."),

            // 2
            CreateQuestion("Async metod compile qilinganda C# compiler nima yaratadi?",
                new List<string> {
                    "State machine (metod bajarilish holatini boshqaruvchi struct/class)",
                    "Yangi OS thread",
                    "Faqat oddiy delegate",
                    "Hech narsa — runtime avtomatik boshqaradi"
                },
                "C# compiler async metodlarni state machine'ga aylantiradi. Bu struct/class metod bajarilish holatini (state) saqlaydi va await nuqtalarida to'xtab, davom etish imkonini beradi."),

            // 3
            CreateQuestion("ValueTask<T> qachon Task<T> o'rniga tavsiya etiladi?",
                new List<string> {
                    "Natija ko'pincha sinxron/tez qaytadigan va allocation'ni kamaytirish kerak bo'lgan holatlarda",
                    "Har doim, chunki u har doim tezroq",
                    "Faqat void metodlarda",
                    "Faqat exception qaytarishda"
                },
                "ValueTask<T> natija ko'pincha sinxron qaytadigan holatlarda Task<T> allocation'ini oldini oladi. Ammo u faqat bir marta await qilinishi kerak va boshqa chelovlar mavjud."),

            // 4
            CreateQuestion("ASP.NET Core kodida ConfigureAwait(false) haqida qaysi fikr to'g'ri?",
                new List<string> {
                    "ASP.NET Core'da SynchronizationContext yo'qligi sababli deadlock xavfi past, lekin qayta ishlatiladigan kutubxona kodida yaxshi amaliyot sifatida qoladi",
                    "Majburiy, aks holda har doim deadlock yuz beradi",
                    "Faqat WPF/WinForms ilovalarida ishlatiladi, web'da umuman kerak emas",
                    "Faqat Task.Run bilan ishlatiladi"
                },
                "ASP.NET Core'da SynchronizationContext mavjud emas, shuning uchun deadlock xavfi past. Ammo qayta ishlatiladigan kutubxona kodida ConfigureAwait(false) hali ham yaxshi amaliyot hisoblanadi."),

            // 5
            CreateQuestion("Async kodda deadlock ko'pincha qaysi holatda yuz beradi?",
                new List<string> {
                    "Task.Result yoki .Wait() ni SynchronizationContext mavjud bo'lgan muhitda (masalan, eski ASP.NET yoki UI thread) chaqirilganda",
                    "To'liq async/await zanjiridan foydalanilganda",
                    "ConfigureAwait(false) qo'llanilganda",
                    "Task.Run bilan background ishga tushirilganda"
                },
                "Deadlock SynchronizationContext mavjud muhitda (eski ASP.NET, WPF) .Result yoki .Wait() chaqirilganda yuz beradi, chunki async callback xuddi shu context'da bajarilishini kutadi, lekin u band."),

            // 6
            CreateQuestion("Predicate<T> delegate turi nima qaytaradi?",
                new List<string> {
                    "bool",
                    "void",
                    "T",
                    "object"
                },
                "Predicate<T> — bu T tipidagi parametr qabul qiluvchi va bool qaytaruvchi delegate. U ko'pincha List<T>.Find, List<T>.FindAll kabi metodlarda ishlatiladi."),

            // 7
            CreateQuestion("event kalit so'zi oddiy delegate maydonidan nimasi bilan farq qiladi?",
                new List<string> {
                    "Class tashqarisidan to'g'ridan-to'g'ri chaqirish yoki qayta tayinlashni cheklaydi, faqat += / -= orqali obuna bo'lish mumkin",
                    "Hech nima, ular bir xil",
                    "event faqat static bo'lishi shart",
                    "event'ga faqat bitta handler ulanishi mumkin"
                },
                "event kalit so'zi delegate maydonini inkapsulyatsiya qiladi — tashqi kod faqat += va -= orqali obuna bo'lishi mumkin, to'g'ridan-to'g'ri invoke yoki = tayinlash taqiqlangan."),

            // 8
            CreateQuestion("where T : class, new() generic cheklovi nimani anglatadi?",
                new List<string> {
                    "T reference type bo'lishi va parametrsiz ochiq konstruktorga ega bo'lishi kerak",
                    "T faqat struct bo'lishi kerak",
                    "T interface bo'lishi shart",
                    "T sealed class bo'lishi shart"
                },
                "class cheklovi T ni reference type bilan chegaralaydi, new() esa T ning parametrsiz public konstruktorga ega bo'lishini talab qiladi. Bu ikkalasi birgalikda T ni instantiate qilish imkonini beradi."),

            // 9
            CreateQuestion("IQueryable<T>ning IEnumerable<T>dan asosiy afzalligi nima?",
                new List<string> {
                    "Expression tree yaratib, so'rovni ma'lumotlar manbaida (masalan, SQL serverda) bajarish imkonini beradi (deferred execution)",
                    "Har doim tezroq ishlaydi",
                    "Faqat in-memory to'plamlar bilan ishlaydi",
                    "Avtomatik thread-safe bo'ladi"
                },
                "IQueryable<T> Expression tree yaratib, provayderga (masalan, EF Core) so'rovni SQL ga tarjima qilish va server tomonida bajarish imkonini beradi. IEnumerable<T> esa faqat in-memory filtrlaydi."),

            // 10
            CreateQuestion("yield return ishlatilganda compiler nima hosil qiladi?",
                new List<string> {
                    "Iterator — holatni saqlovchi lazy enumeration mexanizmi",
                    "Oddiy massiv",
                    "Yangi thread",
                    "Static class"
                },
                "yield return compiler tomonidan state machine'ga aylantiriladi. Bu iterator pattern'ini amalga oshirib, elementlarni faqat kerak bo'lganda (lazy) generatsiya qiladi."),

            // 11
            CreateQuestion("Boxing operatsiyasi performance'ga qanday ta'sir qiladi?",
                new List<string> {
                    "Value type'ni heap'ga nusxalash orqali qo'shimcha allocation va GC yukini oshiradi",
                    "Umuman ta'sir qilmaydi",
                    "Kodni tezlashtiradi",
                    "Faqat compile vaqtida sodir bo'ladi, runtime'ga ta'siri yo'q"
                },
                "Boxing value type'ni heap'da yangi object ichiga joylab nusxalaydi. Bu qo'shimcha xotira ajratish va GC bosimini oshiradi, ayniqsa tight loop'larda sezilarli performance muammosiga olib keladi."),

            // 12
            CreateQuestion("using statement compile vaqtida qanday konstruksiyaga aylanadi?",
                new List<string> {
                    "try/finally blokiga, finally ichida Dispose() chaqiriladi",
                    "if-else blokiga",
                    "while loop'ga",
                    "switch statement'ga"
                },
                "using statement compiler tomonidan try/finally blokiga aylantiriladi. finally blokida IDisposable.Dispose() chaqiriladi, bu exception bo'lsa ham resurslar bo'shatilishini kafolatlaydi."),

            // 13
            CreateQuestion("record va oddiy class o'rtasidagi asosiy farq nima?",
                new List<string> {
                    "record avtomatik value-based equality va with-expression'ni qo'llab-quvvatlaydi",
                    "record har doim to'liq immutable bo'ladi va boshqa hech qanday farqi yo'q",
                    "record faqat struct sifatida kompilyatsiya qilinadi",
                    "Ular to'liq sinonim, farq yo'q"
                },
                "record turi avtomatik ravishda value-based equality (Equals, GetHashCode), ToString, va with-expression'ni generatsiya qiladi. Bu uni DTO va immutable data modellari uchun juda qulay qiladi."),

            // 14
            CreateQuestion("#nullable enable yoqilganda compiler amalda nima qiladi?",
                new List<string> {
                    "Compile vaqtida null bo'lishi mumkin bo'lgan reference'lar haqida ogohlantirish (warning) beradi, runtime xatti-harakatini o'zgartirmaydi",
                    "Runtime'da null tekshiruvini majburiy qiladi",
                    "NullReferenceException'ni butunlay yo'q qiladi",
                    "Faqat value type'larga ta'sir qiladi"
                },
                "#nullable enable compiler darajasida statik tahlil orqali nullable reference type ogohlantirish beradi. Bu runtime xatti-harakatini o'zgartirmaydi, ammo null xavfli joylarni oldindan aniqlashga yordam beradi."),

            // 15
            CreateQuestion("Garbage Collector'ning Gen0 to'plami odatda qanday obyektlarni saqlaydi?",
                new List<string> {
                    "Yangi yaratilgan, qisqa umr ko'radigan obyektlarni (eng tez-tez to'planadigan qism)",
                    "Uzoq umr ko'radigan obyektlarni",
                    "Faqat static obyektlarni",
                    "Faqat Large Object Heap'dagi obyektlarni"
                },
                "Gen0 yangi yaratilgan obyektlarni saqlaydi. Ko'pchilik obyektlar qisqa umr ko'radi va Gen0 yig'ilishida yo'q qilinadi. Omon qolganlar Gen1, keyin Gen2'ga o'tkaziladi.")
        };
    }

    // ==========================================
    // Bo'lim 2: ASP.NET Core Asoslari (16-35)
    // ==========================================
    private static List<Question> GenerateSeniorAspNetCoreBasicsQuestions()
    {
        return new List<Question>
        {
            // 16
            CreateQuestion("app.Use(...) va app.Run(...) middleware'lari o'rtasidagi farq nima?",
                new List<string> {
                    "app.Use keyingi middleware'ga (next) o'tish imkonini beradi, app.Run esa pipeline'ni yakunlovchi (terminal) middleware hisoblanadi",
                    "Farqi yo'q, ikkalasi bir xil ishlaydi",
                    "app.Run faqat static fayllar uchun ishlatiladi",
                    "app.Use faqat authentication uchun mo'ljallangan"
                },
                "app.Use() next() delegate orqali keyingi middleware'ga o'tkazadi. app.Run() esa terminal middleware bo'lib, pipeline'ni yakunlaydi va keyingi middleware chaqirilmaydi."),

            // 17
            CreateQuestion("ASP.NET Core'ning built-in DI konteyneri qaysi lifetime turlarini qo'llab-quvvatlaydi?",
                new List<string> {
                    "Transient, Scoped va Singleton",
                    "Faqat Singleton",
                    "Faqat Transient va Singleton",
                    "Prototype, Session va Application"
                },
                "ASP.NET Core DI uchta lifetime turini qo'llab-quvvatlaydi: Transient (har chaqiruvda yangi), Scoped (har request'da yangi), Singleton (ilova umri davomida bitta)."),

            // 18
            CreateQuestion("Scoped service'ni Singleton service ichiga to'g'ridan-to'g'ri inject qilish nima uchun muammoli (captive dependency)?",
                new List<string> {
                    "Singleton butun ilova umri davomida yashagani uchun, unga inject qilingan Scoped service ham 'qamalib qoladi' va yangilanmaydi (masalan, DbContext eskirgan holda qoladi)",
                    "Bu umuman muammo emas",
                    "Bu faqat performance'ni yaxshilaydi",
                    "Faqat Transient service'lar uchun muammo tug'diradi"
                },
                "Captive dependency — Scoped service Singleton ichida 'qamalib qoladi'. DbContext kabi Scoped service'lar har request'da yangilanishi kerak, lekin Singleton ichida ular bir marta yaratiladi va eskirgan holda qoladi."),

            // 19
            CreateQuestion("BackgroundService abstrakt klassi qaysi metodni override qilishni talab qiladi?",
                new List<string> {
                    "ExecuteAsync(CancellationToken stoppingToken)",
                    "Execute()",
                    "Run()",
                    "Hech qaysisini, faqat StartAsync() yetarli"
                },
                "BackgroundService abstrakt klassi ExecuteAsync(CancellationToken) metodini override qilishni talab qiladi. Bu metod background task logikasini o'z ichiga oladi va CancellationToken orqali to'xtatiladi."),

            // 20
            CreateQuestion(".NET 6+ minimal hosting modelida WebApplicationBuilder nimani bitta joyga jamlaydi?",
                new List<string> {
                    "Host konfiguratsiyasi, DI konteyner va middleware pipeline sozlamalarini",
                    "Faqat routing sozlamalarini",
                    "Faqat logging provayderlarini",
                    "Faqat Kestrel portlarini"
                },
                "WebApplicationBuilder .NET 6+ da Host, DI konteyner, konfiguratsiya va middleware pipeline'ni bitta qulay API orqali sozlash imkonini beradi. Bu Startup.cs va Program.cs'ni birlashtirdi."),

            // 21
            CreateQuestion("Attribute routing'ning conventional routing'dan asosiy afzalligi nima?",
                new List<string> {
                    "Route'lar controller/action ustida aniq va mahalliy tarzda belgilanadi, murakkab yoki notekis URL sxemalarini boshqarish osonlashadi",
                    "Har doim ancha tezroq ishlaydi",
                    "Faqat Razor Pages uchun ishlaydi",
                    "DI talab qilmaydi"
                },
                "Attribute routing route'larni to'g'ridan-to'g'ri controller yoki action ustida [Route], [HttpGet] kabi atributlar orqali belgilaydi. Bu kodni o'qishni va murakkab URL sxemalarini boshqarishni osonlashtiradi."),

            // 22
            CreateQuestion("[ApiController] atributi bilan model validatsiyasi muvaffaqiyatsiz bo'lganda nima sodir bo'ladi?",
                new List<string> {
                    "Framework avtomatik ravishda 400 Bad Request javobini qaytaradi (automatic model state validation)",
                    "Hech narsa, buni har doim qo'lda tekshirish kerak",
                    "Har doim 500 Internal Server Error qaytariladi",
                    "So'rov jimgina e'tiborsiz qoldiriladi"
                },
                "[ApiController] atributi ModelState.IsValid tekshiruvini avtomatik qiladi. Agar model validatsiyasi muvaffaqiyatsiz bo'lsa, framework avtomatik 400 Bad Request qaytaradi."),

            // 23
            CreateQuestion("Standart holatda ASP.NET Core MVC filter pipeline'i qaysi tartibda ishga tushadi?",
                new List<string> {
                    "Authorization → Resource → Model Binding → Action → Exception → Result",
                    "Exception → Result → Action → Authorization",
                    "Result → Action → Authorization → Resource",
                    "Tartib har safar tasodifiy belgilanadi"
                },
                "MVC filter pipeline qat'iy tartibda ishlaydi: avval Authorization, keyin Resource filter, Model Binding, Action filter, Exception filter va nihoyat Result filter."),

            // 24
            CreateQuestion("Action Filter va Middleware o'rtasidagi asosiy farq nima?",
                new List<string> {
                    "Action Filter MVC pipeline ichida ishlaydi va routing/model binding ma'lumotlariga kirish huquqiga ega, Middleware esa umumiy HTTP pipeline darajasida ishlaydi",
                    "Farqi yo'q",
                    "Middleware faqat authentication uchun ishlatiladi",
                    "Action Filter har doim middleware'dan tezroq"
                },
                "Middleware HTTP pipeline darajasida ishlaydi va barcha so'rovlarni ko'radi. Action Filter esa MVC pipeline ichida ishlaydi va ActionContext, model binding natijalariga kirish imkoniga ega."),

            // 25
            CreateQuestion("Razor Pages'ning MVC'dan asosiy farqi nima?",
                new List<string> {
                    "Controller o'rniga sahifa-markazlashgan PageModel yondashuvidan foydalanadi",
                    "DI umuman qo'llab-quvvatlanmaydi",
                    "Faqat API endpoint'lar uchun mo'ljallangan",
                    "Faqat static HTML qaytaradi, C# kod ishlatilmaydi"
                },
                "Razor Pages sahifa-markazlashgan yondashuv bo'lib, har bir sahifa o'z PageModel'iga ega. Bu oddiy sahifalar uchun Controller+View juftligidan ko'ra soddaroq va tushunarli."),

            // 26
            CreateQuestion("Minimal API'ning an'anaviy Controller-based API'ga nisbatan asosiy afzalligi nima?",
                new List<string> {
                    "Kamroq boilerplate kod bilan kichik xizmatlar va endpoint'larni tezroq yaratish imkonini beradi",
                    "Ishlash tezligi har doim sezilarli darajada yuqori",
                    "Filter va validatsiyani umuman qo'llab-quvvatlamaydi",
                    "Faqat GET so'rovlarini qabul qiladi"
                },
                "Minimal API .NET 6+ da kiritilgan bo'lib, Controller, attribute routing va boshqa boilerplate kodsiz, to'g'ridan-to'g'ri endpoint'larni aniqlash imkonini beradi."),

            // 27
            CreateQuestion("AllowAnyOrigin() bilan AllowCredentials() birga ishlatilishi nima uchun muammoli?",
                new List<string> {
                    "Xavfsizlik nuqtai nazaridan xavfli — brauzer bunday kombinatsiyaga yo'l qo'ymaydi, chunki credentials bilan istalgan origin'ga ruxsat berish CSRF xavfini oshiradi",
                    "Hech qanday muammo yo'q, bu tavsiya etiladigan kombinatsiya",
                    "Faqat HTTPS ishlatilmaganda muammo tug'diladi",
                    "Bu ASP.NET Core'da texnik jihatdan mumkin emas"
                },
                "CORS spetsifikatsiyasi bo'yicha AllowAnyOrigin() bilan AllowCredentials() birga ishlatilishi taqiqlangan. Brauzerlar credentials (cookies, auth headers) bilan wildcard origin'ga ruxsat bermaydi."),

            // 28
            CreateQuestion("IOptionsSnapshot<T> qanday lifetime'ga ega va nima uchun Singleton service ichida ishlatib bo'lmaydi?",
                new List<string> {
                    "Scoped lifetime'ga ega, har bir so'rov uchun konfiguratsiyani qayta o'qiydi — shu sababli Singleton'ga inject qilinsa captive dependency xatosi yuzaga keladi",
                    "Singleton lifetime'ga ega, hech qanday cheklov yo'q",
                    "Transient, faqat bir marta ishlatiladi va keyin yo'q qilinadi",
                    "IOptionsSnapshot .NET'da mavjud emas"
                },
                "IOptionsSnapshot<T> Scoped lifetime'ga ega va har request'da konfiguratsiya fayldan qayta o'qiladi. Singleton service ichiga inject qilinsa captive dependency bo'ladi."),

            // 29
            CreateQuestion("Kestrel serverni production muhitida Nginx/IIS kabi reverse proxy ortida ishlatishning asosiy sababi nima?",
                new List<string> {
                    "Qo'shimcha xavfsizlik qatlami, load balancing va SSL termination kabi imkoniyatlarni qo'lga kiritish uchun",
                    "Kestrel mustaqil ishlay olmaydi",
                    "Kestrel faqat Windows platformasida ishlaydi",
                    "Reverse proxy Kestrel'ni majburiy almashtiradi"
                },
                "Kestrel mustaqil ishlay oladi, ammo production'da reverse proxy (Nginx, IIS) SSL termination, load balancing, static file serving va qo'shimcha xavfsizlik qatlamini ta'minlaydi."),

            // 30
            CreateQuestion("UseExceptionHandler middleware'ining asosiy vazifasi nima?",
                new List<string> {
                    "Pipeline'da yuzaga kelgan qayta ishlanmagan istisnolarni ushlab, foydalanuvchiga standartlashtirilgan xatolik javobini (masalan, ProblemDetails) qaytarish",
                    "Faqat log yozish",
                    "Faqat 404 xatoliklarini boshqarish",
                    "Faqat development muhitida ishlaydi"
                },
                "UseExceptionHandler() global exception handling middleware bo'lib, unhandled exception'larni ushlaydi va foydalanuvchiga ProblemDetails formatida standartlashtirilgan xatolik javobini qaytaradi."),

            // 31
            CreateQuestion("Health Checks (AddHealthChecks) nima uchun ishlatiladi?",
                new List<string> {
                    "Ilova va uning bog'liqliklari (DB, tashqi servis va h.k.) holatini monitoring/orkestratsiya tizimlariga (masalan, Kubernetes) bildirish uchun",
                    "Faqat unit test yozish uchun",
                    "Faqat logging konfiguratsiyasi uchun",
                    "Faqat autentifikatsiya tekshiruvi uchun"
                },
                "Health Checks ilova va uning bog'liqliklarining (DB, Redis, tashqi API) sog'lig'ini tekshiradi. Kubernetes, load balancer va monitoring tizimlariga ilova holati haqida ma'lumot beradi."),

            // 32
            CreateQuestion("Response Caching va Output Caching o'rtasidagi asosiy farq nima?",
                new List<string> {
                    "Output Caching (.NET 7+) serverda to'liq javobni saqlaydi va moslashuvchan invalidatsiya siyosatlarini qo'llab-quvvatlaydi, Response Caching esa asosan HTTP cache header'lariga tayanadi",
                    "Ular butunlay bir xil narsa",
                    "Response Caching faqat statik fayllar uchun ishlaydi",
                    "Output Caching faqat mijoz (client) tomonida ishlaydi"
                },
                "Output Caching .NET 7+ da kiritilgan server-side caching bo'lib, javobni serverda saqlaydi va tag-based invalidation qo'llab-quvvatlaydi. Response Caching esa HTTP cache header'lariga tayanadi."),

            // 33
            CreateQuestion(".NET 7+ dagi Rate Limiting middleware qaysi algoritmlarni qo'llab-quvvatlaydi?",
                new List<string> {
                    "Fixed Window, Sliding Window, Token Bucket va Concurrency Limiter kabi bir nechta strategiyalarni",
                    "Faqat bitta qattiq belgilangan algoritm",
                    "Faqat IP-manzil bo'yicha bloklashni",
                    "Rate limiting faqat tashqi kutubxonalar orqali amalga oshiriladi"
                },
                ".NET 7+ built-in Rate Limiting middleware Fixed Window, Sliding Window, Token Bucket va Concurrency Limiter algoritmlarini qo'llab-quvvatlaydi."),

            // 34
            CreateQuestion("SignalR asosan nima uchun ishlatiladi?",
                new List<string> {
                    "Server va mijoz o'rtasida real-vaqtli, ikki tomonlama aloqani (WebSocket va boshqa transportlar orqali) ta'minlash uchun",
                    "Faqat fayl yuklash uchun",
                    "Faqat REST API almashtirish uchun",
                    "Faqat statik kontentni keshlash uchun"
                },
                "SignalR real-time, bidirectional aloqani ta'minlaydi. WebSocket, Server-Sent Events va Long Polling transportlarini avtomatik tanlaydi. Chat, notification, live dashboard kabi ssenariylarda ishlatiladi."),

            // 35
            CreateQuestion("IOptionsMonitor<T> ning asosiy afzalligi nima?",
                new List<string> {
                    "Konfiguratsiya faylida real vaqt rejimida o'zgarish bo'lsa, Singleton service'lar ham buni darhol (change notification orqali) sezishi mumkin",
                    "Faqat ilova ishga tushganda bir marta o'qiydi",
                    "Faqat Scoped service'larda ishlaydi",
                    "Faqat test muhitida ishlatiladi"
                },
                "IOptionsMonitor<T> Singleton lifetime'ga ega va konfiguratsiya fayli o'zgarganda change notification beradi. Bu Singleton service'lar ichida ham konfiguratsiya yangilanishini real-time kuzatish imkonini beradi.")
        };
    }

    // ==========================================
    // Bo'lim 3: Web API va REST Arxitekturasi (36-47)
    // ==========================================
    private static List<Question> GenerateSeniorWebApiQuestions()
    {
        return new List<Question>
        {
            // 36
            CreateQuestion("RESTful API'ning asosiy tamoyillaridan biri qaysi?",
                new List<string> {
                    "Stateless aloqa — har bir so'rov o'zida to'liq kontekstni olib yuradi",
                    "Server har bir so'rov o'rtasida mijoz holatini (state) saqlashi shart",
                    "Faqat XML formatidan foydalanish majburiy",
                    "Faqat POST metodidan foydalanish kerak"
                },
                "REST arxitekturasining asosiy tamoyillaridan biri Stateless aloqa — har bir so'rov o'zida barcha kerakli kontekstni olib yuradi, server mijoz holatini saqlamaydi."),

            // 37
            CreateQuestion("400 va 422 status kodlari o'rtasidagi farq nima?",
                new List<string> {
                    "400 — so'rov sintaksisi noto'g'ri (malformed request), 422 — sintaksis to'g'ri, lekin semantik/validatsiya xatosi bor",
                    "Ular bir xil ma'noni bildiradi",
                    "422 faqat GET so'rovlarida ishlatiladi",
                    "400 faqat autentifikatsiya uchun ishlatiladi"
                },
                "400 Bad Request — so'rov strukturasi noto'g'ri (parse qilib bo'lmaydi). 422 Unprocessable Entity — strukturasi to'g'ri, lekin biznes qoidalariga mos kelmaydi (validatsiya xatosi)."),

            // 38
            CreateQuestion("API versiyalashning URL-based (/api/v1/...) usulining kamchiligi nima?",
                new List<string> {
                    "URL manzillarining 'shishishi' va bir nechta versiyani parallel qo'llab-quvvatlash murakkablashadi, header-based yoki media-type based usullar ko'proq 'toza' hisoblanadi",
                    "Kamchiligi umuman yo'q",
                    "U texnik jihatdan amalga oshirib bo'lmaydi",
                    "Faqat GraphQL bilan ishlaydi"
                },
                "URL-based versiyalash oddiy va tushunarli, ammo URL'lar ko'payadi va client-server contract'ini URL bilan bog'laydi. Header-based yoki media-type usullar ko'proq REST-ful hisoblanadi."),

            // 39
            CreateQuestion("Entity'ni to'g'ridan-to'g'ri API javobida qaytarish nima uchun yomon amaliyot hisoblanadi?",
                new List<string> {
                    "Ichki ma'lumotlar strukturasini oshkor qiladi, over-posting/under-posting xavfini oshiradi va DB sxemasi bilan API contract'ini qattiq bog'laydi",
                    "Chunki bu texnik jihatdan mumkin emas",
                    "Chunki entity'lar har doim juda kichik hajmda bo'ladi",
                    "Chunki JSON serializatsiya entity'larni qo'llab-quvvatlamaydi"
                },
                "Entity to'g'ridan-to'g'ri qaytarilsa, ichki DB strukturasi oshkor bo'ladi, circular reference muammolari yuzaga kelishi va over-posting xavfi oshadi. DTO ishlatish tavsiya etiladi."),

            // 40
            CreateQuestion("AutoMapper kabi mapping kutubxonalarining asosiy xavfi nima?",
                new List<string> {
                    "'Sehrli' (implicit) konfiguratsiyalar debugging'ni murakkablashtirishi va performance narxi (reflection-based mapping) bo'lishi mumkin",
                    "Ular hech qanday xavf tug'dirmaydi",
                    "Ular faqat EF Core bilan ishlaydi",
                    "Ular DI bilan mos kelmaydi"
                },
                "AutoMapper convention-based mapping qiladi, bu implicit bog'lanishlar yaratadi. Xato konfiguratsiya runtime'da topiladi, debugging qiyinlashadi va reflection-based mapping performance narxiga ega."),

            // 41
            CreateQuestion("Idempotency (bir xil natija bilan qayta ishlash) tushunchasi qaysi HTTP metodlariga xos?",
                new List<string> {
                    "GET, PUT, DELETE (to'g'ri loyihalangan holda) — bir necha marta chaqirilsa ham natija bir xil bo'ladi",
                    "Faqat POST",
                    "Faqat PATCH",
                    "Hech qaysi metod idempotent bo'la olmaydi"
                },
                "GET, PUT, DELETE idempotent metodlar — ularni bir necha marta chaqirish bir xil natija beradi. POST esa har chaqiruvda yangi resurs yaratishi mumkin (idempotent emas)."),

            // 42
            CreateQuestion("Katta hajmdagi ma'lumotlar uchun pagination qo'llashning asosiy sababi nima?",
                new List<string> {
                    "Bir vaqtning o'zida server va tarmoq resurslarini haddan tashqari band qilmaslik, javob vaqtini optimallashtirish",
                    "Faqat vizual dizayn uchun",
                    "Pagination faqat mijoz tomonida amalga oshiriladi, server tomoniga aloqasi yo'q",
                    "SQL Server pagination'ni qo'llab-quvvatlamaydi"
                },
                "Pagination server xotirasini, tarmoq bandwidth'ini va client rendering vaqtini optimallashtiradi. Minglab yozuvni bir vaqtda qaytarish server va client uchun og'ir yuk hisoblanadi."),

            // 43
            CreateQuestion("Swagger/OpenAPI ishlatishning asosiy afzalligi nima?",
                new List<string> {
                    "API'ni avtomatik hujjatlashtiradi va interaktiv test qilish, mijoz kod generatsiyasi kabi imkoniyatlarni beradi",
                    "Ilovaning ishlash tezligini oshiradi",
                    "Faqat production muhitida ishlaydi",
                    "Autentifikatsiyani almashtiradi"
                },
                "Swagger/OpenAPI API spetsifikatsiyasini avtomatik generatsiya qiladi, interaktiv UI orqali test qilish va turli tillarda client SDK generatsiya qilish imkonini beradi."),

            // 44
            CreateQuestion("gRPC qaysi holatda REST'dan afzalroq bo'ladi?",
                new List<string> {
                    "Mikroservislar orasidagi yuqori unumdorlikli, kam kechikuvli (low-latency) ichki aloqa uchun (Protobuf va HTTP/2 asosida)",
                    "Brauzerdan to'g'ridan-to'g'ri chaqiriladigan public API uchun",
                    "Faqat statik fayllarni uzatish uchun",
                    "gRPC va REST bir xil holatlarda ishlatiladi, farqi yo'q"
                },
                "gRPC Protobuf serializatsiya va HTTP/2 asosida ishlaydi. Binary format va multiplexing tufayli JSON/REST'dan tezroq va samaraliroq, ayniqsa service-to-service aloqada."),

            // 45
            CreateQuestion("GraphQL'ning REST'ga nisbatan asosiy afzalligi nima?",
                new List<string> {
                    "Mijoz aynan kerakli maydonlarni bitta so'rovda olishi mumkin (over-fetching/under-fetching muammosi kamayadi)",
                    "Har doim ancha oddiyroq sozlanadi",
                    "GraphQL faqat mutatsiyalarni qo'llab-quvvatladi",
                    "Caching GraphQL'da REST'ga qaraganda ancha osonroq"
                },
                "GraphQL mijozga aynan kerakli maydonlarni tanlash imkonini beradi, bu over-fetching (ortiqcha ma'lumot) va under-fetching (yetishmovchi ma'lumot) muammolarini hal qiladi."),

            // 46
            CreateQuestion("HATEOAS (Hypermedia as the Engine of Application State) nimani anglatadi?",
                new List<string> {
                    "API javoblarida keyingi mumkin bo'lgan amallar uchun havolalar (link) taqdim etilishi",
                    "Faqat autentifikatsiya sxemasi",
                    "API versiyasini boshqarish usuli",
                    "Ma'lumotlar bazasi indekslash strategiyasi"
                },
                "HATEOAS REST tamoyillaridan biri bo'lib, API javobida keyingi mumkin bo'lgan amallar (links) taqdim etiladi. Bu mijozga API'ni o'z-o'zidan kashf qilish imkonini beradi."),

            // 47
            CreateQuestion("Katta fayllarni yuklash/yuklab olishda streaming yondashuvining afzalligi nima?",
                new List<string> {
                    "Butun faylni xotiraga yuklamasdan, qismlarga bo'lib qayta ishlash orqali server xotirasi va resurslarini tejaydi",
                    "Fayl to'liq xotiraga (memory) yuklanadi va shu sababli tezroq ishlaydi",
                    "Streaming faqat video fayllar uchun ishlatiladi",
                    "Streaming HTTP protokoli tomonidan qo'llab-quvvatlanmaydi"
                },
                "Streaming faylni to'liq xotiraga yuklamasdan qismlarga bo'lib qayta ishlaydi. Bu server xotirasini tejaydi va katta fayllar (video, backup) bilan ishlashda majburiy yondashuv hisoblanadi.")
        };
    }

    // ==========================================
    // Bo'lim 4: Entity Framework Core (48-65)
    // ==========================================
    private static List<Question> GenerateSeniorEfCoreQuestions()
    {
        return new List<Question>
        {
            // 48
            CreateQuestion("AddDbContext orqali ro'yxatdan o'tkazilgan DbContext standart holatda qanday lifetime'ga ega?",
                new List<string> {
                    "Scoped",
                    "Singleton",
                    "Transient",
                    "Static"
                },
                "AddDbContext<T>() standart holatda DbContext'ni Scoped lifetime bilan ro'yxatdan o'tkazadi. Bu har bir HTTP request uchun bitta DbContext instance yaratiladi va request oxirida dispose qilinadi."),

            // 49
            CreateQuestion("AsNoTracking() nima uchun ishlatiladi?",
                new List<string> {
                    "Faqat o'qish (read-only) uchun ma'lumot olinganda Change Tracker yukini olib tashlab, performance'ni oshirish uchun",
                    "So'rov natijasini keshlash uchun",
                    "Ma'lumotlarni bazaga yozish uchun majburiy",
                    "Faqat migratsiyalar uchun ishlatiladi"
                },
                "AsNoTracking() Change Tracker'ga obyektlarni kuzatmaslikni aytadi. Bu xotira sarfini kamaytiradi va read-only so'rovlarda performance'ni sezilarli oshiradi."),

            // 50
            CreateQuestion("Production muhitida EF Core migratsiyalarini qo'llashning tavsiya etilgan yondashuvi qaysi?",
                new List<string> {
                    "Migratsiyalarni CI/CD pipeline orqali nazorat ostida, SQL skript generatsiya qilib yoki alohida deployment bosqichida qo'llash",
                    "Database.EnsureCreated() metodidan doim foydalanish",
                    "Har bir so'rovda avtomatik Migrate() chaqirish",
                    "Migratsiyalarni umuman ishlatmaslik, faqat qo'lda SQL yozish"
                },
                "Production'da migratsiyalar CI/CD pipeline orqali boshqarilishi kerak. dotnet ef migrations script bilan SQL generatsiya qilish yoki alohida migration step sifatida qo'llash xavfsizroq."),

            // 51
            CreateQuestion("Lazy Loading'ning asosiy xavfi nima?",
                new List<string> {
                    "Nazoratsiz holatda ko'plab qo'shimcha SQL so'rovlarni keltirib chiqarishi mumkin (N+1 muammosi)",
                    "U texnik jihatdan EF Core'da mavjud emas",
                    "U faqat Include() bilan birga ishlaydi",
                    "Faqat Fluent API orqali sozlanadi"
                },
                "Lazy Loading navigation property'ga birinchi marta kirish paytida avtomatik SQL so'rov yuboradi. Loop ichida ishlatilsa N+1 muammosiga olib keladi — 1 ta asosiy so'rov + N ta qo'shimcha so'rov."),

            // 52
            CreateQuestion("N+1 query muammosi nima?",
                new List<string> {
                    "Asosiy ro'yxat uchun 1 ta so'rov, so'ngra har bir element uchun alohida-alohida qo'shimcha N ta so'rov yuborilishi (odatda lazy loading yoki noto'g'ri Include natijasida)",
                    "Bitta so'rovda 1 ta ortiqcha ustun qaytarilishi",
                    "Faqat migratsiyalarga tegishli muammo",
                    "Faqat Raw SQL ishlatilganda yuzaga keladi"
                },
                "N+1 — 1 ta asosiy so'rov + har bir element uchun alohida N ta so'rov. Masalan, 100 ta buyurtma va ularning mahsulotlari uchun 1+100=101 ta SQL so'rov. Include() yoki Projection bilan hal qilinadi."),

            // 53
            CreateQuestion("EF Core'da IQueryable zanjiri qachon aslida SQL so'roviga aylanadi?",
                new List<string> {
                    "Natija haqiqatda materiallashtirilganda — masalan, ToList(), First(), foreach orqali iteratsiya qilinganda (deferred execution)",
                    "Where() chaqirilgan zahoti",
                    "DbContext yaratilgan zahoti",
                    "Faqat Include() chaqirilganda"
                },
                "IQueryable deferred execution ishlaydi — SQL so'rovi faqat natija materiallashtirilganda (ToList, First, Count, foreach) bazaga yuboriladi. Oldin faqat expression tree quriladi."),

            // 54
            CreateQuestion("EF Core'da SaveChanges() chaqiruvi standart holatda qanday tranzaksion xususiyatga ega?",
                new List<string> {
                    "Bitta SaveChanges() ichidagi barcha o'zgarishlar bitta implicit tranzaksiya sifatida atomik tarzda bajariladi",
                    "Har bir o'zgarish alohida-alohida commit qilinadi",
                    "Tranzaksiya umuman qo'llanilmaydi",
                    "Faqat BeginTransaction() chaqirilgandagina atomiklik ta'minlanadi"
                },
                "SaveChanges() barcha pending o'zgarishlarni bitta implicit tranzaksiya ichida bajaradi. Agar biror o'zgarish muvaffaqiyatsiz bo'lsa, barchasi rollback qilinadi (atomiklik)."),

            // 55
            CreateQuestion("Optimistic Concurrency uchun RowVersion/Concurrency Token qanday ishlaydi?",
                new List<string> {
                    "UPDATE so'rovi eski qiymatni WHERE shartida tekshiradi; agar mos kelmasa, DbUpdateConcurrencyException tashlanadi",
                    "Yozuvni bazada butunlay bloklaydi (lock)",
                    "Faqat SELECT so'rovlarida ishlatiladi",
                    "Faqat Fluent API'siz ishlaydi"
                },
                "Optimistic Concurrency yozuvni bloklamaydi. UPDATE paytida WHERE shartida RowVersion tekshiriladi — agar boshqa birov o'zgartirgan bo'lsa, qiymat mos kelmaydi va exception tashlanadi."),

            // 56
            CreateQuestion("Fluent API'ning Data Annotations'ga nisbatan afzalligi nima?",
                new List<string> {
                    "Entity klasslarini 'iflos' qilmasdan, murakkab konfiguratsiyalarni (masalan, composite key, shadow property) markazlashtirilgan holda belgilash imkonini beradi",
                    "Data Annotations umuman ishlamaydi",
                    "Fluent API faqat migratsiyalar uchun kerak",
                    "Ular orasida farq yo'q"
                },
                "Fluent API OnModelCreating ichida markazlashtirilgan konfiguratsiya beradi. Entity klasslari toza qoladi, murakkab scenariolar (composite key, TPH/TPT, shadow property) faqat Fluent API orqali amalga oshiriladi."),

            // 57
            CreateQuestion("Many-to-Many munosabat EF Core (5.0+) da qanday konfiguratsiya qilinishi mumkin?",
                new List<string> {
                    "Explicit join entity'siz, to'g'ridan-to'g'ri ikkita navigation property orqali (EF Core avtomatik join jadval yaratadi)",
                    "Faqat qo'lda join entity yaratish orqali, avtomatik usul yo'q",
                    "Many-to-Many EF Core'da umuman qo'llab-quvvatlanmaydi",
                    "Faqat Raw SQL orqali"
                },
                "EF Core 5.0+ da Many-to-Many skip navigation orqali qo'llab-quvvatlanadi — ikkita entity'da Collection navigation property qo'yilsa, EF Core avtomatik join jadval yaratadi."),

            // 58
            CreateQuestion("Global Query Filters (masalan, soft delete uchun) nima uchun foydali?",
                new List<string> {
                    "Har bir so'rovga avtomatik ravishda qo'shimcha WHERE shartini (masalan, IsDeleted == false) qo'llash orqali kodni takrorlashdan saqlaydi",
                    "Faqat migratsiyalarni tezlashtiradi",
                    "Faqat Include() bilan ishlaydi",
                    "Faqat write operatsiyalarga ta'sir qiladi"
                },
                "Global Query Filters OnModelCreating'da HasQueryFilter() orqali belgilanadi. Har bir LINQ so'rovga avtomatik WHERE sharti qo'shiladi — soft delete, multi-tenancy kabi scenariolar uchun ideal."),

            // 59
            CreateQuestion("EF Core orqali Stored Procedure chaqirishning tavsiya etilgan usuli qaysi?",
                new List<string> {
                    "FromSqlRaw/FromSqlInterpolated (query uchun) yoki ExecuteSqlRaw/ExecuteSqlInterpolated (buyruqlar uchun) metodlari orqali, parametrlarni SQL Injection'dan himoyalangan holda",
                    "Bu EF Core'da mumkin emas",
                    "Faqat DbSet.Add() orqali",
                    "Faqat migratsiya fayli ichida"
                },
                "FromSqlInterpolated so'rov natijalari uchun, ExecuteSqlInterpolated esa DML buyruqlar uchun ishlatiladi. String interpolation parametrlarni avtomatik SQL parametr sifatida uzatadi."),

            // 60
            CreateQuestion("Compiled Queries EF Core'da performance'ni qanday yaxshilaydi?",
                new List<string> {
                    "LINQ so'rovini SQL'ga tarjima qilish (query compilation) xarajatini kesh qilib, takroriy chaqiriladigan so'rovlar uchun bu jarayonni qayta bajarmaydi",
                    "Ular ma'lumotlar bazasi indekslarini avtomatik yaratadi",
                    "Ular faqat AsNoTracking() bilan ishlaydi",
                    "Ular tranzaksiyalarni tezlashtiradi"
                },
                "EF.CompileQuery() LINQ → SQL tarjima natijasini keshga saqlaydi. Hot path'dagi so'rovlar uchun har chaqiruvda qayta compile qilmaslik sezilarli performance yutug'i beradi."),

            // 61
            CreateQuestion("Katta hajmdagi ma'lumotlarni Bulk Insert/Update qilishda standart SaveChanges() nima uchun samarasiz bo'lishi mumkin?",
                new List<string> {
                    "SaveChanges() har bir o'zgarish uchun alohida SQL buyruq yaratishi mumkin va minglab yozuv uchun bu sekin bo'ladi — shu sababli maxsus bulk kutubxonalar (masalan, EFCore.BulkExtensions) qo'llaniladi",
                    "SaveChanges() umuman insert operatsiyasini qo'llab-quvvatlamaydi",
                    "SaveChanges() faqat async rejimda ishlaydi",
                    "Bunday muammo mavjud emas"
                },
                "SaveChanges() har bir entity uchun alohida INSERT/UPDATE SQL yaratadi. 10,000 ta yozuv uchun 10,000 ta round-trip — bu juda sekin. Bulk kutubxonalar bitta SQL buyruq bilan minglab yozuvni qayta ishlaydi."),

            // 62
            CreateQuestion("DbContext Pooling (AddDbContextPool) nima uchun ishlatiladi?",
                new List<string> {
                    "DbContext obyektlarini har safar yangidan yaratish va yo'q qilish xarajatini kamaytirish uchun, obyektlarni qayta ishlatish (reuse) orqali",
                    "Faqat test muhitida ishlatiladi",
                    "DbContext'ni Singleton qilib qo'yadi",
                    "Migratsiyalarni tezlashtirish uchun"
                },
                "AddDbContextPool<T>() DbContext instance'larini pool'da saqlaydi va qayta ishlatadi. Bu initialization xarajatini kamaytiradi, ayniqsa yuqori yuklanishli ilovalarda sezilarli performance yaxshilanishi beradi."),

            // 63
            CreateQuestion("Repository va Unit of Work pattern'larini EF Core ustiga qo'shimcha qatlam sifatida qo'llash haqida qaysi fikr ko'proq to'g'ri hisoblanadi?",
                new List<string> {
                    "DbContext allaqachon Unit of Work va DbSet<T> Repository pattern'larining o'zini namoyon etadi; qo'shimcha abstraksiya faqat test yoki data-access texnologiyasini almashtirish ehtimoli yuqori bo'lgan holatlarda qiymat beradi",
                    "Har doim majburiy, EF Core'siz loyihalar ishlamaydi",
                    "Bu pattern'lar faqat SQL Server bilan ishlaydi",
                    "Bu pattern'lar EF Core tomonidan taqiqlangan"
                },
                "DbContext = Unit of Work, DbSet<T> = Repository. Qo'shimcha abstraksiya qo'shish faqat testability yoki data-access texnologiyasini almashtirish ehtimoli yuqori bo'lganda oqlangan."),

            // 64
            CreateQuestion("AsSplitQuery() qaysi holatda AsSingleQuery() (standart) dan afzalroq bo'lishi mumkin?",
                new List<string> {
                    "Bir nechta 'one-to-many' Include() natijasida yuzaga keladigan cartesian explosion muammosini kamaytirish uchun",
                    "Har doim, chunki u har doim tezroq",
                    "Faqat bitta jadval bilan ishlaganda",
                    "Faqat migratsiyalar uchun"
                },
                "AsSplitQuery() bir nechta Include() bo'lganda single query'dagi cartesian product o'rniga alohida SQL so'rovlar yuboradi. Bu ma'lumot dublikatlanishini va result set hajmini kamaytiradi."),

            // 65
            CreateQuestion("Soft delete uchun Global Query Filter qo'llanganda, ba'zan o'chirilgan yozuvlarni ham ko'rish kerak bo'lsa nima qilish kerak?",
                new List<string> {
                    "IgnoreQueryFilters() metodidan foydalanish mumkin",
                    "Bu imkonsiz, filter doim majburiy qo'llanadi",
                    "DbContext'ni butunlay qayta yaratish kerak",
                    "Faqat Raw SQL orqali"
                },
                "IgnoreQueryFilters() metodi Global Query Filters'ni vaqtincha o'chirib, barcha yozuvlarni (jumladan, soft-deleted) ko'rish imkonini beradi. Admin panel yoki audit log ssenariylarda foydali.")
        };
    }

    // ==========================================
    // Bo'lim 5: Logging, Monitoring va Tracing (66-75)
    // ==========================================
    private static List<Question> GenerateSeniorLoggingQuestions()
    {
        return new List<Question>
        {
            // 66
            CreateQuestion("ILogger<T> interfeysida generic <T> parametrining vazifasi nima?",
                new List<string> {
                    "Log yozuvlariga avtomatik ravishda 'category name' (odatda to'liq class nomi) qo'shib, log manbasini aniqlashni osonlashtiradi",
                    "Hech qanday vazifasi yo'q, faqat sintaktik talab",
                    "Faqat log darajasini belgilaydi",
                    "Faqat exception turlarini filtrlaydi"
                },
                "ILogger<T> da T turi log category name sifatida ishlatiladi (odatda to'liq class nomi). Bu log yozuvlarini filtrlash va qaysi komponentdan kelganini aniqlashni osonlashtiradi."),

            // 67
            CreateQuestion("Structured Logging oddiy matnli (string interpolation asosidagi) logdan nimasi bilan farq qiladi?",
                new List<string> {
                    "Log xabari va uning parametrlari alohida maydonlar sifatida saqlanadi, bu keyinchalik log'larni query/filter qilish va tahlil qilishni ancha osonlashtiradi",
                    "Farqi yo'q, ikkalasi bir xil natija beradi",
                    "Structured Logging faqat Serilog'da mavjud",
                    "Structured Logging faqat error darajasidagi loglar uchun ishlatiladi"
                },
                "Structured Logging log.LogInformation('User {UserId} logged in', userId) shaklida parametrlarni alohida maydon sifatida saqlaydi. Bu Seq, Elasticsearch kabi tizimlarda query/filter qilishni juda osonlashtiradi."),

            // 68
            CreateQuestion("Serilog'ni ASP.NET Core'ga integratsiya qilishning standart usuli qaysi?",
                new List<string> {
                    "UseSerilog() metodini WebApplicationBuilder/Host konfiguratsiyasiga ulash va Sink'larni (masalan, Console, File, Seq, Elasticsearch) sozlash orqali",
                    "Faqat Console.WriteLine() orqali",
                    "Serilog ASP.NET Core bilan mos kelmaydi",
                    "Faqat appsettings.json fayli o'zgartirilsa yetarli, kod yozish shart emas"
                },
                "Serilog UseSerilog() extension metodi orqali ASP.NET Core host'ga ulanadi. Sink'lar (Console, File, Seq, Elasticsearch) konfiguratsiya qilinib, structured logging imkoniyati qo'shiladi."),

            // 69
            CreateQuestion("Log darajalari orasida qaysi tartib to'g'ri (pastdan yuqoriga jiddiylik bo'yicha)?",
                new List<string> {
                    "Trace → Debug → Information → Warning → Error → Critical",
                    "Critical → Error → Warning → Information → Debug → Trace",
                    "Debug → Trace → Error → Warning → Information → Critical",
                    "Information → Trace → Debug → Warning → Critical → Error"
                },
                "Log darajalari jiddiylik bo'yicha: Trace (eng past) → Debug → Information → Warning → Error → Critical (eng yuqori). MinimumLevel sozlashi bu darajadan pastroqlarni filtrlaydi."),

            // 70
            CreateQuestion("Correlation ID nima uchun ishlatiladi?",
                new List<string> {
                    "Bitta so'rovni turli servis va komponentlar bo'ylab kuzatish va bog'liq log yozuvlarini birlashtirish uchun noyob identifikator sifatida",
                    "Faqat foydalanuvchi autentifikatsiyasi uchun",
                    "Faqat ma'lumotlar bazasi tranzaksiyalari uchun",
                    "Faqat keshlashni boshqarish uchun"
                },
                "Correlation ID bitta so'rov yoki operatsiyaga tegishli barcha log yozuvlarini birlashtirish uchun noyob identifikator. Mikroservislar orasida so'rovni end-to-end kuzatish imkonini beradi."),

            // 71
            CreateQuestion("OpenTelemetry nimani birlashtiradi?",
                new List<string> {
                    "Tracing, Metrics va Logging (observability'ning uchta asosiy komponentini) uchun yagona, vendor-neutral standart va API/SDK to'plamini",
                    "Faqat logging",
                    "Faqat CI/CD pipeline'larni",
                    "Faqat Kubernetes klasterlarini boshqarishni"
                },
                "OpenTelemetry observability uchun yagona standart — Traces (so'rov yo'li), Metrics (raqamli ko'rsatkichlar) va Logs ni vendor-neutral API/SDK orqali to'playdi va eksport qiladi."),

            // 72
            CreateQuestion("Distributed Tracing (masalan, Jaeger/Zipkin) mikroservis arxitekturasida nima uchun muhim?",
                new List<string> {
                    "Bitta foydalanuvchi so'rovi bir nechta servis orqali o'tganda, har bir bosqichdagi kechikish va xatoliklarni vizual tarzda kuzatish va tezkor aniqlash imkonini beradi",
                    "Faqat UI dizayni uchun kerak",
                    "Faqat ma'lumotlar bazasi zaxira nusxasini olish uchun",
                    "U faqat monolit ilovalar uchun mo'ljallangan"
                },
                "Distributed Tracing bitta so'rovning turli mikroservislar orqali o'tish yo'lini, har bir bosqichdagi latency va xatoliklarni vizual trace sifatida ko'rsatadi. Bottleneck va failure aniqlashni osonlashtiradi."),

            // 73
            CreateQuestion("Application Insights yoki Prometheus/Grafana kabi vositalar asosan nima uchun ishlatiladi?",
                new List<string> {
                    "Ilovaning real vaqtdagi metrikalari (so'rov soni, javob vaqti, xatolik darajasi va h.k.)ni yig'ish, saqlash va vizualizatsiya qilish uchun",
                    "Faqat kod yozish tezligini oshirish uchun",
                    "Faqat unit testlarni ishga tushirish uchun",
                    "Faqat SQL so'rovlarni optimallashtirish uchun"
                },
                "Monitoring vositalari ilovaning runtime metrikalarini (request rate, response time, error rate, CPU/Memory) to'playdi, saqlaydi va dashboard orqali vizualizatsiya qiladi."),

            // 74
            CreateQuestion("Sensitive ma'lumotlarni (parol, karta raqami) log'larda saqlashdan qanday himoyalanish kerak?",
                new List<string> {
                    "Log yozuvlarida bunday ma'lumotlarni maskirovka qilish yoki umuman log qilmaslik, structured logging'da maxsus scrubbing/filtering mexanizmlaridan foydalanish",
                    "Ularni hech qanday cheklovsiz to'liq log qilish kerak, chunki debugging uchun foydali",
                    "Faqat production muhitida bu masala muhim, development'da ahamiyati yo'q",
                    "Bu masala faqat frontend'ga tegishli"
                },
                "Sensitive data log'larga yozilmasligi kerak. Structured logging'da Destructure.ByTransforming yoki custom enricher/filter orqali sensitive maydonlarni maskirovka qilish yoki to'liq exclude qilish kerak."),

            // 75
            CreateQuestion("Request duration va error rate kabi metrikalar odatda qanday yig'iladi?",
                new List<string> {
                    "Middleware yoki instrumentation kutubxonalari (masalan, OpenTelemetry Metrics) yordamida avtomatik ravishda to'planadi va monitoring tizimiga (Prometheus va h.k.) yuboriladi",
                    "Faqat qo'lda, log fayllarini ko'zdan kechirish orqali",
                    "Faqat SQL Server Profiler orqali",
                    "Bunday metrikalarni yig'ish ASP.NET Core'da mumkin emas"
                },
                "Metrikalar middleware (masalan, UseHttpMetrics) yoki OpenTelemetry SDK orqali avtomatik yig'iladi va Prometheus, Application Insights kabi monitoring tizimlariga yuboriladi.")
        };
    }

    // ==========================================
    // Bo'lim 6: Xavfsizlik (76-85)
    // ==========================================
    private static List<Question> GenerateSeniorSecurityQuestions()
    {
        return new List<Question>
        {
            // 76
            CreateQuestion("Authentication va Authorization o'rtasidagi farq nima?",
                new List<string> {
                    "Authentication — foydalanuvchi kimligini tasdiqlash, Authorization — tasdiqlangan foydalanuvchiga qanday amallarga ruxsat berilishini aniqlash",
                    "Ular bir xil tushuncha",
                    "Authorization faqat parol tekshiradi",
                    "Authentication faqat rol asosida ishlaydi"
                },
                "Authentication (AuthN) — 'Sen kimsan?' savoliga javob (login, JWT). Authorization (AuthZ) — 'Sening nima qilishga huquqing bor?' savoliga javob (rol, policy, claim)."),

            // 77
            CreateQuestion("JWT tokenining uchta asosiy qismi qaysilar?",
                new List<string> {
                    "Header, Payload, Signature",
                    "Username, Password, Salt",
                    "Header, Body, Footer",
                    "Key, Value, Hash"
                },
                "JWT uchta qismdan iborat: Header (algoritm va token turi), Payload (claims — foydalanuvchi ma'lumotlari), Signature (header+payload'ni secret key bilan imzolash natijasi)."),

            // 78
            CreateQuestion("Refresh Token nima uchun ishlatiladi?",
                new List<string> {
                    "Qisqa umrli access token muddati tugaganda, foydalanuvchini qayta login qildirmasdan yangi access token olish imkonini beradi",
                    "Access token'ning o'zini almashtiradi va shart emas",
                    "Faqat parolni eslab qolish uchun",
                    "Faqat administrator huquqlarini berish uchun"
                },
                "Refresh Token uzoq umrli token bo'lib, access token muddati tugaganda yangi access token olish uchun ishlatiladi. Bu foydalanuvchini qayta login qildirmasdan xavfsizlikni ta'minlaydi."),

            // 79
            CreateQuestion("OAuth2 va OpenID Connect o'rtasidagi asosiy farq nima?",
                new List<string> {
                    "OAuth2 — avtorizatsiya (resurslarga kirish huquqini berish) uchun protokol, OpenID Connect esa OAuth2 ustiga qurilgan va autentifikatsiya (identifikatsiya) qatlamini qo'shadi",
                    "Ular bir xil protokol, faqat nomi boshqacha",
                    "OpenID Connect faqat mobil ilovalar uchun",
                    "OAuth2 faqat Google tomonidan ishlatiladi"
                },
                "OAuth2 — avtorizatsiya framework'i (resurslarga kirish). OpenID Connect (OIDC) — OAuth2 ustiga qurilgan autentifikatsiya qatlami, ID Token va UserInfo endpoint qo'shadi."),

            // 80
            CreateQuestion("Role-based va Policy-based authorization o'rtasidagi farq nima?",
                new List<string> {
                    "Role-based faqat foydalanuvchi rollariga asoslanadi, Policy-based esa moslashuvchan, murakkab shartlar (masalan, yosh, claim kombinatsiyasi) asosida avtorizatsiya qoidalarini belgilash imkonini beradi",
                    "Farqi yo'q",
                    "Policy-based faqat administratorlar uchun ishlaydi",
                    "Role-based faqat API'larda, Policy-based faqat MVC'da ishlaydi"
                },
                "Role-based oddiy rol tekshiruvi ([Authorize(Roles='Admin')]). Policy-based esa murakkab shartlar — claim kombinatsiyasi, custom requirement handler'lar orqali moslashuvchan avtorizatsiya qoidalarini belgilaydi."),

            // 81
            CreateQuestion("ASP.NET Core'da CSRF himoyasi qanday amalga oshiriladi?",
                new List<string> {
                    "Anti-forgery token (ValidateAntiForgeryToken, [AutoValidateAntiforgeryToken]) mexanizmi orqali, forma bilan birga yuboriladigan noyob tokenni tekshirish",
                    "Faqat HTTPS ishlatish orqali",
                    "CSRF himoyasi ASP.NET Core'da avtomatik va sozlashsiz ishlaydi, hech narsa qilish shart emas",
                    "Faqat CORS sozlamalari orqali"
                },
                "CSRF himoyasi anti-forgery token orqali amalga oshiriladi. Server har bir forma bilan noyob token yuboradi va POST so'rovda shu tokenni tekshiradi. Bu boshqa saytdan soxta so'rov yuborishni oldini oladi."),

            // 82
            CreateQuestion("XSS hujumlaridan himoyalanishning asosiy usuli qaysi?",
                new List<string> {
                    "Foydalanuvchidan kelgan ma'lumotlarni chiqishda (output) to'g'ri encode qilish (Razor avtomatik HTML encoding qiladi) va Content Security Policy qo'llash",
                    "Faqat parolni murakkab qilish",
                    "Faqat HTTPS ishlatish",
                    "XSS faqat backend'ga tegishli, frontend bilan bog'liq emas"
                },
                "XSS himoyasining asosiy usuli — output encoding. Razor avtomatik HTML encode qiladi. Bundan tashqari CSP header, input sanitization va HttpOnly cookies qo'shimcha himoya qatlamlari."),

            // 83
            CreateQuestion("EF Core SQL Injection'dan qanday himoya qiladi?",
                new List<string> {
                    "LINQ so'rovlari va parametrlashtirilgan so'rovlar (FromSqlInterpolated kabi) orqali kiritilgan qiymatlarni SQL kodi sifatida emas, balki parametr sifatida uzatadi",
                    "U hech qanday himoya bermaydi, dasturchi qo'lda tekshirishi kerak",
                    "Faqat Stored Procedure ishlatilganda himoya beradi",
                    "Faqat AsNoTracking() bilan birga ishlaganda"
                },
                "EF Core LINQ so'rovlarini parametrlashtirilgan SQL'ga aylantiradi. FromSqlInterpolated string interpolation parametrlarini SQL parametr sifatida uzatadi, bu SQL injection'ni oldini oladi."),

            // 84
            CreateQuestion("Parollarni xavfsiz saqlashda nima uchun oddiy hash (masalan, MD5) yetarli emas?",
                new List<string> {
                    "MD5 kabi tez algoritmlar brute-force va rainbow table hujumlariga zaif, shu sababli maxsus, ataylab sekin va 'salt'langan algoritmlar (BCrypt, Argon2, PBKDF2) ishlatiladi",
                    "MD5 juda sekin ishlaydi",
                    "MD5 umuman parollarni hash qila olmaydi",
                    "Bu masala faqat eski tizimlarga tegishli"
                },
                "MD5 juda tez — brute-force hujumchi soniyasiga milliardlab hash sinab ko'rishi mumkin. BCrypt, Argon2, PBKDF2 ataylab sekin va salt'langan bo'lib, hujumni amaliy jihatdan imkonsiz qiladi."),

            // 85
            CreateQuestion("HSTS (HTTP Strict Transport Security) nima uchun ishlatiladi?",
                new List<string> {
                    "Brauzerga saytga faqat HTTPS orqali murojaat qilishni majburlash, HTTP'ga tushib qolish (downgrade) hujumlaridan himoyalanish uchun",
                    "Faqat SEO uchun",
                    "Faqat sertifikatni avtomatik yangilash uchun",
                    "HSTS faqat API'larda ishlatiladi, veb-saytlarda emas"
                },
                "HSTS Strict-Transport-Security header orqali brauzerga faqat HTTPS ishlatishni buyuradi. Bu man-in-the-middle va HTTP downgrade hujumlaridan himoyalaydi.")
        };
    }

    // ==========================================
    // Bo'lim 7: Arxitektura va Dizayn Pattern'lari (86-95)
    // ==========================================
    private static List<Question> GenerateSeniorArchitectureQuestions()
    {
        return new List<Question>
        {
            // 86
            CreateQuestion("Clean Architecture (Onion Architecture)ning asosiy g'oyasi nima?",
                new List<string> {
                    "Bog'liqliklar (dependencies) tashqi qatlamlardan ichki qatlamlarga (Domain'ga) qarab yo'nalgan bo'lishi, Domain qatlami hech qanday tashqi texnologiyaga (DB, UI) bog'liq bo'lmasligi kerak",
                    "Barcha kodni bitta loyihada saqlash",
                    "Faqat mikroservislar uchun mo'ljallangan",
                    "Barcha logikani Controller ichida yozish"
                },
                "Clean Architecture'da bog'liqliklar ichkariga qarab yo'nalgan — Domain qatlami markazda va hech qanday tashqi texnologiyaga bog'liq emas. Infrastructure va UI tashqi qatlamlarda joylashadi."),

            // 87
            CreateQuestion("CQRS (Command Query Responsibility Segregation) nima uchun qo'llaniladi?",
                new List<string> {
                    "O'qish (query) va yozish (command) operatsiyalarini alohida modellar/yo'llar orqali ajratish, bu murakkab domenlarda moslashuvchanlik va masshtablanishni oshiradi",
                    "Faqat ma'lumotlar bazasini zaxiralash uchun",
                    "CQRS faqat mikroservislarda ishlatiladi, monolit'da foydasi yo'q",
                    "Faqat UI dizayni bilan bog'liq"
                },
                "CQRS o'qish va yozish operatsiyalarini alohida model va yo'llar orqali ajratadi. Bu har birini mustaqil optimize qilish, alohida DB ishlatish va murakkab domen logikasini soddalashtirish imkonini beradi."),

            // 88
            CreateQuestion("MediatR kutubxonasi CQRS'ni amalga oshirishda qanday rol o'ynaydi?",
                new List<string> {
                    "Command va Query'larni handler'larga yo'naltiruvchi (mediator pattern) vosita bo'lib, Controller'lar va biznes logika o'rtasidagi to'g'ridan-to'g'ri bog'liqlikni kamaytiradi",
                    "Ma'lumotlar bazasi migratsiyasini boshqaradi",
                    "Faqat logging uchun ishlatiladi",
                    "Faqat authentication uchun ishlatiladi"
                },
                "MediatR mediator pattern'ni amalga oshiradi — Controller IMediator.Send() chaqiradi, MediatR esa tegishli handler'ni topib, bajaradi. Bu loose coupling va SRP'ni ta'minlaydi."),

            // 89
            CreateQuestion("EF Core ustiga qo'shimcha Repository/Unit of Work pattern qo'shishning eng ko'p tanqid qilinadigan tomoni nima?",
                new List<string> {
                    "DbContext allaqachon shu funksiyalarni bajaradi, shuning uchun qo'shimcha abstraksiya ba'zan ortiqcha murakkablik (over-engineering) va EF Core'ning kuchli tomonlarini cheklashi mumkin",
                    "Bu hech qachon tanqid qilinmaydi",
                    "U faqat NoSQL bazalar bilan ishlaydi",
                    "U DI bilan mutlaqo mos kelmaydi"
                },
                "DbContext = Unit of Work, DbSet = Repository. Qo'shimcha abstraksiya IQueryable moslashuvchanligini cheklashi, leaky abstraction yaratishi va ortiqcha boilerplate kod keltirib chiqarishi mumkin."),

            // 90
            CreateQuestion("Single Responsibility Principle (SRP)ga real misol qaysi?",
                new List<string> {
                    "OrderService faqat buyurtma bilan bog'liq biznes logikani bajaradi, email yuborish EmailService'ga, hisobotlash boshqa servisga ajratiladi",
                    "Bitta klass ham ma'lumotlarni saqlash, ham email yuborish, ham hisobotlash logikasini bajarishi",
                    "Barcha logikani bitta 'Utils' klassiga joylash",
                    "SRP faqat interfeyslarga tegishli, klasslarga emas"
                },
                "SRP — har bir klass/modul faqat bitta sababga ko'ra o'zgarishi kerak. OrderService faqat buyurtma logikasi, EmailService faqat email, ReportService faqat hisobot bilan shug'ullanadi."),

            // 91
            CreateQuestion("Mikroservis arxitekturasi monolitga nisbatan qaysi holatda ko'proq oqlanadi?",
                new List<string> {
                    "Katta, murakkab tizim bo'lib, turli qismlar mustaqil masshtablanishi, alohida deploy qilinishi va turli jamoalar tomonidan mustaqil rivojlantirilishi kerak bo'lganda",
                    "Kichik jamoa, kichik loyiha va tez MVP kerak bo'lganda",
                    "Faqat startaplar uchun, katta kompaniyalar uchun mos emas",
                    "Har doim monolitdan afzal, hech qanday kamchiligi yo'q"
                },
                "Mikroservislar katta jamoalar, murakkab domen va mustaqil masshtablanish kerak bo'lganda oqlanadi. Kichik loyihalar uchun monolit soddaroq va tezroq rivojlantiriladi."),

            // 92
            CreateQuestion("RabbitMQ yoki Kafka kabi Message Broker'larning asosiy vazifasi nima?",
                new List<string> {
                    "Servislar o'rtasida asinxron, decoupled (bog'liqligi kamaytirilgan) xabar almashinuvini ta'minlash, bu orqali tizim chidamliligi va masshtablanishini oshirish",
                    "Faqat ma'lumotlar bazasi sifatida ishlatiladi",
                    "Faqat frontend va backend o'rtasidagi aloqa uchun",
                    "Faqat logging uchun ishlatiladi"
                },
                "Message Broker'lar servislar o'rtasida asinxron, loose-coupled xabar almashishni ta'minlaydi. Bu tizim barqarorligi, masshtablanishi va servislar mustaqilligini oshiradi."),

            // 93
            CreateQuestion("Saga Pattern distributed tranzaksiyalarni qanday boshqaradi?",
                new List<string> {
                    "Uzoq davom etadigan tranzaksiyani bir qator lokal tranzaksiyalarga bo'ladi, har biridan keyin kompensatsion amal orqali xatolik yuz berganda oldingi holatga qaytarish imkonini beradi",
                    "Bitta global lock orqali barcha servislarni bloklaydi",
                    "Faqat monolit ilovalarda ishlatiladi",
                    "Saga faqat o'qish operatsiyalari uchun mo'ljallangan"
                },
                "Saga Pattern distributed tranzaksiyani lokal tranzaksiyalar ketma-ketligiga bo'ladi. Har bir bosqichda muvaffaqiyatsizlik bo'lsa, compensating transaction chaqirilib, oldingi holatga qaytariladi."),

            // 94
            CreateQuestion("Polly kutubxonasi orqali amalga oshiriladigan Circuit Breaker pattern'ining maqsadi nima?",
                new List<string> {
                    "Doimiy muvaffaqiyatsiz bo'layotgan tashqi chaqiruvlarni vaqtincha to'xtatib, tizimni keskin yuklanishdan va 'cascading failure'dan himoyalash",
                    "Ma'lumotlar bazasini tezlashtirish",
                    "Faqat logging formatini o'zgartirish",
                    "Faqat unit test yozish uchun mo'ljallangan"
                },
                "Circuit Breaker ketma-ket xatoliklarni kuzatadi va ma'lum chegaraga yetganda chaqiruvlarni vaqtincha to'xtatadi (Open state). Bu tizimni cascading failure'dan himoyalaydi va tashqi servisga vaqt beradi."),

            // 95
            CreateQuestion("Domain-Driven Design'dagi 'Aggregate' tushunchasi nimani anglatadi?",
                new List<string> {
                    "Bir-biri bilan bog'liq entity va value object'lardan tashkil topgan, yagona 'aggregate root' orqali boshqariladigan va izchillik (consistency) chegarasini belgilaydigan klaster",
                    "Faqat ma'lumotlar bazasidagi jadval",
                    "Faqat UI komponentlari to'plami",
                    "Faqat DTO'larning yig'indisi"
                },
                "Aggregate — bir-biri bilan bog'liq entity va value object'larning klasteri. Aggregate Root orqali boshqariladi va tranzaksion izchillik chegarasini belgilaydi. Tashqi kod faqat root orqali murojaat qiladi.")
        };
    }

    // ==========================================
    // Bo'lim 8: Testing, DevOps va Boshqa Mavzular (96-100)
    // ==========================================
    private static List<Question> GenerateSeniorTestingDevOpsQuestions()
    {
        return new List<Question>
        {
            // 96
            CreateQuestion("Unit Test va Integration Test o'rtasidagi asosiy farq nima?",
                new List<string> {
                    "Unit Test alohida komponentni tashqi bog'liqliklardan izolyatsiya qilingan holda (mock'lar yordamida) tekshiradi, Integration Test esa bir nechta komponent birgalikda qanday ishlashini tekshiradi",
                    "Ular bir xil, faqat nomi boshqacha",
                    "Integration Test faqat frontend uchun",
                    "Unit Test faqat production muhitida ishga tushiriladi"
                },
                "Unit Test bitta komponentni izolyatsiyada (mock/stub bilan) tekshiradi — tez va aniq. Integration Test bir nechta komponentni (DB, API) haqiqiy muhitda birgalikda tekshiradi."),

            // 97
            CreateQuestion("WebApplicationFactory<T> sinfi nima uchun ishlatiladi?",
                new List<string> {
                    "ASP.NET Core ilovasini test uchun in-memory server sifatida ishga tushirish va integration test yozishni osonlashtirish uchun",
                    "Faqat unit test uchun mock obyekt yaratish",
                    "Faqat production konfiguratsiyasini boshqarish uchun",
                    "Faqat Docker konteynerlarini boshqarish uchun"
                },
                "WebApplicationFactory<T> ASP.NET Core ilovasini in-memory test server sifatida ishga tushiradi. HttpClient yaratib, real HTTP so'rovlar yuborish va javoblarni tekshirish imkonini beradi."),

            // 98
            CreateQuestion("CI/CD pipeline'da 'CI' (Continuous Integration) bosqichi odatda nimalarni o'z ichiga oladi?",
                new List<string> {
                    "Kodni build qilish, avtomatik testlarni ishga tushirish va kod sifatini tekshirish — o'zgarishlar asosiy branch'ga integratsiya qilinishidan oldin",
                    "Faqat production serverga deploy qilish",
                    "Faqat monitoring sozlash",
                    "Faqat ma'lumotlar bazasi zaxira nusxasini olish"
                },
                "CI — har bir code push'da avtomatik build, test va kod sifat tekshiruvi. Bu xatolarni erta aniqlash, regression'ni oldini olish va jamoaviy ishni tezlashtirishni ta'minlaydi."),

            // 99
            CreateQuestion("Dockerfile'da multi-stage build ishlatishning asosiy afzalligi nima?",
                new List<string> {
                    "Build uchun kerakli og'ir vositalar (SDK) va production uchun kerakli yengil runtime'ni ajratib, yakuniy image hajmini sezilarli kamaytiradi va xavfsizlikni oshiradi",
                    "Faqat build vaqtini tezlashtiradi, boshqa foydasi yo'q",
                    "Multi-stage build faqat .NET Framework uchun ishlaydi",
                    "U faqat Linux konteynerlarida ishlaydi"
                },
                "Multi-stage build birinchi stage'da SDK bilan build/publish qiladi, ikkinchi stage'da faqat runtime bilan yakuniy image yaratadi. Bu image hajmini 5-10 marta kamaytiradi va attack surface'ni kichraytiradi."),

            // 100
            CreateQuestion("Trunk-Based Development strategiyasining Git Flow'dan asosiy farqi nima?",
                new List<string> {
                    "Trunk-Based Development'da dasturchilar kichik, tez-tez o'zgarishlarni to'g'ridan-to'g'ri asosiy branch'ga integratsiya qiladi, Git Flow esa uzoqroq umr ko'radigan alohida branch'lar tuzilmasiga tayanadi",
                    "Farqi yo'q, ikkalasi bir xil jarayon",
                    "Trunk-Based Development faqat kichik loyihalar uchun, Git Flow esa faqat katta loyihalar uchun",
                    "Git Flow versiya nazorati tizimi emas"
                },
                "Trunk-Based Development — kichik, tez-tez commit'lar to'g'ridan-to'g'ri main branch'ga (yoki qisqa umrli feature branch orqali). Git Flow — uzoq umrli develop, release, feature branch'lar. TBD CI/CD bilan yaxshi integratsiya qiladi.")
        };
    }
}
