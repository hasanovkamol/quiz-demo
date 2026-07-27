import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizService } from '../../services/quiz.service';

@Component({
  selector: 'app-quiz-result',
  standalone: true,
  imports: [CommonModule],
  template: `
    @if (quizService.latestResult(); as result) {
      <div class="max-w-4xl mx-auto px-4 sm:px-6 py-8">
        
        <!-- Score Summary Header Card -->
        <div class="glass-card rounded-3xl p-8 sm:p-10 mb-8 border border-slate-800 text-center relative overflow-hidden bg-gradient-to-b from-slate-900 via-slate-900/90 to-slate-950">
          
          <!-- Background Glow -->
          <div [class]="result.scorePercentage >= 70 ? 
            'absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-80 h-80 bg-emerald-500/10 rounded-full blur-3xl pointer-events-none' : 
            'absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-80 h-80 bg-rose-500/10 rounded-full blur-3xl pointer-events-none'">
          </div>

          <div class="relative z-10 max-w-xl mx-auto">
            <!-- Badge -->
            <div class="inline-flex items-center gap-2 px-3.5 py-1 rounded-full bg-slate-800 border border-slate-700 text-xs font-semibold text-slate-300 mb-6">
              <span class="w-2 h-2 rounded-full" [class.bg-emerald-400]="result.scorePercentage >= 70" [class.bg-rose-400]="result.scorePercentage < 70"></span>
              {{ result.categoryName }} • {{ result.quizTitle }}
            </div>

            <!-- Circular / Large Score Display -->
            <div class="relative w-36 h-36 mx-auto mb-6 flex items-center justify-center">
              <svg class="w-full h-full transform -rotate-90" viewBox="0 0 100 100">
                <circle cx="50" cy="50" r="42" stroke="currentColor" stroke-width="8" class="text-slate-800" fill="transparent" />
                <circle 
                  cx="50" cy="50" r="42" 
                  stroke="currentColor" 
                  stroke-width="8" 
                  [class]="result.scorePercentage >= 70 ? 'text-emerald-500' : 'text-rose-500'" 
                  fill="transparent" 
                  stroke-dasharray="263.89" 
                  [style.stroke-dashoffset]="263.89 - (263.89 * result.scorePercentage) / 100"
                  stroke-linecap="round"
                  class="transition-all duration-1000 ease-out" />
              </svg>
              <div class="absolute flex flex-col items-center justify-center">
                <span class="text-3xl font-extrabold text-white tracking-tight">{{ result.scorePercentage }}%</span>
                <span class="text-[10px] uppercase font-bold text-slate-400">Natija</span>
              </div>
            </div>

            <!-- Grade & Verdict -->
            <h2 class="text-2xl sm:text-3xl font-extrabold text-white mb-2">
              {{ getGradeTitle(result.scorePercentage) }}
            </h2>
            <p class="text-xs sm:text-sm text-slate-400 mb-8">
              {{ getGradeSubtitle(result.scorePercentage) }}
            </p>

            <!-- Quick Metrics Grid -->
            <div class="grid grid-cols-2 sm:grid-cols-4 gap-3 bg-slate-950/80 rounded-2xl p-4 border border-slate-800">
              <div class="text-center p-2">
                <div class="text-xs text-slate-400 font-semibold mb-1">Jami Savollar</div>
                <div class="text-lg font-bold text-white">{{ result.totalQuestions }}</div>
              </div>
              <div class="text-center p-2">
                <div class="text-xs text-emerald-400 font-semibold mb-1">To'g'ri Javoblar</div>
                <div class="text-lg font-bold text-emerald-400">{{ result.correctAnswersCount }}</div>
              </div>
              <div class="text-center p-2">
                <div class="text-xs text-rose-400 font-semibold mb-1">Xato Javoblar</div>
                <div class="text-lg font-bold text-rose-400">{{ result.totalQuestions - result.correctAnswersCount }}</div>
              </div>
              <div class="text-center p-2">
                <div class="text-xs text-indigo-400 font-semibold mb-1">Sarflangan Vaqt</div>
                <div class="text-lg font-bold text-indigo-300">{{ formatTime(result.totalTimeSpentSeconds) }}</div>
              </div>
            </div>

          </div>

        </div>

        <!-- Actions Bar -->
        <div class="flex items-center justify-between gap-4 mb-10 flex-wrap">
          <button 
            (click)="quizService.startQuiz(result.quizId)"
            class="flex items-center gap-2 px-5 py-2.5 rounded-xl text-xs sm:text-sm font-bold text-indigo-300 bg-indigo-500/10 border border-indigo-500/20 hover:bg-indigo-500/20 transition">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15" />
            </svg>
            Qayta topshirish
          </button>

          <button 
            (click)="quizService.resetQuiz()"
            class="flex items-center gap-2 px-6 py-2.5 rounded-xl text-xs sm:text-sm font-bold text-white bg-indigo-600 hover:bg-indigo-500 shadow-md shadow-indigo-600/30 transition">
            Barcha testlarga qaytish
            <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M14 5l7 7m0 0l-7 7m7-7H3" />
            </svg>
          </button>
        </div>

        <!-- Question Review Section -->
        @if (getQuizQuestions(); as questions) {
          <div class="space-y-6">
            <h3 class="text-xl font-extrabold text-white mb-4 flex items-center gap-2">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 text-indigo-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
              </svg>
              Savollar Tahlili va Javoblar
            </h3>

            @for (question of questions; track question.id; let qIdx = $index) {
              @let userAnswer = getUserAnswer(question.id);
              @let isCorrect = userAnswer?.isCorrect;

              <div [class]="isCorrect ? 
                'glass-card rounded-2xl p-6 border border-emerald-500/30 bg-emerald-950/10' : 
                'glass-card rounded-2xl p-6 border border-rose-500/30 bg-rose-950/10'">
                
                <div class="flex items-start justify-between gap-4 mb-3">
                  <div class="flex items-center gap-2">
                    <span [class]="isCorrect ? 'w-6 h-6 rounded-full bg-emerald-500 text-white font-bold text-xs flex items-center justify-center' : 'w-6 h-6 rounded-full bg-rose-500 text-white font-bold text-xs flex items-center justify-center'">
                      {{ qIdx + 1 }}
                    </span>
                    <span [class]="isCorrect ? 'text-xs font-bold text-emerald-400 uppercase tracking-wider' : 'text-xs font-bold text-rose-400 uppercase tracking-wider'">
                      {{ isCorrect ? "To'g'ri" : "Noto'g'ri" }}
                    </span>
                  </div>
                </div>

                <h4 class="text-base font-bold text-white mb-4">
                  {{ question.text }}
                </h4>

                @if (question.codeSnippet) {
                  <div class="mb-4 rounded-xl bg-slate-950 border border-slate-800 p-4 font-mono text-xs text-indigo-200 overflow-x-auto">
                    <pre><code>{{ question.codeSnippet }}</code></pre>
                  </div>
                }

                <!-- Options status -->
                <div class="space-y-2 mb-4">
                  @for (opt of question.options; track opt.id) {
                    @let isUserSelected = userAnswer?.selectedOptionId === opt.id;
                    @let isCorrectOption = question.correctOptionId === opt.id;

                    <div [class]="getOptionReviewClass(isUserSelected, isCorrectOption)">
                      <div class="flex items-center justify-between text-xs font-medium">
                        <span>{{ opt.text }}</span>
                        @if (isCorrectOption) {
                          <span class="text-[10px] font-bold text-emerald-400 uppercase bg-emerald-500/20 px-2 py-0.5 rounded border border-emerald-500/30">To'g'ri javob</span>
                        } @else if (isUserSelected && !isCorrectOption) {
                          <span class="text-[10px] font-bold text-rose-400 uppercase bg-rose-500/20 px-2 py-0.5 rounded border border-rose-500/30">Sizning tanlovingiz</span>
                        }
                      </div>
                    </div>
                  }
                </div>

                <!-- Explanation Box -->
                <div class="p-3.5 rounded-xl bg-slate-900/90 border border-slate-800 text-xs text-slate-300 flex items-start gap-2.5">
                  <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-indigo-400 shrink-0 mt-0.5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                  <div>
                    <span class="font-bold text-indigo-300 block mb-0.5">Izoh:</span>
                    {{ question.explanation }}
                  </div>
                </div>

              </div>
            }
          </div>
        }

      </div>
    }
  `
})
export class QuizResultComponent {
  readonly quizService = inject(QuizService);

