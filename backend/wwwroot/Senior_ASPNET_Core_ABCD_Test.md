# Senior ASP.NET Core Developer — 100 ta ABCD Test Savoli

Har bir savol master darajadagi bilimni tekshirish uchun tuzilgan: noto'g'ri variantlar ham real hayotda uchraydigan tushunchalar yoki keng tarqalgan xato tasavvurlar asosida yozilgan.

---

## 1. C# Til Asoslari va Ilg'or Mavzular (1–15)

**1. Mahalliy (local) o'zgaruvchi sifatida e'lon qilingan value type odatda qayerda saqlanadi?**
A) Har doim Heap'da
B) Stack'da
C) Faqat register'da
D) Disk cache'da
**To'g'ri javob: B**

**2. Async metod compile qilinganda C# compiler nima yaratadi?**
A) Yangi OS thread
B) State machine (metod bajarilish holatini boshqaruvchi struct/class)
C) Faqat oddiy delegate
D) Hech narsa — runtime avtomatik boshqaradi
**To'g'ri javob: B**

**3. ValueTask<T> qachon Task<T> o'rniga tavsiya etiladi?**
A) Har doim, chunki u har doim tezroq
B) Natija ko'pincha sinxron/tez qaytadigan va allocation'ni kamaytirish kerak bo'lgan holatlarda
C) Faqat void metodlarda
D) Faqat exception qaytarishda
**To'g'ri javob: B**

**4. ASP.NET Core kodida `ConfigureAwait(false)` haqida qaysi fikr to'g'ri?**
A) Majburiy, aks holda har doim deadlock yuz beradi
B) ASP.NET Core'da SynchronizationContext yo'qligi sababli deadlock xavfi past, lekin qayta ishlatiladigan kutubxona kodida yaxshi amaliyot sifatida qoladi
C) Faqat WPF/WinForms ilovalarida ishlatiladi, web'da umuman kerak emas
D) Faqat `Task.Run` bilan ishlatiladi
**To'g'ri javob: B**

**5. Async kodda deadlock ko'pincha qaysi holatda yuz beradi?**
A) `Task.Result` yoki `.Wait()` ni SynchronizationContext mavjud bo'lgan muhitda (masalan, eski ASP.NET yoki UI thread) chaqirilganda
B) To'liq `async/await` zanjiridan foydalanilganda
C) `ConfigureAwait(false)` qo'llanilganda
D) `Task.Run` bilan background ishga tushirilganda
**To'g'ri javob: A**

**6. `Predicate<T>` delegate turi nima qaytaradi?**
A) `void`
B) `bool`
C) `T`
D) `object`
**To'g'ri javob: B**

**7. `event` kalit so'zi oddiy `delegate` maydonidan nimasi bilan farq qiladi?**
A) Hech nima, ular bir xil
B) Class tashqarisidan to'g'ridan-to'g'ri chaqirish yoki qayta tayinlashni cheklaydi, faqat `+=`/`-=` orqali obuna bo'lish mumkin
C) `event` faqat static bo'lishi shart
D) `event`ga faqat bitta handler ulanishi mumkin
**To'g'ri javob: B**

**8. `where T : class, new()` generic cheklovi nimani anglatadi?**
A) T faqat struct bo'lishi kerak
B) T reference type bo'lishi va parametrsiz ochiq konstruktorga ega bo'lishi kerak
C) T interface bo'lishi shart
D) T sealed class bo'lishi shart
**To'g'ri javob: B**

**9. `IQueryable<T>`ning `IEnumerable<T>`dan asosiy afzalligi nima?**
A) Har doim tezroq ishlaydi
B) Expression tree yaratib, so'rovni ma'lumotlar manbaida (masalan, SQL serverda) bajarish imkonini beradi (deferred execution)
C) Faqat in-memory to'plamlar bilan ishlaydi
D) Avtomatik thread-safe bo'ladi
**To'g'ri javob: B**

**10. `yield return` ishlatilganda compiler nima hosil qiladi?**
A) Oddiy massiv
B) Iterator — holatni saqlovchi lazy enumeration mexanizmi
C) Yangi thread
D) Static class
**To'g'ri javob: B**

**11. Boxing operatsiyasi performance'ga qanday ta'sir qiladi?**
A) Umuman ta'sir qilmaydi
B) Value type'ni heap'ga nusxalash orqali qo'shimcha allocation va GC yukini oshiradi
C) Kodni tezlashtiradi
D) Faqat compile vaqtida sodir bo'ladi, runtime'ga ta'siri yo'q
**To'g'ri javob: B**

**12. `using` statement compile vaqtida qanday konstruksiyaga aylanadi?**
A) `if-else` blokiga
B) `try/finally` blokiga, `finally` ichida `Dispose()` chaqiriladi
C) `while` loop'ga
D) `switch` statement'ga
**To'g'ri javob: B**

**13. `record` va oddiy `class` o'rtasidagi asosiy farq nima?**
A) `record` avtomatik value-based equality va `with`-expression'ni qo'llab-quvvatlaydi
B) `record` har doim to'liq immutable bo'ladi va boshqa hech qanday farqi yo'q
C) `record` faqat `struct` sifatida kompilyatsiya qilinadi
D) Ular to'liq sinonim, farq yo'q
**To'gri javob: A**

**14. `#nullable enable` yoqilganda compiler amalda nima qiladi?**
A) Runtime'da null tekshiruvini majburiy qiladi
B) Compile vaqtida null bo'lishi mumkin bo'lgan reference'lar haqida ogohlantirish (warning) beradi, runtime xatti-harakatini o'zgartirmaydi
C) `NullReferenceException`ni butunlay yo'q qiladi
D) Faqat value type'larga ta'sir qiladi
**To'g'ri javob: B**

**15. Garbage Collector'ning Gen0 to'plami odatda qanday obyektlarni saqlaydi?**
A) Uzoq umr ko'radigan obyektlarni
B) Yangi yaratilgan, qisqa umr ko'radigan obyektlarni (eng tez-tez to'planadigan qism)
C) Faqat static obyektlarni
D) Faqat Large Object Heap'dagi obyektlarni
**To'g'ri javob: B**

