import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
const router = inject(Router);
const token = localStorage.getItem('JWT');
  // maneja todas las llamadas menos la del login
const excludedUrls = ['/login', '/registro', '/resetpassword'];

if (excludedUrls.some(url => req.url.includes(url))) {
  return next(req);
}
  // Si hay token, clonamos la request con el header Authorization
  const authReq = token
    ? req.clone({
        setHeaders: { Authorization: `Bearer ${token}` },
      })
    : req;
  // Pasamos la request al siguiente interceptor / handler
  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      // Si el token no es válido o expiró
      if (error.status === 401) {
        localStorage.removeItem('JWT');
        router.navigate(['/login']);
      }
      return throwError(() => error);
    })
  );
};