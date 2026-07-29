import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { from, switchMap } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { KeycloakService } from '../services/keycloak.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const keycloakService = inject(KeycloakService);

  // Keycloak autentifikatsiya bo'lgan bo'lsa — Keycloak tokenini ishlatamiz
  if (keycloakService.isAuthenticated()) {
    return from(keycloakService.getToken()).pipe(
      switchMap(token => {
        if (token) {
          const cloned = req.clone({
            setHeaders: { Authorization: `Bearer ${token}` }
          });
          return next(cloned);
        }
        return next(req);
      })
    );
  }

  // Fallback: Google OAuth token (mavjud tizim)
  const googleToken = authService.token();
  if (googleToken) {
    const cloned = req.clone({
      setHeaders: { Authorization: `Bearer ${googleToken}` }
    });
    return next(cloned);
  }

  return next(req);
};
