using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetArchitectureQuizzes()
    {
        return new List<Quiz>
        {
            CreateQuiz(
                "Software Architecture & SOLID Fundamentals",
                "architecture",
                "Software Architecture",
                "SOLID prinsiplari, Clean Code, Layered Architecture va Design Patterns asoslari bo'yicha professional savollar.",
                "Easy",
                "layers",
                GenerateArchitectureEasyQuestions()
            ),
            CreateQuiz(
                "Clean Architecture, DDD & Microservices Design",
                "architecture",
                "Software Architecture",
                "Clean Architecture, Domain-Driven Design (DDD), CQRS, Outbox Pattern va Saga Orchestration bo'yicha senior savollar.",
                "Medium",
                "cpu",
                GenerateArchitectureMediumQuestions()
            ),
            CreateQuiz(
                "High-Availability Enterprise System Architecture",
                "architecture",
                "Software Architecture",
                "Event Sourcing Engine, Distributed Transactions, CAP Theorem, Fencing Tokens va Multi-Region Architecture bo'yicha principal savollar.",
                "Hard",
                "terminal",
                GenerateArchitectureHardQuestions()
            )
        };
    }

    private static List<Question> GenerateArchitectureEasyQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "SOLID prinsiplaridan 'Single Responsibility Principle' (SRP) ning asl ma'nosi va amaldagi qo'llanilishi nimada?",
                "public class OrderProcessor { /* Only manages order business logic */ }",
                new List<string> {
                    "Har bir modul yoki sinf faqat bitta mas'uliyat va o'zgarish uchun bitta mantiqiy sababga (one reason to change) ega bo'lishi kerak",
                    "Har bir sinf faqat 1 ta metoddan iborat bo'lishi shart",
                    "Barcha kodingiz faqat bitta faylda yozilishi shart",
                    "Sinf faqat 1 ta o'zgaruvchiga ega bo'lishi kerak"
                },
                "SRP bo'yicha har bir sinf faqat bitta mantiqiy akter yoki vazifa uchun javobgar bo'lishi lozim (Single reason to change)."
            ),
            CreateQuestion(
                "SOLID prinsiplaridan 'Open/Closed Principle' (OCP) ni buzmasdan yangi funksionallik qo'shish qanday amalga oshiriladi?",
                "public interface IPaymentStrategy { Task PayAsync(decimal amount); }",
                new List<string> {
                    "Mavjud sinf kodiga tegmasdan va o'zgartirmasdan, interfeys va polimorfizm orqali yangi sinf kengaytmasi yaratish orqali",
                    "Mavjud sinf ichidagi switch-case shartlariga yangi case qo'shish",
                    "Eski kodlarni o'chirib tashlab noldan yozish",
                    "Barcha metodlarni private qilish"
                },
                "OCP ga ko'ra tizim kengaytirish uchun ochiq, lekin mavjud kodlarni o'zgartirish uchun yopiq bo'lishi lozim."
            ),
            CreateQuestion(
                "SOLID prinsiplaridan 'Liskov Substitution Principle' (LSP) nimani talab qiladi?",
                "// Parent p = new Child(); -> Should behave seamlessly without throwing NotImplementedException!",
                new List<string> {
                    "Voris sinf (Child class) ota sinf (Parent class) o'rnini almashtirganda dastur mantiqi va kutilgan xulq-atvori buzilmasligi lozim",
                    "Voris sinf ota sinfning barcha metodlarida Exception otishi kerak",
                    "Voris sinf faqat private metodlarga ega bo'lishi kerak",
                    "Voris sinf ota sinf atributlarini ishlatmasligi kerak"
                },
                "LSP ga ko'ra Voris sinf ota sinf o'rniga qo'yilganda xulq-atvor shartnomasi buzilmasligi kerak."
            ),
            CreateQuestion(
                "SOLID prinsiplaridan 'Interface Segregation Principle' (ISP) ning maqsadi nimadan iborat?",
                "public interface IPrint { void Print(); }\npublic interface IScan { void Scan(); }",
                new List<string> {
                    "Mijozlar o'zlari ishlatmaydigan ortiqcha va keraksiz metodlarga ega ulkan interfeyslarga bo me me'nilishga majburlanmasligi lozim",
                    "Barcha metodlarni bitta ulkan IApplicationService interfeysiga yig'ish",
                    "Interfeyslarni mutlaqo ishlatmaslik",
                    "Interfeyslarda faqat 1 ta o'zgaruvchi saqlash"
                },
                "ISP ga muvofiq bitta katta va umumiy interfeys o'rniga muayyan maqsadga yo'naltirilgan ixcham interfeyslar yaratiladi."
            ),
            CreateQuestion(
                "SOLID prinsiplaridan 'Dependency Inversion Principle' (DIP) bo'yicha qaysi ta'rif to'g'ri?",
                "public OrderService(IOrderRepository repository) // High level depends on Abstraction!",
                new List<string> {
                    "Yuqori darajadagi biznes modullari quyi darajadagi modullarga emas, balki abstraksiyaga (interfeyslarga) bog me me me'lanishi kerak",
                    "Quyi darajadagi modullarga to'g'ridan-to'g'ri `new` bilan bog me me'lanish kerak",
                    "Barcha bog'liqliklar statik bo'lishi shart",
                    "Interfeyslar ishlatish taqiqlanadi"
                },
                "DIP modullarni abstraksiyalar (interface/abstract class) orqali ajratib bo'sh bog me'liqlik (loose coupling) beradi."
            ),
            CreateQuestion(
                "Design Pattern-lardan 'Factory Method' va 'Abstract Factory' o'rtasidagi farq nimada?",
                "public interface IVehicleFactory { ICar CreateCar(); IBike CreateBike(); }",
                new List<string> {
                    "Factory Method bitta obyekt yaratish metodini abstraktsiya qiladi; Abstract Factory esa o'zaro bog me me'liq obyektlar oilasini yaratish interfeysini taqdim etadi",
                    "Factory Method faqat SQL bazada ishlaydi",
                    "Abstract Factory faqat Singleton bo'lishi shart",
                    "Ikkala pattern ham bir xil ishlaydi"
                },
                "Factory Method 1 ta ob'ekt yaratadi, Abstract Factory esa bog'liq bo'lgan butun obyektlar turkumini (family) beradi."
            ),
            CreateQuestion(
                "Design Pattern-lardan 'Adapter Pattern' qaysi muammoni hal etadi?",
                "public class ThirdPartyLogAdapter : ILogger { ... }",
                new List<string> {
                    "Mos kelmaydigan (incompatible) ikki xil interfeysga ega sinflarni bir-biri bilan muvofiq holda ishlashiga imkon beradi",
                    "Obyekt nusxasini olib yangi obyekt yaratadi",
                    "Baza tranzaksiyalarini commit qiladi",
                    "Faqat JSON fayllarni keshlaydi"
                },
                "Adapter Pattern mos kelmaydigan interfeyslar o'rtasida ko'prik vazifasini o'taydi."
            ),
            CreateQuestion(
                "Design Pattern-lardan 'Observer Pattern' qanday ishlaydi va u Event-Driven Architecture-da qanday qo'llaniladi?",
                "public interface ISubject { void Attach(IObserver observer); void Notify(); }",
                new List<string> {
                    "Bir obyektning (Subject) holati o'zgarganda, unga obuna bo'lgan barcha tinglovchilar (Observers) avtomatik xabardor qilinadi",
                    "Obyekt holatini doimiy ravishda faylga yozib boradi",
                    "Faqat 1 ta tinglovchiga ruxsat beradi",
                    "Faqat multithreading-ni o'chiradi"
                },
                "Observer Pattern o'zaro bog'liq bo'lmagan obyektlar o'rtasida One-to-Many bildirishnoma uzatish imkonini beradi."
            ),
            CreateQuestion(
                "Clean Code prinsiplarida 'KISS' (Keep It Simple, Stupid) va 'YAGNI' (You Aren't Gonna Need It) nimani ta'kidlaydi?",
                null,
                new List<string> {
                    "KISS — kodni iloji boricha sodda va tushunarli tutish; YAGNI — hozir kerak bo'lmagan ortiqcha funksionallik va abstraktsiyalarni oldindan yozmaslik",
                    "YAGNI — barcha kodingizni o'chirib tashlash",
                    "KISS — faqat static metodlar yozish",
                    "Ikkala prinsips ham keshni tozalash haqida"
                },
                "KISS soddalikni targ'ib qiladi, YAGNI esa kelajak uchun taxminiy ortiqcha murakkablik va koddagi ortiqchaliklarni rad etadi."
            ),
            CreateQuestion(
                "Layered Architecture (Ko'p qatlamli arxitektura) da Presentation, Business Logic va Data Access qatlamlarining vazifasi nimada?",
                "UI -> Service -> Repository -> Database",
                new List<string> {
                    "Presentation — UI va so'rovlarni qabul qilish; Business Logic — biznes qoidalari; Data Access — bazaga bog me me me me'lanish va CRUD amallari",
                    "Data Access qatlami UI bilan to'g'ridan-to me'ri muloqot qilishi shart",
                    "Presentation qatlamida SQL so'rovlari yoziladi",
                    "Business Logic qatlami faqat HTML tayyorlaydi"
                },
                "Layered Architecture mas'uliyatlarni alohida qatlamlarga (Presentation, Business Logic, Data Access) ajratadi."
            )
        };
    }

    private static List<Question> GenerateArchitectureMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "Clean Architecture (Onion / Hexagonal Architecture) da qatlamlar bog'liqligi (Dependency Rule) qaysi tomonga yo'naltirilgan bo'lishi shart?",
                "Domain (Entities) <- Application (Use Cases) <- Infrastructure / UI",
                new List<string> {
                    "Tashqi qatlamlar (UI, Database, Infrastructure) ichki yadroga (Domain / Core) bog me me'lanishi shart; Core yadro tashqariga bog me'lanmaydi",
                    "Domain qatlami Database va UI-ga bog me me'lanishi shart",
                    "Barcha qatlamlar bir-biriga doiraviy bog me'lanadi",
                    "UI to'g'ridan-to'g'ri Database-ga bog me'lanishi shart"
                },
                "Clean Architecture-da bog'liqlik kuchi faqat markazga (Domain/Entities) qarab yo'naladi. Tashqi o'zgarishlar biznes mantiqqa ta'sir etmaydi."
            ),
            CreateQuestion(
                "Domain-Driven Design (DDD) da 'Bounded Context' va 'Ubiquitous Language' konseptlari nimani anglatadi?",
                "// Sales Context vs Shipping Context",
                new List<string> {
                    "Bounded Context — domendagi aniq chegaralangan mantiqiy soha; Ubiquitous Language — biznes ekspertlar va dasturchilar o'rtasidagi yagona tushunarli terminologiya",
                    "Bounded Context faqat bitta database jadvali",
                    "Ubiquitous Language faqat C# dasturlash tili",
                    "Ikkala konsept ham faqat UI interfeysi bo'yicha"
                },
                "Bounded Context model va terminlar o'z ma'nosini saqlaydigan chegarani bildiradi. Ubiquitous Language esa umumiy biznes tilidir."
            ),
            CreateQuestion(
                "Domain-Driven Design (DDD) da 'Aggregate Root' va 'Entity' va 'Value Object' farqlari nimada?",
                "public class Order : AggregateRoot { public Address ShippingAddress { get; } /* Value Object */ }",
                new List<string> {
                    "Entity — unikal ID ga ega; Value Object — faqat qiymatlar majmuasi (ID siz); Aggregate Root — ichki entitiylar guruhining izchillik va tranzaksiya darvozaboni",
                    "Value Object har doim unikal ID-ga ega bo'lishi shart",
                    "Aggregate Root faqat SQL database jadvali",
                    "Entity-larni tengligini faqat qiymati bo'yicha solishtiriladi"
                },
                "Entity ID ga ega, Value Object qiymatlari teng bo'lsa teng hisoblanadi. Aggregate Root tranzaksiyaviy chegara (consistency boundary) vazifasini bajaradi."
            ),
            CreateQuestion(
                "CQRS (Command Query Responsibility Segregation) patternida Command va Query so'rovlarining farqi va afzalligi nimada?",
                "public class CreateOrderCommand : IRequest<Guid> { ... }\npublic class GetOrderByIdQuery : IRequest<OrderDto> { ... }",
                new List<string> {
                    "Command — ma'lumotlarni o'zgartiradi (Write, State change); Query — faqat ma'lumotlarni o'qiydi (Read, No state change). Ikkala modelni alohida optimallash imkonini beradi",
                    "Query ma'lumotlarni bazadan o'chiradi",
                    "Command faqat HTML chiqarish uchun ishlatiladi",
                    "Ikkala model ham bitta jadval va DTO bilan ishlashi shart"
                },
                "CQRS Read (Query) va Write (Command) modellarini ajratib, ularning scalability va unumdorligini alohida oshirish imkonini beradi."
            ),
            CreateQuestion(
                "Transactional Outbox Pattern mikroservislarda qaysi muammoni hal qiladi?",
                "// Save Order AND OutboxMessage in ONE DB Transaction -> OutboxPublisher reads and sends to RabbitMQ",
                new List<string> {
                    "Ma'lumotlar bazasi tranzaksiyasi saqlanishi va Xabarlar brokeriga (RabbitMQ) event yuborilishi o'rtasidagi atamarlik (Dual-write problem) ni ta'minlaydi",
                    "Faqat HTML keshini tozalaydi",
                    "Faqat UI duplikasiyasini oldini oladi",
                    "Serverni o me me me'chiradi"
                },
                "Outbox Pattern baza saqlanishi bilan xabar yuborilishi o'rtasidagi atamarlikni ta'minlaydi va xabar yo'qolishini oldini oladi."
            ),
            CreateQuestion(
                "Saga Pattern (Choreography vs Orchestration) taqsimlangan tranzaksiyalarda (Distributed Transactions) qanday ishlaydi?",
                "OrderCreated -> ReserveInventory -> ProcessPayment -> (If payment fails) -> Compensating Transaction: UnreserveInventory",
                new List<string> {
                    "2PC (Two-Phase Commit) o'rniga ketma-ket mahalliy tranzaksiyalar va xatolik bo'lganda Kompensatsiyaviy tranzaksiyalar (Compensating transactions) orqali holatni bekor qiladi",
                    "Barcha servislarga bir vaqtda SQL lock qo me me'yadi",
                    "Faqat single-node bazalarda ishlaydi",
                    "Kompensatsiya tranzaksiyalarini taqiqlaydi"
                },
                "Saga Pattern taqsimlangan tizimlarda 2PC o'rniga kompensatsiyaviy tranzaksiyalar yordamida eventual consistency beradi."
            ),
            CreateQuestion(
                "Circuit Breaker Pattern (Closed, Open, Half-Open holatlari) mikroservislar barqarorligida qanday ishlaydi?",
                "// Closed (Normal) -> Open (Fails fast) -> Half-Open (Probe success)",
                new List<string> {
                    "Tashqi servis ishlamay qolganda so'rovlar oqimini to'xtatib (Open state) tezkor xato qaytaradi, vaqt o'tib (Half-Open) uni qayta tekshirib tiklaydi",
                    "Serverni avtomatik Formatsiyalaydi",
                    "Faqat database parolini o'zgartiradi",
                    "Circuit Breaker so'rovlarni abadiy kutishga qo me me'yadi"
                },
                "Circuit Breaker ishlamayotgan tashqi servisga tinimsiz so'rov yuborib resurslarni tugatmaslik uchun Fail Fast mexanizmini beradi."
            ),
            CreateQuestion(
                "API Gateway Pattern (masalan YARP, Ocelot) mikroservislar arxitekturasida qanday vazifalarni o'z bo'yniga oladi?",
                "Client -> API Gateway (Auth, Rate Limit, Routing, SSL Termination) -> Microservices",
                new List<string> {
                    "So'rovlarni to'g'ri mikroservisga marshrutlash (Routing), Auth, Rate Limiting, SSL Termination va Response Aggregation funksiyalarini beradi",
                    "Faqat static fayllarni yuklaydi",
                    "Faqat database migration bajaradi",
                    "Mikroservislar kodini birlashtiradi"
                },
                "API Gateway mijoz va backend mikroservislar o'rtasida yagona kirish nuqtasi va cross-cutting concern-lar markazi hisoblanadi."
            ),
            CreateQuestion(
                "Event-Driven Architecture (EDA) da Event Sourcing va Command Sourcing o'rtasidagi farq nima?",
                "// Event Sourcing stores: UserRegisteredEvent, UserEmailUpdatedEvent, UserAddressAddedEvent",
                new List<string> {
                    "Event Sourcing ob'ektning joriy holatini emas, unga sodir bo'lgan barcha o'zgarmas hodisalar (Events) zanjirini saqlaydi va audit log hamda replay beradi",
                    "Event Sourcing faqat o'chirilgan ma ma'lumotlarni saqlaydi",
                    "Event Sourcing faqat SQL Server-da ishlaydi",
                    "Ikkalasi ham har sekundda bazani tozalaydi"
                },
                "Event Sourcing tizimda yuz bergan har bir voqeani (Event) xronologik tartibda o'zgarmas (append-only) jurnal sifatda saqlaydi."
            ),
            CreateQuestion(
                "Strangler Fig Pattern monolit tizimni mikroservislarga o'tkazishda (Migration) qanday ishlaydi?",
                "Client -> Reverse Proxy -> (Legacy Monolith OR New Microservice)",
                new List<string> {
                    "Monolitni bittada to me'xtatmasdan, uning funksiyalarini sekin-asta bosqichma-bosqich yangi mikroservislarga ko'chirish va proxy orqali yo me me'yirtirish",
                    "Monolit kodini avtomatik C#-dan Python-ga o'g'irish",
                    "Monolit bazasini darhol o'chirib tashlash",
                    "Faqat In-Memory kesh saqlash"
                },
                "Strangler Fig pattern monolit ilovani bosqichma-bosqich, risk-siz yangi mikroservislar bilan almashtirib borish imkonini beradi."
            )
        };
    }

    private static List<Question> GenerateArchitectureHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "Event Sourcing arxitekturasida ob'ektning joriy holatini (Current State) minglab Event-lardan qayta tiklash (Replay) xarajatini kamaytirish uchun nima ishlatiladi?",
                "public class OrderSnapshot { public int Version; public string Data; }",
                new List<string> {
                    "Snapshotting — Vaqti-vaqti bilan olingan holat suratlari; Oxirgi snapshot-dan boshlab keyingi yangi event-lar qayta o me me me'qiladi",
                    "Barcha event-larni o'chirib tashlash",
                    "Faqat Redis-da saqlash",
                    "Event-larni SQL-ga o'g'irmaslik"
                },
                "Snapshotting barcha minglab voqealarni boshidan qayta o'qish o'rniga oxirgi snapshot-dan boshlab qayta tiklash imkonini beradi."
            ),
            CreateQuestion(
                "Taqsimlangan tizimlarda Distributed Locking va Fencing Tokens (masalan Redlock) yordamida Split-Brain va Race Condition qanday oldi olinadi?",
                "var lock = await _redlock.LockAsync(\"resource\", TimeSpan.FromSeconds(10)); // Returns Fencing Token: 101",
                new List<string> {
                    "Fencing Token — o me me me'sib boruvchi taymer/monoton raqam bo'lib, eski va kechikkan tarmoq so'rovlarini saqlash qurilmasi (storage) tomonidan rad etilishini ta me me'minlaydi",
                    "Faqat local Monitor lock ishlatish",
                    "Faqat baza taymerini kutish",
                    "Faqat RAM-ni tozalash"
                },
                "Fencing Token-lar kutilmagan pausing (GC pause, network lag) oqibatida eskirgan lock egasi saqlash tizimiga noto'g me me me'ri yozishining oldini oladi."
            ),
            CreateQuestion(
                "Multi-Region Active-Active Database Replication va Conflict-Free Replicated Data Types (CRDTs) qanday ishlaydi?",
                "// CRDT counter automatically converges across Region A and Region B without central locks!",
                new List<string> {
                    "Bir vaqtning o'zida bir nechta geografik regionlarda yozish imkonini beradi va matematik ravishda toqnashuvsiz (CRDT) ma me me'lumotlarni birlashtiradi",
                    "Faqat bitta regionda yozishga ruxsat beradi",
                    "Faqat fayllarni shifrlaydi",
                    "Regionlar o'rtasidagi tarmoqni to me me me'xtatadi"
                },
                "CRDT-lar va Active-Active replication bir nechta regionlarda toqnashuvsiz ma'lumotlarni konvergent moslashtirish imkonini beradi."
            ),
            CreateQuestion(
                "Bulkhead Pattern va Thread Pool Isolation mikroservislarda kaskadli nosozliklarni (Cascading Failures) qanday tosadigan mexanizm?",
                "// Isolated Thread Pools for Order Service vs Payment Service",
                new List<string> {
                    "Kema to'siqlari kabi, har bir tashqi resurs uchun alohida thread pool va resurs kvotasi ajratib, bittasidagi muammo butun tizimni to me me'xtatib qo'yishini oldini oladi",
                    "Faqat RAM hajmini oshiradi",
                    "Faqat fayl tizimini shifrlaydi",
                    "Barcha so'rovlarni bitta queue-ga yig me me me'adi"
                },
                "Bulkhead Pattern bir resursdagi sekinlik yoki xatolik boshqa servislarga ajratilgan thread pool-larni to me me'ldirib yubormasligi uchun izolyatsiya beradi."
            ),
            CreateQuestion(
                "Rate Limiting va Throttling tizimlarida Sliding Window Log va Leaky Bucket algoritmlari orasidagi farq nimada?",
                "// Leaky bucket processes requests at a STRICT CONSTANT RATE (smooth outflow)",
                new List<string> {
                    "Leaky Bucket so'rovlarni qanchalik kutilmagan kelishidan qat'i nazar o'zgarmas doimiy tezlikda (constant rate) chiqaradi; Sliding Window Log aniq vaqt taymerlariga tayanadi",
                    "Leaky Bucket so'rovlarni o'chirib yuboradi",
                    "Sliding Window faqat NoSQL-da ishlaydi",
                    "Ikkala algoritm bir xil ishlaydi"
                },
                "Leaky Bucket so me me'rovlar oqimini tekislaydi (traffic shaping). Sliding Window Log esa aniq vaqt oralig'idagi so'rovlar logini yuritadi."
            ),
            CreateQuestion(
                "Domain Events va Integration Events o'rtasidagi asosiy farq va transactional boundary nimada?",
                "// DomainEvent: synchronously handled within same DB transaction\n// IntegrationEvent: published via RabbitMQ to external microservices after DB commit",
                new List<string> {
                    "Domain Event — bitta Bounded Context va tranzaksiya ichida sinxron; Integration Event — bitta context-dan boshqa mikroservislarga asinxron tarqatiladigan voqea",
                    "Integration Event faqat In-Memory ishlaydi",
                    "Domain Event faqat RabbitMQ-da bo'ladi",
                    "Ikkalasi ham bir xil event turi"
                },
                "Domain Events bitta kontekst ichidagi mantiqni sinxron bog'laydi. Integration Events esa boshqa servislar uchun broker orqali asinxron uzatiladi."
            ),
            CreateQuestion(
                "Zero-Downtime Deployment strategiyalaridan Blue-Green va Canary Deployment o'rtasidagi farq nimada?",
                "// Canary: 5% users to v2, 95% users to v1 -> monitor metrics -> rollout 100%",
                new List<string> {
                    "Blue-Green — 2 ta parallel muhit (biri faol, biri yangi) o'rtasida 100% trafikni bir zumda o me me me'tkazish; Canary — yangi versiyaga dastlab kichik foiz (5%) trafikni sekin uzatish",
                    "Canary faqat database migration uchun",
                    "Blue-Green serverni o me me me'chirishni talab qiladi",
                    "Ikkala strategiya ham foydalanuvchilarga 500 error beradi"
                },
                "Canary deployment risk-ni kamaytirish uchun foydalanuvchilarning kichik ulushida yangi versiyani sinaydi. Blue-Green esa 2 ta baravar parallel muhitni almashtiradi."
            ),
            CreateQuestion(
                "High-Throughput Distributed Cache System-da Cache Stampede (Thundering Herd Problem) va probabilistic early expiration (XFetch) qanday ishlaydi?",
                "// Probabilistic early expiration refreshes cache BEFORE it actually expires when demand is high",
                new List<string> {
                    "Kesh muddati tugaganda minglab so'rovlar bir vaqtda bazaga urilishini (Stampede) oldini olish uchun so'rovlarni lock qilish yoki muddat tugamasdan ehtimollik bilan fon rejimida yangilash",
                    "Keshni har soniyada tozalab turish",
                    "Baza ulanishini yopib qo me me'yish",
                    "Faqat static fayllar saqlash"
                },
                "Cache Stampede muddat tugaganda bazaga oqim urilishidir. Lock va Probabilistic early expiration (XFetch) orqali buni oldi olinadi."
            ),
            CreateQuestion(
                "Data Mesh Architecture vs Data Lakehouse (Big Data Architecture) konseptual farqi nimada?",
                "// Data Mesh: Domain-oriented decentralized data ownership as a product",
                new List<string> {
                    "Data Mesh — ma'lumotlarga domenlar bo'yicha markazlashtirilmagan mahsulot (Data as a Product) sifatida yondashadi; Lakehouse esa bitta markaziy platforma beradi",
                    "Data Mesh faqat bitta SQL database saqlaydi",
                    "Data Lakehouse faqat fayllarni o'chiradi",
                    "Ikkala arxitektura ham bir xil"
                },
                "Data Mesh markazlashgan data team o'rniga har bir domen jamoasiga o'z ma'lumotlarini Data Product sifatida egalik qilishni beradi."
            ),
            CreateQuestion(
                "Database Sharding (Horizontal Partitioning) va Distributed Hash Ring (Consistent Hashing) qanday ishlaydi?",
                "hash(key) % 16384 -> Assigned to specific Shard Node",
                new List<string> {
                    "Jadval qatorlarini kalit hash-iga ko'ra har xil fizik ma'lumotlar bazalari (shards) o'rtasida bo me me me'ladi; Consistent Hashing server qo me'shilganda minimal reshuffle beradi",
                    "Sharding faqat bitta kompyuterda ishlaydi",
                    "Consistent Hashing barcha ma'lumotni o me me me'chiradi",
                    "Sharding SQL so'rovlarini taqiqlaydi"
                },
                "Sharding ma'lumotlarni tugunlar bo'ylab taqsimlaydi. Consistent Hashing esa yangi shard qo'shilganda ma'lumotlarni qayta taqsimlash xarajatini minimal qiladi."
            )
        };
    }
}
