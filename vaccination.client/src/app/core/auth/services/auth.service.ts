import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { API_URL } from '../../contants/url';
import { LoginResponse } from '../models/loginResponse';
import { LoginRequest } from '../models/loginRequest';
import { KeyStorage } from '../../enums/storage.enums';
import { TokenResponse } from '../models/tokenResponse';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl = API_URL + '/Auth';
  constructor(private httpClient: HttpClient, private router: Router) { }
  login(request: LoginRequest): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(this.apiUrl + '/Login', request);
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/auth/login']);
  }

  refreshToken(): Observable<TokenResponse | null> {
    const refreshToken = localStorage.getItem(KeyStorage.refreshToken);
    const accessToken = localStorage.getItem(KeyStorage.accessToken);
    let request = {
      accessToken: accessToken,
      refreshToken: refreshToken,
    };
    if (!refreshToken) return of(null);

    return this.httpClient.post<TokenResponse>(
      this.apiUrl + '/refresh-token',
      request
    );
  }
}
