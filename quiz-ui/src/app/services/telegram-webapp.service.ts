import { Injectable, signal } from '@angular/core';

declare global {
  interface Window {
    Telegram?: {
      WebApp?: any;
    };
  }
}

export interface TelegramUser {
  id: number;
  first_name?: string;
  last_name?: string;
  username?: string;
  language_code?: string;
  photo_url?: string;
}

@Injectable({
  providedIn: 'root'
})
export class TelegramWebAppService {
  readonly isTelegramWebApp = signal<boolean>(false);
  readonly telegramUser = signal<TelegramUser | null>(null);

  constructor() {
    this.initTelegramWebApp();
  }

  private initTelegramWebApp(): void {
    let retries = 30;
    const checkTelegram = () => {
      const tg = window.Telegram?.WebApp;
      if (tg) {
        try {
          tg.ready();
          tg.expand();
          this.isTelegramWebApp.set(true);

          if (tg.initDataUnsafe?.user) {
            this.telegramUser.set(tg.initDataUnsafe.user);
          }
        } catch (e) {
          console.warn('Failed to initialize Telegram WebApp SDK:', e);
        }
      } else if (retries > 0) {
        retries--;
        setTimeout(checkTelegram, 100);
      }
    };
    checkTelegram();
  }

  /**
   * Returns a formatted Telegram username (e.g. "@username" or "First Last")
   */
  getFormattedUserName(): string | null {
    const user = this.telegramUser();
    if (!user) return null;

    if (user.username) {
      return `@${user.username}`;
    }

    const fullName = `${user.first_name || ''} ${user.last_name || ''}`.trim();
    return fullName || `TelegramUser_${user.id}`;
  }

  get initData(): string {
    return window.Telegram?.WebApp?.initData || '';
  }

  close(): void {
    window.Telegram?.WebApp?.close();
  }
}
