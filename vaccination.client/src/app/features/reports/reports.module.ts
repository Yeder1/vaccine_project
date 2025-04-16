import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportsComponent } from './reports.component';
import { ReportRoute } from './reports.routing';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ReportCustomerResultComponent } from './report-customer-result/report-customer-result.component';
import { ReportInjectionResultComponent } from './report-injection-result/report-injection-result.component';
import { ReportVaccineResultComponent } from './report-vaccine-result/report-vaccine-result.component';




@NgModule({
  imports: [CommonModule, ReportRoute, SharedModule,FormsModule,
    ReactiveFormsModule],
  declarations: [ReportsComponent, ReportCustomerResultComponent,
    ReportInjectionResultComponent,
    ReportVaccineResultComponent],
})
export class ReportsModule {}