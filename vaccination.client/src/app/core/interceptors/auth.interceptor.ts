import { inject, Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { KeyStorage } from '../enums/storage.enums';
import { AuthService } from '../auth/services/auth.service';
import { TokenResponse } from '../auth/models/tokenResponse';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor() { }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const authService = inject(AuthService);
    const authRequest = request.clone({
      setHeaders: {
        Authorization: `Bearer ${localStorage.getItem(KeyStorage.accessToken)}`,
      },
    });

    return next.handle(authRequest).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          // Try refreshing the token if 401 Unauthorized error
          return authService.refreshToken().pipe(
            switchMap((tokenResponse: TokenResponse | null) => {
              if (tokenResponse) {
                // Update both accessToken and refreshToken in localStorage
                localStorage.setItem(
                  KeyStorage.accessToken,
                  tokenResponse.accessToken
                );
                localStorage.setItem(
                  KeyStorage.refreshToken,
                  tokenResponse.refreshToken
                );

                // Clone the original request with the new access token
                const newAuthRequest = request.clone({
                  setHeaders: {
                    Authorization: `Bearer ${tokenResponse.accessToken}`,
                  },
                });
                return next.handle(newAuthRequest);
              } else {
                // If refreshToken is null, log out and return an error
                authService.logout();
                return throwError('Failed to refresh token');
              }
            }),
            catchError((refreshError) => {
              // If refreshing the token fails, log out and handle accordingly
              authService.logout();
              return throwError(refreshError);
            })
          );
        }
        // If the error is not 401, pass it on to be handled elsewhere
        return throwError(error);
      })
    );
  }
}
