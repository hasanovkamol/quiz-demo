# 🌐 ASP.NET Core & Web API — Test Savollari

| Mavzu | Easy | Medium | Hard | Jami |
|---|---|---|---|---|
| ASP.NET Core & Web API | 30 | 30 | 30 | 90 |

---

## 🟢 EASY (1–30)

**1. ASP.NET Core qanday turdagi freymvork?**
A) Faqat Windows uchun
B) Cross-platform (Windows, Linux, macOS)
C) Faqat mobil ilovalar uchun
D) Faqat desktop ilovalar uchun
**To'g'ri javob: B**

**2. ASP.NET Core loyihasida ilova ishga tushish nuqtasi (entry point) qaysi fayl hisoblanadi?**
A) Startup.cs
B) appsettings.json
C) Program.cs
D) web.config
**To'g'ri javob: C**

**3. ASP.NET Core'da default o'rnatilgan web server nomi nima?**
A) IIS Express
B) Kestrel
C) Apache
D) Nginx
**To'g'ri javob: B**

**4. Middleware nima uchun ishlatiladi?**
A) Faqat ma'lumotlar bazasiga ulanish uchun
B) HTTP so'rov va javob pipeline'ini boshqarish uchun
C) Faqat frontend kodini render qilish uchun
D) Faqat loglarni o'chirish uchun
**To'g'ri javob: B**

**5. Middleware'lar qanday tartibda ishlaydi?**
A) Tasodifiy tartibda
B) Ro'yxatga qo'shilish tartibida (ketma-ket)
C) Alifbo tartibida
D) Faqat bittasi ishlaydi
**To'g'ri javob: B**

**6. Controllerni belgilash uchun qaysi atribut ishlatiladi?**
A) [HttpController]
B) [ApiController]
C) [WebController]
D) [RestController]
**To'g'ri javob: B**

**7. GET so'rovini qabul qiluvchi action metodni belgilash uchun qaysi atribut ishlatiladi?**
A) [FromGet]
B) [HttpGet]
C) [GetMethod]
D) [ActionGet]
**To'g'ri javob: B**

**8. IActionResult interfeysi nima uchun ishlatiladi?**
A) Ma'lumotlar bazasi bilan ishlash uchun
B) HTTP javobining turini qaytarish uchun (Ok, NotFound, BadRequest va h.k.)
C) Middleware yaratish uchun
D) Konfiguratsiya o'qish uchun
**To'g'ri javob: B**

**9. appsettings.json faylining vazifasi nima?**
A) Ilovaning static fayllarini saqlash
B) Ilova konfiguratsiyasini (masalan, connection string) saqlash
C) NuGet paketlarini ro'yxatlash
D) Route'larni belgilash
**To'g'ri javob: B**

**10. Dependency Injection (DI) da "Transient" lifetime nimani anglatadi?**
A) Har bir so'rov uchun bitta obyekt yaratiladi
B) Har safar so'ralganda yangi obyekt yaratiladi
C) Butun ilova davomida bitta obyekt ishlatiladi
D) Faqat konfiguratsiya vaqtida yaratiladi
**To'g'ri javob: B**

**11. "Scoped" lifetime qachon yangi obyekt yaratadi?**
A) Har chaqiruvda
B) Har HTTP so'rov uchun bitta marta
C) Ilova ishga tushganda faqat bir marta
D) Hech qachon yaratmaydi
**To'g'ri javob: B**

**12. "Singleton" lifetime nimani anglatadi?**
A) Har so'rovda yangi obyekt
B) Butun ilova hayoti davomida bitta obyekt
C) Faqat test muhitida ishlaydi
D) Har controller uchun alohida obyekt
**To'g'ri javob: B**

**13. wwwroot papkasi nima uchun ishlatiladi?**
A) Controllerlarni saqlash uchun
B) Static fayllarni (CSS, JS, rasm) saqlash uchun
C) Ma'lumotlar bazasi migratsiyalarini saqlash uchun
D) Loglarni saqlash uchun
**To'g'ri javob: B**

**14. Swagger nima uchun ishlatiladi?**
A) Ma'lumotlar bazasini boshqarish uchun
B) API dokumentatsiyasini avtomatik generatsiya qilish va test qilish uchun
C) Frontend UI yaratish uchun
D) Loyihani deploy qilish uchun
**To'g'ri javob: B**

