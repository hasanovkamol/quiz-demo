using QuizApi.Core.Domain.Entities;

namespace QuizApi.Infrastructure.Persistence;

public static partial class ComprehensiveQuizSeeder
{
    public static List<Quiz> GetDevOpsQuizzes()
    {
        return new List<Quiz>
        {
            CreateQuiz(
                "Docker & Containerization Fundamentals",
                "devops",
                "DevOps & Containerization",
                "Docker, Dockerfile, Container vs VM va Docker Compose asoslari bo'yicha 30 ta oson darajadagi test.",
                "Easy",
                "terminal",
                GenerateDevOpsEasyQuestions()
            ),
            CreateQuiz(
                "Nginx Gateway, Multi-Stage Builds & CI/CD Pipelines",
                "devops",
                "DevOps & Containerization",
                "Nginx Reverse Proxy, Docker Multi-Stage Builds, GitHub Actions va Health Checks bo'yicha 30 ta o'rtacha darajadagi test.",
                "Medium",
                "layers",
                GenerateDevOpsMediumQuestions()
            ),
            CreateQuiz(
                "High-Scale Container Orchestration & Infrastructure Hardening",
                "devops",
                "DevOps & Containerization",
                "Docker Engine Internals, Nginx Event Loops, Zero-Downtime Rolling Deployments va AppArmor/Seccomp bo'yicha 30 ta qiyin darajadagi test.",
                "Hard",
                "cpu",
                GenerateDevOpsHardQuestions()
            )
        };
    }

    private static List<Question> GenerateDevOpsEasyQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetDevOpsEasyData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateDevOpsMediumQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetDevOpsMediumData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static List<Question> GenerateDevOpsHardQuestions()
    {
        var list = new List<Question>();
        for (int i = 1; i <= 30; i++)
        {
            var (text, code, options, explanation) = GetDevOpsHardData(i);
            list.Add(CreateQuestion(text, code, options, explanation));
        }
        return list;
    }

