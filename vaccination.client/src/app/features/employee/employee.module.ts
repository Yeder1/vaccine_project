import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './components/list/list.component';
import { AddComponent } from './components/add/add.component';
import { UpdateComponent } from './components/update/update.component';
import { EmployeeComponent } from './employee.component';
import { EmployeeRoutes } from './employee.routing';
import { ConfirmationService, MessageService, SharedModule } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputTextModule } from 'primeng/inputtext';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ListComponent,
    AddComponent,
    UpdateComponent,
    EmployeeComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    EmployeeRoutes,
    ReactiveFormsModule,
    SharedModule,
    TableModule,
    ButtonModule,
    ConfirmDialogModule,
    ToastModule,
    InputTextModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
  ],
  providers: [ConfirmationService, MessageService]
})
export class EmployeeModule { }
