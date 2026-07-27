import { Component, inject, input, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizService } from '../../services/quiz.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule],
  template: `
    <header class="sticky top-0 z-40 w-full glass-card border-b border-slate-800/80 bg-slate-950/80 backdrop-blur-md">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 h-16 flex items-center justify-between">
        
        <!-- Logo & Active Portal Indicator -->
        <div class="flex items-center gap-3 cursor-pointer" (click)="goHome()">
          <div class="w-10 h-10 rounded-xl bg-gradient-to-tr from-indigo-600 to-violet-500 flex items-center justify-center shadow-lg shadow-indigo-500/25">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6 text-white" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2.5" d="M9.663 17h4.673M12 3v1m6.364 1.636l-.707.707M21 12h-1M4 12H3m3.343-5.657l-.707-.707m2.828 9.9a5 5 0 117.072 0l-.548.547A3.374 3.374 0 0014 18.469V19a2 2 0 11-4 0v-.531c0-.895-.356-1.754-.988-2.386l-.548-.547z" />
            </svg>
          </div>
          <div>
            <span class="text-lg sm:text-xl font-extrabold tracking-tight text-white flex items-center gap-1.5">
              Quiz<span class="gradient-text">Master</span>
              <span [class]="activePortal() === 'admin' ? 
                'text-[10px] uppercase font-bold tracking-widest px-2 py-0.5 rounded-full bg-purple-500/10 text-purple-400 border border-purple-500/20' : 
                'text-[10px] uppercase font-bold tracking-widest px-2 py-0.5 rounded-full bg-indigo-500/10 text-indigo-400 border border-indigo-500/20'">
                {{ activePortal() === 'admin' ? 'ADMIN PORTAL' : 'USER REJIMI' }}
              </span>
            </span>
            <p class="text-xs text-slate-400 hidden sm:block">
              {{ activePortal() === 'admin' ? 'Boshqaruv & AI Generatsiya Paneli' : 'Interaktiv Test & Bilim Sinash Portali' }}
            </p>
          </div>
        </div>

        <!-- Role Mode Switcher & Navigation Actions -->
        <div class="hidden md:flex items-center gap-3">
          
          <!-- Mode Switcher Pill -->
          <div class="flex items-center p-1 rounded-xl bg-slate-900 border border-slate-800 text-xs">
            <button 
              (click)="switchPortal.emit('user')"
              [class]="activePortal() === 'user' ? 
                'flex items-center gap-1.5 px-3 py-1.5 rounded-lg font-bold text-white bg-indigo-600 shadow-md shadow-indigo-600/30 transition' : 
                'flex items-center gap-1.5 px-3 py-1.5 rounded-lg font-medium text-slate-400 hover:text-white transition'">
              <span>🎓</span> User Mode
            </button>

            <button 
              (click)="switchPortal.emit('admin')"
              [class]="activePortal() === 'admin' ? 
                'flex items-center gap-1.5 px-3 py-1.5 rounded-lg font-bold text-white bg-purple-600 shadow-md shadow-purple-600/30 transition' : 
                'flex items-center gap-1.5 px-3 py-1.5 rounded-lg font-medium text-slate-400 hover:text-white transition'">
              <span>⚙️</span> Admin Console
            </button>
          </div>

          <!-- USER PORTAL ACTIONS -->
          @if (activePortal() === 'user') {
            @if (quizService.currentUserName(); as userName) {
              <button 
                (click)="quizService.isNameModalOpen.set(true)"
                class="flex items-center gap-2 px-3 py-1.5 rounded-xl text-xs font-bold text-slate-200 bg-slate-900 border border-slate-800 hover:bg-slate-800 transition">
                <span class="w-2 h-2 rounded-full bg-emerald-400"></span>
                {{ userName }}
              </button>
            }

            @if (quizService.quizHistory().length > 0) {
              <button 
                (click)="toggleHistory.emit()"
                class="flex items-center gap-2 px-3 py-1.5 rounded-lg text-xs font-semibold text-slate-300 bg-slate-900 border border-slate-800 hover:bg-slate-800 transition">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-indigo-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
                Tarix ({{ quizService.quizHistory().length }})
              </button>
            }
          }

          <!-- ADMIN PORTAL ACTIONS -->
          @if (activePortal() === 'admin') {
            <button 
              (click)="openCreator.emit()"
              class="flex items-center gap-2 px-4 py-2 rounded-xl text-xs font-bold text-white bg-gradient-to-r from-purple-600 to-indigo-600 hover:from-purple-500 hover:to-indigo-500 shadow-md shadow-purple-600/30 transition-all hover:scale-105 active:scale-95">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2.5" d="M12 4v16m8-8H4" />
              </svg>
              Yangi Test Yaratish
            </button>
          }

        </div>

        <!-- Mobile Hamburger Button -->
        <button 
          (click)="isMobileMenuOpen.set(!isMobileMenuOpen())"
          class="md:hidden p-2 rounded-xl bg-slate-900 border border-slate-800 text-slate-300 hover:text-white hover:bg-slate-800 transition">
          <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path *ngIf="!isMobileMenuOpen()" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16" />
            <path *ngIf="isMobileMenuOpen()" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
          </svg>
        </button>

      </div>

      <!-- Mobile Sliding Navigation Drawer -->
      @if (isMobileMenuOpen()) {
        <div class="md:hidden border-t border-slate-800/80 bg-slate-950/95 px-4 pt-3 pb-5 space-y-3 backdrop-blur-xl animate-fadeIn">
          
          <!-- Mode Switcher in Mobile -->
          <div class="grid grid-cols-2 gap-2 p-1 rounded-xl bg-slate-900 border border-slate-800 text-xs mb-2">
            <button 
              (click)="switchPortal.emit('user'); isMobileMenuOpen.set(false)"
              [class]="activePortal() === 'user' ? 
                'flex items-center justify-center gap-1.5 py-2 rounded-lg font-bold text-white bg-indigo-600 shadow-md' : 
                'flex items-center justify-center gap-1.5 py-2 rounded-lg font-medium text-slate-400'">
              <span>🎓</span> User Mode
            </button>
            <button 
              (click)="switchPortal.emit('admin'); isMobileMenuOpen.set(false)"
              [class]="activePortal() === 'admin' ? 
                'flex items-center justify-center gap-1.5 py-2 rounded-lg font-bold text-white bg-purple-600 shadow-md' : 
                'flex items-center justify-center gap-1.5 py-2 rounded-lg font-medium text-slate-400'">
              <span>⚙️</span> Admin Console
            </button>
          </div>

          @if (activePortal() === 'user') {
            @if (quizService.currentUserName(); as userName) {
              <button 
                (click)="quizService.isNameModalOpen.set(true); isMobileMenuOpen.set(false)"
                class="w-full flex items-center justify-between px-4 py-2.5 rounded-xl text-xs font-bold text-slate-200 bg-slate-900 border border-slate-800">
                <span class="flex items-center gap-2">
                  <span class="w-2 h-2 rounded-full bg-emerald-400"></span>
                  {{ userName }}
                </span>
                <span class="text-[10px] text-slate-400">Profil</span>
              </button>
            }

            @if (quizService.quizHistory().length > 0) {
              <button 
                (click)="toggleHistory.emit(); isMobileMenuOpen.set(false)"
                class="w-full flex items-center justify-center gap-1.5 px-3 py-2.5 rounded-xl text-xs font-bold text-slate-300 bg-slate-900 border border-slate-800">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-indigo-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
                Tarix ({{ quizService.quizHistory().length }})
              </button>
            }
          }

          @if (activePortal() === 'admin') {
            <button 
              (click)="openCreator.emit(); isMobileMenuOpen.set(false)"
              class="w-full flex items-center justify-center gap-2 px-4 py-2.5 rounded-xl text-xs font-bold text-white bg-gradient-to-r from-purple-600 to-indigo-600 shadow-md">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2.5" d="M12 4v16m8-8H4" />
              </svg>
              Yangi Test Yaratish
            </button>
          }
        </div>
      }
    </header>
  `
})
export class NavbarComponent {
  readonly quizService = inject(QuizService);
  readonly activePortal = input<'user' | 'admin'>('user');
  readonly isMobileMenuOpen = signal<boolean>(false);

  readonly switchPortal = output<'user' | 'admin'>();
  readonly openCreator = output<void>();
  readonly toggleHistory = output<void>();

  goHome(): void {
    this.quizService.resetQuiz();
    this.switchPortal.emit('user');
    this.isMobileMenuOpen.set(false);
  }
}
