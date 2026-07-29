import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuizService } from '../../services/quiz.service';
import { QuizApiService } from '../../services/quiz-api.service';
import { CertificateModalComponent } from '../certificate-modal/certificate-modal.component';
import { CertificateData, CategoryProgress } from '../../models/quiz.model';

@Component({
  selector: 'app-quiz-list',
  standalone: true,
  imports: [CommonModule, FormsModule, CertificateModalComponent],
  template: `
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
      
      <!-- Hero Banner -->
      <div class="relative overflow-hidden rounded-3xl glass-card border border-indigo-500/20 p-6 sm:p-10 mb-10 bg-gradient-to-br from-slate-900 via-indigo-950/40 to-slate-950 shadow-2xl">
        <div class="absolute -top-24 -right-24 w-96 h-96 bg-indigo-500/10 rounded-full blur-3xl pointer-events-none"></div>
        <div class="absolute -bottom-24 -left-24 w-96 h-96 bg-purple-500/10 rounded-full blur-3xl pointer-events-none"></div>

        <div class="relative z-10 max-w-3xl">
          <div class="inline-flex items-center gap-2 px-3.5 py-1.5 rounded-full bg-indigo-500/15 border border-indigo-500/30 text-indigo-300 text-xs font-bold mb-4">
            <span class="w-2 h-2 rounded-full bg-indigo-400 animate-pulse"></span>
            Senior Dasturchi Bilim Sinov Platformasi
          </div>
          <h1 class="text-3xl sm:text-4xl lg:text-5xl font-extrabold text-white tracking-tight leading-tight mb-4">
            Dasturlash bo'yicha <span class="gradient-text">Bilimingizni Sinang</span>
          </h1>
          <p class="text-slate-300 text-sm sm:text-base leading-relaxed mb-6">
            Angular, C# .NET Core, Web Security va infratuzilma bo'yicha tayyorlangan interaktiv testlar, yulduzchalar (⭐) bilan baholash va kasbiy Sertifikatlar.
          </p>

          <!-- Search Bar -->
          <div class="relative max-w-md">
            <div class="absolute inset-y-0 left-0 pl-3.5 flex items-center pointer-events-none text-indigo-400">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2.5" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
              </svg>
            </div>
            <input 
              type="text" 
              [ngModel]="searchTerm()"
              (ngModelChange)="searchTerm.set($event)"
              placeholder="Test nomini qidiring (masalan: Angular, EF Core)..." 
              class="w-full pl-11 pr-4 py-3 bg-slate-900/90 border border-slate-700 rounded-xl text-sm text-white placeholder-slate-400 focus:outline-none focus:border-indigo-500 focus:ring-2 focus:ring-indigo-500/30 transition shadow-inner">
          </div>
        </div>
      </div>

      <!-- How it Works Quick Guide -->
      <div class="grid grid-cols-1 md:grid-cols-3 gap-4 mb-10">
        <div class="p-4 rounded-2xl glass-card border border-slate-800 flex items-center gap-3">
          <div class="w-10 h-10 rounded-xl bg-indigo-500/10 border border-indigo-500/20 text-indigo-400 flex items-center justify-center font-bold text-sm shrink-0">
            1
          </div>
          <div>
            <h4 class="text-xs font-bold text-white">Test & Qiyinchilik</h4>
            <p class="text-[11px] text-slate-400">Oson, O'rtacha, Qiyin yoki Barcha savollarni tanlang</p>
          </div>
        </div>

        <div class="p-4 rounded-2xl glass-card border border-slate-800 flex items-center gap-3">
          <div class="w-10 h-10 rounded-xl bg-amber-500/10 border border-amber-500/20 text-amber-400 flex items-center justify-center font-bold text-sm shrink-0">
            ⭐ 5
          </div>
          <div>
            <h4 class="text-xs font-bold text-white">Yulduzchalar (0-5 ⭐)</h4>
            <p class="text-[11px] text-slate-400">0%-100% natijangizga proportional yulduzlar oling</p>
          </div>
        </div>

        <div class="p-4 rounded-2xl glass-card border border-slate-800 flex items-center gap-3">
          <div class="w-10 h-10 rounded-xl bg-emerald-500/10 border border-emerald-500/20 text-emerald-400 flex items-center justify-center font-bold text-sm shrink-0">
            📜
          </div>
          <div>
            <h4 class="text-xs font-bold text-white">Kasbiy Sertifikat</h4>
            <p class="text-[11px] text-slate-400">70%+ natija ko'rsatib, rasmiy Sertifikatni yuklab oling</p>
          </div>
        </div>
      </div>

      <!-- Filters & Difficulty Tabs -->
      <div class="space-y-4 mb-8">
        
        <!-- Category Pills -->
        <div class="flex items-center justify-between flex-wrap gap-4 border-b border-slate-800/80 pb-4">
          <div class="flex items-center gap-2 overflow-x-auto pb-2 sm:pb-0 scrollbar-none">
            @for (cat of quizService.categories(); track cat.id) {
              <button 
                (click)="quizService.selectCategory(cat.id)"
                [class]="quizService.activeCategory() === cat.id ? 
                  'px-4 py-2 rounded-xl text-xs font-bold text-white bg-indigo-600 border border-indigo-500 shadow-md shadow-indigo-600/30 transition flex items-center gap-1.5' : 
                  'px-4 py-2 rounded-xl text-xs font-medium text-slate-400 bg-slate-900/80 border border-slate-800 hover:text-white hover:bg-slate-800 transition flex items-center gap-1.5'">
                <span>{{ cat.name }}</span>
                @if (getCategoryStars(cat.id); as stars) {
                  @if (stars > 0) {
                    <span class="text-amber-400 text-[11px]">⭐{{ stars }}</span>
                  }
                }
              </button>
            }
          </div>

          <div class="text-xs text-slate-400 font-semibold">
            Mavjud testlar: <span class="text-indigo-400 font-bold">{{ searchedQuizzes().length }}</span> ta
          </div>
        </div>

        <!-- Web UI Difficulty Filter Grouping (All, Oson, Medium/O'rtacha, Qiyin) -->
        <div class="flex items-center gap-2 overflow-x-auto pb-1">
          <span class="text-xs text-slate-400 font-medium mr-2">Qiyinchilik darajasi:</span>
          
          <button 
            (click)="setDifficulty('all')"
            [class]="activeDifficulty() === 'all' ? 
              'px-3 py-1.5 rounded-lg text-xs font-bold text-white bg-slate-800 border border-slate-700' : 
              'px-3 py-1.5 rounded-lg text-xs font-medium text-slate-400 hover:text-slate-200 bg-slate-950 border border-slate-900'">
            🌐 Barchasi (All)
          </button>

          <button 
            (click)="setDifficulty('Oson')"
            [class]="activeDifficulty() === 'Oson' ? 
              'px-3 py-1.5 rounded-lg text-xs font-bold text-emerald-300 bg-emerald-500/20 border border-emerald-500/40' : 
              'px-3 py-1.5 rounded-lg text-xs font-medium text-slate-400 hover:text-emerald-400 bg-slate-950 border border-slate-900'">
            🟢 Oson (Easy)
          </button>

          <button 
            (click)="setDifficulty('Medium')"
            [class]="activeDifficulty() === 'Medium' ? 
              'px-3 py-1.5 rounded-lg text-xs font-bold text-amber-300 bg-amber-500/20 border border-amber-500/40' : 
              'px-3 py-1.5 rounded-lg text-xs font-medium text-slate-400 hover:text-amber-400 bg-slate-950 border border-slate-900'">
            🟡 O'rtacha (Medium)
          </button>

          <button 
            (click)="setDifficulty('Qiyin')"
            [class]="activeDifficulty() === 'Qiyin' ? 
              'px-3 py-1.5 rounded-lg text-xs font-bold text-rose-300 bg-rose-500/20 border border-rose-500/40' : 
              'px-3 py-1.5 rounded-lg text-xs font-medium text-slate-400 hover:text-rose-400 bg-slate-950 border border-slate-900'">
            🔴 Qiyin (Hard)
          </button>
        </div>

      </div>

      <!-- Quiz Grid -->
      @if (searchedQuizzes().length > 0) {
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          @for (quiz of searchedQuizzes(); track quiz.id) {
            <div class="glass-card glass-card-hover rounded-2xl p-6 flex flex-col justify-between border border-slate-800 relative group">
              
              <!-- Top Badges & Stars -->
              <div>
                <div class="flex items-center justify-between mb-4">
                  <div class="flex items-center gap-2">
                    <span [class]="getDifficultyBadgeClass(quiz.difficulty)">
                      {{ quiz.difficulty }}
                    </span>
                    
                    <!-- Stars Rating Badge for Quiz Attempt History -->
                    @if (getBestQuizStars(quiz.id); as stars) {
                      @if (stars > 0) {
                        <span class="px-2 py-0.5 rounded-full text-xs font-extrabold bg-amber-500/10 text-amber-400 border border-amber-500/20 flex items-center gap-1">
                          <span>⭐</span>
                          <span>{{ stars }}/5</span>
                        </span>
                      }
                    }
                  </div>
                  
                  <div class="flex items-center gap-2">
                    @if (quiz.isCustom) {
                      <span class="px-2 py-0.5 rounded-md text-[10px] font-bold uppercase tracking-wider bg-purple-500/20 text-purple-300 border border-purple-500/30">
                        Maxsus
                      </span>
                    }
                    <span class="text-xs text-slate-300 flex items-center gap-1 bg-slate-900 px-2.5 py-1 rounded-lg border border-slate-800 font-medium">
                      <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5 text-indigo-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                      </svg>
                      {{ Math.round(quiz.timeLimitSeconds / 60) }} daq
                    </span>
                  </div>
                </div>

                <!-- Title & Description -->
                <h3 class="text-lg font-bold text-white mb-2 group-hover:text-indigo-300 transition">
                  {{ quiz.title }}
                </h3>
                <p class="text-xs text-slate-400 line-clamp-3 mb-6 leading-relaxed">
                  {{ quiz.description }}
                </p>
              </div>

              <!-- Footer Info & Action Buttons -->
              <div class="pt-4 border-t border-slate-800 flex items-center justify-between gap-2">
                <div class="flex items-center gap-1.5 text-xs text-slate-300">
                  <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-emerald-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8.228 9c.549-1.165 2.03-2 3.772-2 2.21 0 4 1.343 4 3 0 1.4-1.278 2.575-3.006 2.907-.542.104-.994.54-.994 1.093m0 3h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                  <span class="font-bold text-white">{{ quiz.questions.length }}</span> ta
                </div>

                <div class="flex items-center gap-2">
                  <!-- Certificate Button if score >= 70% -->
                  @if (getPassingAttemptId(quiz.id); as attemptId) {
                    <button 
                      (click)="openCertificate(attemptId)"
                      title="Sertifikatni yuklab olish"
                      class="px-2.5 py-1.5 rounded-xl text-xs font-bold text-amber-300 bg-amber-500/15 border border-amber-500/30 hover:bg-amber-500/25 transition">
                      🎓 Sertifikat
                    </button>
                  }

                  <!-- Start / Retake Button -->
                  <button 
                    (click)="quizService.startQuiz(quiz.id)"
                    class="flex items-center gap-1.5 px-3.5 py-2 rounded-xl text-xs font-bold text-white bg-indigo-600 hover:bg-indigo-500 shadow-md shadow-indigo-600/30 transition-all hover:scale-105 active:scale-95">
                    @if (getBestQuizStars(quiz.id) > 0) {
                      <span>🔄 Qayta yechish</span>
                    } @else {
                      <span>Testni Boshlash</span>
                      <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2.5" d="M14 5l7 7m0 0l-7 7m7-7H3" />
                      </svg>
                    }
                  </button>
                </div>
              </div>

            </div>
          }
        </div>
      } @else {
        <!-- Empty state -->
        <div class="glass-card rounded-2xl p-10 text-center max-w-md mx-auto my-10 border border-slate-800">
          <div class="w-14 h-14 rounded-full bg-slate-900 border border-slate-800 flex items-center justify-center mx-auto mb-4 text-slate-500">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-7 h-7" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9.172 16.172a4 4 0 015.656 0M9 10h.01M15 10h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
            </svg>
          </div>
          <h3 class="text-base font-bold text-white mb-2">Hech qanday test topilmadi</h3>
          <p class="text-xs text-slate-400 mb-6">Siz qidirgan qiyinchilik va mezonlar bo'yicha testlar mavjud emas.</p>
          <button 
            (click)="searchTerm.set(''); setDifficulty('all'); quizService.selectCategory('all')"
            class="px-4 py-2 rounded-xl text-xs font-bold text-indigo-400 bg-indigo-500/10 border border-indigo-500/20 hover:bg-indigo-500/20 transition">
            Barcha testlarni ko'rish
          </button>
        </div>
      }

      <!-- Certificate Modal View -->
      @if (selectedCertificate()) {
        <app-certificate-modal 
          [certificate]="selectedCertificate()"
          (closeModal)="selectedCertificate.set(null)">
        </app-certificate-modal>
      }

    </div>
  `
})
export class QuizListComponent implements OnInit {
  readonly quizService = inject(QuizService);
  readonly apiService = inject(QuizApiService);
  readonly Math = Math;

