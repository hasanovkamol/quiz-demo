import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, catchError, of } from 'rxjs';
import { User, AuthResponse } from '../models/user.model';

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

  // ↓ Replace with your actual Google Client ID from Google Cloud Console
  // https://console.cloud.google.com → APIs & Services → Credentials
  private readonly GOOGLE_CLIENT_ID: string = '96736144451-0t99t4s70ka2quuuk3ov2ffatrto23i3.apps.googleusercontent.com';

  readonly currentUser = signal<User | null>(null);
  readonly token = signal<string | null>(null);
  readonly refreshToken = signal<string | null>(null);
  readonly isGoogleReady = signal<boolean>(false);

  private refreshTimer: any = null;

  constructor()
  {
    this.restoreSession();
    this.initGoogleIdentity();
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
    return this.http.post<AuthResponse>('/api/auth/google-login', {
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

    return this.http.post<AuthResponse>('/api/auth/refresh', {
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
