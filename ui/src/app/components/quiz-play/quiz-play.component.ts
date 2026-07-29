import { Component, inject, signal, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizService } from '../../services/quiz.service';
import { QuizApiService } from '../../services/quiz-api.service';
import { Question } from '../../models/quiz.model';
import { CodeEditorComponent } from '../code-editor/code-editor.component';

@Component({
  selector: 'app-quiz-play',
  standalone: true,
  imports: [CommonModule, CodeEditorComponent],
  template: `
    @if (quizService.currentQuiz(); as quiz) {
      <div class="max-w-4xl mx-auto px-4 sm:px-6 py-8 select-none">
        
        <!-- Anti-Cheating Warning Banner -->
        @if (cheatingWarningsCount() > 0) {
          <div class="mb-6 p-4 rounded-xl bg-amber-950/80 border border-amber-600/40 text-amber-300 flex items-center justify-between shadow-xl backdrop-blur-md animate-pulse">
            <div class="flex items-center space-x-3">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 text-amber-400 shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
              </svg>
              <div>
                <h4 class="text-xs font-bold uppercase tracking-wider text-amber-400">Anti-Cheating Ogohlantirish</h4>
                <p class="text-xs text-amber-200/90 mt-0.5">
                  Diqqat! Test vaqtida brauzer oynasini almashtirish taqiqlangan. Ogohlantirish: <strong>{{ cheatingWarningsCount() }} / 3</strong>
                </p>
              </div>
            </div>
            <span class="text-xs font-mono font-bold px-2.5 py-1 rounded bg-amber-500/20 border border-amber-500/30 text-amber-300">
              {{ 3 - cheatingWarningsCount() }} ta qoldi
            </span>
          </div>
        }

        <!-- Toast Warning Modal for Copy/Paste/RightClick -->
        @if (showViolationToast()) {
          <div class="fixed top-6 right-6 z-50 max-w-md p-4 rounded-2xl bg-rose-950/90 border border-rose-600/60 text-white shadow-2xl backdrop-blur-md flex items-start space-x-3 animate-bounce">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5 text-rose-400 shrink-0 mt-0.5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
            </svg>
            <div>
              <h4 class="text-xs font-bold text-rose-300 uppercase tracking-wider">Haqqoniy Baholash Qoidasi</h4>
              <p class="text-xs text-rose-100 mt-1">{{ violationMessage() }}</p>
            </div>
          </div>
        }

        <!-- Header Bar -->
        <div class="glass-card rounded-2xl p-4 sm:p-6 mb-6 border border-slate-800 flex items-center justify-between gap-4 flex-wrap">
          <div class="flex items-center gap-3">
            <button 
              (click)="showConfirmExit.set(true)"
              title="Chiqish"
              class="p-2 rounded-xl bg-slate-900 border border-slate-800 text-slate-400 hover:text-white hover:bg-slate-800 transition">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 19l-7-7m0 0l7-7m-7 7h18" />
              </svg>
            </button>
            <div>
              <div class="flex items-center gap-2">
                <span class="text-[10px] uppercase font-bold tracking-widest px-2 py-0.5 rounded-md bg-indigo-500/10 text-indigo-400 border border-indigo-500/20">
                  {{ quiz.categoryName }}
                </span>
                <span class="text-xs text-slate-400 font-medium">Savol {{ quizService.currentQuestionIndex() + 1 }} / {{ quiz.questions.length }}</span>
              </div>
              <h2 class="text-lg font-bold text-white line-clamp-1 mt-0.5">
                {{ quiz.title }}
              </h2>
            </div>
          </div>

          <!-- Timer Badge -->
          <div 
            [class]="quizService.timerSecondsLeft() < 60 ? 
              'flex items-center gap-2 px-4 py-2 rounded-xl bg-rose-500/10 border border-rose-500/30 text-rose-400 font-mono font-bold text-sm animate-pulse' : 
              'flex items-center gap-2 px-4 py-2 rounded-xl bg-slate-900 border border-slate-800 text-indigo-400 font-mono font-bold text-sm'">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
            </svg>
            {{ quizService.formattedTimer() }}
          </div>
        </div>

        <!-- Progress Bar -->
        <div class="w-full bg-slate-900 h-2.5 rounded-full overflow-hidden mb-8 border border-slate-800/60">
          <div 
            class="bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 h-full rounded-full transition-all duration-300 ease-out"
            [style.width.%]="quizService.progressPercentage()">
          </div>
        </div>

        <!-- Question Navigation Dots / Numbers -->
        <div class="flex items-center gap-1.5 overflow-x-auto pb-3 mb-6 scrollbar-none">
          @for (q of quiz.questions; track q.id; let i = $index) {
            <button 
              (click)="quizService.currentQuestionIndex.set(i)"
              [class]="i === quizService.currentQuestionIndex() ? 
                'w-9 h-9 rounded-xl font-bold text-xs bg-indigo-600 text-white border border-indigo-400 shadow-md shadow-indigo-600/30 flex items-center justify-center shrink-0' : 
                (quizService.userAnswers().has(q.id) ? 
                  'w-9 h-9 rounded-xl font-semibold text-xs bg-emerald-500/20 text-emerald-300 border border-emerald-500/30 flex items-center justify-center shrink-0' : 
                  'w-9 h-9 rounded-xl font-semibold text-xs bg-slate-900 text-slate-400 border border-slate-800 hover:bg-slate-800 flex items-center justify-center shrink-0')">
              {{ i + 1 }}
            </button>
          }
        </div>

        <!-- Question Card -->
        @if (quizService.currentQuestion(); as question) {
          <div class="glass-card rounded-2xl p-6 sm:p-8 mb-8 border border-slate-800">
            <div class="mb-6 flex items-start justify-between gap-4">
              <div>
                <span class="text-xs text-indigo-400 font-bold uppercase tracking-wider mb-2 block">
                  Savol #{{ quizService.currentQuestionIndex() + 1 }}
                </span>
                <h3 class="text-xl sm:text-2xl font-bold text-white leading-snug">
                  {{ question.text }}
                </h3>
              </div>
              
              <button 
                (click)="getAiHelp(question)"
                class="flex items-center gap-2 px-3.5 py-2 rounded-xl bg-purple-500/10 border border-purple-500/30 text-purple-300 hover:bg-purple-500/20 hover:border-purple-500/50 text-xs font-bold transition shadow-lg shadow-purple-500/10 shrink-0">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-purple-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 10V3L4 14h7v7l9-11h-7z" />
                </svg>
                <span>💡 AI Yordam</span>
              </button>
            </div>

            <!-- Optional Code Snippet Block -->
            @if (question.codeSnippet && !question.isCodeQuestion) {
              <div class="mb-8 rounded-xl bg-slate-950 border border-slate-800 p-4 sm:p-5 font-mono text-xs sm:text-sm text-indigo-200 overflow-x-auto relative group">
                <div class="absolute top-2 right-2 text-[10px] uppercase font-bold text-slate-500 px-2 py-0.5 rounded bg-slate-900 border border-slate-800">Code</div>
                <pre class="whitespace-pre-wrap leading-relaxed"><code>{{ question.codeSnippet }}</code></pre>
              </div>
            }

            <!-- Interactive Code Sandbox Editor -->
            @if (question.isCodeQuestion) {
              <app-code-editor
                [initialCode]="question.initialCodeTemplate || ''"
                [expectedOutput]="question.expectedOutput || ''"
                (codeSubmitted)="handleCodeAnswer(question.id, $event)"
                (cheatingAttempt)="triggerViolationToast($event)">
              </app-code-editor>
            }

            <!-- Standard Multiple Choice Options Grid -->
            @if (!question.isCodeQuestion) {
              <div class="space-y-3.5">
                @for (option of question.options; track option.id; let optIdx = $index) {
                  <div 
                    (click)="quizService.selectOption(option.id)"
                    [class]="quizService.currentSelectedOptionId() === option.id ? 
                      'p-4 rounded-xl border-2 border-indigo-500 bg-indigo-500/10 text-white flex items-start gap-4 cursor-pointer transition-all shadow-md shadow-indigo-500/10' : 
                      'p-4 rounded-xl border border-slate-800 bg-slate-900/60 hover:bg-slate-900 hover:border-slate-700 text-slate-300 flex items-start gap-4 cursor-pointer transition-all'">
                    
                    <div 
                      [class]="quizService.currentSelectedOptionId() === option.id ? 
                        'w-7 h-7 rounded-lg bg-indigo-600 text-white font-bold text-xs flex items-center justify-center shrink-0 mt-0.5 shadow-sm' : 
                        'w-7 h-7 rounded-lg bg-slate-800 text-slate-400 font-bold text-xs flex items-center justify-center shrink-0 mt-0.5 border border-slate-700'">
                      {{ getOptionLetter(optIdx) }}
                    </div>

                    <div class="text-sm font-medium leading-relaxed pt-0.5 flex-1">
                      {{ option.text }}
                    </div>

                    @if (quizService.currentSelectedOptionId() === option.id) {
                      <div class="w-5 h-5 rounded-full bg-indigo-500 text-white flex items-center justify-center shrink-0 mt-1">
                        <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="3" d="M5 13l4 4L19 7" />
                        </svg>
                      </div>
                    }
                  </div>
                }
              </div>
            }

            <!-- AI Explanation Modal / Expander Card -->
            @if (activeHelpQuestionId() === question.id) {
              <div class="mt-6 p-5 rounded-2xl bg-gradient-to-br from-purple-950/50 via-slate-900 to-slate-950 border border-purple-500/40 text-slate-200 shadow-2xl backdrop-blur-md">
                <div class="flex items-center justify-between mb-3 border-b border-purple-500/20 pb-3">
                  <div class="flex items-center gap-2 text-purple-300 font-bold text-sm">
                    <div class="w-7 h-7 rounded-lg bg-purple-500/20 border border-purple-500/30 flex items-center justify-center text-purple-400 shadow-sm">
                      <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 10V3L4 14h7v7l9-11h-7z" />
                      </svg>
                    </div>
                    <span>AI Tushuntirish & Ekspert Maslahati</span>
                  </div>
                  <button 
                    (click)="activeHelpQuestionId.set(null)"
                    class="text-xs text-slate-400 hover:text-white px-2.5 py-1 rounded-lg bg-slate-900 border border-slate-800 transition">
                    Yopish ✕
                  </button>
                </div>

                @if (loadingAiQuestionId() === question.id) {
                  <div class="flex items-center justify-center py-6 gap-3 text-purple-300 text-xs font-semibold">
                    <svg class="animate-spin h-5 w-5 text-purple-400" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                      <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                      <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                    </svg>
                    <span>Semantic Kernel AI savol va variantlarni tahlil qilmoqda...</span>
                  </div>
                } @else {
                  <div class="text-xs sm:text-sm leading-relaxed text-slate-200 space-y-2 whitespace-pre-line font-normal">
                    {{ aiExplanations()[question.id] }}
                  </div>
                }
              </div>
            }

          </div>
        }

        <!-- Bottom Controls -->
        <div class="flex items-center justify-between gap-4">
          <button 
            (click)="quizService.previousQuestion()"
            [disabled]="quizService.currentQuestionIndex() === 0"
            class="flex items-center gap-2 px-5 py-2.5 rounded-xl text-xs sm:text-sm font-bold text-slate-300 bg-slate-900 border border-slate-800 hover:bg-slate-800 disabled:opacity-40 disabled:cursor-not-allowed transition">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7" />
            </svg>
            Oldingisi
          </button>

          <div class="flex items-center gap-3">
            <button 
              (click)="quizService.finishQuiz()"
              class="px-4 py-2.5 rounded-xl text-xs font-bold text-rose-400 bg-rose-500/10 border border-rose-500/20 hover:bg-rose-500/20 transition">
              Testni yakunlash
            </button>

            <button 
              (click)="quizService.nextQuestion()"
              class="flex items-center gap-2 px-6 py-2.5 rounded-xl text-xs sm:text-sm font-bold text-white bg-gradient-to-r from-indigo-600 to-purple-600 hover:from-indigo-500 hover:to-purple-500 shadow-lg shadow-indigo-600/30 transition hover:scale-105 active:scale-95">
              @if (isLastQuestion()) {
                <span>Natijani Ko'rish</span>
              } @else {
                <span>Keyingisi</span>
              }
              <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7" />
              </svg>
            </button>
          </div>
        </div>

        <!-- Confirm Exit Modal -->
        @if (showConfirmExit()) {
          <div class="fixed inset-0 z-50 bg-slate-950/80 backdrop-blur-sm flex items-center justify-center p-4">
            <div class="glass-card rounded-2xl max-w-md w-full p-6 border border-slate-800 text-center">
              <div class="w-12 h-12 rounded-full bg-rose-500/10 border border-rose-500/20 text-rose-400 flex items-center justify-center mx-auto mb-4">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
                </svg>
              </div>
              <h3 class="text-lg font-bold text-white mb-2">Testdan chiqmoqchimisiz?</h3>
              <p class="text-xs text-slate-400 mb-6">Test holati saqlanmaydi va erishilgan natijangiz bekor qilinadi.</p>
              <div class="flex items-center justify-center gap-3">
                <button 
                  (click)="showConfirmExit.set(false)"
                  class="px-4 py-2 rounded-xl text-xs font-bold text-slate-300 bg-slate-900 border border-slate-800 hover:bg-slate-800 transition">
                  Davom etish
                </button>
                <button 
                  (click)="quizService.resetQuiz(); showConfirmExit.set(false)"
                  class="px-4 py-2 rounded-xl text-xs font-bold text-white bg-rose-600 hover:bg-rose-500 transition">
                  Ha, chiqish
                </button>
              </div>
            </div>
          </div>
        }

      </div>
    }
  `
})
export class QuizPlayComponent {
  readonly quizService = inject(QuizService);
  private readonly apiService = inject(QuizApiService);

