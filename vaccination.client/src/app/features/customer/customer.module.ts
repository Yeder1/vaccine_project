import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerRoutes } from './customer.routing';
import { AddComponent } from './components/add/add.component';
import { ListComponent } from './components/list/list.component';
import { UpdateComponent } from './components/update/update.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { DateFormatPipe } from 'src/app/core/pipes/date-format.pipe';
import { ConfirmDialogModule } from 'primeng/confirmdialog';  
import { ConfirmationService } from 'primeng/api'; 
import { DialogModule } from 'primeng/dialog';  
import { RadioButtonModule } from 'primeng/radiobutton';  
import { CalendarModule } from 'primeng/calendar';    
import { PasswordModule } from 'primeng/password';    
import { InputMaskModule } from 'primeng/inputmask'; 
import { CardModule } from 'primeng/card'; 
import { ReactiveFormsModule } from '@angular/forms';   
import { ToastModule } from 'primeng/toast';  
import { MessageService } from 'primeng/api';  
import { DatePipe } from '@angular/common';
@NgModule({
  declarations: [
    ListComponent,
    AddComponent,
    UpdateComponent,
    DateFormatPipe,  
  ],
  imports: [
    CommonModule,  
    CustomerRoutes,
    TableModule,
    FormsModule,
    HttpClientModule,  
    TableModule,       
    ButtonModule,      
    InputTextModule,   
    DropdownModule,    
    CheckboxModule,
    ConfirmDialogModule,
    DialogModule,
    RadioButtonModule,
    CalendarModule,
    PasswordModule,
    InputMaskModule,
    CardModule,
    ReactiveFormsModule, 
    ToastModule,  
    
  ],
  exports: [
    
  ],
  providers: [ConfirmationService,DatePipe],
})
export class CustomerModule { }