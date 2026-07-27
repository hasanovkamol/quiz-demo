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
        return new List<Question>
        {
            CreateQuestion("Angular 17+ versiyasida kiritilgan yangi Control Flow sintaksisida shartli chiqarish va sikllar uchun qaysi sintaksis ishlatiladi?",
                new List<string> {
                    "@if, @else va @for (track majburiy)",
                    "*ngIf, *ngFor va trackBy funksiyasi",
                    "v-if va v-for sintaksisi",
                    "ng-switch va ng-repeat"
                },
                "@if/@else va @for yangi ichki Control Flow bo'lib tezroq va sintaktik jihatdan toza. @for da track atributi majburiydir."),

            CreateQuestion("Angular Standalone Component atributida standalone: true bo'lganda, u o'ziga kerakli modullar va komponentlarni qayerda e'lon qiladi?",
                new List<string> {
                    "@Component dekoratori ichidagi imports massivida",
                    "AppModule ichidagi declarations massivida",
                    "main.ts faylida bootstrapApplication ichida",
                    "providers massivida"
                },
                "Standalone komponentlar NgModule ga muhtoj emas. Kerakli modullar va komponentlar bevosita `@Component({ imports: [...] })` massivida ko'rsatiladi."),

            CreateQuestion("Angular-da servislarni (Service) butun ilova bo'ylab bitta umumiy namuna (Singleton) qilish uchun decorator-da nima yoziladi?",
                new List<string> {
                    "providedIn: 'root'",
                    "providedIn: 'any'",
                    "scope: 'global'",
                    "singleton: true"
                },
                "providedIn: 'root' servislarni root injector-ga o'tkazib butun ilovada singleton bo'lishini va tree-shaking qilinishini ta'minlaydi."),

            CreateQuestion("Angular-da hodisalarni (Events - masalan click) va xususiyatlarni (Properties - masalan disabled) bog'lash sintaksisi qanday?",
                new List<string> {
                    "(click) — Event binding, [disabled] — Property binding",
                    "[click] — Event binding, (disabled) — Property binding",
                    "{{click}} va {{disabled}} interpolation",
                    "bind-click va bind-disabled"
                },
                "Dumaloq qavslar (event) hodisalar uchun, to'rtburchak qavslar [property] esa atribut qiymatini uzatish uchun ishlatiladi."),

            CreateQuestion("Angular Signal inputs (input()) va oddiy @Input() decorator-i o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "input() Signal qaytaradi, reactive va read-only bo'lib computed va effect-lar bilan a'lo darajada integratsiya bo'ladi",
                    "input() qiymatini komponent ichida set() qilib o'zgartirish mumkin",
                    "@Input() har doim Signal qaytaradi",
                    "input() faqat string turlarini qabul qiladi"
                },
                "input() Signal Input bo'lib o'zgarmas (read-only) Signal hisoblanadi va komponent reaktivligini oshiradi."),

            CreateQuestion("Angular-da model() (Model Inputs) funksiyasi qanday vazifa bajaradi?",
                new List<string> {
                    "Ota va bola komponent o'rtasida ikki tomonlama (Two-way Data Binding [(count)]) Signal bog'liqligini yaratadi",
                    "Faqat ma'lumotlar bazasi modelini ifodalaydi",
                    "Faqat Form-larni tozalaydi",
                    "Faqat RxJS Observable qaytaradi"
                },
                "model() ikkala komponent o'rtasida 2-way binding ([(val)]) beruvchi yozilishi mumkin bo'lgan Signal yaratadi."),

            CreateQuestion("Angular-da AsyncPipe (| async) ishlatishning asosiy afzalligi nimada?",
                new List<string> {
                    "Observable-ga avtomatik subscribe bo'ladi va komponent yo'qolganda avtomatik unsubscribe bo'lib Memory Leak-ni oldini oladi",
                    "So'rovni 10 marta tezlashtiradi",
                    "Faqat string formatlaydi",
                    "Change Detection-ni o'chirib qo'yadi"
                },
                "AsyncPipe shablonda Observable yoki Promise natijasini o'qiydi va avtomatik unsubscribe qilish orqali xotira sizishini tosadigan eng toza yo'l hisoblanadi."),

            CreateQuestion("Angular-da Reactive Forms (FormGroup, FormControl) va Template-driven Forms (ngModel) o'rtasidagi asosiy farq nima?",
                new List<string> {
                    "Reactive Forms kodda (TypeScript) reaktiv validatsiya va immutable holat beradi; Template-driven esa asosan HTML shablondagi ngModel-ga tayanadi",
                    "Template-driven Forms ko'proq tavsiya etiladi va murakkabroq",
                    "Reactive Forms faqat 1 ta input bilan ishlaydi",
                    "Ikkala forma turi ham bir xil ishlaydi"
                },
                "Reactive Forms TypeScript-da eksplitsit yaratilib reaktiv oqim, qat'iy tiplash va testlash uchun juda qulay."),

            CreateQuestion("Angular-da @defer (Deferrable Views) blokining asosiy vazifasi nimadan iborat?",
                new List<string> {
                    "Og'ir komponent va modullarni faqat ekran sohasiga kelganda (Lazy loading) yuklab, dastlabki sahifa yuklanish tezligini oshiradi",
                    "So'rovlarni 5 soniyaga kechiktiradi",
                    "Faqat xatoliklarni yashiradi",
                    "Faqat CSS animasiyalarni to'xtatadi"
                },
                "@defer Angular 17+ da og'ir komponentlarni dangasa (lazy) va shartli ravishda (masalan viewport, hover, interaction) yuklash imkonini beradi."),

            CreateQuestion("Angular-da HTTP_INTERCEPTORS (yoki withInterceptors) qanday vazifani bajaradi?",
                new List<string> {
                    "Barcha chiquvchi HTTP so'rovlar va keluvchi javoblarni markazlashgan holda ushlab, Auth Token qo'shish yoki xatolar ko'rsatish",
                    "Faqat HTML fayllarni keshlaydi",
                    "Faqat routing amallarini to'xtatadi",
                    "Component CSS stillarini o'zgartiradi"
                },
                "HttpInterceptor barcha HTTP so'rovlarga avtomatik Authorization Header qo'shish va global xatolarni ushlash uchun xizmat qiladi."),

            CreateQuestion("Angular Signal Outputs (output()) va klassik @Output() & EventEmitter o'rtasidagi farq nimada?",
                new List<string> {
                    "output() yengilroq, RxJS va Zone.js ga bog'liq bo'lmagan, clean reactive API beradi; emit() o'rniga emit() metodini qo'llaydi",
                    "output() faqat string qiymat qaytaradi",
                    "@Output() har doim Signal qaytaradi",
                    "output() komponentni o'chirib yuboradi"
                },
                "output() Angular 17.3+ da taqdim etilgan bo'lib, RxJS EventEmitter-ga bog'liq bo'lmagan yengil Signal Output hisoblanadi."),

            CreateQuestion("Angular directives turlarida Structural Directive (*ngIf) va Attribute Directive ([ngClass]) o'rtasidagi farq nima?",
                new List<string> {
                    "Structural Directives DOM elementlarining tuzilishini (qo'shish/o'chirish) o'zgartiradi; Attribute Directives esa element ko'rinishi va atributlarini o'zgartiradi",
                    "Attribute Directives DOM-ni o'chiradi",
                    "Structural Directives faqat rang beradi",
                    "Ular o'rtasida farq yo'q"
                },
                "Struktura direktivalari DOM elementlarini qo'shadi yoki o'chiradi (*). Atribut direktivalari esa mavjud element ko'rinishini ([]) o'zgartiradi."),

            CreateQuestion("Angular Pipes mexanizmida Pure Pipe (@Pipe({ pure: true })) nimani anglatadi?",
                new List<string> {
                    "Pipe faqat uning kiruvchi parametrlarining havolasi (primitive qiymat yoki object reference) o'zgargandagina qayta hisoblanadi (memoization)",
                    "Pipe har bir Change Detection siklida chaqiriladi",
                    "Pipe faqat backend HTTP so'rovlarini bajaradi",
                    "Pipe faqat int turlarini formatlaydi"
                },
                "Pure pipe memoization prinsipida ishlaydi: kiruvchi parametr qiymati o'zgarmasa qayta hisoblanmay keshdagi natijani beradi."),

            CreateQuestion("Angular-da viewChild() (Signal-based ViewChild) va eshitilgan @ViewChild() o'rtasidagi afzallik nimada?",
                new List<string> {
                    "viewChild() Signal qaytaradi, undefined xavfini kamaytiradi va ngAfterViewInit hooksiz reaktiv foydalanish imkonini beradi",
                    "viewChild() DOM elementini o'chirib tashlaydi",
                    "@ViewChild() har doim Signal qaytaradi",
                    "viewChild() faqat string qaytaradi"
                },
                "viewChild() Signal queries Angular 17.2+ da joriy qilingan bo'lib, DOM element va komponentlarga reaktiv va xavfsiz Signal havolasini beradi."),

            CreateQuestion("Angular komponent Lifecycle Hooks zanjirida ngOnInit va ngOnDestroy metodlarining o'rni nima?",
                new List<string> {
                    "ngOnInit — komponent inputlari yuklanib bo'lgach initsializatsiya uchun; ngOnDestroy — komponent o'chirilayotganda resurslarni va obunalarni tozalash uchun",
                    "ngOnInit faqat komponent o'chirilganda ishlaydi",
                    "ngOnDestroy faqat HTML render bo'lganda ishlaydi",
                    "Ular har bir sekundda chaqiriladi"
                },
                "ngOnInit komponent tayyor bo'lganda ishga tushadi, ngOnDestroy esa xotira sizishi (memory leak) ni oldini olish uchun obunalarni tozalaydi."),

            CreateQuestion("Angular-da ContentChild (contentChild()) va ViewChild (viewChild()) o'rtasidagi me'moriy farq nima?",
                new List<string> {
                    "ViewChild — komponentning o'z HTML shablonidagi elementlarni ushlaydi; ContentChild — <ng-content> orqali proyeksiyalangan (Projected) elementlarni ushlaydi",
                    "ContentChild faqat CSS stillarini beradi",
                    "ViewChild faqat ota komponentda ishlaydi",
                    "Ular bir xil elementni ushlaydi"
                },
                "ViewChild o'z template-idagi elementlarni qidiradi, ContentChild esa ota komponent tomonidan `<ng-content>` orqali uzatilgan elementlarni qidiradi."),

            CreateQuestion("Angular CLI vositasida yangi standalone komponent yaratish uchun qaysi buyruq ishlatiladi?",
                new List<string> {
                    "ng generate component my-comp --standalone",
                    "ng create component my-comp",
                    "ng new component my-comp",
                    "ng add component my-comp"
                },
                "ng generate component (yoki ng g c) buyrug'i Angular Standalone komponent shablonini shakllantiradi."),

            CreateQuestion("Angular Reactive Forms-da formaga o'rnatilgan standart validatatorlar (Validators.required, Validators.email) qanday tekshiriladi?",
                new List<string> {
                    "formControl.valid, formControl.hasError('required') va formControl.errors xususiyatlari orqali HTML va TypeScript-da tekshiriladi",
                    "Faqat backend-ga so'rov yuborilganda tekshiriladi",
                    "Faqat brauzer sahifasi yangilanganda tekshiriladi",
                    "Faqat CSS orqali tekshiriladi"
                },
                "FormControl obyektida `errors`, `valid`, `invalid`, `touched` kabi xususiyatlar orqali real-vaqtda validatsiya holati aniqlanadi."),

            CreateQuestion("Angular komponent stillarini kapsulashda (ViewEncapsulation) ViewEncapsulation.Emulated va None o'rtasidagi farq nima?",
                new List<string> {
                    "Emulated — komponent CSS stillarini atributlar (_ngcontent-ng-c123) orqali izolyatsiya qiladi; None — stillarni global tarzda butun ilovaga tarqatadi",
                    "Emulated stillarni umuman qo'llamaydi",
                    "None faqat ShadowDOM brauzerlarida ishlaydi",
                    "Ular o'rtasida farq yo'q"
                },
                "ViewEncapsulation.Emulated (default) Angular-ning stillar izolyatsiyasini ta'minlaydi. None esa stillarni global qilib butun DOM-ga ta'sir o'tkazadi."),

            CreateQuestion("Angular Router-da path va component parametrlari qaysi funksiya orqali ilovada ro'yxatdan o'tkaziladi?",
                new List<string> {
                    "provideRouter([{ path: 'users', component: UsersComponent }])",
                    "provideHttp([{ route: 'users' }])",
                    "bootstrapRouter('users')",
                    "registerRoutes('users')"
                },
                "Angular 15+ standalone ilovalarda `main.ts` ichida `provideRouter(routes)` orqali marshrutlar ro'yxati beriladi."),

            CreateQuestion("Angular-da HostListener (yoki component host metadata) qanday vazifa bajaradi?",
                new List<string> {
                    "Komponent joylashgan host DOM elementidagi va brauzerdagi hodisalarni (masalan: scroll, window:resize) eshitish va metod chaqirish uchun",
                    "Faqat backend so'rovlarini eshitish uchun",
                    "Faqat CSS animation yaratish uchun",
                    "Faqat fayllarni yuklash uchun"
                },
                "HostListener host element va brauzer window hodisalarini eshitib, komponent ichidagi metodni chaqirish imkonini beradi."),

            CreateQuestion("Angular-da ng-template, ng-container va ng-content o'rtasidagi farqlar nimada?",
                new List<string> {
                    "ng-template — render bo'lmaydigan shablon; ng-container — DOM-ga qo'shimcha HTML teg qo'shmaydigan mantiqiy guruh; ng-content — Content Projection uyasi",
                    "ng-container faqat rasm fayllarni saqlaydi",
                    "ng-template har doim brauzerda ko'rinib turadi",
                    "ng-content faqat RxJS uchun kerak"
                },
                "ng-template faqat shart mos kelganda render bo'ladi. ng-container extra wrapper teglarsiz guruhlaydi. ng-content esa ota komponent HTML-ini qabul qiladi."),

            CreateQuestion("Angular-da inject() funksiyasining klassik Constructor Injection-ga nisbatan afzalligi nima?",
                new List<string> {
                    "Constructor yozmasdan, funksiyalar va custom guard/interceptor-lar ichida ham Dependency Injection-dan foydalanish imkonini beradi",
                    "inject() faqat HTML ichida ishlaydi",
                    "Constructor injection taqiqlangan",
                    "inject() faqat SQL Server bilan ishlaydi"
                },
                "inject() funksiyasi injection context ichida istalgan joyda (field initializer, functional guard/interceptor) servislarni chaqirish imkonini beradi."),

            CreateQuestion("Angular shablonlarida KeyValue Pipe (| keyvalue) qachon ishlatiladi?",
                new List<string> {
                    "JavaScript Object yoki Map to'plamlarini @for siklida key-value juftliklari bo'yicha aylanish uchun",
                    "Faqat massivlarni saralash uchun",
                    "Faqat parollarni shifrlash uchun",
                    "Faqat HTTP so'rovlarida"
                },
                "KeyValue Pipe ob'ekt kalitlari va qiymatlarini `{ key: ..., value: ... }` ko'rinishida siklga uzatish imkonini beradi."),

            CreateQuestion("Angular-da environment.ts va environment.development.ts fayllari qanday ishlaydi?",
                new List<string> {
                    "Build vaqtida fileReplacements orqali Production va Development API URL va konfiguratsiyalarini avtomatik almashtirish uchun",
                    "Faqat HTML ranglarini o'zgartirish uchun",
                    "Faqat database parolini saqlash uchun",
                    "Faqat CSS-ni siqish uchun"
                },
                "angular.json ichidagi fileReplacements sozlamasi build rejimiga mos environment faylini avtomatik tanlaydi."),

            CreateQuestion("Angular Title va Meta servislari (Title, Meta) nima uchun kerak?",
                new List<string> {
                    "Sahifa sarlavhasini (<title>) va SEO meta teglarini (description, og:title) dinamik o'zgartirish uchun",
                    "Faqat ma'lumotlar bazasini yangilash uchun",
                    "Faqat rasmlarni yuklash uchun",
                    "Faqat formani validatsiya qilish uchun"
                },
                "Title va Meta servislar SPA ilovalarda SEO va ijtimoiy tarmoq ulashishlari (Open Graph) uchun sahifa metadatasini dinamik yangilaydi."),

            CreateQuestion("Angular Router-da Path Location Strategy va Hash Location Strategy (#) o'rtasidagi farq nima?",
                new List<string> {
                    "Path Location Strategy — toza URL (/users) va HTML5 PushState ishlatadi; Hash Location Strategy esa URL-da # symbol (/#/users) ishlatadi",
                    "Hash Location Strategy faqat Linux-da ishlaydi",
                    "Path Location Strategy server sozlamasini talab qilmaydi",
                    "Ular o'rtasida farq yo'q"
                },
                "Path Location Strategy (default) toza URL beradi, lekin serverda fallback (rewrite to index.html) sozlanishi kerak."),

            CreateQuestion("Angular-da ElementRef.nativeElement orqali to'g'ridan-to'g'ri DOM-ga tegish nima uchun tavsiya etilmaydi?",
                new List<string> {
                    "Xavfsizlik (XSS) xavfini oshiradi hamda Server-Side Rendering (SSR) va Web Workers muhitida dastur crash bo'lishiga olib kelishi mumkin",
                    "Chunki bu dasturni 100 marta sekinlashtiradi",
                    "Chunki nativeElement faqat CSS ni o'qiydi",
                    "Chunki elementRef faqat Angular 10 da mavjud"
                },
                "Direct DOM manipulation SSR va Web Worker muhitlarida ishlamaydi va XSS xavfini oshiradi. Renderer2 yoki Signal/Template binding tavsiya etiladi."),

            CreateQuestion("Angular ilovasiga Tailwind CSS integratsiya qilinganda styles.css-da qaysi ko'rsatmalar qo'shiladi?",
                new List<string> {
                    "@import 'tailwindcss';",
                    "import 'tailwind.js';",
                    "<link rel='stylesheet' href='tailwind.css'>",
                    "useTailwindCSS()"
                },
                "Tailwind CSS v4+ da `@import 'tailwindcss';` ko'rsatmasi standart CSS fayliga qo'shiladi."),

            CreateQuestion("Angular 17+ @for siklida track atributi nima uchun majburiy qilingan?",
                new List<string> {
                    "Kolleksiya o'zgarganda butun DOM-ni qayta chizmasdan, faqat o'zgargan elementni unikal ID bo'yicha samarali va tez yangilash uchun",
                    "Faqat sikl elementlarini sanash uchun",
                    "Faqat text-ni formatlash uchun",
                    "Faqat error-larni bostirish uchun"
                },
                "track atributi React key yoki klassik trackBy kabi DOM node-larini qayta ishlatish (DOM reuse) orqali rendering tezligini oshiradi.")
        };
    }

    private static List<Question> GenerateAngularMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("Angular Signals reaktiv modelida signal(), computed() va effect() funksiyalarining vazifalari va farqlari nimada?",
                new List<string> {
                    "signal — o'zgaruvchi qiymat; computed — boshqalardan avtomatik (lazy/memoized) hisoblanuvchi signal; effect — signal o'zgarganda yon ta'sir (side-effect) bajaruvchi reaksiya",
                    "effect faqat matn formatlaydi",
                    "computed har bir millisoniyada majburiy qayta hisoblanadi",
                    "signal qiymatini o'zgartirib bo'lmaydi"
                },
                "signal o'zgaruvchan holat, computed lazy/memoized hosilaviy holat, effect esa logging yoki DOM bilan bog'liq side-effectlar uchun ishlatiladi."),

            CreateQuestion("RxJS-da switchMap, mergeMap, concatMap va exhaustMap operatorlarining asosiy farqlari nimada?",
                new List<string> {
                    "switchMap yangisi kelishi bilan oldingisini bekor qiladi (search); mergeMap barchasini parallel bajaradi; concatMap ketma-ket bajaradi; exhaustMap bajarilayotganda yangisini inkor etadi",
                    "mergeMap oldingisini bekor qiladi",
                    "concatMap barchasini parallel bajaradi",
                    "exhaustMap har safar yangisini chaqiradi"
                },
                "switchMap qidiruv (search) uchun ideal, chunki u eski tugallanmagan so'rovni bekor qiladi. concatMap tartibni saqlaydi, mergeMap parallel bajaradi."),

            CreateQuestion("Angular Change Detection strategiyalarida ChangeDetectionStrategy.OnPush qanday ishlaydi va uning unumdorlikka ta'siri nimada?",
                new List<string> {
                    "Komponentni har bir hodisada emas, faqat Input reference o'zgarganda, Event sodir bo'lganda yoki Signal/AsyncPipe xabar berganda qayta chizadi",
                    "Komponentni umuman qayta chizmaydi",
                    "Zone.js-ni majburiy o'chirib beradi",
                    "Faqat Standalone komponentlarda ishlaydi"
                },
                "OnPush keraksiz Change Detection tekshiruvlarini keskin kamaytiradi va faqat aniq o'zgarish bo'lganda (Immutable Input reference / Signal) render qiladi."),

            CreateQuestion("RxJS memory leak (xotira sizishi) oldini olishda takeUntilDestroyed() operatori qanday ishlaydi?",
                new List<string> {
                    "DestroyRef kontekstidan foydalanib, komponent yoki servis yo'qotilganda (destroy) Observable oqimini avtomatik yakunlaydi va unsubscribe qiladi",
                    "Observable-ni abadiy saqlab turadi",
                    "Faqat HTTP POST so'rovlarida ishlaydi",
                    "Zone.js-ni to'xtatadi"
                },
                "takeUntilDestroyed Angular 16+ da DestroyRef yordamida avtomatik unsubscribe bo'lib xotira sizishining oldini oladi."),

            CreateQuestion("Angular Dependency Injection-da ElementRef, ViewContainerRef va TemplateRef o'rtasidagi farqlar nimada?",
                new List<string> {
                    "ElementRef — DOM elementiga havola; TemplateRef — <ng-template> shabloniga havola; ViewContainerRef — dinamik komponentlarni joylashtirish konteyneri",
                    "ElementRef dinamik komponent yaratadi",
                    "TemplateRef faqat HTML fayl manzilini saqlaydi",
                    "ViewContainerRef faqat CSS stillarini o'zgartiradi"
                },
                "ElementRef nativ DOM elementini beradi. ViewContainerRef dinamik komponent va ko'rinishlarni insert/remove qilish konteyneri hisoblanadi."),

            CreateQuestion("Angular Router Functional Guards (CanActivateFn, CanDeactivateFn) qanday yoziladi va ularning Class Guard-larga nisbatan afzalligi nimada?",
                new List<string> {
                    "Sinf va NgModule muhtojliksiz oddiy funksiya sifatida yoziladi, inject() orqali servislarni oladi va kod hajmi ixcham bo'ladi",
                    "Guard-larni har doim Class sifatida yozish majburiy",
                    "Functional Guard-lar servislardan foydalana olmaydi",
                    "Faqat Angular 12 versiyasida ishlaydi"
                },
                "Functional Guard-lar Angular 15+ da joriy etilgan bo'lib boilerplate-ni kamaytiradi va inject() bilan clean code beradi."),

            CreateQuestion("Angular-da Content Projection (<ng-content>) va Multi-slot Content Projection (select atributi) qanday ishlaydi?",
                new List<string> {
                    "Ota komponentdan kelayotgan HTML bo'laklarini bola komponent template-ining tayinli joylariga (slots) joylashtirish imkonini beradi",
                    "Faqat text-larni tarjima qilish uchun ishlatiladi",
                    "Faqat CSS grid stillarini o'zgartiradi",
                    "Komponentni o'chirish uchun ishlatiladi"
                },
                "<ng-content select=\"...\"> moslashuvchan re-usable komponentlar va multi-slot layout-lar yaratish imkonini beradi."),

            CreateQuestion("Angular Signals va RxJS integratsiyasida toSignal() va toObservable() funksiyalari qachon va qanday ishlatiladi?",
                new List<string> {
                    "toSignal — RxJS Observable oqimini Signal-ga o'g'iradi; toObservable — Signal o'zgarishlarini RxJS Observable qilib uzatadi",
                    "toSignal faqat HTTP POST so'rovlarida ishlaydi",
                    "toObservable Signal qiymatini o'chirib yuboradi",
                    "Ikkala funksiya ham mutlaqo bir xil vazifa bajaradi"
                },
                "toSignal Observable-ni shablonda oson ishlatish uchun Signal-ga o'giradi. toObservable esa RxJS operatorlarini Signal reaktivligiga ulash uchun ishlatiladi."),

            CreateQuestion("Angular State Management-da Component-Store yoki NgRx Signals Store ishlatishning afzalligi nimada?",
                new List<string> {
                    "Komponent yoki feature darajasida reaktiv holatni (State), harakatlarni (Actions) va hosilaviy qiymatlarni (Selectors) Signal reaktivligida boshqaradi",
                    "Faqat backend-ga SQL yuboradi",
                    "Faqat LocalStorage faylga yozadi",
                    "Zone.js-ni o'chirib yuboradi"
                },
                "SignalStore va ComponentStore holatni markazlashgan va reaktiv boshqarish imkonini beradi."),

            CreateQuestion("Angular Hydration va Server-Side Rendering (SSR - provideClientHydration()) qanday ishlaydi?",
                new List<string> {
                    "Serverda tayyorlangan DOM strukturani klientda (brauzer) qayta noldan chizmasdan, mavjud DOM tugunlariga voqealarni (Event Listeners) ravon biriktiradi",
                    "Serverda HTML yaratilishini taqiqlaydi",
                    "Brauzer keshini har bir sekundda tozalaydi",
                    "Faqat SQLite bilan ishlaydi"
                },
                "Client Hydration serverda render bo'lgan HTML-ni brauzerda qayta yo'qotmasdan ravon biriktirib o'tadi (No DOM flickering)."),

            CreateQuestion("Angular ControlValueAccessor (CVA) interfeysi moslashtirilgan form komponenti (Custom Form Control) yaratishda nima beradi?",
                new List<string> {
                    "Custom komponentni Angular Reactive Forms (formControlName) va ngModel bilan uzviy bog'lanishiga va writeValue, registerOnChange integratsiyasiga imkon beradi",
                    "Faqat CSS rangini o'zgartiradi",
                    "Faqat HTTP header-larini qo'shadi",
                    "Formani majburiy invalid qiladi"
                },
                "ControlValueAccessor custom form input-larini standart Angular Forms API bilan to'liq integratsiya qilish imkonini beradi."),

            CreateQuestion("Angular Reactive Forms-da moslashtirilgan validatatorlar (ValidatorFn va AsyncValidatorFn) qanday yoziladi?",
                new List<string> {
                    "ValidatorFn AbstractControl qabul qilib xatolik ob'ektini ({ invalidKey: true }) yoki null qaytaradi; AsyncValidatorFn esa Observable/Promise qaytaradi",
                    "ValidatorFn har doim boolean qaytarishi shart",
                    "AsyncValidatorFn faqat SQL bazada ishlaydi",
                    "Ular faqat HTML-da yoziladi"
                },
                "ValidatorFn xatolik bo'lsa validation error object qaytaradi, to'g'ri bo'lsa null qaytaradi. AsyncValidator esa asynchronous API call bajaradi."),

            CreateQuestion("RxJS Error Handling operatorlaridan catchError va retry o'rtasidagi farq nimada?",
                new List<string> {
                    "retry xatolik yuz berganda Observable-ga qayta obuna bo'ladi (retry count); catchError esa xatolikni ushlab zaxira Observable (EMPTY yoki of()) qaytaradi",
                    "catchError so'rovni avtomatik takrorlaydi",
                    "retry xatolikni yashirib qo'yadi",
                    "Ular bir xil ishlaydi"
                },
                "retry muvaffaqiyatsiz bo'lgan HTTP so'rovlarni qayta urinadi. catchError esa xatoni ushlab xavfsiz zaxira oqim beradi."),

            CreateQuestion("Angular 15+ Directive Composition API (hostDirectives) nima beradi?",
                new List<string> {
                    "Vorislik (Inheritance) ishlatmasdan, komponent yoki direktivaga boshqa direktivalarning xulq-atvorini va Input/Output-larini kompozitsiya qilib qo'shish",
                    "Komponentlarni o'chirib yuboradi",
                    "Faqat CSS class-larini birlashtiradi",
                    "Faqat SQL query yaratadi"
                },
                "Directive Composition API `hostDirectives: [TooltipDirective]` orqali har xil xatti-harakatlarni komponentlarga oson ulash imkonini beradi."),

            CreateQuestion("RxJS obyektlarini birlashtirish operatorlaridan combineLatest va forkJoin o'rtasidagi farq nima?",
                new List<string> {
                    "combineLatest har bir manba yangi qiymat chiqarganda oxirgi qiymatlarni yuboradi; forkJoin esa barcha Observable-lar yakunlangach (complete) oxirgi natijalarni yuboradi",
                    "forkJoin faqat 1 ta Observable bilan ishlaydi",
                    "combineLatest faqat har kuni 1 marta ishlaydi",
                    "Ular o'rtasida farq yo'q"
                },
                "forkJoin masalan parallel HTTP request-lar tugashini kutadi (Promise.all kabi). combineLatest esa har bir reaktiv o'zgarishda oxirgi holatni beradi."),

            CreateQuestion("Angular-da Lazy Loaded routes uchun loadComponent va loadChildren sintaksisi o'rtasidagi farq nima?",
                new List<string> {
                    "loadComponent — bitta Standalone komponentni dangasa yuklaydi; loadChildren — marshrutlar to'plamini yoki NgModule-ni dangasa yuklaydi",
                    "loadComponent faqat CSS yuklaydi",
                    "loadChildren ilovani sekinlashtiradi",
                    "Ular bir xil sintaksis"
                },
                "loadComponent Standalone komponentlarni to'g'ridan-to'g'ri lazy-load qilish uchun ishlatiladi."),

            CreateQuestion("Angular ViewContainerRef.createComponent() metodining vazifasi nimadan iborat?",
                new List<string> {
                    "Runtime-da dinamik ravishda yangi komponent yaratish va uni DOM konteyneriga joylashtirish",
                    "Faqat HTML fayllarni o'chirish",
                    "Faqat CSS-ni o'zgardi",
                    "Faqat SQL so'rovini bajaradi"
                },
                "ViewContainerRef.createComponent dinamik modallar, popup-lar va dinamik vidjetlarni joylashtirish uchun ishlatiladi."),

            CreateQuestion("Angular Pure Pipe-lar unumdorlikni oshirishda memoization mexanizmidan qanday foydalanadi?",
                new List<string> {
                    "Agar transform(value, ...args) metodiga kelgan argumentlar o'zgarmasa, metod qayta ijro etilmay keshdagi natijani qaytaradi",
                    "Pure Pipe har sekundda qayta hisoblaydi",
                    "Pure Pipe keshni umuman saqlamaydi",
                    "U faqat string-larni o'qiydi"
                },
                "Pure Pipe kiruvchi ko me me me'rsatkichlar teng bo'lsa (reference equality) o'tgan hisob-kitob keshini beradi."),

            CreateQuestion("RxJS Subject turlaridan BehaviorSubject va ReplaySubject o'rtasidagi farq nima?",
                new List<string> {
                    "BehaviorSubject dastlabki qiymat talab qiladi va faqat oxirgi 1 ta qiymatni saqlaydi; ReplaySubject esa ko'rsatilgan sondagi (bufferSize) o'tgan qiymatlarni qayta eshittiradi",
                    "BehaviorSubject hech qachon qiymat saqlamaydi",
                    "ReplaySubject faqat HTTP so'rovlarda ishlaydi",
                    "Ular bir xil Subject"
                },
                "BehaviorSubject.getValue() orqali joriy holatni o'qish mumkin. ReplaySubject(N) esa yangi obunachiga o'tgan N ta xabarni qayta beradi."),

            CreateQuestion("RxJS shareReplay({ bufferSize: 1, refCount: true }) operatori HTTP so'rovlarida nima beradi?",
                new List<string> {
                    "Bir nechta obunachilar bo'lganda HTTP so'rovini qayta-qayta yubormay, natijani keshlab barcha obunachilarga bitta javobni tarqatadi (Multicasting)",
                    "HTTP so'rovini o'chirib yuboradi",
                    "Faqat LocalStorage ga yozadi",
                    "Zone.js-ni to'xtatadi"
                },
                "shareReplay HTTP so'rovi natijasini keshlaydi va ko'plab async pipe-lar bir xil so'rovni qayta yuborishini oldini oladi.")
        };
    }

    private static List<Question> GenerateAngularHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion("Zone-less Angular (provideExperimentalZonelessChangeDetection()) da Change Detection qanday ishlaydi va Zone.js-dan voz kechishning afzalligi nimada?",
                new List<string> {
                    "Zone.js monkey-patching overhead-ini yo'qotadi; Faqat Signal-lar va eksplitsit bildirishnomalar (markForCheck) orqali to'g'ridan-to'g'ri faqat o'zgargan DOM tugunlarini o'ta tez yangilaydi",
                    "Zone.js-ni majburiy qayta yuklaydi",
                    "Change detection-ni umuman ishlamaydigan qiladi",
                    "Faqat Internet Explorer brauzerida ishlaydi"
                },
                "Zoneless Angular Zone.js siz ishlaydi, brauzer native API-larini monkey-patch qilmaydi va Signal reaktivligi bilan o'ta yuqori tezlik beradi."),

            CreateQuestion("Angular Ivy Compiler va Element Instructions (Advanced DOM architecture) qanday ishlaydi?",
                new List<string> {
                    "HTML shablonlarni ixcham va Tree-shakeable bo'lgan incremental DOM instruksiyalariga (JS funksiyalariga) kompilyatsiya qiladi",
                    "HTML-ni serverda rasmga o'g'iradi",
                    "Faqat CSS stillarini o'zgartiradi",
                    "Faqat SQL query yaratadi"
                },
                "Ivy Compiler shablonlarni ɵɵelementStart kabi ko'rinishda JS instruksiyalariga o'giradi, bu xotirani tejaydi va tree-shaking ta'minlaydi."),

            CreateQuestion("Angular-da untracked() funksiyasi Signal effect() yoki computed() ichida qachon ishlatiladi?",
                new List<string> {
                    "Signal qiymatini o'qiganda ushbu Signal-ning reaktiv zanjirga (dependency tracking) kirib qolishini va effekti qayta ishga tushirishini oldini olish uchun",
                    "Signal qiymatini o'chirib tashlash uchun",
                    "Faqat HTTP so'rovlarini to'xtatish uchun",
                    "Faqat Form-larni tozalash uchun"
                },
                "untracked() effekti ichida ma'lumot o'qilganda u ushbu signal o'zgarganda qayta trigger bo'lishining oldini oladi."),

            CreateQuestion("Angular Custom Structural Directive (Directive with TemplateRef and ViewContainerRef) yaratish va micro-syntax parsing qanday ishlaydi?",
                new List<string> {
                    "TemplateRef orqali HTML bo'lagini oladi va ViewContainerRef yordamida uni kerakli shart va takrorlanishlar bo'yicha dinamik DOM-ga kiritadi yoki o'chiradi",
                    "Faqat CSS rangini o'zgartiradi",
                    "Faqat HTTP header-larini qo'shadi",
                    "Faqat SQLite-ga saqlaydi"
                },
                "Struktura direktivalari (*appRepeat) TemplateRef va ViewContainerRef orqali DOM strukturasini dinamik boshqaradi."),

            CreateQuestion("Angular-da Hybrid Rendering va Partial Hydration (@defer (hydrate on ...) - Angular 18+) qanday ishlaydi?",
                new List<string> {
                    "Serverda render bo'lgan statik HTML-ni saqlab turadi va faqat kerakli bo'lak interaktiv bo'lganda (masalan scroll qilganda) uning JS kodi va hydration-ini yuklaydi",
                    "Faqat HTML-ni o'chirib beradi",
                    "Faqat CSS-ni yuklaydi",
                    "Faqat SQL serverda ishlaydi"
                },
                "Partial Hydration Angular 18+ da saqlangan HTML-ning faqat kerakli komponent qismlarini zarurat bo'lganda JS yuklab hydrate qiladi."),

            CreateQuestion("Angular Dependency Injection-da Host, Self, SkipSelf va Optional parameter decorator-lari di-resolution daraxtini qanday boshqaradi?",
                new List<string> {
                    "@Self — faqat o'z komponentida qidiradi; @SkipSelf — ota injector-dan boshlaydi; @Host — shadow DOM / host-gacha qidiradi; @Optional — topilmasa null beradi",
                    "@Self faqat root injector-dan qidiradi",
                    "@SkipSelf izlashni to'xtatadi",
                    "Ikkala decorator bir xil ishlaydi"
                },
                "Ushbu decorator-lar Angular DI konteyneriga bog'liqlikni (dependency) aniq qaysi darajadagi injector-dan qidirish lozimligini ko'rsatadi."),

            CreateQuestion("Angular Directives-da HostBinding va HostListener o'rniga yangi @Directive({ host: { ... } }) ob'ekti ishlatilishining afzalligi nimada?",
                new List<string> {
                    "Host xususiyatlari va hodisalarini alohida decorator-larsiz bitta ixcham ob'ektda ko'rsatadi hamda Signal inputs bilan a'lo integratsiya beradi",
                    "HostBinding fayllarni shifrlaydi",
                    "HostListener-ni taqiqlaydi",
                    "Faqat RxJS bilan ishlaydi"
                },
                "Component/Directive metadata ichida host: { ... } ishlatish koddagi ortiqcha decorator-larni kamaytiradi va toza yozilish ta'minlaydi."),

            CreateQuestion("Angular Micro-Frontend Architecture (Module Federation va Dynamic Remote Component Loading) qanday bajariladi?",
                new List<string> {
                    "Webpack/Rspack Module Federation orqali turli alohida qurilgan (build) Angular ilovalarni runtime-da bitta Shell ilovaga dinamik yuklab birlashtiradi",
                    "Faqat bitta katta bundle fayl hosil qiladi",
                    "Faqat CSS fayllarni import qiladi",
                    "Faqat SQL serverda ishlaydi"
                },
                "Module Federation mikro-frontend arxitekturasida dinamik ravishda boshqa ilovalardagi remote komponent va modullarni yuklash imkonini beradi."),

            CreateQuestion("Angular-da Custom RxJS Operator yaratish va pipe() operator zanjiri unumdorligini oshirish qanday amalga oshiriladi?",
                new List<string> {
                    "Mavjud RxJS operatorlarini birlashtirib yoki yangi Observable oqimini Subscriber orqali qayta yozib maxsus reaktiv operator hosil qilish",
                    "Faqat array-ni sort qiladi",
                    "Faqat string-ni o'chiradi",
                    "Faqat SQL so'rov beradi"
                },
                "Custom RxJS operatorlari takrorlanuvchi reaktiv mantiqlarni bitta toza va qayta ishlatiluvchi operatorga jamlash imkonini beradi."),

            CreateQuestion("Angular CD (Change Detection) profiling va Performance Debugging bo'yicha Angular DevTools Profiler qanday ma'lumot beradi?",
                new List<string> {
                    "Har bir Change Detection sikli qancha vaqt olganini, qaysi komponentlar qayta render bo'lganini va bunga nima sabab bo'lganini (trigger) aniq grafikda ko'rsatadi",
                    "Faqat backend SQL vaqtini ko'rsatadi",
                    "Faqat brauzer RAM hajmini ko'rsatadi",
                    "Faqat CSS xatolarini beradi"
                },
                "Angular DevTools Profiler Change Detection vaqtida qaysi komponentlar render bo'layotgani va unumdorlik muammolarini (bottlenecks) ko'rsatadi."),

            CreateQuestion("Angular Compiler AOT (Ahead-of-Time) va JIT (Just-in-Time) kompilyatsiya rejimlari o'rtasidagi asosiy farq nimada?",
                new List<string> {
                    "AOT shablonlarni build vaqtida JavaScript-ga kompilyatsiya qiladi (tezroq va xavfsiz); JIT esa brauzer ichida runtime-da kompilyatsiya qiladi",
                    "JIT har doim AOT-dan tezroq ishlaydi",
                    "AOT faqat development muhitida ishlaydi",
                    "Ular bir xil kompilyator"
                },
                "AOT build vaqtida shablonlarni tayyor koda o me me me'giradi. Bu brauzerda compiler bundle hajmini tejaydi va tezkor start beradi."),

            CreateQuestion("Angular Signal custom equality funksiyasi (signal(val, { equal: customEqualFn })) qaysi maqsadda ishlatiladi?",
                new List<string> {
                    "Signal qiymatiga yangi obyekt berilganda, u mantiqan o'zgarganini (deep equality) aniq baholash va keraksiz notification-larni to'xtatish uchun",
                    "Signal-ni har doim invalid qilish uchun",
                    "Faqat string-larni shifrlash uchun",
                    "Signal-ni o me'chirish uchun"
                },
                "Signal custom equal funksiyasi o me'zgarish sodir bo'lganini moslashtirilgan mantiq bo me'yicha tekshirishga imkon beradi."),

            CreateQuestion("RxJS Backpressure oqimlarini boshqaruvchi debounceTime va throttleTime operatorlari o'rtasidagi farq nima?",
                new List<string> {
                    "debounceTime so'rovlar oqimi to'xtagandan keyin ko'rsatilgan vaqt o'tgach oxirgi qiymatni yuboradi; throttleTime esa ko'rsatilgan oralikda faqat birinchi qiymatni beradi",
                    "debounceTime har bir millisoniyada yuboradi",
                    "throttleTime faqat HTTP POST so'rovda ishlaydi",
                    "Ular bir xil operator"
                },
                "debounceTime masalan foydalanuvchi yozishdan to'xtaganda (search input) ishlatiladi. throttleTime esa tugma ko'p bosilishida ma'lum intervalda 1 marta beradi."),

            CreateQuestion("Angular-da Memory Leak manbalarini (Detached DOM Nodes, Unsubscribed Observables) brauzer Memory Snapshot orqali qanday aniqlanadi?",
                new List<string> {
                    "Brauzer DevTools Memory panelida Heap Snapshot olib, komponenetsiz qolgan Detached HTMLDivElement va yo'qotilmagan Subscriber-larni solishtirish orqali",
                    "Faqat Network oynasini ko'rish orqali",
                    "Faqat Console log o'qish orqali",
                    "Memory leak-ni aniqlab bo'lmaydi"
                },
                "Heap Snapshot xotirada qolib ketgan komponentlar (Detached DOM nodes) va yopilmagan RxJS subscription-larni topishga yordam beradi."),

            CreateQuestion("Angular TransferState API (makeStateKey, TransferState) SSR va Hydration jarayonida nima uchun o'ta muhim?",
                new List<string> {
                    "Serverda bajarilgan HTTP so'rov natijasini HTML metadata-ga keshlab uzatadi, brauzerda qayta ikkinchi marta takroriy HTTP so'rov urilishining oldini oladi",
                    "TransferState faqat CSS-ni saqlaydi",
                    "TransferState HTML-ni o'chiradi",
                    "U faqat SQL Server bilan ishlaydi"
                },
                "TransferState serverda olingan HTTP ma'lumotlarni HTML saqlab klientga beradi va takroriy (duplicate) HTTP so'rovlarini yo'qotadi."),

            CreateQuestion("Angular Directive Composition API-da input va output-larni re-aliasing qilish sintaksisi qanday?",
                new List<string> {
                    "hostDirectives: [{ directive: TooltipDirective, inputs: ['tooltipText: appTooltip'], outputs: ['tooltipShow: appShow'] }]",
                    "hostDirectives: [TooltipDirective.alias()]",
                    "directives: [TooltipDirective]",
                    "aliasDirectives: [TooltipDirective]"
                },
                "Directive Composition API `inputs: ['internalName: publicName']` orqali ichki direktiva inputlarini tashqi komponent nomiga re-alias qilish imkonini beradi."),

            CreateQuestion("Angular Unit Testing-da fakeAsync va tick() yordamida asinxron vaqt oqimi qanday sinovdan o'tkaziladi?",
                new List<string> {
                    "fakeAsync vaqt oqimini sinxron simulyatsiya qiladi va tick(1000) orqali vaqtni 1 soniya oldinga surib setTimeout/timer test qilinadi",
                    "fakeAsync so'rovlarni o'chirib yuboradi",
                    "tick() faqat SQL so'rov beradi",
                    "fakeAsync har doim sekinroq ishlaydi"
                },
                "fakeAsync zonasi ichida `tick(ms)` yordamida taymer va asinxron kechikishlarni lahzada test qilish imkoniyati beriladi."),

            CreateQuestion("Angular-da Service Worker Dynamic Caching strategiyalarida Freshness (Network First) va Performance (Cache First) o'rtasidagi farq nima?",
                new List<string> {
                    "Freshness — avval tarmoqdan (Network) eng yangi ma'lumotni olishga urinadi, bo'lmasa keshga o me me'tadi; Performance — avval keshdan tezkor berib fonda tarmoqqa o'tadi",
                    "Freshness faqat rasm fayllari uchun",
                    "Performance keshni har sekundda o me me'chiradi",
                    "Ular o'rtasida farq yo'q"
                },
                "Freshness doimiy yangi ma'lumot (masalan balans) uchun, Performance esa o'zgarmas asset-lar uchun mos keladi."),

            CreateQuestion("Angular Web Workers (ng generate web-worker) ishlatishning asosiy unumdorlik afzalligi nimada?",
                new List<string> {
                    "Og'ir hisob-kitoblarni (Heavy computation) brauzerning asosiy UI Thread-idan alohida Background Thread-ga o me me me'tkazib, UI qotib qolishini (60fps lag) oldini oladi",
                    "Web Workers faqat HTML fayllarni keshlaydi",
                    "Web Workers faqat SQL query bajaradi",
                    "Web Workers UI thread-ni to'xtatadi"
                },
                "Web Workers og'ir ma'lumotlarni qayta ishlashni alohida thread-ga yuklab, brauzer UI rendering-ining ravonligini (60 FPS) ta'minlaydi."),

            CreateQuestion("Angular-da DomSanitizer va bypassSecurityTrustHtml qachon ishlatiladi va uning XSS xatari nimada?",
                new List<string> {
                    "Tashqaridan kelgan HTML matnni xavfsiz deb belgilaydi; Noto'g me me me'ri ishlatilsa foydalanuvchi brauzerida ziddiyatli script-lar (XSS Attack) ijro etilishi xavfi paydo bo'ladi",
                    "DomSanitizer fayllarni diskka shifrlaydi",
                    "bypassSecurityTrustHtml HTML-ni o me me me'chiradi",
                    "U faqat CSS uchun ishlaydi"
                },
                "DomSanitizer Angular-ning xavfsizlik filtri hisoblanadi. `bypassSecurityTrustHtml` ehtiyotsiz ishlatilsa XSS zaifligiga olib keladi.")
        };
    }
}