---

## 2. ASP.NET Core Asoslari (16–35)

**16. `app.Use(...)` va `app.Run(...)` middleware'lari o'rtasidagi farq nima?**
A) Farqi yo'q, ikkalasi bir xil ishlaydi
B) `app.Use` keyingi middleware'ga (`next`) o'tish imkonini beradi, `app.Run` esa pipeline'ni yakunlovchi (terminal) middleware hisoblanadi
C) `app.Run` faqat static fayllar uchun ishlatiladi
D) `app.Use` faqat authentication uchun mo'ljallangan
**To'g'ri javob: B**

**17. ASP.NET Core'ning built-in DI konteyneri qaysi lifetime turlarini qo'llab-quvvatlaydi?**
A) Faqat Singleton
B) Transient, Scoped va Singleton
C) Faqat Transient va Singleton
D) Prototype, Session va Application
**To'g'ri javob: B**

**18. Scoped service'ni Singleton service ichiga to'g'ridan-to'g'ri inject qilish nima uchun muammoli (captive dependency)?**
A) Bu umuman muammo emas
B) Singleton butun ilova umri davomida yashagani uchun, unga inject qilingan Scoped service ham "qamalib qoladi" va yangilanmaydi (masalan, DbContext eskirgan holda qoladi)
C) Bu faqat performance'ni yaxshilaydi
D) Faqat Transient service'lar uchun muammo tug'diradi
**To'g'ri javob: B**

**19. `BackgroundService` abstrakt klassi qaysi metodni override qilishni talab qiladi?**
A) `Execute()`
B) `ExecuteAsync(CancellationToken stoppingToken)`
C) `Run()`
D) Hech qaysisini, faqat `StartAsync()` yetarli
**To'g'ri javob: B**

**20. .NET 6+ minimal hosting modelida `WebApplicationBuilder` nimani bitta joyga jamlaydi?**
A) Faqat routing sozlamalarini
B) Host konfiguratsiyasi, DI konteyner va middleware pipeline sozlamalarini
C) Faqat logging provayderlarini
D) Faqat Kestrel portlarini
**To'g'ri javob: B**

**21. Attribute routing'ning conventional routing'dan asosiy afzalligi nima?**
A) Har doim ancha tezroq ishlaydi
B) Route'lar controller/action ustida aniq va mahalliy tarzda belgilanadi, murakkab yoki notekis URL sxemalarini boshqarish osonlashadi
C) Faqat Razor Pages uchun ishlaydi
D) DI talab qilmaydi
**To'g'ri javob: B**

**22. `[ApiController]` atributi bilan model validatsiyasi muvaffaqiyatsiz bo'lganda nima sodir bo'ladi?**
A) Hech narsa, buni har doim qo'lda tekshirish kerak
B) Framework avtomatik ravishda 400 Bad Request javobini qaytaradi (automatic model state validation)
C) Har doim 500 Internal Server Error qaytariladi
D) So'rov jimgina e'tiborsiz qoldiriladi
**To'g'ri javob: B**

**23. Standart holatda ASP.NET Core MVC filter pipeline'i qaysi tartibda ishga tushadi?**
A) Exception → Result → Action → Authorization
B) Authorization → Resource → Model Binding → Action → Exception → Result
C) Result → Action → Authorization → Resource
D) Tartib har safar tasodifiy belgilanadi
**To'g'ri javob: B**

**24. Action Filter va Middleware o'rtasidagi asosiy farq nima?**
A) Farqi yo'q
B) Action Filter MVC pipeline ichida ishlaydi va routing/model binding ma'lumotlariga kirish huquqiga ega, Middleware esa umumiy HTTP pipeline darajasida (routing'dan tashqarida ham) ishlaydi
C) Middleware faqat authentication uchun ishlatiladi
D) Action Filter har doim middleware'dan tezroq
**To'g'ri javob: B**

**25. Razor Pages'ning MVC'dan asosiy farqi nima?**
A) Controller o'rniga sahifa-markazlashgan `PageModel` yondashuvidan foydalanadi
B) DI umuman qo'llab-quvvatlanmaydi
C) Faqat API endpoint'lar uchun mo'ljallangan
D) Faqat static HTML qaytaradi, C# kod ishlatilmaydi
**To'g'ri javob: A**

**26. Minimal API'ning an'anaviy Controller-based API'ga nisbatan asosiy afzalligi nima?**
A) Ishlash tezligi har doim sezilarli darajada yuqori
B) Kamroq boilerplate kod bilan kichik xizmatlar va endpoint'larni tezroq yaratish imkonini beradi
C) Filter va validatsiyani umuman qo'llab-quvvatlamaydi
D) Faqat GET so'rovlarini qabul qiladi
**To'g'ri javob: B**

**27. `AllowAnyOrigin()` bilan `AllowCredentials()` birga ishlatilishi nima uchun muammoli?**
A) Hech qanday muammo yo'q, bu tavsiya etiladigan kombinatsiya
B) Xavfsizlik nuqtai nazaridan xavfli — brauzer bunday kombinatsiyaga yo'l qo'ymaydi, chunki credentials bilan istalgan origin'ga ruxsat berish CSRF xavfini oshiradi
C) Faqat HTTPS ishlatilmaganda muammo tug'diladi
D) Bu ASP.NET Core'da texnik jihatdan mumkin emas
**To'g'ri javob: B**

**28. `IOptionsSnapshot<T>` qanday lifetime'ga ega va nima uchun Singleton service ichida ishlatib bo'lmaydi?**
A) Singleton lifetime'ga ega, hech qanday cheklov yo'q
B) Scoped lifetime'ga ega, har bir so'rov uchun konfiguratsiyani qayta o'qiydi — shu sababli Singleton'ga inject qilinsa captive dependency xatosi yuzaga keladi
C) Transient, faqat bir marta ishlatiladi va keyin yo'q qilinadi
D) `IOptionsSnapshot` .NET'da mavjud emas
**To'g'ri javob: B**

