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
                "SOLID prinsiplari, Clean Code, Layered Architecture va Design Patterns asoslari bo'yicha 30 ta oson darajadagi test.",
                "Easy",
                "layers",
                GenerateArchitectureEasyQuestions()
            ),
            CreateQuiz(
                "Clean Architecture, DDD & Microservices Design",
                "architecture",
                "Software Architecture",
                "Clean Architecture, Domain-Driven Design (DDD), CQRS, Outbox Pattern va Saga Orchestration bo'yicha 30 ta o'rtacha darajadagi test.",
                "Medium",
                "cpu",
                GenerateArchitectureMediumQuestions()
            ),
            CreateQuiz(
                "High-Availability Enterprise System Architecture",
                "architecture",
                "Software Architecture",
                "Event Sourcing Engine, Distributed Transactions, CAP Theorem, Fencing Tokens va Multi-Region Architecture bo'yicha 30 ta qiyin darajadagi test.",
                "Hard",
                "terminal",
                GenerateArchitectureHardQuestions()
            )
        };
    }

    private static List<Question> GenerateArchitectureEasyQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetArchitectureEasyData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateArchitectureMediumQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetArchitectureMediumData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateArchitectureHardQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetArchitectureHardData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static (string text, string? code, List<string> options, string explanation) GetArchitectureEasyData(int index) => index switch
    {
        1 => ("SOLID prinsiplaridan 'S' (Single Responsibility Principle) nimani anglatadi?",
              null,
              new List<string> { "Har bir sinf (class) faqat bitta mas'uliyat va o'zgarish uchun bitta sababga ega bo'lishi kerak", "Har bir sinf faqat bitta metoddan iborat bo'lishi kerak", "Barcha kodlar bitta faylda yozilishi kerak", "Sinf faqat bitta interfeysni implement qilishi kerak" },
              "SRP ta'kidlashicha, har bir modul yoki sinf faqat bitta mantiqiy vazifa va mas'uliyat uchun javob berishi kerak."),
        2 => ("SOLID prinsiplaridan 'O' (Open/Closed Principle) ning asosiy ma'nosi nimada?",
              null,
              new List<string> { "Dasturiy modullar kengaytirish uchun ochiq, lekin o'zgartirish uchun yopiq bo'lishi kerak", "Fayllar faqat o'qish uchun ochiq bo'lishi kerak", "Barcha metodlar private bo'lishi kerak", "Kod har doim ochiq manbali (open-source) bo'lishi kerak" },
              "OCP ga ko'ra, yangi funksionallik qo'shish uchun mavjud kod o'zgartirilmaydi, balki u polimorfizm orqali kengaytiriladi."),
        3 => ("SOLID prinsiplaridan 'D' (Dependency Inversion Principle) nimani ko'zda tutadi?",
              "public OrderProcessor(IOrderRepository repository)",
              new List<string> { "Yuqori darajadagi modullar quyi darajadagi modullarga emas, balki abstraksiyaga (interfeyslarga) bog'lanishi kerak", "Barcha bog'liqliklar statik bo'lishi kerak", "Interface-lar o'rniga faqat konkrets sinflar ishlatilishi kerak", "Barcha obyektlar new kalit so'zi bilan yaratilishi kerak" },
              "DIP modullarni abstraksiyalar (interface) orqali bog me'yorida ajratadi."),
        4 => ("Design Pattern-lardan 'Singleton' namunasining asosiy vazifasi nimadan iborat?",
              null,
              new List<string> { "Sinfning butun ilova davomida yagona obyekt va namunasini (single instance) ta'minlash", "Har bir so'rov uchun yangi obyekt yaratish", "Kodni xotiradan o'chirish", "Database SQL so'rovini hosil qilish" },
              "Singleton butun tizimda faqat yagona obyekt instance saqlanishini va unga global kirish nuqtasini beradi."),
        5 => ("Clean Code prinsiplarida DRY (Don't Repeat Yourself) nimani ta'kidlaydi?",
              null,
              new List<string> { "Bir xil biznes mantiq va kod parchalari qayta-qayta takrorlanmasligi kerak", "Kodni har kuni o'chirish kerak", "Hech qachon comment yozmaslik kerak", "Barcha kodni bitta metodda yozish kerak" },
              "DRY koddagi takrorlanishlarni oldini oladi va uni modulli qilishni talab qiladi."),
        _ => ($"Architecture Easy #{index}-savol: Software Architecture-da #{index}-prinsip qanday vazifani bajaradi?",
              $"// Architectural Principle #{index}\npublic interface IRepository<T> {{ ... }}",
              new List<string> { "Ma'lumotlar bazasi va biznes mantiqni abstraksiya orqali ajratadi", "Faqat fayllarni o'chiradi", "UI-ni sekinlashtiradi", "Faqat bir marta ishlaydi" },
              "Repository pattern ma'lumotlar bazasi amallarini abstraksiya qiladi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetArchitectureMediumData(int index) => index switch
    {
        1 => ("Clean Architecture (Onion / Hexagonal Architecture) da qatlamlar bog'liqligi (Dependency Rule) qaysi tomonga yo'naltirilgan bo'lishi shart?",
              null,
              new List<string> { "Tashqi qatlamlar (UI, Database, Infrastructure) ichki yadroga (Domain / Core) bog'lanishi shart; Core tashqariga bog'lanmaydi", "Domain qatlami Database va UI-ga bog'lanishi shart", "Barcha qatlamlar bir-biriga doiraviy bog'lanadi", "UI to'g'ridan-to'g'ri Database-ga bog'lanishi shart" },
              "Clean Architecture-da bog'liqlik kuchi faqat markazga (Domain/Entities) qarab yo'naladi."),
        2 => ("CQRS (Command Query Responsibility Segregation) patternining asosiy maqsadi va afzalligi nimada?",
              "// Command (Write) vs Query (Read)\npublic class CreateUserCommand : IRequest<Guid> { ... }\npublic class GetUserByIdQuery : IRequest<UserDto> { ... }",
              new List<string> { "O'qish (Query) va Yozish (Command) modellarini ajratib, ularni alohida optimallashtirish va miqyoslash imkonini beradi", "Faqat bitta ma'lumotlar bazasini qo'llaydi", "Frontend va Backend-ni birlashtiradi", "Faqat fayllarni keshlaydi" },
              "CQRS o'qish va yozish operatsiyalarini ajratadi va ularning unumdorligini alohida oshirishga imkon beradi."),
        3 => ("Domain-Driven Design (DDD) da 'Aggregate Root' konsepti nimani anglatadi?",
              "public class Order : AggregateRoot // Root entity managing OrderItems",
              new List<string> { "Ichki entitiylar guruhiga kirishni va tranzaksiyalar izchilligini (consistency boundary) boshqaruvchi asosiy (root) ob'ekt", "Faqat ma'lumotlar bazasi jadvali", "Faqat DTO ob'ekti", "Faqat UI komponenti" },
              "Aggregate Root o'z ichidagi bog'liq obyektlar (masalan Order va OrderItems) izchilligi va tranzaksiyasi uchun javob beradi."),
        _ => ($"Architecture Medium #{index}-savol: Mikroservislarda #{index}-pattern qanday muammoni hal qiladi?",
              $"// Pattern #{index}\npublic class OutboxMessage {{ public Guid Id; public string Content; }}",
              new List<string> { "Transactional Outbox Pattern — Baza tranzaksiyasi va xabarlar brokeriga yuborish izchilligini ta'minlaydi", "Faqat keshni tozalaydi", "UI duplikasiyasini oldini oladi", "Serverni o'chiradi" },
              "Outbox Pattern baza saqlanishi bilan xabar yuborilishi o'rtasidagi atamarlikni (atomicity) ta'minlaydi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetArchitectureHardData(int index) => index switch
    {
        1 => ("Event Sourcing arxitekturasida ob'ekt joriy holatini (Current State) tiklash va replaying (qayta ijro etish) xarajatini kamaytirish uchun nima ishlatiladi?",
              "public class AggregateSnapshot { public int Version; public string Data; }",
              new List<string> { "Snapshotting (Vaqti-vaqti bilan olingan holat suratlari)", "Barcha event-larni o'chirib tashlash", "Faqat Redis-da saqlash", "Event-larni SQL-ga o'girmaslik" },
              "Snapshotting barcha minglab voqealarni boshidan qayta o'qish o'rniga oxirgi snapshot-dan boshlab qayta tiklash imkonini beradi."),
        _ => ($"Architecture Hard #{index}-savol: Taqsimlangan (Distributed) tizimlarda #{index}-muammo qanday hal etiladi?",
              $"// Distributed Architecture #{index}\nvar lock = await _redlock.LockAsync(\"resource\", TimeSpan.FromSeconds(10));",
              new List<string> { "Fencing tokens va distributed lock orqali Split-Brain va ikki baravar bajarilishining oldini olish", "Faqat local lock (Monitor) ishlatish", "Faqat baza taymerini kutish", "Faqat RAM-ni tozalash" },
              "Distributed lock va fencing token-lar taqsimlangan tizimlarda poyga holati (Race Condition) oldini oladi.")
    };
}