**15. HTTP POST metodi odatda nima uchun ishlatiladi?**
A) Ma'lumotni o'chirish uchun
B) Yangi resurs yaratish uchun
C) Faqat ma'lumot olish uchun
D) Serverni qayta ishga tushirish uchun
**To'g'ri javob: B**

**16. HTTP DELETE metodi qaysi maqsadda ishlatiladi?**
A) Yangi obyekt yaratish
B) Resursni o'chirish
C) Ma'lumotni yangilash
D) Faylni yuklab olish
**To'g'ri javob: B**

**17. ActionResult<T> qaysi holatda foydali?**
A) Faqat statik fayllar uchun
B) Aniq tur (T) va turli HTTP javoblarini birga qaytarishda
C) Faqat void metodlar uchun
D) Middleware yozishda
**To'g'ri javob: B**

**18. Route parametri qanday belgilanadi?**
A) {id} shaklida route shablonida
B) [id] qavs ichida
C) $id belgisi bilan
D) #id belgisi bilan
**To'g'ri javob: A**

**19. 200 status kodi nimani bildiradi?**
A) Xatolik yuz berdi
B) So'rov muvaffaqiyatli bajarildi
C) Resurs topilmadi
D) Ruxsat berilmagan
**To'g'ri javob: B**

**20. 404 status kodi nimani anglatadi?**
A) Server xatoligi
B) Resurs topilmadi
C) Muvaffaqiyatli yaratildi
D) Avtorizatsiya talab qilinadi
**To'g'ri javob: B**

**21. 401 status kodi nimani bildiradi?**
A) Ruxsat berilmagan (Unauthorized)
B) Server xatosi
C) Muvaffaqiyatli javob
D) Noto'g'ri so'rov formati
**To'g'ri javob: A**

**22. Minimal API nima?**
A) Faqat testlar uchun mo'ljallangan API
B) Kam kod bilan endpoint yaratishga imkon beruvchi yondashuv (MapGet, MapPost)
C) Faqat XML formatida ishlaydigan API
D) Deprecated bo'lgan texnologiya
**To'g'ri javob: B**

**23. launchSettings.json fayli nima uchun kerak?**
A) Production konfiguratsiyasi uchun
B) Lokal development muhitida ilovani ishga tushirish sozlamalari uchun
C) Ma'lumotlar bazasi sxemasi uchun
D) NuGet paketlarini boshqarish uchun
**To'g'ri javob: B**

**24. ASP.NET Core'da environment (masalan, Development, Production) qanday aniqlanadi?**
A) appsettings.xml orqali
B) ASPNETCORE_ENVIRONMENT muhit o'zgaruvchisi orqali
C) web.config orqali
D) Faqat kod ichida hardcode qilinadi
**To'g'ri javob: B**

**25. JSON serializatsiya uchun ASP.NET Core'da default kutubxona qaysi?**
A) Newtonsoft.Json (har doim)
B) System.Text.Json
C) Xml.Serialization
D) Json.NET Core
**To'g'ri javob: B**

**26. Controller-based API va Minimal API o'rtasidagi asosiy farq nima?**
A) Minimal API faqat GET so'rovlarni qo'llab-quvvatlaydi
B) Controller-based klassik MVC strukturasidan foydalanadi, Minimal API esa yengil, funksional yondashuv
C) Ular bir xil, farq yo'q
D) Minimal API faqat .NET Framework'da ishlaydi
**To'g'ri javob: B**

**27. dotnet CLI orqali yangi Web API loyihasini yaratish uchun qaysi buyruq ishlatiladi?**
A) dotnet create webapi
B) dotnet new webapi
C) dotnet init api
D) dotnet start webapi
**To'g'ri javob: B**

**28. [FromBody] atributi nima uchun ishlatiladi?**
A) Query string'dan ma'lumot olish uchun
B) HTTP so'rov tanasidan (body) ma'lumotni bind qilish uchun
C) Header'dan ma'lumot olish uchun
D) Route'dan ma'lumot olish uchun
**To'g'ri javob: B**

**29. [FromQuery] atributi qaysi manbadan ma'lumot oladi?**
A) URL query parametrlaridan
B) Request body'dan
C) Cookie'dan
D) Header'dan
**To'g'ri javob: A**

