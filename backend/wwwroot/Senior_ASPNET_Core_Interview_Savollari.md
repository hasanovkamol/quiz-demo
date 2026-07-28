# Senior ASP.NET Core Web Developer uchun 100 ta Intervyu Savoli

Quyida ASP.NET Core, EF Core, C#, logging/tracing, arxitektura, xavfsizlik, performance va boshqa senior darajadagi mavzular bo'yicha 100 ta savol keltirilgan. Savollar toifalarga bo'lingan, shunda tayyorgarlikni tizimli olib borish qulay bo'ladi.

---

## 1. C# Til Asoslari va Ilg'or Mavzular (1–15)

1. `value type` va `reference type` o'rtasidagi farq nima? Stack va Heap qanday ishlaydi?
2. `async/await` mexanizmi ichki ishlashi (state machine) qanday tuzilgan?
3. `Task` va `Task<T>` o'rtasidagi farq, `ValueTask` qachon ishlatiladi?
4. `ConfigureAwait(false)` nima uchun kerak va qachon ishlatish tavsiya etiladi?
5. `Deadlock` async kodda qanday yuzaga kelishi mumkin va uni qanday oldini olish kerak?
6. `delegate`, `Func`, `Action`, `Predicate` o'rtasidagi farqlar.
7. `event` va `delegate` qanday bog'liq? `+=` operatori ortida nima yotadi?
8. `Generics` nima uchun kerak, `where T : class, new()` kabi cheklovlar nimani anglatadi?
9. `IEnumerable` va `IQueryable` o'rtasidagi farq, ularning `LINQ` bilan ishlashda ahamiyati.
10. `yield return` qanday ishlaydi va u qachon foydali?
11. `Boxing` va `Unboxing` nima, performance'ga qanday ta'sir qiladi?
12. `IDisposable` va `using` statement, `Finalize` bilan farqi.
13. `struct` va `record` o'rtasidagi farq, qachon qaysi birini ishlatish kerak?
14. `null`-bilan ishlash: `nullable reference types`, `??`, `??=`, `?.` operatorlari.
15. `Garbage Collector` generatsiyalari (Gen0, Gen1, Gen2) qanday ishlaydi?

## 2. ASP.NET Core Asoslari (16–35)

16. ASP.NET Core'da `Middleware pipeline` qanday ishlaydi? `app.Use` va `app.Run` farqi.
17. `Dependency Injection` container'ining ichki ishlash tamoyili qanday?
18. `Transient`, `Scoped`, `Singleton` service lifetime'lari orasidagi farq va tipik xatolar.
19. `IHostedService` va `BackgroundService` nima uchun kerak?
20. `Startup.cs` (yoki `Program.cs` minimal hosting model) tuzilishi va `WebApplicationBuilder` ishlashi.
21. `Routing`: attribute routing va conventional routing farqi.
22. `Model Binding` va `Model Validation` qanday ishlaydi?
23. `Filters` turlari: Authorization, Resource, Action, Exception, Result — qaysi tartibda ishga tushadi?
24. `Action Filter` va `Middleware` o'rtasida qanday farq bor, qachon qaysi birini tanlash kerak?
25. `Razor Pages` va `MVC Controller` yondashuvlari o'rtasidagi farq.
26. `Minimal API` va an'anaviy Controller-based API farqlari, afzallik/kamchiliklari.
27. `CORS` sozlamalari qanday ishlaydi va xavfsizlik nuqtai nazaridan nimalarga e'tibor berish kerak?
28. `Configuration` tizimi: `appsettings.json`, environment variables, `IOptions<T>` qanday ishlaydi?
29. `IOptions`, `IOptionsSnapshot`, `IOptionsMonitor` o'rtasidagi farq.
30. `Kestrel` server nima va u qanday ishlaydi? Reverse proxy (Nginx/IIS) bilan aloqasi.
31. `Exception Handling Middleware` qanday to'g'ri tashkil qilinadi (`UseExceptionHandler`, `ProblemDetails`)?
32. `Health Checks` nima uchun kerak va qanday sozlanadi?
33. `Response Caching` va `Output Caching` o'rtasidagi farq.
34. `Rate Limiting` middleware qanday ishlaydi (.NET 7+)?
35. `SignalR` nima va real-time komunikatsiya uchun qanday ishlatiladi?

