import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { KeyStorage } from 'src/app/core/enums/storage.enums';
import { AuthService } from '../../services/auth.service';
import { LoginRequest } from './../../models/loginRequest';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnDestroy {
  loginForm: FormGroup;
  isFail: boolean = false;
  msgErr: string = '';
  subscriptions: Subscription[] = [];

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  login() {
    var request: LoginRequest = this.loginForm.value;
    this.subscriptions.push(
      this.authService.login(request).subscribe({
        next: (data) => {
          this.isFail = false;
          localStorage.setItem(KeyStorage.user_id, data.userId);
          localStorage.setItem(KeyStorage.accessToken, data.accessToken);
          localStorage.setItem(KeyStorage.refreshToken, data.refreshToken);
          this.router.navigate(['']);
        },
        error: (err) => {
          this.isFail = true;
          this.msgErr = err?.error;
        },
      })
    );
  }
}