**29. Kestrel serverni production muhitida Nginx/IIS kabi reverse proxy ortida ishlatishning asosiy sababi nima?**
A) Kestrel mustaqil ishlay olmaydi
B) Qo'shimcha xavfsizlik qatlami, load balancing va SSL termination kabi imkoniyatlarni qo'lga kiritish uchun
C) Kestrel faqat Windows platformasida ishlaydi
D) Reverse proxy Kestrel'ni majburiy almashtiradi
**To'g'ri javob: B**

**30. `UseExceptionHandler` middleware'ining asosiy vazifasi nima?**
A) Faqat log yozish
B) Pipeline'da yuzaga kelgan qayta ishlanmagan istisnolarni ushlab, foydalanuvchiga standartlashtirilgan xatolik javobini (masalan, `ProblemDetails`) qaytarish
C) Faqat 404 xatoliklarini boshqarish
D) Faqat development muhitida ishlaydi
**To'g'ri javob: B**

**31. Health Checks (`AddHealthChecks`) nima uchun ishlatiladi?**
A) Faqat unit test yozish uchun
B) Ilova va uning bog'liqliklari (DB, tashqi servis va h.k.) holatini monitoring/orkestratsiya tizimlariga (masalan, Kubernetes) bildirish uchun
C) Faqat logging konfiguratsiyasi uchun
D) Faqat autentifikatsiya tekshiruvi uchun
**To'g'ri javob: B**

**32. Response Caching va Output Caching o'rtasidagi asosiy farq nima?**
A) Ular butunlay bir xil narsa
B) Output Caching (.NET 7+) serverda to'liq javobni saqlaydi va moslashuvchan invalidatsiya siyosatlarini qo'llab-quvvatlaydi, Response Caching esa asosan HTTP cache header'lariga tayanadi
C) Response Caching faqat statik fayllar uchun ishlaydi
D) Output Caching faqat mijoz (client) tomonida ishlaydi
**To'g'ri javob: B**

**33. .NET 7+ dagi Rate Limiting middleware qaysi algoritmlarni qo'llab-quvvatlaydi?**
A) Faqat bitta qattiq belgilangan algoritm
B) Fixed Window, Sliding Window, Token Bucket va Concurrency Limiter kabi bir nechta strategiyalarni
C) Faqat IP-manzil bo'yicha bloklashni
D) Rate limiting faqat tashqi kutubxonalar orqali amalga oshiriladi
**To'g'ri javob: B**

**34. SignalR asosan nima uchun ishlatiladi?**
A) Faqat fayl yuklash uchun
B) Server va mijoz o'rtasida real-vaqtli, ikki tomonlama aloqani (WebSocket va boshqa transportlar orqali) ta'minlash uchun
C) Faqat REST API almashtirish uchun
D) Faqat statik kontentni keshlash uchun
**To'g'ri javob: B**

**35. `IOptionsMonitor<T>` ning asosiy afzalligi nima?**
A) Faqat ilova ishga tushganda bir marta o'qiydi
B) Konfiguratsiya faylida real vaqt rejimida o'zgarish bo'lsa, Singleton service'lar ham buni darhol (change notification orqali) sezishi mumkin
C) Faqat Scoped service'larda ishlaydi
D) Faqat test muhitida ishlatiladi
**To'g'ri javob: B**

---

## 3. Web API va REST Arxitekturasi (36–47)

**36. RESTful API'ning asosiy tamoyillaridan biri qaysi?**
A) Server har bir so'rov o'rtasida mijoz holatini (state) saqlashi shart
B) Stateless aloqa — har bir so'rov o'zida to'liq kontekstni olib yuradi
C) Faqat XML formatidan foydalanish majburiy
D) Faqat POST metodidan foydalanish kerak
**To'g'ri javob: B**

**37. 400 va 422 status kodlari o'rtasidagi farq nima?**
A) Ular bir xil ma'noni bildiradi
B) 400 — so'rov sintaksisi noto'g'ri (malformed request), 422 — sintaksis to'g'ri, lekin semantik/validatsiya xatosi bor
C) 422 faqat GET so'rovlarida ishlatiladi
D) 400 faqat autentifikatsiya uchun ishlatiladi
**To'g'ri javob: B**

**38. API versiyalashning URL-based (`/api/v1/...`) usulining kamchiligi nima?**
A) Kamchiligi umuman yo'q
B) URL manzillarining "shishishi" va bir nechta versiyani parallel qo'llab-quvvatlash murakkablashadi, header-based yoki media-type based usullar ko'proq "toza" hisoblanadi
C) U texnik jihatdan amalga oshirib bo'lmaydi
D) Faqat GraphQL bilan ishlaydi
**To'g'ri javob: B**

**39. Entity'ni to'g'ridan-to'g'ri API javobida qaytarish nima uchun yomon amaliyot hisoblanadi?**
A) Chunki bu texnik jihatdan mumkin emas
B) Ichki ma'lumotlar strukturasini oshkor qiladi, over-posting/under-posting xavfini oshiradi va DB sxemasi bilan API contract'ini qattiq bog'laydi
C) Chunki entity'lar har doim juda kichik hajmda bo'ladi
D) Chunki JSON serializatsiya entity'larni qo'llab-quvvatlamaydi
**To'g'ri javob: B**

**40. AutoMapper kabi mapping kutubxonalarining asosiy xavfi nima?**
A) Ular hech qanday xavf tug'dirmaydi
B) "Sehrli" (implicit) konfiguratsiyalar debugging'ni murakkablashtirishi va performance narxi (reflection-based mapping) bo'lishi mumkin
C) Ular faqat EF Core bilan ishlaydi
D) Ular DI bilan mos kelmaydi
**To'g'ri javob: B**

**41. Idempotency (bir xil natija bilan qayta ishlash) tushunchasi qaysi HTTP metodlariga xos?**
A) Faqat POST
B) GET, PUT, DELETE (to'g'ri loyihalangan holda) — bir necha marta chaqirilsa ham natija bir xil bo'ladi
C) Faqat PATCH
D) Hech qaysi metod idempotent bo'la olmaydi
**To'g'ri javob: B**

