import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, catchError, of } from 'rxjs';
import { User, AuthResponse } from '../models/user.model';

const LOCAL_STORAGE_TOKEN_KEY = 'quizmaster_jwt_token';
const LOCAL_STORAGE_REFRESH_TOKEN_KEY = 'quizmaster_refresh_token';
const LOCAL_STORAGE_USER_KEY = 'quizmaster_user_profile';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly http = inject(HttpClient);

  readonly currentUser = signal<User | null>(null);
  readonly token = signal<string | null>(null);
  readonly refreshToken = signal<string | null>(null);

  private refreshTimer: any = null;

  constructor() {
    this.restoreSession();
  }

  private restoreSession(): void {
    const savedToken = localStorage.getItem(LOCAL_STORAGE_TOKEN_KEY);
    const savedRefreshToken = localStorage.getItem(LOCAL_STORAGE_REFRESH_TOKEN_KEY);
    const savedUser = localStorage.getItem(LOCAL_STORAGE_USER_KEY);

    if (savedToken && savedUser) {
      try {
        this.token.set(savedToken);
        this.refreshToken.set(savedRefreshToken);
        this.currentUser.set(JSON.parse(savedUser));
        this.scheduleSilentRefresh(270);
      } catch (e) {
        this.logout();
      }
    }
  }

  hasPermission(permission: string): boolean {
    const user = this.currentUser();
    if (!user || !user.permissions) return false;
    return user.permissions.includes(permission);
  }

  googleLogin(idToken: string, fallbackName?: string, fallbackEmail?: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>('/api/auth/google-login', {
      idToken,
      fallbackName,
      fallbackEmail
    }).pipe(
      tap(res => this.handleAuthSuccess(res))
    );
  }

  requestTokenRefresh(): Observable<AuthResponse | null> {
    const currentRefresh = this.refreshToken();
    const user = this.currentUser();

    if (!currentRefresh || !user) return of(null);

    return this.http.post<AuthResponse>('/api/auth/refresh', {
      refreshToken: currentRefresh,
      userId: user.id
    }).pipe(
      tap(res => this.handleAuthSuccess(res)),
      catchError(() => {
        console.warn('Silent token refresh failed, logging out session');
        this.logout();
        return of(null);
      })
    );
  }

  private handleAuthSuccess(res: AuthResponse): void {
    if (res && res.token) {
      const user: User = {
        id: res.userId,
        email: res.email,
        name: res.name,
        pictureUrl: res.pictureUrl,
        role: res.role,
        permissions: res.permissions || []
      };

      this.token.set(res.token);
      this.refreshToken.set(res.refreshToken);
      this.currentUser.set(user);

      localStorage.setItem(LOCAL_STORAGE_TOKEN_KEY, res.token);
      if (res.refreshToken) {
        localStorage.setItem(LOCAL_STORAGE_REFRESH_TOKEN_KEY, res.refreshToken);
      }
      localStorage.setItem(LOCAL_STORAGE_USER_KEY, JSON.stringify(user));

      const refreshDelay = Math.max(30, (res.expiresInSeconds || 300) - 30);
      this.scheduleSilentRefresh(refreshDelay);
    }
  }

  private scheduleSilentRefresh(delaySeconds: number): void {
    this.clearRefreshTimer();
    this.refreshTimer = setTimeout(() => {
      this.requestTokenRefresh().subscribe();
    }, delaySeconds * 1000);
  }

  private clearRefreshTimer(): void {
    if (this.refreshTimer) {
      clearTimeout(this.refreshTimer);
      this.refreshTimer = null;
    }
  }

  logout(): void {
    this.clearRefreshTimer();
    this.token.set(null);
    this.refreshToken.set(null);
    this.currentUser.set(null);
    localStorage.removeItem(LOCAL_STORAGE_TOKEN_KEY);
    localStorage.removeItem(LOCAL_STORAGE_REFRESH_TOKEN_KEY);
    localStorage.removeItem(LOCAL_STORAGE_USER_KEY);
  }
}
