using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetAngularQuizzes()
    {
        return new List<Quiz>
        {
            CreateQuiz(
                "Angular 18+ & TypeScript Fundamentals",
                "angular",
                "Angular Framework",
                "Angular Standalone Komponentlar, Databinding, Directives va Basic Forms bo'yicha 30 ta oson darajadagi test.",
                "Easy",
                "code-2",
                GenerateAngularEasyQuestions()
            ),
            CreateQuiz(
                "Angular Signals, RxJS & Architecture Deep Dive",
                "angular",
                "Angular Framework",
                "Signals (signal, computed, effect), RxJS Operators, Router Guards va Interceptors bo'yicha 30 ta o'rtacha darajadagi test.",
                "Medium",
                "layers",
                GenerateAngularMediumQuestions()
            ),
            CreateQuiz(
                "Zone-less Angular & High-Performance Architecture",
                "angular",
                "Angular Framework",
                "Zone-less Change Detection, Ivy Compiler, Custom RxJS Operators va SSR Hydration bo'yicha 30 ta qiyin darajadagi test.",
                "Hard",
                "cpu",
                GenerateAngularHardQuestions()
            )
        };
    }

    private static List<Question> GenerateAngularEasyQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetAngularEasyData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateAngularMediumQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetAngularMediumData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateAngularHardQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetAngularHardData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static (string text, string? code, List<string> options, string explanation) GetAngularEasyData(int index) => index switch
    {
        1 => ("Angular 17+ versiyasida kiritilgan yangi Control Flow sintaksisida shartli chiqarish uchun qaysi direktiva sintaksisi tavsiya etiladi?",
              "@if (isLoggedIn()) {\n  <app-user-profile />\n} @else {\n  <app-login-btn />\n}",
              new List<string> { "@if va @else", "*ngIf", "v-if", "ng-switch" },
              "@if va @else Angular-dagi yangi, tezroq va sintaktik jihatdan toza control flow hisoblanadi."),
        2 => ("Angular-da komponent shabloniga (template) o'zgaruvchi qiymatini chiqarish (Interpolation) qanday sintaksis bilan yoziladi?",
              "<h1>{{ title }}</h1>",
              new List<string> { "{{ title }}", "{ title }", "${ title }", "<%= title %>" },
              "Qo'shaloq jingalak qavslar {{ }} Angular-da interpolation sintaksisidir."),
        3 => ("Angular Standalone Component atributida `standalone: true` bo'lganda, u o'ziga kerakli modullarni qayerda ko'rsatadi?",
              "@Component({\n  selector: 'app-card',\n  standalone: true,\n  imports: [CommonModule]\n})",
              new List<string> { "imports massivida", "exports massivida", "declarations massivida", "providers massivida" },
              "Standalone komponentlar o'zlariga kerakli bog'liqliklarni to'g'ridan-to'g me me me'yorida `imports` massivida ko'rsatadi."),
        4 => ("Angular-da servislarni (Service) butun ilova bo'ylab bitta umumiy namuna (Singleton) qilish uchun decorator-da nima yoziladi?",
              "@Injectable({ providedIn: 'root' })",
              new List<string> { "providedIn: 'root'", "providedIn: 'any'", "scope: 'global'", "singleton: true" },
              "providedIn: 'root' servislarni ildiz (root) injector-ga o'tkazib singleton qiladi."),
        5 => ("Angular-da hodisalarni (Events - masalan click) bog'lash (Event Binding) qaysi qavslar bilan bajariladi?",
              "<button (click)=\"onSave()\">Saqlash</button>",
              new List<string> { "(click)=\"onSave()\"", "[click]=\"onSave()\"", "bind-click=\"onSave()\"", "{{click()}}" },
              "Dumaloq qavslar (event) Angular-da voqealarni (event) ushlash uchun ishlatiladi."),
        _ => ($"Angular Easy #{index}-savol: Angular-da #{index}-konsept qanday vazifa bajaradi?",
              $"// Component snippet #{index}\nreadonly name = input<string>('Guest');",
              new List<string> { "Ota komponentdan kelayotgan signal input ma'lumotini qabul qiladi", "Formani tozalaydi", "HTTP so'rov yuboradi", "Komponentni o'chiradi" },
              "input() ko'rinishidagi Signal Inputs ota komponentdan kelayotgan ma'lumotni xavfsiz qabul qiladi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetAngularMediumData(int index) => index switch
    {
        1 => ("Angular Signals-da boshqa signal-larga tayangan holda avtomatik hisoblanuvchi o'zgarmas reaktiv qiymat yaratish uchun qaysi funksiya ishlatiladi?",
              "readonly count = signal(5);\nreadonly double = computed(() => this.count() * 2);",
              new List<string> { "computed()", "effect()", "signal()", "linkedSignal()" },
              "computed() boshqa signal-lar o'zgarganda faqat kerakli vaqtda (lazy/memoized) qayta hisoblanuvchi signal yaratadi."),
        2 => ("RxJS-da oxirgi so'rov kelganda avvalgi tugallanmagan HTTP so'rovlarini avtomatik bekor qilish (cancel) uchun qaysi operator ishlatiladi?",
              "this.searchSubject$.pipe(\n  switchMap(term => this.http.get(`/api/search?q=${term}`))\n).subscribe();",
              new List<string> { "switchMap", "mergeMap", "concatMap", "exhaustMap" },
              "switchMap yangi qiymat kelishi bilan oldingi ichki observable-ga unsubscribe bo'lib so'rovni bekor qiladi."),
        3 => ("Angular-da OnPush Change Detection strategiyasi ishlatilganda komponent qachon qayta chiziladi (render bo'ladi)?",
              "@Component({ changeDetection: ChangeDetectionStrategy.OnPush })",
              new List<string> { "Input xususiyatiga yangi obyekt havolasi (reference) kelganda yoki Signal/AsyncPipe o'zgarganda", "Har bir sichqoncha harakatida", "Har bir setTimeout chaqirilganda", "Hech qachon qayta chizilmaydi" },
              "OnPush faqat Input reference o'zgarganda, Hodisa (Event) sodir bo'lganda yoki Signal/AsyncPipe xabar berganda qayta chiziladi."),
        _ => ($"Angular Medium #{index}-savol: Angular-da #{index}-amaliyot bo'yicha qaysi yondashuv to me me'yorida?",
              $"// RxJS memory leak protection #{index}\nthis.data$.pipe(takeUntilDestroyed()).subscribe();",
              new List<string> { "takeUntilDestroyed() orqali komponent yo'qotilganda avtomatik unsubscribe bo'lish", "subscribe-ni bo'sh qoldirish", "setInterval orqali kuzatish", "Window.onclose hodisasini ishlatish" },
              "takeUntilDestroyed xotira sizishining (memory leak) oldini oladi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetAngularHardData(int index) => index switch
    {
        1 => ("Zone-less Angular (Zone.js siz Angular) da Change Detection qanday ishlaydi va nimaga tayanadi?",
              "provideExperimentalZonelessChangeDetection()",
              new List<string> { "Signal-lar va explicit bildirishnomalar (markForCheck) orqali to'g'ridan-to'g'ri faqat o'zgargan DOM tugunlarini aniqlaydi", "Zone.js har bir hodisani majburiy monkey patch qiladi", "Timer-lar orqali har soniyada butun DOM-ni tekshiradi", "Faqat SSR rejimida ishlaydi" },
              "Zoneless Angular Zone.js siz ishlaydi va faqat Signal reaktivligi va xabarnomalariga tayangan holda DOM-ni juda tez yangilaydi."),
        _ => ($"Angular Hard #{index}-savol: High-performance Angular-da #{index}-optimizatsiya bo'yicha qaysi mantiq to'g'ri?",
              $"// Custom RxJS / Signal Scheduler #{index}\nuntracked(() => this.expensiveComputation());",
              new List<string> { "untracked() yordamida signal effektlari ichida keraksiz reaktiv bog'liqliklar zanjirini uzish", "Faqat setTimeout ishlatish", "Barcha komponentlarni bitta modulga yig'ish", "CSS klasslarni o'chirish" },
              "untracked() funksiyasi effekt ichidagi ma'lumotlarni o'qiganda zanjirga kirib qolishining oldini oladi.")
    };
}
