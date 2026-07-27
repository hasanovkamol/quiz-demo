import { Component, inject, input, output, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizService } from '../../services/quiz.service';
import { AuthService } from '../../services/auth.service';
import { KeycloakService } from '../../services/keycloak.service';

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

        <!-- Desktop Navigation -->
        <div class="hidden md:flex items-center gap-3">
          
          <!-- Role Switcher Pill -->
          <div class="flex items-center p-1 rounded-xl bg-slate-900 border border-slate-800 text-xs">
            <button 
              (click)="switchPortal.emit('user')"
              [class]="activePortal() === 'user' ? 
                'flex items-center gap-1.5 px-3 py-1.5 rounded-lg font-bold text-white bg-indigo-600 shadow-md shadow-indigo-600/30 transition' : 
                'flex items-center gap-1.5 px-3 py-1.5 rounded-lg font-medium text-slate-400 hover:text-white transition'">
              <span>🎓</span> User Mode
            </button>
            @if (authService.isAdmin()) {
              <button 
                (click)="switchPortal.emit('admin')"
                [class]="activePortal() === 'admin' ? 
                  'flex items-center gap-1.5 px-3 py-1.5 rounded-lg font-bold text-white bg-purple-600 shadow-md shadow-purple-600/30 transition' : 
                  'flex items-center gap-1.5 px-3 py-1.5 rounded-lg font-medium text-slate-400 hover:text-white transition'">
                <span>⚙️</span> Admin Console
              </button>
            }
          </div>

          <!-- USER PORTAL ACTIONS -->
          @if (activePortal() === 'user') {
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

          <!-- LOGGED IN USER PROFILE + SIGN OUT -->
          @if (activeUser; as user) {
            <div class="flex items-center gap-2 pl-2 border-l border-slate-800">
              <!-- Avatar -->
              @if (user.pictureUrl) {
                <img [src]="user.pictureUrl" [alt]="user.name"
                  class="w-8 h-8 rounded-full border-2 border-indigo-500/50 object-cover" />
              } @else {
                <div class="w-8 h-8 rounded-full bg-indigo-600 border-2 border-indigo-500/50 flex items-center justify-center text-white text-xs font-extrabold shadow-md shadow-indigo-600/30">
                  {{ user.name.charAt(0).toUpperCase() }}
                </div>
              }
              <!-- Name -->
              <div class="hidden lg:block">
                <div class="text-xs font-bold text-white leading-tight max-w-[120px] truncate">{{ user.name }}</div>
                <div class="text-[10px] text-slate-400 font-medium">{{ user.role }}</div>
              </div>
              <!-- Sign Out Button -->
              <button 
                (click)="signOut()"
                title="Chiqish (Sign Out)"
                class="flex items-center gap-1.5 px-2.5 py-1.5 rounded-xl text-xs font-bold text-rose-400 bg-rose-500/10 border border-rose-500/20 hover:bg-rose-500/20 hover:text-rose-300 transition-all duration-200 ml-1">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1" />
                </svg>
                <span class="hidden lg:inline">Chiqish</span>
              </button>
            </div>
          } @else {
            <!-- Not signed in: show login buttons -->
            <div class="flex items-center gap-2">
              <!-- Keycloak Login -->
              <button
                (click)="loginWithKeycloak()"
                class="flex items-center gap-2 px-3.5 py-2 rounded-xl text-xs font-bold text-white bg-gradient-to-r from-orange-500 to-red-500 hover:from-orange-400 hover:to-red-400 shadow-md shadow-orange-500/20 transition-all duration-200 hover:scale-[1.02] active:scale-95"
                title="Keycloak orqali kirish">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 7a2 2 0 012 2m4 0a6 6 0 01-7.743 5.743L11 17H9v2H7v2H4a1 1 0 01-1-1v-2.586a1 1 0 01.293-.707l5.964-5.964A6 6 0 1121 9z" />
                </svg>
                Kirish
              </button>
              <!-- Google / Modal Login -->
              <button
                (click)="quizService.isNameModalOpen.set(true)"
                class="flex items-center gap-2 px-3 py-2 rounded-xl text-xs font-semibold text-slate-300 bg-slate-900 border border-slate-800 hover:bg-slate-800 transition-all duration-200"
                title="Mehmonsiz davom etish">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
                </svg>
                Mehmon
              </button>
            </div>
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
          @if (authService.isAdmin()) {
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
          }

          @if (activePortal() === 'user' && quizService.quizHistory().length > 0) {
            <button 
              (click)="toggleHistory.emit(); isMobileMenuOpen.set(false)"
              class="w-full flex items-center justify-center gap-1.5 px-3 py-2.5 rounded-xl text-xs font-bold text-slate-300 bg-slate-900 border border-slate-800">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4 text-indigo-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
              </svg>
              Tarix ({{ quizService.quizHistory().length }})
            </button>
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

          <!-- Mobile User profile + Sign Out -->
          @if (activeUser; as user) {
            <div class="flex items-center justify-between px-3 py-2.5 rounded-xl bg-slate-900 border border-slate-800">
              <div class="flex items-center gap-2.5">
                @if (user.pictureUrl) {
                  <img [src]="user.pictureUrl" [alt]="user.name" class="w-8 h-8 rounded-full border border-indigo-500/50 object-cover" />
                } @else {
                  <div class="w-8 h-8 rounded-full bg-indigo-600 flex items-center justify-center text-white text-xs font-extrabold shadow-sm">
                    {{ user.name.charAt(0).toUpperCase() }}
                  </div>
                }
                <div>
                  <div class="text-xs font-bold text-white max-w-[140px] truncate">{{ user.name }}</div>
                  <div class="text-[10px] text-slate-400 font-medium">{{ user.role }}</div>
                </div>
              </div>
              <button 
                (click)="signOut(); isMobileMenuOpen.set(false)"
                class="flex items-center gap-1.5 px-3 py-1.5 rounded-xl text-xs font-bold text-rose-400 bg-rose-500/10 border border-rose-500/20 hover:bg-rose-500/20 transition-all duration-200">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1" />
                </svg>
                Chiqish
              </button>
            </div>
          } @else {
            <button 
              (click)="quizService.isNameModalOpen.set(true); isMobileMenuOpen.set(false)"
              class="w-full flex items-center justify-center gap-2 px-4 py-2.5 rounded-xl text-xs font-bold text-white bg-indigo-600 hover:bg-indigo-500 shadow-md shadow-indigo-600/20 transition-all">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 16l-4-4m0 0l4-4m-4 4h14m-5 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h7a3 3 0 013 3v1" />
              </svg>
              Tizimga Kirish
            </button>
          }
        </div>
      }
    </header>
  `
})
export class NavbarComponent {
  readonly quizService = inject(QuizService);
  readonly authService = inject(AuthService);
  readonly activePortal = input<'user' | 'admin'>('user');
  readonly isMobileMenuOpen = signal<boolean>(false);

  readonly switchPortal = output<'user' | 'admin'>();
  readonly openCreator = output<void>();
  readonly toggleHistory = output<void>();

  readonly keycloakService = inject(KeycloakService);

  get activeUser(): { name: string; role: string; pictureUrl?: string } | null {
    // 1. Keycloak bilan kirgan bo'lsa — Keycloak profilini ko'rsat
    if (this.keycloakService.isAuthenticated()) {
      return {
        name: this.keycloakService.fullName() || this.keycloakService.username(),
        role: this.keycloakService.isAdmin() ? 'Admin' : 'User',
        pictureUrl: this.keycloakService.pictureUrl() || undefined
      };
    }
    // 2. Google OAuth bilan kirgan bo'lsa
    const authUser = this.authService.currentUser();
    if (authUser && authUser.name) {
      return {
        name: authUser.name,
        role: authUser.role || 'User',
        pictureUrl: authUser.pictureUrl
      };
    }
    // 3. Mehmon (lokal ism)
    const localName = this.quizService.currentUserName();
    if (localName) {
      return {
        name: localName,
        role: 'Mehmon'
      };
    }
    return null;
  }

  loginWithKeycloak(): void {
    this.keycloakService.login();
  }

  goHome(): void {
    this.quizService.resetQuiz();
    this.switchPortal.emit('user');
    this.isMobileMenuOpen.set(false);
  }

  signOut(): void {
    // Keycloak session bo'lsa — Keycloak orqali chiqamiz
    if (this.keycloakService.isAuthenticated()) {
      this.keycloakService.logout(window.location.origin);
      return;
    }
    // Google OAuth
    this.authService.logout();
    this.quizService.currentUserName.set('');
    localStorage.removeItem('quizmaster_user_name');
    this.quizService.resetQuiz();
    this.isMobileMenuOpen.set(false);
  }
}
