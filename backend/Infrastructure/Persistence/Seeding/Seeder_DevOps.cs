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
                "Docker, Dockerfile, Container vs VM va Docker Compose asoslari bo'yicha professional savollar.",
                "Easy",
                "terminal",
                GenerateDevOpsEasyQuestions()
            ),
            CreateQuiz(
                "Nginx Gateway, Multi-Stage Builds & CI/CD Pipelines",
                "devops",
                "DevOps & Containerization",
                "Nginx Reverse Proxy, Docker Multi-Stage Builds, GitHub Actions va Health Checks bo'yicha senior savollar.",
                "Medium",
                "layers",
                GenerateDevOpsMediumQuestions()
            ),
            CreateQuiz(
                "High-Scale Container Orchestration & Infrastructure Hardening",
                "devops",
                "DevOps & Containerization",
                "Docker Engine Internals, Nginx Event Loops, Zero-Downtime Rolling Deployments va AppArmor/Seccomp bo'yicha principal savollar.",
                "Hard",
                "cpu",
                GenerateDevOpsHardQuestions()
            )
        };
    }

    private static List<Question> GenerateDevOpsEasyQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "Docker konteyneri va an'anaviy Virtual Mashina (VM) o'rtasidagi asosiy farqlar nimada?",
                "Docker Container (Shares Host OS Kernel) vs Virtual Machine (Hypervisor + Full Guest OS)",
                new List<string> {
                    "Konteynerlar host OT (Operatsion Tizim) yadro (Kernel) sini baham ko'radi va ancha yengil va tezkor ishlaydi; VM esa to'liq alohida OS va gipervizor talab qiladi",
                    "VM-lar tezroq ishga tushadi",
                    "Konteynerlar faqat Windows-da ishlaydi",
                    "Hech qanday farq mavjud emas"
                },
                "Docker konteynerlari host OS kernel-ini ulashadi, shuning uchun juda yengil va soniyalarda tez ishga tushadi."
            ),
            CreateQuestion(
                "Docker-da konteyner tasvirini (Image) yaratish ko'rsatmalari yoziladigan standart fayl va uning asosiy instruktsiyalari qanday?",
                "FROM mcr.microsoft.com/dotnet/aspnet:10.0\nWORKDIR /app\nCOPY . .\nENTRYPOINT [\"dotnet\", \"QuizApi.dll\"]",
                new List<string> {
                    "Dockerfile — FROM (asosiy image), WORKDIR (ishchi papka), COPY (fayllarni ko'chirish), RUN (buyruq bajarish) va ENTRYPOINT (ishga tushirish)",
                    "docker-compose.yml — faqat portlarni o'zgarmas saqlaydi",
                    "build.sh — faqat Linux skriptini saqlaydi",
                    "Containerfile.json — faqat JSON sozlamalarini saqlaydi"
                },
                "Dockerfile — Docker image qurish uchun bosqichma-bosqich ko'rsatmalar yoziladigan standart deklarativ fayl."
            ),
            CreateQuestion(
                "Docker-da konteyner portini kompyuter (host) portiga ulash (Port Forwarding) uchun `docker run` da qaysi bayroq ishlatiladi?",
                "docker run -d -p 8081:80 --name my-quiz-app qiuz-backend",
                new List<string> {
                    "-p (host_port:container_port)",
                    "-v (host_path:container_path)",
                    "-e (KEY=VALUE)",
                    "-d (detached mode)"
                },
                "-p host_port:container_port parametri konteyner ichki portini tashqi host kompyuter portiga yo'naltiradi."
            ),
            CreateQuestion(
                "Nginx veb-serverida kiruvchi so'rovlarni backend serverlariga yo'naltirish (Reverse Proxy) mexanizmi va sozlamasi qanday?",
                "location /api/ {\n    proxy_pass http://backend:5000/api/;\n    proxy_set_header Host $host;\n}",
                new List<string> {
                    "proxy_pass ko'rsatmasi orqali so'rovlarni ichki backend konteynerlari yoki IP manzillariga xavfsiz o'tkazib beradi",
                    "proxy_pass fayllarni o'chirib beradi",
                    "location faqat HTML fayllarni keshlaydi",
                    "Nginx faqat static fayllar uchun, proxy yo'naltirolmaydi"
                },
                "Nginx Reverse Proxy mijoz so'rovlarini qabul qilib ichki servis va backend-larga yo'naltiradi."
            ),
            CreateQuestion(
                "Bir nechta bog'liq Docker konteynerlarini (masalan Backend, Database, Redis, UI) bitta konfiguratsiya fayli orqali boshqarish vositasi qaysi?",
                "docker compose up -d --build",
                new List<string> {
                    "Docker Compose (`docker-compose.yml`)",
                    "Docker Swarm CLI",
                    "Kubernetes Minikube",
                    "Bash script"
                },
                "Docker Compose bir nechta konteynerlardan iborat ilovani birgalikda declarative boshqarish imkonini beradi."
            ),
            CreateQuestion(
                "Docker-da `Docker Volume` va `Bind Mount` o'rtasidagi asosiy farq nimada va ma'lumotlarni saqlashda qaysi biri tavsiya etiladi?",
                "volumes:\n  pgdata:\n    driver: local",
                new List<string> {
                    "Docker Volume — Docker tomonidan to'liq boshqariladigan va izolyatsiyalangan xavfsiz saqlash joyi; Bind Mount — host fayl tizimidagi aniq papka",
                    "Bind Mount har doim Volume-ga qaraganda xavfsizroq",
                    "Volume faqat RAM xotirada saqlaydi",
                    "Ikkala usul ham bir xil ishlaydi"
                },
                "Docker Volume ma'lumotlar bazasi va doimiy ma'lumotlarni (persistent data) konteyner qayta tushganda yo'qotmaslik uchun Docker tomonidan boshqariladigan eng yaxshi usul."
            ),
            CreateQuestion(
                "Docker Image Layer Caching (Qatlamli keshlar) mexanizmi qanday ishlaydi va Dockerfile-ni tezkor qurish uchun buyruqlar tartibi qanday bo'lishi kerak?",
                "COPY *.csproj ./\nRUN dotnet restore\nCOPY . .",
                new List<string> {
                    "Kam o'zgaradigan fayllar (masalan csproj va restore) yuqorida joylashishi lozim; Docker o'zgarmagan qatlamlarni keshdan olib build-ni o'ta tez bajaradi",
                    "Har bir build-da barcha qatlamlar noldan qayta yuklanadi",
                    "COPY . . har doim birinchi qatorda turishi kerak",
                    "Keshlar build tezligiga ta'sir qilmaydi"
                },
                "Docker har bir instruktsiyani qatlam (layer) sifatida keshlaydi. Tez-tez o'zgarmaydigan csproj va restore yuqorida tursa kesh samara beradi."
            ),
            CreateQuestion(
                "Docker konteyner holatini (status, logs, stop) tekshirish uchun CLI-da qaysi standart buyruqlar ishlatiladi?",
                "docker ps\ndocker logs -f <container_id>\ndocker stop <container_id>",
                new List<string> {
                    "docker ps (ro'yxat), docker logs (loglar), docker stop (to'xtatish)",
                    "docker run (to'xtatish), docker build (loglar)",
                    "docker inspect (o'chirish)",
                    "docker exec (statistika)"
                },
                "docker ps faol konteynerlarni ko'rsatadi, docker logs uning loglarini beradi, docker stop esa uni tartibli to'xtatadi."
            ),
            CreateQuestion(
                "Docker-da `.dockerignore` faylining asosiy vazifasi va foydasi nimada?",
                "node_modules\nbin/\nobj/\n.git",
                new List<string> {
                    "Keraksiz mahalliy fayllarni (bin, obj, node_modules, .git) Docker build context-ga ko'chirmaydi va build hajmi hamda tezligini oshiradi",
                    "Dockerfile instruktsiyalarini o'chirib yuboradi",
                    "Konteyner portlarini o'chiradi",
                    "Faqat Windows-da ishlaydi"
                },
                ".dockerignore kerakmas og'ir fayllarni Docker daemon-ga yuborilishini tosad va build vaqtini keskin qisqartiradi."
            ),
            CreateQuestion(
                "Nginx-da Static File Hosting (`root` va `try_files`) Single Page Application (SPA - Angular) ilovalarda routing xatolarini (404) qanday hal qiladi?",
                "location / {\n    root /usr/share/nginx/html;\n    try_files $uri $uri/ /index.html;\n}",
                new List<string> {
                    "try_files so'ralgan fayl topilmasa, so'rovni Angular SPA kirish nuqtasi bo'lmish `index.html` ga yo me me'yirtiradi",
                    "try_files 404 xatosini majburlaydi",
                    "root ko'rsatmasi fayllarni o'chiradi",
                    "SPA ilovalarda try_files ishlatib bo'lmaydi"
                },
                "try_files $uri $uri/ /index.html Angular client-side routing uchun barcha yo'nalishlarni index.html ga xavfsiz yo'naltiradi."
            )
        };
    }

    private static List<Question> GenerateDevOpsMediumQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "Docker Multi-Stage Build usulidan foydalanishning eng asosiy afzalligi nimada?",
                "FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build\nWORKDIR /src\n...\nFROM mcr.microsoft.com/dotnet/aspnet:10.0\nCOPY --from=build /app/publish .",
                new List<string> {
                    "Build-SDK qurish muhitini yakuniy runtime image-dan ajratib, image hajmini minimal (yengil) va xavfsiz qiladi",
                    "Konteyner tezligini 10 marta oshiradi",
                    "Docker Compose-ni o'chiradi",
                    "Faqat Linux-da ishlaydi"
                },
                "Multi-stage build o'zi bilan og'ir SDK-ni olib yurmaydi, faqat tayyor publish fayllarni yengil runtime image-ga ko'chiradi."
            ),
            CreateQuestion(
                "Docker-da Container Health Checks (`HEALTHCHECK` instruktsiyasi) va `docker-compose` healthcheck qanday ishlaydi?",
                "healthcheck:\n  test: [\"CMD\", \"curl\", \"-f\", \"http://localhost:5000/api/health\"]\n  interval: 10s\n  retries: 3",
                new List<string> {
                    "Konteyner ichidagi servisning haqiqatan tayyor va sog'lom (Healthy) ekanligini davriy vaqt oralig'ida tekshirib boradi",
                    "Konteynerni har 10 soniyada qayta tushiradi",
                    "Faqat RAM hajmini tozalaydi",
                    "Healthcheck faqat Windows-da bo'ladi"
                },
                "HEALTHCHECK konteyner faqat 'running' emas, balki ichidagi servis haqiqatan so'rov qabul qila olishini (Healthy) tekshiradi."
            ),
            CreateQuestion(
                "GitHub Actions CI/CD pipeline-da Secrets management va Environment variables qanday xavfsiz ishlatiladi?",
                "steps:\n  - name: Build & Push Docker Image\n    env:\n      DOCKER_PASSWORD: ${{ secrets.DOCKER_PASSWORD }}",
                new List<string> {
                    "GitHub Repository Secrets bo'limida maxfiy token va parollar shifrlangan saqlanadi va pipeline-da `secrets.KEY` orqali xavfsiz ishlatiladi",
                    "Parollarni Dockerfile ichida ochiq matn ko'rinishida yozish",
                    "Parollarni README.md ga qo me me'yish",
                    "Secrets har bir build-da o'chib ketadi"
                },
                "GitHub Secrets maxfiy kalitlar va tokenlarni ochiq kodga chiqarmasdan pipeline-da xavfsiz ishlatish imkonini beradi."
            ),
            CreateQuestion(
                "Nginx Rate Limiting (`limit_req_zone` va `limit_req`) DDoS va Brute-Force hujumlaridan qanday himoya qiladi?",
                "limit_req_zone $binary_remote_addr zone=mylimit:10m rate=10r/s;\nlocation /api/login {\n    limit_req zone=mylimit burst=5 nodelay;\n}",
                new List<string> {
                    "Mijoz IP manzili bo'yicha sekundiga ruxsat berilgan so'rovlar sonini cheklaydi va me me'yordan oshganini 429 Too Many Requests bilan qaytaradi",
                    "Faqat static fayllar keshini tozalaydi",
                    "IP manzilni abadiy o'chirib yuboradi",
                    "Nginx-ni qayta ishga tushiradi"
                },
                "limit_req_zone IP bo'yicha so'rovlar chastotasini cheklaydi va burst/nodelay orqali silliqlaydi."
            ),
            CreateQuestion(
                "Docker Network turlari (`bridge`, `host`, `overlay`, `none`) o'rtasidagi asosiy farqlar nimada?",
                "networks:\n  quiz_net:\n    driver: bridge",
                new List<string> {
                    "bridge — bitta host-dagi izolyatsiya tarmoq; host — host tarmoq interfeysidan to'g'ridan-to'g'ri foydalanish; overlay — ko'p hostli Swarm/K8s tarmog'i",
                    "host faqat NoSQL bazalarda ishlaydi",
                    "bridge konteynerlar muloqotini taqiqlaydi",
                    "Ikkala tarmoq turi ham bir xil"
                },
                "bridge bitta kompyuter ichidagi konteynerlar muloqoti uchun standart izolyatsiyalangan virtual tarmoqdir."
            ),
            CreateQuestion(
                "CI/CD Pipeline-da Automated Testing va Quality Gate (SonarQube) bosqichlarining o'rni nimada?",
                "steps:\n  - run: npm run test\n  - run: dotnet test",
                new List<string> {
                    "Koddagi testlar yoki sifat ko'rsatkichlari (coverage, security vulnerabilities) o'tmasa, avtomatik ravishda build-ni to'xtatib nosoz kod production-ga o'tishini tosad",
                    "Testlar muvaffaqiyatsiz bo'lsa ham deploy qiladi",
                    "Faqat fayllarni shifrlaydi",
                    "Quality Gate faqat HTML formatlaydi"
                },
                "Quality Gate CI/CD pipeline-da sifat standartlari bajarilmasa jarayonni to'xtatib (fail fast) xavfsizlik va sifatni ta'minlaydi."
            ),
            CreateQuestion(
                "Nginx Gzip / Brotli Compression sozlamasining Veb unumdorligiga ta'siri nimada?",
                "gzip on;\ngzip_types text/plain text/css application/json application/javascript;",
                new List<string> {
                    "Matnli resurslarni (HTML, JS, CSS, JSON) tarmoq orqali uzatishdan oldin siqadi va fayl hajmini 70% gacha kamaytirib yuklanishni tezlashtiradi",
                    "Faqat rasmlarni siqadi",
                    "Server RAM-ini 10 marta to me'ldiradi",
                    "Gzip o'chirilishi tavsiya etiladi"
                },
                "Gzip va Brotli matnli fayllar hajmini sezilarli siqib tarmoq trafigini tejaydi va sahifa yuklanish tezligini oshiradi."
            ),
            CreateQuestion(
                "Docker Exec (`docker exec -it <container> bash`) buyrug'i nima uchun ishlatiladi?",
                "docker exec -it quiz_aspnet_backend curl http://localhost:5000/api/health",
                new List<string> {
                    "Ishlab turgan konteyner ichiga kirib buyruqlar va diagnostika skriptlarini muloqot rejimida (interactive shell) bajarish uchun",
                    "Yangi image yaratish uchun",
                    "Konteynerni o me me me'chirish uchun",
                    "Docker daemon-ni qayta tushirish uchun"
                },
                "docker exec ishlab turgan konteyner ichida diagnostika va debugging buyruqlarini bajarish imkonini beradi."
            ),
            CreateQuestion(
                "Docker Compose-da `depends_on` va `condition: service_healthy` sozlamasi qanday muammoni hal qiladi?",
                "depends_on:\n  postgres:\n    condition: service_healthy",
                new List<string> {
                    "Backend konteynerini ma'lumotlar bazasi konteyneri nafaqat start bo'lishini, balki u to'liq tayyor (Healthy) bo'lgandan keyingina ishga tushirishni kafolatlaydi",
                    "Backend konteynerini birinchi ishga tushiradi",
                    "Database-ni o'chirib yuboradi",
                    "Faqat 1 ta konteynerga ruxsat beradi"
                },
                "depends_on with condition: service_healthy backend DB tayyor bo'lmasdan oldin tushib 500 error berishining oldini oladi."
            ),
            CreateQuestion(
                "Nginx SSL/TLS Termination (HTTPS) qanday bajariladi va uning backend servislarga ta'siri nimada?",
                "server {\n    listen 443 ssl;\n    ssl_certificate /etc/nginx/certs/site.crt;\n    ssl_certificate_key /etc/nginx/certs/site.key;\n}",
                new List<string> {
                    "HTTPS shifrlash va sertifikatlarni Nginx darajasida yechib (Decrypt), ichki backend konteynerlariga oddiy yengil HTTP sifatida uzatadi",
                    "Backend-ni ham shifrlashga majburlaydi",
                    "SSL sertifikatni o'chirib tashlaydi",
                    "Faqat HTTP 1.0 ni qo me me'llaydi"
                },
                "SSL Termination Nginx-da HTTPS-ni yechib backend-ga yengil HTTP beradi, bu backend CPU resurslarini tejaydi."
            )
        };
    }

    private static List<Question> GenerateDevOpsHardQuestions()
    {
        return new List<Question>
        {
            CreateQuestion(
                "Docker Engine ichki arxitekturasida Linux Namespaces va Control Groups (cgroups) mos ravishda qanday vazifani bajaradi?",
                "// Linux Namespaces (PID, NET, IPC, MNT, UTS) & Control Groups (CPU, Memory, I/O limits)",
                new List<string> {
                    "Namespaces jarayonlarni (PID, Net, IPC, Mount) ajratadi (isolation); cgroups esa resurslarni (CPU, Memory, I/O) cheklaydi va nazorat qiladi",
                    "cgroups jarayonlarni ajratadi, Namespaces esa xotirani beradi",
                    "Ikkalasi ham faqat fayllarni shifrlaydi",
                    "Faqat Windows OS-da ishlaydi"
                },
                "Namespaces izolyatsiya (isolation) ta'minlaydi, cgroups esa konteyner ishlatadigan CPU/RAM resurslarini cheklaydi."
            ),
            CreateQuestion(
                "Nginx Event-driven Non-blocking Event Loop arxitekturasi va Worker Processes, Epoll (Linux) qanday ishlaydi?",
                "events {\n    worker_connections 1024;\n    use epoll;\n}",
                new List<string> {
                    "Har bir so'rov uchun alohida thread yaratmasdan, bitta worker process epoll I/O multiplexing orqali o me'n minglab so'rovlarni asinxron va 0-allocation bilan boshqaradi",
                    "Har bir so'rovga 1 ta alohida heavy OS thread beradi",
                    "Faqat Apache server kabi ishlaydi",
                    "Faqat 1 ta so'rovni qabul qiladi"
                },
                "Nginx epoll event loop yordamida thread-per-request modelisiz o'n minglab parallel ulanishlarni o'ta kam RAM bilan boshqaradi."
            ),
            CreateQuestion(
                "Docker Container Security Hardening: Non-root User, Read-only Root Filesystem va Capability Dropping (`--cap-drop=ALL`) nima beradi?",
                "USER appuser\n--read-only --cap-drop=ALL --cap-add=NET_BIND_SERVICE",
                new List<string> {
                    "Konteyner ichidan buzib kirilganda host tizimga va ildiz (root) huquqlariga ega bo'lishining va zararli fayllar yozilishining oldini oladi",
                    "Konteynerni 10 marta sekinlashtiradi",
                    "Faqat fayllarni o'chirish uchun",
                    "Faqat Windows-da ishlaydi"
                },
                "Non-root user va Read-only filesystem konteyner xavfsizligini oshirib, izolyatsiyani buzib kirish (container escape) xavfini minimale qiladi."
            ),
            CreateQuestion(
                "Zero-Downtime Rolling Deployment strategiyasida Nginx Dynamic Upstream reload (`nginx -s reload`) va Graceful Shutdown qanday bajariladi?",
                "// Nginx Master process sends SIGHUP to workers -> Old workers finish existing connections then terminate",
                new List<string> {
                    "Nginx master jarayoni yangi worker-larni ishga tushiradi; Eski worker-lar mavjud so'rovlarni tugatgachgina (Graceful) yopiladi, bitta ham so'rov uzilmaydi",
                    "Nginx serverni darhol o'chirib 5 minut kutadi",
                    "Barcha ulanishlarni uzib tashlaydi",
                    "Faqat DNS-ni o'chiradi"
                },
                "nginx -s reload master process yordamida so'rovlarni birortasini ham uzmasdan yangi konfiguratsiya va worker-larga ravon o me me me'tkazadi."
            ),
            CreateQuestion(
                "Docker OverlayFS2 Storage Driver (LowerDir, UpperDir, MergedDir, WorkDir) va Copy-on-Write (CoW) qanday ishlaydi?",
                "// MergedDir = LowerDir (Read-only Image Layers) + UpperDir (Writable Container Layer)",
                new List<string> {
                    "Image qatlamlari (LowerDir) faqat o'qish uchun; Konteyner o'zgarishlari UpperDir-ga yoziladi (CoW); MergedDir ularni birga ko'rsatadi",
                    "OverlayFS2 barcha fayllarni o'chirib beradi",
                    "UpperDir faqat RAM-da bo'ladi",
                    "OverlayFS2 faqat Windows-da ishlaydi"
                },
                "OverlayFS2 qatlamlarni ustma-ust taxlaydi. O'zgarish sodir bo'lganda Copy-on-Write orqali fayl UpperDir container qatlamiga ko'chiriladi."
            ),
            CreateQuestion(
                "Container Orchestration (Kubernetes / Docker Swarm) da Ingress Controller va Service Mesh (Istio / Linkerd) farqi nima?",
                "// Ingress = North-South traffic (External to Cluster)\n// Service Mesh = East-West traffic (Microservice to Microservice mTLS)",
                new List<string> {
                    "Ingress Controller — tashqaridan klasterga keluvchi trafig (North-South); Service Mesh — klaster ichidagi mikroservislar muloqoti va mTLS shifrlash (East-West)",
                    "Service Mesh faqat static fayllar uchun",
                    "Ingress Controller mikroservislarni o'chiradi",
                    "Ikkala vosita bir xil"
                },
                "Ingress tashqi so'rovlarni yo'naltiradi (North-South). Service Mesh esa ichki mikroservislar o'rtasida mTLS va observabillity beradi (East-West)."
            ),
            CreateQuestion(
                "Nginx Keep-Alive Timeout va HTTP/2 Multiplexing so'rovlar samaradorligida qanday ishlaydi?",
                "http2 on;\nkeepalive_timeout 65;",
                new List<string> {
                    "HTTP/2 bitta TCP ulanish orqali bir vaqtda yuzlab so'rovlarni (Multiplexing) parallel uzatadi va TCP handshake xarajatini 0 ga tushiradi",
                    "HTTP/2 har bir so'rovga alohida TCP ulanish ochadi",
                    "Keep-Alive so'rovlarni sekinlashtiradi",
                    "HTTP/2 faqat HTTP 1.0 bilan ishlaydi"
                },
                "HTTP/2 Multiplexing bitta TCP ulanish ustida ko'plab so'rovlarni parallel uzatib Head-of-Line blocking muammosini hal qiladi."
            ),
            CreateQuestion(
                "Linux Seccomp (Secure Computing Mode) va AppArmor profiles Docker konteynerlarida qanday xavfsizlik beradi?",
                "docker run --security-opt seccomp=default.json --security-opt apparmor=docker-default",
                new List<string> {
                    "Konteyner bajarishi mumkin bo'lgan Linux System Call-larni (syscalls) va fayl tizimi huquqlarini apparat/kernel darajasida taqiqlaydi va izolyatsiya qiladi",
                    "Faqat portlarni yopadi",
                    "Faqat IP manzilni o me me me'zgartiradi",
                    "Seccomp faqat Windows-da ishlaydi"
                },
                "Seccomp va AppArmor kernel darajasida taqiqlangan syscalls-ni tosad va konteyner xavfsizligini maksimal (hardening) ta me'minlaydi."
            ),
            CreateQuestion(
                "Infrastructure as Code (IaC) vositalaridan Terraform va Ansible o'rtasidagi asosiy farq nimada?",
                "// Terraform (Declarative Provisioning: VMs, Cloud Resources) vs Ansible (Procedural/Declarative Configuration Management)",
                new List<string> {
                    "Terraform — asosan bulut va infratuzilmani yaratish (Infrastructure Provisioning); Ansible — serverlar va konteynerlar sozlamasini o'rnatish (Configuration Management)",
                    "Ansible faqat bulut resurslarini yaratadi",
                    "Terraform faqat Linux skriptlarini yozadi",
                    "Ikkala vosita ham bir xil"
                },
                "Terraform infratuzilma (VM, VPC, K8s) yaratish uchun. Ansible esa yaratilgan serverlarga dasturlar va sozlamalarni o'rnatish uchun mo'ljallangan."
            ),
            CreateQuestion(
                "CI/CD Pipeline Canary Deployment va Automated Rollback (Prometheus Metrics query) qanday avtomatlashtiriladi?",
                "// Query Prometheus 5xx error rate > 1% -> Trigger automated git rollback!",
                new List<string> {
                    "Canary versiyasida xatolik foizi (5xx rate yoki latency) oshsa, monitoring tizimi (Prometheus) avtomatik pipeline-ga signal berib roll-back bajaradi",
                    "Rollback faqat qo'lda Visual Studio-dan bajariladi",
                    "Prometheus barcha serverlarni o me me me'chiradi",
                    "Canary rollback bajara olmaydi"
                },
                "Prometheus va CI/CD integratsiyasi real metric-lar yomonlashganda inson aralashuvisiz avtomatik Rollback qilish imkonini beradi."
            )
        };
    }
}
