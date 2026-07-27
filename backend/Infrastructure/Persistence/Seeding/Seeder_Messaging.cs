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
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetMessagingEasyData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateMessagingMediumQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetMessagingMediumData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateMessagingHardQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetMessagingHardData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static (string text, string? code, List<string> options, string explanation) GetMessagingEasyData(int index) => index switch
    {
        1 => ("RabbitMQ-da xabarlarni qabul qilib oluvchi va ularni navbatlarga (Queues) yo'naltiruvchi komponent nima deyiladi?",
              null,
              new List<string> { "Exchange", "Queue", "Consumer", "Publisher" },
              "RabbitMQ-da xabar to'g'ridan-to'g'ri navbatga emas, avval Exchange-ga keladi va u kerakli navbatlarga yo'naltiradi."),
        2 => ("RabbitMQ-da 'Fanout Exchange' xabarni qanday yo'naltiradi?",
              null,
              new List<string> { "Xabarni o'ziga ulangan BARCHA navbatlarga (Queues) nusxalab yuboradi", "Faqat routing key mos keladigan navbatga yuboradi", "Faqat bitta birinchi navbatga yuboradi", "Xabarni o'chirib yuboradi" },
              "Fanout Exchange routing key-ga qaramay, barcha ulangan navbatlarga (queues) broadcast rejimidab xabarni tarqatadi."),
        3 => ("MassTransit karkasida (framework) xabarni qayta ishlovchi sinf qaysi interfeysni amalga oshirishi shart?",
              "public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>",
              new List<string> { "IConsumer<T>", "IMessageHandler<T>", "IListener<T>", "IReceiver<T>" },
              "MassTransit-da xabarlarni iste'mol qilish uchun `IConsumer<T>` interfeysi implement qilinadi."),
        4 => ("RabbitMQ-da Consumer xabarni muvaffaqiyatli qayta ishlaganini brokerga bildirish uchun qaysi signalni yuboradi?",
              "channel.BasicAck(deliveryTag, false);",
              new List<string> { "ACK (BasicAck)", "NACK (BasicNack)", "REJECT", "CANCEL" },
              "ACK (Acknowledgement) brokerga xabar muvaffaqiyatli bajarilganini va uni navbatdan o'chirish mumkinligini bildiradi."),
        5 => ("MassTransit-da xabarni barcha tinglovchilarga tarqatish uchun `publish` va bitta aniq manzilga yuborish uchun nima ishlatiladi?",
              "await _publishEndpoint.Publish<OrderCreated>(new { ... });",
              new List<string> { "Publish (Broadcast) va Send (Point-to-Point)", "Send va Post", "Emit va Dispatch", "Broadcast va Direct" },
              "Publish xabarni barcha tinglovchilarga, Send esa tayinli bitta navbat manziliga (endpoint) yuboradi."),
        _ => ($"Messaging Easy #{index}-savol: RabbitMQ-da #{index}-tushuncha qanday ishlaydi?",
              $"// Queue declaration #{index}\nchannel.QueueDeclare(\"queue-{index}\", durable: true, false, false, null);",
              new List<string> { "Durable navbat RabbitMQ qayta tushganda ham xabarlarni diskda saqlab qoladi", "Navbatni har minutda o'chiradi", "Faqat RAM-da saqlaydi", "Faqat 1 ta xabar qabul qiladi" },
              "Durable: true navbat va xabarlarning diskda saqlanishini ta'minlaydi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetMessagingMediumData(int index) => index switch
    {
        1 => ("RabbitMQ-da xabarni qayta ishlashda xatolik yuz berganda xabarlar yo'qolib ketmasligi uchun qaysi mexanizmdan foydalaniladi?",
              "// Dead Letter Exchange (DLX) Configuration",
              new List<string> { "Dead Letter Exchange (DLX) va Dead Letter Queue (DLQ)", "Auto-ACK yoqib qo'yish", "Xabarni darhol o'chirib tashlash", "Faqat RAM-ni tozalash" },
              "DLX xatolik bergan yoki qayta ishlash muddati o me'yori bo'yicha o'tgan xabarlarni maxsus DLQ navbatiga yo'naltiradi."),
        2 => ("MassTransit-da Transactional Outbox pattern ishlatilganda xabarlar dastlab qayerga yoziladi?",
              "services.AddMassTransit(x => {\n    x.AddEntityFrameworkOutbox<QuizDbContext>(o => o.UsePostgres());\n});",
              new List<string> { "Business tranzaksiya bilan birga SQL ma'lumotlar bazasidagi Outbox jadvaliga saqlanadi", "Darhol RabbitMQ-ga uzatiladi", "Faqat In-Memory keshga yoziladi", "Faqat faylga yoziladi" },
              "Transactional Outbox xabarlarni biznes tranzaksiya bilan birga SQL bazaga saqlaydi, bu atamarlikni ta me me me'minlaydi."),
        _ => ($"Messaging Medium #{index}-savol: MassTransit-da #{index}-sozlama qanday afzallik beradi?",
              $"// Retry policy #{index}\nx.UsingRabbitMq((context, cfg) => {{\n    cfg.UseMessageRetry(r => r.Interval(3, 1000));\n}});",
              new List<string> { "Xatolik bo'lganda 3 marta 1 soniyalik interval bilan qayta harakat qiladi", "Xabarni darhol o'chiradi", "RabbitMQ-ni qayta ishga tushiradi", "Faqat 1 marta ishlaydi" },
              "UseMessageRetry xabarlarni qayta ishlashdagi vaqtinchalik xatoliklarni qayta urinib ko'radi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetMessagingHardData(int index) => index switch
    {
        1 => ("MassTransit Saga State Machine-da taqsimlangan jarayonlarni boshqarishda (Orchestration) holat saqlanishi (State Persistence) qanday ta'minlanadi?",
              "public class OrderSaga : MassTransitStateMachine<OrderSagaState> { ... }",
              new List<string> { "DbContext / Redis orqali har bir voqea (event) kelganda Saga State jadvali va Concurrency Token mos ravishda yangilanadi", "Faqat o'zgaruvchilarda saqlanadi", "Faqat RabbitMQ navbatida saqlanadi", "State saqlanmaydi" },
              "MassTransit Saga Automaton jarayon holatini DbContext yoki Redis-da optimistik qulflash (Concurrency Token) bilan saqlaydi."),
        _ => ($"Messaging Hard #{index}-savol: RabbitMQ #{index}-yuqori unumdorlik mexanizmi bo'yicha qaysi ta'rif to'g'ri?",
              "// RabbitMQ Quorum Queues (Raft Consensus)",
              new List<string> { "Quorum Queues Raft konsensus algoritmi orqali tugunlar o'rtasida yuqori ma'lumotlar xavfsizligini ta'minlaydi", "Faqat In-Memory ishlaydi", "Faqat single node qo'llaydi", "Xabarlarni shifrlamaydi" },
              "Quorum Queues RabbitMQ-da taqsimlangan konsensus va yuqori ishonchlilik beruvchi zamonaviy navbat turi hisoblanadi.")
    };
}
