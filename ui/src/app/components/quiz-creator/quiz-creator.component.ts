import { Component, inject, signal, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuizService } from '../../services/quiz.service';
import { Quiz, Question, QuestionOption, QuizCategory, Difficulty } from '../../models/quiz.model';

@Component({
  selector: 'app-quiz-creator',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="fixed inset-0 z-50 bg-slate-950/80 backdrop-blur-md flex items-center justify-center p-4 overflow-y-auto">
      <div class="glass-card rounded-3xl max-w-3xl w-full p-6 sm:p-8 border border-slate-800 my-8 shadow-2xl relative">
        
        <!-- Modal Header -->
        <div class="flex items-center justify-between border-b border-slate-800 pb-4 mb-6">
          <div class="flex items-center gap-3">
            <div class="w-10 h-10 rounded-xl bg-purple-500/10 border border-purple-500/20 text-purple-400 flex items-center justify-center font-bold">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4v16m8-8H4" />
              </svg>
            </div>
            <div>
              <h2 class="text-xl font-extrabold text-white">Yangi Maxsus Test Yaratish</h2>
              <p class="text-xs text-slate-400">O'zingizning savol to'plamingizni tuzing va saqlang</p>
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

        <!-- Form Body -->
        <div class="space-y-6 max-h-[70vh] overflow-y-auto pr-2 scrollbar-thin">
          
          <!-- General Details -->
          <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
            <div>
              <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Test Nomi *</label>
              <input 
                type="text" 
                [(ngModel)]="quizTitle" 
                placeholder="masalan: TypeScript Advance Quiz" 
                class="w-full px-4 py-2.5 bg-slate-900 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-indigo-500">
            </div>

            <div>
              <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Kategoriya</label>
              <select 
                [(ngModel)]="category" 
                class="w-full px-4 py-2.5 bg-slate-900 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-indigo-500">
                <option value="angular">Angular Framework</option>
                <option value="dotnet">C# & .NET Core</option>
                <option value="webdev">Web Infrastructure</option>
                <option value="custom">Maxsus Test</option>
              </select>
            </div>

            <div>
              <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Qiyinchilik Darajasi</label>
              <select 
                [(ngModel)]="difficulty" 
                class="w-full px-4 py-2.5 bg-slate-900 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-indigo-500">
                <option value="Oson">Oson</option>
                <option value="O'rta">O'rta</option>
                <option value="Qiyin">Qiyin</option>
              </select>
            </div>

            <div>
              <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Vaqt Chegarasi (Daqiqa)</label>
              <input 
                type="number" 
                [(ngModel)]="timeLimitMinutes" 
                min="1" max="60" 
                class="w-full px-4 py-2.5 bg-slate-900 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-indigo-500">
            </div>
          </div>

          <div>
            <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Tavsif (Description)</label>
            <textarea 
              [(ngModel)]="description" 
              rows="2" 
              placeholder="Test haqida qisqacha ma'lumot..." 
              class="w-full px-4 py-2.5 bg-slate-900 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-indigo-500"></textarea>
          </div>

          <!-- Questions Header -->
          <div class="border-t border-slate-800 pt-6">
            <div class="flex items-center justify-between mb-4">
              <h3 class="text-base font-bold text-white flex items-center gap-2">
                Savollar ro'yxati ({{ questions().length }})
              </h3>
              <button 
                (click)="addQuestion()" 
                class="px-3 py-1.5 rounded-xl text-xs font-bold text-indigo-400 bg-indigo-500/10 border border-indigo-500/20 hover:bg-indigo-500/20 transition flex items-center gap-1">
                + Yangi Savol Qo'shish
              </button>
            </div>

            <!-- Questions Items -->
            <div class="space-y-6">
              @for (q of questions(); track q.id; let qIdx = $index) {
                <div class="p-5 rounded-2xl bg-slate-900/80 border border-slate-800 relative">
                  
                  <div class="flex items-center justify-between mb-3">
                    <span class="text-xs font-bold text-indigo-400">Savol #{{ qIdx + 1 }}</span>
                    @if (questions().length > 1) {
                      <button 
                        (click)="removeQuestion(qIdx)" 
                        class="text-slate-500 hover:text-rose-400 text-xs font-semibold transition">
                        O'chirish
                      </button>
                    }
                  </div>

                  <!-- Question Text -->
                  <input 
                    type="text" 
                    [(ngModel)]="q.text" 
                    placeholder="Savol matnini kiriting..." 
                    class="w-full px-4 py-2 bg-slate-950 border border-slate-800 rounded-xl text-xs text-white mb-3 focus:outline-none focus:border-indigo-500">

                  <!-- Optional Code Snippet -->
                  <textarea 
                    [(ngModel)]="q.codeSnippet" 
                    rows="2" 
                    placeholder="Kod parchasi (Ixtiyoriy)..." 
                    class="w-full px-4 py-2 bg-slate-950 border border-slate-800 rounded-xl text-xs font-mono text-indigo-300 mb-4 focus:outline-none focus:border-indigo-500"></textarea>

                  <!-- Options (A, B, C, D) -->
                  <div class="space-y-2 mb-4">
                    <label class="block text-[11px] font-bold text-slate-400 uppercase tracking-wider">Javob Variantlari (To'g'ri javobni tanlang)</label>
                    @for (opt of q.options; track opt.id; let optIdx = $index) {
                      <div class="flex items-center gap-2">
                        <input 
                          type="radio" 
                          [name]="'correct-opt-' + q.id" 
                          [checked]="q.correctOptionId === opt.id" 
                          (change)="q.correctOptionId = opt.id" 
                          class="w-4 h-4 text-indigo-600 focus:ring-indigo-500">
                        <input 
                          type="text" 
                          [(ngModel)]="opt.text" 
                          [placeholder]="'Variant ' + getLetter(optIdx)" 
                          class="w-full px-3 py-1.5 bg-slate-950 border border-slate-800 rounded-lg text-xs text-slate-200 focus:outline-none focus:border-indigo-500">
                      </div>
                    }
                  </div>

                  <!-- Explanation -->
                  <input 
                    type="text" 
                    [(ngModel)]="q.explanation" 
                    placeholder="To'g'ri javob uchun izoh (Explanation)..." 
                    class="w-full px-4 py-2 bg-slate-950 border border-slate-800 rounded-xl text-xs text-slate-300 focus:outline-none focus:border-indigo-500">

                </div>
              }
            </div>

          </div>

        </div>

        <!-- Footer Submit Button -->
        <div class="border-t border-slate-800 pt-4 mt-6 flex items-center justify-end gap-3">
          <button 
            (click)="closeModal.emit()" 
            class="px-5 py-2.5 rounded-xl text-xs font-bold text-slate-400 hover:text-white transition">
            Bekor qilish
          </button>
          <button 
            (click)="saveQuiz()" 
            class="px-6 py-2.5 rounded-xl text-xs font-bold text-white bg-indigo-600 hover:bg-indigo-500 shadow-md shadow-indigo-600/30 transition">
            Testni Saqlash
          </button>
        </div>

      </div>
    </div>
  `
})
export class QuizCreatorComponent {
  readonly quizService = inject(QuizService);
  readonly closeModal = output<void>();

  quizTitle = '';
  category: QuizCategory = 'custom';
  difficulty: Difficulty = 'O\'rta';
  timeLimitMinutes = 5;
  description = '';

  readonly questions = signal<Question[]>([
    this.createEmptyQuestion('1')
  ]);

  private createEmptyQuestion(idSuffix: string): Question {
    const qId = 'custom-q-' + idSuffix;
    const optAId = 'opt-a-' + idSuffix;
    const optBId = 'opt-b-' + idSuffix;
    const optCId = 'opt-c-' + idSuffix;
    const optDId = 'opt-d-' + idSuffix;

    return {
      id: qId,
      text: '',
      codeSnippet: '',
      options: [
        { id: optAId, text: '' },
        { id: optBId, text: '' },
        { id: optCId, text: '' },
        { id: optDId, text: '' }
      ],
      correctOptionId: optAId,
      explanation: ''
    };
  }

  addQuestion(): void {
    const count = this.questions().length + 1;
    this.questions.set([...this.questions(), this.createEmptyQuestion(count.toString() + '-' + Date.now())]);
  }

  removeQuestion(index: number): void {
    if (this.questions().length <= 1) return;
    const updated = [...this.questions()];
    updated.splice(index, 1);
    this.questions.set(updated);
  }

  getLetter(idx: number): string {
    return String.fromCharCode(65 + idx);
  }

  saveQuiz(): void {
    if (!this.quizTitle.trim()) {
      alert("Iltimos, test nomini kiriting!");
      return;
    }

    const qs = this.questions();
    for (let i = 0; i < qs.length; i++) {
      if (!qs[i].text.trim()) {
        alert(`Iltimos, #${i + 1} savol matnini to'ldiring!`);
        return;
      }
    }

    const newQuiz: Quiz = {
      id: 'quiz-custom-' + Date.now(),
      title: this.quizTitle,
      category: this.category,
      categoryName: this.category === 'angular' ? 'Angular Framework' : (this.category === 'dotnet' ? 'C# & .NET Core' : 'Maxsus Test'),
      description: this.description || 'Foydalanuvchi tomonidan yaratilgan maxsus test.',
      iconName: 'sparkles',
      difficulty: this.difficulty,
      timeLimitSeconds: Math.max(1, this.timeLimitMinutes) * 60,
      questions: qs,
      isCustom: true
    };

    this.quizService.addCustomQuiz(newQuiz);
    this.closeModal.emit();
  }
}
