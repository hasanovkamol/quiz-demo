import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { Quiz, QuizAttempt } from '../models/quiz.model';

@Injectable({
  providedIn: 'root'
})
export class QuizApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api';

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

  getAdminStats(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/admin/stats`).pipe(
      catchError(() => of({ totalQuizzes: 0, totalAttempts: 0, avgScore: 0, uniqueUsersCount: 0 }))
    );
  }
}