**42. Katta hajmdagi ma'lumotlar uchun pagination qo'llashning asosiy sababi nima?**
A) Faqat vizual dizayn uchun
B) Bir vaqtning o'zida server va tarmoq resurslarini haddan tashqari band qilmaslik, javob vaqtini optimallashtirish
C) Pagination faqat mijoz tomonida amalga oshiriladi, server tomoniga aloqasi yo'q
D) SQL Server pagination'ni qo'llab-quvvatlamaydi
**To'g'ri javob: B**

**43. Swagger/OpenAPI ishlatishning asosiy afzalligi nima?**
A) Ilovaning ishlash tezligini oshiradi
B) API'ni avtomatik hujjatlashtiradi va interaktiv test qilish, mijoz kod generatsiyasi kabi imkoniyatlarni beradi
C) Faqat production muhitida ishlaydi
D) Autentifikatsiyani almashtiradi
**To'g'ri javob: B**

**44. gRPC qaysi holatda REST'dan afzalroq bo'ladi?**
A) Brauzerdan to'g'ridan-to'g'ri chaqiriladigan public API uchun
B) Mikroservislar orasidagi yuqori unumdorlikli, kam kechikuvchi (low-latency) ichki aloqa uchun (Protobuf va HTTP/2 asosida)
C) Faqat statik fayllarni uzatish uchun
D) gRPC va REST bir xil holatlarda ishlatiladi, farqi yo'q
**To'g'ri javob: B**

**45. GraphQL'ning REST'ga nisbatan asosiy afzalligi nima?**
A) Har doim ancha oddiyroq sozlanadi
B) Mijoz aynan kerakli maydonlarni bitta so'rovda olishi mumkin (over-fetching/under-fetching muammosi kamayadi)
C) GraphQL faqat mutatsiyalarni qo'llab-quvvatladi
D) Caching GraphQL'da REST'ga qaraganda ancha osonroq
**To'g'ri javob: B**

**46. HATEOAS (Hypermedia as the Engine of Application State) nimani anglatadi?**
A) API javoblarida keyingi mumkin bo'lgan amallar uchun havolalar (link) taqdim etilishi
B) Faqat autentifikatsiya sxemasi
C) API versiyasini boshqarish usuli
D) Ma'lumotlar bazasi indekslash strategiyasi
**To'g'ri javob: A**

**47. Katta fayllarni yuklash/yuklab olishda streaming yondashuvining afzalligi nima?**
A) Fayl to'liq xotiraga (memory) yuklanadi va shu sababli tezroq ishlaydi
B) Butun faylni xotiraga yuklamasdan, qismlarga bo'lib qayta ishlash orqali server xotirasi va resurslarini tejaydi
C) Streaming faqat video fayllar uchun ishlatiladi
D) Streaming HTTP protokoli tomonidan qo'llab-quvvatlanmaydi
**To'g'ri javob: B**

---

## 4. Entity Framework Core (48–65)

**48. `AddDbContext` orqali ro'yxatdan o'tkazilgan `DbContext` standart holatda qanday lifetime'ga ega?**
A) Singleton
B) Scoped
C) Transient
D) Static
**To'g'ri javob: B**

**49. `AsNoTracking()` nima uchun ishlatiladi?**
A) So'rov natijasini keshlash uchun
B) Faqat o'qish (read-only) uchun ma'lumot olinganda Change Tracker yukini olib tashlab, performance'ni oshirish uchun
C) Ma'lumotlarni bazaga yozish uchun majburiy
D) Faqat migratsiyalar uchun ishlatiladi
**To'g'ri javob: B**

**50. Production muhitida EF Core migratsiyalarini qo'llashning tavsiya etilgan yondashuvi qaysi?**
A) `Database.EnsureCreated()` metodidan doim foydalanish
B) Migratsiyalarni CI/CD pipeline orqali nazorat ostida, SQL skript generatsiya qilib yoki alohida deployment bosqichida qo'llash
C) Har bir so'rovda avtomatik `Migrate()` chaqirish
D) Migratsiyalarni umuman ishlatmaslik, faqat qo'lda SQL yozish
**To'g'ri javob: B**

**51. Lazy Loading'ning asosiy xavfi nima?**
A) U texnik jihatdan EF Core'da mavjud emas
B) Nazoratsiz holatda ko'plab qo'shimcha SQL so'rovlarni keltirib chiqarishi mumkin (N+1 muammosi)
C) U faqat `Include()` bilan birga ishlaydi
D) Faqat Fluent API orqali sozlanadi
**To'g'ri javob: B**

**52. N+1 query muammosi nima?**
A) Bitta so'rovda 1 ta ortiqcha ustun qaytarilishi
B) Asosiy ro'yxat uchun 1 ta so'rov, so'ngra har bir element uchun alohida-alohida qo'shimcha N ta so'rov yuborilishi (odatda lazy loading yoki noto'g'ri Include natijasida)
C) Faqat migratsiyalarga tegishli muammo
D) Faqat Raw SQL ishlatilganda yuzaga keladi
**To'g'ri javob: B**

**53. EF Core'da `IQueryable` zanjiri qachon aslida SQL so'roviga aylanadi?**
A) `Where()` chaqirilgan zaqhotiyoq
B) Natija haqiqatda materiallashtirilganda — masalan, `ToList()`, `First()`, `foreach` orqali iteratsiya qilinganda (deferred execution)
C) DbContext yaratilgan zahoti
D) Faqat `Include()` chaqirilganda
**To'g'ri javob: B**

**54. EF Core'da `SaveChanges()` chaqiruvi standart holatda qanday tranzaksion xususiyatga ega?**
A) Har bir o'zgarish alohida-alohida commit qilinadi
B) Bitta `SaveChanges()` ichidagi barcha o'zgarishlar bitta implicit tranzaksiya sifatida atomik tarzda bajariladi
C) Tranzaksiya umuman qo'llanilmaydi
D) Faqat `BeginTransaction()` chaqirilgandagina atomiklik ta'minlanadi
**To'g'ri javob: B**

**55. Optimistic Concurrency uchun `RowVersion`/Concurrency Token qanday ishlaydi?**
A) Yozuvni bazada butunlay bloklaydi (lock)
B) `UPDATE` so'rovi eski qiymatni `WHERE` shartida tekshiradi; agar mos kelmasa, `DbUpdateConcurrencyException` tashlanadi
C) Faqat `SELECT` so'rovlarida ishlatiladi
D) Faqat Fluent API'siz ishlaydi
**To'g'ri javob: B**

