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
                "Angular Standalone Komponentlar, Databinding, Directives va Basic Forms bo'yicha professional savollar.",
                "Easy",
                "code-2",
                GenerateAngularEasyQuestions()
            ),
            CreateQuiz(
                "Angular Signals, RxJS & Architecture Deep Dive",
                "angular",
                "Angular Framework",
                "Signals (signal, computed, effect), RxJS Operators, Router Guards va Interceptors bo'yicha senior savollar.",
                "Medium",
                "layers",
                GenerateAngularMediumQuestions()
            ),
            CreateQuiz(
                "Zone-less Angular & High-Performance Architecture",
                "angular",
                "Angular Framework",
                "Zone-less Change Detection, Ivy Compiler, Custom RxJS Operators va SSR Hydration bo'yicha principal savollar.",
                "Hard",
                "cpu",
                GenerateAngularHardQuestions()
            )
        };
    }

    private static List<Question> GenerateAngularEasyQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "Angular 17+ versiyasida kiritilgan yangi Control Flow sintaksisida shartli chiqarish va sikllar uchun qaysi sintaksis ishlatiladi?",
                "@if (isLoggedIn()) {\n  <app-user-profile />\n} @else {\n  <app-login-btn />\n}\n\n@for (item of items(); track item.id) {\n  <div>{{ item.name }}</div>\n}",
                new List<string> {
                    "@if, @else va @for (track majburiy)",
                    "*ngIf, *ngFor va trackBy funksiyasi",
                    "v-if va v-for sintaksisi",
                    "ng-switch va ng-repeat"
                },
                "@if/@else va @for yangi ichki Control Flow bo'lib tezroq va sintaktik jihatdan toza. @for da track atributi majburiydir."
            ),
            CreateQuestion(
                "Angular Standalone Component atributida `standalone: true` bo'lganda, u o'ziga kerakli modullar va komponentlarni qayerda e'lon qiladi?",
                "@Component({\n  selector: 'app-card',\n  standalone: true,\n  imports: [CommonModule, UserCardComponent]\n})",
                new List<string> {
                    "@Component dekoratori ichidagi `imports` massivida",
                    "AppModule ichidagi declarations massivida",
                    "main.ts faylida bootstrapApplication ichida",
                    "providers massivida"
                },
                "Standalone komponentlar NgModule ga muhtoj emas. Kerakli modullar va komponentlar bevosita `@Component({ imports: [...] })` massivida ko'rsatiladi."
            ),
            CreateQuestion(
                "Angular-da servislarni (Service) butun ilova bo'ylab bitta umumiy namuna (Singleton) qilish uchun decorator-da nima yoziladi?",
                "@Injectable({ providedIn: 'root' })",
                new List<string> {
                    "providedIn: 'root'",
                    "providedIn: 'any'",
                    "scope: 'global'",
                    "singleton: true"
                },
                "providedIn: 'root' servislarni root injector-ga o'tkazib butun ilovada singleton bo'lishini va tree-shaking qilinishini ta me'minlaydi."
            ),
            CreateQuestion(
                "Angular-da hodisalarni (Events - masalan click) va xususiyatlarni (Properties - masalan value) bog'lash sintaksisi qanday?",
                "<button (click)=\"save()\" [disabled]=\"isSaving()\">Saqlash</button>",
                new List<string> {
                    "(click) — Event binding, [disabled] — Property binding",
                    "[click] — Event binding, (disabled) — Property binding",
                    "{{click}} va {{disabled}} interpolation",
                    "bind-click va bind-disabled"
                },
                "Dumaloq qavslar (event) hodisalar uchun, to'rtburchak qavslar [property] esa atribut qiymatini uzatish uchun ishlatiladi."
            ),
            CreateQuestion(
                "Angular Signal inputs (`input()`) va oddiy `@Input()` decorator-i o'rtasidagi asosiy farq nimada?",
                "readonly title = input<string>('Default'); // Signal Input",
                new List<string> {
                    "input() Signal qaytaradi, reactive va read-only bo'lib computed va effect-lar bilan a'lo darajada integratsiya bo'ladi",
                    "input() qiymatini komponent ichida set() qilib o'zgartirish mumkin",
                    "@Input() har doim Signal qaytaradi",
                    "input() faqat string turlarini qabul qiladi"
                },
                "input() Signal Input bo'lib o'zgarmas (read-only) Signal hisoblanadi va komponent reaktivligini oshiradi."
            ),
            CreateQuestion(
                "Angular-da `model()` (Model Inputs) funksiyasi qanday vazifa bajaradi?",
                "readonly count = model<number>(0); // Two-way Signal binding",
                new List<string> {
                    "Ota va bola komponent o'rtasida ikki tomonlama (Two-way Data Binding `[(count)]`) Signal bog me me'liqligini yaratadi",
                    "Faqat ma'lumotlar bazasi modelini ifodalaydi",
                    "Faqat Form-larni tozalaydi",
                    "Faqat RxJS Observable qaytaradi"
                },
                "model() ikkala komponent o'rtasida 2-way binding (`[(val)]`) beruvchi yozilishi mumkin bo'lgan Signal yaratadi."
            ),
            CreateQuestion(
                "Angular-da AsyncPipe (`| async`) ishlatishning asosiy afzalligi nimada?",
                "<div>{{ data$ | async }}</div>",
                new List<string> {
                    "Observable-ga avtomatik subscribe bo'ladi va komponent yo'qolganda avtomatik unsubscribe bo'lib Memory Leak-ni oldini oladi",
                    "So'rovni 10 marta tezlashtiradi",
                    "Faqat string formatlaydi",
                    "Change Detection-ni o me me me'chirib qo'yadi"
                },
                "AsyncPipe shablonda Observable yoki Promise natijasini o'qiydi va avtomatik unsubscribe qilish orqali xotira sizishini tosadigan eng toza yo'l hisoblanadi."
            ),
            CreateQuestion(
                "Angular-da Reactive Forms (`FormGroup`, `FormControl`) va Template-driven Forms (`ngModel`) o'rtasidagi asosiy farq nima?",
                "const form = new FormGroup({ email: new FormControl('', Validators.required) });",
                new List<string> {
                    "Reactive Forms kodda (TypeScript) reaktiv validatsiya va immutable holat beradi; Template-driven esa asosan HTML shablondagi ngModel-ga tayanadi",
                    "Template-driven Forms ko'proq tavsiya etiladi va murakkabroq",
                    "Reactive Forms faqat 1 ta input bilan ishlaydi",
                    "Ikkala forma turi ham bir xil ishlaydi"
                },
                "Reactive Forms TypeScript-da eksplitsit yaratilib reaktiv oqim, qat'iy tiplash va testlash uchun juda qulay."
            ),
            CreateQuestion(
                "Angular-da `@defer` (Deferrable Views) blokining asosiy vazifasi nimadan iborat?",
                "@defer (on viewport) {\n  <app-heavy-chart />\n} @placeholder {\n  <div>Loading chart...</div>\n}",
                new List<string> {
                    "Og'ir komponent va modullarni faqat ekran sohasiga kelganda (Lazy loading) yuklab, dastlabki sahifa yuklanish tezligini oshiradi",
                    "So'rovlarni 5 soniyaga kechiktiradi",
                    "Faqat xatoliklarni yashiradi",
                    "Faqat CSS animasiyalarni to'xtatadi"
                },
                "@defer Angular 17+ da og'ir komponentlarni dangasa (lazy) va shartli ravishda (masalan viewport, hover, interaction) yuklash imkonini beradi."
            ),
            CreateQuestion(
                "Angular-da `HTTP_INTERCEPTORS` (yoki `withInterceptors`) qanday vazifani bajaradi?",
                "export const authInterceptor: HttpInterceptorFn = (req, next) => {\n  const authReq = req.clone({ headers: req.headers.set('Authorization', 'Bearer ...') });\n  return next(authReq);\n};",
                new List<string> {
                    "Barcha chiquvchi HTTP so'rovlar va keluvchi javoblarni markazlashgan holda ushlab, Auth Token qo me'shish yoki xatolar ko'rsatish",
                    "Faqat HTML fayllarni keshlaydi",
                    "Faqat routing amallarini to'xtatadi",
                    "Component CSS stillarini o'zgartiradi"
                },
                "HttpInterceptor barcha HTTP so'rovlarga avtomatik Authorization Header qo'shish va global xatolarni ushlash uchun xizmat qiladi."
            )
        };
    }

    private static List<Question> GenerateAngularMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "Angular Signals reaktiv modelida `signal()`, `computed()` va `effect()` funksiyalarining vazifalari va farqlari nimada?",
                "readonly count = signal(5);\nreadonly double = computed(() => this.count() * 2);\nconstructor() { effect(() => console.log(this.count())); }",
                new List<string> {
                    "signal — o'zgaruvchi qiymat; computed — boshqalardan avtomatik (lazy/memoized) hisoblanuvchi signal; effect — signal o'zgarganda yon ta'sir (side-effect) bajaruvchi reaksiya",
                    "effect faqat matn formatlaydi",
                    "computed har bir millisoniyada majburiy qayta hisoblanadi",
                    "signal qiymatini o'zgartirib bo'lmaydi"
                },
                "signal o'zgaruvchan holat, computed lazy/memoized hosilaviy holat, effect esa logging yoki DOM bilan bog'liq side-effectlar uchun ishlatiladi."
            ),
            CreateQuestion(
                "RxJS-da `switchMap`, `mergeMap`, `concatMap` va `exhaustMap` operatorlarining asosiy farqlari nimada?",
                "this.searchSubject$.pipe(switchMap(term => this.http.get(`/api/search?q=${term}`)))",
                new List<string> {
                    "switchMap yangisi kelishi bilan oldingisini bekor qiladi (search); mergeMap barchasini parallel bajaradi; concatMap ketma-ket bajaradi; exhaustMap bajarilayotganda yangisini inkor etadi",
                    "mergeMap oldingisini bekor qiladi",
                    "concatMap barchasini parallel bajaradi",
                    "exhaustMap har safar yangisini chaqiradi"
                },
                "switchMap qidiruv (search) uchun ideal, chunki u eski tugallanmagan so'rovni bekor qiladi. concatMap tartibni saqlaydi, mergeMap parallel bajaradi."
            ),
            CreateQuestion(
                "Angular Change Detection strategiyalarida `ChangeDetectionStrategy.OnPush` qanday ishlaydi va uning unumdorlikka ta'siri nimada?",
                "@Component({ changeDetection: ChangeDetectionStrategy.OnPush })",
                new List<string> {
                    "Komponentni har bir hodisada emas, faqat Input reference o'zgarganda, Event sodir bo'lganda yoki Signal/AsyncPipe xabar berganda qayta chizadi",
                    "Komponentni umuman qayta chizmaydi",
                    "Zone.js-ni majburiy o me me'chirib beradi",
                    "Faqat Standalone komponentlarda ishlaydi"
                },
                "OnPush keraksiz Change Detection tekshiruvlarini keskin kamaytiradi va faqat aniq o'zgarish bo'lganda (Immutable Input reference / Signal) render qiladi."
            ),
            CreateQuestion(
                "RxJS memory leak (xotira sizishi) oldini olishda `takeUntilDestroyed()` operatori qanday ishlaydi?",
                "this.data$.pipe(takeUntilDestroyed()).subscribe();",
                new List<string> {
                    "DestroyRef kontekstidan foydalanib, komponent yoki servis yo'qotilganda (destroy) Observable oqimini avtomatik yakunlaydi va unsubscribe qiladi",
                    "Observable-ni abadiy saqlab turadi",
                    "Faqat HTTP POST so'rovlarida ishlaydi",
                    "Zone.js-ni to me me me'xtatadi"
                },
                "takeUntilDestroyed Angular 16+ da DestroyRef yordamida avtomatik unsubscribe bo'lib xotira sizishining oldini oladi."
            ),
            CreateQuestion(
                "Angular Dependency Injection-da `ElementRef`, `ViewContainerRef` va `TemplateRef` o'rtasidagi farqlar nimada?",
                "constructor(private el: ElementRef, private vcr: ViewContainerRef) {}",
                new List<string> {
                    "ElementRef — DOM elementiga havola; TemplateRef — `<ng-template>` shabloniga havola; ViewContainerRef — dinamik komponentlarni joylashtirish konteyneri",
                    "ElementRef dinamik komponent yaratadi",
                    "TemplateRef faqat HTML fayl manzilini saqlaydi",
                    "ViewContainerRef faqat CSS stillarini o'zgartiradi"
                },
                "ElementRef nativ DOM elementini beradi. ViewContainerRef dinamik komponent va ko'rinishlarni insert/remove qilish konteyneri hisoblanadi."
            ),
            CreateQuestion(
                "Angular Router Functional Guards (`CanActivateFn`, `CanDeactivateFn`) qanday yoziladi va ularning Class Guard-larga nisbatan afzalligi nimada?",
                "export const authGuard: CanActivateFn = (route, state) => {\n  const authService = inject(AuthService);\n  return authService.isLoggedIn();\n};",
                new List<string> {
                    "Sinf va NgModule muhtojliksiz oddiy funksiya sifatida yoziladi, `inject()` orqali servislarni oladi va kod hajmi ixcham bo'ladi",
                    "Guard-larni har doim Class sifatida yozish majburiy",
                    "Functional Guard-lar servislardan foydalana olmaydi",
                    "Faqat Angular 12 versiyasida ishlaydi"
                },
                "Functional Guard-lar Angular 15+ da joriy etilgan bo'lib boilerplate-ni kamaytiradi va `inject()` bilan clean code beradi."
            ),
            CreateQuestion(
                "Angular-da Content Projection (`<ng-content>`) va Multi-slot Content Projection (`select` atributi) qanday ishlaydi?",
                "<app-card>\n  <h2 card-title>Sarlavha</h2>\n  <p card-body>Matn</p>\n</app-card>\n<!-- Card component template: <ng-content select=\"[card-title]\" /> -->",
                new List<string> {
                    "Ota komponentdan kelayotgan HTML bo'laklarini bola komponent template-ining tayinli joylariga (slots) joylashtirish imkonini beradi",
                    "Faqat text-larni tarjima qilish uchun ishlatiladi",
                    "Faqat CSS grid stillarini o'zgartiradi",
                    "Komponentni o me me'chirish uchun ishlatiladi"
                },
                "<ng-content select=\"...\"> moslashuvchan re-usable komponentlar va multi-slot layout-lar yaratish imkonini beradi."
            ),
            CreateQuestion(
                "Angular Signals va RxJS integratsiyasida `toSignal()` va `toObservable()` funksiyalari qachon va qanday ishlatiladi?",
                "readonly userSignal = toSignal(this.userService.user$, { initialValue: null });",
                new List<string> {
                    "toSignal — RxJS Observable oqimini Signal-ga o'g me'yiradi; toObservable — Signal o'zgarishlarini RxJS Observable qilib uzatadi",
                    "toSignal faqat HTTP POST so'rovlarida ishlaydi",
                    "toObservable Signal qiymatini o'chirib yuboradi",
                    "Ikkala funksiya ham mutlaqo bir xil vazifa bajaradi"
                },
                "toSignal Observable-ni shablonda oson ishlatish uchun Signal-ga o'giradi. toObservable esa RxJS operatorlarini Signal reaktivligiga ulash uchun ishlatiladi."
            ),
            CreateQuestion(
                "Angular State Management-da Component-Store yoki NgRx Signals Store ishlatishning afzalligi nimada?",
                "export const UserStore = signalStore({ state: { users: [] }, methods: ... });",
                new List<string> {
                    "Komponent yoki feature darajasida reaktiv holatni (State), harakatlarni (Actions) va hosilaviy qiymatlarni (Selectors) Signal reaktivligida boshqaradi",
                    "Faqat backend-ga SQL yuboradi",
                    "Faqat LocalStorage faylga yozadi",
                    "Zone.js-ni o'chirib yuboradi"
                },
                "SignalStore va ComponentStore holatni markazlashgan va reaktiv boshqarish imkonini beradi."
            ),
            CreateQuestion(
                "Angular Hydration va Server-Side Rendering (SSR - `provideClientHydration()`) qanday ishlaydi?",
                "bootstrapApplication(AppComponent, { providers: [provideClientHydration()] });",
                new List<string> {
                    "Serverda tayyorlangan DOM strukturani klientda (brauzer) qayta noldan chizmasdan, mavjud DOM tugunlariga voqealarni (Event Listeners) ravon biriktiradi",
                    "Serverda HTML yaratilishini taqiqlaydi",
                    "Brauzer keshini har bir sekundda tozalaydi",
                    "Faqat SQLite bilan ishlaydi"
                },
                "Client Hydration serverda render bo'lgan HTML-ni brauzerda qayta yo'qotmasdan ravon biriktirib o me'tadi (No DOM flickering)."
            )
        };
    }

    private static List<Question> GenerateAngularHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "Zone-less Angular (`provideExperimentalZonelessChangeDetection()`) da Change Detection qanday ishlaydi va Zone.js-dan voz kechishning afzalligi nimada?",
                "bootstrapApplication(AppComponent, { providers: [provideExperimentalZonelessChangeDetection()] });",
                new List<string> {
                    "Zone.js monkey-patching overhead-ini yo'qotadi; Faqat Signal-lar va eksplitsit bildirishnomalar (markForCheck) orqali to'g'ridan-to'g'ri faqat o'zgargan DOM tugunlarini o'ta tez yangilaydi",
                    "Zone.js-ni majburiy qayta yuklaydi",
                    "Change detection-ni umuman ishlamaydigan qiladi",
                    "Faqat Internet Explorer brauzerida ishlaydi"
                },
                "Zoneless Angular Zone.js siz ishlaydi, brauzer native API-larini monkey-patch qilmaydi va Signal reaktivligi bilan o'ta yuqori tezlik beradi."
            ),
            CreateQuestion(
                "Angular Ivy Compiler va Element Instructions (Advanced DOM architecture) qanday ishlaydi?",
                "// ɵɵelementStart(0, 'div'); ɵɵtext(1); ɵɵelementEnd();",
                new List<string> {
                    "HTML shablonlarni ixcham va Tree-shakeable bo'lgan incremental DOM instruksiyalariga (JS funksiyalariga) kompilyatsiya qiladi",
                    "HTML-ni serverda rasmga o'g'iradi",
                    "Faqat CSS stillarini o me'zgartiradi",
                    "Faqat SQL query yaratadi"
                },
                "Ivy Compiler shablonlarni `ɵɵelementStart` kabi ko me me'rinishda JS instruksiyalariga o'giradi, bu xotirani tejaydi va tree-shaking ta'minlaydi."
            ),
            CreateQuestion(
                "Angular-da `untracked()` funksiyasi Signal `effect()` yoki `computed()` ichida qachon ishlatiladi?",
                "effect(() => {\n  const currentUser = this.user();\n  const logCount = untracked(() => this.counter()); // Won't re-trigger effect on counter change!\n});",
                new List<string> {
                    "Signal qiymatini o'qiganda ushbu Signal-ning reaktiv zanjirga (dependency tracking) kirib qolishini va effekti qayta ishga tushirishini oldini olish uchun",
                    "Signal qiymatini o'chirib tashlash uchun",
                    "Faqat HTTP so'rovlarini to'xtatish uchun",
                    "Faqat Form-larni tozalash uchun"
                },
                "untracked() effekti ichida ma'lumot o'qilganda u ushbu signal o'zgarganda qayta trigger bo'lishining oldini oladi."
            ),
            CreateQuestion(
                "Angular Custom Structural Directive (`Directive` with `TemplateRef` and `ViewContainerRef`) yaratish va micro-syntax parsing qanday ishlaydi?",
                "@Directive({ selector: '[appRepeat]' })\npublic class RepeatDirective {\n  constructor(private template: TemplateRef<any>, private vcr: ViewContainerRef) {}\n}",
                new List<string> {
                    "TemplateRef orqali HTML bo'lagini oladi va ViewContainerRef yordamida uni kerakli shart va takrorlanishlar bo'yicha dinamik DOM-ga kiritadi yoki o'chiradi",
                    "Faqat CSS rangini o me'zgartiradi",
                    "Faqat HTTP header-larini qo'shadi",
                    "Faqat SQLite-ga saqlaydi"
                },
                "Struktura direktivalari (`*appRepeat`) TemplateRef va ViewContainerRef orqali DOM strukturasini dinamik boshqaradi."
            ),
            CreateQuestion(
                "Angular-da Hybrid Rendering va Partial Hydration (`@defer (hydrate on ...)` - Angular 18+) qanday ishlaydi?",
                "@defer (hydrate on viewport) {\n  <app-interactive-chart />\n}",
                new List<string> {
                    "Serverda render bo'lgan statik HTML-ni saqlab turadi va faqat kerakli bo'lak interaktiv bo'lganda (masalan scroll qilganda) uning JS kodi va hydration-ini yuklaydi",
                    "Faqat HTML-ni o'chirib beradi",
                    "Faqat CSS-ni yuklaydi",
                    "Faqat SQL serverda ishlaydi"
                },
                "Partial Hydration Angular 18+ da saqlangan HTML-ning faqat kerakli komponent qismlarini zarurat bo'lganda JS yuklab hydrate qiladi."
            ),
            CreateQuestion(
                "Angular Dependency Injection-da `Host`, `Self`, `SkipSelf` va `Optional` parameter decorator-lari di-resolution daraxtini qanday boshqaradi?",
                "constructor(@Host() @Optional() private parent: ParentComponent) {}",
                new List<string> {
                    "@Self — faqat o'z komponentida qidiradi; @SkipSelf — ota injector-dan boshlaydi; @Host — shadow DOM / host-gacha qidiradi; @Optional — topilmasa null beradi",
                    "@Self faqat root injector-dan qidiradi",
                    "@SkipSelf izlashni to'xtatadi",
                    "Ikkala decorator bir xil ishlaydi"
                },
                "Ushbu decorator-lar Angular DI konteyneriga bog'liqlikni (dependency) aniq qaysi darajadagi injector-dan qidirish lozimligini ko'rsatadi."
            ),
            CreateQuestion(
                "Angular Directives-da HostBinding va HostListener o'rniga yangi `@Directive({ host: { ... } })` ob'ekti ishlatilishining afzalligi nimada?",
                "@Component({\n  host: {\n    '[class.active]': 'isActive()',\n    '(click)': 'onClick()'\n  }\n})",
                new List<string> {
                    "Host xususiyatlari va hodisalarini alohida decorator-larsiz bitta ixcham ob'ektda ko'rsatadi hamda Signal inputs bilan a'lo integratsiya beradi",
                    "HostBinding fayllarni shifrlaydi",
                    "HostListener-ni taqiqlaydi",
                    "Faqat RxJS bilan ishlaydi"
                },
                "Component/Directive metadata ichida `host: { ... }` ishlatish koddagi ortiqcha decorator-larni kamaytiradi va toza yozilish ta'minlaydi."
            ),
            CreateQuestion(
                "Angular Micro-Frontend Architecture (Module Federation va Dynamic Remote Component Loading) qanday bajariladi?",
                "const m = await import('remoteApp/UserModule');",
                new List<string> {
                    "Webpack/Rspack Module Federation orqali turli alohida qurilgan (build) Angular ilovalarni runtime-da bitta Shell ilovaga dinamik yuklab birlashtiradi",
                    "Faqat bitta katta bundle fayl hosil qiladi",
                    "Faqat CSS fayllarni import qiladi",
                    "Faqat SQL serverda ishlaydi"
                },
                "Module Federation mikro-frontend arxitekturasida dinamik ravishda boshqa ilovalardagi remote komponent va modullarni yuklash imkonini beradi."
            ),
            CreateQuestion(
                "Angular-da Custom RxJS Operator yaratish va `pipe()` operator zanjiri unumdorligini oshirish qanday amalga oshiriladi?",
                "export function customFilter<T>(predicate: (val: T) => boolean) {\n  return (source: Observable<T>) => new Observable<T>(subscriber => ...);\n}",
                new List<string> {
                    "Mavjud RxJS operatorlarini birlashtirib yoki yangi Observable oqimini Subscriber orqali qayta yozib maxsus reaktiv operator hosil qilish",
                    "Faqat array-ni sort qiladi",
                    "Faqat string-ni o'chiradi",
                    "Faqat SQL so'rov beradi"
                },
                "Custom RxJS operatorlari takrorlanuvchi reaktiv mantiqlarni bitta toza va qayta ishlatiluvchi operatorga jamlash imkonini beradi."
            ),
            CreateQuestion(
                "Angular CD (Change Detection) profiling va Performance Debugging bo'yicha Angular DevTools Profiler qanday ma'lumot beradi?",
                "// DevTools Profiler recording session",
                new List<string> {
                    "Har bir Change Detection sikli qancha vaqt olganini, qaysi komponentlar qayta render bo'lganini va bunga nima sabab bo'lganini (trigger) aniq grafikda ko'rsatadi",
                    "Faqat backend SQL vaqtini ko'rsatadi",
                    "Faqat brauzer RAM hajmini ko'rsatadi",
                    "Faqat CSS xatolarini beradi"
                },
                "Angular DevTools Profiler Change Detection vaqtida qaysi komponentlar render bo'layotgani va unumdorlik muammolarini (bottlenecks) ko'rsatadi."
            )
        };
    }
}