**30. .csproj faylida TargetFramework nimani belgilaydi?**
A) Loyihaning nomi
B) Ilova qaysi .NET versiyasiga mo'ljallanganini
C) Ma'lumotlar bazasi turini
D) Server portini
**To'g'ri javob: B**

---

## 🟡 MEDIUM (31–60)

**31. Action Filter qachon ishga tushadi?**
A) Faqat ilova ishga tushganda
B) Action metod bajarilishidan oldin va keyin
C) Faqat xatolik yuz berganda
D) Faqat authentication vaqtida
**To'g'ri javob: B**

**32. Exception Filter vazifasi nima?**
A) Ma'lumotlarni validatsiya qilish
B) Action ichida yuz bergan istisnolarni ushlab, boshqarish
C) Route'larni belgilash
D) Middleware'ni ro'yxatdan o'tkazish
**To'g'ri javob: B**

**33. Model validatsiyasi uchun qaysi atribut ishlatiladi?**
A) [Validate]
B) [Required]
C) [Check]
D) [Mandatory]
**To'g'ri javob: B**

**34. Custom middleware yaratishda odatda qaysi metod chaqiriladi?**
A) Execute()
B) InvokeAsync()
C) Run()
D) Handle()
**To'g'ri javob: B**

**35. JWT (JSON Web Token) autentifikatsiyada nima uchun ishlatiladi?**
A) Ma'lumotlar bazasini shifrlash uchun
B) Foydalanuvchi identifikatsiyasi va claims'larni token ko'rinishida uzatish uchun
C) Static fayllarni siqish uchun
D) Loglarni saqlash uchun
**To'g'ri javob: B**

**36. Authorization policy nima uchun ishlatiladi?**
A) Foydalanuvchi parolini shifrlash uchun
B) Murakkab ruxsat berish qoidalarini (masalan, rol, claim asosida) belgilash uchun
C) Ma'lumotlar bazasi ulanishini sozlash uchun
D) API versiyasini belgilash uchun
**To'g'ri javob: B**

**37. Entity Framework Core'da migratsiya nima uchun ishlatiladi?**
A) Fayllarni ko'chirish uchun
B) Model o'zgarishlarini ma'lumotlar bazasi sxemasiga qo'llash uchun
C) API endpoint yaratish uchun
D) Loglarni tozalash uchun
**To'g'ri javob: B**

**38. DbContext odatda qaysi DI lifetime bilan ro'yxatdan o'tkaziladi?**
A) Singleton
B) Scoped
C) Transient
D) Static
**To'g'ri javob: B**

**39. async/await ishlatishning asosiy afzalligi nima?**
A) Kodni qisqartirish
B) Thread'larni bloklamasdan resurslardan samarali foydalanish
C) Xotirani ko'proq ishlatish
D) Faqat sinxron kod uchun kerak
**To'g'ri javob: B**

**40. Global xatolikni ushlash uchun ASP.NET Core'da qaysi middleware ishlatiladi?**
A) UseRouting()
B) UseExceptionHandler()
C) UseAuthentication()
D) UseStaticFiles()
**To'g'ri javob: B**

**41. ProblemDetails nima uchun ishlatiladi?**
A) Ma'lumotlar bazasi xatoliklarini loglash uchun
B) RFC 7807 standartiga mos xato javoblarini formatlash uchun
C) Frontend komponentlarini render qilish uchun
D) Route'larni tekshirish uchun
**To'g'ri javob: B**

**42. CORS (Cross-Origin Resource Sharing) nima uchun kerak?**
A) Ma'lumotlar bazasi ulanishini tezlashtirish
B) Boshqa domendan kelayotgan so'rovlarga ruxsat berish/cheklash
C) Static fayllarni siqish
D) JWT tokenlarni generatsiya qilish
**To'g'ri javob: B**

**43. API versiyalashning asosiy maqsadi nima?**
A) Kodni tezlashtirish
B) Eski clientlarni buzmasdan API'ni rivojlantirish
C) Ma'lumotlar bazasini optimallashtirish
D) Faqat test uchun kerak
**To'g'ri javob: B**

**44. DTO (Data Transfer Object) nima uchun ishlatiladi?**
A) Ma'lumotlar bazasi jadvalini yaratish uchun
B) Qatlamlar (layers) o'rtasida ma'lumot uzatish uchun, domain modelni yashirish
C) Middleware yozish uchun
D) Routing uchun
**To'g'ri javob: B**

