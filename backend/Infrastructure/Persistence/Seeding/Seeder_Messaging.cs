using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetMessagingQuizzes()
    {
        return new List<Quiz>
        {
            CreateQuiz(
                "RabbitMQ & Asynchronous Messaging Fundamentals",
                "messaging",
                "Message Brokers",
                "Message Broker konseptlari, RabbitMQ Exchanges, Queues va Basic ACK/NACK bo'yicha 30 ta oson darajadagi test.",
                "Easy",
                "mail",
                GenerateMessagingEasyQuestions()
            ),
            CreateQuiz(
                "MassTransit & RabbitMQ Advanced Integration",
                "messaging",
                "Message Brokers",
                "MassTransit Consumers, Outbox Pattern, Dead Letter Queues va Retry Policies bo'yicha 30 ta o'rtacha darajadagi test.",
                "Medium",
                "send",
                GenerateMessagingMediumQuestions()
            ),
            CreateQuiz(
                "High-Throughput Messaging & Saga State Machines",
                "messaging",
                "Message Brokers",
                "MassTransit Saga Automaton, Quorum Queues, Publisher Confirms va Circuit Breakers bo'yicha 30 ta qiyin darajadagi test.",
                "Hard",
                "cpu",
                GenerateMessagingHardQuestions()
            )
        };
    }

    private static List<Question> GenerateMessagingEasyQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("RabbitMQ-da Exchange va Queue (Navbat) va Binding (Bog'liqlik) komponentlarining vazifasi va munosabati nimada?",
                new List<string> {
                    "Publisher xabarni Exchange-ga yuboradi; Exchange Routing Key va Binding orqali xabarni kerakli Queue (navbat) larga tarqatadi; Consumer esa Queue-dan o'qiydi",
                    "Publisher xabarni to'g'ridan-to'g'ri Consumer-ga uzatadi va Queue shart emas",
                    "Exchange xabarlarni diskda doimiy saqlaydi, Queue esa saqlamaydi",
                    "Queue faqat bitta xabar saqlay oladi"
                },
                "RabbitMQ-da xabar avval Exchange-ga keladi, u Routing Key va Binding qoidalariga muvofiq tegishli Queue-larga yuboriladi."),

            CreateQuestion("RabbitMQ Exchange turlaridan Direct, Fanout, Topic va Headers o'rtasidagi farqlar nimalardan iborat?",
                new List<string> {
                    "Direct — exact routing key mos bo'lsa; Fanout — barcha ulangan navbatlarga broadcast; Topic — pattern (*, #) bo'yicha; Headers — header qiymatlari bo'yicha yo'naltiradi",
                    "Fanout faqat bitta navbatga yuboradi",
                    "Direct barcha navbatlarga broadcast qiladi",
                    "Topic faqat sonli routing key-lar bilan ishlaydi"
                },
                "Fanout barcha ulangan navbatlarga nusxalaydi. Direct aniq moslikni tekshiradi. Topic esa wildcards (*, #) pattern-larini qo'llaydi."),

            CreateQuestion("RabbitMQ-da xabarni qabul qiluvchi (Consumer) tomonidan yuboriladigan BasicACK, BasicNACK va BasicReject signallari nimani anglatadi?",
                new List<string> {
                    "ACK — xabar muvaffaqiyatli ishlandi (o'chirilsin); NACK/Reject — xabar ishlanmadi (requeue qilish yoki Dead Letter Queue-ga yuborish)",
                    "ACK xabarni qayta ishlanmadi deb o'chiradi",
                    "NACK har doim xabarni o'chirib yuboradi",
                    "BasicReject faqat Publisher tomonidan yuboriladi"
                },
                "ACK brokerga xabar muvaffaqiyatli bajarilganini bildiradi. NACK/Reject esa xatolik bo'lganini va requeue bo'lishi yoki DLQ-ga o'tishini bildiradi."),

            CreateQuestion("RabbitMQ-da Durable Queue va Persistent Message sozlamalari nimani kafolatlaydi?",
                new List<string> {
                    "RabbitMQ serveri kutilmagan holda o'chib-yonib qayta tushsa ham navbat va xabarlarning diskda saqlanib qolishini ta'minlaydi",
                    "Xabarlar 1 soniyada avtomatik o'chirilishini",
                    "Xabarlar faqat RAM-da saqlanishini",
                    "Publisher so'rovi bloklanib qolishini"
                },
                "Durable navbat va Persistent xabarlar server qayta tushganda (restart/crash) xabarlar yo'qolmasligi uchun ularni diskka yozadi."),

            CreateQuestion("MassTransit karkasida Publish va Send operatsiyalari o'rtasidagi asosiy konseptual farq nima?",
                new List<string> {
                    "Publish — Event (hodisa) tarqatadi va barcha mos Consumer-lar tinglaydi; Send — Command (buyruq) yuboradi va aynan bitta aniq navbat manziliga yo'naltiradi",
                    "Send barcha Consumer-larga tarqatadi",
                    "Publish faqat SQL bazaga yozadi",
                    "Ikkala operatsiya bir xil vazifani bajaradi"
                },
                "Publish — Publish/Subscribe (Event) namunasi. Send — Point-to-Point (Command) namunasi hisoblanadi."),

            CreateQuestion("MassTransit-da IConsumer<T> interfeysi va Consume(ConsumeContext<T> context) metodi qanday ishlaydi?",
                new List<string> {
                    "Turdagi xabar kelganda MassTransit avtomatik ushbu consumer-ni yaratadi, context orqali xabarni o'qiydi va asinxron qayta ishlaydi",
                    "Faqat 1 marta ilova tushganda ishlaydi",
                    "Faqat HTTP controller-da chaqiriladi",
                    "Consumer-ni qo'lda instansiya qilish shart"
                },
                "MassTransit IConsumer<T> orqali xabar kelishi bilan uni asinxron Consume metodiga uzatadi va scoped DI container taqdim etadi."),

            CreateQuestion("RabbitMQ-da Competing Consumers (Raqobatchi iste'molchilar) patterni qanday ishlaydi va yuklama qanday bo'linadi?",
                new List<string> {
                    "Bitta navbatga bir nechta Consumer ulansa, RabbitMQ xabarlarni unumli taqsimlash uchun Round-Robin yoki Prefetch Count bo'yicha ketma-ket birma-bir bo'ladi",
                    "Barcha Consumer-lar bir xil xabarni nusxalab oladi",
                    "Faqat birinchi Consumer barcha xabarni oladi",
                    "Queue bir vaqtda faqat 1 ta Consumer-ga ruxsat beradi"
                },
                "Competing Consumers bitta navbatdagi xabarlarni parallel qayta ishlash uchun ishlatiladi; Har bir xabar faqat 1 ta Consumer-ga beriladi."),

            CreateQuestion("RabbitMQ-da BasicQos va Prefetch Count sozlamasining vazifasi nimadan iborat?",
                new List<string> {
                    "Consumer-ga bir vaqtning o'zida ACK olinmagan maksimal qancha xabar berilishini cheklaydi, bu esa sekin ishlovchi Consumer-lar to'lib qolishini oldini oladi",
                    "Queue xabarlar hajmini cheklaydi",
                    "Publisher tezligini oshiradi",
                    "Faqat RAM hajmini tozalaydi"
                },
                "Prefetch Count Consumer ACK qaytarguncha unga beriladigan tasdiqlanmagan xabarlar sonini cheklab fair dispatch beradi."),

            CreateQuestion("MassTransit-da Message Retry Policy (UseMessageRetry) qanday ishlaydi?",
                new List<string> {
                    "Consumer-da exception berilganda xabarni darhol xatoga chiqarmasdan, belgilangan interval va takrorlashlar soni bo'yicha qayta harakat qiladi",
                    "Xabarni darhol o'chirib tashlaydi",
                    "RabbitMQ-ni qayta tushiradi",
                    "Faqat 1 marta bajaradi"
                },
                "UseMessageRetry vaqtinchalik tarmoq va DB ulanish xatolarida xabarni Consumer ichida qayta urinib ko'rish imkonini beradi."),

            CreateQuestion("RabbitMQ-da Virtual Hosts (vhosts) nima uchun ishlatiladi?",
                new List<string> {
                    "Bitta RabbitMQ serveri ichida turli loyihalar yoki muhitlar (Dev, Staging, Prod) uchun ajratilgan izolyatsiyalangan mantiqiy maydonlar yaratish",
                    "Faqat fayllarni keshlaydi",
                    "Faqat IP manzillarini shifrlaydi",
                    "Faqat In-Memory bazalar uchun"
                },
                "vhosts bitta RabbitMQ cluster ichida xavfsizlik, permissions va queues/exchanges-ni mantiqiy ajratish uchun xizmat qiladi."),

            CreateQuestion("Event-Driven Architecture-da Asinxron Xabarlar va Sinxron HTTP REST API o'rtasidagi asosiy me'moriy farq nima?",
                new List<string> {
                    "Asinxron xabarlar tarmoq sekinlashuvi (latency) va servislarning vaqtinchalik to'xtashiga (decoupling) chidamli; HTTP sinxron muloqot esa darhol javob kutadi (blocking)",
                    "HTTP REST API har doim tezroq ishlaydi",
                    "Asinxron xabarlar xotirani ko'p yeydi",
                    "Ular o me'rtasida farq yo'q"
                },
                "Asinxron xabarlar servislar o'rtasida bo'sh bog'liqlik (loose coupling) va yuqori fault tolerance beradi."),

            CreateQuestion("RabbitMQ Xabar sarlavhalarida (Headers) CorrelationId va MessageId nimani anglatadi?",
                new List<string> {
                    "MessageId — bitta xabarning unikal kodi; CorrelationId — bir nechta bog'liq so'rov va javob xabarlarini bitta zanjirga biriktiruvchi ID",
                    "MessageId faqat Publisher IP manzilini saqlaydi",
                    "CorrelationId faqat SQL jadval nomini beradi",
                    "Ular bir xil parametrlar"
                },
                "MessageId har bir xabar uchun unikal identity beradi. CorrelationId esa asinxron zanjirdagi so'rov va javoblarni bog'laydi."),

            CreateQuestion("RabbitMQ Management Plugin (Web UI - port 15672) nima beradi?",
                new List<string> {
                    "Brauzer orqali Connections, Channels, Exchanges, Queues va message rate ko me'rsatkichlarini real-vaqtda vizual monitoring va boshqarish imkonini beradi",
                    "Faqat HTML keshini tozalaydi",
                    "Faqat SQL query bajaradi",
                    "Faqat C# kodini kompilyatsiya qiladi"
                },
                "RabbitMQ Management Web UI port 15672-da ishlaydi va navbatlar hajmi, xabarlar oqimi hamda ulanishlarni kuzatish uchun xizmat qiladi."),

            CreateQuestion("Message Broker tizimlarida Poison Message nimani anglatadi?",
                new List<string> {
                    "Consumer tomonidan qayta-qayta o me'qilsa ham har safar xatolik beruvchi va navbatni to'sib qo'yadigan zararli/buzuq xabar",
                    "Faqat o'chirilgan xabar",
                    "Faqat 100MB li katta xabar",
                    "Faqat paroli shifrlanmagan xabar"
                },
                "Poison Message — xatosi tuzalmaydigan va navbatni to me'sib qo me'yadigan xabar. U Retry limitdan so'ng Dead Letter Queue-ga o'tkaziladi."),

            CreateQuestion("ASP.NET Core Dependency Injection-da MassTransit AddMassTransit() qanday sozlanadi?",
                new List<string> {
                    "services.AddMassTransit(x => { x.AddConsumers(...); x.UsingRabbitMq((ctx, cfg) => ...); });",
                    "services.AddRabbitMqConsumer();",
                    "services.RegisterMassTransitBus();",
                    "MassTransit DI saqlashni qo'llamaydi"
                },
                "AddMassTransit() service collection-ga MassTransit bus, consumers, va RabbitMQ transport sozlamalarini Scoped DI bilan ro me'yxatga oladi."),

            CreateQuestion("RabbitMQ-da Producer (Publisher) mas me'uliyati nimadan iborat?",
                new List<string> {
                    "Xabarlarni to'g'ri strukturada yaratib Exchange-ga mos Routing Key bilan uzatish",
                    "Navbatdan xabarlarni o'qib bazaga yozish",
                    "Faqat keshni tozalash",
                    "Consumer-larni boshqarish"
                },
                "Producer xabarni Exchange-ga yuborish uchun mas'ul bo me'lib, u navbat ichki holatini bilishi shart emas."),

            CreateQuestion("Consumer-da ishlanmagan Exception yuz berganda default RabbitMQ behavior qanday bo'ladi?",
                new List<string> {
                    "Xabar NACK/Reject bo'ladi va requeue=true bo'lsa navbatga qaytib abadiy siklga (infinite loop) kirib qolishi mumkin",
                    "Xabar darhol o'chiriladi",
                    "RabbitMQ avtomatik to'xtaydi",
                    "Xabar avtomatik SQL-ga saqlanadi"
                },
                "Unhandled Exception bo me'lganda NACK requeue=true xabarni abadiy siklga solishi mumkin. Buni oldini olish uchun Retry va DLQ sozlanadi."),

            CreateQuestion("RabbitMQ Connection va Channel o'rtasidagi me me me'moriy farq nima?",
                new List<string> {
                    "Connection — bitta og'ir TCP ulanish; Channel — ushbu TCP ulanish ichidagi engil (lightweight) virtual ulanish kanali",
                    "Channel faqat diskka yozadi",
                    "Connection faqat 1 ta xabar yuboradi",
                    "Ular o'rtasida farq yo'q"
                },
                "Connection bitta TCP ulanish hisoblanadi. Undan ko me'plab virtual Channel-lar ochib foydalanish resurslarni tejaydi."),

            CreateQuestion("Message Broker orqali xabar uzatishda Serialization formatlaridan JSON va Protobuf o'rtasidagi farq nima?",
                new List<string> {
                    "JSON matnli, o me'qilishi oson lekin hajmi kattaroq; Protobuf ikkilik (binary), ixcham va o'ta tez ishlaydi",
                    "JSON faqat 1KB saqlaydi",
                    "Protobuf matnli format",
                    "Ular bir xil format"
                },
                "Protobuf va MessagePack binary serialization shaklida kamroq tarmoq va I/O sarflaydi. JSON esa tushunarli va universal."),

            CreateQuestion("AMQP 0-9-1 protokoli RabbitMQ-da qanday rol o'ynaydi?",
                new List<string> {
                    "RabbitMQ client-lari va broker o'rtasida muloqot va xabar uzatishni ta'minlaydigan o'zaro o'rnatilgan standart tarmoq protokoli",
                    "Faqat web brauzerlar uchun HTTP protokoli",
                    "Faqat SQL Server protokoli",
                    "Faqat fayllarni yuklash protokoli"
                },
                "AMQP 0-9-1 RabbitMQ-ning asosiy binary xabar uzatish tarmoq protokoli hisoblanadi."),

            CreateQuestion("RabbitMQ Queue Naming Conventions va Namespace izolyatsiyasi nima uchun kerak?",
                new List<string> {
                    "Har bir servis va muhit uchun navbat nomlarini standartlashtirish (masalan order-service.order-created) va toqnashuvlarni oldini olish uchun",
                    "Faqat C# kodini siqish uchun",
                    "Faqat fayllarni keshga yozish uchun",
                    "Navbat nomlari muhim emas"
                },
                "Navbat nomlarini to'g'ri konvensiya bilan nomlash va izolyatsiya qilish mikroservislar o'rtasida tartibni beradi."),

            CreateQuestion("RabbitMQ-da Auto-Delete Queue va Transient Queue sozlamalari qachon kerak bo'ladi?",
                new List<string> {
                    "So me'nggi Consumer uzilganda navbat avtomatik o'chirilishi va diskda ortiqcha xotira ushlamasligi kerak bo'lgan vaqtinchalik javob navbatlarida",
                    "Har doim production ma'lumotlarida",
                    "Faqat Durable navbatlarda",
                    "Auto-Delete navbatlar mavjud emas"
                },
                "Auto-Delete navbatlar consumer ishini tugatgach keraksiz resurslarni avtomatik tozalash uchun ishlatiladi."),

            CreateQuestion("MassTransit In-Memory Bus (UsingInMemory) qachon va qayerda ishlatiladi?",
                new List<string> {
                    "Tashqi RabbitMQ brokerini o me'rnatmasdan, Unit Test va lokal tezkor sinovlarda MassTransit consumer-larini tekshirish uchun",
                    "Production muhitida high throughput uchun",
                    "Faqat SQL bazaga yozish uchun",
                    "Faqat Angular frontend-da"
                },
                "UsingInMemory Unit va Integration testlarda RabbitMQ-siz tezkor in-memory xabar uzatishni sinash uchun juda qulay."),

            CreateQuestion("Message Messaging modellarida Broadcast va Unicast o'rtasidagi farq nima?",
                new List<string> {
                    "Broadcast (Fanout) — xabarni barcha ulangan tinglovchilarga tarqatadi; Unicast (Direct/Send) — xabarni faqat 1 ta aniq tinglovchiga uzatadi",
                    "Broadcast faqat 1 ta tinglovchiga yuboradi",
                    "Unicast barchaga yuboradi",
                    "Ular bir xil model"
                },
                "Broadcast barcha tinglovchilarga nusha beradi (Pub/Sub). Unicast esa 1:1 manzilga uzatadi."),

            CreateQuestion("Message Contract Design-da xabar interfeyslari (Contracts) immutable va sodda bo'lishi nima uchun kerak?",
                new List<string> {
                    "Xabar uzatish vaqtida serializatsiya xatolarini oldini olish hamda mikroservislar o'rtasida bo'sh bog'liqlikni saqlash uchun",
                    "Faqat keshni tozalash uchun",
                    "Faqat SQL bazani o me'chirish uchun",
                    "Contract design shart emas"
                },
                "Xabar shartnomalari (Contracts) minimal, faqat zarur DTO qiymatlari va o'zgarmas (read-only) bo'lishi lozim."),

            CreateQuestion("MassTransit Health Checks (addHealthChecks()) nima beradi?",
                new List<string> {
                    "RabbitMQ ulanishi va MassTransit Bus holatini kuzatib, Kubernetes Readiness/Liveness probe-lariga 200 OK yoki 503 Unhealthy beradi",
                    "Faqat RAM hajmini ko'rsatadi",
                    "Faqat foydalanuvchilar sonini beradi",
                    "Health checks ishlamaydi"
                },
                "MassTransit Health Check insfrastruktura va broker ulanishini avtomatik kuzatib K8s orchestrator-ga xabar beradi."),

            CreateQuestion("RabbitMQ-da Queue Backlog va Consumer Lag nimani anglatadi?",
                new List<string> {
                    "Navbatda yig me me me'lanib qolgan va hali Consumer-lar tomonidan o'qib ulgurilmagan ishlanmagan xabarlar soni va vaqti",
                    "Faqat server xotirasi hajmi",
                    "Faqat Publisher tezligi",
                    "Queue backlog har doim 0 bo'lishi shart"
                },
                "Consumer Lag navbatda ishlovini kutayotgan xabarlar to me'planib qolayotganini (bottleneck) ko'rsatuvchi muhim metrikadir."),

            CreateQuestion("Idempotent Consumer Pattern-ning asosiy ta'rifi nima?",
                new List<string> {
                    "Bir xil xabar bir necha marta qayta kelsa ham, tizim yakuniy holati va bazadagi o'zgarish faqat 1 marta bajarilgandek bo'lishini ta'minlash",
                    "Xabarni 100 marta bajarish",
                    "Xabarlarni o'chirib yuborish",
                    "Faqat single thread-da ishlash"
                },
                "Idempotency bir xil xabar takroran kelganda ham (At-Least-Once) tizimda nojo'ya takroriy o'zgarish hosil qilmaydi."),

            CreateQuestion("RabbitMQ-da Message Expiration (TTL) per Queue va per Message sozlanishi o'rtasidagi farq nima?",
                new List<string> {
                    "Per Queue — navbatga tushgan barcha xabarlarga bir xil TTL beradi; Per Message — har bir alohida xabarning header-ida individual TTL beradi",
                    "Per Message faqat diskda saqlaydi",
                    "Per Queue xabarlarni o'chirmaydi",
                    "Ular bir xil sozlama"
                },
                "Per Queue barcha kelgan xabarlarga umumiy muddat qo me me'yadi. Per Message esa har bir xabarga alohida TTL berish imkonini beradi."),

            CreateQuestion("RabbitMQ-da Message Properties (ContentType, DeliveryMode) nima beradi?",
                new List<string> {
                    "ContentType — xabar formatini (application/json); DeliveryMode — xabar persistent (diskda saqlanishi) yoki transient (RAM) ekanligini ko'rsatadi",
                    "DeliveryMode faqat SQL-da ishlaydi",
                    "ContentType faqat rasm fayllari uchun",
                    "Properties shart emas"
                },
                "Message Properties broker va consumer-ga xabarni qanday o'qish va saqlash bo'yicha ko'rsatma beradi.")
        };
    }

    private static List<Question> GenerateMessagingMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("RabbitMQ-da Dead Letter Exchange (DLX) va Dead Letter Queue (DLQ) mexanizmi qaysi holatlarda xabarni qabul qiladi?",
                new List<string> {
                    "Xabar NACK/Reject qilinib requeue=false bo'lganda, TTL muddati tugaganda yoki Queue uzunligi limitdan oshib ketganda (Oversized)",
                    "Xabar muvaffaqiyatli ACK bo'lganda",
                    "Faqat Publisher ulanganida",
                    "Faqat server o'chirilganda"
                },
                "DLX/DLQ ishlanmagan, muddati o'tgan yoki rad etilgan nosoz xabarlarni tahlil qilish uchun alohida navbatga yo'naltiradi."),

            CreateQuestion("MassTransit-da Entity Framework / PostgreSQL Transactional Outbox Pattern qanday ishlaydi?",
                new List<string> {
                    "Biznes ma'lumot saqlanishi va Xabar saqlanishi bitta SQL tranzaksiyasida Outbox table-ga yoziladi, keyin fondagi Outbox Service uni RabbitMQ-ga yuboradi",
                    "Xabarlarni darhol RabbitMQ-ga yuborib SQL tranzaksiyasini kutmaydi",
                    "Faqat Redis keshda saqlaydi",
                    "Faqat In-Memory ishlaydi"
                },
                "Transactional Outbox Dual-Write muammosini hal etib, SQL DB va Message Broker o'rtasidagi 100% eventual consistency-ni kafolatlaydi."),

            CreateQuestion("MassTransit-da Message Redelivery (UseDelayedRedelivery yoki Quartz/Hangfire) va Retry Policy o'rtasidagi farq nima?",
                new List<string> {
                    "Retry xabarni xotirada tezda qayta urintiradi; Redelivery esa uzoq vaqtli kechikishlar uchun xabarni RabbitMQ Delayed Exchange-ga qaytarib beradi",
                    "Retry xabarni abadiy saqlaydi",
                    "Redelivery xabarni darhol o'chiradi",
                    "Ikkalasi ham bir xil taymer ishlatadi"
                },
                "Redelivery xabarni navbatdan chiqarib belgilangan kelajakdagi vaqtga (masalan 15 minutdan keyin) qayta rejalashtirish uchun ishlatiladi."),

            CreateQuestion("RabbitMQ Publisher Confirms (Publisher Acknowledgement) nima beradi va u asinxron tarzda qanday ishlaydi?",
                new List<string> {
                    "Publisher yuborgan xabar RabbitMQ Exchange va Durable Queue-ga muvaffaqiyatli yetib diskka yozilganini tasdiqlab Publisher-ga ACK beradi",
                    "Consumer xabarni o'qiganini bildiradi",
                    "Faqat memory to'lganda ishlaydi",
                    "Publisher-ni bloklab so'rovni sekinlashtiradi"
                },
                "Publisher Confirms xabar yuborilayotganda tarmoq uzilishi yoki broker crash bo'lganda xabar yo'qolmaganini Publisher-ga tasdiqlaydi."),

            CreateQuestion("MassTransit-da Consumer Definition (ConsumerDefinition<T>) sinfidan foydalanishning afzalligi nimada?",
                new List<string> {
                    "Consumer uchun Max Concurrency, Retry policy, Endpoint nomlari va Rate limiting sozlamalarini alohida va toza konfiguratsiya qilish imkonini beradi",
                    "Faqat xabarlar matnini tarjima qiladi",
                    "Consumer kodi unumdorligini 10 marta oshiradi",
                    "Faqat SQL query-larni keshlaydi"
                },
                "ConsumerDefinition har bir Consumer-ning unumdorligi va concurrency cheklovlarini koddagi alohida izolyatsiyalangan sinfda beradi."),

            CreateQuestion("MassTransit Message Correlation (CorrelationId) va In-Reply-To sarlavhalari taqsimlangan tizimlarda qanday ishlaydi?",
                new List<string> {
                    "So'rov va javob (Request-Response pattern) va Saga jarayonlarida turli servislardan o'tayotgan xabarlarni bitta umumiy bitimga (Correlation) bog'laydi",
                    "Faqat IP manzilni saqlaydi",
                    "Faqat xabar hajmini cheklaydi",
                    "Faqat password-ni shifrlaydi"
                },
                "CorrelationId bir nechta asinxron xabar va javoblarni bitta mantiqiy tranzaksiya yoki Saga jarayoniga uzviy biriktiradi."),

            CreateQuestion("RabbitMQ-da Message TTL (Time-To-Live) va Queue Expiration sozlamalari nimani bajaradi?",
                new List<string> {
                    "Xabar ko'rsatilgan vaqt (TTL) ichida qayta ishlanmasa avtomatik eskiradi va o'chiriladi yoki DLQ-ga o me me me'tkaziladi",
                    "Queue-ni abadiy saqlab turadi",
                    "Publisher so'rovini to'xtatadi",
                    "Faqat RAM-ni tozalaydi"
                },
                "Message TTL vaqt oralig'ida iste'mol qilinmagan eskirgan xabarlarni tozalash yoki DLQ-ga yo'naltirish uchun xizmat qiladi."),

            CreateQuestion("MassTransit Request-Response Pattern (IRequestClient<TRequest>) asinxron va sinxron muloqotni qanday birlashtiradi?",
                new List<string> {
                    "Temporary Response Queue yaratadi, xabarni Send qiladi va javob xabari kelguncha C# async/await Task bilan kutib oladi",
                    "Faqat HTTP REST API ishlatadi",
                    "Barcha ma'lumotlarni bazaga yozadi",
                    "Thread Pool-ni bloklaydi"
                },
                "IRequestClient temporary reply queue yordamida Message Broker orqali asinxron Request-Response muloqotni oson await qilish imkonini beradi."),

            CreateQuestion("RabbitMQ High Availability (HA) va Classic Mirrored Queues o'rtasidagi asosiy muammo nima bo'lgan?",
                new List<string> {
                    "Mirrored Queues tarmoq uzilishi va sinxronizatsiya vaqtida to'liq bloklanish (blocking synchronization) va ma'lumot yo'qolish xavfini tug'dirgan",
                    "Mirrored Queues faqat Linux-da ishlamaydi",
                    "Mirrored Queues xabarlarni shifrlamaydi",
                    "Ikkala mexanizm ham muammosiz bo'lgan"
                },
                "Mirrored Queues eskirgan va tarmoq bo'linishida sinxronizatsiya to'xtashlariga olib kelgan, o'rniga Quorum Queues (Raft) keldi."),

            CreateQuestion("RabbitMQ Exclusive Queue va Auto-Delete Queue sozlamalari qaysi hollarda qo'llaniladi?",
                new List<string> {
                    "Exclusive — faqat joriy ulanishga ko'rinadi va ulanish yopilganda o'chadi; Auto-Delete — so'nggi Consumer uzilganda avtomatik o'chiriladi",
                    "Exclusive — barcha foydalanuvchilarga ochiq",
                    "Auto-Delete — xabarlarni abadiy saqlaydi",
                    "Ikkala sozlama ham faqat Durable navbatlarda ishlaydi"
                },
                "Exclusive va Auto-Delete vaqtinchalik (temporary) va reply navbatlarini tarmoq muloqoti tugashi bilan avtomatik tozalash uchun ishlatiladi."),

            CreateQuestion("RabbitMQ Priority Queues (x-max-priority) qanday ishlaydi va uning cheklovi nima?",
                new List<string> {
                    "Xabarlarga ustuvorlik (Priority 1..255) beradi va yuqori ustuvorlikdagi xabar oldin ishlanadi; Xotira va CPU resurs sarfini oshiradi",
                    "Priority Queue xabarlarni saralamaydi",
                    "Faqat 1 ta xabar saqlay oladi",
                    "Priority queues taqiqlangan"
                },
                "Priority Queues muhim xabarlarni oddiy xabarlardan oldin bajarilishini ta'minlaydi, biroq qo me'shimcha RAM va CPU talab etadi."),

            CreateQuestion("MassTransit Fault Consumers (IConsumer<Fault<TMessage>>) nima beradi?",
                new List<string> {
                    "Xabar barcha retry-lardan keyin ham barbod bo'lsa, xatolik ma'lumotini va uning sababini ushlab alohida loglash yoki xabar yuborish uchun",
                    "Faqat so me'rovni muvaffaqiyatli bajaradi",
                    "Faqat SQL query yaratadi",
                    "Fault Consumer ishlamaydi"
                },
                "IConsumer<Fault<T>> ishlanmagan va barbod bo'lgan xabarlar uchun global exception handling va xabardor qilish mexanizmini beradi."),

            CreateQuestion("RabbitMQ Alternate Exchange (x-alternate-exchange) parametri qachon kerak bo'ladi?",
                new List<string> {
                    "Exchange kelgan xabar uchun hech qanday mos Queue (Unroutable message) topa olmaganda, xabarni o'chirmasdan zaxira Exchange-ga yo'naltirish uchun",
                    "Faqat navbat to'lganda",
                    "Faqat Publisher uzilganda",
                    "Alternate Exchange navbatni o me'chiradi"
                },
                "Alternate Exchange unroutable (hech bir navbatga mos kelmagan) xabarlarni yo me'qolmasligi uchun zaxira Exchange-ga yo me'naltiradi."),

            CreateQuestion("MassTransit Scheduled Messages (SchedulePublish / ScheduleSend) Delayed Exchange Plugin bilan qanday ishlaydi?",
                new List<string> {
                    "Xabarni darhol uzatmay, RabbitMQ x-delayed-message exchange-da saqlab, belgilangan vaqt (masalan 1 soatdan keyin) yetganda navbatga chiqaradi",
                    "Xabarni darhol o'chiradi",
                    "Faqat LocalStorage-da saqlaydi",
                    "Faqat Sinxron ishlaydi"
                },
                "Delayed Exchange plugin xabarlarni RabbitMQ darajasida kelajakdagi vaqtga rejalashtirish imkoniyatini ta'minlaydi."),

            CreateQuestion("RabbitMQ Cluster Architecture-da Master va Replica Node-lar metadata va xabarlarni qanday bo'lishadi?",
                new List<string> {
                    "Queue metadata barcha node-larda nusxalanadi; Navbat xabarlarining o me'zi esa faqat ushbu navbat joylashgan Master Node-da saqlanadi",
                    "Barcha xabarlar 100% barcha node-larda nusxalanadi",
                    "Cluster faqat 1 ta node saqlay oladi",
                    "Node-lar o me'rtasida aloqa bo'lmaydi"
                },
                "RabbitMQ Classic Cluster-da metadata (exchanges, bindings) hamma joyda nusxalanadi, xabarlar esa muayyan master node navbatida saqlanadi."),

            CreateQuestion("MassTransit Middleware Pipeline (UseExecute, UseConsumeFilter) nima beradi?",
                new List<string> {
                    "Xabar Consumer-ga yetib bormasdan oldin va keyin kiruvchi quvurlarda (pipeline) custom validation, logging va Auth tekshiruvlarini bajarish",
                    "Faqat HTML keshaydi",
                    "Faqat SQL Server connection saqlaydi",
                    "Middleware pipeline-ni o'zgartirib bo'lmaydi"
                },
                "MassTransit Middleware custom filter-lar orqali cross-cutting concern (logging, auth, validation) mantiqlarini xabar bajarilishidan oldin ulash imkonini beradi."),

            CreateQuestion("RabbitMQ-da Message Deduplication strategies (Redis Cache vs DB MessageId table) qanday amalga oshiriladi?",
                new List<string> {
                    "Consumer xabarni ishlashdan oldin Redis yoki DB-da MessageId bormoq-yo'qligini atomik tekshirib, bor bo'lsa xabarni skip qiladi",
                    "Faqat navbatni o'chirish orqali",
                    "Faqat Publisher so'rovini to'xtatish orqali",
                    "Deduplication-ni bajarib bo me'lmaydi"
                },
                "Idempotent consumer Redis keshida yoki SQL jadvalida `MessageId` bormasligini tekshirib takroriy bajarilishni oldini oladi."),

            CreateQuestion("MassTransit Transport configuration (UsingRabbitMq) da Connection Retry Policy nimani beradi?",
                new List<string> {
                    "RabbitMQ serveri uzilib qolganda yoki qayta tushayotganda MassTransit avtomatik ulanishni qayta tiklashga (Re-connect) harakat qiladi",
                    "Ilovani darhol yopib qo'yadi",
                    "Faqat SQL bazaga ulanadi",
                    "Connection Retry-ni ilojisi yo'q"
                },
                "MassTransit resilient bus transporti bo'lib tarmoq yoki RabbitMQ uzilganda background-da ulanishni qayta tiklayveradi."),

            CreateQuestion("MassTransit-da Endpoint Conventions (ConfigureEndpoints) avtomatik navbat nomlashni qanday soddalashtiradi?",
                new List<string> {
                    "Kesh va Consumer sinf nomlariga qarab (Kebab-case konvensiyasi bo'yicha) RabbitMQ navbat va exchange-larini avtomatik nomlaydi va bog me'laydi",
                    "Navbat nomlarini tasodifiy raqam qiladi",
                    "Navbatlarni yaratishni taqiqlaydi",
                    "Faqat 1 ta navbat yaratadi"
                },
                "ConfigureEndpoints MassTransit-ga koddagi Consumer sinf nomlaridan kelib chiqib standart va toza navbat nomlarini avtomatik berishni topshiradi."),

            CreateQuestion("RabbitMQ Memory & Disk Alarms (vm_memory_high_watermark, disk_free_limit) qanday xavfsizlik funksiyasini bajaradi?",
                new List<string> {
                    "Server RAM yoki Disk bo me me'sh joyi tugay boshlaganda, server crash bo'lishining oldini olish uchun Publisher so me'rovlarini Flow Control bilan to'xtatadi",
                    "Barcha navbatlarni avtomatik o me me me'chiradi",
                    "Faqat foydalanuvchilar parolini o'zgartiradi",
                    "Alarms hech qachon ishlamaydi"
                },
                "RabbitMQ Resource Alarms server resurslari (RAM/Disk) xavfli darajaga etganda Publisher-larni bloklab xotira to'lib yiqilishidan himoyalaydi.")
        };
    }

    private static List<Question> GenerateMessagingHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("RabbitMQ Quorum Queues internals va Raft Consensus Algorithm konsensusi qanday ishlaydi?",
                new List<string> {
                    "Raft algoritmi orqali tugunlar (Leader va Followers) o'rtasida ko'pchilik (majority quorum) tasdig'i bo'lgachgina xabarni saqlab yuqori ishonchlilik beradi",
                    "Faqat In-Memory ishlaydi va diskka yozmaydi",
                    "Faqat single-node RabbitMQ-da ishlaydi",
                    "Quorum Queues xabarlarni o'chirib yuboradi"
                },
                "Quorum Queues RabbitMQ-da Raft konsensus algoritmi bilan taqsimlangan tugunlarda o'ta yuqori ma'lumotlar xavfsizligini beruvchi zamonaviy navbat turi."),

            CreateQuestion("MassTransit Automaton / Saga State Machine (MassTransitStateMachine<TSagaInstance>) da Optimistic Concurrency Control va State Persistence qanday bajariladi?",
                new List<string> {
                    "DbContext / Redis orqali har bir voqea kelganda Saga State va Concurrency Version mos ravishda yangilanadi; Toqnashuvda DbUpdateConcurrencyException bilan retry qiladi",
                    "Saga State faqat o'zgaruvchilarda va RAM-da saqlanadi",
                    "Saga State faqat RabbitMQ navbatida saqlanadi",
                    "State saqlanishi taqiqlanadi"
                },
                "MassTransit Saga Automaton taqsimlangan uzoq davom etuvchi biznes jarayonlar holatini (State) optimistik qulflash bilan ma'lumotlar bazasida saqlaydi."),

            CreateQuestion("RabbitMQ Streams (Plugin / Protocol) va Log-based Streaming (Apache Kafka-ga o'xshash) konsepti oddiy RabbitMQ Queues-dan nimasi bilan farq qiladi?",
                new List<string> {
                    "Streams — append-only o'zgarmas log bo'lib xabar o'qilganda o'chmaydi; Offset bo'yicha millionlab xabarlarni qayta-qayta o'qish (replay) va yuqori throughput beradi",
                    "Streams xabarlarni har 1 soniyada o'chirib beradi",
                    "Streams faqat HTTP GET bilan ishlaydi",
                    "Streams klassik queues-dan sekinroq ishlaydi"
                },
                "RabbitMQ Streams Kafka kabi o'zgarmas log saqlaydi; xabar o'qilganda yo'qolmaydi va offset orqali replay qilish imkonini beradi."),

            CreateQuestion("MassTransit Partitioner (UsePartitioner) va Message Ordering (Xabarlar ketma-ketligi) taqsimlangan muhitda qanday saqlanadi?",
                new List<string> {
                    "Bir xil CustomerId ga ega bo'lgan xabarlarni har doim aynan bitta worker thread/partition-ga yo'naltirib, parallel muhitda ham ketma-ketlikni (Order) kafolatlaydi",
                    "Barcha xabarlarni bitta thread-ga to'playdi va sekinlashtiradi",
                    "Xabarlar ketma-ketligini o'chiradi",
                    "Faqat single Consumer bo'lganda ishlaydi"
                },
                "UsePartitioner belgilangan Kalit (masalan CustomerId) bo'yicha xabarlarni aynan bitta bo'limga uzatib, parallel ravishda ketma-ketlikni saqlaydi."),

            CreateQuestion("MassTransit Circuit Breaker (UseCircuitBreaker) va Message Bus Fault Handling qanday ko'rinishda muvofiqlashtiriladi?",
                new List<string> {
                    "Consumer xatolar foizi limitdan oshsa endpoint-ni vaqtincha to'xtatadi (Open state), RabbitMQ so'rovlarini to'plab turadi va fonda qayta tiklaydi",
                    "Faqat RabbitMQ cluster-ni o'chirib tushiradi",
                    "Faqat SQL database-ni o'chiradi",
                    "Circuit breaker xatolarni e'tiborsiz qoldiradi"
                },
                "MassTransit Circuit Breaker Consumer doimiy xato berayotganda endpoint-ni vaqtincha muzlatib resurslarni saqlaydi."),

            CreateQuestion("RabbitMQ Flow Control (Memory Alarm & Disk Free Alarm) server resurslari to'lganda Publisher-larga qanday ta'sir o'tkazadi?",
                new List<string> {
                    "RAM yoki Disk bo'sh joyi belgilangan limitdan tushib ketganda, RabbitMQ Publisher TCP ulanishlarini bloklaydi (Flow Control) va xabar qabul qilishni to'xtatadi",
                    "Server avtomatik barcha navbatlarni o'chirib tashlaydi",
                    "Consumer-larni to'xtatadi",
                    "Flow control hech qachon ishlamaydi"
                },
                "RabbitMQ resurs alarms yetganda Publisher ulanishlarini tormozlaydi (block/pause), bu server crash bo'lishining oldini oladi."),

            CreateQuestion("MassTransit Batch Consumer (IConsumer<Batch<T>>) va High-Throughput Database Bulk Writes qanday birlashtiriladi?",
                new List<string> {
                    "Bir nechta xabarlarni (masalan 100 ta) bitta paketga yig'ib oladi va ma'lumotlar bazasiga bitta ommaviy Bulk Insert SQL so'rovi bilan yozadi",
                    "Xabarlarni bittalab sekin yozadi",
                    "Faqat In-Memory testlarda ishlaydi",
                    "Batch consumer faqat 1 ta xabar qabul qiladi"
                },
                "Batch Consumer ko'plab kelayotgan xabarlarni to'plab (pack) 1 ta Bulk SQL Insert bilan yozadi va I/O unumdorligini 10 marta oshiradi."),

            CreateQuestion("RabbitMQ Consistent Hash Exchange Plugin orqali xabarlarni bir nechta navbatlar o'rtasida Sharding qilish qanday bajariladi?",
                new List<string> {
                    "Routing key hash-iga ko'ra xabarlarni ulangan navbatlar o'rtasida teng va muvozanatli ravishda Sharded qilib bo'ladi",
                    "Faqat 1 ta navbatga hamma xabarni yuboradi",
                    "Xabarlarni diskka yozmaydi",
                    "Faqat Headers bilan ishlaydi"
                },
                "Consistent Hash Exchange xabarlarni ulangan navbatlar (queues) bo'ylab teng va taqsimlangan tarzda sharding qilish imkonini beradi."),

            CreateQuestion("MassTransit-da Outbox Message Delivery & CleanUp Service (Quartz / Hosted Service) jurnallar o'chirilishini qanday boshqaradi?",
                new List<string> {
                    "Yuborib bo'lingan eskirgan Outbox xabarlarini SQL bazadan vaqti-vaqti bilan o'chirib (Cleanup) jadval to'lib ketishining (table bloat) oldini oladi",
                    "Outbox jurnallarini abadiy saqlab turadi",
                    "Faqat RabbitMQ keshini tozalaydi",
                    "Outbox Cleanup jadvalni o'chirib tashlaydi"
                },
                "Outbox Cleanup maintenance background worker bajarilgan va o'tgan Outbox yozuvlarini SQL bazadan davriy tozalaydi."),

            CreateQuestion("Taqsimlangan xabarlar tizimida Exactly-Once Delivery imkoniyati va Idempotent Consumer Pattern (Idempotency Key) qanday amalga oshiriladi?",
                new List<string> {
                    "At-Least-Once kafolati sharoitida MessageId bo'yicha takroriy kelgan xabarlarni aniqlab, ularni qayta ishlamasden e'tiborsiz qoldirish (Idempotency)",
                    "RabbitMQ har doim 100% Exactly-once delivery kafolatlaydi",
                    "Idempotency xabarlarni o'chirib yuboradi",
                    "MessageId har doim har xil bo'ladi"
                },
                "Tarmoq muammolarida bir xil xabar qayta kelishi mumkin. Idempotent Consumer MessageId tekshiruvi orqali takroriy amallarni tosadigan yagona usuldir."),

            CreateQuestion("Taqsimlangan tranzaksiyalarda Two-Phase Commit (2PC) va Saga Orchestration pattern-larining farqi va tanlov mezoni nima?",
                new List<string> {
                    "2PC sinxron lock tutib darhol ACID izchillik beradi (lekin sekin va bloklanuvchan); Saga esa asinxron kompensatsiyaviy tranzaksiyalar orqali Eventual Consistency beradi (high-scale)",
                    "2PC har doim Saga-dan tezroq",
                    "Saga faqat 1 ta bazada ishlaydi",
                    "2PC hech qachon lock tutmaydi"
                },
                "Microservice-larda 2PC o'rniga asinxron va fault-tolerant bo'lgan Saga Pattern (Orchestration/Choreography) ishlatiladi."),

            CreateQuestion("RabbitMQ Publisher Confirms asinxron batch tasdiqlash (Batch Confirmation) qanday bajariladi?",
                new List<string> {
                    "Har bir xabarga bloklanib kutmasdan, ConfirmListener orqali deliveryTag bo'yicha asinxron ACK/NACK voqealarini tinglab yuqori throughput-da xabarlarni tasdiqlash",
                    "Faqat sinxron waitForConfirms() ishlatish",
                    "Publisher Confirms xabarlarni o'chirish uchun",
                    "Batch confirmation taqiqlangan"
                },
                "Asinxron ConfirmListener deliveryTag bo'yicha xabarlarni ketma-ket asinxron tasdiqlaydi va Publisher tezligini pasaytirmaydi."),

            CreateQuestion("RabbitMQ Stream Protocol va AMQP 0-9-1 protokoli unumdorligi o'rtasidagi asosiy farq nima?",
                new List<string> {
                    "Stream Protocol maxsus binary TCP uzatish va Zero-copy Read ishlatadi, bu AMQP 0-9-1 ga qaraganda 10 marta ko'proq (GB/s level) xabarlar oqimini beradi",
                    "AMQP 0-9-1 har doim Stream-dan tezroq",
                    "Stream Protocol faqat brauzerda ishlaydi",
                    "Ular bir xil protokol"
                },
                "RabbitMQ Stream Protocol Zero-copy va binary socket optimization orqali o'ta yuqori (Millions msg/sec) oqim beradi."),

            CreateQuestion("MassTransit Saga State Machine uchun Repository tanlashda Entity Framework Core va Redis Repository o'rtasidagi me'moriy farq nima?",
                new List<string> {
                    "EF Core relatsion SQL bazada tranzaksiyaviy izchillik va murakkab audit beradi; Redis Repository esa in-memory va o'ta tezkor sub-millisecond Saga state boshqaruvini beradi",
                    "Redis Repository Saga state-ni saqlay olmaydi",
                    "EF Core Saga state-ni o'chirib tashlaydi",
                    "Ular bir xil repository"
                },
                "EF Core Saga state-ni relational SQL bazada audit izchilligi bilan saqlaydi, Redis Repository esa in-memory high-throughput beradi."),

            CreateQuestion("High-Availability RabbitMQ Federation Plugin va Shovel Plugin o'rtasidagi farq nima?",
                new List<string> {
                    "Federation — turli alohida RabbitMQ cluster-lar o'rtasida navbat va exchange-larni avtomatik bog'laydi; Shovel — xabarlarni bitta manbadan boshqa manzilga uzluksiz ko me'chiradi (Move)",
                    "Shovel faqat LocalStorage-da ishlaydi",
                    "Federation faqat single-node-da ishlaydi",
                    "Ular bir xil plugin"
                },
                "Federation va Shovel geografik taqsimlangan WAN va Cross-Datacenter RabbitMQ cluster-larini bog'lash uchun xizmat qiladi."),

            CreateQuestion("MassTransit-da Custom Middleware Filter (`IFilter<ConsumeContext<T>>`) yozish va quvur zanjiriga ulashtirish qanday bajariladi?",
                new List<string> {
                    "public class CustomFilter<T> : IFilter<ConsumeContext<T>> { public async Task Send(ConsumeContext<T> ctx, IPipe<ConsumeContext<T>> next) { ... await next.Send(ctx); } }",
                    "Faqat SQL query-ni yozish orqali",
                    "Faqat Controller-da chaqirish orqali",
                    "Custom filter-larni yozib bo me'lmaydi"
                },
                "MassTransit custom pipe filter-lar async `next.Send(context)` zanjiri bo'yicha middleware yaratish imkonini beradi."),

            CreateQuestion("Taqsimlangan xabarlar tizimida OpenTelemetry va W3C TraceContext headers (traceparent) orqali Distributed Tracing qanday o'tkaziladi?",
                new List<string> {
                    "Publisher xabar header-iga traceparent ID yozadi; MassTransit/RabbitMQ Consumer buni o'qib bir xil TraceId ostida Span-larni davom ettiradi",
                    "Distributed Tracing faqat single node-da ishlaydi",
                    "Headers orqali TraceId uzatib bo'lmaydi",
                    "Faqat SQL-da ishlaydi"
                },
                "W3C `traceparent` va `tracestate` header-lari AMQP xabar sarlavhasida uzatilib barcha mikroservislar bo'ylab TraceId-ni saqlaydi."),

            CreateQuestion("RabbitMQ Connection Churn Anti-Pattern nima va u server resurslariga qanday ta'sir qiladi?",
                new List<string> {
                    "Har bir xabar yuborish/o'qish uchun yangidan TCP Connection ochib va yopish; RabbitMQ serverida CPU va Socket-larni tugatib crash qiladi (Use Long-lived connections!)",
                    "Connection Churn so me'rovlarni tezlashtiradi",
                    "Connection Churn keshni tozalaydi",
                    "Connection Churn xavfsiz"
                },
                "RabbitMQ-da Connection va Channel-lar uzoq yashovchi (Long-lived) bo'lishi shart. Har bir so me'rovda yangi Connection ochish og'ir anti-pattern hisoblanadi."),

            CreateQuestion("MassTransit Job Consumer (IJobConsumer<T>) uzoq davom etuvchi og'ir vazifalar uchun nima beradi?",
                new List<string> {
                    "Sekin va og'ir vazifalarni (masalan PDF render, Video encoding) fon rejimida navbatga qo me me'yib, progress kuzatish, pause/resume va concurrency cheklovlarini beradi",
                    "Job Consumer so'rovni darhol bekor qiladi",
                    "Job Consumer faqat HTTP controller-da bo me'ladi",
                    "U faqat In-Memory ishlaydi"
                },
                "MassTransit Job Consumer uzoq davom etuvchi heavy background job-larni boshqarish, pause, resume va job tracking imkoniyatini taqdim etadi."),

            CreateQuestion("RabbitMQ Lazy Queues (x-queue-mode: lazy) qanday ishlaydi va oddiy navbatlardan farqi nima?",
                new List<string> {
                    "Xabarlarni RAM-da emas, iloji boricha tezroq diskka yozib saqlaydi (Page out); RAM xotirasini tejaydi va millionlab xabarlar yig'ilganda server yiqilishini oldini oladi",
                    "Lazy Queues xabarlarni yo me'qotib yuboradi",
                    "Lazy Queues faqat RAM-da saqlaydi",
                    "Lazy Queues har 1 marta o'chadi"
                },
                "Lazy Queues xabarlarni zudlik bilan diskka yozadi va RAM sarfini minimal tutadi, bu esa kutilmagan millionlab navbat to'planishlarida serverni asraydi.")
        };
    }
}