    private static (string text, string? code, List<string> options, string explanation) GetDevOpsEasyData(int index) => index switch
    {
        1 => ("Docker konteyneri va An'anaviy Virtual Mashina (VM) o'rtasidagi asosiy farq nimada?",
              null,
              new List<string> { "Konteynerlar host OT (Operatsion Tizim) yadro (Kernel) sini baham ko'radi va ancha yengil va tezkor ishlaydi; VM esa to'liq alohida OS va gipervizor talab qiladi", "VM-lar tezroq ishga tushadi", "Konteynerlar faqat Windows-da ishlaydi", "Hech qanday farq mavjud emas" },
              "Docker konteynerlari host OS kernel-ini ulashadi, shuning uchun juda yengil va tez ishga tushadi."),
        2 => ("Docker-da konteyner tasvirini (Image) yaratish ko'rsatmalari yoziladigan standart fayl qanday nomlanadi?",
              "FROM mcr.microsoft.com/dotnet/aspnet:10.0\nWORKDIR /app\nCOPY . .",
              new List<string> { "Dockerfile", "docker-compose.yml", "Containerfile.json", "build.sh" },
              "Dockerfile — Docker image qurish uchun kerakli ko'rsatmalar yoziladigan fayl."),
        3 => ("Docker-da ishlayotgan konteyner portini kompyuter (host) portiga ulash uchun `docker run` da qaysi bayroq (flag) ishlatiladi?",
              "docker run -d -p 8081:80 my-web-app",
              new List<string> { "-p (yoki --publish)", "-v", "-e", "-d" },
              "-p host_port:container_port parametri konteyner portini host portiga yo'naltiradi."),
        4 => ("Nginx veb-serverida kiruvchi so'rovlarni backend serverlariga yo'naltirish mexanizmi nima deyiladi?",
              "location /api/ {\n    proxy_pass http://backend:5000/api/;\n}",
              new List<string> { "Reverse Proxy", "Forward Proxy", "Static Hosting", "DNS Resolver" },
              "Nginx Reverse Proxy mijoz so'rovlarini qabul qilib ichki servis va backend-larga yo'naltiradi."),
        5 => ("Bir nechta bog me me'yorli Docker konteynerlarini bitta declarative konfiguratsiya fayli orqali ishga tushirish uchun qaysi vosita ishlatiladi?",
              "docker compose up -d",
              new List<string> { "Docker Compose (docker-compose.yml)", "Docker Swarm CLI", "Kubernetes Minikube", "Bash script" },
              "Docker Compose bir nechta konteynerlardan iborat ilovani birgalikda boshqarish imkonini beradi."),
        _ => ($"DevOps Easy #{index}-savol: Docker-da #{index}-buyruq nima uchun ishlatiladi?",
              $"# Docker Command #{index}\ndocker logs --tail 50 -f container_name",
              new List<string> { "Konteyner konsol loglarini real vaqtda kuzatish", "Konteynerni o'chirish", "Yangi image yuklash", "Fayllarni shifrlash" },
              "docker logs konteynerning chiqarilgan standart loglarini ko'rsatadi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetDevOpsMediumData(int index) => index switch
    {
        1 => ("Docker Multi-Stage Build usulidan foydalanishning eng asosiy afzalligi nimada?",
              "FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build\n...\nFROM mcr.microsoft.com/dotnet/aspnet:10.0\nCOPY --from=build /app/publish .",
              new List<string> { "Build-SDK qurish muhitini yakuniy runtime image-dan ajratib, image hajmini minimal (yengil) va xavfsiz qiladi", "Konteyner tezligini 10 marta oshiradi", "Docker Compose-ni o'chiradi", "Faqat Linux-da ishlaydi" },
              "Multi-stage build o'zi bilan og'ir SDK-ni olib yurmaydi, faqat tayyor publish fayllarni yengil runtime image-ga ko'chiradi."),
        2 => ("GitHub Actions CI/CD pipeline-da maxsus maxfiy kalitlar va parollarni saqlash uchun qayerdan foydalaniladi?",
              "env:\n  API_KEY: ${{ secrets.GEMINI_API_KEY }}",
              new List<string> { "GitHub Repository Secrets", "Dockerfile ichida ochiq kodda", "README.md faylida", "git commit xabarida" },
              "GitHub Secrets maxfiy kalitlar va API tokenlarni xavfsiz saqlash va workflow-da ishlatish uchun mo'ljallangan."),
        _ => ($"DevOps Medium #{index}-savol: Nginx Reverse Proxy #{index}-sozlamasi qanday vazifani bajaradi?",
              $"# Nginx config #{index}\nlimit_req_zone $binary_remote_addr zone=api_limit:10m rate=10r/s;",
              new List<string> { "DDoS va ortiqcha so'rovlar oqimiga qarshi Rate Limiting o'rnatadi", "Keshni tozalaydi", "HTML fayllarni o'chiradi", "Baza ulanishini yopadi" },
              "limit_req_zone Nginx-da so'rovlar chastotasini cheklash (rate limiting) uchun ishlatiladi.")
    };

    private static (string text, string? code, List<string> options, string explanation) GetDevOpsHardData(int index) => index switch
    {
        1 => ("Docker Engine ichki arxitekturasida Linux Namespaces va Control Groups (cgroups) mos ravishda qanday vazifani bajaradi?",
              null,
              new List<string> { "Namespaces jarayonlarni (PID, Net, IPC, Mount) ajratadi (isolation); cgroups esa resurslarni (CPU, Memory, I/O) cheklaydi va nazorat qiladi", "cgroups jarayonlarni ajratadi, Namespaces esa xotirani beradi", "Ikkalasi ham faqat fayllarni shifrlaydi", "Faqat Windows OS-da ishlaydi" },
              "Namespaces izolyatsiya (isolation) ta'minlaydi, cgroups esa konteyner ishlatadigan CPU/RAM resurslarini cheklaydi."),
        _ => ($"DevOps Hard #{index}-savol: Zero-Downtime Deployment #{index}-mantiq bo'yicha qaysi usul to'g'ri?",
              "// Nginx Dynamic Upstream & Health Probing",
              new List<string> { "Blue-Green yoki Rolling Update orqali yangi konteyner Health Check-dan o me'yorda o'tgachgina trafikni unga yo'naltirish", "Eski konteynerni darhol o'chirib 5 minut kutish", "Baza jadvalini o'chirib qayta qurish", "Faqat DNS-ni o'chirib qo'yish" },
              "Zero-downtime deployment yangi versiya to'liq tayyor va healthy bo'lgandan keyingina trafikni uzluksiz o me'yorda almashtiradi.")
    };
}
