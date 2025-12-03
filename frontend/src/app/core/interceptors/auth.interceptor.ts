import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

function isTokenExpired(): boolean {
  const iso = localStorage.getItem('expiry');
  if (!iso) return true;
  const expMs = new Date(iso).getTime();
  return Date.now() > expMs;
}

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  if (req.url.includes('/auth/login') || req.url.includes('/auth/register')) {
    return next(req);
  }
  const token = localStorage.getItem('token');

  if (token) {
    if (isTokenExpired()) {
      localStorage.removeItem('token');
      localStorage.removeItem('tokenExpirationDate');
      router.navigate(['/login']);
      return throwError(() => new Error('Token expired'));
    }

    req = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
  }

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {

      if (err.status === 401) {
        localStorage.removeItem('token');
        localStorage.removeItem('tokenExpirationDate');
        router.navigate(['/login']);
      }

      return throwError(() => err);
    })
  );
};