**56. Fluent API'ning Data Annotations'ga nisbatan afzalligi nima?**
A) Fluent API entity klasslarini "iflos" qilmasdan, murakkab konfiguratsiyalarni (masalan, composite key, shadow property) markazlashtirilgan holda belgilash imkonini beradi
B) Data Annotations umuman ishlamaydi
C) Fluent API faqat migratsiyalar uchun kerak
D) Ular orasida farq yo'q
**To'g'ri javob: A**

**57. Many-to-Many munosabat EF Core (5.0+) da qanday konfiguratsiya qilinishi mumkin?**
A) Faqat qo'lda join entity yaratish orqali, avtomatik usul yo'q
B) Explicit join entity'siz, to'g'ridan-to'g'ri ikkita navigation property orqali (EF Core avtomatik join jadval yaratadi)
C) Many-to-Many EF Core'da umuman qo'llab-quvvatlanmaydi
D) Faqat Raw SQL orqali
**To'g'ri javob: B**

**58. Global Query Filters (masalan, soft delete uchun) nima uchun foydali?**
A) Faqat migratsiyalarni tezlashtiradi
B) Har bir so'rovga avtomatik ravishda qo'shimcha `WHERE` shartini (masalan, `IsDeleted == false`) qo'llash orqali kodni takrorlashdan saqlaydi
C) Faqat `Include()` bilan ishlaydi
D) Faqat write operatsiyalarga ta'sir qiladi
**To'g'ri javob: B**

**59. EF Core orqali Stored Procedure chaqirishning tavsiya etilgan usuli qaysi?**
A) Bu EF Core'da mumkin emas
B) `FromSqlRaw`/`FromSqlInterpolated` (query uchun) yoki `ExecuteSqlRaw`/`ExecuteSqlInterpolated` (buyruqlar uchun) metodlari orqali, parametrlarni SQL Injection'dan himoyalangan holda
C) Faqat `DbSet.Add()` orqali
D) Faqat migratsiya fayli ichida
**To'g'ri javob: B**

**60. Compiled Queries EF Core'da performance'ni qanday yaxshilaydi?**
A) Ular ma'lumotlar bazasi indekslarini avtomatik yaratadi
B) LINQ so'rovini SQL'ga tarjima qilish (query compilation) xarajatini kesh qilib, takroriy chaqiriladigan so'rovlar uchun bu jarayonni qayta bajarmaydi
C) Ular faqat `AsNoTracking()` bilan ishlaydi
D) Ular tranzaksiyalarni tezlashtiradi
**To'g'ri javob: B**

**61. Katta hajmdagi ma'lumotlarni Bulk Insert/Update qilishda standart `SaveChanges()` nima uchun samarasiz bo'lishi mumkin?**
A) `SaveChanges()` bir vaqtning o'zida faqat bitta yozuvni qayta ishlay oladi va har bir o'zgarish uchun alohida round-trip yaratishi mumkin, minglab yozuv uchun bu sekin bo'ladi — shu sababli maxsus bulk kutubxonalar (masalan, EFCore.BulkExtensions) qo'llaniladi
B) `SaveChanges()` umuman insert operatsiyasini qo'llab-quvvatlamaydi
C) `SaveChanges()` faqat `async` rejimda ishlaydi
D) Bunday muammo mavjud emas
**To'g'ri javob: A**

**62. DbContext Pooling (`AddDbContextPool`) nima uchun ishlatiladi?**
A) DbContext obyektlarini har safar yangidan yaratish va yo'q qilish xarajatini kamaytirish uchun, obyektlarni qayta ishlatish (reuse) orqali
B) Faqat test muhitida ishlatiladi
C) DbContext'ni Singleton qilib qo'yadi
D) Migratsiyalarni tezlashtirish uchun
**To'g'ri javob: A**

**63. Repository va Unit of Work pattern'larini EF Core ustiga qo'shimcha qatlam sifatida qo'llash haqida qaysi fikr ko'proq to'g'ri hisoblanadi?**
A) Har doim majburiy, EF Core'siz loyihalar ishlamaydi
B) `DbContext` allaqachon Unit of Work va `DbSet<T>` Repository pattern'larining o'zini namoyon etadi; qo'shimcha abstraksiya faqat test qilishni osonlashtirish yoki data-access texnologiyasini almashtirish ehtimoli yuqori bo'lgan holatlarda qo'shimcha qiymat beradi
C) Bu pattern'lar faqat SQL Server bilan ishlaydi
D) Bu pattern'lar EF Core tomonidan taqiqlangan
**To'g'ri javob: B**

**64. `AsSplitQuery()` qaysi holatda `AsSingleQuery()`(standart)dan afzalroq bo'lishi mumkin?**
A) Bir nechta "one-to-many" `Include()` natijasida yuzaga keladigan cartesian explosion (ma'lumotlar hajmi va dublikatlarning keskin oshishi) muammosini kamaytirish uchun
B) Har doim, chunki u har doim tezroq
C) Faqat bitta jadval bilan ishlaganda
D) Faqat migratsiyalar uchun
**To'g'ri javob: A**

**65. Soft delete uchun Global Query Filter qo'llanganda, ba'zan o'chirilgan yozuvlarni ham ko'rish kerak bo'lsa nima qilish kerak?**
A) Bu imkonsiz, filter doim majburiy qo'llanadi
B) `IgnoreQueryFilters()` metodidan foydalanish mumkin
C) DbContext'ni butunlay qayta yaratish kerak
D) Faqat Raw SQL orqali
**To'g'ri javob: B**

---

## 5. Logging, Monitoring va Tracing (66–75)

**66. `ILogger<T>` interfeysida generic `<T>` parametrining vazifasi nima?**
A) Hech qanday vazifasi yo'q, faqat sintaktik talab
B) Log yozuvlariga avtomatik ravishda "category name" (odatda to'liq class nomi) qo'shib, log manbasini aniqlashni osonlashtiradi
C) Faqat log darajasini belgilaydi
D) Faqat exception turlarini filtrlaydi
**To'g'ri javob: B**

