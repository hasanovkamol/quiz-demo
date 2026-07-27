import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { QuizApiService } from '../../services/quiz-api.service';
import { QuizAttempt } from '../../models/quiz.model';

@Component({
  selector: 'app-result-share',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="max-w-4xl mx-auto px-4 sm:px-6 py-8">
      @if (loading()) {
        <div class="glass-card rounded-2xl p-12 text-center text-slate-400">
          Natija yuklanmoqda...
        </div>
      } @else if (attempt(); as res) {
        <div class="glass-card rounded-3xl p-8 sm:p-10 border border-slate-800 text-center relative overflow-hidden bg-gradient-to-b from-slate-900 via-slate-900/90 to-slate-950">
          
          <div class="inline-flex items-center gap-2 px-3.5 py-1.5 rounded-full bg-indigo-500/10 border border-indigo-500/20 text-indigo-300 text-xs font-bold mb-6">
            <span class="w-2 h-2 rounded-full bg-emerald-400"></span>
            {{ res.userName }} ning Natijasi • {{ res.quizTitle }}
          </div>

          <div class="text-4xl font-extrabold text-white mb-2">
            {{ res.scorePercentage }}%
          </div>
          <p class="text-xs text-slate-400 mb-6">
            {{ res.correctAnswersCount }} / {{ res.totalQuestions }} to'g'ri javob topshirildi ({{ res.completedAt }})
          </p>

          <button 
            (click)="copyLink()"
            class="px-5 py-2.5 rounded-xl text-xs font-bold text-white bg-indigo-600 hover:bg-indigo-500 transition">
            {{ copied() ? 'Havola Nusxalandi! ✅' : 'Natijani Boshqalarga Ulashish (Copy Link)' }}
          </button>
        </div>
      } @else {
        <div class="glass-card rounded-2xl p-12 text-center text-slate-400">
          Natija topilmadi yoki o'chirilgan.
        </div>
      }
    </div>
  `
})
export class ResultShareComponent implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly apiService = inject(QuizApiService);

  readonly attempt = signal<QuizAttempt | null>(null);
  readonly loading = signal<boolean>(true);
  readonly copied = signal<boolean>(false);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.apiService.getAttempt(id).subscribe(res => {
        this.attempt.set(res);
        this.loading.set(false);
      });
    } else {
      this.loading.set(false);
    }
  }

  copyLink(): void {
    navigator.clipboard.writeText(window.location.href);
    this.copied.set(true);
    setTimeout(() => this.copied.set(false), 2000);
  }
}
