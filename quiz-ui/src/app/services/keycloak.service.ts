import { Injectable, signal, computed } from '@angular/core';
import Keycloak from 'keycloak-js';

export interface KeycloakConfig {
  url: string;
  realm: string;
  clientId: string;
}

@Injectable({ providedIn: 'root' })
export class KeycloakService {
  private kc: Keycloak | null = null;

  readonly isAuthenticated = signal<boolean>(false);
  readonly isInitialized = signal<boolean>(false);

  readonly username = computed(() => this.isAuthenticated() ? (this.kc?.tokenParsed?.['preferred_username'] ?? '') : '');
  readonly email = computed(() => this.isAuthenticated() ? (this.kc?.tokenParsed?.['email'] ?? '') : '');
  readonly fullName = computed(() => this.isAuthenticated() ? (this.kc?.tokenParsed?.['name'] ?? '') : '');
  readonly pictureUrl = computed(() => this.isAuthenticated() ? (this.kc?.tokenParsed?.['picture'] ?? '') : '');

  /** realm_access.roles dan olingan rolar ro'yxati */
  readonly roles = computed<string[]>(() => {
    if (!this.isAuthenticated()) return [];
    return (this.kc?.tokenParsed?.['realm_access']?.['roles'] ?? []) as string[];
  });

  /** `permission` claim dan olingan ruxsatlar (realm-export.json da sozlangan) */
  readonly permissions = computed<string[]>(() => {
    if (!this.isAuthenticated()) return [];
    const raw = this.kc?.tokenParsed?.['permission'];
    if (!raw) return [];
    return Array.isArray(raw) ? raw : [raw];
  });

  readonly isAdmin = computed(() => {
    const roles = this.roles();
    const perms = this.permissions();
    return roles.includes('Admin') || perms.includes('admin:stats') || perms.includes('users:manage');
  });

  /**
   * Keycloak ni ishga tushirish.
   * `redirectUri` — login'dan keyin qaytish manzili (default: joriy sahifa)
   */
  async init(config: KeycloakConfig, redirectUri?: string): Promise<boolean> {
    this.kc = new Keycloak({
      url: config.url,
      realm: config.realm,
      clientId: config.clientId,
    });

    try {
      // Angular <base href> dan to'g'ri base URL ni olamiz
      // GitHub Pages: https://hasanovkamol.github.io/quiz-demo/
      // Local:        http://localhost:4200/
      const baseHref = (document.querySelector('base') as HTMLBaseElement)?.href
        ?? (window.location.origin + '/');

      const authenticated = await this.kc.init({
        onLoad: 'check-sso',
        silentCheckSsoRedirectUri: baseHref + 'silent-check-sso.html',
        pkceMethod: 'S256',
        checkLoginIframe: false,
        redirectUri: redirectUri ?? baseHref,
      });

      this.isAuthenticated.set(authenticated);
      this.isInitialized.set(true);

      if (authenticated) {
        this._scheduleTokenRefresh();
      }

      return authenticated;
    } catch (err) {
      console.error('[KeycloakService] init failed:', err);
      this.isInitialized.set(true);
      return false;
    }
  }

  /** Keycloak login sahifasiga redirect qiladi */
  login(redirectUri?: string): void {
    const baseHref = (document.querySelector('base') as HTMLBaseElement)?.href
      ?? (window.location.origin + '/');
    this.kc?.login({ redirectUri: redirectUri ?? baseHref });
  }

  /** Keycloak logout — session o'chiriladi */
  logout(redirectUri?: string): void {
    this.kc?.logout({ redirectUri: redirectUri ?? window.location.origin });
  }

  /** Joriy access token ni qaytaradi (avtomatik yangilanadi) */
  async getToken(): Promise<string | null> {
    if (!this.kc) return null;
    try {
      // Token 30 soniya ichida tugasa — yangilaydi
      await this.kc.updateToken(30);
      return this.kc.token ?? null;
    } catch {
      console.warn('[KeycloakService] Token yangilash muvaffaqiyatsiz');
      this.isAuthenticated.set(false);
      return null;
    }
  }

  /** Ruxsatni tekshirish */
  hasPermission(permission: string): boolean {
    return this.permissions().includes(permission);
  }

  /** Rolni tekshirish */
  hasRole(role: string): boolean {
    return this.roles().includes(role);
  }

  /** Token ma'lumotlari (raw parsed JWT) */
  get tokenParsed(): Record<string, unknown> | null {
    return (this.kc?.tokenParsed as Record<string, unknown>) ?? null;
  }

  // Token muddati tugashidan 60 soniya oldin avtomatik yangilash
  private _refreshTimer: ReturnType<typeof setTimeout> | null = null;

  private _scheduleTokenRefresh(): void {
    this._clearRefreshTimer();
    const expiresIn = (this.kc?.tokenParsed?.['exp'] as number ?? 0) - Math.floor(Date.now() / 1000);
    const delay = Math.max(10, expiresIn - 60) * 1000;

    this._refreshTimer = setTimeout(async () => {
      try {
        await this.kc?.updateToken(60);
        this._scheduleTokenRefresh();
      } catch {
        console.warn('[KeycloakService] Silent token refresh muvaffaqiyatsiz');
        this.isAuthenticated.set(false);
      }
    }, delay);
  }

  private _clearRefreshTimer(): void {
    if (this._refreshTimer) {
      clearTimeout(this._refreshTimer);
      this._refreshTimer = null;
    }
  }
}