**45. AutoMapper kutubxonasi nima uchun ishlatiladi?**
A) Ma'lumotlar bazasi migratsiyasi uchun
B) Obyektlar orasida (masalan, Entity → DTO) avtomatik map qilish uchun
C) Authentication uchun
D) Loglash uchun
**To'g'ri javob: B**

**46. Repository pattern qanday maqsadda qo'llaniladi?**
A) Ma'lumotlar bazasiga murojaat qilish logikasini abstraktsiyalash uchun
B) Frontend UI yaratish uchun
C) HTTP so'rovlarini logging qilish uchun
D) Middleware tartibini belgilash uchun
**To'g'ri javob: A**

**47. IHttpClientFactory nima uchun tavsiya etiladi?**
A) HttpClient obyektlarini to'g'ri boshqarish va socket exhaustion muammosini oldini olish uchun
B) Faqat testlarda ishlatish uchun
C) Ma'lumotlar bazasiga ulanish uchun
D) JSON serializatsiya uchun
**To'g'ri javob: A**

**48. ILogger interfeysi nima uchun ishlatiladi?**
A) Ma'lumotlarni validatsiya qilish
B) Strukturaviy loglashni amalga oshirish
C) Route'larni belgilash
D) DI konteynerni sozlash
**To'g'ri javob: B**

**49. Health Checks (sog'liqni tekshirish) nima uchun ishlatiladi?**
A) Ilovaning va uning bog'liqliklarining (DB, tashqi servis) ishlash holatini monitoring qilish uchun
B) Faqat UI testlari uchun
C) JWT tokenlarni tekshirish uchun
D) Routing xatolarini topish uchun
**To'g'ri javob: A**

**50. In-memory caching qachon foydali?**
A) Har doim, hech qanday cheklovsiz
B) Tez-tez o'zgarmaydigan va tez-tez o'qiladigan ma'lumotlar uchun
C) Faqat parollarni saqlash uchun
D) Faqat static fayllar uchun
**To'g'ri javob: B**

**51. Response caching qanday ishlaydi?**
A) Server javoblarini keshlab, keyingi bir xil so'rovlarga tezroq javob berish
B) Faqat client tomonida ishlaydi
C) Ma'lumotlar bazasini keshlaydi
D) Faqat statik saytlarda ishlaydi
**To'g'ri javob: A**

**52. Rate limiting nima uchun qo'llaniladi?**
A) API'ga so'rovlar sonini cheklab, resurslarni himoya qilish uchun
B) Ma'lumotlar bazasini tezlashtirish uchun
C) JSON formatini o'zgartirish uchun
D) Faqat GET so'rovlar uchun
**To'g'ri javob: A**

**53. Pagination (sahifalash) nima uchun muhim?**
A) Katta hajmdagi ma'lumotlarni bo'laklab qaytarib, performance'ni yaxshilash uchun
B) Faqat UI dizayni uchun
C) Ma'lumotlar bazasini shifrlash uchun
D) Authentication uchun
**To'g'ri javob: A**

**54. FluentValidation kutubxonasining afzalligi nima?**
A) Faqat frontend uchun ishlaydi
B) Murakkab validatsiya qoidalarini aniq va o'qilishi oson kod bilan yozish imkonini beradi
C) Ma'lumotlar bazasi migratsiyasini avtomatlashtiradi
D) HTTP so'rovlarini keshlaydi
**To'g'ri javob: B**

**55. Options pattern (IOptions<T>) nima uchun ishlatiladi?**
A) Konfiguratsiya qiymatlarini strongly-typed obyekt sifatida in'ektsiya qilish uchun
B) Ma'lumotlar bazasi ulanishini yaratish uchun
C) Middleware buyurtmasini o'zgartirish uchun
D) Routing uchun
**To'g'ri javob: A**

**56. IHostedService interfeysi nima uchun ishlatiladi?**
A) Fon rejimida (background) uzoq muddatli vazifalarni bajarish uchun
B) Faqat controller yaratish uchun
C) JSON serializatsiya uchun
D) Static fayllarni xizmat qilish uchun
**To'g'ri javob: A**

