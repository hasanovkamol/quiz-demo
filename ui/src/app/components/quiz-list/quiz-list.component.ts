import { Component, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuizService } from '../../services/quiz.service';
import { QuizCategory } from '../../models/quiz.model';

@Component({
  selector: 'app-quiz-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
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
            Angular, C# .NET Core, Web Security va zamonaviy web infratuzilmasi bo'yicha tayyorlangan interaktiv va taymerli testlarni topshiring.
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
            <h4 class="text-xs font-bold text-white">Testni Tanlang</h4>
            <p class="text-[11px] text-slate-400">Kerakli kategoriya va darajadagi testni tanlang</p>
          </div>
        </div>

        <div class="p-4 rounded-2xl glass-card border border-slate-800 flex items-center gap-3">
          <div class="w-10 h-10 rounded-xl bg-purple-500/10 border border-purple-500/20 text-purple-400 flex items-center justify-center font-bold text-sm shrink-0">
            2
          </div>
          <div>
            <h4 class="text-xs font-bold text-white">Javob Bering (Taymer)</h4>
            <p class="text-[11px] text-slate-400">Belgilangan vaqt ichida savollarga javob belgilang</p>
          </div>
        </div>

        <div class="p-4 rounded-2xl glass-card border border-slate-800 flex items-center gap-3">
          <div class="w-10 h-10 rounded-xl bg-emerald-500/10 border border-emerald-500/20 text-emerald-400 flex items-center justify-center font-bold text-sm shrink-0">
            3
          </div>
          <div>
            <h4 class="text-xs font-bold text-white">Natija va Izohlar</h4>
            <p class="text-[11px] text-slate-400">Har bir savol bo'yicha batafsil tahlilni ko'ring</p>
          </div>
        </div>
      </div>

      <!-- Category Filter Pills -->
      <div class="flex items-center justify-between flex-wrap gap-4 mb-8 border-b border-slate-800/80 pb-4">
        <div class="flex items-center gap-2 overflow-x-auto pb-2 sm:pb-0 scrollbar-none">
          @for (cat of categories; track cat.id) {
            <button 
              (click)="quizService.selectCategory(cat.id)"
              [class]="quizService.activeCategory() === cat.id ? 
                'px-4 py-2 rounded-xl text-xs font-bold text-white bg-indigo-600 border border-indigo-500 shadow-md shadow-indigo-600/30 transition' : 
                'px-4 py-2 rounded-xl text-xs font-medium text-slate-400 bg-slate-900/80 border border-slate-800 hover:text-white hover:bg-slate-800 transition'">
              {{ cat.label }}
            </button>
          }
        </div>
        <div class="text-xs text-slate-400 font-semibold">
          Mavjud testlar: <span class="text-indigo-400 font-bold">{{ searchedQuizzes().length }}</span> ta
        </div>
      </div>

      <!-- Quiz Grid -->
      @if (searchedQuizzes().length > 0) {
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          @for (quiz of searchedQuizzes(); track quiz.id) {
            <div class="glass-card glass-card-hover rounded-2xl p-6 flex flex-col justify-between border border-slate-800 relative group">
              
              <!-- Top Badges -->
              <div>
                <div class="flex items-center justify-between mb-4">
                  <span [class]="getDifficultyBadgeClass(quiz.difficulty)">
                    {{ quiz.difficulty }}
                  </span>
                  
                  <div class="flex items-center gap-2">
                    @if (quiz.isCustom) {
                      <span class="px-2 py-0.5 rounded-md text-[10px] font-bold uppercase tracking-wider bg-purple-500/20 text-purple-300 border border-purple-500/30">
                        Maxsus
                      </span>
                      <button 
                        (click)="quizService.deleteQuiz(quiz.id)" 
                        title="O'chirish"
                        class="text-slate-500 hover:text-rose-400 transition p-1">
                        <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                        </svg>
                      </button>
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

              <!-- Footer Info & Start Button -->
              <div class="pt-4 border-t border-slate-800 flex items-center justify-between">
                <div class="flex items-center gap-1.5 text-xs text-slate-300">
                  <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-emerald-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8.228 9c.549-1.165 2.03-2 3.772-2 2.21 0 4 1.343 4 3 0 1.4-1.278 2.575-3.006 2.907-.542.104-.994.54-.994 1.093m0 3h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                  <span class="font-bold text-white">{{ quiz.questions.length }}</span> ta savol
                </div>

                <button 
                  (click)="quizService.startQuiz(quiz.id)"
                  class="flex items-center gap-1.5 px-4 py-2 rounded-xl text-xs font-bold text-white bg-indigo-600 hover:bg-indigo-500 shadow-md shadow-indigo-600/30 transition-all hover:scale-105 active:scale-95">
                  Testni Boshlash
                  <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2.5" d="M14 5l7 7m0 0l-7 7m7-7H3" />
                  </svg>
                </button>
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
          <p class="text-xs text-slate-400 mb-6">Siz qidirgan so'rov bo'yicha testlar mavjud emas.</p>
          <button 
            (click)="searchTerm.set(''); quizService.selectCategory('all')"
            class="px-4 py-2 rounded-xl text-xs font-bold text-indigo-400 bg-indigo-500/10 border border-indigo-500/20 hover:bg-indigo-500/20 transition">
            Barcha testlarni ko'rish
          </button>
        </div>
      }

    </div>
  `
})
export class QuizListComponent {
  readonly quizService = inject(QuizService);
  readonly Math = Math;

  readonly searchTerm = signal<string>('');

  readonly categories: { id: QuizCategory | 'all'; label: string }[] = [
    { id: 'all', label: 'Barchasi' },
    { id: 'angular', label: 'Angular Framework' },
    { id: 'dotnet', label: 'C# & .NET Core' },
    { id: 'webdev', label: 'Web Infrastructure' },
    { id: 'custom', label: 'Maxsus Testlar' }
  ];

  readonly searchedQuizzes = computed(() => {
    const term = this.searchTerm().toLowerCase().trim();
    const filtered = this.quizService.filteredQuizzes();
    if (!term) return filtered;

    return filtered.filter(q => 
      q.title.toLowerCase().includes(term) || 
      q.description.toLowerCase().includes(term) ||
      q.categoryName.toLowerCase().includes(term)
    );
  });

  getDifficultyBadgeClass(difficulty: string): string {
    switch (difficulty) {
      case 'Oson':
        return 'px-2.5 py-0.5 rounded-full text-xs font-bold bg-emerald-500/15 text-emerald-400 border border-emerald-500/30';
      case 'O\'rta':
        return 'px-2.5 py-0.5 rounded-full text-xs font-bold bg-amber-500/15 text-amber-400 border border-amber-500/30';
      case 'Qiyin':
        return 'px-2.5 py-0.5 rounded-full text-xs font-bold bg-rose-500/15 text-rose-400 border border-rose-500/30';
      default:
        return 'px-2.5 py-0.5 rounded-full text-xs font-bold bg-slate-800 text-slate-300';
    }
  }
}
