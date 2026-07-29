import { Quiz } from '../models/quiz.model';

export const SAMPLE_QUIZZES: Quiz[] = [
  {
    id: 'quiz-coding-sandbox',
    title: 'TypeScript Algorithms & Live Code Sandbox',
    category: 'custom',
    categoryName: 'Dasturlash va Sandbox',
    description: 'Jonli kod redaktorida berilgan algoritmik masalalarni yozing va "Kodni Tekshirish" tugmasini bosib tekshiring.',
    iconName: 'terminal',
    difficulty: 'O\'rta',
    timeLimitSeconds: 300,
    questions: [
      {
        id: 'q-code-1',
        text: 'Ikki sonning yig\'indisini hisoblaydigan funksiyani konsolga chiqaring (`console.log(add(5, 10))` -> 15).',
        isCodeQuestion: true,
        initialCodeTemplate: `function add(a, b) {\n  return a + b;\n}\n\nconsole.log(add(5, 10));`,
        expectedOutput: '15',
        options: [],
        correctOptionId: 'code-correct',
        explanation: 'Konsolga `15` chiqaradigan to\'g\'ri funksiya kiritildi.'
      },
      {
        id: 'q-code-2',
        text: 'Massivdagi juft sonlarni filter qilib konsolga chiqaruvchi kod yozing.',
        isCodeQuestion: true,
        initialCodeTemplate: `const numbers = [1, 2, 3, 4, 5, 6];\nconst evens = numbers.filter(n => n % 2 === 0);\nconsole.log(evens.join(','));`,
        expectedOutput: '2,4,6',
        options: [],
        correctOptionId: 'code-correct',
        explanation: 'Massiv juft sonlari `2,4,6` chiqarildi.'
      }
    ]
  },
  {
    id: 'quiz-angular-1',
    title: 'Angular 18+ & Signals Mastery',
    category: 'angular',
    categoryName: 'Angular Framework',
    description: 'Angular 18+, Signals, Standalone komponentlar va yangi Control Flow (`@if`, `@for`) bo\'yicha bilimlaringizni sinang.',
    iconName: 'code-2',
    difficulty: 'O\'rta',
    timeLimitSeconds: 300,
    questions: [
      {
        id: 'q-ng-1',
        text: 'Angular 16+ versiyasida kiritilgan Signal reaktiv modelida o\'zgaruvchini yangilash uchun qaysi metod ishlatiladi?',
        codeSnippet: `const count = signal(0);\n// Qiymatga 1 ni qo'shish uchun qaysi biridan foydalaniladi?`,
        options: [
          { id: 'opt-1', text: 'count.set(count() + 1)' },
          { id: 'opt-2', text: 'count.update(val => val + 1)' },
          { id: 'opt-3', text: 'count.mutate(val => val + 1)' },
          { id: 'opt-4', text: 'Yuqoridagi 1 va 2 javoblarning ikkalasi ham to\'g\'ri' }
        ],
        correctOptionId: 'opt-4',
        explanation: 'Signal qiymatini o\'zgartirish uchun ham `set()`, ham `update()` metodidan foydalanish mumkin. `update()` avvalgi qiymatga asoslanib yangilashda qulayroqdir.'
      },
      {
        id: 'q-ng-2',
        text: 'Angular ning yangi Control Flow sintaksisida sikllar uchun qaysi direktiva o\'rniga yangi sintaksis ishlatiladi?',
        codeSnippet: `@for (item of items(); track item.id) {\n  <div>{{ item.name }}</div>\n}`,
        options: [
          { id: 'opt-1', text: '*ngFor yerine @for ishlatiladi va `track` atributi majburiydir' },
          { id: 'opt-2', text: '*ngFor o\'rniga @loop ishlatiladi' },
          { id: 'opt-3', text: '*ngRepeat o\'rniga @for ishlatiladi' },
          { id: 'opt-4', text: '@for sintaksisida track kalit so\'zidan foydalanish shart emas' }
        ],
        correctOptionId: 'opt-1',
        explanation: 'Angular 17+ Control Flow da `*ngFor` o\'rniga `@for` ishlatiladi hamda unumdorlik uchun `track` ko\'rsatkichidan foydalanish majburiy etib belgilangan.'
      },
      {
        id: 'q-ng-3',
        text: 'Boshqa Signal lar qiymatiga bog\'liq holda avtomatik hisoblanadigan va faqat o\'qish uchun mo\'ljallangan Signal qanday yaratiladi?',
        codeSnippet: `const firstName = signal('John');\nconst lastName = signal('Doe');\nconst fullName = ???;`,
        options: [
          { id: 'opt-1', text: 'computed(() => `${firstName()} ${lastName()}`)' },
          { id: 'opt-2', text: 'effect(() => `${firstName()} ${lastName()}`)' },
          { id: 'opt-3', text: 'signal.read(() => `${firstName()} ${lastName()}`)' },
          { id: 'opt-4', text: 'derived(() => `${firstName()} ${lastName()}`)' }
        ],
        correctOptionId: 'opt-1',
        explanation: '`computed()` funksiyasi boshqa Signal lardan xosil bo\'ladigan (derived state) faqat o\'qiladigan Signal yaratadi va u keshlanadi.'
      },
      {
        id: 'q-ng-4',
        text: 'Angular da Xotira to\'lib qolishi (Memory leak) ni oldini olish uchun RxJS Observable lardan to\'g\'ri unsubscribe qilishning zamonaviy usuli qaysi?',
        options: [
          { id: 'opt-1', text: 'takeUntilDestroyed() operatoridan foydalanish' },
          { id: 'opt-2', text: 'ngOnDestroy da qo\'lda .unsubscribe() chaqirish' },
          { id: 'opt-3', text: 'HTML shablonida AsyncPipe (| async) ishlatish' },
          { id: 'opt-4', text: 'Barcha javoblar to\'g\'ri' }
        ],
        correctOptionId: 'opt-4',
        explanation: 'TakeUntilDestroyed, AsyncPipe va ngOnDestroy da unsubscribe qilish barchasi to\'g\'ri usullar hisoblanadi. Eng zamonaviysi `takeUntilDestroyed()` va AsyncPipe dir.'
      },
      {
        id: 'q-ng-5',
        text: 'Standalone komponentlarda boshqa komponent yoki modullarni ishlatish uchun ular qayerda e\'lon qilinadi?',
        codeSnippet: `@Component({\n  selector: 'app-user',\n  standalone: true,\n  imports: [???],\n  templateUrl: './user.component.html'\n})`,
        options: [
          { id: 'opt-1', text: 'AppModule ichida declarations massivida' },
          { id: 'opt-2', text: '@Component dekoratori ichidagi `imports` massivida' },
          { id: 'opt-3', text: 'main.ts faylida bootstrapApplication funksiyasida' },
          { id: 'opt-4', text: 'Angular Standalone komponentlarda boshqa komponent ishlatib bo\'lmaydi' }
        ],
        correctOptionId: 'opt-2',
        explanation: 'Standalone komponentlar NgModule ga muhtoj emas. Kerakli komponentlar, direktivalar va modullar bevosita `@Component({ imports: [...] })` massivida ko\'rsatiladi.'
      }
    ]
  },
  {
    id: 'quiz-dotnet-1',
    title: '.NET 8/9 & C# Senior Architecture',
    category: 'dotnet',
    categoryName: 'C# & .NET Core',
    description: 'C# 12/13, EF Core, LINQ optimization, Async/Await va SOLID prinsiplari bo\'yicha bilim darajangizni sinang.',
    iconName: 'cpu',
    difficulty: 'Qiyin',
    timeLimitSeconds: 360,
    questions: [
      {
        id: 'q-cs-1',
        text: 'Entity Framework Core da `N+1` muammosini oldini olish uchun bog\'liq ob\'yektlarni birinchi so\'rovning o\'zidayoq yuklab olish qanday amalga oshiriladi?',
        codeSnippet: `var orders = await context.Orders\n    .???(o => o.OrderItems)\n    .ToListAsync();`,
        options: [
          { id: 'opt-1', text: '.Include(o => o.OrderItems)' },
          { id: 'opt-2', text: '.Join(o => o.OrderItems)' },
          { id: 'opt-3', text: '.Load(o => o.OrderItems)' },
          { id: 'opt-4', text: '.Attach(o => o.OrderItems)' }
        ],
        correctOptionId: 'opt-1',
        explanation: 'EF Core da Eager Loading uchun `.Include()` metodi ishlatiladi va u so\'rovda SQL JOIN ishlatib N+1 muammosini hal qiladi.'
      },
      {
        id: 'q-cs-2',
        text: 'C# 12 da kiritilgan Primary Constructors imkoniyati klasslar uchun qanday yoziladi?',
        codeSnippet: `public class UserService(IUserRepository userRepo, ILogger<UserService> logger)\n{\n    // ...\n}`,
        options: [
          { id: 'opt-1', text: 'Konstruktor parametri to\'g\'ridan-to\'g\'ri klass nomi yonidagi qavs ichida e\'lon qilinadi' },
          { id: 'opt-2', text: 'Primary constructor faqat struct lar uchun mavjud' },
          { id: 'opt-3', text: 'Faqat record lar uchun qo\'llaniladi' },
          { id: 'opt-4', text: 'C# da bunday sintaksis yo\'q' }
        ],
        correctOptionId: 'opt-1',
        explanation: 'C# 12 dan boshlab oddiy klasslar ham Primary Constructor ni qo\'llab-quvvatlaydi, bu constructor boilerplate kodini sezilarli darajada kamaytiradi.'
      },
      {
        id: 'q-cs-3',
        text: 'EF Core da ma\'lumotlarni faqat o\'qish (read-only) uchun SQL so\'rov yuborilganda Change Tracker ni o\'chirib unumdorlikni oshirish uchun qaysi metod ishlatiladi?',
        options: [
          { id: 'opt-1', text: '.AsNoTracking()' },
          { id: 'opt-2', text: '.DisableTracking()' },
          { id: 'opt-3', text: '.ReadOnly()' },
          { id: 'opt-4', text: '.WithoutCache()' }
        ],
        correctOptionId: 'opt-1',
        explanation: '`.AsNoTracking()` LINQ so\'rovi natijalarini Entity Framework Change Tracker xotirasida saqlamaydi, bu xotira va tezlikni sezilarli oshiradi.'
      },
      {
        id: 'q-cs-4',
        text: 'C# asinxron dasturlashda `Task.Run` va `async/await` bo\'yicha qaysi qoida to\'g\'ri?',
        options: [
          { id: 'opt-1', text: 'I/O amallar (fayl, DB, API) uchun Task.Run ishlatish shart emas, asl async API larni ishlatish kerak' },
          { id: 'opt-2', text: 'Hamma metodlar oldiga Task.Run qo\'yish kerak' },
          { id: 'opt-3', text: 'async void faqat UI hodisalari (event handler) uchun ishlatilishi kerak, boshqa joyda taqiqlanadi' },
          { id: 'opt-4', text: '1 va 3-javoblar to\'g\'ri' }
        ],
        correctOptionId: 'opt-4',
        explanation: 'I/O amallar uchun thread pool ni band qiluvchi Task.Run ishlatilmasligi kerak, va `async void` exceptions ushlay olmagani uchun faqat event handler larda ishlatilishi lozim.'
      },
      {
        id: 'q-cs-5',
        text: 'SOLID prinsiplaridan "Dependency Inversion Principle" (DIP) nimani talab qiladi?',
        options: [
          { id: 'opt-1', text: 'Yuqori darajali modullar quyi darajali modullarga bog\'lanmasligi, ikkalasi ham abstraktsiyaga (interfeysga) bog\'lanishi kerak' },
          { id: 'opt-2', text: 'Har bir klass faqat bitta mas\'uliyatga ega bo\'lishi kerak' },
          { id: 'opt-3', text: 'Klasslar kengaytirish uchun ochoq, o\'zgartirish uchun yopiq bo\'lishi kerak' },
          { id: 'opt-4', text: 'Interfeyslar iloji boricha kichik va maxsus bo\'lishi kerak' }
        ],
        correctOptionId: 'opt-1',
        explanation: 'DIP ga ko\'ra konkret klasslarga emas, balki abstraktsiyalarga (interface/abstract class) bog\'lanish kerak.'
      }
    ]
  },
  {
    id: 'quiz-webdev-1',
    title: 'Full-Stack Web Development Fundamentals',
    category: 'webdev',
    categoryName: 'Web Infrastructure & Performance',
    description: 'REST API design, Web Security (CORS, JWT, XSS), Browser Rendering va Modern CSS bo\'yicha savollar.',
    iconName: 'globe',
    difficulty: 'O\'rta',
    timeLimitSeconds: 240,
    questions: [
      {
        id: 'q-web-1',
        text: 'REST API arxitekturasida ma\'lumotni qisman o\'zgartirish (partial update) uchun qaysi HTTP metodi ishlatiladi?',
        options: [
          { id: 'opt-1', text: 'PATCH' },
          { id: 'opt-2', text: 'PUT' },
          { id: 'opt-3', text: 'POST' },
          { id: 'opt-4', text: 'UPDATE' }
        ],
        correctOptionId: 'opt-1',
        explanation: 'PUT butun resursni almashtirish uchun, PATCH esa faqat belgilangan maydonlarni qisman yangilash uchun ishlatiladi.'
      },
      {
        id: 'q-web-2',
        text: 'XSS (Cross-Site Scripting) hujumlaridan himoyalanish uchun qaysi chora eng samarali hisoblanadi?',
        options: [
          { id: 'opt-1', text: 'Foydalanuvchi kiritgan ma\'lumotlarni sanitizatsiya va HTML-escape qilish' },
          { id: 'opt-2', text: 'Faqat HTTPS ishlatish' },
          { id: 'opt-3', text: 'Cookie fayllarida SameSite=Strict qo\'yish' },
          { id: 'opt-4', text: 'CORS ni o\'chirib qo\'yish' }
        ],
        correctOptionId: 'opt-1',
        explanation: 'XSS foydalanuvchi brauzerida ziyonli JavaScript ijro etilishining oldini olish uchun kiritilgan ma\'lumotlarni escape/sanitise qilishni talab qiladi.'
      },
      {
        id: 'q-web-3',
        text: 'CORS (Cross-Origin Resource Sharing) nima va u qayerda tekshiriladi?',
        options: [
          { id: 'opt-1', text: 'Bu brauzer tomonidan xavfsizlik uchun boshqa domendan keladigan so\'rovlarni nazorat qiluvchi mexanizm' },
          { id: 'opt-2', text: 'Bu ma\'lumotlar bazasi xavfsizligini ta\'minlovchi protokol' },
          { id: 'opt-3', text: 'Bu faqat Android ilovalarda ishlaydigan tarmoq sozlamasi' },
          { id: 'opt-4', text: 'Bu Node.js ning ichki paketi' }
        ],
        correctOptionId: 'opt-1',
        explanation: 'CORS brauzerlar darajasida ishlaydi va bir domendagi frontend boshqa domendagi backend resurslariga kirishini havfsiz boshqaradi.'
      }
    ]
  }
];