**57. Custom Model Binder qachon kerak bo'ladi?**
A) Standart binding mexanizmi murakkab yoki maxsus formatdagi ma'lumotni to'g'ri bog'lay olmaganda
B) Faqat GET so'rovlar uchun
C) Faqat authentication uchun
D) Har doim majburiy
**To'g'ri javob: A**

**58. Content negotiation nima?**
A) Client va server o'rtasida ma'lumot formatini (JSON, XML) kelishish jarayoni
B) Ma'lumotlar bazasi bilan muzokara
C) Authentication jarayoni
D) Xatolarni qayta ishlash
**To'g'ri javob: A**

**59. AsNoTracking() EF Core'da nima uchun ishlatiladi?**
A) Faqat o'qish uchun so'rovlarda change tracking'ni o'chirib, performance'ni oshirish uchun
B) Ma'lumotni o'chirish uchun
C) Migratsiya yaratish uchun
D) Connection string'ni sozlash uchun
**To'g'ri javob: A**

**60. Role-based authorization qanday ishlaydi?**
A) Foydalanuvchi rollariga (masalan, Admin, User) asoslanib ruxsat beriladi
B) Faqat IP manzil asosida
C) Faqat parol uzunligiga qarab
D) Faqat vaqt asosida
**To'g'ri javob: A**

---

## 🔴 HARD (61–90)

**61. gRPC va REST API o'rtasidagi asosiy farq nima?**
A) gRPC HTTP/2 va Protocol Buffers ishlatadi, ko'proq performance va strongly-typed contract beradi
B) gRPC faqat frontend uchun
C) REST har doim tezroq ishlaydi
D) Ular bir xil protokol
**To'g'ri javob: A**

**62. SignalR nima uchun ishlatiladi?**
A) Real-time, ikki tomonlama aloqa (masalan, chat, notification) uchun
B) Faqat ma'lumotlar bazasi migratsiyasi uchun
C) Static fayllarni siqish uchun
D) Faqat REST API yaratish uchun
**To'g'ri javob: A**

**63. Distributed cache (masalan, Redis) local in-memory cache'dan nimasi bilan farq qiladi?**
A) Bir nechta server instance'lari orasida umumiy keshni ta'minlaydi
B) Faqat bitta serverga xos
C) U hech qanday tarmoq talab qilmaydi
D) Faqat statik fayllar uchun ishlaydi
**To'g'ri javob: A**

**64. Circuit Breaker pattern (masalan, Polly kutubxonasi) nima uchun ishlatiladi?**
A) Muvaffaqiyatsiz bo'layotgan tashqi servisga bo'lgan so'rovlarni vaqtincha to'xtatib, tizimni himoya qilish uchun
B) Ma'lumotlar bazasi migratsiyasi uchun
C) JSON serializatsiya uchun
D) Routing uchun
**To'g'ri javob: A**

**65. IHostedService va BackgroundService o'rtasidagi farq nima?**
A) BackgroundService — IHostedService'ni implement qiluvchi abstract klass, ExecuteAsync orqali yozishni osonlashtiradi
B) Ular butunlay bog'liq emas
C) BackgroundService faqat controller'larda ishlaydi
D) IHostedService faqat .NET Framework'da mavjud
**To'g'ri javob: A**

**66. Channel<T> fon vazifalarni boshqarishda nima uchun foydali?**
A) Producer-consumer pattern'ni thread-safe tarzda amalga oshirish uchun
B) Faqat UI thread uchun
C) Ma'lumotlar bazasi ulanishini poolga solish uchun
D) HTTP header'larni o'qish uchun
**To'g'ri javob: A**

**67. Refresh token mexanizmi nima uchun kerak?**
A) Access token muddati tugaganda foydalanuvchini qayta login qildirmasdan yangi token olish uchun
B) Parolni saqlash uchun
C) Ma'lumotlar bazasini shifrlash uchun
D) Faqat admin foydalanuvchilar uchun
**To'g'ri javob: A**

**68. OAuth2 va OpenID Connect o'rtasidagi farq nima?**
A) OAuth2 avtorizatsiya protokoli, OpenID Connect esa OAuth2 ustiga qurilgan autentifikatsiya qatlami
B) Ular bir xil protokol, faqat nomi boshqa
C) OpenID Connect faqat mobil ilovalar uchun
D) OAuth2 faqat Google uchun ishlatiladi
**To'g'ri javob: A**