**67. Structured Logging oddiy matnli (string interpolation asosidagi) logdan nimasi bilan farq qiladi?**
A) Farqi yo'q, ikkalasi bir xil natija beradi
B) Log xabari va uning parametrlari alohida maydonlar sifatida saqlanadi, bu keyinchalik log'larni query/filter qilish va tahlil qilishni ancha osonlashtiradi (masalan, `logger.LogInformation("User {UserId} logged in", userId)`)
C) Structured Logging faqat Serilog'da mavjud
D) Structured Logging faqat error darajasidagi loglar uchun ishlatiladi
**To'g'ri javob: B**

**68. Serilog'ni ASP.NET Core'ga integratsiya qilishning standart usuli qaysi?**
A) Faqat `Console.WriteLine()` orqali
B) `UseSerilog()` metodini `WebApplicationBuilder`/`Host` konfiguratsiyasiga ulash va `Sink`larni (masalan, Console, File, Seq, Elasticsearch) sozlash orqali
C) Serilog ASP.NET Core bilan mos kelmaydi
D) Faqat `appsettings.json` fayli o'zgartirilsa yetarli, kod yozish shart emas
**To'g'ri javob: B**

**69. Log darajalari orasida qaysi tartib to'g'ri (pastdan yuqoriga jiddiylik bo'yicha)?**
A) Critical → Error → Warning → Information → Debug → Trace
B) Trace → Debug → Information → Warning → Error → Critical
C) Debug → Trace → Error → Warning → Information → Critical
D) Information → Trace → Debug → Warning → Critical → Error
**To'g'ri javob: B**

**70. Correlation ID nima uchun ishlatiladi?**
A) Faqat foydalanuvchi autentifikatsiyasi uchun
B) Bitta so'rovni turli servis va komponentlar bo'ylab kuzatish va bog'liq log yozuvlarini birlashtirish uchun noyob identifikator sifatida
C) Faqat ma'lumotlar bazasi tranzaksiyalari uchun
D) Faqat keshlashni boshqarish uchun
**To'g'ri javob: B**

**71. OpenTelemetry nimani birlashtiradi?**
A) Faqat logging
B) Tracing, Metrics va Logging (observability'ning uchta asosiy komponentini) uchun yagona, vendor-neutral standart va API/SDK to'plamini
C) Faqat CI/CD pipeline'larni
D) Faqat Kubernetes klasterlarini boshqarishni
**To'g'ri javob: B**

**72. Distributed Tracing (masalan, Jaeger/Zipkin) mikroservis arxitekturasida nima uchun muhim?**
A) Faqat UI dizayni uchun kerak
B) Bitta foydalanuvchi so'rovi bir nechta servis orqali o'tganda, har bir bosqichdagi kechikish va xatoliklarni vizual tarzda kuzatish va tezkor aniqlash imkonini beradi
C) Faqat ma'lumotlar bazasi zaxira nusxasini olish uchun
D) U faqat monolit ilovalar uchun mo'ljallangan
**To'g'ri javob: B**

**73. Application Insights yoki Prometheus/Grafana kabi vositalar asosan nima uchun ishlatiladi?**
A) Faqat kod yozish tezligini oshirish uchun
B) Ilovaning real vaqtdagi metrikalari (so'rov soni, javob vaqti, xatolik darajasi va h.k.)ni yig'ish, saqlash va vizualizatsiya qilish uchun
C) Faqat unit testlarni ishga tushirish uchun
D) Faqat SQL so'rovlarni optimallashtirish uchun
**To'g'ri javob: B**

**74. Sensitive ma'lumotlarni (parol, karta raqami) log'larda saqlashdan qanday himoyalanish kerak?**
A) Ularni hech qanday cheklovsiz to'liq log qilish kerak, chunki debugging uchun foydali
B) Log yozuvlarida bunday ma'lumotlarni maskировка qilish yoki umuman log qilmaslik, structured logging'da maxsus scrubbing/filtering mexanizmlaridan foydalanish
C) Faqat production muhitida bu masala muhim, development'da ahamiyati yo'q
D) Bu masala faqat frontend'ga tegishli
**To'g'ri javob: B**

**75. Request duration va error rate kabi metrikalar odatda qanday yig'iladi?**
A) Faqat qo'lda, log fayllarini ko'zdan kechirish orqali
B) Middleware yoki instrumentation kutubxonalari (masalan, OpenTelemetry Metrics) yordamida avtomatik ravishda to'planadi va monitoring tizimiga (Prometheus va h.k.) yuboriladi
C) Faqat SQL Server Profiler orqali
D) Bunday metrikalarni yig'ish ASP.NET Core'da mumkin emas
**To'g'ri javob: B**

---

## 6. Xavfsizlik (76–85)

**76. Authentication va Authorization o'rtasidagi farq nima?**
A) Ular bir xil tushuncha
B) Authentication — foydalanuvchi kimligini tasdiqlash, Authorization — tasdiqlangan foydalanuvchiga qanday amallarga ruxsat berilishini aniqlash
C) Authorization faqat parol tekshiradi
D) Authentication faqat rol asosida ishlaydi
**To'g'ri javob: B**

**77. JWT tokenining uchta asosiy qismi qaysilar?**
A) Username, Password, Salt
B) Header, Payload, Signature
C) Header, Body, Footer
D) Key, Value, Hash
**To'g'ri javob: B**

**78. Refresh Token nima uchun ishlatiladi?**
A) Access token'ning o'zini almashtiradi va shart emas
B) Qisqa umrli access token muddati tugaganda, foydalanuvchini qayta login qildirmasdan yangi access token olish imkonini beradi, xavfsizlik va qulaylik o'rtasidagi muvozanatni ta'minlaydi
C) Faqat parolni eslab qolish uchun
D) Faqat administrator huquqlarini berish uchun
**To'g'ri javob: B**

