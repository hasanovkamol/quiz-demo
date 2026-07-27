import { Injectable } from '@angular/core';

export interface AppConfig {
  apiUrl: string;
}

@Injectable({ providedIn: 'root' })
export class AppConfigService {
  // main.ts da bootstrap dan oldin config.json yuklangandan keyin window.__APP_CONFIG__ ga yoziladi
  get apiUrl(): string {
    const cfg = (window as any).__APP_CONFIG__;
    return cfg?.apiUrl ?? '/api';
  }
}