  readonly showConfirmExit = signal<boolean>(false);

  readonly cheatingWarningsCount = signal<number>(0);
  readonly showViolationToast = signal<boolean>(false);
  readonly violationMessage = signal<string>('');

  readonly aiExplanations = signal<Record<string, string>>({});
  readonly loadingAiQuestionId = signal<string | null>(null);
  readonly activeHelpQuestionId = signal<string | null>(null);

  getAiHelp(question: Question): void {
    if (this.activeHelpQuestionId() === question.id) {
      this.activeHelpQuestionId.set(null);
      return;
    }

    this.activeHelpQuestionId.set(question.id);

    if (this.aiExplanations()[question.id]) {
      return; // Already cached
    }

    this.loadingAiQuestionId.set(question.id);

    const optionsText = question.options ? question.options.map(o => o.text) : [];
    this.apiService.explainQuestion({
      questionText: question.text,
      codeSnippet: question.codeSnippet,
      options: optionsText
    }).subscribe({
      next: (res) => {
        this.loadingAiQuestionId.set(null);
        if (res && res.explanation) {
          this.aiExplanations.update(prev => ({
            ...prev,
            [question.id]: res.explanation
          }));
        }
      },
      error: (err) => {
        this.loadingAiQuestionId.set(null);
        console.error('AI Explanation error:', err);
        this.aiExplanations.update(prev => ({
          ...prev,
          [question.id]: `🎯 **Savolning Asosiy Mazmuni**: Ushbu savol "${question.text}" bo'yicha bilimlarni sinashga qaratilgan.\n\n✅ **To'g'ri Javob Tahlili**: To'g'ri javob dasturlashning eng yaxshi amaliyotlariga (best practices) mos keladi.\n\n💡 **Ekspert Maslahati**: Dasturlashda resurslarni to'g'ri boshqarish va xatoliklarni oldini olish uchun standart tamoyillarga amal qiling.`
        }));
      }
    });
  }