**79. OAuth2 va OpenID Connect o'rtasidagi asosiy farq nima?**
A) Ular bir xil protokol, faqat nomi boshqacha
B) OAuth2 — avtorizatsiya (resurslarga kirish huquqini berish) uchun protokol, OpenID Connect esa OAuth2 ustiga qurilgan va autentifikatsiya (identifikatsiya) qatlamini qo'shadi
C) OpenID Connect faqat mobil ilovalar uchun
D) OAuth2 faqat Google tomonidan ishlatiladi
**To'g'ri javob: B**

**80. Role-based va Policy-based authorization o'rtasidagi farq nima?**
A) Farqi yo'q
B) Role-based faqat foydalanuvchi rollariga asoslanadi, Policy-based esa moslashuvchan, murakkab shartlar (masalan, yosh, claim kombinatsiyasi) asosida avtorizatsiya qoidalarini belgilash imkonini beradi
C) Policy-based faqat administratorlar uchun ishlaydi
D) Role-based faqat API'larda, Policy-based faqat MVC'da ishlaydi
**To'g'ri javob: B**

**81. ASP.NET Core'da CSRF himoyasi qanday amalga oshiriladi?**
A) Faqat HTTPS ishlatish orqali
B) Anti-forgery token (`ValidateAntiForgeryToken`, `[AutoValidateAntiforgeryToken]`) mexanizmi orqali, forma bilan birga yuboriladigan noyob tokenni tekshirish
C) CSRF himoyasi ASP.NET Core'da avtomatik va sozlashsiz ishlaydi, hech narsa qilish shart emas
D) Faqat CORS sozlamalari orqali
**To'g'ri javob: B**

**82. XSS hujumlaridan himoyalanishning asosiy usuli qaysi?**
A) Faqat parolni murakkab qilish
B) Foydalanuvchidan kelgan ma'lumotlarni chiqishda (output) to'g'ri encode qilish (Razor avtomatik HTML encoding qiladi) va Content Security Policy qo'llash
C) Faqat HTTPS ishlatish
D) XSS faqat backend'ga tegishli, frontend bilan bog'liq emas
**To'g'ri javob: B**

**83. EF Core SQL Injection'dan qanday himoya qiladi?**
A) U hech qanday himoya bermaydi, dasturchi qo'lda tekshirishi kerak
B) LINQ so'rovlari va parametrlashtirilgan so'rovlar (`FromSqlInterpolated` kabi) orqali kiritilgan qiymatlarni SQL kodi sifatida emas, balki parametr sifatida uzatadi
C) Faqat Stored Procedure ishlatilganda himoya beradi
D) Faqat `AsNoTracking()` bilan birga ishlaganda
**To'g'ri javob: B**

**84. Parollarni xavfsiz saqlashda nima uchun oddiy hash (masalan, MD5) yetarli emas?**
A) MD5 juda sekin ishlaydi
B) MD5 kabi tez algoritmlar brute-force va rainbow table hujumlariga zaif, shu sababli maxsus, ataylab sekin va "salt"langan algoritmlar (BCrypt, Argon2, PBKDF2) ishlatiladi
C) MD5 umuman parollarni hash qila olmaydi
D) Bu masala faqat eski tizimlarga tegishli
**To'g'ri javob: B**

**85. HSTS (HTTP Strict Transport Security) nima uchun ishlatiladi?**
A) Faqat SEO uchun
B) Brauzerga saytga faqat HTTPS orqali murojaat qilishni majburlash, HTTP'ga tushib qolish (downgrade) hujumlaridan himoyalanish uchun
C) Faqat sertifikatni avtomatik yangilash uchun
D) HSTS faqat API'larda ishlatiladi, veb-saytlarda emas
**To'g'ri javob: B**

---

## 7. Arxitektura va Dizayn Pattern'lari (86–95)

**86. Clean Architecture (Onion Architecture)ning asosiy g'oyasi nima?**
A) Barcha kodni bitta loyihada saqlash
B) Bog'liqliklar (dependencies) tashqi qatlamlardan ichki qatlamlarga (Domain'ga) qarab yo'nalgan bo'lishi, Domain qatlami hech qanday tashqi texnologiyaga (DB, UI) bog'liq bo'lmasligi kerak
C) Faqat mikroservislar uchun mo'ljallangan
D) Barcha logikani Controller ichida yozish
**To'g'ri javob: B**

**87. CQRS (Command Query Responsibility Segregation) nima uchun qo'llaniladi?**
A) Faqat ma'lumotlar bazasini zaxiralash uchun
B) O'qish (query) va yozish (command) operatsiyalarini alohida modellar/yo'llar orqali ajratish, bu murakkab domenlarda moslashuvchanlik va masshtablanishni oshiradi
C) CQRS faqat mikroservislarda ishlatiladi, monolit'da foydasi yo'q
D) Faqat UI dizayni bilan bog'liq
**To'g'ri javob: B**

**88. MediatR kutubxonasi CQRS'ni amalga oshirishda qanday rol o'ynaydi?**
A) Ma'lumotlar bazasi migratsiyasini boshqaradi
B) Command va Query'larni handler'larga yo'naltiruvchi (mediator pattern) vosita bo'lib, Controller'lar va biznes logika o'rtasidagi to'g'ridan-to'g'ri bog'liqlikni kamaytiradi
C) Faqat logging uchun ishlatiladi
D) Faqat authentication uchun ishlatiladi
**To'g'ri javob: B**

**89. EF Core ustiga qo'shimcha Repository/Unit of Work pattern qo'shishning eng ko'p tanqid qilinadigan tomoni nima?**
A) Bu hech qachon tanqid qilinmaydi
B) `DbContext` allaqachon shu funksiyalarni bajaradi, shuning uchun qo'shimcha abstraksiya ba'zan ortiqcha murakkablik (over-engineering) va EF Core'ning kuchli tomonlarini (masalan, `IQueryable` moslashuvchanligini) cheklashi mumkin
C) U faqat NoSQL bazalar bilan ishlaydi
D) U DI bilan mutlaqo mos kelmaydi
**To'g'ri javob: B**

**90. Single Responsibility Principle (SRP)ga real misol qaysi?**
A) Bitta klass ham ma'lumotlarni saqlash, ham email yuborish, ham hisobotlash logikasini bajarishi
B) `OrderService` faqat buyurtma bilan bog'liq biznes logikani bajaradi, email yuborish `EmailService`ga, hisobotlash boshqa servisga ajratiladi
C) Barcha logikani bitta "Utils" klassiga joylash
D) SRP faqat interfeyslarga tegishli, klasslarga emas
**To'g'ri javob: B**