**69. Custom Authorization Handler qachon zarur bo'ladi?**
A) Oddiy rol asosidagi tekshiruv yetarli bo'lmagan, murakkab biznes qoidalari kerak bo'lganda
B) Faqat static fayllar uchun
C) Har doim, oddiy holatlarda ham
D) Faqat GET so'rovlar uchun
**To'g'ri javob: A**

**70. Claims-based authorization qanday ishlaydi?**
A) Foydalanuvchi haqidagi turli claim'lar (masalan, yosh, bo'lim) asosida ruxsat qarorlari qabul qilinadi
B) Faqat parol uzunligi asosida
C) Faqat IP manzil asosida
D) Faqat vaqt zonasiga qarab
**To'g'ri javob: A**

**71. N+1 muammosi EF Core'da nima?**
A) Har bir asosiy yozuv uchun alohida-alohida qo'shimcha so'rov yuborilishi natijasida performance pasayishi
B) Ma'lumotlar bazasida jadval yetishmasligi
C) JSON formatidagi xatolik
D) Migratsiya xatosi
**To'g'ri javob: A**

**72. Compiled query EF Core'da nima uchun ishlatiladi?**
A) Tez-tez bajariladigan LINQ so'rovlarni oldindan kompilyatsiya qilib, performance'ni oshirish uchun
B) Ma'lumotlar bazasini yaratish uchun
C) Migratsiyani orqaga qaytarish uchun
D) Connection string'ni shifrlash uchun
**To'g'ri javob: A**

**73. Connection pooling nima uchun muhim?**
A) Ma'lumotlar bazasi ulanishlarini qayta ishlatib, yangi ulanish yaratish xarajatini kamaytirish uchun
B) Faqat static fayllar uchun
C) Faqat authentication uchun
D) JSON serializatsiya uchun
**To'g'ri javob: A**

**74. Horizontal scaling nima?**
A) Bitta serverga ko'proq resurs (CPU/RAM) qo'shish
B) Ko'proq server instance'lari qo'shib, yukni taqsimlash
C) Faqat ma'lumotlar bazasini kattalashtirish
D) Faqat kod optimizatsiyasi
**To'g'ri javob: B**

**75. Load balancer nima uchun ishlatiladi?**
A) So'rovlarni bir nechta server instance'lari orasida taqsimlash uchun
B) Ma'lumotlar bazasini zaxiralash uchun
C) JSON formatlash uchun
D) Faqat frontend uchun
**To'g'ri javob: A**

**76. Distributed tracing (masalan, OpenTelemetry) nima uchun kerak?**
A) Microservice'lar orasida bitta so'rovning yo'lini kuzatib, performance muammolarini aniqlash uchun
B) Faqat frontend animatsiyalari uchun
C) Ma'lumotlar bazasi migratsiyasi uchun
D) Faqat log darajasini o'zgartirish uchun
**To'g'ri javob: A**

**77. Server-Sent Events (SSE) WebSocket'dan nimasi bilan farq qiladi?**
A) SSE faqat serverdan client'ga bir tomonlama oqim, WebSocket esa ikki tomonlama
B) SSE ikki tomonlama, WebSocket bir tomonlama
C) Ular butunlay bir xil
D) SSE faqat mobil qurilmalarda ishlaydi
**To'g'ri javob: A**

**78. Custom ModelBinderProvider qanday holatda ishlatiladi?**
A) Ma'lum bir turdagi barcha parametrlar uchun global binding logikasini ro'yxatdan o'tkazishda
B) Faqat static fayllar uchun
C) Faqat authentication uchun
D) Faqat routing uchun
**To'g'ri javob: A**

**79. Response compression (masalan, Gzip, Brotli) nima uchun ishlatiladi?**
A) Javob hajmini kichraytirib, tarmoq orqali uzatish tezligini oshirish uchun
B) Ma'lumotlar bazasini siqish uchun
C) JWT tokenni shifrlash uchun
D) Faqat rasm fayllari uchun
**To'g'ri javob: A**

