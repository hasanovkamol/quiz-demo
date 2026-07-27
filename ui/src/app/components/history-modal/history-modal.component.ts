import { Component, inject, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizService } from '../../services/quiz.service';

@Component({
  selector: 'app-history-modal',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="fixed inset-0 z-50 bg-slate-950/80 backdrop-blur-md flex items-center justify-center p-4">
      <div class="glass-card rounded-3xl max-w-2xl w-full p-6 sm:p-8 border border-slate-800 my-8 shadow-2xl relative">
        
        <!-- Modal Header -->
        <div class="flex items-center justify-between border-b border-slate-800 pb-4 mb-6">
          <div class="flex items-center gap-3">
            <div class="w-10 h-10 rounded-xl bg-indigo-500/10 border border-indigo-500/20 text-indigo-400 flex items-center justify-center font-bold">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
              </svg>
            </div>
            <div>
              <h2 class="text-xl font-extrabold text-white">Natijalar Tarixi</h2>
              <p class="text-xs text-slate-400">Oldingi topshirilgan testlar ko'rsatkichlari</p>
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
            <div class="p-4 rounded-2xl bg-slate-900/80 border border-slate-800 flex items-center justify-between gap-4">
              <div>
                <div class="flex items-center gap-2 mb-1">
                  <span class="text-[10px] uppercase font-bold text-indigo-400 px-2 py-0.5 rounded bg-indigo-500/10 border border-indigo-500/20">
                    {{ item.categoryName }}
                  </span>
                  <span class="text-xs text-slate-500">{{ item.completedAt }}</span>
                </div>
                <h4 class="text-sm font-bold text-white">{{ item.quizTitle }}</h4>
                <p class="text-xs text-slate-400">
                  {{ item.correctAnswersCount }} / {{ item.totalQuestions }} to'g'ri ({{ Math.floor(item.totalTimeSpentSeconds / 60) }} daq {{ item.totalTimeSpentSeconds % 60 }} son)
                </p>
              </div>

              <div class="text-right">
                <span [class]="item.scorePercentage >= 70 ? 
                  'px-3 py-1 rounded-full text-xs font-extrabold bg-emerald-500/10 text-emerald-400 border border-emerald-500/20' : 
                  'px-3 py-1 rounded-full text-xs font-extrabold bg-rose-500/10 text-rose-400 border border-rose-500/20'">
                  {{ item.scorePercentage }}%
                </span>
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
  `
})
export class HistoryModalComponent {
  readonly quizService = inject(QuizService);
  readonly closeModal = output<void>();
  readonly Math = Math;
}