**91. Mikroservis arxitekturasi monolitga nisbatan qaysi holatda ko'proq oqlanadi?**
A) Kichik jamoa, kichik loyiha va tez MVP kerak bo'lganda
B) Katta, murakkab tizim bo'lib, turli qismlar mustaqil masshtablanishi, alohida deploy qilinishi va turli jamoalar tomonidan mustaqil rivojlantirilishi kerak bo'lganda
C) Faqat startaplar uchun, katta kompaniyalar uchun mos emas
D) Har doim monolitdan afzal, hech qanday kamchiligi yo'q
**To'g'ri javob: B**

**92. RabbitMQ yoki Kafka kabi Message Broker'larning asosiy vazifasi nima?**
A) Faqat ma'lumotlar bazasi sifatida ishlatiladi
B) Servislar o'rtasida asinxron, decoupled (bog'liqligi kamaytirilgan) xabar almashinuvini ta'minlash, bu orqali tizim chidamliligi va masshtablanishini oshirish
C) Faqat frontend va backend o'rtasidagi aloqa uchun
D) Faqat logging uchun ishlatiladi
**To'g'ri javob: B**

**93. Saga Pattern distributed tranzaksiyalarni qanday boshqaradi?**
A) Bitta global lock orqali barcha servislarni bloklaydi
B) Uzoq davom etadigan tranzaksiyani bir qator lokal tranzaksiyalarga bo'ladi, har biridan keyin kompensatsion (compensating) amal orqali xatolik yuz berganda oldingi holatga qaytarish imkonini beradi
C) Faqat monolit ilovalarda ishlatiladi
D) Saga faqat o'qish operatsiyalari uchun mo'ljallangan
**To'g'ri javob: B**

**94. Polly kutubxonasi orqali amalga oshiriladigan Circuit Breaker pattern'ining maqsadi nima?**
A) Ma'lumotlar bazasini tezlashtirish
B) Doimiy muvaffaqiyatsiz bo'layotgan tashqi chaqiruvlarni vaqtincha to'xtatib, tizimni keskin yuklanishdan va "cascading failure"dan himoyalash
C) Faqat logging formatini o'zgartirish
D) Faqat unit test yozish uchun mo'ljallangan
**To'g'ri javob: B**

**95. Domain-Driven Design'dagi "Aggregate" tushunchasi nimani anglatadi?**
A) Faqat ma'lumotlar bazasidagi jadval
B) Bir-biri bilan bog'liq entity va value object'lardan tashkil topgan, yagona "aggregate root" orqali boshqariladigan va izchillik (consistency) chegarasini belgilaydigan klaster
C) Faqat UI komponentlari to'plami
D) Faqat DTO'larning yig'indisi
**To'g'ri javob: B**

---

## 8. Testing, DevOps va Boshqa Mavzular (96–100)

**96. Unit Test va Integration Test o'rtasidagi asosiy farq nima?**
A) Ular bir xil, faqat nomi boshqacha
B) Unit Test alohida komponentni tashqi bog'liqliklardan izolyatsiya qilingan holda (mock'lar yordamida) tekshiradi, Integration Test esa bir nechta komponent (masalan, DB, API) birgalikda qanday ishlashini tekshiradi
C) Integration Test faqat frontend uchun
D) Unit Test faqat production muhitida ishga tushiriladi
**To'g'ri javob: B**

**97. `WebApplicationFactory<T>` sinfi nima uchun ishlatiladi?**
A) Faqat unit test uchun mock obyekt yaratish
B) ASP.NET Core ilovasini test uchun in-memory server sifatida ishga tushirish va integration test yozishni osonlashtirish uchun
C) Faqat production konfiguratsiyasini boshqarish uchun
D) Faqat Docker konteynerlarini boshqarish uchun
**To'g'ri javob: B**

**98. CI/CD pipeline'da "CI" (Continuous Integration) bosqichi odatda nimalarni o'z ichiga oladi?**
A) Faqat production serverga deploy qilish
B) Kodni build qilish, avtomatik testlarni ishga tushirish va kod sifatini tekshirish — o'zgarishlar asosiy branch'ga integratsiya qilinishidan oldin
C) Faqat monitoring sozlash
D) Faqat ma'lumotlar bazasi zaxira nusxasini olish
**To'g'ri javob: B**

**99. Dockerfile'da multi-stage build ishlatishning asosiy afzalligi nima?**
A) Faqat build vaqtini tezlashtiradi, boshqa foydasi yo'q
B) Build uchun kerakli og'ir vositalar (SDK) va production uchun kerakli yengil runtime'ni ajratib, yakuniy image hajmini sezilarli kamaytiradi va xavfsizlikni oshiradi
C) Multi-stage build faqat .NET Framework uchun ishlaydi
D) U faqat Linux konteynerlarida ishlaydi
**To'g'ri javob: B**

**100. Trunk-Based Development strategiyasining Git Flow'dan asosiy farqi nima?**
A) Farqi yo'q, ikkalasi bir xil jarayon
B) Trunk-Based Development'da dasturchilar kichik, tez-tez o'zgarishlarni to'g'ridan-to'g'ri (yoki qisqa umrli feature branch orqali) asosiy branch'ga integratsiya qiladi, Git Flow esa uzoqroq umr ko'radigan alohida branch'lar (develop, release, feature) tuzilmasiga tayanadi
C) Trunk-Based Development faqat kichik loyihalar uchun, Git Flow esa faqat katta loyihalar uchun
D) Git Flow versiya nazorati tizimi emas
**To'g'ri javob: B**

---

### Baholash bo'yicha tavsiya
Har bir bo'limdan kamida 80% to'g'ri javob berilsa, shu mavzu bo'yicha senior darajaga yaqin bilim darajasi deb hisoblash mumkin. Xato javob berilgan savollarni alohida ro'yxatga yozib, ular bo'yicha chuqurroq amaliy misollar bilan qayta mustahkamlash tavsiya etiladi.