  readonly searchTerm = signal<string>('');
  readonly activeDifficulty = signal<string>('all');
  readonly selectedCertificate = signal<CertificateData | null>(null);

  readonly categoryProgressList = signal<CategoryProgress[]>([]);

  ngOnInit(): void {
    this.apiService.getCategoryProgress(this.quizService.currentUserName()).subscribe(progress => {
      if (progress && progress.length > 0) {
        this.categoryProgressList.set(progress);
      }
    });
  }

  setDifficulty(diff: string): void {
    this.activeDifficulty.set(diff);
  }

  readonly searchedQuizzes = computed(() => {
    const term = this.searchTerm().toLowerCase().trim();
    const diff = this.activeDifficulty().toLowerCase();
    let filtered = this.quizService.filteredQuizzes();

    if (diff !== 'all') {
      filtered = filtered.filter(q => {
        const qDiff = q.difficulty.toLowerCase();
        if (diff === 'medium' || diff === "o'rta") {
          return qDiff.includes('rta') || qDiff === 'medium';
        }
        return qDiff === diff;
      });
    }

    if (!term) return filtered;

    return filtered.filter(q => 
      q.title.toLowerCase().includes(term) || 
      q.description.toLowerCase().includes(term) ||
      q.categoryName.toLowerCase().includes(term)
    );
  });

