import { Injectable, signal, computed, inject } from '@angular/core';
import { Quiz, QuizCategory, QuizResult, UserAnswer, QuizAttempt } from '../models/quiz.model';
import { SAMPLE_QUIZZES } from '../data/sample-quizzes';
import { QuizApiService } from './quiz-api.service';
import { TelegramWebAppService } from './telegram-webapp.service';
import confetti from 'canvas-confetti';

const LOCAL_STORAGE_CUSTOM_QUIZZES_KEY = 'quizmaster_custom_quizzes';
const LOCAL_STORAGE_HISTORY_KEY = 'quizmaster_quiz_history';
const LOCAL_STORAGE_USER_NAME_KEY = 'quizmaster_user_name';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  private readonly apiService = inject(QuizApiService);
  private readonly tgWebAppService = inject(TelegramWebAppService);

  // User Name state
  readonly currentUserName = signal<string>('');
  readonly isNameModalOpen = signal<boolean>(false);
  private pendingQuizIdToStart: string | null = null;

  // Primary Signals
  readonly quizzes = signal<Quiz[]>([]);
  readonly activeCategory = signal<QuizCategory | 'all'>('all');
  readonly quizHistory = signal<QuizResult[]>([]);

  // Active Quiz Playing State Signals
  readonly currentQuiz = signal<Quiz | null>(null);
  readonly currentQuestionIndex = signal<number>(0);
  readonly userAnswers = signal<Map<string, UserAnswer>>(new Map());
  readonly timerSecondsLeft = signal<number>(0);
  readonly isQuizActive = signal<boolean>(false);
  readonly isQuizCompleted = signal<boolean>(false);
  readonly latestResult = signal<QuizResult | null>(null);

  private timerInterval: any = null;

  // Computed signals
  readonly filteredQuizzes = computed(() => {
    const category = this.activeCategory();
    const list = this.quizzes();
    if (category === 'all') return list;
    return list.filter(q => q.category === category);
  });

  readonly currentQuestion = computed(() => {
    const quiz = this.currentQuiz();
    const index = this.currentQuestionIndex();
    if (!quiz || !quiz.questions || index < 0 || index >= quiz.questions.length) {
      return null;
    }
    return quiz.questions[index];
  });

  readonly totalQuestionsCount = computed(() => {
    return this.currentQuiz()?.questions.length || 0;
  });

  readonly progressPercentage = computed(() => {
    const total = this.totalQuestionsCount();
    if (total === 0) return 0;
    return Math.round(((this.currentQuestionIndex() + 1) / total) * 100);
  });

  readonly currentSelectedOptionId = computed(() => {
    const question = this.currentQuestion();
    if (!question) return null;
    const answer = this.userAnswers().get(question.id);
    return answer ? answer.selectedOptionId : null;
  });

  readonly formattedTimer = computed(() => {
    const totalSecs = this.timerSecondsLeft();
    const minutes = Math.floor(totalSecs / 60);
    const seconds = totalSecs % 60;
    return `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
  });

  constructor() {
    this.loadInitialData();
  }

  private loadInitialData(): void {
    // Automatic Telegram User Authentication
    const tgName = this.tgWebAppService.getFormattedUserName();
    if (tgName) {
      this.currentUserName.set(tgName);
      localStorage.setItem(LOCAL_STORAGE_USER_NAME_KEY, tgName);
      this.isNameModalOpen.set(false);
    } else {
      const savedName = localStorage.getItem(LOCAL_STORAGE_USER_NAME_KEY);
      if (savedName) {
        this.currentUserName.set(savedName);
      }
    }

    this.apiService.getQuizzes().subscribe(apiQuizzes => {
      if (apiQuizzes && apiQuizzes.length > 0) {
        this.quizzes.set(apiQuizzes);
      } else {
        let customQuizzes: Quiz[] = [];
        try {
          const savedCustom = localStorage.getItem(LOCAL_STORAGE_CUSTOM_QUIZZES_KEY);
          if (savedCustom) {
            customQuizzes = JSON.parse(savedCustom);
          }
        } catch (e) {
          console.error(e);
        }
        this.quizzes.set([...SAMPLE_QUIZZES, ...customQuizzes]);
      }
    });

    try {
      const savedHistory = localStorage.getItem(LOCAL_STORAGE_HISTORY_KEY);
      if (savedHistory) {
        this.quizHistory.set(JSON.parse(savedHistory));
      }
    } catch (e) {
      console.error('Failed to parse quiz history', e);
    }
  }

  setUserName(name: string): void {
    this.currentUserName.set(name);
    localStorage.setItem(LOCAL_STORAGE_USER_NAME_KEY, name);
    this.isNameModalOpen.set(false);

    if (this.pendingQuizIdToStart) {
      const qId = this.pendingQuizIdToStart;
      this.pendingQuizIdToStart = null;
      this.startQuiz(qId);
    }
  }

  selectCategory(category: QuizCategory | 'all'): void {
    this.activeCategory.set(category);
  }

  startQuiz(quizId: string): void {
    if (!this.currentUserName()) {
      const tgName = this.tgWebAppService.getFormattedUserName();
      if (tgName) {
        this.setUserName(tgName);
      } else {
        this.pendingQuizIdToStart = quizId;
        this.isNameModalOpen.set(true);
        return;
      }
    }

    const found = this.quizzes().find(q => q.id === quizId);
    if (!found) return;

    this.currentQuiz.set(found);
    this.currentQuestionIndex.set(0);
    this.userAnswers.set(new Map());
    this.timerSecondsLeft.set(found.timeLimitSeconds);
    this.isQuizActive.set(true);
    this.isQuizCompleted.set(false);
    this.latestResult.set(null);

    this.startTimer();
  }

  private startTimer(): void {
    this.stopTimer();
    this.timerInterval = setInterval(() => {
      const currentLeft = this.timerSecondsLeft();
      if (currentLeft <= 1) {
        this.timerSecondsLeft.set(0);
        this.stopTimer();
        this.finishQuiz();
      } else {
        this.timerSecondsLeft.set(currentLeft - 1);
      }
    }, 1000);
  }

  private stopTimer(): void {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
      this.timerInterval = null;
    }
  }

  selectOption(optionId: string): void {
    const question = this.currentQuestion();
    if (!question || !this.isQuizActive()) return;

    const isCorrect = question.correctOptionId === optionId;
    const currentAnswers = new Map(this.userAnswers());

    currentAnswers.set(question.id, {
      questionId: question.id,
      selectedOptionId: optionId,
      isCorrect,
      timeSpentSeconds: 0
    });

    this.userAnswers.set(currentAnswers);
  }

  nextQuestion(): void {
    const currentIndex = this.currentQuestionIndex();
    const total = this.totalQuestionsCount();
    if (currentIndex < total - 1) {
      this.currentQuestionIndex.set(currentIndex + 1);
    } else {
      this.finishQuiz();
    }
  }

  previousQuestion(): void {
    const currentIndex = this.currentQuestionIndex();
    if (currentIndex > 0) {
      this.currentQuestionIndex.set(currentIndex - 1);
    }
  }

  finishQuiz(): void {
    this.stopTimer();
    const quiz = this.currentQuiz();
    if (!quiz) return;

    const answers = Array.from(this.userAnswers().values());
    let correctCount = 0;

    quiz.questions.forEach(q => {
      const ans = this.userAnswers().get(q.id);
      if (ans && ans.isCorrect) {
        correctCount++;
      }
    });

    const scorePct = Math.round((correctCount / quiz.questions.length) * 100);
    const timeSpent = quiz.timeLimitSeconds - this.timerSecondsLeft();

    const result: QuizResult = {
      id: 'res-' + Date.now(),
      quizId: quiz.id,
      quizTitle: quiz.title,
      categoryName: quiz.categoryName,
      totalQuestions: quiz.questions.length,
      correctAnswersCount: correctCount,
      scorePercentage: scorePct,
      totalTimeSpentSeconds: timeSpent,
      completedAt: new Date().toLocaleDateString('uz-UZ', {
        year: 'numeric',
        month: 'short',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
      }),
      userAnswers: answers
    };

    this.latestResult.set(result);
    this.isQuizActive.set(false);
    this.isQuizCompleted.set(true);

    // Post attempt to Backend REST API
    this.apiService.submitAttempt({
      quizId: quiz.id,
      quizTitle: quiz.title,
      categoryName: quiz.categoryName,
      userName: this.currentUserName() || 'Foydalanuvchi',
      totalQuestions: quiz.questions.length,
      correctAnswersCount: correctCount,
      scorePercentage: scorePct,
      totalTimeSpentSeconds: timeSpent,
      userAnswers: answers as any
    }).subscribe({
      next: (serverAttempt) => {
        if (serverAttempt && serverAttempt.id) {
          result.id = serverAttempt.id;
          this.latestResult.set({ ...result });
        }
      },
      error: (e) => console.warn('Could not post attempt to backend server', e)
    });

    // Save history locally
    const updatedHistory = [result, ...this.quizHistory().slice(0, 19)];
    this.quizHistory.set(updatedHistory);
    try {
      localStorage.setItem(LOCAL_STORAGE_HISTORY_KEY, JSON.stringify(updatedHistory));
    } catch (e) {
      console.error(e);
    }

    if (scorePct >= 70) {
      confetti({
        particleCount: 100,
        spread: 70,
        origin: { y: 0.6 }
      });
    }
  }

  resetQuiz(): void {
    this.stopTimer();
    this.currentQuiz.set(null);
    this.currentQuestionIndex.set(0);
    this.userAnswers.set(new Map());
    this.isQuizActive.set(false);
    this.isQuizCompleted.set(false);
    this.latestResult.set(null);
  }

  addCustomQuiz(newQuiz: Quiz): void {
    this.apiService.saveQuiz(newQuiz).subscribe({
      next: (saved) => {
        this.quizzes.set([saved, ...this.quizzes()]);
      },
      error: () => {
        this.quizzes.set([newQuiz, ...this.quizzes()]);
      }
    });
  }

  deleteQuiz(quizId: string): void {
    this.apiService.deleteQuiz(quizId).subscribe({
      next: () => {
        this.quizzes.set(this.quizzes().filter(q => q.id !== quizId));
      },
      error: () => {
        this.quizzes.set(this.quizzes().filter(q => q.id !== quizId));
      }
    });
  }
}
