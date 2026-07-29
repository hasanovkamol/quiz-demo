import { Component, input, output, ElementRef, ViewChild, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CertificateData } from '../../models/quiz.model';

@Component({
  selector: 'app-certificate-modal',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="fixed inset-0 z-50 bg-slate-950/85 backdrop-blur-md flex items-center justify-center p-4 overflow-y-auto">
      <div class="glass-card rounded-3xl max-w-3xl w-full p-6 sm:p-8 border border-slate-800 my-8 shadow-2xl relative">
        
        <!-- Modal Header -->
        <div class="flex items-center justify-between border-b border-slate-800 pb-4 mb-6">
          <div class="flex items-center gap-3">
            <div class="w-10 h-10 rounded-xl bg-amber-500/10 border border-amber-500/20 text-amber-400 flex items-center justify-center font-bold">
              🎓
            </div>
            <div>
              <h2 class="text-xl font-extrabold text-white">Sertifikat</h2>
              <p class="text-xs text-slate-400">QuizMaster PRO rasmiy kasbiy sertifikati</p>
            </div>
          </div>

          <button 
            (click)="closeModal.emit()"
            class="p-2 rounded-xl text-slate-400 hover:text-white hover:bg-slate-800 transition">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
            </svg>
          </button>
        </div>

        <!-- Certificate Container (White Theme, ~14px Professional Font) -->
        <div #certContainer class="relative bg-white text-slate-900 rounded-2xl p-8 sm:p-12 shadow-2xl border-4 border-amber-500/80 font-sans select-none overflow-hidden my-4">
          
          <!-- Outer Gold Geometry Lines -->
          <div class="absolute inset-2 border-2 border-amber-400/40 rounded-xl pointer-events-none"></div>
          <div class="absolute top-4 left-4 w-12 h-12 border-t-2 border-l-2 border-amber-500"></div>
          <div class="absolute top-4 right-4 w-12 h-12 border-t-2 border-r-2 border-amber-500"></div>
          <div class="absolute bottom-4 left-4 w-12 h-12 border-b-2 border-l-2 border-amber-500"></div>
          <div class="absolute bottom-4 right-4 w-12 h-12 border-b-2 border-r-2 border-amber-500"></div>

          <!-- Header Logo & Title -->
          <div class="text-center mb-6">
            <div class="inline-flex items-center gap-2 mb-2">
              <span class="w-8 h-8 rounded-lg bg-amber-500 text-white font-extrabold flex items-center justify-center text-sm shadow">QM</span>
              <span class="text-xs font-black tracking-widest text-slate-700 uppercase">QuizMaster PRO IT Platform</span>
            </div>
            <h1 class="text-2xl sm:text-3xl font-serif font-black tracking-wider text-slate-900 uppercase mt-1">
              CERTIFICATE OF ACHIEVEMENT
            </h1>
            <div class="w-24 h-1 bg-amber-500 mx-auto mt-2 rounded-full"></div>
          </div>

          <!-- Presentation Text -->
          <div class="text-center my-6">
            <p class="text-sm font-medium text-slate-600 mb-2 italic">Ushbu sertifikat munosib ravishda topshiriladi:</p>
            <h2 class="text-2xl sm:text-4xl font-extrabold text-slate-900 tracking-tight my-2 border-b-2 border-slate-200 pb-2 inline-block px-6">
              {{ certificate()?.userName || 'Kamol Hasanov' }}
            </h2>
            
            <p class="text-sm text-slate-700 leading-relaxed max-w-xl mx-auto mt-4 font-normal">
              Senior <strong class="text-amber-700 font-semibold">{{ certificate()?.categoryName || 'ASP.NET Core & Software Architecture' }}</strong> 
              yo'nalishi bo'yicha nazariy va amaliy bilimlarni a'lo darajada topshirganligi uchun:
            </p>

            <!-- Score & Rating -->
            <div class="inline-flex items-center gap-3 bg-amber-50 border border-amber-200 px-4 py-2 rounded-full my-4">
              <span class="text-sm font-bold text-amber-900">Natija: {{ certificate()?.scorePercentage || 95 }}%</span>
              <span class="text-amber-500 tracking-widest">
                {{ getStarsStr(certificate()?.starsCount || 5) }}
              </span>
              <span class="text-xs font-semibold px-2 py-0.5 rounded bg-amber-200 text-amber-900">
                {{ certificate()?.badgeTitle || 'Senior Certified Architect' }}
              </span>
            </div>
          </div>

          <!-- Footer Metadata & Signature -->
          <div class="grid grid-cols-2 items-end pt-6 border-t border-slate-200 mt-8 text-xs text-slate-600">
            <div>
              <p class="font-medium">Berilgan sana: <strong class="text-slate-800">{{ certificate()?.issuedAt || '29.07.2026' }}</strong></p>
              <p class="font-medium mt-1">Sertifikat ID: <strong class="text-slate-800 font-mono">{{ certificate()?.certificateCode || 'CERT-QM-9876-2026' }}</strong></p>
            </div>
            
            <div class="text-right">
              <div class="inline-block border-b border-slate-400 pb-1 mb-1 italic font-serif text-slate-800 text-sm font-bold">
                John D. Smith
              </div>
              <p class="text-[11px] text-slate-500 font-medium">QuizMaster PRO Certification Board</p>
            </div>
          </div>

        </div>

        <!-- Action Buttons -->
        <div class="flex flex-wrap items-center justify-between gap-3 border-t border-slate-800 pt-4 mt-6">
          <button 
            (click)="copyShareLink()"
            class="px-4 py-2.5 rounded-xl text-xs font-bold text-slate-300 bg-slate-900 border border-slate-800 hover:bg-slate-800 transition flex items-center gap-2">
            <span>🔗 Havolani nusxalash</span>
            @if (copied()) {
              <span class="text-emerald-400 text-[11px]">(Nusxalandi!)</span>
            }
          </button>

          <div class="flex items-center gap-2">
            <button 
              (click)="printCertificate()"
              class="px-5 py-2.5 rounded-xl text-xs font-bold text-slate-900 bg-amber-400 hover:bg-amber-300 transition shadow-lg flex items-center gap-2">
              <span>📥 Yuklab olish / Bosib chiqarish (PDF)</span>
            </button>

            <button 
              (click)="closeModal.emit()"
              class="px-5 py-2.5 rounded-xl text-xs font-bold text-slate-300 bg-slate-900 border border-slate-800 hover:bg-slate-800 transition">
              Yopish
            </button>
          </div>
        </div>

      </div>
    </div>
  `
})
export class CertificateModalComponent {
  readonly certificate = input<CertificateData | null>(null);
  readonly closeModal = output<void>();
  
  @ViewChild('certContainer') certContainer!: ElementRef<HTMLDivElement>;

  readonly copied = signal<boolean>(false);

  getStarsStr(count: number): string {
    return '⭐'.repeat(Math.max(1, Math.min(5, count)));
  }

  printCertificate(): void {
    const element = this.certContainer.nativeElement;
    const printWindow = window.open('', '_blank');
    if (!printWindow) return;

    printWindow.document.write(`
      <html>
        <head>
          <title>QuizMaster PRO Certificate - ${this.certificate()?.userName || 'Developer'}</title>
          <script src="https://cdn.tailwindcss.com"></script>
          <style>
            @media print {
              body { margin: 0; padding: 0; background: white; }
              @page { size: landscape; margin: 0; }
            }
          </style>
        </head>
        <body class="bg-white flex items-center justify-center p-8 min-h-screen">
          <div style="width: 100%; max-width: 900px;">
            ${element.innerHTML}
          </div>
        </body>
      </html>
    `);
    printWindow.document.close();
    setTimeout(() => {
      printWindow.print();
    }, 500);
  }

  copyShareLink(): void {
    const certCode = this.certificate()?.certificateCode || 'CERT-QM-DEMO-2026';
    const shareUrl = `${window.location.origin}/?certCode=${certCode}`;
    navigator.clipboard.writeText(shareUrl).then(() => {
      this.copied.set(true);
      setTimeout(() => this.copied.set(false), 2500);
    });
  }
}
