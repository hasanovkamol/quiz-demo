import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuizService } from './services/quiz.service';
import { NavbarComponent } from './components/navbar/navbar.component';
import { QuizListComponent } from './components/quiz-list/quiz-list.component';
import { QuizPlayComponent } from './components/quiz-play/quiz-play.component';
import { QuizResultComponent } from './components/quiz-result/quiz-result.component';
import { QuizCreatorComponent } from './components/quiz-creator/quiz-creator.component';
import { HistoryModalComponent } from './components/history-modal/history-modal.component';
import { UserModalComponent } from './components/user-modal/user-modal.component';
import { AdminDashboardComponent } from './components/admin/admin-dashboard.component';

import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    NavbarComponent,
    QuizListComponent,
    QuizPlayComponent,
    QuizResultComponent,
    QuizCreatorComponent,
    HistoryModalComponent,
    UserModalComponent,
    AdminDashboardComponent
  ],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  readonly quizService = inject(QuizService);
  readonly authService = inject(AuthService);

  readonly activePortal = signal<'user' | 'admin'>('user');
  readonly isCreatorOpen = signal<boolean>(false);
  readonly isHistoryOpen = signal<boolean>(false);

  setPortal(mode: 'user' | 'admin'): void {
    if (mode === 'admin' && !this.authService.isAdmin()) {
      this.quizService.isNameModalOpen.set(true);
      return;
    }
    this.activePortal.set(mode);
  }
}
