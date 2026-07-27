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
                "Dependency Injection lifetimes, Action Filters, JWT Auth, Custom Middleware va Rate Limiting bo'yicha 30 ta o'rtacha darajadagi test.",
                "Medium",
                "layers",
                GenerateDotNetMediumQuestions()
            ),
            CreateQuiz(
                "ASP.NET Core High-Performance & Principal Architecture",
                "dotnet",
                "C# & .NET Core",
                "gRPC, SignalR, Distributed Caching, Circuit Breaker, YARP va Performance Tuning bo'yicha 30 ta qiyin darajadagi test.",
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
            CreateQuestion("ASP.NET Core qanday turdagi freymvork?",
                new List<string> { "Cross-platform (Windows, Linux, macOS)", "Faqat Windows uchun", "Faqat mobil ilovalar uchun", "Faqat desktop ilovalar uchun" },
                "ASP.NET Core - ko'p platformali (Windows, Linux, macOS) ochiq manbali zamonaviy web freymvorkdir."),

            CreateQuestion("ASP.NET Core loyihasida ilova ishga tushish nuqtasi (entry point) qaysi fayl hisoblanadi?",
                new List<string> { "Program.cs", "Startup.cs", "appsettings.json", "web.config" },
                "Program.cs fayli .NET 6+ ilovalarida `Main` metodiga va WebApplication builder-ga ega asosiy kirish faylidir."),

            CreateQuestion("ASP.NET Core'da default o'rnatilgan web server nomi nima?",
                new List<string> { "Kestrel", "IIS Express", "Apache", "Nginx" },
                "Kestrel — ASP.NET Core ilovalari uchun standart va yuqori unumdorlikka ega ko'p platformali web serverdir."),

            CreateQuestion("Middleware nima uchun ishlatiladi?",
                new List<string> { "HTTP so'rov va javob pipeline'ini boshqarish uchun", "Faqat ma'lumotlar bazasiga ulanish uchun", "Faqat frontend kodini render qilish uchun", "Faqat loglarni o'chirish uchun" },
                "Middleware so'rovlar quvurida (pipeline) HTTP so'rovlarini qabul qilib, ularni qayta ishlash va javob shakllantirish uchun xizmat qiladi."),

            CreateQuestion("Middleware'lar qanday tartibda ishlaydi?",
                new List<string> { "Ro'yxatga qo'shilish tartibida (ketma-ket)", "Tasodifiy tartibda", "Alifbo tartibida", "Faqat bittasi ishlaydi" },
                "Middleware-lar Program.cs faylida ro'yxatga olingan ketma-ketligi bo'yicha so'rov quvurida birma-bir chaqiriladi."),

            CreateQuestion("Controllerni belgilash uchun qaysi atribut ishlatiladi?",
                new List<string> { "[ApiController]", "[HttpController]", "[WebController]", "[RestController]" },
                "[ApiController] atributi controller klasslariga avtomatik validation va binding qulayliklarini beradi."),

            CreateQuestion("GET so'rovini qabul qiluvchi action metodni belgilash uchun qaysi atribut ishlatiladi?",
                new List<string> { "[HttpGet]", "[FromGet]", "[GetMethod]", "[ActionGet]" },
                "[HttpGet] atributi HTTP GET so'rovlarini muayyan action metodga marshrutlaydi."),

            CreateQuestion("IActionResult interfeysi nima uchun ishlatiladi?",
                new List<string> { "HTTP javobining turini qaytarish uchun (Ok, NotFound, BadRequest va h.k.)", "Ma'lumotlar bazasi bilan ishlash uchun", "Middleware yaratish uchun", "Konfiguratsiya o'qish uchun" },
                "IActionResult HTTP status kodlari va ma'lumotlarni mos moslashuvchan formatda (200 OK, 404 NotFound) qaytaradi."),

            CreateQuestion("appsettings.json faylining vazifasi nima?",
                new List<string> { "Ilova konfiguratsiyasini (masalan, connection string) saqlash", "Ilovaning static fayllarini saqlash", "NuGet paketlarini ro'yxatlash", "Route'larni belgilash" },
                "appsettings.json loyiha sozlamalari va maxfiy bo'lmagan konfiguratsiyalarni saqlash uchun mo'ljallangan."),

            CreateQuestion("Dependency Injection (DI) da 'Transient' lifetime nimani anglatadi?",
                new List<string> { "Har safar so'ralganda yangi obyekt yaratiladi", "Har bir so'rov uchun bitta obyekt yaratiladi", "Butun ilova davomida bitta obyekt ishlatiladi", "Faqat konfiguratsiya vaqtida yaratiladi" },
                "Transient lifetime har safar servis inject qilinganda mutlaqo yangi obyekt namunasini beradi."),

            CreateQuestion("'Scoped' lifetime qachon yangi obyekt yaratadi?",
                new List<string> { "Har HTTP so'rov uchun bitta marta", "Har chaqiruvda", "Ilova ishga tushganda faqat bir marta", "Hech qachon yaratmaydi" },
                "Scoped lifetime bitta HTTP so'rovi (request) doirasida yagona bitta obyekt saqlaydi va so'rov tugagach o'chiriladi."),

            CreateQuestion("'Singleton' lifetime nimani anglatadi?",
                new List<string> { "Butun ilova hayoti davomida bitta obyekt", "Har so'rovda yangi obyekt", "Faqat test muhitida ishlaydi", "Har controller uchun alohida obyekt" },
                "Singleton lifetime ilova ishga tushgandan to'xtaguncha yagona yagona obyekt instance-ini saqlaydi."),

            CreateQuestion("wwwroot papkasi nima uchun ishlatiladi?",
                new List<string> { "Static fayllarni (CSS, JS, rasm) saqlash uchun", "Controllerlarni saqlash uchun", "Ma'lumotlar bazasi migratsiyalarini saqlash uchun", "Loglarni saqlash uchun" },
                "wwwroot papkasi brauzerga ochiq bo'lgan statik fayllarni (images, css, js) saqlash uchun xizmat qiladi."),

            CreateQuestion("Swagger nima uchun ishlatiladi?",
                new List<string> { "API dokumentatsiyasini avtomatik generatsiya qilish va test qilish uchun", "Ma'lumotlar bazasini boshqarish uchun", "Frontend UI yaratish uchun", "Loyihani deploy qilish uchun" },
                "Swagger (OpenAPI) Web API endpoint-larini hujjatlashtirish va interaktiv test qilish imkonini beradi."),

            CreateQuestion("HTTP POST metodi odatda nima uchun ishlatiladi?",
                new List<string> { "Yangi resurs yaratish uchun", "Ma'lumotni o'chirish uchun", "Faqat ma'lumot olish uchun", "Serverni qayta ishga tushirish uchun" },
                "HTTP POST metodi serverda yangi resurs yoki obyekt yaratish uchun ishlatiladi."),

            CreateQuestion("HTTP DELETE metodi qaysi maqsadda ishlatiladi?",
                new List<string> { "Resursni o'chirish", "Yangi obyekt yaratish", "Ma'lumotni yangilash", "Faylni yuklab olish" },
                "HTTP DELETE metodi muayyan ID ga ega resursni bazadan o'chirish uchun ishlatiladi."),

            CreateQuestion("ActionResult<T> qaysi holatda foydali?",
                new List<string> { "Aniq tur (T) va turli HTTP javoblarini birga qaytarishda", "Faqat statik fayllar uchun", "Faqat void metodlar uchun", "Middleware yozishda" },
                "ActionResult<T> strongly-typed `T` ob'ektini yoki HTTP status kodi (404/400) ni birgalikda qaytarish imkonini beradi."),

            CreateQuestion("Route parametri qanday belgilanadi?",
                new List<string> { "{id} shaklida route shablonida", "[id] qavs ichida", "$id belgisi bilan", "#id belgisi bilan" },
                "Route parametrlari jingalak qavslar `{id}` shaklida atribut yo'lida belgilab olinadi."),

            CreateQuestion("200 status kodi nimani bildiradi?",
                new List<string> { "So'rov muvaffaqiyatli bajarildi", "Xatolik yuz berdi", "Resurs topilmadi", "Ruxsat berilmagan" },
                "200 OK — HTTP so'rovining muvaffaqiyatli yakunlanganligini bildiruvchi standart javob kodidir."),

            CreateQuestion("404 status kodi nimani anglatadi?",
                new List<string> { "Resurs topilmadi", "Server xatoligi", "Muvaffaqiyatli yaratildi", "Avtorizatsiya talab qilinadi" },
                "404 Not Found — so me'ralgan resurs yoki endpoint serverda topilmaganini bildiradi."),

            CreateQuestion("401 status kodi nimani bildiradi?",
                new List<string> { "Ruxsat berilmagan (Unauthorized)", "Server xatosi", "Muvaffaqiyatli javob", "Noto'g'ri so'rov formati" },
                "401 Unauthorized — foydalanuvchi autentifikatsiyadan o me'tmaganligini va token yo'qligini anglatadi."),

            CreateQuestion("Minimal API nima?",
                new List<string> { "Kam kod bilan endpoint yaratishga imkon beruvchi yondashuv (MapGet, MapPost)", "Faqat testlar uchun mo'ljallangan API", "Faqat XML formatida ishlaydigan API", "Deprecated bo'lgan texnologiya" },
                "Minimal API controllersiz, minimal boilerplate kod bilan MapGet/MapPost orqali tezkor API yozish usulidir."),

            CreateQuestion("launchSettings.json fayli nima uchun kerak?",
                new List<string> { "Lokal development muhitida ilovani ishga tushirish sozlamalari uchun", "Production konfiguratsiyasi uchun", "Ma'lumotlar bazasi sxemasi uchun", "NuGet paketlarini boshqarish uchun" },
                "launchSettings.json lokal ishlab chiqishda portlar va environment o'zgaruvchilarini sozlash uchun kerak."),

            CreateQuestion("ASP.NET Core'da environment (masalan, Development, Production) qanday aniqlanadi?",
                new List<string> { "ASPNETCORE_ENVIRONMENT muhit o'zgaruvchisi orqali", "appsettings.xml orqali", "web.config orqali", "Faqat kod ichida hardcode qilinadi" },
                "ASPNETCORE_ENVIRONMENT muhit o'zgaruvchisi ilovaning ishchi rejimini (Development, Staging, Production) belgilaydi."),

            CreateQuestion("JSON serializatsiya uchun ASP.NET Core'da default kutubxona qaysi?",
                new List<string> { "System.Text.Json", "Newtonsoft.Json (har doim)", "Xml.Serialization", "Json.NET Core" },
                "System.Text.Json — ASP.NET Core 3.0+ dan boshlab o'rnatilgan yuqori tezlikka ega standart JSON kutubxonasidir."),

            CreateQuestion("Controller-based API va Minimal API o'rtasidagi asosiy farq nima?",
                new List<string> { "Controller-based klassik MVC strukturasidan foydalanadi, Minimal API esa yengil, funksional yondashuv", "Minimal API faqat GET so'rovlarni qo'llab-quvvatlaydi", "Ular bir xil, farq yo'q", "Minimal API faqat .NET Framework'da ishlaydi" },
                "Controller-based API klassik MVC strukturaga tayanadi; Minimal API esa ortiqcha sinflarsiz yengil funksional yondashuv beradi."),

            CreateQuestion("dotnet CLI orqali yangi Web API loyihasini yaratish uchun qaysi buyruq ishlatiladi?",
                new List<string> { "dotnet new webapi", "dotnet create webapi", "dotnet init api", "dotnet start webapi" },
                "`dotnet new webapi` buyrug'i yangi ASP.NET Core Web API shablon loyihasini shakllantiradi."),

            CreateQuestion("[FromBody] atributi nima uchun ishlatiladi?",
                new List<string> { "HTTP so'rov tanasidan (body) ma'lumotni bind qilish uchun", "Query string'dan ma'lumot olish uchun", "Header'dan ma'lumot olish uchun", "Route'dan ma'lumot olish uchun" },
                "[FromBody] kelayotgan JSON body ma'lumotini C# modeliga bind qilishni yuklaydi."),

            CreateQuestion("[FromQuery] atributi qaysi manbadan ma'lumot oladi?",
                new List<string> { "URL query parametrlaridan", "Request body'dan", "Cookie'dan", "Header'dan" },
                "[FromQuery] URL manzildagi `?key=value` query parametrlarini action metod ko'rsatkichlariga bog'laydi."),

            CreateQuestion(".csproj faylida TargetFramework nimani belgilaydi?",
                new List<string> { "Ilova qaysi .NET versiyasiga mo'ljallanganini", "Loyihaning nomi", "Ma'lumotlar bazasi turini", "Server portini" },
                "TargetFramework (masalan `<TargetFramework>net10.0</TargetFramework>`) loyiha kompilyatsiya bo'ladigan .NET platforma versiyasidir.")
        };
    }

    private static List<Question> GenerateDotNetMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("Action Filter qachon ishga tushadi?",
                new List<string> { "Action metod bajarilishidan oldin va keyin", "Faqat ilova ishga tushganda", "Faqat xatolik yuz berganda", "Faqat authentication vaqtida" },
                "Action Filter controller action metodi bajarilishidan bevosita oldin va keyin mantiq bajarish uchun xizmat qiladi."),

            CreateQuestion("Exception Filter vazifasi nima?",
                new List<string> { "Action ichida yuz bergan istisnolarni ushlab, boshqarish", "Ma'lumotlarni validatsiya qilish", "Route'larni belgilash", "Middleware'ni ro'yxatdan o'tkazish" },
                "Exception Filter controller action metodlari ichida yuz bergan tutilmagan exception-larni markazlashgan holda ushlaydi."),

            CreateQuestion("Model validatsiyasi uchun qaysi atribut ishlatiladi?",
                new List<string> { "[Required]", "[Validate]", "[Check]", "[Mandatory]" },
                "[Required] Data Annotation atributi model maydonining bo'sh (null/empty) bo'lmasligini validatsiya qiladi."),

            CreateQuestion("Custom middleware yaratishda odatda qaysi metod chaqiriladi?",
                new List<string> { "InvokeAsync()", "Execute()", "Run()", "Handle()" },
                "Custom middleware sinflarida `public async Task InvokeAsync(HttpContext context)` metodi so'rovni qayta ishlash uchun ishlatiladi."),

            CreateQuestion("JWT (JSON Web Token) autentifikatsiyada nima uchun ishlatiladi?",
                new List<string> { "Foydalanuvchi identifikatsiyasi va claims'larni token ko'rinishida uzatish uchun", "Ma'lumotlar bazasini shifrlash uchun", "Static fayllarni siqish uchun", "Loglarni saqlash uchun" },
                "JWT stateliss autentifikatsiya bo'lib, foydalanuvchi ma'lumotlari (claims) va raqamli imzoni token sifatida uzatadi."),

            CreateQuestion("Authorization policy nima uchun ishlatiladi?",
                new List<string> { "Murakkab ruxsat berish qoidalarini (masalan, rol, claim asosida) belgilash uchun", "Foydalanuvchi parolini shifrlash uchun", "Ma'lumotlar bazasi ulanishini sozlash uchun", "API versiyasini belgilash uchun" },
                "Authorization Policy bir nechta talab va da'volarni (claims, roles) jamlab moslashtirilgan ruxsat tizimini hosil qiladi."),

            CreateQuestion("Entity Framework Core'da migratsiya nima uchun ishlatiladi?",
                new List<string> { "Model o'zgarishlarini ma'lumotlar bazasi sxemasiga qo'llash uchun", "Fayllarni ko'chirish uchun", "API endpoint yaratish uchun", "Loglarni tozalash uchun" },
                "EF Core Migrations C# entity modellaridagi o'zgarishlarni ma'lumotlar bazasi DDL sxemasiga tatbiq qiladi."),

            CreateQuestion("DbContext odatda qaysi DI lifetime bilan ro'yxatdan o'tkaziladi?",
                new List<string> { "Scoped", "Singleton", "Transient", "Static" },
                "DbContext thread-safe bo'lmagani sababli har bir HTTP request uchun alohida `Scoped` lifetime bilan ro'yxatdan o'tkaziladi."),

            CreateQuestion("async/await ishlatishning asosiy afzalligi nima?",
                new List<string> { "Thread'larni bloklamasdan resurslardan samarali foydalanish", "Kodni qisqartirish", "Xotirani ko'proq ishlatish", "Faqat sinxron kod uchun kerak" },
                "asinxron dasturlash (async/await) I/O operatsiyalarida Thread Pool worker iplarini bloklamaydi va yuqori skalabillik beradi."),

            CreateQuestion("Global xatolikni ushlash uchun ASP.NET Core'da qaysi middleware ishlatiladi?",
                new List<string> { "UseExceptionHandler()", "UseRouting()", "UseAuthentication()", "UseStaticFiles()" },
                "`app.UseExceptionHandler()` so'rovlar quvurida tutilmagan barcha exception-larni tutib olib xavfsiz response qaytaradi."),

            CreateQuestion("ProblemDetails nima uchun ishlatiladi?",
                new List<string> { "RFC 7807 standartiga mos xato javoblarini formatlash uchun", "Ma'lumotlar bazasi xatoliklarini loglash uchun", "Frontend komponentlarini render qilish uchun", "Route'larni tekshirish uchun" },
                "ProblemDetails (RFC 7807) API xatoliklarini standart va bir xillashgan JSON strukturada taqdim etadi."),

            CreateQuestion("CORS (Cross-Origin Resource Sharing) nima uchun kerak?",
                new List<string> { "Boshqa domendan kelayotgan so'rovlarga ruxsat berish/cheklash", "Ma'lumotlar bazasi ulanishini tezlashtirish", "Static fayllarni siqish", "JWT tokenlarni generatsiya qilish" },
                "CORS brauzerlar darajasida boshqa domenlardan (origin) kelayotgan API so me'rovlariga ruxsat berishni xavfsiz boshqaradi."),

            CreateQuestion("API versiyalashning asosiy maqsadi nima?",
                new List<string> { "Eski clientlarni buzmasdan API'ni rivojlantirish", "Kodni tezlashtirish", "Ma'lumotlar bazasini optimallashtirish", "Faqat test uchun kerak" },
                "API Versioning (masalan `/api/v1`, `/api/v2`) mavjud mijoz ilovalarni (mobile/web) izdan chiqarmay yangi imkoniyatlar qo'shish imkonini beradi."),

            CreateQuestion("DTO (Data Transfer Object) nima uchun ishlatiladi?",
                new List<string> { "Qatlamlar (layers) o'rtasida ma'lumot uzatish uchun, domain modelni yashirish", "Ma'lumotlar bazasi jadvalini yaratish uchun", "Middleware yozish uchun", "Routing uchun" },
                "DTO ichki Domain va DB entity modellarini tashqi dunyoga to'g'ridan-to'g'ri ko'rsatmasdan, kerakli ma'lumotlarnigina uzatish uchun ishlatiladi."),

            CreateQuestion("AutoMapper kutubxonasi nima uchun ishlatiladi?",
                new List<string> { "Obyektlar orasida (masalan, Entity → DTO) avtomatik map qilish uchun", "Ma'lumotlar bazasi migratsiyasi uchun", "Authentication uchun", "Loglash uchun" },
                "AutoMapper C# ob'ektlari va DTO-lar o me'rtasida nusxalash kodi (mapping boilerplate) ni avtomatlashtiradi."),

            CreateQuestion("Repository pattern qanday maqsadda qo'llaniladi?",
                new List<string> { "Ma'lumotlar bazasiga murojaat qilish logikasini abstraktsiyalash uchun", "Frontend UI yaratish uchun", "HTTP so'rovlarini logging qilish uchun", "Middleware tartibini belgilash uchun" },
                "Repository Pattern ma'lumotlar bazasiga kirish logikasini abstraktsiya qilib, biznes mantiqni ma'lumotlar manbasidan ajratadi."),

            CreateQuestion("IHttpClientFactory nima uchun tavsiya etiladi?",
                new List<string> { "HttpClient obyektlarini to'g'ri boshqarish va socket exhaustion muammosini oldini olish uchun", "Faqat testlarda ishlatish uchun", "Ma'lumotlar bazasiga ulanish uchun", "JSON serializatsiya uchun" },
                "IHttpClientFactory socket ulanishlarini (handler pooling) samarali qayta ishlatib, Socket Exhaustion va DNS o'zgarishi muammolarini hal etadi."),

            CreateQuestion("ILogger interfeysi nima uchun ishlatiladi?",
                new List<string> { "Strukturaviy loglashni amalga oshirish", "Ma'lumotlarni validatsiya qilish", "Route'larni belgilash", "DI konteynerni sozlah" },
                "ILogger text va parametrli structured loglarni yozish hamda Serilog, NLog kabi tizimlarga uzatish uchun ishlatiladi."),

            CreateQuestion("Health Checks (sog'liqni tekshirish) nima uchun ishlatiladi?",
                new List<string> { "Ilovaning va uning bog'liqliklarining (DB, tashqi servis) ishlash holatini monitoring qilish uchun", "Faqat UI testlari uchun", "JWT tokenlarni tekshirish uchun", "Routing xatolarini topish uchun" },
                "Health Checks API va unga bog'liq PostgreSQL, Redis, RabbitMQ kabi servislarning tayyor va sog'lomligini (Healthy) monitoring qiladi."),

            CreateQuestion("In-memory caching qachon foydali?",
                new List<string> { "Tez-tez o'zgarmaydigan va tez-tez o'qiladigan ma'lumotlar uchun", "Har doim, hech qanday cheklovsiz", "Faqat parollarni saqlash uchun", "Faqat static fayllar uchun" },
                "In-memory caching o'zgarmas ma'lumotlarni RAM xotirada saqlab, ma'lumotlar bazasiga tushadigan yukni kamaytiradi."),

            CreateQuestion("Response caching qanday ishlaydi?",
                new List<string> { "Server javoblarini keshlab, keyingi bir xil so'rovlarga tezroq javob berish", "Faqat client tomonida ishlaydi", "Ma'lumotlar bazasini keshlaydi", "Faqat statik saytlarda ishlaydi" },
                "Response Caching HTTP Header-lariga (Cache-Control) tayanib, bir xil so'rovlarga keshdan tezkor javob beradi."),

            CreateQuestion("Rate limiting nima uchun qo'llaniladi?",
                new List<string> { "API'ga so'rovlar sonini cheklab, resurslarni himoya qilish uchun", "Ma'lumotlar bazasini tezlashtirish uchun", "JSON formatini o'zgartirish uchun", "Faqat GET so'rovlar uchun" },
                "Rate Limiting so'rovlar oqimini cheklash orqali serverni DDoS va zo'riqish (overload) hujumlaridan himoya qiladi."),

            CreateQuestion("Pagination (sahifalash) nima uchun muhim?",
                new List<string> { "Katta hajmdagi ma'lumotlarni bo'laklab qaytarib, performance'ni yaxshilash uchun", "Faqat UI dizayni uchun", "Ma'lumotlar bazasini shifrlash uchun", "Authentication uchun" },
                "Pagination minglab ma'lumotlarni bir vaqtda xotiraga va tarmoqqa yuklamasdan, sahifalab (PageNumber, PageSize) unumli uzatadi."),

            CreateQuestion("FluentValidation kutubxonasining afzalligi nima?",
                new List<string> { "Murakkab validatsiya qoidalarini aniq va o'qilishi oson kod bilan yozish imkonini beradi", "Faqat frontend uchun ishlaydi", "Ma'lumotlar bazasi migratsiyasini avtomatlashtiradi", "HTTP so'rovlarini keshlaydi" },
                "FluentValidation Data Annotation-lardan xalos etib, koddagi alohida Validator sinflarda toza va strongly-typed validatsiya beradi."),

            CreateQuestion("Options pattern (IOptions<T>) nima uchun ishlatiladi?",
                new List<string> { "Konfiguratsiya qiymatlarini strongly-typed obyekt sifatida in'ektsiya qilish uchun", "Ma'lumotlar bazasi ulanishini yaratish uchun", "Middleware buyurtmasini o'zgartirish uchun", "Routing uchun" },
                "Options Pattern appsettings.json konfiguratsiyalarini strongly-typed C# obyektlariga bog'lab uzatadi."),

            CreateQuestion("IHostedService interfeysi nima uchun ishlatiladi?",
                new List<string> { "Fon rejimida (background) uzoq muddatli vazifalarni bajarish uchun", "Faqat controller yaratish uchun", "JSON serializatsiya uchun", "Static fayllarni xizmat qilish uchun" },
                "IHostedService ilova ishga tushishi bilan orqa fonda ishlovchi uzoq umrli background task-larni ta'minlaydi."),

            CreateQuestion("Custom Model Binder qachon kerak bo'ladi?",
                new List<string> { "Standart binding mexanizmi murakkab yoki maxsus formatdagi ma'lumotni to'g'ri bog'lay olmaganda", "Faqat GET so'rovlar uchun", "Faqat authentication uchun", "Har doim majburiy" },
                "Custom Model Binder kelayotgan nodatiy query, header yoki string ma'lumotlarni murakkab C# obyektlariga o'girish uchun kerak."),

            CreateQuestion("Content negotiation nima?",
                new List<string> { "Client va server o'rtasida ma'lumot formatini (JSON, XML) kelishish jarayoni", "Ma'lumotlar bazasi bilan muzokara", "Authentication jarayoni", "Xatolarni qayta ishlash" },
                "Content Negotiation mijoz yuborgan `Accept` header-i bo'yicha server qaysi formatda (JSON/XML) javob qaytarishini kelishadi."),

            CreateQuestion("AsNoTracking() EF Core'da nima uchun ishlatiladi?",
                new List<string> { "Faqat o'qish uchun so'rovlarda change tracking'ni o'chirib, performance'ni oshirish uchun", "Ma'lumotni o'chirish uchun", "Migratsiya yaratish uchun", "Connection string'ni sozlash uchun" },
                "AsNoTracking() so'rov natijalarini ChangeTracker snapshot-larida saqlamaydi va o'qish tezligini sezilarli oshiradi."),

            CreateQuestion("Role-based authorization qanday ishlaydi?",
                new List<string> { "Foydalanuvchi rollariga (masalan, Admin, User) asoslanib ruxsat beriladi", "Faqat IP manzil asosida", "Faqat parol uzunligiga qarab", "Faqat vaqt asosida" },
                "Role-based authorization `[Authorize(Roles = 'Admin')]` atributi yordamida foydalanuvchi roliga qarab ruxsatlarni cheklaydi.")
        };
    }

    private static List<Question> GenerateDotNetHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("gRPC va REST API o'rtasidagi asosiy farq nima?",
                new List<string> { "gRPC HTTP/2 va Protocol Buffers ishlatadi, ko'proq performance va strongly-typed contract beradi", "gRPC faqat frontend uchun", "REST har doim tezroq ishlaydi", "Ular bir xil protokol" },
                "gRPC HTTP/2 ustida mukammal va strongly-typed Protobuf serializatsiyasini beradi va REST-ga qaraganda ancha tezkor."),

            CreateQuestion("SignalR nima uchun ishlatiladi?",
                new List<string> { "Real-time, ikki tomonlama aloqa (masalan, chat, notification) uchun", "Faqat ma'lumotlar bazasi migratsiyasi uchun", "Static fayllarni siqish uchun", "Faqat REST API yaratish uchun" },
                "SignalR WebSockets va Fallback transportlar yordamida server va mijozlar o'rtasida real-vaqtdagi duplex aloqani ta'minlaydi."),

            CreateQuestion("Distributed cache (masalan, Redis) local in-memory cache'dan nimasi bilan farq qiladi?",
                new List<string> { "Bir nechta server instance'lari orasida umumiy keshni ta'minlaydi", "Faqat bitta serverga xos", "U hech qanday tarmoq talab qilmaydi", "Faqat statik fayllar uchun ishlaydi" },
                "Distributed Cache (Redis) bir nechta mikroservis yoki load balancer orqasidagi serverlar o'rtasida umumiy keshni beradi."),

            CreateQuestion("Circuit Breaker pattern (masalan, Polly kutubxonasi) nima uchun ishlatiladi?",
                new List<string> { "Muvaffaqiyatsiz bo'layotgan tashqi servisga bo'lgan so'rovlarni vaqtincha to'xtatib, tizimni himoya qilish uchun", "Ma'lumotlar bazasi migratsiyasi uchun", "JSON serializatsiya uchun", "Routing uchun" },
                "Circuit Breaker buzilgan tashqi servisga tinimsiz so'rov yuborib resurslarni band qilmaslik uchun so'rovlarni vaqtincha tosad."),

            CreateQuestion("IHostedService va BackgroundService o'rtasidagi farq nima?",
                new List<string> { "BackgroundService — IHostedService'ni implement qiluvchi abstract klass, ExecuteAsync orqali yozishni osonlashtiradi", "Ular butunlay bog'liq emas", "BackgroundService faqat controller'larda ishlaydi", "IHostedService faqat .NET Framework'da mavjud" },
                "BackgroundService abstract sinfi IHostedService-ni implement qilgan va CancellationToken bilan `ExecuteAsync` metodini beradi."),

            CreateQuestion("Channel<T> fon vazifalarni boshqarishda nima uchun foydali?",
                new List<string> { "Producer-consumer pattern'ni thread-safe tarzda amalga oshirish uchun", "Faqat UI thread uchun", "Ma'lumotlar bazasi ulanishini poolga solish uchun", "HTTP header'larni o'qish uchun" },
                "System.Threading.Channels Producer-Consumer modelida asinxron, thread-safe va backpressure-li ma'lumot oqimini beradi."),

            CreateQuestion("Refresh token mexanizmi nima uchun kerak?",
                new List<string> { "Access token muddati tugaganda foydalanuvchini qayta login qildirmasdan yangi token olish uchun", "Parolni saqlash uchun", "Ma'lumotlar bazasini shifrlash uchun", "Faqat admin foydalanuvchilar uchun" },
                "Refresh Token Access Token muddati tugaganda foydalanuvchini qayta parolni so me'ramasdan yangi Access Token berish uchun ishlatiladi."),

            CreateQuestion("OAuth2 va OpenID Connect o'rtasidagi farq nima?",
                new List<string> { "OAuth2 avtorizatsiya protokoli, OpenID Connect esa OAuth2 ustiga qurilgan autentifikatsiya qatlami", "Ular bir xil protokol, faqat nomi boshqa", "OpenID Connect faqat mobil ilovalar uchun", "OAuth2 faqat Google uchun ishlatiladi" },
                "OAuth2 resurslarga kirish ruxsatini (Authorization) beradi. OpenID Connect esa uning ustida shaxsni tasdiqlaydi (Authentication)."),

            CreateQuestion("Custom Authorization Handler qachon zarur bo'ladi?",
                new List<string> { "Oddiy rol asosidagi tekshiruv yetarli bo'lmagan, murakkab biznes qoidalari kerak bo'lganda", "Faqat static fayllar uchun", "Har doim, oddiy holatlarda ham", "Faqat GET so'rovlar uchun" },
                "Custom Authorization Handler yosh, vaqt yoki database ma me'lumotlariga asoslangan murakkab avtorizatsiya mantiqlarini tekshiradi."),

            CreateQuestion("Claims-based authorization qanday ishlaydi?",
                new List<string> { "Foydalanuvchi haqidagi turli claim'lar (masalan, yosh, bo'lim) asosida ruxsat qarorlari qabul qilinadi", "Faqat parol uzunligi asosida", "Faqat IP manzil asosida", "Faqat vaqt zonasiga qarab" },
                "Claims-based authorization token ichidagi da'volarga (Email, Role, Department) tayanib ruxsat beradi."),

            CreateQuestion("N+1 muammosi EF Core'da nima?",
                new List<string> { "Har bir asosiy yozuv uchun alohida-alohida qo'shimcha so'rov yuborilishi natijasida performance pasayishi", "Ma'lumotlar bazasida jadval yetishmasligi", "JSON formatidagi xatolik", "Migratsiya xatosi" },
                "N+1 muammosi bog'liq ma me'lumotlar tsiklda har bir qator uchun alohida SQL bilan o'qilganda kelib chiqadi va tezlikni pasaytiradi."),

            CreateQuestion("Compiled query EF Core'da nima uchun ishlatiladi?",
                new List<string> { "Tez-tez bajariladigan LINQ so'rovlarni oldindan kompilyatsiya qilib, performance'ni oshirish uchun", "Ma'lumotlar bazasini yaratish uchun", "Migratsiyani orqaga qaytarish uchun", "Connection string'ni shifrlash uchun" },
                "Compiled Query LINQ tree parsing va compilation xarajatlarini 1 marta bajarib keshlaydi."),

            CreateQuestion("Connection pooling nima uchun muhim?",
                new List<string> { "Ma'lumotlar bazasi ulanishlarini qayta ishlatib, yangi ulanish yaratish xarajatini kamaytirish uchun", "Faqat static fayllar uchun", "Faqat authentication uchun", "JSON serializatsiya uchun" },
                "Connection Pooling mavjud baza ulanishlarini (connections) qayta ishlatib, har safar TCP handshake va ulanish xarajatini tejaydi."),

            CreateQuestion("Horizontal scaling nima?",
                new List<string> { "Ko'proq server instance'lari qo'shib, yukni taqsimlash", "Bitta serverga ko'proq resurs (CPU/RAM) qo'shish", "Faqat ma'lumotlar bazasini kattalashtirish", "Faqat kod optimizatsiyasi" },
                "Horizontal Scaling (Scale-out) tizimga yangi qo'shimcha server nusxalarini qo'shib yukni bo'lishdir."),

            CreateQuestion("Load balancer nima uchun ishlatiladi?",
                new List<string> { "So'rovlarni bir nechta server instance'lari orasida taqsimlash uchun", "Ma'lumotlar bazasini zaxiralash uchun", "JSON formatlash uchun", "Faqat frontend uchun" },
                "Load Balancer kelayotgan tarmoq so'rovlarini bir nechta backend serverlar o'rtasida muvozanatli taqsimlaydi."),

            CreateQuestion("Distributed tracing (masalan, OpenTelemetry) nima uchun kerak?",
                new List<string> { "Microservice'lar orasida bitta so'rovning yo'lini kuzatib, performance muammolarini aniqlash uchun", "Faqat frontend animatsiyalari uchun", "Ma'lumotlar bazasi migratsiyasi uchun", "Faqat log darajasini o'zgartirish uchun" },
                "Distributed Tracing bitta so'rovning barcha mikroservislar va bazalar bo'ylab o'tish zanjirini va ketgan vaqtni trace id orqali ko'rsatadi."),

            CreateQuestion("Server-Sent Events (SSE) WebSocket'dan nimasi bilan farq qiladi?",
                new List<string> { "SSE faqat serverdan client'ga bir tomonlama oqim, WebSocket esa ikki tomonlama", "SSE ikki tomonlama, WebSocket bir tomonlama", "Ular butunlay bir xil", "SSE faqat mobil qurilmalarda ishlaydi" },
                "SSE faqat serverdan mijozga bir tomonlama (one-way) hodisalar yuboradi, WebSocket esa duplex ikki tomonlama aloqa beradi."),

            CreateQuestion("Custom ModelBinderProvider qanday holatda ishlatiladi?",
                new List<string> { "Ma'lum bir turdagi barcha parametrlar uchun global binding logikasini ro'yxatdan o'tkazishda", "Faqat static fayllar uchun", "Faqat authentication uchun", "Faqat routing uchun" },
                "ModelBinderProvider ko'rsatilgan C# tiplari uchun custom model binder-larni global ro'yxatdan o me'tkazish imkonini beradi."),

            CreateQuestion("Response compression (masalan, Gzip, Brotli) nima uchun ishlatiladi?",
                new List<string> { "Javob hajmini kichraytirib, tarmoq orqali uzatish tezligini oshirish uchun", "Ma'lumotlar bazasini siqish uchun", "JWT tokenni shifrlash uchun", "Faqat rasm fayllari uchun" },
                "Response Compression JSON, HTML, CSS va JS javoblarini siqib tarmoq trafigini va yuklanish vaqtini qisqartiradi."),

            CreateQuestion("HTTP/2 HTTP/1.1'dan asosiy afzalligi nima?",
                new List<string> { "Multiplexing orqali bir ulanishda bir nechta so'rov-javobni parallel yuborish imkoniyati", "Faqat xavfsizlikni oshiradi", "Faqat kattaroq fayllarni qo'llab-quvvatlaydi", "Ular bir xil ishlaydi" },
                "HTTP/2 Multiplexing texnologiyasi bitta TCP ulanishi ustida o'nlab so'rov va javoblarni bir vaqtning o'zida parallel uzatadi."),

            CreateQuestion("Idempotency key nima uchun ishlatiladi?",
                new List<string> { "Bir xil so'rov bir necha marta yuborilganda ham operatsiya faqat bir marta bajarilishini kafolatlash uchun", "Ma'lumotlar bazasini shifrlash uchun", "JSON formatini o'zgartirish uchun", "Faqat GET so'rovlar uchun" },
                "Idempotency Key bir xil HTTP POST so'rovi tarmoq qayta harakatida ikkinchi bor kelganda ham to'lov yoki amal faqat 1 marta bajarilishini ta me'minlaydi."),

            CreateQuestion("YARP (Yet Another Reverse Proxy) nima uchun ishlatiladi?",
                new List<string> { ".NET asosida API Gateway/reverse proxy qurish uchun", "Ma'lumotlar bazasi migratsiyasi uchun", "Frontend komponent yaratish uchun", "Faqat testlar uchun" },
                "YARP (Yet Another Reverse Proxy) Microsoft tomonidan yozilgan yuqori unumdorlikdagi Reverse Proxy va API Gateway freymvorkidir."),

            CreateQuestion("WebApplicationFactory nima uchun ishlatiladi?",
                new List<string> { "Integration testlarda in-memory test serverini yaratish uchun", "Production serverini sozlash uchun", "Ma'lumotlar bazasi migratsiyasi uchun", "JSON serializatsiya uchun" },
                "WebApplicationFactory ASP.NET Core-da integratsion testlar yozishda test serveri va TestHttpClient-ni xotirada ishga tushirish uchun ishlatiladi."),

            CreateQuestion("Output caching Response caching'dan nimasi bilan farq qiladi?",
                new List<string> { "Output caching server tomonida butun javobni saqlab, keyingi so'rovlarga ilovaga tegmasdan javob beradi, kengroq boshqaruv imkoniyatlari bilan", "Ular butunlay bir xil narsa", "Output caching faqat client tomonida ishlaydi", "Response caching faqat POST so'rovlar uchun" },
                "Output Caching ASP.NET Core 7+ da server-side kesh platformasi bo'lib Tag Eviction va Custom Policies beradi."),

            CreateQuestion("Minimal API va Controller-based API o'rtasidagi performance farqi nimadan kelib chiqadi?",
                new List<string> { "Minimal API'da MVC pipeline'ining ba'zi qatlamlari (masalan, model binding overhead) yengilroq bo'lishi mumkin", "Ular har doim bir xil tezlikda ishlaydi", "Controller-based har doim tezroq", "Farq faqat ma'lumotlar bazasida" },
                "Minimal API MVC pipeline-ning ortiqcha reflection va filter zanjirlaridan xalos bo'lgani uchun yengilroq va tezroq ishlaydi."),

            CreateQuestion("Custom middleware pipeline'da UseAuthentication() va UseAuthorization() tartibi nima uchun muhim?",
                new List<string> { "Avval foydalanuvchi kimligi aniqlanishi (Authentication), keyin ruxsatlari tekshirilishi (Authorization) kerak", "Tartib ahamiyatsiz", "Authorization har doim birinchi bo'lishi kerak", "Ular alohida pipeline'da ishlaydi" },
                "UseAuthentication avval foydalanuvchi shaxsini (User Claims) tiklaydi, shundan so me'nggina UseAuthorization uning huquqlarini tekshira oladi."),

            CreateQuestion("gRPC streaming turlari nechta va qaysilar?",
                new List<string> { "4 ta: Unary, Server streaming, Client streaming, Bidirectional streaming", "Faqat 1 ta: Unary", "Faqat 2 ta: GET va POST", "3 ta: Sync, Async, Batch" },
                "gRPC 4 xil muloqot rejimiga ega: Unary (1:1), Server streaming (1:N), Client streaming (N:1), va Bidirectional streaming (N:M)."),

            CreateQuestion("Distributed system'da eventual consistency nimani anglatadi?",
                new List<string> { "Ma'lumotlar vaqt o'tishi bilan barcha node'larda bir xil holatga keladi, lekin darhol emas", "Ma'lumotlar barcha node'larda darhol bir xil bo'lishi kafolatlanadi", "Ma'lumotlar hech qachon sinxronlanmaydi", "Faqat bitta node ishlatiladi" },
                "Eventual Consistency taqsimlangan tizimlarda ma'lumotlar biroz vaqt o'tgach (asinxron sinxronizatsiyadan so'ng) barcha tugunlarda bir xil holatga kelishini anglatadi."),

            CreateQuestion("API Gateway pattern microservice arxitekturasida nima uchun muhim?",
                new List<string> { "Client so'rovlarini bitta kirish nuqtasi orqali marshrutlash, autentifikatsiya va rate limiting kabi umumiy vazifalarni markazlashtirish uchun", "Ma'lumotlar bazasini almashtirish uchun", "Faqat frontend uchun UI yaratish uchun", "Faqat loglash uchun" },
                "API Gateway barcha mijoz so me'rovlari uchun yagona darvoza (entry point) bo'lib routing, security va rate-limiting-ni markazlashtiradi."),

            CreateQuestion("Health check'larni Kubernetes/orkestrator bilan integratsiya qilishning asosiy maqsadi nima?",
                new List<string> { "Nosog'lom instance'larni avtomatik aniqlab, trafikdan chetlashtirish yoki qayta ishga tushirish uchun", "Faqat loglarni ko'rish uchun", "Ma'lumotlar bazasi zaxira nusxasini olish uchun", "Faqat UI testlash uchun" },
                "Kubernetes Liveness va Readiness probe-lari Health Check endpoint-lariga qarab ishlamayotgan pod-larni avtomatik qayta tushiradi (Self-healing).")
        };
    }
}