  @HostListener('document:copy', ['$event'])
  @HostListener('document:cut', ['$event'])
  onCopyCutAttempt(event: ClipboardEvent): void {
    event.preventDefault();
    this.triggerViolationToast('Haqqoniy baholash uchun savol matnini nusxalash (Copy) taqiqlangan!');
  }

  @HostListener('document:contextmenu', ['$event'])
  onContextMenuAttempt(event: MouseEvent): void {
    event.preventDefault();
    this.triggerViolationToast('Test vaqtida sichqonchaning o\'ng tugmasi taqiqlangan!');
  }

  @HostListener('window:visibilitychange')
  @HostListener('window:blur')
  onWindowBlur(): void {
    if (document.hidden && this.quizService.currentQuiz()) {
      const newCount = this.cheatingWarningsCount() + 1;
      this.cheatingWarningsCount.set(newCount);

      if (newCount >= 3) {
        this.triggerViolationToast('Ogohlantirishlar soni oshdi (3/3). Test avtomatik topshirilmoqda!');
        setTimeout(() => this.quizService.finishQuiz(), 1500);
      } else {
        this.triggerViolationToast(`Boshqa oynaga o'tildi! Ogohlantirish: ${newCount} / 3`);
      }
    }
  }

  triggerViolationToast(msg: string): void {
    this.violationMessage.set(msg);
    this.showViolationToast.set(true);
    setTimeout(() => this.showViolationToast.set(false), 4000);
  }

  handleCodeAnswer(questionId: string, result: { code: string; isCorrect: boolean }): void {
    const currentQuiz = this.quizService.currentQuiz();
    if (!currentQuiz) return;

    const question = currentQuiz.questions.find(q => q.id === questionId);
    if (!question) return;

    const selectedOptionId = result.isCorrect ? question.correctOptionId : 'incorrect-code-option';
    this.quizService.selectOption(selectedOptionId);
  }

  getOptionLetter(index: number): string {
    return String.fromCharCode(65 + index);
  }

  isLastQuestion(): boolean {
    const quiz = this.quizService.currentQuiz();
    if (!quiz) return false;
    return this.quizService.currentQuestionIndex() === quiz.questions.length - 1;
  }
}
