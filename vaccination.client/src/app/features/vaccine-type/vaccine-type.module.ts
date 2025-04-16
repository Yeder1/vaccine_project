import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { SharedModule } from 'src/app/shared/shared.module';
import { VaccineTypeAddComponent } from './components/add/add.component';
import { VaccineTypeListComponent } from './components/list/list.component';
import { VaccineTypeUpdateComponent } from './components/update/update.component';
import { VaccineTypeRoutes } from './vaccine-type.routing';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CheckboxModule } from 'primeng/checkbox';

@NgModule({
  declarations: [
    VaccineTypeListComponent,
    VaccineTypeUpdateComponent,
    VaccineTypeAddComponent
  ],
  imports: [

    CommonModule,
    VaccineTypeRoutes,
    SharedModule,
    ReactiveFormsModule,
    TableModule,
    FormsModule,
    FontAwesomeModule,
    CheckboxModule,
  ]
})
export class VaccineTypeModule { }
