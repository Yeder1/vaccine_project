import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { DatePipe } from '@angular/common';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { VaccineTypeComponent } from './features/vaccine-type/vaccine-type.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DateFormatPipe } from './core/pipes/date-format.pipe';

@NgModule({
  declarations: 
  [
    AppComponent,
    VaccineTypeComponent, 
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ToastModule,
    FontAwesomeModule,
  ],
  providers: [
    DatePipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    ConfirmationService,
    MessageService,
    DateFormatPipe
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
