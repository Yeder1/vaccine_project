import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddComponent } from './components/add/add.component';
import { UpdateComponent } from './components/update/update.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { VaccinationResultManagementComponent } from './vaccination-result-management.component';
import { VaccinationResultManagementRoutes } from './vaccination-result-management.routing';
import { VaccinationResultList } from './components/list/list.component';
import { TableModule } from 'primeng/table';
import { HttpClientModule } from '@angular/common/http';
import { InputTextModule } from 'primeng/inputtext';


import { TagModule } from 'primeng/tag';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';

@NgModule({
  declarations: [
    AddComponent,
    UpdateComponent,
    VaccinationResultManagementComponent,
    VaccinationResultList,
  ],
  imports: [
    CommonModule,
    SharedModule,
    VaccinationResultManagementRoutes,
    TableModule,
    HttpClientModule,
    InputTextModule,
    TagModule,
    ConfirmDialogModule,
    ToastModule, // Include the modules here
  ],
  exports: [
    AddComponent,
    UpdateComponent,
    VaccinationResultManagementComponent,
    VaccinationResultList,
  ],
})
export class VaccinationResultManagementModule { }