## 3. Web API va REST Arxitekturasi (36–47)

36. RESTful API dizayn tamoyillari nimalardan iborat?
37. `HTTP status code`larni to'g'ri tanlash: 400 vs 422, 401 vs 403 farqi.
38. API versioning strategiyalari (URL, header, query string) qanday amalga oshiriladi?
39. `DTO` (Data Transfer Object) nima uchun kerak, entity'ni to'g'ridan-to'g'ri qaytarish nima uchun yomon amaliyot?
40. `AutoMapper` yoki `Mapster` kabi mapping kutubxonalarining afzalliklari va xavflari.
41. `Idempotency` tushunchasi va uni API'da qanday amalga oshirish mumkin?
42. `Pagination`, `filtering`, `sorting` katta hajmdagi ma'lumotlar uchun qanday to'g'ri loyihalanadi?
43. `Swagger/OpenAPI` hujjatlashtirish qanday sozlanadi va nima uchun muhim?
44. `gRPC` va REST API o'rtasidagi farq, qachon gRPC afzal bo'ladi?
45. `GraphQL`ning REST'ga nisbatan afzallik va kamchiliklari.
46. `HATEOAS` nima va u amaliyotda qanchalik qo'llaniladi?
47. `File upload/download` katta fayllar bilan qanday samarali ishlanadi (streaming)?

## 4. Entity Framework Core (48–65)

48. `DbContext` lifecycle qanday boshqariladi va `AddDbContext` scope'i nima?
49. `Change Tracker` qanday ishlaydi va `AsNoTracking()` nima uchun kerak?
50. `Migrations` mexanizmi qanday ishlaydi, production muhitda migratsiyalarni qo'llashning eng yaxshi amaliyotlari qanday?
51. `Lazy Loading`, `Eager Loading` (`Include`), `Explicit Loading` o'rtasidagi farq.
52. `N+1 query problem` nima va uni qanday aniqlash/oldini olish mumkin?
53. `IQueryable` va `IEnumerable` EF Core kontekstida farqi — qachon SQL'ga tarjima qilinadi?
54. `Transactions` EF Core'da qanday boshqariladi (`BeginTransaction`, `SaveChanges` atomikligi)?
55. `Optimistic Concurrency` va `RowVersion`/`Concurrency Token` qanday ishlaydi?
56. `Fluent API` va `Data Annotations` o'rtasidagi farq, qachon qaysi birini tanlash kerak?
57. `One-to-Many`, `Many-to-Many`, `Owned Entity Types` qanday konfiguratsiya qilinadi?
58. `Global Query Filters` (masalan, soft delete uchun) qanday ishlaydi?
59. `Raw SQL` va `Stored Procedure`larni EF Core orqali chaqirish qanday amalga oshiriladi?
60. `Compiled Queries` performance'ni qanday yaxshilaydi?
61. `Bulk Insert/Update/Delete` EF Core'da samarali qanday amalga oshiriladi?
62. `DbContext Pooling` nima uchun kerak va qanday ishlaydi?
63. EF Core'da `Unit of Work` va `Repository Pattern`ni qo'llash kerakmi yoki `DbContext` o'zi yetarlimi?
64. Migratsiyalarni CI/CD pipeline orqali avtomatlashtirish qanday to'g'ri yo'lga qo'yiladi?
65. `Split Query` va `Single Query` (`AsSplitQuery`) rejimlari orasidagi farq qachon muhim bo'ladi?

## 5. Logging, Monitoring va Tracing (66–75)

66. `ILogger<T>` va built-in logging providerlar qanday ishlaydi?
67. `Structured Logging` nima va u oddiy matnli logdan nima bilan farq qiladi?
68. `Serilog` yoki `NLog` kabi kutubxonalarni ASP.NET Core'ga integratsiya qilish qanday amalga oshiriladi?
69. `Log Levels` (Trace, Debug, Information, Warning, Error, Critical) qanday to'g'ri qo'llaniladi?
70. `Correlation ID` nima va distributed sistemalarda so'rovlarni kuzatishda qanday yordam beradi?
71. `OpenTelemetry` nima va u `tracing`, `metrics`, `logging`ni qanday birlashtiradi?
72. `Distributed Tracing` (masalan, Jaeger, Zipkin) qanday ishlaydi va mikroservislarda nima uchun muhim?
73. `Application Insights` yoki Prometheus/Grafana kabi monitoring vositalari qanday integratsiya qilinadi?
74. Production muhitda log'larni qanday xavfsiz saqlash kerak (sensitive data masking)?
75. `Performance Counters` va `Metrics` (masalan, request duration, error rate) qanday to'planadi?

