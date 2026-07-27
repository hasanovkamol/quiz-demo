import { Component, inject, signal, output, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuizService } from '../../services/quiz.service';
import { AuthService } from '../../services/auth.service';
import { TelegramWebAppService } from '../../services/telegram-webapp.service';

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
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="1.8" d="M9 12l2 2 4-4m5.618-4.016A11.955 11.955 0 0112 2.944a11.955 11.955 0 01-8.618 3.04A12.02 12.02 0 003 9c0 5.591 3.824 10.29 9 11.622 5.176-1.332 9-6.03 9-11.622 0-1.042-.133-2.052-.382-3.016z" />
            </svg>
          </div>

          <!-- Header -->
          <h2 class="text-2xl font-black tracking-tight text-white mb-1.5">
            Tizimga Kirish
          </h2>
          <p class="text-xs font-medium text-slate-400 mb-6 leading-relaxed">
            Test topshirish va natijalarni saqlash uchun Google yoki Telegram profilidan foydalaning (Rol: <span class="text-indigo-400 font-bold">User</span>):
          </p>

          <!-- Telegram WebApp Login Option -->
          @if (tgService.isTelegramWebApp() || tgService.getFormattedUserName()) {
            <button 
              (click)="loginWithTelegram()"
              class="w-full py-3.5 px-5 mb-4 rounded-2xl text-xs font-bold text-white bg-gradient-to-r from-sky-500 to-blue-600 hover:from-sky-400 hover:to-blue-500 shadow-lg shadow-sky-500/25 transition-all duration-200 flex items-center justify-center gap-3">
              <svg class="w-5 h-5 fill-current" viewBox="0 0 24 24">
                <path d="M12 0C5.37 0 0 5.37 0 12s5.37 12 12 12 12-5.37 12-12S18.63 0 12 0zm5.56 8.16l-2.03 9.56c-.15.68-.56.84-1.13.53l-3.1-2.28-1.5 1.44c-.16.16-.3.3-.61.3l.22-3.17 5.77-5.21c.25-.22-.05-.34-.39-.12l-7.14 4.5-3.07-.96c-.67-.21-.68-.67.14-.99l12.02-4.63c.56-.21 1.05.13.83.99z"/>
              </svg>
              Telegram Profilingiz Bilan Kirish ({{ tgService.getFormattedUserName() }})
            </button>
            
            <div class="relative flex items-center justify-center my-4">
              <div class="absolute inset-0 flex items-center"><div class="w-full border-t border-slate-800"></div></div>
              <span class="relative px-3 bg-slate-900 text-[10px] font-extrabold tracking-widest text-slate-500 uppercase">YOKI GOOGLE ILE</span>
            </div>
          }

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
                Google Orqali Kirish (User)
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

          <!-- Security Notice -->
          <div class="mt-5 pt-4 border-t border-slate-800/80 text-[11px] text-slate-500 flex items-center justify-center gap-1.5">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5 text-indigo-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z" />
            </svg>
            <span>Autentifikatsiya roli: <b>User</b></span>
          </div>

        </div>
      </div>
    </div>
  `
})
export class UserModalComponent implements AfterViewInit {
  readonly quizService = inject(QuizService);
  readonly authService = inject(AuthService);
  readonly tgService = inject(TelegramWebAppService);
  readonly nameSubmitted = output<string>();

  @ViewChild('googleBtnContainer') googleBtnContainer!: ElementRef<HTMLDivElement>;

  userNameInput = '';
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

  loginWithTelegram(): void {
    const tgName = this.tgService.getFormattedUserName();
    if (tgName) {
      this.confirmName(tgName);
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

  confirmName(customName?: string): void {
    const targetName = customName || this.userNameInput || this.tgService.getFormattedUserName();
    if (!targetName || !targetName.trim()) {
      this.errorMessage.set('Iltimos Google yoki Telegram orqali autentifikatsiyadan o\'ting.');
      return;
    }

    this.errorMessage.set('');
    const name = targetName.trim();
    this.quizService.setUserName(name);
    this.nameSubmitted.emit(name);
    this.quizService.isNameModalOpen.set(false);
  }
}
