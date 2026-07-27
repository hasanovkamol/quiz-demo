import { Injectable, signal } from '@angular/core';

declare global {
  interface Window {
    Telegram?: {
      WebApp?: any;
    };
  }
}

@Injectable({
  providedIn: 'root'
})
export class TelegramWebAppService {
  readonly isTelegramWebApp = signal<boolean>(false);
  readonly telegramUser = signal<any>(null);

  constructor() {
    this.initTelegramWebApp();
  }

  private initTelegramWebApp(): void {
    const tg = window.Telegram?.WebApp;
    if (tg) {
      tg.ready();
      tg.expand();
      this.isTelegramWebApp.set(true);
      if (tg.initDataUnsafe?.user) {
        this.telegramUser.set(tg.initDataUnsafe.user);
      }
    }
  }

  get initData(): string {
    return window.Telegram?.WebApp?.initData || '';
  }

  close(): void {
    window.Telegram?.WebApp?.close();
  }
}
