import { Component, Input, Output, EventEmitter, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-code-editor',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="rounded-xl border border-slate-700/80 bg-slate-900/90 shadow-2xl backdrop-blur-md overflow-hidden my-4">
      <!-- Editor Header Bar -->
      <div class="flex items-center justify-between px-4 py-2.5 bg-slate-950/80 border-b border-slate-800">
        <div class="flex items-center space-x-2">
          <div class="w-3 h-3 rounded-full bg-rose-500/80"></div>
          <div class="w-3 h-3 rounded-full bg-amber-500/80"></div>
          <div class="w-3 h-3 rounded-full bg-emerald-500/80"></div>
          <span class="text-xs font-mono text-slate-400 ml-2">solution.ts</span>
        </div>
        <button
          (click)="runCode()"
          [disabled]="isRunning()"
          class="flex items-center space-x-1.5 px-3 py-1.5 rounded-lg bg-emerald-600 hover:bg-emerald-500 text-white text-xs font-semibold shadow-lg shadow-emerald-600/20 transition-all disabled:opacity-50">
          <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5 fill-current" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M14.752 11.168l-3.197-2.132A1 1 0 0010 9.87v4.263a1 1 0 001.555.832l3.197-2.132a1 1 0 000-1.664z" />
          </svg>
          <span>{{ isRunning() ? 'Bajarilmoqda...' : 'Kodni Tekshirish' }}</span>
        </button>
      </div>

      <!-- Main Editor Container -->
      <div class="relative flex min-h-[160px] font-mono text-sm">
        <!-- Line Numbers -->
        <div class="w-10 py-3 bg-slate-950/50 border-r border-slate-800 text-right pr-3 select-none text-slate-600 font-mono text-xs">
          <div *ngFor="let num of lineNumbers()">{{ num }}</div>
        </div>

        <!-- Code Input Textarea -->
        <textarea
          [ngModel]="code()"
          (ngModelChange)="onCodeChange($event)"
          (keydown)="handleKeyDown($event)"
          (paste)="onPasteAttempt($event)"
          (copy)="onCopyAttempt($event)"
          spellcheck="false"
          placeholder="// kodingizni shu yerga yozing..."
          class="w-full p-3 bg-transparent text-emerald-300 font-mono text-sm focus:outline-none resize-none leading-6 selection:bg-indigo-500/30">
        </textarea>
      </div>

      <!-- Paste Violation Alert -->
      <div *ngIf="showPasteWarning()" class="bg-rose-950/90 border-t border-rose-700/50 px-4 py-2 flex items-center space-x-2 text-rose-300 text-xs animate-pulse">
        <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-rose-400 shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
        </svg>
        <span><strong>Taqiqlangan:</strong> Test davomida kodingizni tashqaridan nusxalash/qo'yish (Paste) mumkin emas! Kodingizni qo'lda yozing.</span>
      </div>

      <!-- Output Console Drawer -->
      <div *ngIf="executionResult()" class="border-t border-slate-800 bg-slate-950/90 px-4 py-3">
        <div class="flex items-center justify-between mb-1.5">
          <span class="text-xs font-semibold text-slate-400 uppercase tracking-wider">Ijro Natijasi Console</span>
          <span [class]="executionResult()?.success ? 'text-emerald-400 font-bold text-xs flex items-center gap-1' : 'text-rose-400 font-bold text-xs flex items-center gap-1'">
            {{ executionResult()?.success ? 'Toʻgʻri' : 'Xato' }}
          </span>
        </div>
        <pre class="text-xs font-mono p-2.5 rounded-lg bg-slate-900 border border-slate-800 text-slate-300 overflow-x-auto whitespace-pre-wrap">{{ executionResult()?.output }}</pre>
      </div>
    </div>
  `
})
export class CodeEditorComponent {
  @Input() set initialCode(val: string) {
    this.code.set(val || '');
  }
  @Input() expectedOutput: string = '';

  @Output() codeSubmitted = new EventEmitter<{ code: string; isCorrect: boolean }>();
  @Output() cheatingAttempt = new EventEmitter<string>();

  readonly code = signal<string>('');
  readonly isRunning = signal<boolean>(false);
  readonly showPasteWarning = signal<boolean>(false);
  readonly executionResult = signal<{ success: boolean; output: string } | null>(null);

  lineNumbers(): number[] {
    const lines = this.code().split('\n').length;
    return Array.from({ length: Math.max(lines, 6) }, (_, i) => i + 1);
  }

  onCodeChange(val: string): void {
    this.code.set(val);
  }

  handleKeyDown(event: KeyboardEvent): void {
    if (event.key === 'Tab') {
      event.preventDefault();
      const textarea = event.target as HTMLTextAreaElement;
      const start = textarea.selectionStart;
      const end = textarea.selectionEnd;
      const currentCode = this.code();

      const newCode = currentCode.substring(0, start) + '  ' + currentCode.substring(end);
      this.code.set(newCode);

      setTimeout(() => {
        textarea.selectionStart = textarea.selectionEnd = start + 2;
      });
    }
  }

  onPasteAttempt(event: ClipboardEvent): void {
    event.preventDefault();
    this.showPasteWarning.set(true);
    this.cheatingAttempt.emit('Kod kiritish joyiga nusxa qo\'yish (Paste) taqiqlangan!');
    setTimeout(() => this.showPasteWarning.set(false), 5000);
  }

  onCopyAttempt(event: ClipboardEvent): void {
    event.preventDefault();
    this.cheatingAttempt.emit('Kodni nusxalash (Copy) taqiqlangan!');
  }

  runCode(): void {
    this.isRunning.set(true);
    this.executionResult.set(null);

    setTimeout(() => {
      try {
        const userCode = this.code();
        let logs: string[] = [];
        const customConsole = {
          log: (...args: any[]) => logs.push(args.map(a => typeof a === 'object' ? JSON.stringify(a) : a).join(' ')),
          error: (...args: any[]) => logs.push('ERROR: ' + args.join(' '))
        };

        const runner = new Function('console', userCode);
        runner(customConsole);

        const actualOutput = logs.join('\n').trim();
        const expected = (this.expectedOutput || '').trim();

        const success = expected ? actualOutput === expected : actualOutput.length > 0;
        const outputText = actualOutput || (success ? 'Kod muvaffaqiyatli bajarildi.' : 'Konsolga hech narsa chiqarilmadi.');

        this.executionResult.set({
          success,
          output: `[Chiquvchi Natija]:\n${outputText}`
        });

        this.codeSubmitted.emit({ code: userCode, isCorrect: success });
      } catch (err: any) {
        this.executionResult.set({
          success: false,
          output: `[Xatolik]: ${err.message}`
        });
        this.codeSubmitted.emit({ code: this.code(), isCorrect: false });
      } finally {
        this.isRunning.set(false);
      }
    }, 400);
  }
}
