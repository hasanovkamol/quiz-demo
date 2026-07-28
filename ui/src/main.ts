import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { KeycloakService } from './app/services/keycloak.service';

// 1. config.json ni ilovadan oldin yuklab, API URL va Keycloak konfigini aniqlash
fetch('config.json')
  .then(r => r.ok ? r.json() : { apiUrl: '/api' })
  .catch(() => ({ apiUrl: '/api' }))
  .then(async (cfg: { apiUrl: string; keycloak?: any }) => {
    // AppConfigService singleton window.__APP_CONFIG__ dan o'qiydi
    (window as any).__APP_CONFIG__ = cfg;

    // 2. Keycloak ni Angular bootstrap'dan OLDIN bloklamaydigan qilib (orqa fonda) ishga tushiramiz
    if (cfg.keycloak) {
      const kc = new KeycloakService();
      (window as any).__KEYCLOAK_SERVICE__ = kc;
      kc.init(cfg.keycloak).catch(err => console.error('[main] Keycloak init failed:', err));
    }

    // 3. Angular ilovasini ishga tushiramiz
    return bootstrapApplication(App, appConfig);
  })
  .catch((err) => console.error('[main] bootstrap error:', err));
