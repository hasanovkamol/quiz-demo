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
                "Message Broker konseptlari, RabbitMQ Exchanges, Queues va Basic ACK/NACK bo'yicha professional savollar.",
                "Easy",
                "mail",
                GenerateMessagingEasyQuestions()
            ),
            CreateQuiz(
                "MassTransit & RabbitMQ Advanced Integration",
                "messaging",
                "Message Brokers",
                "MassTransit Consumers, Outbox Pattern, Dead Letter Queues va Retry Policies bo'yicha senior savollar.",
                "Medium",
                "send",
                GenerateMessagingMediumQuestions()
            ),
            CreateQuiz(
                "High-Throughput Messaging & Saga State Machines",
                "messaging",
                "Message Brokers",
                "MassTransit Saga Automaton, Quorum Queues, Publisher Confirms va Circuit Breakers bo'yicha principal savollar.",
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
            CreateQuestion(
                "RabbitMQ-da `Exchange` va `Queue` (Navbat) va `Binding` (Bog'liqlik) komponentlarining vazifasi va munosabati nimada?",
                "Publisher -> Exchange --(Binding Key)--> Queue -> Consumer",
                new List<string> {
                    "Publisher xabarni Exchange-ga yuboradi; Exchange Routing Key va Binding orqali xabarni kerakli Queue (navbat) larga tarqatadi; Consumer esa Queue-dan o'qiydi",
                    "Publisher xabarni to'g'ridan-to'g'ri Consumer-ga uzatadi va Queue shart emas",
                    "Exchange xabarlarni diskda doimiy saqlaydi, Queue esa saqlamaydi",
                    "Queue faqat bitta xabar saqlay oladi"
                },
                "RabbitMQ-da xabar avval Exchange-ga keladi, u Routing Key va Binding qoidalariga muvofiq tegishli Queue-larga yuboriladi."
            ),
            CreateQuestion(
                "RabbitMQ Exchange turlaridan `Direct`, `Fanout`, `Topic` va `Headers` o'rtasidagi farqlar nimalardan iborat?",
                "channel.ExchangeDeclare(\"logs\", ExchangeType.Fanout);",
                new List<string> {
                    "Direct — exact routing key mos bo'lsa; Fanout — barcha ulangan navbatlarga broadcast; Topic — pattern (*, #) bo'yicha; Headers — header qiymatlari bo'yicha yo'naltiradi",
                    "Fanout faqat bitta navbatga yuboradi",
                    "Direct barcha navbatlarga broadcast qiladi",
                    "Topic faqat sonli routing key-lar bilan ishlaydi"
                },
                "Fanout barcha ulangan navbatlarga nusxalaydi. Direct aniq moslikni tekshiradi. Topic esa wildcards (*, #) pattern-larini qo'llaydi."
            ),
            CreateQuestion(
                "RabbitMQ-da xabarni qabul qiluvchi (Consumer) tomonidan yuboriladigan `BasicACK`, `BasicNACK` va `BasicReject` signallari nimani anglatadi?",
                "channel.BasicAck(deliveryTag, multiple: false);",
                new List<string> {
                    "ACK — xabar muvaffaqiyatli ishlandi (o'chirilsin); NACK/Reject — xabar ishlanmadi (requeue qilish yoki Dead Letter Queue-ga yuborish)",
                    "ACK xabarni qayta ishlanmadi deb o'chiradi",
                    "NACK har doim xabarni o'chirib yuboradi",
                    "BasicReject faqat Publisher tomonidan yuboriladi"
                },
                "ACK brokerga xabar muvaffaqiyatli bajarilganini bildiradi. NACK/Reject esa xatolik bo'lganini va requeue bo'lishi yoki DLQ-ga o'tishini bildiradi."
            ),
            CreateQuestion(
                "RabbitMQ-da `Durable Queue` va `Persistent Message` sozlamalari nimani kafolatlaydi?",
                "channel.QueueDeclare(\"orders\", durable: true, false, false, null);\nvar properties = channel.CreateBasicProperties(); properties.Persistent = true;",
                new List<string> {
                    "RabbitMQ serveri kutilmagan holda o'chib-yonib qayta tushsa ham navbat va xabarlarning diskda saqlanib qolishini ta'minlaydi",
                    "Xabarlar 1 soniyada avtomatik o'chirilishini",
                    "Xabarlar faqat RAM-da saqlanishini",
                    "Publisher so'rovi bloklanib qolishini"
                },
                "Durable navbat va Persistent xabarlar server qayta tushganda (restart/crash) xabarlar yo'qolmasligi uchun ularni diskka yozadi."
            ),
            CreateQuestion(
                "MassTransit karkasida `Publish` va `Send` operatsiyalari o'rtasidagi asosiy konseptual farq nima?",
                "await _publishEndpoint.Publish<OrderCreated>(new { ... }); // Event\nawait _sendEndpoint.Send<SubmitOrder>(new { ... }); // Command",
                new List<string> {
                    "Publish — Event (hodisa) tarqatadi va barcha mos Consumer-lar tinglaydi; Send — Command (buyruq) yuboradi va aynan bitta aniq navbat manziliga yo'naltiradi",
                    "Send barcha Consumer-larga tarqatadi",
                    "Publish faqat SQL bazaga yozadi",
                    "Ikkala operatsiya bir xil vazifani bajaradi"
                },
                "Publish — Publish/Subscribe (Event) namunasi. Send — Point-to-Point (Command) namunasi hisoblanadi."
            ),
            CreateQuestion(
                "MassTransit-da `IConsumer<T>` interfeysi va `Consume(ConsumeContext<T> context)` metodi qanday ishlaydi?",
                "public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent> {\n    public async Task Consume(ConsumeContext<OrderCreatedEvent> context) { ... }\n}",
                new List<string> {
                    "Turdagi xabar kelganda MassTransit avtomatik ushbu consumer-ni yaratadi, context orqali xabarni o'qiydi va asinxron qayta ishlaydi",
                    "Faqat 1 marta ilova tushganda ishlaydi",
                    "Faqat HTTP controller-da chaqiriladi",
                    "Consumer-ni qo'lda instansiya qilish shart"
                },
                "MassTransit `IConsumer<T>` orqali xabar kelishi bilan uni asinxron `Consume` metodiga uzatadi va scoped DI container taqdim etadi."
            ),
            CreateQuestion(
                "RabbitMQ-da Competing Consumers (Raqobatchi iste'molchilar) patterni qanday ishlaydi va yuklama qanday bo'linadi?",
                "// Queue 'orders' has 3 instances of OrderConsumer listening simultaneously",
                new List<string> {
                    "Bitta navbatga bir nechta Consumer ulansa, RabbitMQ xabarlarni unumli taqsimlash uchun Round-Robin yoki Prefetch Count bo'yicha ketma-ket birma-bir bo'ladi",
                    "Barcha Consumer-lar bir xil xabarni nusxalab oladi",
                    "Faqat birinchi Consumer barcha xabarni oladi",
                    "Queue bir vaqtda faqat 1 ta Consumer-ga ruxsat beradi"
                },
                "Competing Consumers bitta navbatdagi xabarlarni parallel qayta ishlash uchun ishlatiladi; Har bir xabar faqat 1 ta Consumer-ga beriladi."
            ),
            CreateQuestion(
                "RabbitMQ-da `BasicQos` va `Prefetch Count` sozlamasining vazifasi nimadan iborat?",
                "channel.BasicQos(prefetchSize: 0, prefetchCount: 10, global: false);",
                new List<string> {
                    "Consumer-ga bir vaqtning o'zida ACK olinmagan maksimal qancha xabar berilishini cheklaydi, bu esa sekin ishlovchi Consumer-lar to'lib qolishini oldini oladi",
                    "Queue xabarlar hajmini cheklaydi",
                    "Publisher tezligini oshiradi",
                    "Faqat RAM hajmini tozalaydi"
                },
                "Prefetch Count Consumer ACK qaytarguncha unga beriladigan tasdiqlanmagan xabarlar sonini cheklab fair dispatch beradi."
            ),
            CreateQuestion(
                "MassTransit-da Message Retry Policy (`UseMessageRetry`) qanday ishlaydi?",
                "cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(2)));",
                new List<string> {
                    "Consumer-da exception berilganda xabarni darhol xatoga chiqarmasdan, belgilangan interval va takrorlashlar soni bo'yicha qayta harakat qiladi",
                    "Xabarni darhol o'chirib tashlaydi",
                    "RabbitMQ-ni qayta tushiradi",
                    "Faqat 1 marta bajaradi"
                },
                "UseMessageRetry vaqtinchalik tarmoq va DB ulanish xatolarida xabarni Consumer ichida qayta urinib ko'rish imkonini beradi."
            ),
            CreateQuestion(
                "RabbitMQ-da Virtual Hosts (vhosts) nima uchun ishlatiladi?",
                "// Connection URI: amqp://user:pass@localhost:5672/vhost_dev",
                new List<string> {
                    "Bitta RabbitMQ serveri ichida turli loyihalar yoki muhitlar (Dev, Staging, Prod) uchun ajratilgan izolyatsiyalangan mantiqiy maydonlar yaratish",
                    "Faqat fayllarni keshlaydi",
                    "Faqat IP manzillarini shifrlaydi",
                    "Faqat In-Memory bazalar uchun"
                },
                "vhosts bitta RabbitMQ cluster ichida xavfsizlik, permissions va queues/exchanges-ni mantiqiy ajratish uchun xizmat qiladi."
            )
        };
    }

    private static List<Question> GenerateMessagingMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "RabbitMQ-da Dead Letter Exchange (DLX) va Dead Letter Queue (DLQ) mexanizmi qaysi holatlarda xabarni qabul qiladi?",
                "// Arguments: x-dead-letter-exchange: dlx.orders",
                new List<string> {
                    "Xabar NACK/Reject qilinib requeue=false bo'lganda, TTL muddati tugaganda yoki Queue uzunligi limitdan oshib ketganda (Oversized)",
                    "Xabar muvaffaqiyatli ACK bo'lganda",
                    "Faqat Publisher ulanganida",
                    "Faqat server o'chirilganda"
                },
                "DLX/DLQ ishlanmagan, muddati o'tgan yoki rad etilgan nosoz xabarlarni tahlil qilish uchun alohida navbatga yo'naltiradi."
            ),
            CreateQuestion(
                "MassTransit-da Entity Framework / PostgreSQL Transactional Outbox Pattern qanday ishlaydi?",
                "x.AddEntityFrameworkOutbox<QuizDbContext>(o => { o.UsePostgres(); o.UseBusOutbox(); });",
                new List<string> {
                    "Biznes ma'lumot saqlanishi va Xabar saqlanishi bitta SQL tranzaksiyasida Outbox table-ga yoziladi, keyin fondagi Outbox Service uni RabbitMQ-ga yuboradi",
                    "Xabarlarni darhol RabbitMQ-ga yuborib SQL tranzaksiyasini kutmaydi",
                    "Faqat Redis keshda saqlaydi",
                    "Faqat In-Memory ishlaydi"
                },
                "Transactional Outbox Dual-Write muammosini hal etib, SQL DB va Message Broker o'rtasidagi 100% eventual consistency-ni kafolatlaydi."
            ),
            CreateQuestion(
                "MassTransit-da Message Redelivery (`UseDelayedRedelivery` yoki Quartz/Hangfire) va Retry Policy o'rtasidagi farq nima?",
                "cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30)));",
                new List<string> {
                    "Retry xabarni xotirada tezda qayta urintiradi; Redelivery esa uzoq vaqtli kechikishlar uchun xabarni RabbitMQ Delayed Exchange-ga qaytarib beradi",
                    "Retry xabarni abadiy saqlaydi",
                    "Redelivery xabarni darhol o'chiradi",
                    "Ikkalasi ham bir xil taymer ishlatadi"
                },
                "Redelivery xabarni navbatdan chiqarib belgilangan kelajakdagi vaqtga (masalan 15 minutdan keyin) qayta rejalashtirish uchun ishlatiladi."
            ),
            CreateQuestion(
                "RabbitMQ Publisher Confirms (Publisher Acknowledgemnt) nima beradi va u asinxron tarzda qanday ishlaydi?",
                "channel.ConfirmSelect();\nchannel.BasicAcks += (sender, ea) => { /* Published successfully! */ };",
                new List<string> {
                    "Publisher yuborgan xabar RabbitMQ Exchange va Durable Queue-ga muvaffaqiyatli yetib diskka yozilganini tasdiqlab Publisher-ga ACK beradi",
                    "Consumer xabarni o'qiganini bildiradi",
                    "Faqat memory to'lganda ishlaydi",
                    "Publisher-ni bloklab so'rovni sekinlashtiradi"
                },
                "Publisher Confirms xabar yuborilayotganda tarmoq uzilishi yoki broker crash bo'lganda xabar yo'qolmaganini Publisher-ga tasdiqlaydi."
            ),
            CreateQuestion(
                "MassTransit-da Consumer Definition (`ConsumerDefinition<T>`) sinfidan foydalanishning afzalligi nimada?",
                "public class OrderConsumerDefinition : ConsumerDefinition<OrderConsumer> {\n    protected override void ConfigureConsumer(...) { Endpoint(e => e.ConcurrentMessageLimit = 8); }\n}",
                new List<string> {
                    "Consumer uchun Max Concurrency, Retry policy, Endpoint nomlari va Rate limiting sozlamalarini alohida va toza konfiguratsiya qilish imkonini beradi",
                    "Faqat xabarlar matnini tarjima qiladi",
                    "Consumer kodi unumdorligini 10 marta oshiradi",
                    "Faqat SQL query-larni keshlaydi"
                },
                "ConsumerDefinition har bir Consumer-ning unumdorligi va concurrency cheklovlarini koddagi alohida izolyatsiyalangan sinfda beradi."
            ),
            CreateQuestion(
                "MassTransit Message Correlation (`CorrelationId`) va In-Reply-To sarlavhalari taqsimlangan tizimlarda qanday ishlaydi?",
                "public interface OrderSubmitted { Guid CorrelationId { get; } }",
                new List<string> {
                    "So'rov va javob (Request-Response pattern) va Saga jarayonlarida turli servislardan o'tayotgan xabarlarni bitta umumiy bitimga (Correlation) bog'laydi",
                    "Faqat IP manzilni saqlaydi",
                    "Faqat xabar hajmini cheklaydi",
                    "Faqat password-ni shifrlaydi"
                },
                "CorrelationId bir nechta asinxron xabar va javoblarni bitta mantiqiy tranzaksiya yoki Saga jarayoniga uzviy biriktiradi."
            ),
            CreateQuestion(
                "RabbitMQ-da Message TTL (Time-To-Live) va Queue Expiration sozlamalari nimani bajaradi?",
                "arguments[\"x-message-ttl\"] = 60000; // 60 seconds",
                new List<string> {
                    "Xabar ko'rsatilgan vaqt (TTL) ichida qayta ishlanmasa avtomatik eskiradi va o'chiriladi yoki DLQ-ga o me me me'tkaziladi",
                    "Queue-ni abadiy saqlab turadi",
                    "Publisher so'rovini to'xtatadi",
                    "Faqat RAM-ni tozalaydi"
                },
                "Message TTL vaqt oralig'ida iste'mol qilinmagan eskirgan xabarlarni tozalash yoki DLQ-ga yo'naltirish uchun xizmat qiladi."
            ),
            CreateQuestion(
                "MassTransit Request-Response Pattern (`IRequestClient<TRequest>`) asinxron va sinxron muloqotni qanday birlashtiradi?",
                "var response = await _requestClient.GetResponse<CheckStatusResult>(new CheckStatus { OrderId = id });",
                new List<string> {
                    "Temporary Response Queue yaratadi, xabarni Send qiladi va javob xabari kelguncha C# async/await `Task` bilan kutib oladi",
                    "Faqat HTTP REST API ishlatadi",
                    "Barcha ma'lumotlarni bazaga yozadi",
                    "Thread Pool-ni bloklaydi"
                },
                "IRequestClient temporary reply queue yordamida Message Broker orqali asinxron Request-Response muloqotni oson `await` qilish imkonini beradi."
            ),
            CreateQuestion(
                "RabbitMQ High Availability (HA) va Classic Mirrored Queues o'rtasidagi asosiy muammo nima bo'lgan?",
                "// Deprecated Mirrored Queues had synchronization blocking issues under network partition",
                new List<string> {
                    "Mirrored Queues tarmoq uzilishi va sinxronizatsiya vaqtida to'liq bloklanish (blocking synchronization) va ma'lumot yo'qolish xavfini tug'dirgan",
                    "Mirrored Queues faqat Linux-da ishlamaydi",
                    "Mirrored Queues xabarlarni shifrlamaydi",
                    "Ikkala mexanizm ham muammosiz bo'lgan"
                },
                "Mirrored Queues eskirgan va tarmoq bo'linishida sinxronizatsiya to'xtashlariga olib kelgan, o'rniga Quorum Queues (Raft) keldi."
            ),
            CreateQuestion(
                "RabbitMQ Exclusive Queue va Auto-Delete Queue sozlamalari qaysi hollarda qo'llaniladi?",
                "channel.QueueDeclare(\"reply-queue\", durable: false, exclusive: true, autoDelete: true, null);",
                new List<string> {
                    "Exclusive — faqat joriy ulanishga ko'rinadi va ulanish yopilganda o'chadi; Auto-Delete — so'nggi Consumer uzilganda avtomatik o'chiriladi",
                    "Exclusive — barcha foydalanuvchilarga ochiq",
                    "Auto-Delete — xabarlarni abadiy saqlaydi",
                    "Ikkala sozlama ham faqat Durable navbatlarda ishlaydi"
                },
                "Exclusive va Auto-Delete vaqtinchalik (temporary) va reply navbatlarini tarmoq muloqoti tugashi bilan avtomatik tozalash uchun ishlatiladi."
            )
        };
    }

    private static List<Question> GenerateMessagingHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "RabbitMQ Quorum Queues internals va Raft Consensus Algorithm konsensusi qanday ishlaydi?",
                "channel.QueueDeclare(\"quorum-orders\", durable: true, false, false, new Dictionary<string, object> { {\"x-queue-type\", \"quorum\"} });",
                new List<string> {
                    "Raft algoritmi orqali tugunlar (Leader va Followers) o'rtasida ko'pchilik (majority quorum) tasdig'i bo'lgachgina xabarni saqlab yuqori ishonchlilik beradi",
                    "Faqat In-Memory ishlaydi va diskka yozmaydi",
                    "Faqat single-node RabbitMQ-da ishlaydi",
                    "Quorum Queues xabarlarni o'chirib yuboradi"
                },
                "Quorum Queues RabbitMQ-da Raft konsensus algoritmi bilan taqsimlangan tugunlarda o'ta yuqori ma'lumotlar xavfsizligini beruvchi zamonaviy navbat turi."
            ),
            CreateQuestion(
                "MassTransit Automaton / Saga State Machine (`MassTransitStateMachine<TSagaInstance>`) da Optimistic Concurrency Control va State Persistence qanday bajariladi?",
                "public class OrderSaga : MassTransitStateMachine<OrderSagaState> {\n    public OrderSaga() {\n        InstanceState(x => x.CurrentState);\n        Event(() => OrderSubmitted, x => x.CorrelateById(c => c.Message.OrderId));\n    }\n}",
                new List<string> {
                    "DbContext / Redis orqali har bir voqea kelganda Saga State va Concurrency Version mos ravishda yangilanadi; Toqnashuvda DbUpdateConcurrencyException bilan retry qiladi",
                    "Saga State faqat o'zgaruvchilarda va RAM-da saqlanadi",
                    "Saga State faqat RabbitMQ navbatida saqlanadi",
                    "State saqlanishi taqiqlanadi"
                },
                "MassTransit Saga Automaton taqsimlangan uzoq davom etuvchi biznes jarayonlar holatini (State) optimistik qulflash bilan ma'lumotlar bazasida saqlaydi."
            ),
            CreateQuestion(
                "RabbitMQ Streams (Plugin / Protocol) va Log-based Streaming (Apache Kafka-ga o'xshash) konsepti oddiy RabbitMQ Queues-dan nimasi bilan farq qiladi?",
                "// Append-only log with non-destructive reads and replay capability by offset",
                new List<string> {
                    "Streams — append-only o'zgarmas log bo'lib xabar o'qilganda o'chmaydi; Offset bo'yicha millionlab xabarlarni qayta-qayta o'qish (replay) va yuqori throughput beradi",
                    "Streams xabarlarni har 1 soniyada o'chirib beradi",
                    "Streams faqat HTTP GET bilan ishlaydi",
                    "Streams klassik queues-dan sekinroq ishlaydi"
                },
                "RabbitMQ Streams Kafka kabi o'zgarmas log saqlaydi; xabar o'qilganda yo'qolmaydi va offset orqali replay qilish imkonini beradi."
            ),
            CreateQuestion(
                "MassTransit Partitioner (`UsePartitioner`) va Message Ordering (Xabarlar ketma-ketligi) taqsimlangan muhitda qanday saqlanadi?",
                "cfg.ReceiveEndpoint(\"order-events\", e => {\n    e.UsePartitioner(8, context => context.Message.CustomerId);\n});",
                new List<string> {
                    "Bir xil CustomerId ga ega bo'lgan xabarlarni har doim aynan bitta worker thread/partition-ga yo me me'yirib, parallel muhitda ham ketma-ketlikni (Order) kafolatlaydi",
                    "Barcha xabarlarni bitta thread-ga to'playdi va sekinlashtiradi",
                    "Xabarlar ketma-ketligini o'chiradi",
                    "Faqat single Consumer bo'lganda ishlaydi"
                },
                "UsePartitioner belgilangan Kalit (masalan CustomerId) bo'yicha xabarlarni aynan bitta bo'limga uzatib, parallel ravishda ketma-ketlikni saqlaydi."
            ),
            CreateQuestion(
                "MassTransit Circuit Breaker (`UseCircuitBreaker`) va Message Bus Fault Handling qanday ko me me'rinishda muvofiqlashtiriladi?",
                "cb.TrackingPeriod = TimeSpan.FromMinutes(1);\ncb.TripThreshold = 15; // 15% error rate\ncb.ActiveThreshold = 10;\ncb.ResetInterval = TimeSpan.FromMinutes(5);",
                new List<string> {
                    "Consumer xatolar foizi limitdan oshsa endpoint-ni vaqtincha to'xtatadi (Open state), RabbitMQ so'rovlarini to'plab turadi va fonda qayta tiklaydi",
                    "Faqat RabbitMQ cluster-ni o'chirib tushiradi",
                    "Faqat SQL database-ni o'chiradi",
                    "Circuit breaker xatolarni e'tiborsiz qoldiradi"
                },
                "MassTransit Circuit Breaker Consumer doimiy xato berayotganda endpoint-ni vaqtincha muzlatib resurslarni saqlaydi."
            ),
            CreateQuestion(
                "RabbitMQ Flow Control (Memory Alarm & Disk Free Alarm) server resurslari to'lganda Publisher-larga qanday ta'sir o'tkazadi?",
                "// Memory high watermark reached (40% RAM) -> Block TCP Connections from Publishers!",
                new List<string> {
                    "RAM yoki Disk bo'sh joyi belgilangan limitdan tushib ketganda, RabbitMQ Publisher TCP ulanishlarini bloklaydi (Flow Control) va xabar qabul qilishni to'xtatadi",
                    "Server avtomatik barcha navbatlarni o me me me'chirib tashlaydi",
                    "Consumer-larni to'xtatadi",
                    "Flow control hech qachon ishlamaydi"
                },
                "RabbitMQ resurs alarms yetganda Publisher ulanishlarini tormozlaydi (block/pause), bu server crash bo'lishining oldini oladi."
            ),
            CreateQuestion(
                "MassTransit Batch Consumer (`IConsumer<Batch<T>>`) va High-Throughput Database Bulk Writes qanday birlashtiriladi?",
                "public class OrderBatchConsumer : IConsumer<Batch<OrderCreatedEvent>> {\n    public async Task Consume(ConsumeContext<Batch<OrderCreatedEvent>> context) {\n        // Bulk Insert into DB!\n    }\n}",
                new List<string> {
                    "Bir nechta xabarlarni (masalan 100 ta) bitta paketga yig'ib oladi va ma'lumotlar bazasiga bitta ommaviy Bulk Insert SQL so'rovi bilan yozadi",
                    "Xabarlarni bittalab sekin yozadi",
                    "Faqat In-Memory testlarda ishlaydi",
                    "Batch consumer faqat 1 ta xabar qabul qiladi"
                },
                "Batch Consumer ko'plab kelayotgan xabarlarni to'plab (pack) 1 ta Bulk SQL Insert bilan yozadi va I/O unumdorligini 10 marta oshiradi."
            ),
            CreateQuestion(
                "RabbitMQ Consistent Hash Exchange Plugin orqali xabarlarni bir nechta navbatlar o'rtasida Sharding qilish qanday bajariladi?",
                "channel.ExchangeDeclare(\"sharded-exchange\", \"x-consistent-hash\");",
                new List<string> {
                    "Routing key hash-iga ko'ra xabarlarni ulangan navbatlar o'rtasida teng va muvozanatli ravishda Sharded qilib bo'ladi",
                    "Faqat 1 ta navbatga hamma xabarni yuboradi",
                    "Xabarlarni diskka yozmaydi",
                    "Faqat Headers bilan ishlaydi"
                },
                "Consistent Hash Exchange xabarlarni ulangan navbatlar (queues) bo'ylab teng va taqsimlangan tarzda sharding qilish imkonini beradi."
            ),
            CreateQuestion(
                "MassTransit-da Outbox Message Delivery & CleanUp Service (Quartz / Hosted Service) jurnallar o'chirilishini qanday boshqaradi?",
                "services.AddOptions<MassTransitHostOptions>().Configure(options => { options.WaitUntilStarted = true; });",
                new List<string> {
                    "Yuborib bo me'lingan eskirgan Outbox xabarlarini SQL bazadan vaqti-vaqti bilan o'chirib (Cleanup) jadval to'lib ketishining (table bloat) oldini oladi",
                    "Outbox jurnallarini abadiy saqlab turadi",
                    "Faqat RabbitMQ keshini tozalaydi",
                    "Outbox Cleanup jadvalni o'chirib tashlaydi"
                },
                "Outbox Cleanup maintenance background worker bajarilgan va o'tgan Outbox yozuvlarini SQL bazadan davriy tozalaydi."
            ),
            CreateQuestion(
                "Taqsimlangan xabarlar tizimida Exactly-Once Delivery imkoniyati va Idempotent Consumer Pattern (Idempotency Key) qanday amalga oshiriladi?",
                "if (await _db.ProcessedMessages.AnyAsync(m => m.MessageId == context.MessageId)) return; // Skip duplicate!",
                new List<string> {
                    "At-Least-Once kafolati sharoitida MessageId bo'yicha takroriy kelgan xabarlarni aniqlab, ularni qayta ishlamasdan e'tiborsiz qoldirish (Idempotency)",
                    "RabbitMQ har doim 100% Exactly-once delivery kafolatlaydi",
                    "Idempotency xabarlarni o'chirib yuboradi",
                    "MessageId har doim har xil bo'ladi"
                },
                "Tarmoq muammolarida bir xil xabar qayta kelishi mumkin. Idempotent Consumer MessageId tekshiruvi orqali takroriy amallarni tosadigan yagona usuldir."
            )
        };
    }
}
