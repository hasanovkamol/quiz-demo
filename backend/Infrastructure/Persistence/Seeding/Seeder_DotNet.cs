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
            CreateQuestion("ASP.NET Core arxitekturasi va uni ko'p platformada (Cross-platform) ishlash imkoniyati haqida qaysi tasdiq to'g'ri?",
                new List<string> { 
                    "U faqat Windows OS ichidagi IIS web serverida ishlaydi", 
                    "U Windows, Linux va macOS operatsion tizimlarida bir xil .NET runtime muhitida ishlaydi", 
                    "U cross-platform bo'lishi uchun alohida Java Virtual Machine (JVM) talab etadi", 
                    "U faqat Linux muhitida Docker konteyneri ichida ishlash uchun cheklangan" 
                },
                "ASP.NET Core arxitektura jihatidan xolis Cross-platform freymvork bo'lib, .NET Core / .NET 6+ runtime-i mavjud bo'lgan har qanday operatsion tizimda (Windows, Linux, macOS) bir xil ishlaydi."),

            CreateQuestion("ASP.NET Core loyihalarida ilovaning kirish nuqtasi (entry point) va servislar hamda middleware-larni sozlash qaysi faylda amalga oshiriladi?",
                new List<string> { "web.config", "Startup.cs", "Program.cs", "appsettings.json" },
                "Program.cs fayli .NET 6+ ilovalarida top-level statements va WebApplication builder orqali ilovani sozlash va ishga tushirish uchun yagona kirish faylidir."),

            CreateQuestion("ASP.NET Core-da o'rnatilgan, yuqori unumdorlikka ega cross-platform HTTP web server nomi qaysi?",
                new List<string> { "Nginx", "Kestrel", "Apache HTTP Server", "IIS Worker Process (w3wp.exe)" },
                "Kestrel — ASP.NET Core ilovalari uchun standart va yuqori unumdorlikka ega ko'p platformali web serverdir. Nginx va IIS odatda Kestrel oldida Reverse Proxy sifatida ishlatiladi."),

            CreateQuestion("ASP.NET Core HTTP so'rovlar quvurida (HTTP Request Pipeline) Middleware qanday vazifani bajaradi?",
                new List<string> { 
                    "Faqat ma'lumotlar bazasi jadvallari va Entity model-lari o'rtasida bog'liqlik o'rnatadi", 
                    "Kelayotgan HTTP so'rovni qabul qilib, uni qayta ishlash, keyingi middleware-ga uzatish (next) yoki javob qaytarish vazifasini bajaradi", 
                    "C# kodini ilova ishga tushganda CIL (Common Intermediate Language) ga o'giradi", 
                    "Faqat frontend komponentlarini HTML formatida render qilish bilan shug'ullanadi" 
                },
                "Middleware — so'rovlar quvuriga joylashtirilgan dasturiy komponent bo'lib, HTTP so'rovlarini tahlil qilishi, o'zgartirishi, autentifikatsiyadan o'tkazishi yoki javob qaytarib so'rovni to'xtatishi mumkin."),

            CreateQuestion("Program.cs faylida chaqirilgan middleware-larning ishlash tartibi haqida qaysi tasdiq to'g'ri?",
                new List<string> { 
                    "Middleware-larning nomlari alifbo tartibida joylashgan bo'lishi kerak", 
                    "Middleware-lar Program.cs faylida kod bo'yicha ketma-ket qaysi tartibda qo'shilgan (app.Use...) bo'lsa, so'rov aynan shu tartibda ishlanadi", 
                    "Tartib mutlaqo ahamiyatsiz, runtime ularni avtomatik tarzda to'g'ri joylashtiradi", 
                    "Har bir middleware alohida Thread ichida tasodifiy tartibda ishga tushadi" 
                },
                "HTTP Request Pipeline qat'iy chiziqli tartibga ega. Masalan, app.UseAuthentication() avval chaqirilmasa, undan keyingi app.UseAuthorization() foydalanuvchi claims-larini topa olmaydi."),

            CreateQuestion("Controller sinfiga avtomatik model validatsiyasi (400 BadRequest) va API qulayliklarini berish uchun qaysi atribut qo'llaniladi?",
                new List<string> { "[ApiController]", "[HttpController]", "[WebController]", "[RestController]" },
                "[ApiController] atributi controller klasslariga avtomatik validation va binding qulayliklarini beradi va ModelState.IsValid ni avtomatik tekshiradi."),

            CreateQuestion("GET so'rovini qabul qiluvchi action metodni belgilash uchun qaysi atribut ishlatiladi?",
                new List<string> { "[HttpGet]", "[RouteGet]", "[FromGet]", "[FetchAction]" },
                "[HttpGet] atributi HTTP GET verbi orqali kelgan RESTful so'rovlarni Controller-dagi tegishli Action metodga marshrutlaydi."),

            CreateQuestion("IActionResult interfeysi controller action metodida nima uchun qo'llaniladi?",
                new List<string> { 
                    "Faqat C# sinflari yaratish va ularni serializatsiya qilish uchun", 
                    "Har xil turdagi HTTP status javoblarini (200 OK, 404 NotFound, 400 BadRequest) mos elastik formatda qaytarish uchun", 
                    "Baza operatsiyalarini transaction darajasida ushlash uchun", 
                    "Middleware pipeline-ga yangi obyekt qo'shish uchun" 
                },
                "IActionResult moslashuvchan interfeys bo'lib, mantiqiy natijaga qarab har xil HTTP natija obyektlarini (OkObjectResult, NotFoundResult, va h.k.) qaytarishga imkon beradi."),

            CreateQuestion("ASP.NET Core-da appsettings.json fayli qaysi maqsadda ishlatiladi?",
                new List<string> { 
                    "Foydalanuvchi brauzerida ishlaydigan HTML/JS resurslarini joylashtirish uchun", 
                    "Loyihaning environment sozlamalari, connection string-lar va ilova parametrlarini saqlash uchun", 
                    "C# kodi uchun zarur NuGet kutubxonalari ro'yxatini belgilash uchun", 
                    "SQL ma'lumotlar bazasida jadval sxemalarini yaratish uchun" 
                },
                "appsettings.json JSON formatidagi konfiguratsiya fayli bo'lib, IConfiguration servisi orqali o'qiladi va ilova sozlamalarini boshqarishda ishlatiladi."),

            CreateQuestion("Dependency Injection (DI) da 'Transient' lifetime bilan ro'yxatdan o'tkazilgan servis qachon yaratiladi?",
                new List<string> { 
                    "Servis har safar (inject qilinganda yoki so'ralganda) yangi obyekt instance-i sifatida yaratiladi", 
                    "Har bir HTTP so'rovi uchun bitta obyekt yaratiladi va so'rov oxirida o'chiriladi", 
                    "Ilova ishga tushganda faqat 1 marta yaratiladi va barcha foydalanuvchilar uchun umumiy bo'ladi", 
                    "Faqat foydalanuvchi tizimga kirganida yaratiladi" 
                },
                "Transient eng qisqa umrga ega lifetime bo'lib, har safar servis inject qilganda alohida yangi obyekt yaratiladi."),

            CreateQuestion("Dependency Injection konteynerida 'Scoped' lifetime obyektining umr ko'rish doirasi qaysi javobda to'g'ri ko'rsatilgan?",
                new List<string> { 
                    "Har safar chaqirilganda yangi instance beriladi", 
                    "Yagona bitta HTTP so'rovi doirasida 1 marta yaratiladi va shu so'rov yakunlanguncha barcha joyda bir xil instance ishlatiladi", 
                    "Ilova to'xtagunga qadar xotirada 1 ta instance saqlanadi", 
                    "Faqat ma'lumotlar bazasiga ulangan vaqtda yaratiladi" 
                },
                "Scoped HTTP so'rov darajasidagi lifetime bo'lib, bitta so'rov kelganda yaratiladi va shu so'rov zanjiridagi barcha servislar bir xil xotira namunasidan foydalanadi."),

            CreateQuestion("'Singleton' lifetime bilan ro'yxatdan o'tkazilgan servis haqidagi qaysi fikr to'g'ri?",
                new List<string> { 
                    "Har bir yangi mijoz ulanishi uchun alohida obyekt yaratiladi", 
                    "Birinchi marta so'ralganda 1 marta yaratiladi va butun ilova hayot tsikli davomida barcha so'rovlar uchun yagona instance bo'lib xizmat qiladi", 
                    "U har doim har bir HTTP so'rovidan keyin xotiradan tozalanadi", 
                    "U faqat Transient servislarning ichiga inject qilinishi mumkin" 
                },
                "Singleton ilova davomida yagona xotira nuqtasi bo'lib xizmat qiladi. State saqlaydigan Singleton servislar thread-safety talab qiladi."),

            CreateQuestion("Web API loyihasida mijoz brauzeriga to'g me'ridan-to'g me'ri ochilishi kerak bo'lgan statik fayllar (masalan images/logo.png) qaysi papkada joylashtiriladi?",
                new List<string> { "Controllers/", "appsettings/", "wwwroot/", "bin/Debug/" },
                "wwwroot — ASP.NET Core ilovalarida statik fayllar (web root) uchun ajratilgan standart papka bo'lib, app.UseStaticFiles() middleware-i orqali xizmat ko'rsatadi."),

            CreateQuestion("REST API endpoint-larini interaktiv hujjatlashtirish va brauzer orqali test qilish imkonini beruvchi standart vosita qaysi?",
                new List<string> { "Swagger / OpenAPI (Swashbuckle)", "Entity Framework Core Designer", "Postman CLI Runner", "Kestrel Web Host Manager" },
                "OpenAPI (Swagger) yordamida API struktura va modellaridan avtomatik ravishda interaktiv UI hujjat shakllantiriladi."),

            CreateQuestion("REST API arxitekturasida serverda yangi resurs yaratish uchun qaysi HTTP verbi va HTTP status kodi ishlatilishi standart hisoblanadi?",
                new List<string> { "HTTP GET va 200 OK", "HTTP POST va 201 Created", "HTTP PUT va 204 NoContent", "HTTP PATCH va 304 NotModified" },
                "Yangi resurs yaratish uchun HTTP POST qo'llaniladi. Muvaffaqiyatli yaratilganda server Location header-i bilan birga 201 Created qaytarishi RESTful standartdir."),

            CreateQuestion("Bazadagi mavjud resursni o'chirish uchun mo'ljallangan HTTP metodi qaysi?",
                new List<string> { "HTTP POST", "HTTP GET", "HTTP DELETE", "HTTP OPTIONS" },
                "HTTP DELETE resursni identifikatori bo'yicha o'chirish operatsiyasini bildiradi va 204 No Content yoki 200 OK qaytaradi."),

            CreateQuestion("Action metodida ActionResult<T> ishlatishning oddiy IActionResult ga nisbatan afzalligi nimada?",
                new List<string> { 
                    "U faqat JSON javoblarni qaytaradi, XML formatini taqiqlaydi", 
                    "U strongly-typed obyekt T ni to'g'ridan-to'g'ri qaytarish imkonini beradi hamda OpenAPI (Swagger) hujjatlashtirishda qaytish turini aniq ko'rsatadi", 
                    "U metod ichidagi barcha exception-larni avtomatik ravishda tutib oladi", 
                    "U metodni asinxron holatga avtomatik o'tkazadi" 
                },
                "ActionResult<T> ham aniq tipdagi obyekt T ni, ham HTTP status kodli natijalarni (NotFound, BadRequest) birgalikda qaytarish imkonini beradi."),

            CreateQuestion("Quyidagi yo'nalish shablonida URL manzildan ID parametrini ushlab olish uchun qanday sintaksis ishlatiladi?\n[HttpGet(\"products/{id}\")]",
                new List<string> { "products/$id", "products/{id}", "products/[id]", "products/:id" },
                "ASP.NET Core Routing tizimida dinamik URL parametrlarini belgilash uchun jingalak qavslar {id} sintaksisi qo'llaniladi."),

            CreateQuestion("HTTP status kodi 200 OK nimani anglatadi?",
                new List<string> { 
                    "So'rov kelib tushdi va serverda resurs muvaffaqiyatli yaratildi", 
                    "HTTP so'rovi muvaffaqiyatli bajarildi va kutilgan ma'lumot qaytarildi", 
                    "So'rov sintaktik jihatdan xato yuborilgan", 
                    "So'ralgan sahifa boshqa manzilga ko'chirilgan" 
                },
                "200 OK — standart HTTP javob kodi bo'lib, GET, PUT yoki POST so'rovi muvaffaqiyatli amalga oshirilganini bildiradi."),

            CreateQuestion("Mijoz tomonidan so'ralgan URL resurs serverda topilmaganida qaysi HTTP status kodi qaytariladi?",
                new List<string> { "400 Bad Request", "401 Unauthorized", "404 Not Found", "500 Internal Server Error" },
                "404 Not Found kodi so'ralgan resurs (masalan, ko'rsatilgan ID ga ega mahsulot) ma'lumotlar bazasida yoki server marshrutida mavjud emasligini bildiradi."),

            CreateQuestion("Mijoz autentifikatsiyadan o'tmagan bo'lsa (JWT token yuborilmagan bo'lsa), API qaysi HTTP status kodini qaytarishi kerak?",
                new List<string> { "403 Forbidden", "401 Unauthorized", "400 Bad Request", "405 Method Not Allowed" },
                "401 Unauthorized — shaxsini tasdiqlamagan mijozlarga qaytariladi. 403 Forbidden esa shaxsini tasdiqlagan, lekin ushbu resursga kirish huquqi bo'lmagan foydalanuvchiga qaytariladi."),

            CreateQuestion("ASP.NET Core Minimal API yondashuvida GET so'roviga javob beruvchi endpoint qanday belgilanadi?",
                new List<string> { "app.MapGet(\"/api/hello\", () => \"Hello\");", "app.UseGet(\"/api/hello\", ...);", "app.AddGetRoute(\"/api/hello\", ...);", "app.RegisterEndpoint(\"/api/hello\", ...);" },
                "Minimal API arxitekturasida Controller sinflarisiz to'g'ridan-to'g'ri app.MapGet(), app.MapPost() metodlari orqali yengil endpoint-lar shakllantiriladi."),

            CreateQuestion("Loyihani lokal ishlab chiqish (Development) muhitida qaysi port va parametrlarda ishga tushishini belgilovchi fayl qaysi?",
                new List<string> { "appsettings.Production.json", "Properties/launchSettings.json", "Properties/AssemblyInfo.cs", "global.json" },
                "Properties/launchSettings.json fayli lokal ishlab chiqish jarayonida SSL portlari, applicationUrl va environment o'zgaruvchilarini sozlaydi."),

            CreateQuestion("ASP.NET Core ilovasining joriy ishchi rejimini (Development, Staging, Production) belgilovchi muhit o'zgaruvchisi (Environment Variable) nomi nima?",
                new List<string> { "DOTNET_MODE", "ASPNETCORE_ENVIRONMENT", "APP_STAGE", "CONFIG_ENV" },
                "ASPNETCORE_ENVIRONMENT muhit o'zgaruvchisi ilovaning ishchi rejimini aniqlaydi va shunga mos appsettings.{Environment}.json fayllarini yuklaydi."),

            CreateQuestion("ASP.NET Core 3.0 va undan yuqori versiyalarda o'rnatilgan standart high-performance JSON serializatsiya kutubxona qaysi?",
                new List<string> { "Newtonsoft.Json (Json.NET)", "System.Text.Json", "ServiceStack.Text", "FastJsonParser" },
                "System.Text.Json — Microsoft tomonidan memory allocation va unumdorlikni optimallashtirish maqsadida yaratilgan o'rnatilgan ultra-tezkor JSON kutubxonasidir."),

            CreateQuestion("Controller-based API va Minimal API o'rtasidagi mantiqiy farq qaysi javobda to'g'ri tushuntirilgan?",
                new List<string> { 
                    "Minimal API faqat JSON qaytara oladi, Controller-based esa faqat HTML", 
                    "Controller-based API MVC klassik strukturasi va attribute routing-ga tayanadi; Minimal API esa ortiqcha controller sinflarisiz yengil lambda metodlar bilan ishlaydi", 
                    "Minimal API har doim sekinroq ishlaydi", 
                    "Controller-based API loyihada SQL Server ishlatishni taqiqlaydi" 
                },
                "Controller-based yondashuv yirik, ko'p qatlamli ilovalarni guruhlash uchun qulay; Minimal API esa kichik mikroservislar va yuqori tezlik talab qilinadigan loyihalar uchun mo'ljallangan."),

            CreateQuestion("dotnet CLI vositasi yordamida terminalda yangi ASP.NET Core Web API loyihasini shakllantirish uchun qaysi buyruq ishlatiladi?",
                new List<string> { "dotnet new webapi", "dotnet create api", "dotnet init web", "dotnet generate controller" },
                "dotnet new webapi buyrug'i joriy papkada barcha kerakli Program.cs, .csproj, appsettings.json va namunaviy Controller bilan tayyor Web API shablonini yaratadi."),

            CreateQuestion("Action metodida kelayotgan HTTP POST so'rovi tanasidagi (Request Body) JSON obyektni C# modeliga bog'lash uchun qaysi atribut qo'llaniladi?",
                new List<string> { "[FromQuery]", "[FromBody]", "[FromHeader]", "[FromRoute]" },
                "[FromBody] atributi ASP.NET Core model binder-iga so'rovning HTTP Body qismidagi JSON ma'lumotni o'qib, uni DTO modeliga deserializatsiya qilishni ko'rsatadi."),

            CreateQuestion("URL manzildagi query parametrlarni (masalan: ?page=2&pageSize=10) action metod ko'rsatkichlariga bog'lash uchun qaysi atribut ishlatiladi?",
                new List<string> { "[FromQuery]", "[FromForm]", "[FromBody]", "[FromServices]" },
                "[FromQuery] atributi URL manzildagi ?key=value ko'rinishidagi parametrlarni metod parametrlariga bog'laydi. Ayniqsa filtrlash va sahifalashda ishlatiladi."),

            CreateQuestion(".csproj faylidagi <TargetFramework>net10.0</TargetFramework> tegi nimani belgilaydi?",
                new List<string> { 
                    "Ma'lumotlar bazasi drayveri versiyasini", 
                    "Loyiha kompilyatsiya bo'ladigan va ishlaydigan .NET platformasi maqsadli versiyasini (Target Framework Moniker)", 
                    "Kestrel serverining maksimal ulanishlar sonini", 
                    "Foydalanuvchining brauzer versiyasini" 
                },
                "Target Framework Moniker (TFM) ilova qaysi .NET SDK va runtime versiyasida kompilyatsiya va execution bo'lishini qat'iy belgilaydi.")
        };
    }

    private static List<Question> GenerateDotNetMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("Quyidagi Action Filter kodida OnActionExecuting metodi qachon chaqiriladi va context.Result ga qiymat berilsa nima yuz beradi?\npublic override void OnActionExecuting(ActionExecutingContext context) { context.Result = new BadRequestResult(); }",
                new List<string> { 
                    "Action metod bajarilishidan oldin chaqiriladi va context.Result o'rnatilsa, action metod va keyingi filterlar bajarilmasdan so'rov darhol to'xtatiladi (short-circuit)", 
                    "Action metod bajarilib bo'lingach chaqiriladi va xatolik chiqaradi", 
                    "Faqat ilova ishga tushganda 1 marta bajariladi", 
                    "U faqat foydalanuvchi paroli noto'g'ri bo'lganda ishlaydi" 
                },
                "OnActionExecuting Action metod bajarilishidan avval ishga tushadi. Unda context.Result ga natija berilsa, pipeline short-circuit bo'ladi va Action metod chaqirilmaydi."),

            CreateQuestion("ASP.NET Core-da Exception Filter va Global Middleware Exception Handler (app.UseExceptionHandler) o'rtasidagi asosiy me'moriy farq nima?",
                new List<string> { 
                    "Exception Filter butun HTTP pipeline-dagi (middleware, routing) barcha xatolarni tutadi; Middleware esa faqat controller-dagi xatoni tutadi", 
                    "Exception Filter faqat MVC/Controller action-lar ichida yuz bergan xatolarni tutadi; Global Middleware esa butun HTTP pipeline bo'ylab yuz bergan barcha tutilmagan xatolarni ushlaydi", 
                    "Ular bir xil ishlaydi, hech qanday farq yo'q", 
                    "Middleware faqat SQL xatoliklarini tutadi" 
                },
                "Exception Filter faqat Controller harakatlari doirasida ishlaydi. Global Middleware esa Routing, Auth, Custom Middleware va Controller-lardan chiqadigan barcha xatolarni qamrab oladi."),

            CreateQuestion("Data Annotation atributlari ([Required], [StringLength]) ishlatilganda, ASP.NET Core validation xatoliklarini qachon tekshiradi va u nima qaytaradi?",
                new List<string> { 
                    "Model Binding bosqichida tekshiriladi; [ApiController] atributi bo'lsa avtomatik 400 Bad Request va ModelState xatoliklarini qaytaradi", 
                    "Faqat ma'lumotlar bazasiga save qilinayotganda EF Core tomonidan tekshiriladi", 
                    "Faqat frontend JavaScript kodi orqali brauzerda tekshiriladi", 
                    "Faqat background servisda tekshiriladi" 
                },
                "[ApiController] mavjud bo'lganda Model Binding vaqtida Data Annotations avtomatik bajariladi va xatolik bo'lsa 400 ValidationProblemDetails qaytariladi."),

            CreateQuestion("Custom Middleware yozishda so'rovni quvurdagi (pipeline) keyingi middleware-ga o'tkazish uchun qaysi metod delegati chaqirilishi shart?",
                new List<string> { "await _next(context);", "context.Response.CompleteAsync();", "return Task.CompletedTask;", "context.Skip();" },
                "Custom Middleware-da RequestDelegate _next in'ektsiya qilinadi va await _next(context) orqali so'rov pipeline-dagi keyingi middleware-ga uzatiladi."),

            CreateQuestion("JWT (JSON Web Token) autentifikatsiyasida token xavfsizligi va yaxlitligi (integrity) qanday kafolatlanadi?",
                new List<string> { 
                    "Token ichidagi ma'lumotlar shifrlanadi va uni hech kim o'qiy olmaydi", 
                    "Token Header va Payload qismlari serverdagi Secret Key yordamida HMAC-SHA256 algoritmi bilan raqamli imzo (Signature) bilan muhrlanadi", 
                    "Token har bir so'rovda ma'lumotlar bazasiga saqlanadi", 
                    "Token faqat IP manzil o'zgarmaganda ishlaydi" 
                },
                "JWT stateliss bo'lib, uning Payload qismi (Claims) ochiq Base64URL shaklida bo'ladi, lekin Secret Key bilan yaratilgan raqamli imzo (Signature) token o'zgartirilmaganini kafolatlaydi."),

            CreateQuestion("ASP.NET Core Authorization Policy qanday tarkibiy qismlardan iborat?",
                new List<string> { 
                    "Faqat bitta foydalanuvchi parolidan", 
                    "Bir yoki bir nechta Requirements (talablar) va ularni tekshiruvchi AuthorizationHandler-lardan", 
                    "Faqat Connection String parametridan", 
                    "Faqat IP manzillar ro'yxatidan" 
                },
                "Policy-based Authorization moslashuvchan bo'lib, Requirement (masalan MinimalAgeRequirement) va uni tekshiruvchi Custom AuthorizationHandler bilan birga ishlaydi."),

            CreateQuestion("EF Core-da `dotnet ef migrations add` va `dotnet ef database update` buyruqlari mos ravishda nima bajaradi?",
                new List<string> { 
                    "Birinchisi loyihani kompilyatsiya qiladi, ikkinchisi serverni qayta tushiradi", 
                    "Birinchisi C# Entity o'zgarishlari bo'yicha DDL kod faylini (Migration) yaratadi, ikkinchisi ushbu DDL-ni ma'lumotlar bazasiga tatbiq etadi", 
                    "Birinchisi bazani o'chiradi, ikkinchisi qayta yaratadi", 
                    "Ular faqat In-Memory bazasida ishlaydi" 
                },
                "EF Core Migrations C# entity modellaridagi o'zgarishlarni snapshot qilib Migration fayl yaratadi va update buyrug'i bazaga SQL DDL yuboradi."),

            CreateQuestion("EF Core DbDbContext servisi nima uchun Singleton o'rniga har doim Scoped lifetime bilan ro'yxatdan o'tkazilishi shart?",
                new List<string> { 
                    "Chunki Singleton qilinsa u xotirani juda ko'p egallaydi", 
                    "Chunki DbContext va uning ichidagi ChangeTracker thread-safe emas va bir vaqtning o'zida bir nechta parallel HTTP so'rovlarida ishlatilsa concurrency crash beradi", 
                    "Chunki Singleton DbContext faqat SQL Server bilan ishlaydi", 
                    "Chunki Scoped qilinganda baza paroli har so'rovda yangilanadi" 
                },
                "DbContext thread-safe emas. Agar u Singleton qilinsa, bir vaqtda kelgan ikkita HTTP so'rovi ChangeTracker-ni baravar o'zgartirib InvalidOperationException beradi."),

            CreateQuestion("C# async/await (Task-based Asynchronous Pattern) yondashuvi Web API server unumdorligini qanday oshiradi?",
                new List<string> { 
                    "U C# kodini C++ kodiga o'girib CPU tezligini oshiradi", 
                    "U I/O operatsiyalari (baza so'rovi, HTTP call) bajarilayotgan vaqtda Thread Pool worker iplarini bloklamasdan boshqa so'rovlarga bo'shatib beradi", 
                    "U ma'lumotlar bazasini avtomatik keshlaydi", 
                    "U bir vaqtning o'zida faqat 1 ta so'rovni ishlashni ta'minlaydi" 
                },
                "Asinxron I/O kutish vaqtida (I/O completion port) Thread Pool ipini bloklamaydi, bu esa serverning bir vaqtda minglab parallel so'rovlarni (throughput) qabul qilishiga imkon beradi."),

            CreateQuestion("ASP.NET Core-da `app.UseExceptionHandler()` middleware-i tutilmagan exception yuz berganda odatda qanday javob shakllantiradi?",
                new List<string> { 
                    "Serverni darhol to'xtatadi (shutdown)", 
                    "Mijozga 500 Internal Server Error va xavfsiz (sensitive ma'lumotlar yashiringan) JSON javob (ProblemDetails) qaytaradi", 
                    "Xatolikni brauzerga C# source code satrlari bilan chiqarib beradi", 
                    "Mijoz so'rovini avtomatik ravishda 200 OK ga o'zgartiradi" 
                },
                "UseExceptionHandler production muhitida tutilmagan exception-larni tutadi, loglaydi va mijozga maxfiy server stacktrace-larini ko'rsatmasdan xavfsiz 500 ProblemDetails qaytaradi."),

            CreateQuestion("ProblemDetails (RFC 7807) standarti Web API-da nima uchun muhim?",
                new List<string> { 
                    "U API xatoliklarini unifikatsiyalashgan, bir xil tipdagi JSON obyekti (type, title, status, detail, instance) ko'rinishida taqdim etadi", 
                    "U ma'lumotlar bazasi zaxira nusxasini yaratadi", 
                    "U JWT tokenlarni avtomatik shifrlash uchun kerak", 
                    "U Swagger hujjatini generatsiya qiladi" 
                },
                "RFC 7807 (ProblemDetails) standarti barcha API xatoliklarini bitta standart formatda qaytarishni ta'minlab, frontend va mobile mijozlarga xatolarni oson parser qilish imkonini beradi."),

            CreateQuestion("CORS (Cross-Origin Resource Sharing) xavfsizlik siyosatida Preflight so'rovi (OPTIONS) qachon va nima uchun yuboriladi?",
                new List<string> { 
                    "Har bir GET so'rovidan oldin avtomatik yuboriladi", 
                    "Brauzer tomonidan custom header-lar (Authorization) yoki murakkab HTTP metodlar (PUT/DELETE) ishlatilganda server ruxsatini tekshirish uchun yuboriladi", 
                    "Faqat mobile ilovalardan yuboriladi", 
                    "Faqat fayl yuklanayotganda ishlatiladi" 
                },
                "CORS Preflight (HTTP OPTIONS) so me'rovi brauzer tomonidan haqiqiy so me'rov yuborilishidan avval server ushbu Origin va Header-larga ruxsat berishini tekshirish uchun yuboriladi."),

            CreateQuestion("REST API-da Breaking Change (buzuq o'zgarish) kiritilganda API Versioning strategiyasi nima uchun qo'llaniladi?",
                new List<string> { 
                    "Kodni tezroq kompilyatsiya qilish uchun", 
                    "Mavjud ishlayotgan mijoz ilovalarni (v1) izdan chiqarmay, yangi o'zgarishlarni alohida versiyada (v2) taqdim etish uchun", 
                    "Ma'lumotlar bazasidagi barcha jadvallarni o'chirish uchun", 
                    "Faqat Swagger interfeysini bezash uchun" 
                },
                "API Versioning (URL, Header yoki Query String orqali) ilovaning moslashuvchanligini oshiradi va eski mijozlar v1 bilan ishlayotganda v2 ni parallel rivojlantirish imkonini beradi."),

            CreateQuestion("Web API arxitekturasida DTO (Data Transfer Object) ishlatishning asosiy sababi nima?",
                new List<string> { 
                    "Baza entity modellarining ichki strukturasini va maxfiy maydonlarini (password hash) yashirish hamda faqat kerakli ma'lumotlarni uzatish uchun", 
                    "SQL so'rovlarini tezlashtirish uchun", 
                    "Middleware-larni avtomatik ro'yxatga olish uchun", 
                    "Faqat In-Memory caching uchun" 
                },
                "DTO qatlamlararo ma'lumot uzatishda ishlatilib, Domain Model va DB Entity-larni tashqi dunyodan yashiradi (Over-posting va Under-posting hujumlaridan himoyalaydi)."),

            CreateQuestion("AutoMapper kutubxonasiga nisbatan LINQ Projections (.Select(x => new Dto { ... })) ishlatishning samaradorlik afzalligi nimada?",
                new List<string> { 
                    "Hech qanday afzalligi yo'q, AutoMapper har doim tezroq", 
                    "LINQ Select projection SQL so'roviga faqat kerakli kolonkalarnigina qo'shadi (SQL SELECT col1, col2), AutoMapper esa butun entity-ni xotiraga yuklab keyin map qilishi mumkin", 
                    "LINQ Select so'rovi ma'lumotlar bazasini o'chirib yuboradi", 
                    "AutoMapper faqat C# 7 versiyasida ishlaydi" 
                },
                "EF Core-da `.Select()` proyeksiyasi SQL darajasida faqat kerakli ustunlarni so'rab oladi va tarmoq/xotira sarfini tejaydi. In-Memory AutoMapper esa butun obyektni o'qishini talab qilishi mumkin."),

            CreateQuestion("Repository Pattern ishlatishning asosiy savdo-sotiq (trade-off) va afzallik tomoni nimada?",
                new List<string> { 
                    "U ma'lumotlar bazasiga murojaatni abstraktsiya qilib unit-test yozishni osonlashtiradi, lekin EF Core tayyor DbContext (Unit of Work) ustidan ortiqcha abstraksiya qatlami qo'shishi mumkin", 
                    "U ma'lumotlar bazasi tezligini 10 barobarga oshiradi", 
                    "U Swagger hujjatini avtomatik o'chirib qo'yadi", 
                    "U faqat NoSQL bazalar bilan ishlaydi" 
                },
                "Repository Pattern ORM-ni biznes mantiqdan ajratadi va Moq testlashni osonlashtiradi. Biroq EF Core DbDbContext o'zi Repository/UnitOfWork bo'lgani uchun ba'zan redundant abstraksiya hisoblanadi."),

            CreateQuestion("ASP.NET Core-da to'g'ridan-to'g'ri `new HttpClient()` yaratish o'rniga `IHttpClientFactory` ishlatish nimaning oldini oladi?",
                new List<string> { 
                    "Memory leak va Socket Exhaustion (amaldagi ulanishlar tugashi va TIME_WAIT) muammolarining", 
                    "SQL deadlock xatoliklarining", 
                    "JWT token muddati tugashining", 
                    "Routing xatolarining" 
                },
                "HttpClient noto'g'ri dispose qilinganda OS socket-larini yopmaydi (Socket Exhaustion). IHttpClientFactory ichki HttpMessageHandler-larni pool qilib, socket-larni samarali qayta ishlatadi."),

            CreateQuestion("ILogger<T> bilan structured logging (masalan: `_logger.LogInformation(\"User {UserId} logged in\", userId)`) ishlatishning string interpolation (`$\"User {userId}...\"`) ga nisbatan afzalligi nima?",
                new List<string> { 
                    "String interpolation ishlatilsa log fayl o'chib ketadi", 
                    "Structured logging log matnini va parametrlarni JSON shaklida (Serilog/Elasticsearch) saqlaydi, bu esa keyinchalik ma'lumotlarni oson qidirish va tahlil qilish imkonini beradi", 
                    "ILogger string interpolation-ni umuman qo'llab-quvvatlamaydi", 
                    "Strukturaviy loglash fayl hajmini 100 baravar kattalashtiradi" 
                },
                "Structured logging parametrlarni alohida kalit-qiymat (Key-Value) sifatida saqlaydi. Bu Elasticsearch va Seq kabi monitoring tizimlarida mantiqiy filtrlar o'tkazishga imkon beradi."),

            CreateQuestion("ASP.NET Core Health Checks va Kubernetes Liveness/Readiness probe-lari o'rtasidagi integratsiya maqsadi nima?",
                new List<string> { 
                    "Kubernetes ilovaning /healthz endpoint-iga qarab, container nosog'lom bo'lsa (Unhealthy) uni avtomatik restart qilish yoki trafikni yo'naltirmaslik uchun", 
                    "Faqat dasturchiga SMS xabarnoma yuborish uchun", 
                    "Ma'lumotlar bazasi parolini o'zgartirish uchun", 
                    "JWT tokenlarni tekshirish uchun" 
                },
                "Kubernetes Health Probes API-ning HealthCheck endpoint-larini (DB, Redis ulanishini) doimiy so'rab turadi. Agar DB uzilsa, pod-ga yangi trafik yubormaydi."),

            CreateQuestion("IMemoryCache ishlatilganda Cache Stampede (bir vaqtning o'zida kesh tugab minglab so'rovlar bazaga urilishi) muammosining oldi qanday olinadi?",
                new List<string> { 
                    "Keshni umuman ishlatmaslik orqali", 
                    "SemaphoreSlim lock yoki GetOrCreateAsync mos keluvchi lock mexanizmlari bilan faqat 1 ta birinchi so'rovga bazadan o me'qishga ruxsat berish orqali", 
                    "Faqat CPU sonini oshirish orqali", 
                    "Kesh muddatini cheksiz qilish orqali" 
                },
                "Cache Stampede (yoki thundering herd) kesh muddati tugaganda yuz beradi. SemaphoreSlim kabi thread-locking usuli bilan faqat 1 ta thread bazaga boradi va keshni yangilaydi."),

            CreateQuestion("ASP.NET Core `[ResponseCache]` atributi qaysi HTTP header-lari orqali mijoz va proksi serverlarda keshni boshqaradi?",
                new List<string> { "Authorization va Bearer", "Cache-Control, Vary va Pragma", "Content-Type va Accept", "X-Forwarded-For" },
                "[ResponseCache] atributi HTTP standarti bo'yicha `Cache-Control: public, max-age=60` kabi sarlavhalarni shakllantirib, mijoz va CDN-larga kesh qoidasini uzatadi."),

            CreateQuestion(".NET 7+ da taqdim etilgan o'rnatilgan Rate Limiting middleware-ida `Fixed Window` va `Sliding Window` algoritmlari o'rtasidagi asosiy farq nima?",
                new List<string> { 
                    "Fixed Window faqat GET so'rovlarni cheklaydi, Sliding Window esa POST so'rovlarni", 
                    "Fixed Window vaqt darchasi chegarasida (window boundary) so me'rovlar bursting (quyulib kelishi) muammosiga ega; Sliding Window esa darchani kichik segmentlarga bo'lib bir tekis cheklaydi", 
                    "Ular bir xil ishlaydi", 
                    "Sliding Window faqat Redis bo'lganda ishlaydi" 
                },
                "Fixed Window darcha yangilanganda daqiqaning oxiri va boshida ikki baravar so'rov o'tkazib yuborishi mumkin (burst). Sliding Window esa segmentlar bo'yicha silliq oqim ta'minlaydi."),

            CreateQuestion("REST API-da katta ma'lumotlar to'plamini sahifalashda Offset-based (Skip/Take) va Cursor-based (Keyset) pagination o'rtasidagi unumdorlik farqi nimada?",
                new List<string> { 
                    "Offset-based sahifalash (Skip(100000)) SQL darajasida avvalgi 100,000 qatorni baribir o'qib chiqadi va sekinlashadi; Keyset pagination esa indekslangan kalit (WHERE Id > lastId) orqali O(1) tezlikda ishlaydi", 
                    "Offset-based har doim tezroq ishlaydi", 
                    "Keyset pagination faqat In-Memory bazada ishlaydi", 
                    "Ular orasida unumdorlik farqi yo'q" 
                },
                "EF Core-da `Skip(OFFSET)` katta qiymatlarda sekinlashadi. Keyset (Cursor) pagination oxirgi ko'rilgan id bo'yicha indekslangan `WHERE Id > @lastId LIMIT @pageSize` bajaradi va o'ta tez ishlaydi."),

            CreateQuestion("FluentValidation kutubxonasining standart ASP.NET Core Data Annotations ([Required]) ga nisbatan asosiy arxitekturaviy afzalligi nima?",
                new List<string> { 
                    "U validatsiya mantiqini DTO/Domain obyektlaridan ajratib, alohida sinflarda (Single Responsibility Principle) strongly-typed va murakkab shartli qoidalar yozish imkonini beradi", 
                    "U validatsiyasiz so'rovlarni qabul qiladi", 
                    "U faqat SQL Server bilan ishlaydi", 
                    "U loyiha hajmini kichraytiradi" 
                },
                "FluentValidation validation qoidalarini model sinfidan ajratib toza kod beradi va murakkab biznes qoidalarini (masalan: `.When(x => x.IsActive)`) oson implement qilishga yordam beradi."),

            CreateQuestion("ASP.NET Core Options Pattern-da `IOptions<T>`, `IOptionsSnapshot<T>` va `IOptionsMonitor<T>` o'rtasidagi farq nima?",
                new List<string> { 
                    "IOptions Singleton bo'lib appsettings o'zgarishini sezmaydi; IOptionsSnapshot Scoped bo'lib har HTTP so'rovda yangi qiymatni o'qiydi; IOptionsMonitor Singleton bo me'lib real-vaqtda o'zgarishni tutadi", 
                    "Ular bir xil ishlaydi, faqat nomlanishi har xil", 
                    "IOptionsSnapshot faqat testlarda ishlatiladi", 
                    "IOptionsMonitor faqat XML fayllar bilan ishlaydi" 
                },
                "IOptions ilova ishga tushganda 1 marta o'qiladi. IOptionsSnapshot har bir HTTP request uchun appsettings faylidagi yangi o'zgarishni oladi. IOptionsMonitor esa OnChange event-i bilan real-vaqtda bildiradi."),

            CreateQuestion("`IHostedService` va `BackgroundService` yordamida yaratilgan fon xizmatlari (Background Tasks) haqida qaysi tasdiq to'g'ri?",
                new List<string> { 
                    "Ular faqat foydalanuvchi sahifani yangilaganda ishlaydi", 
                    "Ular ASP.NET Core web ilovasi bilan birga ishga tushib, orqa fonda asinxron tsikl (ExecuteAsync) ko'rinishida doimiy vazifalarni bajaradi", 
                    "Ular har doim alohida protsess (EXE) sifatida ishga tushishi shart", 
                    "Ular HTTP so'rovlarini qabul qila oladi" 
                },
                "BackgroundService — IHostedService-ning mavhum sinfi bo'lib, CancellationToken bilan boshqariladigan uzoq muddatli fon vazifalarini (masalan, navbatdagi xabarlarni qayta ishlash) bajaradi."),

            CreateQuestion("Custom Model Binder (`IModelBinder`) yaratish qaysi vaziyatda zarur bo'ladi?",
                new List<string> { 
                    "Standart model binder kelayotgan nodatiy formatdagi (masalan, vergul bilan ajratilgan string \"1,2,3\" ni List<int> ga) ma'lumotni bog'lay olmaganda", 
                    "Faqat JSON body-ni o'qish uchun", 
                    "Faqat JWT tokenni tekshirish uchun", 
                    "SQL so'rovlarini optimallashtirish uchun" 
                },
                "IModelBinder interface-i custom bog'lash mantiqini yozishga imkon beradi (masalan, Header yoki Complex Query-dagi maxsus string-ni C# obyektiga o'girish)."),

            CreateQuestion("HTTP Content Negotiation jarayonida mijoz serverdan qaysi HTTP Header orqali javob formatini (JSON yoki XML) talab qiladi?",
                new List<string> { "Content-Type", "Accept", "User-Agent", "Host" },
                "Mijoz `Accept: application/xml` yuborganda Content Negotiation vositasi agar XML Formatter ro'yxatdan o'tgan bo'lsa javobni XML shaklida qaytaradi. `Content-Type` esa so'rov tanasi formatini bildiradi."),

            CreateQuestion("EF Core-da `.AsNoTracking()` metodini qo'llash qachon va nima uchun maslahat beriladi?",
                new List<string> { 
                    "Ma'lumotlar bazasiga yangi obyekt qo'shayotganda", 
                    "Faqat o'qish uchun (Read-only) bajariladigan LINQ so'rovlarida DbContext ChangeTracker xotirasini tejash va tezlikni oshirish uchun", 
                    "Ma'lumotni bazadan o'chirib yuborish uchun", 
                    "Migratsiyalarni o'tkazish uchun" 
                },
                "AsNoTracking() o'qilgan obyektlarni ChangeTracker snapshot-larida saqlamaydi. Bu xotira sarfini kamaytiradi va ko'p hajmdagi o'qish so'rovlarini sezilarli tezlashtiradi."),

            CreateQuestion("Role-based va Claims-based authorization o'rtasidagi farq nima?",
                new List<string> { 
                    "Role-based faqat statik guruhlarga (Admin/User) tayanadi; Claims-based esa foydalanuvchining shaxsiy da'volari (Email, Department, Permission) bo'yicha moslashuvchan ruxsat beradi", 
                    "Role-based faqat Linux-da ishlaydi", 
                    "Claims-based faqat parollarni tekshiradi", 
                    "Ular o'rtasida hech qanday farq yo'q" 
                },
                "Role-based authorization oddiy rol nomlariga tayanadi. Claims-based va Policy-based esa foydalanuvchiga tegishli har qanday atribut va da'volar (Claims) bo'yicha nozik ruxsatlarni beradi.")
        };
    }

    private static List<Question> GenerateDotNetHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("High-performance aloqada gRPC va REST (JSON over HTTP/1.1) o'rtasidagi asosiy arxitekturaviy va unumdorlik farqi nimada?",
                new List<string> { 
                    "gRPC HTTP/2 bitta TCP ulanishida Multiplexing va ixcham binary Protobuf serializatsiyasini ishlatadi; REST esa o'qilishi qimmat bo'lgan text-based JSON va HTTP/1.1 dan foydalanadi", 
                    "REST har doim gRPC-dan tezroq ishlaydi", 
                    "gRPC faqat brauzerlar ichida ishlaydi, mobilda ishlamaydi", 
                    "gRPC JSON formatini ishlatadi, REST esa binary formatni" 
                },
                "gRPC HTTP/2 ustida strongly-typed Contract-First (.proto) va o'ta ixcham Protobuf binary serializatsiyasidan foydalanib, mikroservislar o'rtasidagi tarmoq kechikishini keskin kamaytiradi."),

            CreateQuestion("SignalR kutubxonasida taqdim etiladigan transport turlari (WebSockets, Server-Sent Events, Long Polling) qanday tartibda tanlanadi?",
                new List<string> { 
                    "Faqat dasturchi tomonidan hardcode qilinadi", 
                    "SignalR avval eng afzal bo'lgan WebSockets-ni sinaydi, agar mijoz yoki tarmoq/proxy uni qo'llab-quvvatlamasa mos ravishda SSE yoki Long Polling-ga avtomatik o'tadi (Fallback)", 
                    "Har doim birinchi bo'lib Long Polling tanlanadi", 
                    "SignalR faqat WebSockets-da ishlaydi, boshqasini qo'llamaydi" 
                },
                "SignalR transport negotiation mexanizmiga ega: Duplex WebSockets -> Server-Sent Events -> Long Polling zanjiri bo'yicha eng yaxshi mavjud aloqa kanalini o me'zi tanlaydi."),

            CreateQuestion("Klassik In-Memory Cache (IMemoryCache) va Distributed Cache (Redis) o'rtasidagi tanlov mezoni ko'p tugunli (Multi-node Load Balanced) muhitda nimaga tayanadi?",
                new List<string> { 
                    "IMemoryCache har doim bir nechta serverlar o'rtasida ma'lumotni sinxronlab turadi", 
                    "Multi-node muhitida IMemoryCache ishlatilsa serverlar o'rtasida kesh inki konsistentlik (Sticky sessions bo'lmasa) buziladi; Redis esa barcha serverlar uchun markazlashgan yagona kesh beradi", 
                    "Redis faqat bitta serverda ishlay oladi", 
                    "Distributed Cache xotiradan foydalanmaydi, faqat diskda ishlaydi" 
                },
                "Load Balancer orqasidagi ko'p instansiyali mikroservislarda IMemoryCache har bir server RAM-ida har xil ma'lumot saqlab qo'yadi. Redis kabi Distributed Cache markaziy va kesh ko'p instansiyalar uchun umumiy bo'ladi."),

            CreateQuestion("Circuit Breaker pattern (Polly kutubxonasi) tashqi servis nosoz bo me'lganda tizimni qanday holatlarga (States) o'tkazib himoya qiladi?",
                new List<string> { 
                    "Closed (normal) -> Open (nosozlikda so'rovlarni darhol to'xtatadi) -> Half-Open (muayyan vaqtdan keyin sinov so'rovlarini o'tkazib ko'radi)", 
                    "Active -> Passive -> Inactive", 
                    "Start -> Retry -> Stop", 
                    "Pending -> Executing -> Completed" 
                },
                "Circuit Breaker buzilgan tashqi servisga tinimsiz so'rov yuborib server resurslarini zoe ketkazmaslik uchun zanjirni uzadi (Open). Sinov so'rovi muvaffaqiyatli bo'lsa u yana yopiladi (Closed)."),

            CreateQuestion("`BackgroundService` sinfida `ExecuteAsync(CancellationToken stoppingToken)` metodida cheksiz tsikl yozganda nima uchun `stoppingToken` e'tiborga olinishi shart?",
                new List<string> { 
                    "Chunki stoppingToken bo'lmasa kesh tozalanmaydi", 
                    "Ilova to'xtatilganda (Shutdown/SIGTERM) background task zudlik bilan tsikldan chiqishi va thread blocking berib server yopilishini dead-lock qilmasligi uchun", 
                    "stoppingToken faqat SQL Server bilan ishlaydi", 
                    "U faqat xatoliklarni loglash uchun kerak" 
                },
                "Graceful Shutdown vaqtida web server BackgroundService-larga CancellationToken yuboradi. `stoppingToken.IsCancellationRequested` tekshirilmasa yoki `Task.Delay(..., stoppingToken)` berilmasa, container yopilayotganda osilib qoladi."),

            CreateQuestion("`System.Threading.Channels` kutubxonasidagi `Channel<T>` texnologiyasining an'anaviy ConcurrentQueue<T> ga nisbatan afzalligi nimada?",
                new List<string> { 
                    "U faqat string ma me'lumotlarni saqlaydi", 
                    "Channel Producer-Consumer modelida asinxron kutish (await ReadAsync), Backpressure (xotira to'lib ketmasligi uchun xotira chegaralash) va Thread-safe oqim ta'minlaydi", 
                    "U ma me'lumotlarni avtomatik bazaga yozadi", 
                    "ConcurrentQueue faqat .NET Framework-da mavjud" 
                },
                "System.Threading.Channels yuqori unumli in-memory navbat bo'lib, Producer va Consumer o'rtasida xotirani to'ldirmaydigan asinxron backpressure mexanizmini beradi."),

            CreateQuestion("Stateless JWT sistemalarida Refresh Token Rotation va Blacklisting mexanizmi nima uchun zarur?",
                new List<string> { 
                    "Faqat JSON formatini siqish uchun", 
                    "O'g'irlangan yoki muddati tugagan Access Token-larni bekor qilish va Refresh Token har bir ishlatilganda yangisiga almashtirilib, o'g'irlangan tokenlarni tezda fosh etish uchun", 
                    "Ma'lumotlar bazasi parolini shifrlash uchun", 
                    "JWT token hajmini kattalashtirish uchun" 
                },
                "JWT o'zi stateless bo'lgani uchun uni serverdan bekor qilib bo'lmaydi. Refresh Token Rotation va DB/Redis revocation list orqali xavfsizlik va sessiyani nazorat qilish imkoniyati beriladi."),

            CreateQuestion("OAuth2 va OpenID Connect (OIDC) protokollari o'rtasidagi fundamental farq nima?",
                new List<string> { 
                    "OAuth2 — bu Autentifikatsiya protokoli, OpenID Connect — bu Avtorizatsiya protokoli", 
                    "OAuth2 — bu resurslarga ruxsat berish (Authorization/Access Token) protokoli; OpenID Connect esa uning ustiga qurilgan shaxsni tasdiqlash (Authentication/ID Token) qatlamidir", 
                    "Ular mutlaqo bir xil narsa", 
                    "OpenID Connect faqat mobil qurilmalarda ishlaydi" 
                },
                "OAuth2 delegated authorization beradi (Access Token). OpenID Connect esa OAuth 2.0 ustiga ID Token (JWT) qo'shib autentifikatsiya (foydalanuvchi kimgaligini tasdiqlash) beradi."),

            CreateQuestion("Custom `AuthorizationHandler<TRequirement>` yaratish qaysi vaziyatda talab qilinadi?",
                new List<string> { 
                    "Faqat foydalanuvchi Admin yoki User ekanligini tekshirishda", 
                    "Resurs darajasidagi (Resource-based) yoki murakkab dinamik biznes qoidalarini (masalan: foydalanuvchi faqat o'zi yaratgan hujjatni tahrirlay olishi) tekshirishda", 
                    "Faqat IP manzil bo'yicha bloklashda", 
                    "Swagger hujjatini sozlashda" 
                },
                "Resource-based va Policy AuthorizationHandler kelayotgan resurs konteksti (Document) va foydalanuvchi Claims-larini solishtirib nozik biznes ruxsatlarini baholaydi."),

            CreateQuestion("ASP.NET Core autentifikatsiya tizimida `ClaimsPrincipal` obyektining tuzilishi qanday?",
                new List<string> { 
                    "U bitta kalit so'zdan iborat string", 
                    "U bir yoki bir nechta `ClaimsIdentity` lardan va har bir Identity o'z navbatida ko me'plab `Claim` (Type-Value juftliklari) lardan iborat bo'ladi", 
                    "U faqat foydalanuvchi ID-sini saqlaydi", 
                    "U ma'lumotlar bazasi jadvali hisoblanadi" 
                },
                "ClaimsPrincipal foydalanuvchi shaxsini ifodalaydi. U bir nechta identifikatorlarni (Passport, DriverLicense kabi ClaimsIdentity) va ularga tegishli Claim-larni saqlaydi."),

            CreateQuestion("Entity Framework Core-da **N+1 so'rovlar muammosi** nima va u LINQ-da qanday bartaraf etiladi?",
                new List<string> { 
                    "N+1 — bu bazaga ulanishlar soni 100 tadan oshgandagi xatolik; u AsNoTracking bilan hal etiladi", 
                    "Asosiy obyektni o'qib, tsikl ichida uning bog me'liq har bir bolalar to me'plamini alohida SQL bilan o'qiganda yuz beradi; u `.Include()` (Eager Loading) yoki `.Select()` proyeksiyasi bilan hal etiladi", 
                    "U faqat SQL Server-da yuz beradi", 
                    "U DbContext-ni o me'chirish bilan hal etiladi" 
                },
                "N+1 so'rovlar muammosi 1 ta ota so me'rov va N ta bola so'rovlar natijasida bazani zo'riqtiradi. Undan qutulish uchun `.Include()` (JOIN) yoki `.Select()` proyeksiya ishlatiladi."),

            CreateQuestion("EF Core-da `EF.CompileAsyncQuery` (Compiled Queries) ishlatishning unumdorlik afzalligi nimada?",
                new List<string> { 
                    "U ma me'lumotlar bazasidagi jadvallarni siqib beradi", 
                    "U tez-tez takrorlanadigan LINQ query expression tree-ni o me'qish va SQL ga translation qilish xarajatini 1 marta bajarib keshlaydi va ijroni tezlashtiradi", 
                    "U faqat In-Memory bazada ishlaydi", 
                    "U DbContext-ni majburiy Singleton qiladi" 
                },
                "EF Core har safar LINQ so'rov kelganda Expression Tree-ni tahlil qilib SQL tuzadi. Compiled Queries ushbu parsing/compilation bosqichini keshlab tezlikni oshiradi."),

            CreateQuestion("PostgreSQL / SQL Server bilan ishlashda Connection Pooling limitidan oshib ketish (Pool Exhaustion) sababi nima va u qanday oldini olinadi?",
                new List<string> { 
                    "Sababi DbContext yoki NpgsqlConnection ob'ektlarini to'g'ri dispose qilmaslik (connection leak); oldini olish uchun DbContext-ni DI orqali Scoped ishlatish va uning umrini qisqa tutish kerak", 
                    "Sababi RAM yetishmasligi; oldini olish uchun faqat Singleton DbContext ishlatish kerak", 
                    "Sababi fayl hajmi kattaligi", 
                    "Sababi Swagger yoqilganligi" 
                },
                "Connection Leak kodda DbContext yoki DataReader open holatda yopilmay qolganda yuz beradi. Pool exhausted bo'lganda yangi so'rovlar ulanish kutib vaqt tugab (timeout) yiqiladi."),

            CreateQuestion("Stateless Web API mikroservislarida Horizontal Scaling (Scale-out) amalga oshirilganda sessiya holati (Session State) qanday saqlanishi kerak?",
                new List<string> { 
                    "Har bir server o'z in-memory xotirasida saqlashi kerak", 
                    "API serverlari mantiqan Stateless bo'lishi kerak yoki Session State markazlashgan Distributed Cache (Redis) da saqlanishi shart", 
                    "Sessiyalar local C drive faylida saqlanishi kerak", 
                    "Horizontal scaling-da sessiyadan foydalanib bo'lmaydi" 
                },
                "Scale-out (bir nechta API container-lari) da mijoz so'rovi istalgan instansiyaga tushishi mumkin. Shuning uchun API serverlar Stateless bo'lishi yoki Redis da umumiy state saqlashi shart."),

            CreateQuestion("Microservice arxitekturasida Load Balancer nosog'lom instansiyaga trafik yubormasligi uchun nima qiladi?",
                new List<string> { 
                    "U instansiyaning Health Check (Readiness probe) HTTP endpoint-ini davriy so'rab turadi va muvaffaqiyatsiz bo'lsa uni rotatsiyadan chiqaradi", 
                    "U kodni o'zgartirib qayta kompilyatsiya qiladi", 
                    "U ma'lumotlar bazasini o me'chirib yoqadi", 
                    "U faqat har kuni kechasi 1 marta tekshiradi" 
                },
                "Load Balancer (Nginx/HAProxy/K8s Ingress) Readiness Probe orqali serverning 200 OK qaytarishini tekshiradi. Agar 500 yoki Timeout bo'lsa u instansiyaga yangi so'rov yo'naltirmaydi."),

            CreateQuestion("OpenTelemetry va Distributed Tracing texnologiyasida mikroservislar bo'ylab bitta HTTP so'rovining yo'lini kuzatish uchun qaysi W3C Header standarti uzatiladi?",
                new List<string> { "Authorization: Bearer", "traceparent (TraceId, SpanId)", "Content-Encoding: gzip", "X-Cache-Status" },
                "W3C Trace Context standarti bo'yicha `traceparent` (TraceId-SpanId-Sampled) header-i xizmatlar o'rtasida uzatilib, bitta so'rovning barcha mikroservislardagi umumiy izini (Trace) yig'adi."),

            CreateQuestion("Server-Sent Events (SSE) va WebSockets aloqa protokollari o'rtasidagi asosiy farq nima?",
                new List<string> { 
                    "SSE — bu HTTP ustida ishlaydigan bir tomonlama (Unidirectional: Server -> Client) oqim; WebSocket — bu bitta TCP ulanishida to'liq ikki tomonlama (Full-Duplex) aloqa", 
                    "SSE faqat fayllarni yuklash uchun ishlaydi", 
                    "WebSockets bir tomonlama aloqa beradi", 
                    "Ular mutlaqo bir xil protokol" 
                },
                "SSE standart HTTP protokoli orqali serverdan mijozga matnli ma'lumotlarni uzatadi (masalan, narxlar o'zgarishi). WebSockets esa past darajadagi ikki tomonlama duplex aloqadir."),

            CreateQuestion("ASP.NET Core-da Custom `IModelBinderProvider` yaratish qaysi me'moriy vaziyatda ishlatiladi?",
                new List<string> { 
                    "Ma'lum bir tipga yoki maxsus Custom Atributga ega bo'lgan barcha model ko'rsatkichlari uchun global Model Binder-ni dynamically tanlash va ro'yxatdan o me'tkazish uchun", 
                    "Faqat Controller nomini o'zgartirish uchun", 
                    "SQL so'rovlarini tahlil qilish uchun", 
                    "Faqat static fayllar uchun" 
                },
                "ModelBinderProvider kelayotgan ModelBinderProviderContext (Type, Metadata) bo'yicha qaysi Custom ModelBinder qo'llanilishini dinamik aniqlaydi va ro'yxatga oladi."),

            CreateQuestion("Response Compression (Gzip / Brotli) middleware-ini Web API-da yoqishda qaysi muhim jihatga e'tibor berish kerak?",
                new List<string> { 
                    "Siqish faqat fayllar uchun ishlaydi, JSON uchun ishlamaydi", 
                    "Siqish tarmoq hajmini kamaytirgani bilan server CPU va xotira yuklamasini oshiradi, shuningdek allaqachon siqilgan fayllarga (PNG, JPEG, ZIP) qo'llamaslik kerak", 
                    "Response compression ishlatilganda HTTP status kodlari ishlamaydi", 
                    "Brotli siqish faqat HTTP/1.0 da ishlaydi" 
                },
                "Response Compression JSON javoblarini siqib tarmoq o'tkazuvchanligini oshiradi, lekin CPU sarflaydi. Kichik javoblar yoki allaqachon siqilgan media fayllar uchun uni qo'llash samarasiz."),

            CreateQuestion("HTTP/2 va HTTP/1.1 o'rtasidagi **Multiplexing** imkoniyati nimani anglatadi?",
                new List<string> { 
                    "Bir nechta parallel HTTP so'rov va javoblarni bitta TCP ulanishi ustida bir vaqtda (Head-of-Line blocking-siz) uzatish imkoniyati", 
                    "Faqat ma'lumotlar bazasi jadvallarini birlashtirish", 
                    "So'rovlarni bir nechta portlarga taqsimlash", 
                    "Faqat parollarni shifrlash imkoniyati" 
                },
                "HTTP/1.1 da har bir so me'rov uchun alohida TCP ulanish kerak edi (yoki pipeline blocking). HTTP/2 Multiplexing esa bitta TCP ulanishida ko'plab stream-larni parallel uzatadi."),

            CreateQuestion("Taqsimlangan tizimlarda **Idempotency Key Pattern** (masalan, to'lov API-larida) nima uchun zarur?",
                new List<string> { 
                    "HTTP POST so'rovi tarmoq uzilishi sababli mijoz tomonidan takroriy yuborilganda (Retry), serverda amal faqat 1 marta bajarilishini va takroriy to'lov yechilmasligini kafolatlash uchun", 
                    "JWT token muddatini uzaytirish uchun", 
                    "Swagger hujjatini shifrlash uchun", 
                    "Faqat GET so'rovlarida ma'lumotni keshga solish uchun" 
                },
                "Idempotency Key (masalan, Header: `X-Idempotency-Key: uuid`) yuborilganda server Redis/DB-da uning bajarilganini tekshiradi va takroriy so'rovga qayta to'lov qilmay oldingi javobni beradi."),

            CreateQuestion("Microsoft **YARP (Yet Another Reverse Proxy)** vositasi ASP.NET Core-da nima uchun ishlatiladi?",
                new List<string> { 
                    "Faqat ma'lumotlar bazasi migratsiyalari uchun", 
                    "ASP.NET Core ekotizimida yuqori unumdorlikka ega, sozlanuvchan API Gateway, Load Balancer va Reverse Proxy qurish uchun", 
                    "Frontend UI komponentlarini yaratish uchun", 
                    "Faqat unit-testlar yozish uchun" 
                },
                "YARP — Microsoft-ning o'ta tezkor .NET reverse proxy kutubxonasi bo'lib, uning yordamida custom routing, rate-limiting, auth va load balancing-ga ega API Gateway yaratiladi."),

            CreateQuestion("ASP.NET Core-da integratsion testlar yozishda `WebApplicationFactory<TEntryPoint>` sinfining o'rni nimada?",
                new List<string> { 
                    "U test muhitida in-memory TestServer va TestHttpClient yaratib, butun HTTP pipeline va DI servislarini xotirada ishga tushirib test qilish imkonini beradi", 
                    "U faqat ma'lumotlar bazasini tozalash uchun kerak", 
                    "U HTML sahifalarini brauzerda ochib beradi", 
                    "U faqat unit testlarda Moq sinflar yaratadi" 
                },
                "WebApplicationFactory haqiqiy HTTP server va tarmoq portini band qilmasdan, xotirada (In-Memory) butun Web API ilovangizni ishga tushirib integratsion test o'tkazishga yordam beradi."),

            CreateQuestion("ASP.NET Core 7+ dagi **Output Caching** va klassik **Response Caching** o'rtasidagi asosiy me'moriy farq nima?",
                new List<string> { 
                    "Output Caching server-side kesh platformasi bo'lib, keshni server xotirasida/Redis-da saqlaydi hamda Tag-based eviction (EvictByTagAsync) va Custom Policy-larni qo'llab-quvvatlaydi", 
                    "Response Caching faqat POST so'rovlar uchun ishlaydi", 
                    "Output Caching faqat brauzer ichida keshlaydi", 
                    "Ular orasida hech qanday farq yo'q" 
                },
                "Response Caching faqat HTTP Header-larga tayansa, Output Caching (.NET 7+) server tomonidagi to'liq kesh mexanizmidir va keshni ma'lum bir teglari (`EvictByTagAsync(\"products\")`) bo'yicha bekor qilish imkonini beradi."),

            CreateQuestion("Minimal API va Controller-based API o'rtasidagi unumdorlik (Performance) farqi nimadan kelib chiqadi?",
                new List<string> { 
                    "Minimal API-da MVC controller-lariga tegishli Reflection, Action Descriptor-lar va ortiqcha Filter pipeline yuklamalari yo'qligi sababli so'rovlar tezroq va yengilroq ishlanadi", 
                    "Controller-based API har doim tezroq ishlaydi", 
                    "Minimal API faqat SQL Server bo'lganda tez ishlaydi", 
                    "Farq faqat fayl hajmidadir" 
                },
                "Minimal API MVC-ning og'ir Reflection va Filter infrastructure-sidan holi bo'lgani uchun kamroq Memory Allocation qiladi va yuqoriroq RPS (Requests Per Second) beradi."),

            CreateQuestion("ASP.NET Core HTTP Request Pipeline sozlanayotganda `UseAuthentication()` va `UseAuthorization()` middleware-larining tartibi nima uchun kritik muhim?",
                new List<string> { 
                    "Chunki UseAuthorization avval chaqirilsa, u hali foydalanuvchi kimligi (ClaimsPrincipal) aniqlanmasdan turib ruxsatlarni tekshirishga urinadi va 401/403 beradi", 
                    "Tartib mutlaqo ahamiyatsiz", 
                    "UseAuthorization har doim birinchi turishi kerak", 
                    "Ular faqat CORS-dan keyin kelishi kerak" 
                },
                "HTTP Pipeline-da UseAuthentication avval kelishi va so'rovdagi JWT/Cookie-ni tekshirib `HttpContext.User` ni to'ldirishi kerak. Shundan keyingina UseAuthorization ushbu User claims-larini tekshiradi."),

            CreateQuestion("gRPC protokolidagi 4 ta muloqot rejimiga qaysilar kiradi?",
                new List<string> { 
                    "Unary, Server Streaming, Client Streaming, Bidirectional Streaming", 
                    "GET, POST, PUT, DELETE", 
                    "Sync, Async, Batch, Queue", 
                    "Request, Response, Publish, Subscribe" 
                },
                "gRPC HTTP/2 ustida 4 xil aloqa rejimini beradi: Unary (1:1), Server Streaming (1:N), Client Streaming (N:1) va Bidirectional Streaming (N:M)."),

            CreateQuestion("Taqsimlangan mikroservislar tizimida **Eventual Consistency** (Natijaviy konsistentlik) konsepti nimani anglatadi?",
                new List<string> { 
                    "Ma'lumotlar asinxron event-driven aloqa (masalan, RabbitMQ/Kafka) orqali vaqt o'tishi bilan barcha servis bazalarida bir xil holatga keladi, lekin bu darhol atomik sodir bo'lmaydi", 
                    "Ma'lumotlar darhol ACID tranzaksiya bilan barcha bazalarda bir vaqtda yangilanadi", 
                    "Ma'lumotlar hech qachon sinxronlanmaydi", 
                    "U faqat bitta monolit bazada ishlaydi" 
                },
                "CAP teoremasiga ko'ra taqsimlangan tizimlarda yuqori mavjudlik (Availability) uchun Eventual Consistency tanlanadi: xabar navbat orqali yetib borgach ma'lumotlar yakuniy konsistent holatga keladi."),

            CreateQuestion("Microservice arxitekturasida API Gateway Pattern qo'llashning asosiy afzalligi nima?",
                new List<string> { 
                    "Tashqi mijozlar uchun yagona kirish nuqtasini (Single Entry Point) berish va Cross-Cutting concern-larni (Auth, Rate Limiting, SSL Termination, Routing) markazlashtirish", 
                    "Ma'lumotlar bazasini almashtirishni ta'minlash", 
                    "Faqat HTML sahifalarni render qilish", 
                    "Microservice-lar o'rtasida tarmoq ulanishini taqiqlash" 
                },
                "API Gateway barcha tashqi mijoz so'rovlarini o'zida kutib oladi, avtorizatsiya va rate-limiting-ni markaziy bajarib, so'rovlarni ichki mikroservislarga (Routing/BFF) yo'naltiradi."),

            CreateQuestion("Health Check probe-larining **Liveness** va **Readiness** turlari o'rtasidagi farq nima?",
                new List<string> { 
                    "Liveness — container tirik va qotib qolmaganini (Deadlock emasligini) tekshiradi; Readiness — ilova va uning bog'liqliklari (DB, Redis) so'rovlarni qabul qilishga tayyorligini tekshiradi", 
                    "Ular mutlaqo bir xil narsa", 
                    "Liveness faqat CPU haroratini tekshiradi", 
                    "Readiness faqat fayllarni o me'qiydi" 
                },
                "Kubernetes-da Liveness probe muvaffaqiyatsiz bo'lsa pod-ni kill qilib restart qiladi. Readiness probe muvaffaqiyatsiz bo'lsa pod-ni o me'chirmaydi, faqat unga yangi tarmoq trafigi yuborishni to'xtatib turadi.")
        };
    }
}
