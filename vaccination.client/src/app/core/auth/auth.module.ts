import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'src/app/shared/shared.module';
import { AuthRoutes } from './auth.routing';
import { LoginComponent } from './components/login/login.component';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  imports: [
    CommonModule,
    AuthRoutes,
    SharedModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
  ],
  declarations: [LoginComponent],
})
export class AuthModule {}