## 6. Xavfsizlik (76–85)

76. `Authentication` va `Authorization` o'rtasidagi farq.
77. `JWT` (JSON Web Token) qanday ishlaydi, uning tuzilishi (header, payload, signature) nimadan iborat?
78. `Refresh Token` mexanizmi nima uchun kerak va qanday xavfsiz amalga oshiriladi?
79. `OAuth2` va `OpenID Connect` o'rtasidagi farq.
80. `Role-based` va `Policy-based` authorization o'rtasidagi farq.
81. `CSRF` (Cross-Site Request Forgery) himoyasi ASP.NET Core'da qanday amalga oshiriladi?
82. `XSS` (Cross-Site Scripting) hujumlaridan qanday himoyalanish mumkin?
83. `SQL Injection`dan EF Core qanday himoya qiladi va parametrlashtirilgan so'rovlar nima uchun muhim?
84. Parollarni xavfsiz saqlash (`hashing`, `salting`, `BCrypt`/`Argon2`) qanday amalga oshiriladi?
85. `HTTPS`, `HSTS` va sertifikatlarni boshqarish bo'yicha eng yaxshi amaliyotlar.

## 7. Arxitektura va Dizayn Pattern'lari (86–95)

86. `Clean Architecture` yoki `Onion Architecture` nima va u qanday qatlamlardan iborat?
87. `CQRS` (Command Query Responsibility Segregation) nima uchun kerak va qachon qo'llash maqsadga muvofiq?
88. `MediatR` kutubxonasi CQRS'ni amalga oshirishda qanday yordam beradi?
89. `Repository` va `Unit of Work` pattern'lari EF Core bilan birga qanchalik zarur?
90. `SOLID` prinsiplarini real loyihada qo'llashga misollar keltiring.
91. `Microservices` va `Monolith` arxitekturasi o'rtasidagi farq, qachon qaysi birini tanlash kerak?
92. `Event-Driven Architecture` va `Message Broker`lar (RabbitMQ, Kafka, Azure Service Bus) qanday ishlaydi?
93. `Saga Pattern` distributed tranzaksiyalarni boshqarishda qanday yordam beradi?
94. `Circuit Breaker` va `Polly` kutubxonasi resilience'ni qanday ta'minlaydi?
95. `Domain-Driven Design (DDD)`dagi asosiy tushunchalar (Aggregate, Entity, Value Object, Bounded Context) nimalardan iborat?

## 8. Testing, DevOps va Boshqa Mavzular (96–100)

96. `Unit Test`, `Integration Test` va `E2E Test` o'rtasidagi farq, ASP.NET Core'da qanday yoziladi (`xUnit`, `Moq`, `WebApplicationFactory`)?
97. `CI/CD pipeline`da (GitHub Actions, Azure DevOps, GitLab CI) ASP.NET Core loyihasini build va deploy qilish bosqichlari qanday tashkil etiladi?
98. `Docker` konteynerida ASP.NET Core ilovasini ishga tushirish va `Dockerfile` optimallashtirish qanday amalga oshiriladi?
99. Versiya nazorati tizimlarida (Git, va eski tizimlar kabi Trac/TFS) branching strategiyalari (Git Flow, Trunk-Based Development) qanday tanlanadi?
100. Ko'p protsessli/serverli muhitda `Caching` strategiyalari (`In-Memory`, `Distributed Cache` — Redis) qanday to'g'ri loyihalanadi?

---

### Qo'shimcha maslahat
Intervyuga tayyorlanishda har bir savolga faqat nazariy javob emas, balki **kichik kod misoli** va **real loyihadagi tajriba** bilan javob berishga harakat qiling — senior darajadagi intervyularda buni ayniqsa qadrlashadi.