**80. HTTP/2 HTTP/1.1'dan asosiy afzalligi nima?**
A) Multiplexing orqali bir ulanishda bir nechta so'rov-javobni parallel yuborish imkoniyati
B) Faqat xavfsizlikni oshiradi
C) Faqat kattaroq fayllarni qo'llab-quvvatlaydi
D) Ular bir xil ishlaydi
**To'g'ri javob: A**

**81. Idempotency key nima uchun ishlatiladi?**
A) Bir xil so'rov bir necha marta yuborilganda ham operatsiya faqat bir marta bajarilishini kafolatlash uchun
B) Ma'lumotlar bazasini shifrlash uchun
C) JSON formatini o'zgartirish uchun
D) Faqat GET so'rovlar uchun
**To'g'ri javob: A**

**82. YARP (Yet Another Reverse Proxy) nima uchun ishlatiladi?**
A) .NET asosida API Gateway/reverse proxy qurish uchun
B) Ma'lumotlar bazasi migratsiyasi uchun
C) Frontend komponent yaratish uchun
D) Faqat testlar uchun
**To'g'ri javob: A**

**83. WebApplicationFactory nima uchun ishlatiladi?**
A) Integration testlarda in-memory test serverini yaratish uchun
B) Production serverini sozlash uchun
C) Ma'lumotlar bazasi migratsiyasi uchun
D) JSON serializatsiya uchun
**To'g'ri javob: A**

**84. Output caching Response caching'dan nimasi bilan farq qiladi?**
A) Output caching server tomonida butun javobni saqlab, keyingi so'rovlarga ilovaga tegmasdan javob beradi, kengroq boshqaruv imkoniyatlari bilan
B) Ular butunlay bir xil narsa
C) Output caching faqat client tomonida ishlaydi
D) Response caching faqat POST so'rovlar uchun
**To'g'ri javob: A**

**85. Minimal API va Controller-based API o'rtasidagi performance farqi nimadan kelib chiqadi?**
A) Minimal API'da MVC pipeline'ining ba'zi qatlamlari (masalan, model binding overhead) yengilroq bo'lishi mumkin
B) Ular har doim bir xil tezlikda ishlaydi
C) Controller-based har doim tezroq
D) Farq faqat ma'lumotlar bazasida
**To'g'ri javob: A**

**86. Custom middleware pipeline'da UseAuthentication() va UseAuthorization() tartibi nima uchun muhim?**
A) Avval foydalanuvchi kimligi aniqlanishi (Authentication), keyin ruxsatlari tekshirilishi (Authorization) kerak
B) Tartib ahamiyatsiz
C) Authorization har doim birinchi bo'lishi kerak
D) Ular alohida pipeline'da ishlaydi
**To'g'ri javob: A**

**87. gRPC streaming turlari nechta va qaysilar?**
A) 4 ta: Unary, Server streaming, Client streaming, Bidirectional streaming
B) Faqat 1 ta: Unary
C) Faqat 2 ta: GET va POST
D) 3 ta: Sync, Async, Batch
**To'g'ri javob: A**

**88. Distributed system'da eventual consistency nimani anglatadi?**
A) Ma'lumotlar barcha node'larda darhol bir xil bo'lishi kafolatlanadi
B) Ma'lumotlar vaqt o'tishi bilan barcha node'larda bir xil holatga keladi, lekin darhol emas
C) Ma'lumotlar hech qachon sinxronlanmaydi
D) Faqat bitta node ishlatiladi
**To'g'ri javob: B**

**89. API Gateway pattern microservice arxitekturasida nima uchun muhim?**
A) Client so'rovlarini bitta kirish nuqtasi orqali marshrutlash, autentifikatsiya va rate limiting kabi umumiy vazifalarni markazlashtirish uchun
B) Ma'lumotlar bazasini almashtirish uchun
C) Faqat frontend uchun UI yaratish uchun
D) Faqat loglash uchun
**To'g'ri javob: A**

**90. Health check'larni Kubernetes/orkestrator bilan integratsiya qilishning asosiy maqsadi nima?**
A) Nosog'lom instance'larni avtomatik aniqlab, trafikdan chetlashtirish yoki qayta ishga tushirish uchun
B) Faqat loglarni ko'rish uchun
C) Ma'lumotlar bazasi zaxira nusxasini olish uchun
D) Faqat UI testlash uchun
**To'g'ri javob: A**

---

*Jami: 90 ta savol (30 Easy + 30 Medium + 30 Hard)*
