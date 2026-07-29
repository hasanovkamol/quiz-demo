import { Injectable, signal, computed, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, catchError, of } from 'rxjs';
import { User, AuthResponse } from '../models/user.model';
import { AppConfigService } from './app-config.service';

import { TelegramWebAppService } from './telegram-webapp.service';

const LOCAL_STORAGE_TOKEN_KEY = 'quizmaster_jwt_token';
const LOCAL_STORAGE_REFRESH_TOKEN_KEY = 'quizmaster_refresh_token';
const LOCAL_STORAGE_USER_KEY = 'quizmaster_user_profile';

// Google Identity Services type declarations
declare const google: {
  accounts: {
    id: {
      initialize: (config: {
        client_id: string;
        callback: (response: { credential: string }) => void;
        auto_select?: boolean;
        cancel_on_tap_outside?: boolean;
      }) => void;
      prompt: (notification?: (n: { isDisplayed: () => boolean }) => void) => void;
      disableAutoSelect: () => void;
      renderButton: (element: HTMLElement, options: Record<string, unknown>) => void;
    };
  };
};

@Injectable({
  providedIn: 'root'
})
export class AuthService
{
  private readonly http = inject(HttpClient);
  private readonly appConfig = inject(AppConfigService);
  private readonly telegramWebApp = inject(TelegramWebAppService);

  private get apiBase(): string { return this.appConfig.apiUrl; }

  // ↓ Replace with your actual Google Client ID from Google Cloud Console
  // https://console.cloud.google.com → APIs & Services → Credentials
  private readonly GOOGLE_CLIENT_ID: string = '96736144451-0t99t4s70ka2quuuk3ov2ffatrto23i3.apps.googleusercontent.com';

  readonly currentUser = signal<User | null>(null);
  readonly token = signal<string | null>(null);
  readonly refreshToken = signal<string | null>(null);
  readonly isGoogleReady = signal<boolean>(false);

  readonly isAdmin = computed(() => {
    const user = this.currentUser();
    if (!user) return false;
    const role = (user.role || '').toLowerCase();
    if (role === 'admin') return true;
    return user.permissions?.includes('admin:stats') || user.permissions?.includes('users:manage') || false;
  });

  private refreshTimer: any = null;

  constructor()
  {
    this.restoreSession();
    this.initTelegramAuth();
    this.initGoogleIdentity();
  }

  private initTelegramAuth(): void
  {
    let retries = 20;
    const attemptAuth = () => {
      const isTg = this.telegramWebApp.isTelegramWebApp();
      const user = this.telegramWebApp.telegramUser();
      const tgUserId = user?.id || 0;

      if (isTg || tgUserId > 0)
      {
        const username = user?.username || '';
        const name = this.telegramWebApp.getFormattedUserName() || (user ? `${user.first_name || ''} ${user.last_name || ''}`.trim() : 'Telegram User');
        const initData = this.telegramWebApp.initData;

        // Immediate client-side user fallback so UI is NEVER blocked
        if (!this.currentUser())
        {
          const cleanUsername = (username || '').toLowerCase().replace('@', '');
          const isAdmin = cleanUsername === 'hasanovkamol';
          const fallbackUser: User = {
            id: (tgUserId > 0 ? tgUserId : 'tg_guest').toString(),
            email: `${tgUserId}@telegram.user`,
            name: name || 'Telegram User',
            pictureUrl: user?.photo_url || '',
            role: isAdmin ? 'Admin' : 'User',
            permissions: isAdmin ? ['admin:stats', 'users:manage', 'quiz:create', 'quiz:delete'] : []
          };
          this.currentUser.set(fallbackUser);
        }

        // Attempt backend JWT auth in background
        if (tgUserId > 0 && initData)
        {
          this.telegramLogin(tgUserId, username, name, initData).subscribe({
            next: () => console.log('Telegram WebApp Auto-Authenticated with Backend JWT!'),
            error: (err) => console.warn('Telegram WebApp Backend Auth fallback active:', err)
          });
        }
      } else if (retries > 0) {
        retries--;
        setTimeout(attemptAuth, 100);
      }
    };
    attemptAuth();
  }

  telegramLogin(telegramUserId: number, username?: string, name?: string, initData?: string): Observable<AuthResponse | null>
  {
    return this.http.post<AuthResponse>(`${this.apiBase}/telegram/auth`, {
      telegramUserId,
      username,
      name,
      initData
    }).pipe(
      tap(res => this.handleAuthSuccess(res)),
      catchError(err => {
        console.warn('Telegram backend login error, keeping client-side Telegram profile:', err);
        return of(null);
      })
    );
  }

  private restoreSession(): void
  {
    const savedToken = localStorage.getItem(LOCAL_STORAGE_TOKEN_KEY);
    const savedRefreshToken = localStorage.getItem(LOCAL_STORAGE_REFRESH_TOKEN_KEY);
    const savedUser = localStorage.getItem(LOCAL_STORAGE_USER_KEY);

    if (savedToken && savedUser)
    {
      try
      {
        this.token.set(savedToken);
        this.refreshToken.set(savedRefreshToken);
        this.currentUser.set(JSON.parse(savedUser));
        this.scheduleSilentRefresh(270);
      } catch (e)
      {
        this.logout();
      }
    }
  }

  private initGoogleIdentity(): void
  {
    const waitForGoogle = () =>
    {
      if (typeof google !== 'undefined' && google?.accounts?.id)
      {
        google.accounts.id.initialize({
          client_id: this.GOOGLE_CLIENT_ID,
          callback: (response) =>
          {
            this.handleGoogleCredential(response.credential);
          },
          auto_select: false,
          cancel_on_tap_outside: true
        });
        this.isGoogleReady.set(true);
      } else
      {
        setTimeout(waitForGoogle, 300);
      }
    };
    setTimeout(waitForGoogle, 500);
  }

