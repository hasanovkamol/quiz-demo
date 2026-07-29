import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { Quiz, QuizAttempt, CategoryItem, AiSingleQuestionRequest, Question, AiQuestionExplainRequest } from '../models/quiz.model';
import { AppConfigService } from './app-config.service';

@Injectable({
  providedIn: 'root'
})
export class QuizApiService {
  private readonly http = inject(HttpClient);
  private readonly appConfig = inject(AppConfigService);

  private get baseUrl(): string {
    return this.appConfig.apiUrl;
  }

  getQuizzes(): Observable<Quiz[]> {
    return this.http.get<Quiz[]>(`${this.baseUrl}/quizzes`).pipe(
      catchError(err => {
        console.warn('Backend API connection unavailable, falling back to local dataset', err);
        return of([]);
      })
    );
  }

  getQuiz(id: string): Observable<Quiz | null> {
    return this.http.get<Quiz>(`${this.baseUrl}/quizzes/${id}`).pipe(
      catchError(() => of(null))
    );
  }

  saveQuiz(quiz: Quiz): Observable<Quiz> {
    return this.http.post<Quiz>(`${this.baseUrl}/quizzes`, quiz);
  }

  deleteQuiz(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/quizzes/${id}`);
  }

  submitAttempt(attempt: Partial<QuizAttempt>): Observable<QuizAttempt> {
    return this.http.post<QuizAttempt>(`${this.baseUrl}/quizattempts`, attempt);
  }

  getAttempts(): Observable<QuizAttempt[]> {
    return this.http.get<QuizAttempt[]>(`${this.baseUrl}/quizattempts`).pipe(
      catchError(() => of([]))
    );
  }

  getAttempt(id: string): Observable<QuizAttempt | null> {
    return this.http.get<QuizAttempt>(`${this.baseUrl}/quizattempts/${id}`).pipe(
      catchError(() => of(null))
    );
  }

  generateAiQuiz(req: {
    topic: string;
    category: string;
    difficulty: string;
    questionCount: number;
    timeLimitMinutes: number;
    apiKey?: string;
  }): Observable<Quiz> {
    return this.http.post<Quiz>(`${this.baseUrl}/admin/generate-ai-quiz`, req);
  }

  generateAiQuestion(req: AiSingleQuestionRequest): Observable<Question> {
    return this.http.post<Question>(`${this.baseUrl}/admin/generate-ai-question`, req);
  }

  getCategories(): Observable<CategoryItem[]> {
    return this.http.get<CategoryItem[]>(`${this.baseUrl}/admin/categories`).pipe(
      catchError(() => of([]))
    );
  }

  createCategory(category: CategoryItem): Observable<CategoryItem> {
    return this.http.post<CategoryItem>(`${this.baseUrl}/admin/categories`, category);
  }

  addQuestionToQuiz(quizId: string, question: Question): Observable<Question> {
    return this.http.post<Question>(`${this.baseUrl}/admin/quizzes/${quizId}/questions`, question);
  }

  previewMarkdownQuiz(req: { markdownText: string; title?: string; category?: string; categoryName?: string; difficulty?: string }): Observable<Quiz> {
    return this.http.post<Quiz>(`${this.baseUrl}/admin/parse-markdown-preview`, req);
  }

  importMarkdownQuiz(req: { markdownText: string; title?: string; category?: string; categoryName?: string; difficulty?: string }): Observable<Quiz> {
    return this.http.post<Quiz>(`${this.baseUrl}/admin/import-markdown`, req);
  }

  explainQuestion(req: AiQuestionExplainRequest): Observable<{ explanation: string }> {
    return this.http.post<{ explanation: string }>(`${this.baseUrl}/quizzes/explain-question`, req);
  }

  getAdminStats(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/admin/stats`).pipe(
      catchError(() => of({ totalQuizzes: 0, totalAttempts: 0, avgScore: 0, uniqueUsersCount: 0 }))
    );
  }
}
