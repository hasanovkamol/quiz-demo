import { Component, inject, signal, output, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuizService } from '../../services/quiz.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-user-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="fixed inset-0 z-50 bg-slate-950/80 backdrop-blur-md flex items-center justify-center p-4 transition-all duration-300"
         (click)="onBackdropClick($event)">
      
      <!-- Outer Card Border Glow Wrapper -->
      <div class="relative w-full max-w-md p-[1px] rounded-3xl bg-gradient-to-b from-indigo-500/40 via-purple-500/20 to-slate-800/50 shadow-2xl shadow-indigo-500/10 overflow-hidden"
           (click)="$event.stopPropagation()">
        
        <!-- Glow accent background blur -->
        <div class="absolute -top-24 -left-24 w-48 h-48 bg-indigo-500/20 rounded-full blur-3xl pointer-events-none"></div>
        <div class="absolute -bottom-24 -right-24 w-48 h-48 bg-purple-500/20 rounded-full blur-3xl pointer-events-none"></div>

        <div class="bg-slate-900/95 backdrop-blur-2xl rounded-3xl p-6 sm:p-8 text-center relative z-10">

          <!-- Close Button -->
          <button 
            (click)="quizService.isNameModalOpen.set(false)"
            class="absolute top-4 right-4 p-2 rounded-full text-slate-400 hover:text-white hover:bg-slate-800/80 transition-all duration-200 border border-transparent hover:border-slate-700/60"
            title="Yopish">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
            </svg>
          </button>

          <!-- Top Icon Badge with Glow -->
          <div class="relative w-16 h-16 rounded-2xl bg-gradient-to-br from-indigo-500/20 via-purple-500/20 to-pink-500/20 border border-indigo-500/30 text-indigo-400 flex items-center justify-center mx-auto mb-5 shadow-lg shadow-indigo-500/15 group">
            <div class="absolute inset-0 rounded-2xl bg-indigo-500/10 blur-sm group-hover:blur-md transition-all"></div>
            <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8 relative z-10 text-indigo-300" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="1.8" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
            </svg>
          </div>

          <!-- Header -->
          <h2 class="text-2xl font-black tracking-tight text-white mb-1.5">
            Xush Kelibsiz!
          </h2>
          <p class="text-xs font-medium text-slate-400 mb-6 leading-relaxed">
            Test topshirish va natijalaringizni saqlash uchun tizimga kiring:
          </p>

          <!-- Official Google GIS Button Container -->
          <div class="flex justify-center mb-3 min-h-[44px]">
            <div #googleBtnContainer class="rounded-full overflow-hidden shadow-md"></div>
          </div>

          <!-- Custom Google Fallback Trigger (Only if GIS Container fails or loading) -->
          @if (!hasOfficialGoogleBtn()) {
            <button 
              id="google-signin-btn"
              (click)="loginWithGoogle()"
              [disabled]="isGoogleLoading()"
              class="w-full py-3.5 px-5 mb-4 rounded-2xl text-xs font-bold text-slate-900 bg-white hover:bg-slate-50 active:bg-slate-100 shadow-lg shadow-white/5 transition-all duration-200 flex items-center justify-center gap-3 border border-slate-200 disabled:opacity-60 disabled:cursor-not-allowed">
              @if (isGoogleLoading()) {
                <svg class="animate-spin w-4 h-4 text-slate-600" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                  <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                  <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                </svg>
                Google Kirish Jarayoni...
              } @else {
                <svg class="w-4 h-4 shrink-0" viewBox="0 0 24 24">
                  <path fill="#4285F4" d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z"/>
                  <path fill="#34A853" d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z"/>
                  <path fill="#FBBC05" d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.06H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.94l2.85-2.22.81-.63z"/>
                  <path fill="#EA4335" d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.06l3.66 2.84c.87-2.6 3.3-4.52 6.16-4.52z"/>
                </svg>
                Google Orqali Kirish
              }
            </button>
          }

          <!-- Error message alert -->
          @if (errorMessage()) {
            <div class="mb-4 px-4 py-3 rounded-2xl bg-rose-500/10 border border-rose-500/20 text-rose-300 text-xs text-left flex items-start gap-2.5 animate-fade-in">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 mt-0.5 shrink-0 text-rose-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
              </svg>
              <span>{{ errorMessage() }}</span>
            </div>
          }

          <!-- Divider -->
          <div class="relative flex items-center justify-center my-5">
            <div class="absolute inset-0 flex items-center"><div class="w-full border-t border-slate-800"></div></div>
            <span class="relative px-3 bg-slate-900 text-[10px] font-extrabold tracking-widest text-slate-500 uppercase">yoki ismingiz bilan kiring</span>
          </div>

          <!-- Name Input Form -->
          <div class="mb-5 text-left">
            <label class="block text-[11px] font-extrabold text-slate-400 uppercase tracking-wider mb-2">Ism va Familiya *</label>
            <div class="relative">
              <div class="absolute inset-y-0 left-0 pl-3.5 flex items-center pointer-events-none text-slate-500">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
                </svg>
              </div>
              <input 
                type="text" 
                [(ngModel)]="userNameInput" 
                (keyup.enter)="confirmName()"
                placeholder="masalan: Alisher Navoiy" 
                class="w-full pl-10 pr-4 py-3.5 bg-slate-950/70 border border-slate-800 rounded-2xl text-sm font-medium text-white placeholder-slate-600 focus:outline-none focus:border-indigo-500 focus:ring-4 focus:ring-indigo-500/15 transition-all duration-200">
            </div>
          </div>

          <!-- Submit Button -->
          <button 
            (click)="confirmName()"
            class="w-full py-3.5 px-6 rounded-2xl text-sm font-bold text-white bg-gradient-to-r from-indigo-600 via-purple-600 to-pink-600 hover:from-indigo-500 hover:via-purple-500 hover:to-pink-500 shadow-lg shadow-indigo-600/25 hover:shadow-indigo-600/40 transition-all duration-200 hover:scale-[1.01] active:scale-[0.98] flex items-center justify-center gap-2 group">
            <span>Davom Etish</span>
            <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 transition-transform group-hover:translate-x-1" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M14 5l7 7m0 0l-7 7m7-7H3" />
            </svg>
          </button>

        </div>
      </div>
    </div>
  `
})
export class UserModalComponent implements AfterViewInit {
  readonly quizService = inject(QuizService);
  readonly authService = inject(AuthService);
  readonly nameSubmitted = output<string>();

  @ViewChild('googleBtnContainer') googleBtnContainer!: ElementRef<HTMLDivElement>;

  userNameInput = this.quizService.currentUserName() || '';
  readonly isGoogleLoading = signal<boolean>(false);
  readonly hasOfficialGoogleBtn = signal<boolean>(false);
  readonly errorMessage = signal<string>('');

  ngAfterViewInit(): void {
    if (this.googleBtnContainer?.nativeElement) {
      this.authService.renderGoogleButton(
        this.googleBtnContainer.nativeElement,
        (user) => {
          this.isGoogleLoading.set(false);
          this.quizService.setUserName(user.name);
          this.nameSubmitted.emit(user.name);
        },
        () => {
          this.isGoogleLoading.set(false);
        }
      );

      // Check if official iframe button rendered inside container
      setTimeout(() => {
        if (this.googleBtnContainer.nativeElement.childElementCount > 0) {
          this.hasOfficialGoogleBtn.set(true);
        }
      }, 500);
    }
  }

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
          'Google Cloud Console da Authorized JavaScript origins sozlanmagan. Iltimos Cloud Console ga HTTPS havolangizni qo\'shing.'
        );
      }
    );
  }

  confirmName(): void {
    if (!this.userNameInput || !this.userNameInput.trim()) {
      this.errorMessage.set('Iltimos ismingizni kiriting');
      return;
    }

    this.errorMessage.set('');
    const name = this.userNameInput.trim();
    this.quizService.setUserName(name);
    this.nameSubmitted.emit(name);
    this.quizService.isNameModalOpen.set(false);
  }
}
