import { Component, inject, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizService } from '../../services/quiz.service';
import { QuizApiService } from '../../services/quiz-api.service';
import { CertificateModalComponent } from '../certificate-modal/certificate-modal.component';
import { CertificateData } from '../../models/quiz.model';

@Component({
  selector: 'app-history-modal',
  standalone: true,
  imports: [CommonModule, CertificateModalComponent],
  template: `
    <div class="fixed inset-0 z-50 bg-slate-950/80 backdrop-blur-md flex items-center justify-center p-4 overflow-y-auto">
      <div class="glass-card rounded-3xl max-w-3xl w-full p-6 sm:p-8 border border-slate-800 my-8 shadow-2xl relative">
        
        <!-- Modal Header -->
        <div class="flex items-center justify-between border-b border-slate-800 pb-4 mb-6">
          <div class="flex items-center gap-3">
            <div class="w-10 h-10 rounded-xl bg-indigo-500/10 border border-indigo-500/20 text-indigo-400 flex items-center justify-center font-bold">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
              </svg>
            </div>
            <div>
              <h2 class="text-xl font-extrabold text-white">Natijalar Tarixi & Sertifikatlar</h2>
              <p class="text-xs text-slate-400">Oldingi topshirilgan testlar ko'rsatkichlari, yulduzchalar va qayta yechish</p>
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

        <!-- History List -->
        <div class="space-y-3 max-h-[60vh] overflow-y-auto pr-1">
          @for (item of quizService.quizHistory(); track item.id) {
            <div class="p-4 rounded-2xl bg-slate-900/80 border border-slate-800 flex flex-col sm:flex-row sm:items-center justify-between gap-4">
              <div>
                <div class="flex items-center gap-2 mb-1">
                  <span class="text-[10px] uppercase font-bold text-indigo-400 px-2 py-0.5 rounded bg-indigo-500/10 border border-indigo-500/20">
                    {{ item.categoryName }}
                  </span>
                  <span class="text-xs text-slate-500">{{ item.completedAt }}</span>
                </div>
                <h4 class="text-sm font-bold text-white">{{ item.quizTitle }}</h4>
                <p class="text-xs text-slate-400 mt-0.5">
                  {{ item.correctAnswersCount }} / {{ item.totalQuestions }} to'g'ri ({{ Math.floor(item.totalTimeSpentSeconds / 60) }} daq {{ item.totalTimeSpentSeconds % 60 }} son)
                </p>
              </div>

              <div class="flex items-center justify-between sm:justify-end gap-3 pt-2 sm:pt-0 border-t sm:border-0 border-slate-800/60">
                <!-- Stars & Percentage Badge -->
                <div class="flex items-center gap-2">
                  <span class="text-xs text-amber-400 font-bold">
                    {{ getStarsStr(item.scorePercentage) }}
                  </span>

                  <span [class]="item.scorePercentage >= 70 ? 
                    'px-3 py-1 rounded-full text-xs font-extrabold bg-emerald-500/10 text-emerald-400 border border-emerald-500/20' : 
                    'px-3 py-1 rounded-full text-xs font-extrabold bg-rose-500/10 text-rose-400 border border-rose-500/20'">
                    {{ item.scorePercentage }}%
                  </span>
                </div>

                <!-- Action Buttons: Retake & Certificate -->
                <div class="flex items-center gap-2">
                  @if (item.scorePercentage >= 70) {
                    <button 
                      (click)="openCertificate(item)"
                      class="px-2.5 py-1.5 rounded-xl text-xs font-bold text-amber-300 bg-amber-500/15 border border-amber-500/30 hover:bg-amber-500/25 transition">
                      🎓 Sertifikat
                    </button>
                  }

                  <button 
                    (click)="retakeQuiz(item.quizId)"
                    class="px-3 py-1.5 rounded-xl text-xs font-bold text-white bg-indigo-600 hover:bg-indigo-500 transition shadow-sm">
                    🔄 Qayta yechish
                  </button>
                </div>
              </div>

            </div>
          } @empty {
            <div class="text-center py-8 text-slate-500 text-xs">
              Hali hech qanday test topshirilmagan.
            </div>
          }
        </div>

        <!-- Close Button -->
        <div class="border-t border-slate-800 pt-4 mt-6 text-right">
          <button 
            (click)="closeModal.emit()"
            class="px-5 py-2 rounded-xl text-xs font-bold text-slate-300 bg-slate-900 border border-slate-800 hover:bg-slate-800 transition">
            Yopish
          </button>
        </div>

      </div>
    </div>

    <!-- Certificate Modal -->
    @if (selectedCertificate()) {
      <app-certificate-modal 
        [certificate]="selectedCertificate()"
        (closeModal)="selectedCertificate.set(null)">
      </app-certificate-modal>
    }
  `
})
export class HistoryModalComponent {
  readonly quizService = inject(QuizService);
  readonly apiService = inject(QuizApiService);
  readonly closeModal = output<void>();
  readonly Math = Math;

  readonly selectedCertificate = signal<CertificateData | null>(null);

  getStarsStr(score: number): string {
    if (score > 80) return '⭐⭐⭐⭐⭐';
    if (score > 60) return '⭐⭐⭐⭐';
    if (score > 40) return '⭐⭐⭐';
    if (score > 20) return '⭐⭐';
    if (score > 0) return '⭐';
    return '⚪';
  }

  retakeQuiz(quizId: string): void {
    this.closeModal.emit();
    this.quizService.startQuiz(quizId);
  }

  openCertificate(item: any): void {
    if (item.id) {
      this.apiService.getCertificate(item.id).subscribe(cert => {
        if (cert) {
          this.selectedCertificate.set(cert);
        } else {
          this.setFallbackCertificate(item);
        }
      });
    } else {
      this.setFallbackCertificate(item);
    }
  }

  private setFallbackCertificate(item: any): void {
    const stars = item.scorePercentage > 80 ? 5 : item.scorePercentage > 60 ? 4 : 3;
    this.selectedCertificate.set({
      certificateId: `CERT-${(item.id || 'DEMO').substring(0, 8).toUpperCase()}`,
      certificateCode: `CERT-QM-${(item.id || 'DEMO').substring(0, 8).toUpperCase()}-2026`,
      userName: this.quizService.currentUserName() || 'Dasturchi',
      quizTitle: item.quizTitle,
      categoryName: item.categoryName,
      scorePercentage: item.scorePercentage,
      starsCount: stars,
      issuedAt: item.completedAt,
      certificateUrl: '',
      issuer: 'QuizMaster PRO Certification Board',
      badgeTitle: stars === 5 ? 'Senior Certified Architect' : 'Certified Professional'
    });
  }
}
