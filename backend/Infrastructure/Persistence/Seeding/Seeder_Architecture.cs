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
        return new List<Question>
        {
            CreateQuestion("SOLID prinsiplaridan 'Single Responsibility Principle' (SRP) ning asl ma'nosi va amaldagi qo'llanilishi nimada?",
                new List<string> {
                    "Har bir modul yoki sinf faqat bitta mas'uliyat va o'zgarish uchun bitta mantiqiy sababga (one reason to change) ega bo'lishi kerak",
                    "Har bir sinf faqat 1 ta metoddan iborat bo'lishi shart",
                    "Barcha kodingiz faqat bitta faylda yozilishi shart",
                    "Sinf faqat 1 ta o'zgaruvchiga ega bo'lishi kerak"
                },
                "SRP bo'yicha har bir sinf faqat bitta mantiqiy akter yoki vazifa uchun javobgar bo'lishi lozim (Single reason to change)."),

            CreateQuestion("SOLID prinsiplaridan 'Open/Closed Principle' (OCP) ni buzmasdan yangi funksionallik qo'shish qanday amalga oshiriladi?",
                new List<string> {
                    "Mavjud sinf kodiga tegmasdan va o'zgartirmasdan, interfeys va polimorfizm orqali yangi sinf kengaytmasi yaratish orqali",
                    "Mavjud sinf ichidagi switch-case shartlariga yangi case qo'shish",
                    "Eski kodlarni o'chirib tashlab noldan yozish",
                    "Barcha metodlarni private qilish"
                },
                "OCP ga ko'ra tizim kengaytirish uchun ochiq, lekin mavjud kodlarni o'zgartirish uchun yopiq bo'lishi lozim."),

            CreateQuestion("SOLID prinsiplaridan 'Liskov Substitution Principle' (LSP) nimani talab qiladi?",
                new List<string> {
                    "Voris sinf (Child class) ota sinf (Parent class) o'rnini almashtirganda dastur mantiqi va kutilgan xulq-atvori buzilmasligi lozim",
                    "Voris sinf ota sinfning barcha metodlarida Exception otishi kerak",
                    "Voris sinf faqat private metodlarga ega bo'lishi kerak",
                    "Voris sinf ota sinf atributlarini ishlatmasligi kerak"
                },
                "LSP ga ko'ra Voris sinf ota sinf o'rniga qo'yilganda xulq-atvor shartnomasi buzilmasligi kerak."),

            CreateQuestion("SOLID prinsiplaridan 'Interface Segregation Principle' (ISP) ning maqsadi nimadan iborat?",
                new List<string> {
                    "Mijozlar o'zlari ishlatmaydigan ortiqcha va keraksiz metodlarga ega ulkan interfeyslarga bog'lanishga majburlanmasligi lozim",
                    "Barcha metodlarni bitta ulkan IApplicationService interfeysiga yig'ish",
                    "Interfeyslarni mutlaqo ishlatmaslik",
                    "Interfeyslarda faqat 1 ta o'zgaruvchi saqlash"
                },
                "ISP ga muvofiq bitta katta va umumiy interfeys o'rniga muayyan maqsadga yo'naltirilgan ixcham interfeyslar yaratiladi."),

            CreateQuestion("SOLID prinsiplaridan 'Dependency Inversion Principle' (DIP) bo'yicha qaysi ta'rif to'g'ri?",
                new List<string> {
                    "Yuqori darajadagi biznes modullari quyi darajadagi modullarga emas, balki abstraksiyaga (interfeyslarga) bog'lanishi kerak",
                    "Quyi darajadagi modullarga to'g'ridan-to'g'ri new bilan bog'lanish kerak",
                    "Barcha bog'liqliklar statik bo'lishi shart",
                    "Interfeyslar ishlatish taqiqlanadi"
                },
                "DIP modullarni abstraksiyalar (interface/abstract class) orqali ajratib bo'sh bog'liqlik (loose coupling) beradi."),

            CreateQuestion("Design Pattern-lardan 'Factory Method' va 'Abstract Factory' o'rtasidagi farq nimada?",
                new List<string> {
                    "Factory Method bitta obyekt yaratish metodini abstraktsiya qiladi; Abstract Factory esa o'zaro bog'liq obyektlar oilasini yaratish interfeysini taqdim etadi",
                    "Factory Method faqat SQL bazada ishlaydi",
                    "Abstract Factory faqat Singleton bo'lishi shart",
                    "Ikkala pattern ham bir xil ishlaydi"
                },
                "Factory Method 1 ta ob'ekt yaratadi, Abstract Factory esa bog'liq bo'lgan butun obyektlar turkumini (family) beradi."),

            CreateQuestion("Design Pattern-lardan 'Adapter Pattern' qaysi muammoni hal etadi?",
                new List<string> {
                    "Mos kelmaydigan (incompatible) ikki xil interfeysga ega sinflarni bir-biri bilan muvofiq holda ishlashiga imkon beradi",
                    "Obyekt nusxasini olib yangi obyekt yaratadi",
                    "Baza tranzaksiyalarini commit qiladi",
                    "Faqat JSON fayllarni keshlaydi"
                },
                "Adapter Pattern mos kelmaydigan interfeyslar o'rtasida ko'prik vazifasini o'taydi."),

            CreateQuestion("Design Pattern-lardan 'Observer Pattern' qanday ishlaydi va u Event-Driven Architecture-da qanday qo'llaniladi?",
                new List<string> {
                    "Bir obyektning (Subject) holati o'zgarganda, unga obuna bo'lgan barcha tinglovchilar (Observers) avtomatik xabardor qilinadi",
                    "Obyekt holatini doimiy ravishda faylga yozib boradi",
                    "Faqat 1 ta tinglovchiga ruxsat beradi",
                    "Faqat multithreading-ni o'chiradi"
                },
                "Observer Pattern o'zaro bog'liq bo'lmagan obyektlar o'rtasida One-to-Many bildirishnoma uzatish imkonini beradi."),

            CreateQuestion("Clean Code prinsiplarida 'KISS' (Keep It Simple, Stupid) va 'YAGNI' (You Aren't Gonna Need It) nimani ta'kidlaydi?",
                new List<string> {
                    "KISS — kodni iloji boricha sodda va tushunarli tutish; YAGNI — hozir kerak bo'lmagan ortiqcha funksionallik va abstraktsiyalarni oldindan yozmaslik",
                    "YAGNI — barcha kodingizni o'chirib tashlash",
                    "KISS — faqat static metodlar yozish",
                    "Ikkala prinsip ham keshni tozalash haqida"
                },
                "KISS soddalikni targ'ib qiladi, YAGNI esa kelajak uchun taxminiy ortiqcha murakkablik va koddagi ortiqchaliklarni rad etadi."),

            CreateQuestion("Layered Architecture (Ko'p qatlamli arxitektura) da Presentation, Business Logic va Data Access qatlamlarining vazifasi nimada?",
                new List<string> {
                    "Presentation — UI va so'rovlarni qabul qilish; Business Logic — biznes qoidalari; Data Access — bazaga bog'lanish va CRUD amallari",
                    "Data Access qatlami UI bilan to'g'ridan-to'g'ri muloqot qilishi shart",
                    "Presentation qatlamida SQL so'rovlari yoziladi",
                    "Business Logic qatlami faqat HTML tayyorlaydi"
                },
                "Layered Architecture mas'uliyatlarni alohida qatlamlarga (Presentation, Business Logic, Data Access) ajratadi."),

            CreateQuestion("Singleton Design Pattern yaratishda Thread Safety va Double-Check Locking nima uchun kerak?",
                new List<string> {
                    "Multithreading muhitida bir vaqtning o'zida ikkita thread Singleton obyektini parallel yaratib qo'ymasligini kafolatlash uchun",
                    "Singleton xotirasini tezroq tozalash uchun",
                    "Faqat SQL Server bilan ishlash uchun",
                    "Singleton ob'ektini static taqiqlash uchun"
                },
                "Double-Check Locking multithreading tizimlarda kutilmaganda ikkita Singleton instance yaratilib ketmasligi uchun lock bilan himoyalaydi."),

            CreateQuestion("Decorator Pattern va oddiy Inheritance (Vorislik) o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "Decorator ob'ektga dinamik ravishda runtime-da yangi xulq-atvor (masalan kesh, log) qo'shadi; Vorislik esa kompilyatsiya vaqtida qat'iy bog'lanadi",
                    "Vorislik har doim tezroq ishlaydi",
                    "Decorator faqat abstract class-da ishlaydi",
                    "Ikkala yondashuv bir xil"
                },
                "Decorator Pattern 'Composition over Inheritance' tamoyili bo'yicha ob'ektlarni moslashtirish va yangi mas'uliyatlar qo'shish imkonini beradi."),

            CreateQuestion("Facade Pattern qaysi muammoni hal qiladi va uning subsystem-lar bilan aloqasi qanday?",
                new List<string> {
                    "Murakkab va ko'p komponentli ichki quyi tizimlar (subsystems) ustidan mijoz uchun bitta sodda va yagona interfeys beradi",
                    "Barcha sinflarni private qiladi",
                    "Faqat fayllarni diskka saqlaydi",
                    "SQL so'rovlarini avtomatik o'chiradi"
                },
                "Facade Pattern ichki murakkab subsystem-larni yashirib, foydalanuvchiga 1 ta soddalashtirilgan interfeys taqdim etadi."),

            CreateQuestion("Strategy Pattern qanday ishlaydi va u if-else / switch-case zanjirlarini qanday almashtiradi?",
                new List<string> {
                    "Har bir algoritmni alohida sinfga o'rab (encapsulate) va interfeys orqali runtime-da almashtirish imkonini berib, switch zanjirlaridan xalos etadi",
                    "Strategy pattern faqat loop-larni o'chirish uchun ishlatiladi",
                    "Strategy pattern faqat static metodlarda ishlaydi",
                    "U faqat string-larni solishtiradi"
                },
                "Strategy Pattern polimorfizm orqali har bir algoritm variantini alohida sinfga ajratib OCP prinsipini ta'minlaydi."),

            CreateQuestion("Command Pattern ishlatishning asosiy afzalligi nimada?",
                new List<string> {
                    "HTTP yoki biznes so'rovni alohida obyekt (Command) sifatida o'rab, uni navbatga qo'yish (Queueing), loglash va Undo/Redo operatsiyalarini bajarishga imkon beradi",
                    "U faqat SQL so'rovlarini bajaradi",
                    "U faqat UI ranglarini o'zgartiradi",
                    "U ilovani 10 marta sekinlashtiradi"
                },
                "Command Pattern so'rov va uni bajaruvchi o me me'rtasidagi bog'liqlikni uzadi va operatsiyalarni obyekt shaklida saqlaydi."),

            CreateQuestion("DRY (Don't Repeat Yourself) prinsipi va Premature Abstraction (erta abstraktsiya) o'rtasidagi balans nima?",
                new List<string> {
                    "DRY koddagi bilimlarning takrorlanmasligini ko'zlaydi, lekin hali aniq shakllanmagan kodni barvaqt abstraksiya qilish koddagi murakkablikni oshirib yuborishi mumkin",
                    "DRY har doim barcha metodlarni alohida faylga ajratishni shart qiladi",
                    "Premature abstraction har doim majburiy",
                    "Ular bir-biriga qarama-qarshi emas"
                },
                "DRY bilim va mantiq takrorlanishini oldini oladi. Biroq shoshib barvaqt noto'g'ri abstraksiya qurish (Premature Abstraction) kodni tushunarsiz qiladi."),

            CreateQuestion("Low Coupling va High Cohesion tushunchalari dasturiy arxitekturada nimani anglatadi?",
                new List<string> {
                    "Low Coupling — modullarning bir-biriga bog'liqligi minimal bo'lishi; High Cohesion — modul ichidagi a'zolar yagona mantiqiy maqsadga xizmat qilishi",
                    "Low Coupling — barcha kodingiz 1 faylda bo'lishi",
                    "High Cohesion — modullar bir-biri bilan uzviy bog'langan bo'lishi",
                    "Ular o'rtasida farq yo'q"
                },
                "Ideal arxitektura mustaqil o'zgaruvchan modullar (Low Coupling) va o'z ichida mantiqan jipslashgan sinflar (High Cohesion) beradi."),

            CreateQuestion("Monolithic Architecture (Monolit) arxitekturasining asosiy afzalligi va kamchiligi nimada?",
                new List<string> {
                    "Afzalligi — boshlash, deploy qilish va testlash oson; Kamchiligi — loyiha kattalashganda horizontal scale qilish va mustaqil jamoalar ishlashi qiyinlashadi",
                    "Monolit har doim sekinroq ishlaydi",
                    "Monolitda ma'lumotlar bazasi ishlatib bo'lmaydi",
                    "Monolit faqat 1 ta foydalanuvchiga xizmat qiladi"
                },
                "Monolit kichik va o'rta loyihalar uchun tez va oddiy. Biroq yirik masshtabda deploy va scaling cheklovlariga uchraydi."),

            CreateQuestion("MVC (Model-View-Controller) arxitekturasida ma'lumotlar oqimi qanday yo'naltirilgan?",
                new List<string> {
                    "Controller so'rovni qabul qiladi -> Model orqali ma'lumotlarni oladi/o'zgartiradi -> View-ga ma'lumotni render qilish uchun uzatadi",
                    "View to'g'ridan-to'g'ri ma'lumotlar bazasiga yozadi",
                    "Model faqat HTML tugmalarini render qiladi",
                    "Controller faqat CSS stillarini beradi"
                },
                "MVC pattern foydalanuvchi interfeysi (View), ma'lumotlar mantiqi (Model) va kirish harakatlarini (Controller) ajratadi."),

            CreateQuestion("Repository Pattern ishlatishning asosiy maqsadi nima?",
                new List<string> {
                    "Ma'lumotlar bazasiga murojaat qilish logikasini biznes mantiqdan ajratib, In-Memory kolleksiya ko'rinishida taqdim etish",
                    "Faqat HTML sahifa tayyorlash",
                    "Faqat foydalanuvchi parolini shifrlash",
                    "Faqat fayllarni diskka saqlash"
                },
                "Repository Pattern ma'lumotlar manbasini (SQL/NoSQL) abstraktsiyalab, domenga toza CRUD interfeys beradi."),

            CreateQuestion("Unit of Work Pattern nima va u Repository Pattern bilan qanday hamkorlik qiladi?",
                new List<string> {
                    "Bir nechta repository operatsiyalarini bitta ma'lumotlar bazasi tranzaksiyasida (Single Transaction) birlashtirib commit/rollback qilishni ta'minlaydi",
                    "Unit of Work faqat fayllarni o me me me'chiydi",
                    "Unit of Work faqat 1 ta repository bilan ishlaydi",
                    "Ular o'zaro bog'liq emas"
                },
                "Unit of Work bir nechta repository-lar o'rtasida yagona DB transaction va ChangeTracker-ni muvofiqlashtiradi."),

            CreateQuestion("Anemic Domain Model va Rich Domain Model o'rtasidagi asosiy me'moriy farq nimada?",
                new List<string> {
                    "Anemic Model — faqat getter/setter (Data container), biznes mantiq esa servicelarda; Rich Model — ma'lumot va unga tegishli biznes mantiqni bitta entity ichida kapsulalaydi",
                    "Anemic Model har doim tezroq ishlaydi",
                    "Rich Model faqat SQL Server bilan ishlaydi",
                    "Ikkalasi ham mutlaqo bir xil model"
                },
                "Rich Domain Model OOP printsiplariga to'liq mos kelib, biznes mantiq va inki invariantlarni Entity tanasida saqlaydi (OOP encapsulation)."),

            CreateQuestion("Separation of Concerns (SoC) prinsipi nimani talab qiladi?",
                new List<string> {
                    "Dasturni bir-birini takrorlamaydigan va har biri alohida mas'uliyatga ega bo'lgan mustaqil seksiyalarga bo'lishni",
                    "Barcha kodni bitta sinfga yig'ishni",
                    "SQL so'rovlarini HTML ichiga yozishni",
                    "Faqat 1 ta dasturchi ishlashini"
                },
                "SoC (Mas'uliyatlarni ajratish) koddagi turli sohalarni (UI, Business, DB) alohida qatlam yoki komponentlarga ajratishni talab etadi."),

            CreateQuestion("Architectural Spike va Proof of Concept (PoC) nima uchun o'tkaziladi?",
                new List<string> {
                    "Yangi texnologiya, arxitekturaviy risk yoki noaniq talabni asosiy loyihaga qo'shishdan oldin kichik prototipda tekshirib ko'rish uchun",
                    "Faqat loyihani o'chirish uchun",
                    "Faqat UI rangini tanlash uchun",
                    "Faqat hujjat yozish uchun"
                },
                "Spike — texnik risk va texnologiya muvofiqligini amalda baholash uchun o'tkaziladigan tezkor tadqiqot kodi."),

            CreateQuestion("Technical Debt (Texnik qarz) nima va u loyihaga qanday ta'sir qiladi?",
                new List<string> {
                    "Tezkor lekin sifatiz yechimlar oqibatida koddagi murakkablikning ortishi; vaqt o'tishi bilan yangi feature qo me'shishni sekinlashtiradi va xarajatni oshiradi",
                    "Loyihaning ma'lumotlar bazasidagi qarzlar",
                    "Dasturchining maoshi",
                    "Faqat server to me me'lovi"
                },
                "Technical Debt barvaqt sifatli arxitektura qilmaslik oqibatida kelajakda kodni o'zgartirish va refactoring xarajatlarini oshiradi."),

            CreateQuestion("Microservices Architecture (Mikroservislar) ning asosiy ta'rifi va xususiyati nima?",
                new List<string> {
                    "Ilovani alohida deploy bo'ladigan, mustaqil ma'lumotlar bazasiga ega va tarmoq orqali aloqa qiladigan kichik servislar to'plamiga bo'lish",
                    "Barcha kodlarni bitta DLL faylga yig me me me'ish",
                    "Faqat bitta umumiy SQL database ishlatish",
                    "Faqat monolit loyihani nomini o me'zgartirish"
                },
                "Microservices — har bir servis alohida biznes doirasiga (Bounded Context) ega va mustaqil deploy hamda scale bo me'ladigan arxitekturadir."),

            CreateQuestion("REST (Representational State Transfer) arxitektura uslubining Statelessness (Holatsizlik) cheklovi nimani anglatadi?",
                new List<string> {
                    "Server mijozning sessiya holatini o me'zida saqlamaydi; Har bir HTTP so'rovi uni qayta ishlash uchun zarur bo'lgan barcha ma'lumotlarni o me'zi bilan olib kelishi shart",
                    "Server faqat 1 marta so me'rov qabul qila oladi",
                    "Client har doim ma me'lumotlar bazasini o me me'chirishi kerak",
                    "REST faqat stateful ishlaydi"
                },
                "Statelessness bo'yicha server so me'rovlar oralig'ida mijoz holatini saqlamaydi, bu esa serverlarni oson scale qilish imkonini beradi."),

            CreateQuestion("REST API-da HTTP metodlarining Idempotency xususiyati nimani anglatadi?",
                new List<string> {
                    "Bir xil so'rov bir necha marta takroran yuborilganda ham serverdagi yakuniy holat 1 marta yuborilgani bilan bir xil bo'lishi (GET, PUT, DELETE)",
                    "Metod faqat 1 marta chaqirilishi mumkinligi",
                    "Metod har safar har xil natija berishi",
                    "Faqat POST metodi idempotency beradi"
                },
                "Idempotent metodlar (GET, PUT, DELETE) bir necha bor bajarilganda ham server resursi holatini qo'shimcha o me'zgartirmaydi."),

            CreateQuestion("Data Transfer Object (DTO) va Domain Entity o'rtasidagi farq nima?",
                new List<string> {
                    "DTO — faqat ma'lumot uzatish uchun (behavior-less data holder); Domain Entity — biznes mantiq va inki holat qoidalariga ega obyekt",
                    "DTO faqat ma'lumotlar bazasiga yozadi",
                    "Domain Entity faqat JSON serializatsiya uchun",
                    "Ular mutlaqo bir xil"
                },
                "DTO qatlamlararo va tarmoq orqali ma'lumot ko'chirish uchun xizmat qiladi. Domain Entity esa biznes mantiqni kapsulalaydi."),

            CreateQuestion("Peer-to-Peer (P2P) va Client-Server arxitekturalari o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "Client-Server markaziy serverga tayanadi; P2P-da esa har bir tugun (peer) bir vaqtning o'zida ham mijoz ham server vazifasini bajaradi",
                    "P2P faqat brauzerda ishlaydi",
                    "Client-Server hech qanday tarmoq talab qilmaydi",
                    "Ular o'rtasida farq yo'q"
                },
                "Client-Server markazlashgan resurs beruvchi va so'rovchi modelga tayanadi. P2P esa markazlashtirilmagan tugunlar tarmog'idir.")
        };
    }

    private static List<Question> GenerateArchitectureMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("Clean Architecture (Onion / Hexagonal Architecture) da qatlamlar bog'liqligi (Dependency Rule) qaysi tomonga yo'naltirilgan bo'lishi shart?",
                new List<string> {
                    "Tashqi qatlamlar (UI, Database, Infrastructure) ichki yadroga (Domain / Core) bog'lanishi shart; Core yadro tashqariga bog'lanmaydi",
                    "Domain qatlami Database va UI-ga bog'lanishi shart",
                    "Barcha qatlamlar bir-biriga doiraviy bog'lanadi",
                    "UI to'g'ridan-to'g'ri Database-ga bog'lanishi shart"
                },
                "Clean Architecture-da bog'liqlik kuchi faqat markazga (Domain/Entities) qarab yo'naladi. Tashqi o'zgarishlar biznes mantiqqa ta'sir etmaydi."),

            CreateQuestion("Domain-Driven Design (DDD) da 'Bounded Context' va 'Ubiquitous Language' konseptlari nimani anglatadi?",
                new List<string> {
                    "Bounded Context — domendagi aniq chegaralangan mantiqiy soha; Ubiquitous Language — biznes ekspertlar va dasturchilar o'rtasidagi yagona tushunarli terminologiya",
                    "Bounded Context faqat bitta database jadvali",
                    "Ubiquitous Language faqat C# dasturlash tili",
                    "Ikkala konsept ham faqat UI interfeysi bo'yicha"
                },
                "Bounded Context model va terminlar o'z ma'nosini saqlaydigan chegarani bildiradi. Ubiquitous Language esa umumiy biznes tilidir."),

            CreateQuestion("Domain-Driven Design (DDD) da 'Aggregate Root' va 'Entity' va 'Value Object' farqlari nimada?",
                new List<string> {
                    "Entity — unikal ID ga ega; Value Object — faqat qiymatlar majmuasi (ID siz); Aggregate Root — ichki entitiylar guruhining izchillik va tranzaksiya darvozaboni",
                    "Value Object har doim unikal ID-ga ega bo'lishi shart",
                    "Aggregate Root faqat SQL database jadvali",
                    "Entity-larni tengligini faqat qiymati bo'yicha solishtiriladi"
                },
                "Entity ID ga ega, Value Object qiymatlari teng bo'lsa teng hisoblanadi. Aggregate Root tranzaksiyaviy chegara (consistency boundary) vazifasini bajaradi."),

            CreateQuestion("CQRS (Command Query Responsibility Segregation) patternida Command va Query so'rovlarining farqi va afzalligi nimada?",
                new List<string> {
                    "Command — ma'lumotlarni o'zgartiradi (Write, State change); Query — faqat ma'lumotlarni o'qiydi (Read, No state change). Ikkala modelni alohida optimallash imkonini beradi",
                    "Query ma'lumotlarni bazadan o'chiradi",
                    "Command faqat HTML chiqarish uchun ishlatiladi",
                    "Ikkala model ham bitta jadval va DTO bilan ishlashi shart"
                },
                "CQRS Read (Query) va Write (Command) modellarini ajratib, ularning scalability va unumdorligini alohida oshirish imkonini beradi."),

            CreateQuestion("Transactional Outbox Pattern mikroservislarda qaysi muammoni hal qiladi?",
                new List<string> {
                    "Ma'lumotlar bazasi tranzaksiyasi saqlanishi va Xabarlar brokeriga (RabbitMQ) event yuborilishi o'rtasidagi atamarlik (Dual-write problem) ni ta'minlaydi",
                    "Faqat HTML keshini tozalaydi",
                    "Faqat UI duplikasiyasini oldini oladi",
                    "Serverni o'chiradi"
                },
                "Outbox Pattern baza saqlanishi bilan xabar yuborilishi o'rtasidagi atamarlikni ta'minlaydi va xabar yo'qolishini oldini oladi."),

            CreateQuestion("Saga Pattern (Choreography vs Orchestration) taqsimlangan tranzaksiyalarda (Distributed Transactions) qanday ishlaydi?",
                new List<string> {
                    "2PC (Two-Phase Commit) o'rniga ketma-ket mahalliy tranzaksiyalar va xatolik bo'lganda Kompensatsiyaviy tranzaksiyalar (Compensating transactions) orqali holatni bekor qiladi",
                    "Barcha servislarga bir vaqtda SQL lock qo'yadi",
                    "Faqat single-node bazalarda ishlaydi",
                    "Kompensatsiya tranzaksiyalarini taqiqlaydi"
                },
                "Saga Pattern taqsimlangan tizimlarda 2PC o'rniga kompensatsiyaviy tranzaksiyalar yordamida eventual consistency beradi."),

            CreateQuestion("Circuit Breaker Pattern (Closed, Open, Half-Open holatlari) mikroservislar barqarorligida qanday ishlaydi?",
                new List<string> {
                    "Tashqi servis ishlamay qolganda so'rovlar oqimini to'xtatib (Open state) tezkor xato qaytaradi, vaqt o'tib (Half-Open) uni qayta tekshirib tiklaydi",
                    "Serverni avtomatik Formatsiyalaydi",
                    "Faqat database parolini o'zgartiradi",
                    "Circuit Breaker so'rovlarni abadiy kutishga qo'yadi"
                },
                "Circuit Breaker ishlamayotgan tashqi servisga tinimsiz so'rov yuborib resurslarni tugatmaslik uchun Fail Fast mexanizmini beradi."),

            CreateQuestion("API Gateway Pattern (masalan YARP, Ocelot) mikroservislar arxitekturasida qanday vazifalarni o'z bo'yniga oladi?",
                new List<string> {
                    "So'rovlarni to'g'ri mikroservisga marshrutlash (Routing), Auth, Rate Limiting, SSL Termination va Response Aggregation funksiyalarini beradi",
                    "Faqat static fayllarni yuklaydi",
                    "Faqat database migration bajaradi",
                    "Mikroservislar kodini birlashtiradi"
                },
                "API Gateway mijoz va backend mikroservislar o'rtasida yagona kirish nuqtasi va cross-cutting concern-lar markazi hisoblanadi."),

            CreateQuestion("Event-Driven Architecture (EDA) da Event Sourcing va Command Sourcing o'rtasidagi farq nima?",
                new List<string> {
                    "Event Sourcing ob'ektning joriy holatini emas, unga sodir bo'lgan barcha o'zgarmas hodisalar (Events) zanjirini saqlaydi va audit log hamda replay beradi",
                    "Event Sourcing faqat o'chirilgan ma'lumotlarni saqlaydi",
                    "Event Sourcing faqat SQL Server-da ishlaydi",
                    "Ikkalasi ham har sekundda bazani tozalaydi"
                },
                "Event Sourcing tizimda yuz bergan har bir voqeani (Event) xronologik tartibda o'zgarmas (append-only) jurnal sifatda saqlaydi."),

            CreateQuestion("Strangler Fig Pattern monolit tizimni mikroservislarga o'tkazishda (Migration) qanday ishlaydi?",
                new List<string> {
                    "Monolitni bittada to'xtatmasdan, uning funksiyalarini sekin-asta bosqichma-bosqich yangi mikroservislarga ko'chirish va proxy orqali yo'naltirish",
                    "Monolit kodini avtomatik C#-dan Python-ga o'g'irish",
                    "Monolit bazasini darhol o'chirib tashlash",
                    "Faqat In-Memory kesh saqlash"
                },
                "Strangler Fig pattern monolit ilovani bosqichma-bosqich, risk-siz yangi mikroservislar bilan almashtirib borish imkonini beradi."),

            CreateQuestion("Eventual Consistency va Strong Consistency (ACID vs BASE) o'rtasidagi me'moriy tanlov mezonlari nimada?",
                new List<string> {
                    "Strong Consistency darhol barcha node-larda bir xil ma'lumotni kafolatlaydi; Eventual Consistency esa yuqori mavjudlik (Availability) uchun ma'lumotlarni vaqt o'tishi bilan moslashtiradi",
                    "Eventual consistency faqat moliya tizimlarida ishlatiladi",
                    "Strong consistency hech qachon sekinlashmaydi",
                    "Ular o'rtasida farq yo'q"
                },
                "CAP teoremasiga ko'ra high-availability va distributed scaling uchun Eventual Consistency (BASE) tanlanadi."),

            CreateQuestion("Event Sourcing engine-da Event Store xususiyati va Append-Only log nimasi bilan ajralib turadi?",
                new List<string> {
                    "Event Store ma'lumotlarni update yoki delete qilmaydi, faqat yangi event-larni ketma-ket append qiladi va o'zgarmaslik (immutability) beradi",
                    "Event Store faqat fayllarni o'chiradi",
                    "Event Store har doim SQL UPDATE bajaradi",
                    "Event Store faqat 100 ta event saqlay oladi"
                },
                "Event Store append-only log bo'lib, har qanday o'zgarishni o'zgarmas voqea sifatida saqlaydi va audit imkonini beradi."),

            CreateQuestion("Rate Limiting algoritmlarida Token Bucket va Leaky Bucket o'rtasidagi farq nimada?",
                new List<string> {
                    "Token Bucket kelayotgan so'rovlar burst-ini (vaqtinchalik ko'payishini) o'tkazishi mumkin; Leaky Bucket esa so'rovlarni qat'iy o'zgarmas tezlikda (smooth traffic) chiqaradi",
                    "Leaky Bucket har doim so'rovlarni rad etadi",
                    "Token Bucket faqat IP manzil bo'yicha ishlaydi",
                    "Ikkala algoritm bir xil"
                },
                "Token Bucket vaqtinchalik so'rovlar oqimining sakrashini (bursts) qabul qila oladi. Leaky Bucket esa chiqarish tezligini qat'iy tekislaydi."),

            CreateQuestion("Retry Pattern ishlatilganda Exponential Backoff va Jitter nima uchun qo'shiladi?",
                new List<string> {
                    "Tashqi servis nosoz bo'lganda so'rovlar orasidagi vaqtni eksponentsial oshirish va tasodifiy kechikish (Jitter) qo'shib, barcha mijozlar bir vaqtda so'rov urishini (Thundering herd) oldini olish uchun",
                    "Retry pattern-ni to'xtatish uchun",
                    "Faqat SQL bazani o'chirish uchun",
                    "Faqat 1 marta qayta urish uchun"
                },
                "Exponential backoff va Jitter qayta urinishlar vaqtini ehtimollik bilan tarqatib, tashqi servisni qayta tushib ketishidan himoyalaydi."),

            CreateQuestion("Bulkhead Isolation Pattern mikroservislarda nosozlik izolyatsiyasini qanday ta'minlaydi?",
                new List<string> {
                    "Kema bo'linmalari kabi, har bir resurs uchun alohida thread pool va resurs kvotasi ajratib, bitta servisdagi muammo butun ilovani qotirib qo'yishini oldini oladi",
                    "Faqat RAM xotirani tozalaydi",
                    "Barcha so'rovlarni bitta queue-ga yig'adi",
                    "Faqat fayllarni shifrlaydi"
                },
                "Bulkhead Pattern bitta nosoz resurs sababli butun tizim thread-lari band bo'lib yiqilishining (Cascading Failure) oldini oladi."),

            CreateQuestion("Database per Service pattern-da mikroservislar o'rtasida ma'lumotlarni birlashtirish (Cross-Service Data Join) qanday amalga oshiriladi?",
                new List<string> {
                    "SQL JOIN o'rniga API Composition (Gateway/Service layer) yoki CQRS Materialized Views orqali ma'lumotlar avvaldan sinxronlab tayyorlanadi",
                    "To'g'ridan-to'g'ri boshqa servisning bazasiga SQL JOIN yoziladi",
                    "Barcha bazalar bitta SQL Server-ga ko'chiriladi",
                    "Cross-Service join taqiqlangan va buni ilojisi yo'q"
                },
                "Microservice-larda baza izolyatsiya qilinadi. Data Join uchun API Composition yoki CQRS Event Read Store ishlatiladi."),

            CreateQuestion("Asinxron xabarlar almashinuvida RabbitMQ (Message Broker) va Apache Kafka (Event Streaming Platform) o'rtasidagi asosiy me me'moriy farq nima?",
                new List<string> {
                    "RabbitMQ xabarni yetkazgach o me me'chiradi (Smart Broker, Dumb Consumer); Kafka esa xabarlarni saqlaydi va consumer-larga offset bo'yicha qayta o'qish (Dumb Broker, Smart Consumer) imkonini beradi",
                    "RabbitMQ faqat fayllarni yuklaydi",
                    "Kafka faqat 1 ta consumer-ni qo'llaydi",
                    "Ular o'rtasida farq yo'q"
                },
                "RabbitMQ AMQP xabarlarni yetkazishga qaratilgan broker. Kafka esa uzoq muddatli, yuqori unumdorlikdagi append-only event stream platformasidir."),

            CreateQuestion("Domain-Driven Design (DDD) da Anti-Corruption Layer (ACL) pattern qanday rol o'ynaydi?",
                new List<string> {
                    "Eski (Legacy) yoki tashqi tizim modelini yangi toza Domain modelga o'girib, tashqi yomon model kodi domenga sizib kirishini oldini oluvchi tarjimon qatlam",
                    "Faqat antivirus dasturi",
                    "Faqat database parolini tekshirish",
                    "Faqat SQL so'rovlarini bloklash"
                },
                "ACL pattern eski/tashqi API modellarini izolyatsiya qilib, toza Domain modelining buzilishini oldini oladi."),

            CreateQuestion("Domain Events va Integration Events o'rtasidagi farq va ularning tranzaksiya chegarasi qanday?",
                new List<string> {
                    "Domain Event — bitta Bounded Context va tranzaksiya ichida sinxron; Integration Event — bitta context-dan boshqa mikroservislarga asinxron tarqatiladigan voqea",
                    "Integration Event faqat In-Memory ishlaydi",
                    "Domain Event faqat RabbitMQ-da bo'ladi",
                    "Ikkalasi ham bir xil event turi"
                },
                "Domain Events bitta kontekst ichidagi mantiqni sinxron bog me me'laydi. Integration Events esa boshqa servislar uchun broker orqali asinxron uzatiladi."),

            CreateQuestion("CQRS arxitekturasida Materialized View va Read Model qanday yangilanadi?",
                new List<string> {
                    "Write Model-da sodir bo'lgan Domain/Integration Event-lar asinxron tinglanib (EventHandler), Read Model bazasi yangilanadi (Eventual Consistency)",
                    "Read model har sekundda SQL Server-dan avtomatik ko'chiriladi",
                    "Read model faqat dasturchi qo'lda yangilaganda ishlaydi",
                    "Write model va Read model har doim sinxron lock bo'ladi"
                },
                "CQRS-da Command yozilgach, chiqarilgan Event-lar asinxron tinglanib Read Model (MongoDB/Elasticsearch/SQL) yangilab boriladi."),

            CreateQuestion("Feature Flags (Feature Toggles) arxitekturasi va Continuous Deployment o'rtasidagi bog'liqlik nimada?",
                new List<string> {
                    "Yangi funksionallik kodini production-ga deploy qilib, uni foydalanuvchilardan yashirgan holda dinamik ravishda knopka (flag) orqali yoqish/o'chirish imkonini beradi",
                    "Feature flags loyihani o'chirish uchun ishlatiladi",
                    "Feature flags faqat CSS uchun kerak",
                    "U faqat AOT ilovalarda ishlaydi"
                },
                "Feature Toggles koddagi yangi funksiyalarni deployment-dan ajratib, canary/A-B testing va sekin-asta chiqarish imkonini beradi."),

            CreateQuestion("Multi-tenant ilovalarda Database-per-tenant va Shared Database (Discriminator Column) yondashuvlari farqi nimada?",
                new List<string> {
                    "Database-per-tenant har bir mijoz uchun alohida fiziki baza (maksimal izolyatsiya va xavfsizlik); Shared Database esa barcha mijozlarni bitta bazada TenantId kolonka bilan saqlaydi",
                    "Shared Database faqat 1 ta mijozni saqlay oladi",
                    "Database-per-tenant faqat NoSQL-da ishlaydi",
                    "Ular o'rtasida farq yo'q"
                },
                "Multi-tenancy-da izolyatsiya va narx balansi tanlanadi: Database-per-tenant eng xavfsiz va qimmat, Shared DB esa arzon va unumli."),

            CreateQuestion("Microservice Health Checking-da Readiness Probe va Liveness Probe farqi nimada?",
                new List<string> {
                    "Liveness probe container qotib qolgan bo'lsa uni restart qiladi; Readiness probe esa container so'rovlarni qabul qilishga tayyor bo'lmaguncha unga trafik yubormaydi",
                    "Readiness probe container-ni o'chirib yuboradi",
                    "Liveness probe faqat RAM-ni tozalaydi",
                    "Ular bir xil probe"
                },
                "Readiness probe DB ulanmagan pod-ga trafik bermay turadi. Liveness probe esa deadlock bo'lgan pod-ni qayta tushiradi."),

            CreateQuestion("Service Discovery Pattern (masalan Consul, Eureka, Kubernetes DNS) mikroservislarda nima uchun kerak?",
                new List<string> {
                    "Dinamik ravishda o'zgarib turuvchi mikroservis IP manzillari va portlarini markazlashgan registrdan topish (Service Registry) va ulanish uchun",
                    "Faqat fayllarni diskda qidirish uchun",
                    "Faqat IP manzillarni bloklash uchun",
                    "Faqat static fayllarni yuklash uchun"
                },
                "Service Discovery mikroservislar IP manzillari dinamik o'zgarganda (K8s scaling) ularni nomi bo'yicha avtomatik topishni ta'minlaydi."),

            CreateQuestion("Microservice Decomposition (Servislarga bo'lish) da By Business Capability va By Subdomain prinsiplari nimaga tayanadi?",
                new List<string> {
                    "By Business Capability — tashkilot biznes imkoniyatlariga ko'ra (Billing, Shipping); By Subdomain — DDD domen va subdomenlariga ko'ra bo'lish",
                    "Ular kod fayllari hajmiga ko me'ra bo'ladi",
                    "Ular dasturchilar soniga ko me'ra bo me'ladi",
                    "Ular faqat SQL Server jadvallari bo me'yicha bo me'ladi"
                },
                "Decomposition strategiyalari monolitni to'g'ri chegaralangan va mustaqil biznes doiralariga bo me'lish imkonini beradi."),

            CreateQuestion("Distributed Caching-da Cache-Aside va Write-Through keshlash strategiyalari qanday ishlaydi?",
                new List<string> {
                    "Cache-Aside da ilova avval keshni o me me me'qiydi, bo'lmasa bazadan olib keshga yozadi; Write-Through da esa har bir yozish avval keshga keyin bazaga birga bajariladi",
                    "Cache-Aside keshni umuman yangilamaydi",
                    "Write-Through keshni o me'chirib yuboradi",
                    "Ular bir xil strategiya"
                },
                "Cache-Aside eng o'rnatilgan keshlash usuli bo'lib ilova keshni o'zi boshqaradi. Write-Through esa kesh va DB-ni birga yangilaydi."),

            CreateQuestion("Idempotent Consumer Pattern xabarlar brokerida (RabbitMQ/Kafka) takroriy xabar (Duplicate Message) kelganda nima qiladi?",
                new List<string> {
                    "Har bir xabarning unikal MessageId-sini Redis/DB-da tekshiradi; agar xabar avval qayta ishlangan bo'lsa uni ikkinchi bor bajarishni rad etadi",
                    "Takroriy xabarni 2 marta bazaga yozadi",
                    "Barcha xabarlarni o'chirib yuboradi",
                    "Broker-ni to'xtatadi"
                },
                "At-Least-Once delivery kafolatida takroriy xabarlar kelishi mumkin. Idempotent Consumer MessageId orqali takroriy amallarni bloklaydi."),

            CreateQuestion("OpenTelemetry orqali Distributed Tracing yuritilganda TraceId va SpanId nimani anglatadi?",
                new List<string> {
                    "TraceId — butun mikroservislar bo me me me'ylab o me'tgan bitta so'rovning umumiy ID-si; SpanId — har bir alohida servis/metod ishining vaqt bo me'lagi ID-si",
                    "TraceId faqat baza jadvali ID-si",
                    "SpanId faqat IP manzil",
                    "Ular bir xil ID"
                },
                "TraceId barcha servislar bo'ylab o'tuvchi so'rov zanjirini birlashtiradi. SpanId esa har bir servis ichidagi alohida ishini baholaydi."),

            CreateQuestion("Backend-For-Frontend (BFF) Pattern nima uchun ishlatiladi?",
                new List<string> {
                    "Har bir turdagi mijoz (Mobile App, Web SPA, Desktop) uchun maxsus moslashtirilgan alohida API Gateway/Backend qatlamini yaratish uchun",
                    "Faqat ma me'lumotlar bazasini almashtirish uchun",
                    "Faqat CSS fayllarni siqish uchun",
                    "Faqat frontend ramkalarini yuklash uchun"
                },
                "BFF Pattern mobil va web mijozlarga moslashtirilgan alohida yengil API qatlamlarini yaratish imkonini beradi."),

            CreateQuestion("Redis Lua Scripts yordamida Ta taqsimlangan Rate Limiting yaratishning afzalligi nimada?",
                new List<string> {
                    "Lua Script Redis serverida atomik (atomic execution) va bitta tarmoq so'rovida (1 round-trip) bajariladi, race condition oldi olinadi",
                    "Lua script faqat fayllarni o me'qiydi",
                    "Lua script Redis-ni sekinlashtiradi",
                    "U faqat Single-node-da ishlaydi"
                },
                "Redis Lua skriptlari atomik bajarilishi sababli ko'p instansiyali mikreservislarda tezkor va toqnashuvsiz Distributed Rate Limiter beradi.")
        };
    }

    private static List<Question> GenerateArchitectureHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("Event Sourcing arxitekturasida ob'ektning joriy holatini (Current State) minglab Event-lardan qayta tiklash (Replay) xarajatini kamaytirish uchun nima ishlatiladi?",
                new List<string> {
                    "Snapshotting — Vaqti-vaqti bilan olingan holat suratlari; Oxirgi snapshot-dan boshlab keyingi yangi event-lar qayta o'qiladi",
                    "Barcha event-larni o'chirib tashlash",
                    "Faqat Redis-da saqlash",
                    "Event-larni SQL-ga o'g'irmaslik"
                },
                "Snapshotting barcha minglab voqealarni boshidan qayta o'qish o'rniga oxirgi snapshot-dan boshlab qayta tiklash imkonini beradi."),

            CreateQuestion("Taqsimlangan tizimlarda Distributed Locking va Fencing Tokens (masalan Redlock) yordamida Split-Brain va Race Condition qanday oldi olinadi?",
                new List<string> {
                    "Fencing Token — o'sib boruvchi taymer/monoton raqam bo'lib, eski va kechikkan tarmoq so'rovlarini saqlash qurilmasi (storage) tomonidan rad etilishini ta'minlaydi",
                    "Faqat local Monitor lock ishlatish",
                    "Faqat baza taymerini kutish",
                    "Faqat RAM-ni tozalash"
                },
                "Fencing Token-lar kutilmagan pausing (GC pause, network lag) oqibatida eskirgan lock egasi saqlash tizimiga noto'g'ri yozishining oldini oladi."),

            CreateQuestion("Multi-Region Active-Active Database Replication va Conflict-Free Replicated Data Types (CRDTs) qanday ishlaydi?",
                new List<string> {
                    "Bir vaqtning o'zida bir nechta geografik regionlarda yozish imkonini beradi va matematik ravishda toqnashuvsiz (CRDT) ma'lumotlarni birlashtiradi",
                    "Faqat bitta regionda yozishga ruxsat beradi",
                    "Faqat fayllarni shifrlaydi",
                    "Regionlar o'rtasidagi tarmoqni to'xtatadi"
                },
                "CRDT-lar va Active-Active replication bir nechta regionlarda toqnashuvsiz ma'lumotlarni konvergent moslashtirish imkonini beradi."),

            CreateQuestion("Bulkhead Pattern va Thread Pool Isolation mikroservislarda kaskadli nosozliklarni (Cascading Failures) qanday tosadigan mexanizm?",
                new List<string> {
                    "Kema to'siqlari kabi, har bir tashqi resurs uchun alohida thread pool va resurs kvotasi ajratib, bittasidagi muammo butun tizimni to'xtatib qo'yishini oldini oladi",
                    "Faqat RAM hajmini oshiradi",
                    "Faqat fayl tizimini shifrlaydi",
                    "Barcha so'rovlarni bitta queue-ga yig'adi"
                },
                "Bulkhead Pattern bir resursdagi sekinlik yoki xatolik boshqa servislarga ajratilgan thread pool-larni to'ldirib yubormasligi uchun izolyatsiya beradi."),

            CreateQuestion("Rate Limiting va Throttling tizimlarida Sliding Window Log va Leaky Bucket algoritmlari orasidagi farq nimada?",
                new List<string> {
                    "Leaky Bucket so'rovlarni qanchalik kutilmagan kelishidan qat'i nazar o'zgarmas doimiy tezlikda (constant rate) chiqaradi; Sliding Window Log aniq vaqt taymerlariga tayanadi",
                    "Leaky Bucket so'rovlarni o'chirib yuboradi",
                    "Sliding Window faqat NoSQL-da ishlaydi",
                    "Ikkala algoritm bir xil ishlaydi"
                },
                "Leaky Bucket so'rovlar oqimini tekislaydi (traffic shaping). Sliding Window Log esa aniq vaqt oralig'idagi so'rovlar logini yuritadi."),

            CreateQuestion("Domain Events va Integration Events o'rtasidagi asosiy farq va transactional boundary nimada?",
                new List<string> {
                    "Domain Event — bitta Bounded Context va tranzaksiya ichida sinxron; Integration Event — bitta context-dan boshqa mikroservislarga asinxron tarqatiladigan voqea",
                    "Integration Event faqat In-Memory ishlaydi",
                    "Domain Event faqat RabbitMQ-da bo'ladi",
                    "Ikkalasi ham bir xil event turi"
                },
                "Domain Events bitta kontekst ichidagi mantiqni sinxron bog'laydi. Integration Events esa boshqa servislar uchun broker orqali asinxron uzatiladi."),

            CreateQuestion("Zero-Downtime Deployment strategiyalaridan Blue-Green va Canary Deployment o'rtasidagi farq nimada?",
                new List<string> {
                    "Blue-Green — 2 ta parallel muhit (biri faol, biri yangi) o'rtasida 100% trafikni bir zumda o'tkazish; Canary — yangi versiyaga dastlab kichik foiz (5%) trafikni sekin uzatish",
                    "Canary faqat database migration uchun",
                    "Blue-Green serverni o'chirishni talab qiladi",
                    "Ikkala strategiya ham foydalanuvchilarga 500 error beradi"
                },
                "Canary deployment risk-ni kamaytirish uchun foydalanuvchilarning kichik ulushida yangi versiyani sinaydi. Blue-Green esa 2 ta baravar parallel muhitni almashtiradi."),

            CreateQuestion("High-Throughput Distributed Cache System-da Cache Stampede (Thundering Herd Problem) va probabilistic early expiration (XFetch) qanday ishlaydi?",
                new List<string> {
                    "Kesh muddati tugaganda minglab so'rovlar bir vaqtda bazaga urilishini (Stampede) oldini olish uchun so'rovlarni lock qilish yoki muddat tugamasdan ehtimollik bilan fon rejimida yangilash",
                    "Keshni har soniyada tozalab turish",
                    "Baza ulanishini yopib qo'yish",
                    "Faqat static fayllar saqlash"
                },
                "Cache Stampede muddat tugaganda bazaga oqim urilishidir. Lock va Probabilistic early expiration (XFetch) orqali buni oldi olinadi."),

            CreateQuestion("Data Mesh Architecture vs Data Lakehouse (Big Data Architecture) konseptual farqi nimada?",
                new List<string> {
                    "Data Mesh — ma'lumotlarga domenlar bo'yicha markazlashtirilmagan mahsulot (Data as a Product) sifatida yondashadi; Lakehouse esa bitta markaziy platforma beradi",
                    "Data Mesh faqat bitta SQL database saqlaydi",
                    "Data Lakehouse faqat fayllarni o'chiradi",
                    "Ikkala arxitektura ham bir xil"
                },
                "Data Mesh markazlashgan data team o'rniga har bir domen jamoasiga o'z ma'lumotlarini Data Product sifatida egalik qilishni beradi."),

            CreateQuestion("Database Sharding (Horizontal Partitioning) va Distributed Hash Ring (Consistent Hashing) qanday ishlaydi?",
                new List<string> {
                    "Jadval qatorlarini kalit hash-iga ko'ra har xil fizik ma'lumotlar bazalari (shards) o'rtasida bo'ladi; Consistent Hashing server qo'shilganda minimal reshuffle beradi",
                    "Sharding faqat bitta kompyuterda ishlaydi",
                    "Consistent Hashing barcha ma'lumotni o'chirib yuboradi",
                    "Sharding SQL so'rovlarini taqiqlaydi"
                },
                "Sharding ma'lumotlarni tugunlar bo'ylab taqsimlaydi. Consistent Hashing esa yangi shard qo'shilganda ma'lumotlarni qayta taqsimlash xarajatini minimal qiladi."),

            CreateQuestion("CAP teoremasi (Consistency, Availability, Partition Tolerance) va PACELC teoremasining ma me'nosi nimada?",
                new List<string> {
                    "CAP — tarmoq uzilishida (Partition) Consistency va Availability o'rtasida tanlov; PACELC — normal vaqtda Latency va Consistency o'rtasidagi tanlovni baholaydi",
                    "CAP teoremasi faqat Single-node database-lar uchun",
                    "Partition Tolerance har doim o'chirilishi kerak",
                    "Ular kompyuter xotirasi haqida"
                },
                "CAP teoremasi taqsimlangan tizimlarda 3 ta xususiyatdan faqat 2 tasini tanlash mumkinligini uqtiradi. PACELC esa buni kengaytiradi."),

            CreateQuestion("Two-Phase Commit (2PC) taqsimlangan tranzaksiyasida Blocking Problem va Single Point of Failure qanday kelib chiqadi?",
                new List<string> {
                    "Coordinator va Participant node-lar tayyorlik berib (Prepare) javob kutganda lock tutib turadi; Coordinator yiqilsa barcha node-lar cheksiz bloklanib osilib qoladi",
                    "2PC hech qachon lock ishlatmaydi",
                    "2PC har doim tez ishlaydi",
                    "U faqat NoSQL bazalarda bo'ladi"
                },
                "2PC tranzaksiya tugaguncha barcha bazalarda lock tutib turadi. Tarmoq uzilsa barcha node-lar bloklanadi (Blocking Vulnerability)."),

            CreateQuestion("Change Data Capture (CDC - Debezium) texnologiyasi Transactional Outbox Pattern bilan birga qanday ishlaydi?",
                new List<string> {
                    "Outbox jadvaliga yozilgan SQL INSERT-larni DB Transaction Log (WAL / Binlog) orqali real-vaqtda o'qib, ilovaga og'irlik qilmay RabbitMQ/Kafka-ga yuboradi",
                    "CDC har sekundda bazani drop qiladi",
                    "CDC faqat brauzerda ishlaydi",
                    "CDC transaction-larni rad etadi"
                },
                "Debezium CDC baza transaction log-larini (Write-Ahead Log) o'qib zero-impact Outbox publisher vazifasini bajaradi."),

            CreateQuestion("Distributed Locks ishlatilganda Lease Expiration va Clock Drift xavfi qanday yuzaga keladi?",
                new List<string> {
                    "Lock muddati (TTL) server taymerining tezlashishi yoki GC pause sababli tugab ketsa, boshqa instansiya ham lock olib Race Condition hosil qiladi",
                    "Lease expiration faqat SQL-da ishlaydi",
                    "Clock drift lock-ni abadiy saqlaydi",
                    "Ular multithreading-ni to'xtatadi"
                },
                "Clock Drift va kutilmagan Stop-the-world GC pause sababli Lock TTL muddati tugab, ikkita instansiya bir vaqtda resursni o'zgartirib qo'yishi mumkin."),

            CreateQuestion("Event Sourcing Read Model Projections asinxron qayta qurilayotganda (Rebuilding) qanday usul ishlatiladi?",
                new List<string> {
                    "Blue-Green Read Model Projection: Yangi Read Model jadvalini fonda 0-dan event-larni o'qib to'ldirib, tayyor bo'lgach routing-ni unga o'tkazish",
                    "Eski va yangi read model-ni bir vaqtda o me me'chirish",
                    "Event-larni o'chirish",
                    "Faqat In-Memory caching"
                },
                "Read Model Projections yangilanganda Blue-Green proyeksiya usuli qo'llanilib, o'qish xizmatlari uzilishsiz yangi o'qish bazasiga o me'tkaziladi."),

            CreateQuestion("High Availability va Disaster Recovery ko'rsatkichlarida RTO (Recovery Time Objective) va RPO (Recovery Point Objective) nima?",
                new List<string> {
                    "RTO — tizim nosozlikdan keyin qayta tiklanishi uchun ruxsat berilgan maksimal vaqt; RPO — nosozlikda yo me'qotishga ruxsat berilgan maksimal ma'lumotlar vaqti (Data loss window)",
                    "RTO ma'lumotlar bazasi hajmi",
                    "RPO foydalanuvchilar soni",
                    "Ular bir xil ko'rsatkich"
                },
                "RTO — qancha vaqt ichida tizim muloqotga qaytishi kerakligi. RPO — qancha daqiqalik ma'lumot yo'qotilishiga ruxsat borligi."),

            CreateQuestion("Zero-Trust Security Architecture va mTLS (Mutual TLS) mikroservislar o'rtasida nimani beradi?",
                new List<string> {
                    "Tashqi va ichki tarmoqqa bir xil ishonchsiz deb yondashib, har bir mikroservis o'rtasidagi aloqada ikki tomonlama sertifikat (mTLS) va shifrlashni talab qiladi",
                    "Barcha parollarni o'chirib yuboradi",
                    "Faqat IP bo'yicha ruxsat beradi",
                    "Faqat HTTPS portini yopadi"
                },
                "Zero-Trust 'Never Trust, Always Verify' tamoyiliga tayanadi. mTLS ichki mikroservislar aloqasini ham ikki tomonlama sertifikat bilan shifrlaydi."),

            CreateQuestion("Multi-Master Database Replication-da Last Write Wins (LWW) va Vector Clocks toqnashuvlarni qanday hal qiladi?",
                new List<string> {
                    "LWW oxirgi vaqt taymeriga tayanib ma me me me'lumotni bosib yozadi (lekin vaqt siljishida ma'lumot yo me'qoladi); Vector Clocks esa hodisalar ketma-ketligi ierarxiyasini saqlaydi",
                    "LWW har doim birinchi yozilgan ma me me'lumotni saqlaydi",
                    "Vector Clocks faqat RAM-da ishlaydi",
                    "Ular toqnashuvni hal qilmaydi"
                },
                "Vector Clocks causal history-ni kuzatib toqnashuvlarni (conflicts) aniq aniqlaydi. LWW esa sodda bo'lsada Clock Drift-da ma'lumot yo'qotishi mumkin."),

            CreateQuestion("High-Scale Rate Limiting-da RedisBloom / Redis Lua Scripts ishlatishning samaradorligi nimada?",
                new List<string> {
                    "Millionlab foydalanuvchilar so me'rovlarini minimal xotira (Bloom Filter) va 1 round-trip tarmoq bilan atomik cheklash imkonini beradi",
                    "RedisBloom faqat string-larni o me'qiydi",
                    "Lua script-lar tezlikni sekinlashtiradi",
                    "Ular faqat SQL Server-da bo me'ladi"
                },
                "RedisBloom ehtimollik ma'lumotlar tuzilmasi bo'lib, o'ta kam xotira sarflab millionlab kalitlar bo'yicha Rate Limit beradi."),

            CreateQuestion("Chaos Engineering (masalan Chaos Monkey) metodologiyasi nima uchun qo'llaniladi?",
                new List<string> {
                    "Production muhitida kutilmaganda servis yoki tarmoq nosozliklarini sun'iy hosil qilib, tizimning Resiliency va Self-Healing qobiliyatini amalda tekshirish uchun",
                    "Faqat koddagi sintaktik xatolarni topish uchun",
                    "Faqat parollarni buzish uchun",
                    "Faqat ma'lumotlar bazasini o'chirish uchun"
                },
                "Chaos Engineering tizimga qasddan buzilishlar kiritish orqali uning barqarorligi va avtomatik tiklanishini (Self-Healing) sinaydi."),

            CreateQuestion("Service Mesh Architecture (Istio / Linkerd) da Sidecar Proxy qanday ishlaydi?",
                new List<string> {
                    "Har bir mikroservis pod-i yoniga alohida proxy container (Envoy) joylashtirilib, tarmoq aloqasi, mTLS, Tracing va Rate Limiting dastur kodidan ajratib o'tkaziladi",
                    "Sidecar proxy faqat HTML fayllarni render qiladi",
                    "Sidecar proxy faqat ma me me me'lumotlar bazasini saqlaydi",
                    "Sidecar proxy ilovani o'chirib beradi"
                },
                "Service Mesh (Sidecar pattern) tarmoq va xavfsizlik logikasini (mTLS, Retries, Tracing) ilova kodidan ajratib Envoy proxy-ga yuklaydi."),

            CreateQuestion("Apache Kafka Event Streaming-da Consumer Group Rebalance Storm hodisasi nima va u qanday oldini olinadi?",
                new List<string> {
                    "Consumer heart-beat yetib bormay qolganda barcha partition-lar qayta taqsimlanib so'rovlar qotib qoladi; max poll interval va Cooperative Sticky Assignor bilan oldi olinadi",
                    "Rebalance storm Kafka-ni o'chirib tashlaydi",
                    "Faqat foydalanuvchilar soni kamayganda yuz beradi",
                    "U faqat SQL-da bo me'ladi"
                },
                "Cooperative Sticky Assignor partition-larni to'liq to me'xtatmay, faqat o'zgargan consumer-lar o'rtasida silliq taqsimlash imkonini beradi."),

            CreateQuestion("Consistent Hashing (Distributed Hash Ring) da Virtual Nodes (vnodes) nimani ta me'minlaydi?",
                new List<string> {
                    "Fizik serverlarni halqada bir nechta mantiqiy nuqtalar (vnodes) bilan ifodalab, ma me me me'lumotlar taqsimotidagi disbalans (hotspots) ni tekislaydi",
                    "Virtual nodes ma'lumotlarni o'chirib yuboradi",
                    "vnodes faqat Windows OS-da bo me'ladi",
                    "Ular faqat 1 ta node qo me'llaydi"
                },
                "Virtual Nodes har bir fizik serverga halqada ko'plab nuqtalar ajratib, tugunlar o'rtasida ma'lumotlar yuklamasini bir tekis taqsimlaydi."),

            CreateQuestion("Active-Passive (Warm/Cold Standby) va Active-Active Regional Failover o'rtasidagi tanlov mezoni nimada?",
                new List<string> {
                    "Active-Passive arzonroq va sodda, lekin Failover vaqtida RTO talab qiladi; Active-Active esa 0-RTO beradi lekin CRDT / Conflict resolution murakkabligini talab qiladi",
                    "Active-Active faqat Single-node-da bo me'ladi",
                    "Active-Passive har doim qimmatroq",
                    "Ular bir xil failover beri"
                },
                "Active-Active 2 ta regionda baravar yozish imkonini berib 0-RTO beradi, lekin toqnashuvlarni hal qilish arxitekturasini talab etadi."),

            CreateQuestion("OpenTelemetry gRPC va HTTP o'rtasida Context Propagation qanday amalga oshiriladi?",
                new List<string> {
                    "HTTP Header-lar (W3C traceparent) yoki gRPC Metadata orqali TraceId va SpanId axborotini tarmoq so'rovi bilan birga keyingi servisga uzatish orqali",
                    "Faqat faylga yozish orqali",
                    "Faqat baza ulanishini uzish orqali",
                    "Context propagation taqiqlangan"
                },
                "Context Propagation gRPC Metadata yoki HTTP Headers orqali `traceparent` ma'lumotini uzatib, bitta so'rov zanjirini bog me'laydi."),

            CreateQuestion("Asinxron xabarlar yetkazish kafolatlarida At-Least-Once va Exactly-Once Delivery o'rtasidagi amaliy farq nima?",
                new List<string> {
                    "At-Least-Once xabar kamida 1 marta yetib boradi (takroriy xabar bo'lishi mumkin); Exactly-Once esa Idempotency va Deduplication orqali to'liq 1 marta bajarilishini beradi",
                    "At-Least-Once xabarni yo'qotib yuboradi",
                    "Exactly-Once har doim 100 marta sekinroq",
                    "Ular bir xil kafolat"
                },
                "Taqsimlangan tarmoqda At-Least-Once eng tarqalgan. Deduplication va Idempotency bilan u Exactly-Once natijasini beradi."),

            CreateQuestion("Taqsimlangan tranzaksiyalarda Try-Confirm-Cancel (TCC) Pattern va Saga Pattern o'rtasidagi farq nimada?",
                new List<string> {
                    "TCC har bir servisda resursni avval rezerv qiladi (Try), so me me'ng tasdiqlaydi (Confirm) yoki bekor qiladi (Cancel); Saga esa kompensatsiya event-lariga tayanadi",
                    "TCC faqat SQL Server-da ishlaydi",
                    "Saga faqat 1 ta servis bilan ishlaydi",
                    "Ular bir xil pattern"
                },
                "TCC 2PC ga o'xshash 2-bosqichli biznes rezervatsiyasiga tayanadi va izolyatsiya darajasini oshiradi."),

            CreateQuestion("SignalR Scale-Out arxitekturasida Redis Pub/Sub Backplane vazifasi nimada?",
                new List<string> {
                    "Bir nechta API serverlar orqasida turib, bitta serverga uylangan mijoz xabarini boshqa serverga ulangan mijozga Redis Pub/Sub orqali yetkazish",
                    "Faqat HTML keshini tozalash",
                    "Faqat ma me me'lumotlar bazasini o me me'chirish",
                    "Redis Pub/Sub faqat mobile ilovalar uchun"
                },
                "Redis Backplane ko'p instansiyali SignalR serverlar o'rtasida xabarlarni barcha ulangan WebSocket mijozlariga tarqatishni ta'minlaydi."),

            CreateQuestion("Serverless Architecture (FaaS - AWS Lambda / Azure Functions) dagi Cold Start muammosi qanday yengillashtiriladi?",
                new List<string> {
                    "Provisioned Concurrency (doimiy tayyor instansiya) yoki Native AOT kompilyatsiyasi yordamida runtime ishga tushish vaqtini minimal qilish orqali",
                    "Faqat RAM-ni o me'chirish orqali",
                    "Faqat fayllarni yuklamaslik orqali",
                    "Cold start-ni yo'qotib bo'lmaydi"
                },
                "Cold Start FaaS container birinchi marta bootstrap bo'layotganda yuzaga keladi. Provisioned Concurrency va Native AOT buni minimallashtiradi."),

            CreateQuestion("Enterprise Integration Patterns (EIP) da Content-Based Router va Dead Letter Channel nimani bajaradi?",
                new List<string> {
                    "Content-Based Router xabar mazmuniga (payload) qarab uni kerakli kanallarga yo'naltiradi; Dead Letter Channel esa qayta ishlanmagan xatolik xabarlarni alohida navbatga yig'adi",
                    "Dead Letter Channel xabarlarni darhol o'chiradi",
                    "Content-Based Router faqat IP bo'yicha ishlaydi",
                    "Ular faqat frontend-da bo me'ladi"
                },
                "Content-Based Router payload tarkibiga qarab marshrutlaydi. Dead Letter Channel (DLQ) esa barcha xatoli so'rovlarni xavfsiz izolyatsiya qiladi.")
        };
    }
}
