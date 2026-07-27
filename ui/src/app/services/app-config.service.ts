import { Injectable } from '@angular/core';
import { KeycloakConfig } from './keycloak.service';

export interface AppConfig {
  apiUrl: string;
  keycloak?: KeycloakConfig;
}

@Injectable({ providedIn: 'root' })
export class AppConfigService {
  // main.ts da bootstrap dan oldin config.json yuklangandan keyin window.__APP_CONFIG__ ga yoziladi
  get apiUrl(): string {
    const cfg = (window as any).__APP_CONFIG__;
    return cfg?.apiUrl ?? '/api';
  }

  get keycloakConfig(): KeycloakConfig {
    const cfg = (window as any).__APP_CONFIG__;
    return cfg?.keycloak ?? {
      url: 'http://localhost:8080',
      realm: 'quizmaster-realm',
      clientId: 'quizmaster-app',
    };
  }
}