  getCategoryStars(catId: string): number {
    const found = this.categoryProgressList().find(p => p.category.toLowerCase() === catId.toLowerCase());
    return found ? found.starsCount : 0;
  }

  getBestQuizStars(quizId: string): number {
    const history = this.quizService.quizHistory();
    const attempts = history.filter(h => h.quizId === quizId);
    if (!attempts.length) return 0;

    const maxScore = Math.max(...attempts.map(a => a.scorePercentage));
    if (maxScore > 80) return 5;
    if (maxScore > 60) return 4;
    if (maxScore > 40) return 3;
    if (maxScore > 20) return 2;
    if (maxScore > 0) return 1;
    return 0;
  }

  getPassingAttemptId(quizId: string): string | null {
    const history = this.quizService.quizHistory();
    const passing = history.find(h => h.quizId === quizId && h.scorePercentage >= 70);
    return passing?.id || null;
  }

  openCertificate(attemptId: string): void {
    this.apiService.getCertificate(attemptId).subscribe(cert => {
      if (cert) {
        this.selectedCertificate.set(cert);
      } else {
        const attempt = this.quizService.quizHistory().find(a => a.id === attemptId);
        if (attempt) {
          const stars = this.getBestQuizStars(attempt.quizId);
          this.selectedCertificate.set({
            certificateId: `CERT-${attemptId.substring(0, 8).toUpperCase()}`,
            certificateCode: `CERT-QM-${attemptId.substring(0, 8).toUpperCase()}-2026`,
            userName: this.quizService.currentUserName() || 'Dasturchi',
            quizTitle: attempt.quizTitle,
            categoryName: attempt.categoryName,
            scorePercentage: attempt.scorePercentage,
            starsCount: stars,
            issuedAt: attempt.completedAt,
            certificateUrl: '',
            issuer: 'QuizMaster PRO Certification Board',
            badgeTitle: stars === 5 ? 'Senior Certified Architect' : 'Certified Professional'
          });
        }
      }
    });
  }

  getDifficultyBadgeClass(difficulty: string): string {
    switch (difficulty) {
      case 'Oson':
        return 'px-2.5 py-0.5 rounded-full text-xs font-bold bg-emerald-500/15 text-emerald-400 border border-emerald-500/30';
      case 'O\'rta':
      case 'Medium':
        return 'px-2.5 py-0.5 rounded-full text-xs font-bold bg-amber-500/15 text-amber-400 border border-amber-500/30';
      case 'Qiyin':
        return 'px-2.5 py-0.5 rounded-full text-xs font-bold bg-rose-500/15 text-rose-400 border border-rose-500/30';
      default:
        return 'px-2.5 py-0.5 rounded-full text-xs font-bold bg-slate-800 text-slate-300';
    }
  }
}
