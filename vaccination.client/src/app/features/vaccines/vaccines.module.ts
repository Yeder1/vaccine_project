import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VaccinesComponent } from './vaccines.component';
import { VaccinesRoutes } from './vaccines.routing';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { VaccineAddComponent } from './components/add/vaccine-add.component';
import { VaccineUpdateComponent } from './components/update/vaccine-update.component';
import { TableModule } from 'primeng/table';
import { VaccineListComponent } from './components/list/vaccine-list.component';
import { VaccineImportComponent } from './components/vaccine-import/vaccine-import.component';

@NgModule({
  imports: [
    CommonModule,
    VaccinesRoutes,
    SharedModule,
    ReactiveFormsModule,
    TableModule,
  ],
  declarations: [
    VaccinesComponent,
    VaccineAddComponent,
    VaccineUpdateComponent,
    VaccineListComponent,
    VaccineImportComponent
  ],
})
export class VaccinesModule {}