  /**
   * Renders standard Google Sign-In button into a DOM container element
   */
  renderGoogleButton(containerElement: HTMLElement, onSuccess: (user: User) => void, onError: () => void): void {
    const attachButton = () => {
      if (typeof google !== 'undefined' && google?.accounts?.id) {
        this._pendingGoogleCallback = onSuccess;
        this._pendingGoogleErrorCallback = onError;
        google.accounts.id.renderButton(containerElement, {
          type: 'standard',
          theme: 'filled_black',
          size: 'large',
          text: 'continue_with',
          shape: 'pill',
          width: 320
        });
      } else {
        setTimeout(attachButton, 300);
      }
    };
    attachButton();
  }

  /**
   * Triggers Google One Tap / Sign-In popup.
   * onSuccess callback is called with User after backend authentication.
   */
  triggerGoogleSignIn(onSuccess: (user: User) => void, onError: () => void): void
  {
    if (!this.isGoogleReady())
    {
      console.warn('Google Identity Services SDK is not ready yet. Retrying...');
      setTimeout(() => this.triggerGoogleSignIn(onSuccess, onError), 500);
      return;
    }

    if (this.GOOGLE_CLIENT_ID === 'YOUR_GOOGLE_CLIENT_ID.apps.googleusercontent.com')
    {
      // Demo mode fallback (no real Google Client ID configured)
      onError();
      return;
    }

    google.accounts.id.prompt();
    this._pendingGoogleCallback = onSuccess;
    this._pendingGoogleErrorCallback = onError;
  }

  private _pendingGoogleCallback: ((user: User) => void) | null = null;
  private _pendingGoogleErrorCallback: (() => void) | null = null;

  private handleGoogleCredential(idToken: string): void
  {
    this.googleLogin(idToken).subscribe({
      next: (res) =>
      {
        const user: User = {
          id: res.userId,
          email: res.email,
          name: res.name,
          pictureUrl: res.pictureUrl,
          role: res.role,
          permissions: res.permissions || []
        };
        if (this._pendingGoogleCallback)
        {
          this._pendingGoogleCallback(user);
          this._pendingGoogleCallback = null;
          this._pendingGoogleErrorCallback = null;
        }
      },
      error: () =>
      {
        if (this._pendingGoogleErrorCallback)
        {
          this._pendingGoogleErrorCallback();
          this._pendingGoogleCallback = null;
          this._pendingGoogleErrorCallback = null;
        }
      }
    });
  }

  hasPermission(permission: string): boolean
  {
    const user = this.currentUser();
    if (!user || !user.permissions) return false;
    return user.permissions.includes(permission);
  }

  googleLogin(idToken: string, fallbackName?: string, fallbackEmail?: string): Observable<AuthResponse>
  {
    return this.http.post<AuthResponse>(`${this.apiBase}/auth/google-login`, {
      idToken,
      fallbackName,
      fallbackEmail
    }).pipe(
      tap(res => this.handleAuthSuccess(res))
    );
  }

  requestTokenRefresh(): Observable<AuthResponse | null>
  {
    const currentRefresh = this.refreshToken();
    const user = this.currentUser();

    if (!currentRefresh || !user) return of(null);

    return this.http.post<AuthResponse>(`${this.apiBase}/auth/refresh`, {
      refreshToken: currentRefresh,
      userId: user.id
    }).pipe(
      tap(res => this.handleAuthSuccess(res)),
      catchError(() =>
      {
        console.warn('Silent token refresh failed, logging out session');
        this.logout();
        return of(null);
      })
    );
  }

  private handleAuthSuccess(res: AuthResponse): void
  {
    if (res && res.token)
    {
      const rawUser = (res as any).user;
      const user: User = {
        id: res.userId || rawUser?.id,
        email: res.email || rawUser?.email,
        name: res.name || rawUser?.name,
        pictureUrl: res.pictureUrl || rawUser?.pictureUrl,
        role: res.role || rawUser?.role,
        permissions: res.permissions || rawUser?.permissions || []
      };

      this.token.set(res.token);
      this.refreshToken.set(res.refreshToken);
      this.currentUser.set(user);

      localStorage.setItem(LOCAL_STORAGE_TOKEN_KEY, res.token);
      if (res.refreshToken)
      {
        localStorage.setItem(LOCAL_STORAGE_REFRESH_TOKEN_KEY, res.refreshToken);
      }
      localStorage.setItem(LOCAL_STORAGE_USER_KEY, JSON.stringify(user));

      const refreshDelay = Math.max(30, (res.expiresInSeconds || 300) - 30);
      this.scheduleSilentRefresh(refreshDelay);
    }
  }

  private scheduleSilentRefresh(delaySeconds: number): void
  {
    this.clearRefreshTimer();
    this.refreshTimer = setTimeout(() =>
    {
      this.requestTokenRefresh().subscribe();
    }, delaySeconds * 1000);
  }

  private clearRefreshTimer(): void
  {
    if (this.refreshTimer)
    {
      clearTimeout(this.refreshTimer);
      this.refreshTimer = null;
    }
  }

  logout(): void
  {
    this.clearRefreshTimer();

    // Sign out from Google Identity Services to clear session
    if (typeof google !== 'undefined' && google?.accounts?.id)
    {
      google.accounts.id.disableAutoSelect();
    }

    this.token.set(null);
    this.refreshToken.set(null);
    this.currentUser.set(null);
    localStorage.removeItem(LOCAL_STORAGE_TOKEN_KEY);
    localStorage.removeItem(LOCAL_STORAGE_REFRESH_TOKEN_KEY);
    localStorage.removeItem(LOCAL_STORAGE_USER_KEY);
  }
}
