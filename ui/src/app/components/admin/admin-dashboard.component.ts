import { Component, inject, signal, OnInit, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuizApiService } from '../../services/quiz-api.service';
import { QuizService } from '../../services/quiz.service';
import { AuthService } from '../../services/auth.service';
import { QuizAttempt, Question, Difficulty, CategoryItem } from '../../models/quiz.model';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
      
      @if (!authService.isAdmin()) {
        <div class="max-w-xl mx-auto my-16 p-8 glass-card rounded-3xl border border-rose-500/30 text-center relative overflow-hidden shadow-2xl">
          <div class="w-16 h-16 rounded-2xl bg-rose-500/10 border border-rose-500/20 text-rose-400 flex items-center justify-center mx-auto mb-5 shadow-lg shadow-rose-500/10">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z" />
            </svg>
          </div>
          <h2 class="text-2xl font-black text-white mb-2">Ruxsat Cheklangan (403 Forbidden)</h2>
          <p class="text-xs text-slate-400 mb-6 leading-relaxed">
            Admin Console faqat Keycloak yoki Tizimda <b>Admin</b> roliga ega bo'lgan foydalanuvchilar uchun ochiq. Iltimos Admin hisobi bilan tizimga kiring.
          </p>
          <button 
            (click)="quizService.isNameModalOpen.set(true)"
            class="px-6 py-3 rounded-2xl text-xs font-bold text-white bg-gradient-to-r from-indigo-600 to-purple-600 hover:from-indigo-500 hover:to-purple-500 shadow-lg shadow-indigo-600/30 transition">
            🔑 Tizimga Kirish / Autentifikatsiya
          </button>
        </div>
      } @else {

      <!-- Admin Header -->
      <div class="flex items-center justify-between mb-8 pb-4 border-b border-slate-800 flex-wrap gap-4">
        <div>
          <div class="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-purple-500/10 border border-purple-500/20 text-purple-400 text-xs font-bold mb-2">
            <span class="w-2 h-2 rounded-full bg-purple-400 animate-pulse"></span>
            Tizim Boshqaruv Portali
          </div>
          <h1 class="text-2xl sm:text-3xl font-extrabold text-white">
            Admin <span class="gradient-text">Management Console</span>
          </h1>
        </div>

        <div class="flex items-center gap-3 flex-wrap">
          <!-- Create Test Button -->
          <button 
            (click)="openCreator.emit()"
            class="flex items-center gap-2 px-4 py-2.5 rounded-xl text-xs font-bold text-white bg-gradient-to-r from-purple-600 to-indigo-600 hover:from-purple-500 hover:to-indigo-500 shadow-lg shadow-purple-600/30 transition">
            <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2.5" d="M12 4v16m8-8H4" />
            </svg>
            Yangi Test Yaratish
          </button>

          <!-- Navigation Tabs -->
          <div class="flex items-center gap-1.5 bg-slate-900/90 p-1.5 rounded-2xl border border-slate-800 flex-wrap">
            <button 
              (click)="activeTab.set('attempts')"
              [class]="activeTab() === 'attempts' ? 
                'px-3.5 py-2 rounded-xl text-xs font-bold text-white bg-indigo-600 shadow-md shadow-indigo-600/30 transition' : 
                'px-3.5 py-2 rounded-xl text-xs font-medium text-slate-400 hover:text-white transition'">
              Test Tarixi
            </button>

            <button 
              (click)="activeTab.set('ai-single')"
              [class]="activeTab() === 'ai-single' ? 
                'px-3.5 py-2 rounded-xl text-xs font-bold text-white bg-amber-600 shadow-md shadow-amber-600/30 transition flex items-center gap-1.5' : 
                'px-3.5 py-2 rounded-xl text-xs font-medium text-slate-400 hover:text-white transition flex items-center gap-1.5'">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5 text-amber-300" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 10V3L4 14h7v7l9-11h-7z" />
              </svg>
              AI Bitta Savol & Insert
            </button>

            <button 
              (click)="activeTab.set('categories')"
              [class]="activeTab() === 'categories' ? 
                'px-3.5 py-2 rounded-xl text-xs font-bold text-white bg-emerald-600 shadow-md shadow-emerald-600/30 transition flex items-center gap-1.5' : 
                'px-3.5 py-2 rounded-xl text-xs font-medium text-slate-400 hover:text-white transition flex items-center gap-1.5'">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5 text-emerald-300" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10" />
              </svg>
              Kategoriya Yaratish
            </button>

            <button 
              (click)="activeTab.set('ai-generator')"
              [class]="activeTab() === 'ai-generator' ? 
                'px-3.5 py-2 rounded-xl text-xs font-bold text-white bg-purple-600 shadow-md shadow-purple-600/30 transition flex items-center gap-1.5' : 
                'px-3.5 py-2 rounded-xl text-xs font-medium text-slate-400 hover:text-white transition flex items-center gap-1.5'">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-3.5 h-3.5 text-purple-300" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19.428 15.428a2 2 0 00-1.022-.547l-2.387-.477a6 6 0 00-3.86.517l-.318.158a6 6 0 01-3.86.517L6.05 15.21a2 2 0 00-1.806.547M8 4h8l-1 1v5.172a2 2 0 00.586 1.414l5 5c1.26 1.26.367 3.414-1.415 3.414H4.828c-1.782 0-2.674-2.154-1.414-3.414l5-5A2 2 0 009 10.172V5L8 4z" />
              </svg>
              To'liq AI Test
            </button>
          </div>
        </div>
      </div>

      <!-- Metrics Cards -->
      <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4 mb-8">
        <div class="glass-card rounded-2xl p-5 border border-slate-800">
          <div class="text-xs text-slate-400 font-semibold mb-1">Jami Test Topshirishlar</div>
          <div class="text-2xl font-extrabold text-white">{{ stats().totalAttempts }}</div>
        </div>
        <div class="glass-card rounded-2xl p-5 border border-slate-800">
          <div class="text-xs text-indigo-400 font-semibold mb-1">Foydalanuvchilar Soni</div>
          <div class="text-2xl font-extrabold text-indigo-300">{{ stats().uniqueUsersCount }}</div>
        </div>
        <div class="glass-card rounded-2xl p-5 border border-slate-800">
          <div class="text-xs text-emerald-400 font-semibold mb-1">O'rtacha Ball Natijasi</div>
          <div class="text-2xl font-extrabold text-emerald-400">{{ stats().avgScore }}%</div>
        </div>
        <div class="glass-card rounded-2xl p-5 border border-slate-800">
          <div class="text-xs text-purple-400 font-semibold mb-1">Mavjud Testlar & Kategoriyalar</div>
          <div class="text-2xl font-extrabold text-purple-300">{{ quizService.quizzes().length }} test / {{ quizService.categories().length }} cat</div>
        </div>
      </div>

      <!-- Tab 1: User Attempts Table -->
      @if (activeTab() === 'attempts') {
        <div class="glass-card rounded-2xl p-6 border border-slate-800">
          <h3 class="text-lg font-bold text-white mb-4">
            Foydalanuvchilarning Test Natijalari Tarixi
          </h3>

          <div class="overflow-x-auto">
            <table class="w-full text-left text-xs text-slate-300">
              <thead class="bg-slate-900/90 text-slate-400 uppercase font-bold tracking-wider border-b border-slate-800">
                <tr>
                  <th class="p-3">Foydalanuvchi (UserName)</th>
                  <th class="p-3">Test Nomi</th>
                  <th class="p-3">Kategoriya</th>
                  <th class="p-3">To'g'ri Javoblar</th>
                  <th class="p-3">Ball (%)</th>
                  <th class="p-3">Vaqt</th>
                  <th class="p-3">Sana</th>
                </tr>
              </thead>
              <tbody class="divide-y divide-slate-800/60">
                @for (attempt of userAttempts(); track attempt.id) {
                  <tr class="hover:bg-slate-900/50 transition">
                    <td class="p-3 font-bold text-white flex items-center gap-2">
                      <div class="w-7 h-7 rounded-full bg-indigo-500/20 text-indigo-300 font-bold text-xs flex items-center justify-center border border-indigo-500/30">
                        {{ attempt.userName.charAt(0).toUpperCase() }}
                      </div>
                      {{ attempt.userName }}
                    </td>
                    <td class="p-3 font-medium text-slate-200">{{ attempt.quizTitle }}</td>
                    <td class="p-3 text-slate-400">{{ attempt.categoryName }}</td>
                    <td class="p-3 text-slate-300 font-semibold">{{ attempt.correctAnswersCount }} / {{ attempt.totalQuestions }}</td>
                    <td class="p-3 font-extrabold" [class.text-emerald-400]="attempt.scorePercentage >= 70" [class.text-rose-400]="attempt.scorePercentage < 70">
                      {{ attempt.scorePercentage }}%
                    </td>
                    <td class="p-3 text-slate-400 font-mono">{{ formatTime(attempt.totalTimeSpentSeconds) }}</td>
                    <td class="p-3 text-slate-500 text-[11px]">{{ attempt.completedAt }}</td>
                  </tr>
                } @empty {
                  <tr>
                    <td colspan="7" class="p-8 text-center text-slate-500 text-xs">
                      Hali hech bir foydalanuvchi test topshirmagan.
                    </td>
                  </tr>
                }
              </tbody>
            </table>
          </div>
        </div>
      }

      <!-- Tab 2: AI Single Question & 1-Click Insert -->
      @if (activeTab() === 'ai-single') {
        <div class="glass-card rounded-3xl p-8 border border-amber-500/30 bg-gradient-to-br from-slate-900 via-slate-900/90 to-amber-950/20 shadow-2xl max-w-4xl mx-auto">
          
          <div class="flex items-center gap-3 mb-6 border-b border-slate-800 pb-4">
            <div class="w-12 h-12 rounded-2xl bg-amber-500/15 border border-amber-500/30 text-amber-300 flex items-center justify-center font-bold shadow-lg shadow-amber-500/20">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 10V3L4 14h7v7l9-11h-7z" />
              </svg>
            </div>
            <div>
              <h2 class="text-xl font-extrabold text-white">AI Bitta-bitta Savol Yaratish va 1-Click Insert</h2>
              <p class="text-xs text-slate-300">AI savolni va javob variantlarini taklif etadi. Siz 1-click tugmasi orqali uni tanlangan testga kiritishingiz mumkin.</p>
            </div>
          </div>

          <div class="grid grid-cols-1 md:grid-cols-2 gap-6 mb-6">
            <!-- Left Side: Inputs -->
            <div class="space-y-4">
              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Maqsadli Test (Qaysi testga insert qilinadi?) *</label>
                <select 
                  [(ngModel)]="selectedTargetQuizId" 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-amber-500">
                  <option value="" disabled>-- Testni Tanlang --</option>
                  @for (q of quizService.quizzes(); track q.id) {
                    <option [value]="q.id">{{ q.title }} ({{ q.categoryName }})</option>
                  }
                </select>
              </div>

              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Savol Mavzusi (Specific Topic) *</label>
                <input 
                  type="text" 
                  [(ngModel)]="singleTopic" 
                  placeholder="masalan: C# Task.WhenAll vs Task.WaitAll, Angular Signals effect cleanup..." 
                  class="w-full px-4 py-3 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-amber-500">
              </div>

              <div class="grid grid-cols-2 gap-4">
                <div>
                  <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Qiyinchilik</label>
                  <select 
                    [(ngModel)]="singleDifficulty" 
                    class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-amber-500">
                    <option value="Oson">Oson</option>
                    <option value="O'rta">O'rta</option>
                    <option value="Qiyin">Qiyin</option>
                  </select>
                </div>

                <div>
                  <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">API Key (Ixtiyoriy)</label>
                  <input 
                    type="password" 
                    [(ngModel)]="singleApiKey" 
                    placeholder="Gemini Key..." 
                    class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-amber-500">
                </div>
              </div>

              <button 
                (click)="generateSingleQuestion()"
                [disabled]="isSingleGenerating()"
                class="w-full py-3 rounded-xl text-xs font-bold text-white bg-gradient-to-r from-amber-600 to-orange-600 hover:from-amber-500 hover:to-orange-500 shadow-lg shadow-amber-600/30 transition disabled:opacity-50 flex items-center justify-center gap-2">
                @if (isSingleGenerating()) {
                  <svg class="animate-spin h-4 w-4 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                    <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                    <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                  </svg>
                  AI Savol va Javoblarni Yaratmoqda...
                } @else {
                  <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 10V3L4 14h7v7l9-11h-7z" />
                  </svg>
                  ⚡ AI Bilan Savol va Javoblarni Taklif Etish
                }
              </button>
            </div>

            <!-- Right Side: AI Generated Question Preview -->
            <div>
              <label class="block text-xs font-bold text-amber-300 uppercase tracking-wider mb-2">AI Taklif Etgan Savol & Javoblar (Preview)</label>
              
              @if (generatedQuestion()) {
                <div class="p-5 rounded-2xl bg-slate-950 border border-amber-500/30 space-y-4 text-xs">
                  <div>
                    <span class="text-[10px] uppercase font-bold text-amber-400 tracking-wider">Savol Matni:</span>
                    <textarea 
                      [(ngModel)]="generatedQuestion()!.text" 
                      rows="2" 
                      class="w-full mt-1 p-2 bg-slate-900 border border-slate-800 rounded-lg text-white font-medium focus:outline-none focus:border-amber-500"></textarea>
                  </div>

                  @if (generatedQuestion()!.codeSnippet) {
                    <div>
                      <span class="text-[10px] uppercase font-bold text-indigo-400 tracking-wider">Kod Parchasi:</span>
                      <textarea 
                        [(ngModel)]="generatedQuestion()!.codeSnippet" 
                        rows="3" 
                        class="w-full mt-1 p-2 bg-slate-900 border border-slate-800 rounded-lg text-indigo-300 font-mono text-[11px] focus:outline-none focus:border-amber-500"></textarea>
                    </div>
                  }

                  <div>
                    <span class="text-[10px] uppercase font-bold text-slate-400 tracking-wider">Javob Variantlari (To'g'risi ajratilgan):</span>
                    <div class="space-y-1.5 mt-1">
                      @for (opt of generatedQuestion()!.options; track opt.id; let idx = $index) {
                        <div class="flex items-center gap-2 p-2 rounded-lg border" [class.border-emerald-500]="opt.id === generatedQuestion()!.correctOptionId" [class.bg-emerald-500\/10]="opt.id === generatedQuestion()!.correctOptionId" [class.border-slate-800]="opt.id !== generatedQuestion()!.correctOptionId">
                          <input 
                            type="radio" 
                            name="preview-correct" 
                            [checked]="opt.id === generatedQuestion()!.correctOptionId" 
                            (change)="generatedQuestion()!.correctOptionId = opt.id">
                          <input 
                            type="text" 
                            [(ngModel)]="opt.text" 
                            class="w-full bg-transparent text-slate-200 focus:outline-none">
                        </div>
                      }
                    </div>
                  </div>

                  <div>
                    <span class="text-[10px] uppercase font-bold text-slate-400 tracking-wider">Izoh (Explanation):</span>
                    <input 
                      type="text" 
                      [(ngModel)]="generatedQuestion()!.explanation" 
                      class="w-full mt-1 p-2 bg-slate-900 border border-slate-800 rounded-lg text-slate-300">
                  </div>

                  <div class="pt-3 border-t border-slate-800">
                    <button 
                      (click)="insertGeneratedQuestion()"
                      class="w-full py-2.5 rounded-xl text-xs font-extrabold text-white bg-emerald-600 hover:bg-emerald-500 shadow-lg shadow-emerald-600/30 transition flex items-center justify-center gap-2">
                      <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2.5" d="M5 13l4 4L19 7" />
                      </svg>
                      ⚡ 1-CLICK INSERT: Testga Qo'shish va Saqlash
                    </button>
                  </div>
                </div>
              } @else {
                <div class="p-8 rounded-2xl bg-slate-950/50 border border-dashed border-slate-800 text-center text-slate-500 text-xs my-auto">
                  <svg xmlns="http://www.w3.org/2000/svg" class="w-10 h-10 mx-auto mb-2 text-slate-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="1.5" d="M19.428 15.428a2 2 0 00-1.022-.547l-2.387-.477a6 6 0 00-3.86.517l-.318.158a6 6 0 01-3.86.517L6.05 15.21a2 2 0 00-1.806.547M8 4h8l-1 1v5.172a2 2 0 00.586 1.414l5 5c1.26 1.26.367 3.414-1.415 3.414H4.828c-1.782 0-2.674-2.154-1.414-3.414l5-5A2 2 0 009 10.172V5L8 4z" />
                  </svg>
                  Chap tomonda maqsadli test va mavzuni kiritib "AI Bilan Savol Taklif Etish" tugmasini bosing.
                </div>
              }
            </div>
          </div>

        </div>
      }

      <!-- Tab 3: Categories Creation & Management -->
      @if (activeTab() === 'categories') {
        <div class="glass-card rounded-3xl p-8 border border-emerald-500/30 bg-gradient-to-br from-slate-900 via-slate-900/90 to-emerald-950/20 shadow-2xl max-w-4xl mx-auto">
          
          <div class="flex items-center gap-3 mb-6 border-b border-slate-800 pb-4">
            <div class="w-12 h-12 rounded-2xl bg-emerald-500/15 border border-emerald-500/30 text-emerald-300 flex items-center justify-center font-bold shadow-lg shadow-emerald-500/20">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 11H5m14 0a2 2 0 012 2v6a2 2 0 01-2 2H5a2 2 0 01-2-2v-6a2 2 0 012-2m14 0V9a2 2 0 00-2-2M5 11V9a2 2 0 012-2m0 0V5a2 2 0 012-2h6a2 2 0 012 2v2M7 7h10" />
              </svg>
            </div>
            <div>
              <h2 class="text-xl font-extrabold text-white">Yangi Kategoriya Yaratish va Boshqaruv</h2>
              <p class="text-xs text-slate-300">Platformaga yangi texnologiya kategoriyasini qo'shing. U avtomatik ravishda barcha filter va generatorlarda paydo bo'ladi.</p>
            </div>
          </div>

          <div class="grid grid-cols-1 md:grid-cols-2 gap-8">
            <!-- Create Category Form -->
            <div class="space-y-4">
              <h3 class="text-sm font-bold text-white uppercase tracking-wider">📁 Yangi Kategoriya Qo'shish</h3>
              
              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Kategoriya ID (Slug) *</label>
                <input 
                  type="text" 
                  [(ngModel)]="newCatId" 
                  placeholder="masalan: python, devops, java, database..." 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-emerald-500">
              </div>

              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Ko'rinish Nomi (Display Name) *</label>
                <input 
                  type="text" 
                  [(ngModel)]="newCatName" 
                  placeholder="masalan: Python Programming, DevOps & Cloud..." 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-emerald-500">
              </div>

              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Ikonka Nomi (Icon Name)</label>
                <input 
                  type="text" 
                  [(ngModel)]="newCatIcon" 
                  placeholder="masalan: code-2, terminal, globe, sparkles..." 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-emerald-500">
              </div>

              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Tavsif (Description)</label>
                <textarea 
                  [(ngModel)]="newCatDesc" 
                  rows="2" 
                  placeholder="Kategoriya haqida qisqacha ma'lumot..." 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-emerald-500"></textarea>
              </div>

              <button 
                (click)="createCategory()"
                class="w-full py-3 rounded-xl text-xs font-bold text-white bg-emerald-600 hover:bg-emerald-500 shadow-lg shadow-emerald-600/30 transition flex items-center justify-center gap-2">
                <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4v16m8-8H4" />
                </svg>
                Yangi Kategoriyani Saqlash
              </button>
            </div>

            <!-- Existing Categories List -->
            <div>
              <h3 class="text-sm font-bold text-white uppercase tracking-wider mb-4"> Mavlud Kategoriyalar ({{ quizService.categories().length }})</h3>
              
              <div class="space-y-3 max-h-[380px] overflow-y-auto pr-2 scrollbar-thin">
                @for (c of quizService.categories(); track c.id) {
                  <div class="p-4 rounded-2xl bg-slate-950 border border-slate-800 flex items-center justify-between">
                    <div>
                      <div class="flex items-center gap-2">
                        <span class="px-2 py-0.5 rounded text-[10px] font-mono font-bold bg-indigo-500/20 text-indigo-300 border border-indigo-500/30">
                          {{ c.id }}
                        </span>
                        <h4 class="text-xs font-bold text-white">{{ c.name }}</h4>
                      </div>
                      @if (c.description) {
                        <p class="text-[11px] text-slate-400 mt-1">{{ c.description }}</p>
                      }
                    </div>
                  </div>
                }
              </div>
            </div>
          </div>

        </div>
      }

      <!-- Tab 4: AI Full Quiz Generator -->
      @if (activeTab() === 'ai-generator') {
        <div class="glass-card rounded-3xl p-8 border border-purple-500/30 bg-gradient-to-br from-slate-900 via-slate-900/90 to-purple-950/30 shadow-2xl max-w-3xl mx-auto">
          
          <div class="flex items-center gap-3 mb-6">
            <div class="w-12 h-12 rounded-2xl bg-purple-500/15 border border-purple-500/30 text-purple-300 flex items-center justify-center font-bold shadow-lg shadow-purple-500/20">
              <svg xmlns="http://www.w3.org/2000/svg" class="w-6 h-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 10V3L4 14h7v7l9-11h-7z" />
              </svg>
            </div>
            <div>
              <h2 class="text-xl font-extrabold text-white">Semantic Kernel AI To'liq Test Generatori</h2>
              <p class="text-xs text-slate-300">Sun'iy intellekt yordamida har qanday texnologiya bo'yicha to'liq test to'plamini avtomatik yaratish</p>
            </div>
          </div>

          <div class="space-y-5">
            <div>
              <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Mavzu yoki Texnologiya (Topic) *</label>
              <input 
                type="text" 
                [(ngModel)]="aiTopic" 
                placeholder="masalan: Angular Signals Performance, C# Entity Framework Core 9 Indexes, PostgreSQL Locks..." 
                class="w-full px-4 py-3 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-purple-500">
            </div>

            <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Kategoriya</label>
                <select 
                  [(ngModel)]="aiCategory" 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-purple-500">
                  @for (cat of quizService.categories(); track cat.id) {
                    @if (cat.id !== 'all') {
                      <option [value]="cat.id">{{ cat.name }}</option>
                    }
                  }
                </select>
              </div>

              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Qiyinchilik Darajasi</label>
                <select 
                  [(ngModel)]="aiDifficulty" 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-purple-500">
                  <option value="Oson">Oson</option>
                  <option value="O'rta">O'rta</option>
                  <option value="Qiyin">Qiyin</option>
                </select>
              </div>
            </div>

            <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Savollar Soni (1-10)</label>
                <input 
                  type="number" 
                  [(ngModel)]="aiQuestionCount" 
                  min="1" max="10" 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-purple-500">
              </div>

              <div>
                <label class="block text-xs font-bold text-slate-300 uppercase tracking-wider mb-2">Gemini / AI API Key *</label>
                <input 
                  type="password" 
                  [(ngModel)]="aiApiKey" 
                  placeholder="AI API Key kiriting..." 
                  class="w-full px-4 py-2.5 bg-slate-950 border border-slate-800 rounded-xl text-sm text-white focus:outline-none focus:border-purple-500">
              </div>
            </div>

            <!-- Submit Button -->
            <div class="pt-4 border-t border-slate-800 text-right">
              <button 
                (click)="generateAiQuiz()"
                [disabled]="isGenerating()"
                class="w-full sm:w-auto px-8 py-3 rounded-xl text-sm font-bold text-white bg-gradient-to-r from-purple-600 to-indigo-600 hover:from-purple-500 hover:to-indigo-500 shadow-lg shadow-purple-600/30 transition disabled:opacity-50 flex items-center justify-center gap-2 ml-auto">
                @if (isGenerating()) {
                  <svg class="animate-spin -ml-1 mr-2 h-4 w-4 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                    <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                    <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                  </svg>
                  Semantic Kernel AI Savollar Generatsiyasi Ketmoqda...
                } @else {
                  <svg xmlns="http://www.w3.org/2000/svg" class="w-4 h-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 10V3L4 14h7v7l9-11h-7z" />
                  </svg>
                  AI Bilan To'liq Test Yaratish
                }
              </button>
            </div>

          </div>

        </div>
      }
      }

    </div>
  `
})
export class AdminDashboardComponent implements OnInit {
  private readonly apiService = inject(QuizApiService);
  readonly quizService = inject(QuizService);
  readonly authService = inject(AuthService);

  readonly openCreator = output<void>();

  readonly activeTab = signal<'attempts' | 'ai-single' | 'categories' | 'ai-generator'>('attempts');
  readonly userAttempts = signal<QuizAttempt[]>([]);
  readonly stats = signal<any>({ totalQuizzes: 0, totalAttempts: 0, avgScore: 0, uniqueUsersCount: 0 });

  readonly isGenerating = signal<boolean>(false);
  readonly isSingleGenerating = signal<boolean>(false);
  readonly generatedQuestion = signal<Question | null>(null);

  // Full AI Quiz Generator fields
  aiTopic = '';
  aiCategory = 'angular';
  aiDifficulty = 'O\'rta';
  aiQuestionCount = 3;
  aiApiKey = '';

  // Single AI Question fields
  selectedTargetQuizId = '';
  singleTopic = '';
  singleDifficulty: Difficulty = 'O\'rta';
  singleApiKey = '';

  // New Category fields
  newCatId = '';
  newCatName = '';
  newCatIcon = 'code-2';
  newCatDesc = '';

  ngOnInit(): void {
    this.loadAdminData();
  }

  loadAdminData(): void {
    this.apiService.getAttempts().subscribe(attempts => {
      if (attempts && attempts.length > 0) {
        this.userAttempts.set(attempts);
      } else {
        this.userAttempts.set(this.quizService.quizHistory().map(h => ({
          ...h,
          userName: 'Noma\'lum Foydalanuvchi'
        } as QuizAttempt)));
      }
    });

    this.apiService.getAdminStats().subscribe(s => this.stats.set(s));
  }

  generateSingleQuestion(): void {
    if (!this.singleTopic.trim()) {
      alert("Iltimos, savol mavzusini kiriting!");
      return;
    }

    this.isSingleGenerating.set(true);
    this.generatedQuestion.set(null);

    this.apiService.generateAiQuestion({
      topic: this.singleTopic,
      difficulty: this.singleDifficulty,
      apiKey: this.singleApiKey
    }).subscribe({
      next: (question) => {
        this.isSingleGenerating.set(false);
        this.generatedQuestion.set(question);
      },
      error: (err) => {
        this.isSingleGenerating.set(false);
        console.error(err);
        alert("AI Savol yaratishda xatolik yuz berdi.");
      }
    });
  }

  insertGeneratedQuestion(): void {
    const question = this.generatedQuestion();
    if (!question) return;

    if (!this.selectedTargetQuizId) {
      alert("Iltimos, savol kiritilishi kerak bo'lgan maqsadli testni tanlang!");
      return;
    }

    this.quizService.addQuestionToQuiz(this.selectedTargetQuizId, question);
    alert("⚡ Savol tanlangan testga muvaffaqiyatli saqlandi va insert qilindi!");
    this.generatedQuestion.set(null);
    this.singleTopic = '';
  }

  createCategory(): void {
    if (!this.newCatId.trim() || !this.newCatName.trim()) {
      alert("Iltimos, Kategoriya ID va Nomini kiriting!");
      return;
    }

    const categoryItem: CategoryItem = {
      id: this.newCatId.toLowerCase().trim(),
      name: this.newCatName.trim(),
      iconName: this.newCatIcon || 'code-2',
      description: this.newCatDesc.trim()
    };

    this.quizService.addCategory(categoryItem);
    alert(`" ${categoryItem.name} " kategoriyasi muvaffaqiyatli saqlandi va tizimga qo'shildi!`);

    this.newCatId = '';
    this.newCatName = '';
    this.newCatIcon = 'code-2';
    this.newCatDesc = '';
  }

  generateAiQuiz(): void {
    if (!this.aiTopic.trim()) {
      alert("Iltimos, test mavzusini kiriting!");
      return;
    }

    this.isGenerating.set(true);

    this.apiService.generateAiQuiz({
      topic: this.aiTopic,
      category: this.aiCategory,
      difficulty: this.aiDifficulty,
      questionCount: this.aiQuestionCount,
      timeLimitMinutes: 5,
      apiKey: this.aiApiKey
    }).subscribe({
      next: (quiz) => {
        this.isGenerating.set(false);
        this.quizService.addCustomQuiz(quiz);
        alert(`" ${quiz.title} " testi muvaffaqiyatli yaratildi va saqlandi!`);
        this.aiTopic = '';
        this.activeTab.set('attempts');
      },
      error: (err) => {
        this.isGenerating.set(false);
        console.error(err);
        alert("AI Test yaratishda xatolik yuz berdi. Iltimos API key ni tekshiring.");
      }
    });
  }

  formatTime(seconds: number): string {
    const m = Math.floor(seconds / 60);
    const s = seconds % 60;
    return `${m}m ${s}s`;
  }
}
