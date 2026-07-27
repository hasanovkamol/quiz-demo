import { Component, inject, signal, output, OnInit, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuizService } from '../../services/quiz.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-user-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="fixed inset-0 z-50 bg-slate-950/85 backdrop-blur-md flex items-center justify-center p-4"
         (click)="onBackdropClick($event)">
      <div class="glass-card rounded-3xl max-w-md w-full p-6 sm:p-8 border border-indigo-500/30 shadow-2xl relative text-center"
           (click)="$event.stopPropagation()">

        <!-- Close Button -->
        <button 
          (click)="quizService.isNameModalOpen.set(false)"
          class="absolute top-4 right-4 p-1.5 rounded-lg text-slate-400 hover:text-white hover:bg-slate-800 transition">
          <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
          </svg>
        </button>

        <!-- Icon -->
        <div class="w-14 h-14 rounded-2xl bg-indigo-500/10 border border-indigo-500/20 text-indigo-400 flex items-center justify-center mx-auto mb-4 shadow-lg shadow-indigo-500/10">
          <svg xmlns="http://www.w3.org/2000/svg" class="w-7 h-7" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
          </svg>
        </div>

        <h2 class="text-xl font-extrabold text-white mb-1">Xush Kelibsiz!</h2>
        <p class="text-xs text-slate-400 mb-6">Test topshirish va natijalarni saqlash uchun tizimga kiring:</p>

        <!-- Google OAuth Sign-In Button -->
        <button 
          id="google-signin-btn"
          (click)="loginWithGoogle()"
          [disabled]="isGoogleLoading()"
          class="w-full py-3 px-4 mb-4 rounded-xl text-xs font-bold text-slate-900 bg-white hover:bg-slate-50 shadow-md transition flex items-center justify-center gap-3 border border-slate-200 disabled:opacity-60 disabled:cursor-not-allowed relative overflow-hidden group">
          @if (isGoogleLoading()) {
            <svg class="animate-spin w-4 h-4 text-slate-500" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
              <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
              <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
            </svg>
            Google Kirish Jarayoni...
          } @else {
            <svg class="w-4 h-4" viewBox="0 0 24 24">
              <path fill="#4285F4" d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z"/>
              <path fill="#34A853" d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z"/>
              <path fill="#FBBC05" d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.06H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.94l2.85-2.22.81-.63z"/>
              <path fill="#EA4335" d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.06l3.66 2.84c.87-2.6 3.3-4.52 6.16-4.52z"/>
            </svg>
            Google Account Bilan Kirish
          }
        </button>

        <!-- Error message -->
        @if (errorMessage()) {
          <div class="mb-4 px-4 py-2.5 rounded-xl bg-rose-500/10 border border-rose-500/20 text-rose-400 text-xs text-left flex items-start gap-2">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 mt-0.5 shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
            </svg>
            {{ errorMessage() }}
          </div>
        }

        <div class="flex items-center gap-3 my-4">
          <div class="h-px bg-slate-800 flex-1"></div>
          <span class="text-[10px] text-slate-500 font-bold uppercase">yoki Ismingizni Kiriting</span>
          <div class="h-px bg-slate-800 flex-1"></div>
        </div>

        <div class="mb-6 text-left">
          <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Ism va Familiya *</label>
          <input 
            type="text" 
            [(ngModel)]="userNameInput" 
            (keyup.enter)="confirmName()"
            placeholder="masalan: Alisher Navoiy" 
            class="w-full px-4 py-3 bg-slate-900 border border-slate-700 rounded-xl text-sm text-white focus:outline-none focus:border-indigo-500 focus:ring-2 focus:ring-indigo-500/30 transition">
        </div>

        <button 
          (click)="confirmName()"
          class="w-full py-3 rounded-xl text-sm font-bold text-white bg-gradient-to-r from-indigo-600 to-purple-600 hover:from-indigo-500 hover:to-purple-500 shadow-lg shadow-indigo-600/30 transition hover:scale-[1.02] active:scale-95">
          Davom Etish
        </button>

      </div>
    </div>
  `
})
export class UserModalComponent {
  readonly quizService = inject(QuizService);
  readonly authService = inject(AuthService);
  readonly nameSubmitted = output<string>();

  userNameInput = this.quizService.currentUserName() || '';
  readonly isGoogleLoading = signal<boolean>(false);
  readonly errorMessage = signal<string>('');

  onBackdropClick(event: MouseEvent): void {
    if (event.target === event.currentTarget) {
      this.quizService.isNameModalOpen.set(false);
    }
  }

  loginWithGoogle(): void {
    this.isGoogleLoading.set(true);
    this.errorMessage.set('');

    this.authService.triggerGoogleSignIn(
      (user) => {
        this.isGoogleLoading.set(false);
        this.quizService.setUserName(user.name);
        this.nameSubmitted.emit(user.name);
      },
      () => {
        this.isGoogleLoading.set(false);
        this.errorMessage.set(
          'Google Client ID sozlanmagan. Iltimos, ismingizni qo\'lda kiriting yoki Google Cloud Console\'dan Client ID oling.'
        );
      }
    );
  }

  confirmName(): void {
    const trimmed = this.userNameInput.trim();
    if (!trimmed) {
      this.errorMessage.set('Iltimos, Ismingizni kiriting!');
      return;
    }
    this.quizService.setUserName(trimmed);
    this.nameSubmitted.emit(trimmed);
  }
}
