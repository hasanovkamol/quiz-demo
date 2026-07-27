import { Component, inject, signal, OnInit, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuizApiService } from '../../services/quiz-api.service';
import { QuizService } from '../../services/quiz.service';
import { AuthService } from '../../services/auth.service';
import { QuizAttempt } from '../../models/quiz.model';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
      
      @if (!authService.isAdmin()) {
        <div class="max-w-xl mx-auto my-16 p-8 glass-card rounded-3xl border border-rose-500/30 text-center relative overflow-hidden shadow-2xl">
          <div class="w-16 h-16 rounded-2xl bg-rose-500/10 border border-rose-500/20 text-rose-400 flex items-center justify-center mx-auto mb-5 shadow-lg shadow-rose-500/10">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z" />
            </svg>
          </div>
          <h2 class="text-2xl font-black text-white mb-2">Ruxsat Cheklangan (403 Forbidden)</h2>
          <p class="text-xs text-slate-400 mb-6 leading-relaxed">
            Admin Console faqat Keycloak yoki Tizimda <b>Admin</b> roliga ega bo'lgan foydalanuvchilar uchun ochiq. Iltimos Admin hisobi bilan tizimga kiring.
          </p>
          <button 
            (click)="quizService.isNameModalOpen.set(true)"
            class="px-6 py-3 rounded-2xl text-xs font-bold text-white bg-gradient-to-r from-indigo-600 to-purple-600 hover:from-indigo-500 hover:to-purple-500 shadow-lg shadow-indigo-600/30 transition">
            🔑 Tizimga Kirish / Autentifikatsiya
          </button>
        </div>
      } @else {

      <!-- Admin Header -->
      <div class="flex items-center justify-between mb-8 pb-4 border-b border-slate-800 flex-wrap gap-4">
        <div>
          <div class="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-purple-500/10 border border-purple-500/20 text-purple-400 text-xs font-bold mb-2">
            <span class="w-2 h-2 rounded-full bg-purple-400 animate-pulse"></span>
            Tizim Boshqaruv Portali
          </div>
          <h1 class="text-2xl sm:text-3xl font-extrabold text-white">
            Admin <span class="gradient-text">Management Console</span>
          </h1>
        </div>

        <div class="flex items-center gap-3">
          <!-- Create Test Button -->
          <button 
            (click)="openCreator.emit()"
            class="flex items-center gap-2 px-4 py-2.5 rounded-xl text-xs font-bold text-white bg-gradient-to-r from-purple-600 to-indigo-600 hover:from-purple-500 hover:to-indigo-500 shadow-lg shadow-purple-600/30 transition">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2.5" d="M12 4v16m8-8H4" />
            </svg>
            Yangi Test Yaratish
          </button>

          <!-- Navigation Tabs -->
          <div class="flex items-center gap-2 bg-slate-900/90 p-1.5 rounded-2xl border border-slate-800">
            <button 
              (click)="activeTab.set('attempts')"
              [class]="activeTab() === 'attempts' ? 
                'px-4 py-2 rounded-xl text-xs font-bold text-white bg-indigo-600 shadow-md shadow-indigo-600/30 transition' : 
                'px-4 py-2 rounded-xl text-xs font-medium text-slate-400 hover:text-white transition'">
              Test Topshirishlar
            </button>

            <button 
              (click)="activeTab.set('ai-generator')"
              [class]="activeTab() === 'ai-generator' ? 
                'px-4 py-2 rounded-xl text-xs font-bold text-white bg-purple-600 shadow-md shadow-purple-600/30 transition flex items-center gap-1.5' : 
                'px-4 py-2 rounded-xl text-xs font-medium text-slate-400 hover:text-white transition flex items-center gap-1.5'">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-purple-300" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 10V3L4 14h7v7l9-11h-7z" />
              </svg>
              AI Generatori
            </button>
          </div>
        </div>
      </div>

      <!-- Metrics Cards -->
      <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4 mb-8">
        <div class="glass-card rounded-2xl p-5 border border-slate-800">
          <div class="text-xs text-slate-400 font-semibold mb-1">Jami Test Topshirishlar</div>
          <div class="text-2xl font-extrabold text-white">{{ stats().totalAttempts }}</div>
        </div>
        <div class="glass-card rounded-2xl p-5 border border-slate-800">
          <div class="text-xs text-indigo-400 font-semibold mb-1">Foydalanuvchilar Soni</div>
          <div class="text-2xl font-extrabold text-indigo-300">{{ stats().uniqueUsersCount }}</div>
        </div>
        <div class="glass-card rounded-2xl p-5 border border-slate-800">
          <div class="text-xs text-emerald-400 font-semibold mb-1">O'rtacha Ball Natijasi</div>
          <div class="text-2xl font-extrabold text-emerald-400">{{ stats().avgScore }}%</div>
        </div>
        <div class="glass-card rounded-2xl p-5 border border-slate-800">
          <div class="text-xs text-purple-400 font-semibold mb-1">Mavjud Testlar</div>
          <div class="text-2xl font-extrabold text-purple-300">{{ quizService.quizzes().length }}</div>
        </div>
      </div>

      <!-- Tab 1: User Attempts Table -->
      @if (activeTab() === 'attempts') {
        <div class="glass-card rounded-2xl p-6 border border-slate-800">
          <h3 class="text-lg font-bold text-white mb-4">
            Foydalanuvchilarning Test Natijalari Tarixi
          </h3>

          <div class="overflow-x-auto">
            <table class="w-full text-left text-xs text-slate-300">
              <thead class="bg-slate-900/90 text-slate-400 uppercase font-bold tracking-wider border-b border-slate-800">
                <tr>
                  <th class="p-3">Foydalanuvchi (UserName)</th>
                  <th class="p-3">Test Nomi</th>
                  <th class="p-3">Kategoriya</th>
                  <th class="p-3">To'g'ri Javoblar</th>
                  <th class="p-3">Ball (%)</th>
                  <th class="p-3">Vaqt</th>
                  <th class="p-3">Sana</th>
                </tr>
              </thead>
              <tbody class="divide-y divide-slate-800/60">
                @for (attempt of userAttempts(); track attempt.id) {
                  <tr class="hover:bg-slate-900/50 transition">
                    <td class="p-3 font-bold text-white flex items-center gap-2">
                      <div class="w-7 h-7 rounded-full bg-indigo-500/20 text-indigo-300 font-bold text-xs flex items-center justify-center border border-indigo-500/30">
                        {{ attempt.userName.charAt(0).toUpperCase() }}
                      </div>
                      {{ attempt.userName }}
                    </td>
                    <td class="p-3 font-medium text-slate-200">{{ attempt.quizTitle }}</td>
                    <td class="p-3 text-slate-400">{{ attempt.categoryName }}</td>
                    <td class="p-3 text-slate-300 font-semibold">{{ attempt.correctAnswersCount }} / {{ attempt.totalQuestions }}</td>
                    <td class="p-3 font-extrabold" [class.text-emerald-400]="attempt.scorePercentage >= 70" [class.text-rose-400]="attempt.scorePercentage < 70">
                      {{ attempt.scorePercentage }}%
                    </td>
                    <td class="p-3 text-slate-400 font-mono">{{ formatTime(attempt.totalTimeSpentSeconds) }}</td>
                    <td class="p-3 text-slate-500 text-[11px]">{{ attempt.completedAt }}</td>
                  </tr>
                } @empty {
                  <tr>
                    <td colspan="7" class="p-8 text-center text-slate-500 text-xs">
                      Hali hech bir foydalanuvchi test topshirmagan.
                    </td>
                  </tr>
                }
              </tbody>
            </table>
          </div>
        </div>
      }

      <!-- Tab 2: AI Question Generator -->
      @if (activeTab() === 'ai-generator') {
        <div class="glass-card rounded-3xl p-8 border border-purple-500/30 bg-gradient-to-br from-slate-900 via-slate-900/90 to-purple-950/30 shadow-2xl max-w-3xl mx-auto">
          
          <div class="flex items-center gap-3 mb-6">
            <div class="w-12 h-12 rounded-2xl bg-purple-500/15 border border-purple-500/30 text-purple-300 flex items-center justify-center font-bold shadow-lg shadow-purple-500/20">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 10V3L4 14h7v7l9-11h-7z" />
              </svg>
            </div>
            <div>
              <h2 class="text-xl font-extrabold text-white">Semantic Kernel AI Savollar Generatori</h2>
              <p class="text-xs text-slate-300">Sun'iy intellekt yordamida har qanday texnologiya bo'yicha tayyor test yaratish</p>
            </div>
          </div>

          <div class="space-y-5">
            <div>
              <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Mavzu yoki Texnologiya (Topic) *</label>
              <input 
                type="text" 
                [(ngModel)]="aiTopic" 
                placeholder="masalan: Angular Signals Performance, C# Entity Framework Core 9 Indexes, PostgreSQL Locks..." 
                class="w-full px-4 py-3 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-purple-500">
            </div>

            <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Kategoriya</label>
                <select 
                  [(ngModel)]="aiCategory" 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-purple-500">
                  <option value="angular">Angular Framework</option>
                  <option value="dotnet">C# & .NET Core</option>
                  <option value="webdev">Web Infrastructure</option>
                </select>
              </div>

              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Qiyinchilik Darajasi</label>
                <select 
                  [(ngModel)]="aiDifficulty" 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-purple-500">
                  <option value="Oson">Oson</option>
                  <option value="O'rta">O'rta</option>
                  <option value="Qiyin">Qiyin</option>
                </select>
              </div>
            </div>

            <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Savollar Soni (1-10)</label>
                <input 
                  type="number" 
                  [(ngModel)]="aiQuestionCount" 
                  min="1" max="10" 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-purple-500">
              </div>

              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Gemini / AI API Key *</label>
                <input 
                  type="password" 
                  [(ngModel)]="aiApiKey" 
                  placeholder="AI API Key kiriting..." 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-purple-500">
              </div>
            </div>

            <!-- Submit Button -->
            <div class="pt-4 border-t border-slate-800 text-right">
              <button 
                (click)="generateAiQuiz()"
                [disabled]="isGenerating()"
                class="w-full sm:w-auto px-8 py-3 rounded-xl text-sm font-bold text-white bg-gradient-to-r from-purple-600 to-indigo-600 hover:from-purple-500 hover:to-indigo-500 shadow-lg shadow-purple-600/30 transition disabled:opacity-50 flex items-center justify-center gap-2 ml-auto">
                @if (isGenerating()) {
                  <svg class="animate-spin -ml-1 mr-2 h-4 w-4 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                    <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                    <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                  </svg>
                  Semantic Kernel AI Savollar Generatsiyasi Ketmoqda...
                } @else {
                  <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 10V3L4 14h7v7l9-11h-7z" />
                  </svg>
                  AI Bilan Test Yaratish
                }
              </button>
            </div>

          </div>

        </div>
      }
      }

    </div>
  `
})
export class AdminDashboardComponent implements OnInit {
  private readonly apiService = inject(QuizApiService);
  readonly quizService = inject(QuizService);
  readonly authService = inject(AuthService);

  readonly openCreator = output<void>();

  readonly activeTab = signal<'attempts' | 'ai-generator'>('attempts');
  readonly userAttempts = signal<QuizAttempt[]>([]);
  readonly stats = signal<any>({ totalQuizzes: 0, totalAttempts: 0, avgScore: 0, uniqueUsersCount: 0 });

  readonly isGenerating = signal<boolean>(false);

  aiTopic = '';
  aiCategory = 'angular';
  aiDifficulty = 'O\'rta';
  aiQuestionCount = 3;
  aiApiKey = '';

  ngOnInit(): void {
    this.loadAdminData();
  }

  loadAdminData(): void {
    this.apiService.getAttempts().subscribe(attempts => {
      if (attempts && attempts.length > 0) {
        this.userAttempts.set(attempts);
      } else {
        this.userAttempts.set(this.quizService.quizHistory().map(h => ({
          ...h,
          userName: 'Noma\'lum Foydalanuvchi'
        } as QuizAttempt)));
      }
    });

    this.apiService.getAdminStats().subscribe(s => this.stats.set(s));
  }

  generateAiQuiz(): void {
    if (!this.aiTopic.trim()) {
      alert("Iltimos, test mavzusini kiriting!");
      return;
    }

    this.isGenerating.set(true);

    this.apiService.generateAiQuiz({
      topic: this.aiTopic,
      category: this.aiCategory,
      difficulty: this.aiDifficulty,
      questionCount: this.aiQuestionCount,
      timeLimitMinutes: 5,
      apiKey: this.aiApiKey
    }).subscribe({
      next: (quiz) => {
        this.isGenerating.set(false);
        this.quizService.addCustomQuiz(quiz);
        alert(`" ${quiz.title} " testi muvaffaqiyatli yaratildi va saqlandi!`);
        this.aiTopic = '';
        this.activeTab.set('attempts');
      },
      error: (err) => {
        this.isGenerating.set(false);
        console.error(err);
        alert("AI Test yaratishda xatolik yuz berdi. Iltimos API key ni tekshiring.");
      }
    });
  }

  formatTime(seconds: number): string {
    const m = Math.floor(seconds / 60);
    const s = seconds % 60;
    return `${m}m ${s}s`;
  }
}