  getQuizQuestions() {
    const result = this.quizService.latestResult();
    if (!result) return [];
    const quiz = this.quizService.quizzes().find(q => q.id === result.quizId);
    return quiz ? quiz.questions : [];
  }

  getUserAnswer(questionId: string) {
    const result = this.quizService.latestResult();
    if (!result) return null;
    return result.userAnswers.find(a => a.questionId === questionId) || null;
  }

  getOptionReviewClass(isUserSelected: boolean, isCorrectOption: boolean): string {
    if (isCorrectOption) {
      return 'p-3 rounded-xl border border-emerald-500/40 bg-emerald-500/10 text-emerald-200';
    }
    if (isUserSelected && !isCorrectOption) {
      return 'p-3 rounded-xl border border-rose-500/40 bg-rose-500/10 text-rose-200';
    }
    return 'p-3 rounded-xl border border-slate-800 bg-slate-900/40 text-slate-400';
  }

  getGradeTitle(percentage: number): string {
    if (percentage >= 90) return "A'lo! Mukammal Natija! 🌟";
    if (percentage >= 70) return "Barakalla! Yaxshi Natija! 👍";
    if (percentage >= 50) return "Qoniqarli Natija 👌";
    return "Yaxshiroq Tayyorgarlik Kerak 📚";
  }

  getGradeSubtitle(percentage: number): string {
    if (percentage >= 70) {
      return "Siz ushbu bo'lim bo'yicha mustahkam bilimga egasiz. Bilimlaringizni yanada boyitishda davom eting!";
    }
    return "Ushbu bo'limdagi tushunchalarni qayta ko'rib chiqishni va testni yana bir bor topshirishni tavsiya etamiz.";
  }

  formatTime(seconds: number): string {
    const m = Math.floor(seconds / 60);
    const s = seconds % 60;
    return `${m} daq ${s} son`;
  }
}
