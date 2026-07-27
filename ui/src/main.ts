import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { AppConfigService } from './app/services/app-config.service';

// config.json ni ilovadan oldin yuklab, API URL ni aniqlash
fetch('config.json')
  .then(r => r.ok ? r.json() : { apiUrl: '/api' })
  .catch(() => ({ apiUrl: '/api' }))
  .then((cfg: { apiUrl: string }) => {
    // Singleton AppConfigService ga bog'liq bo'lgani uchun window._appCfg ga saqlaymiz
    (window as any).__APP_CONFIG__ = cfg;
    return bootstrapApplication(App, appConfig);
  })
  .catch((err) => console.error(err));

