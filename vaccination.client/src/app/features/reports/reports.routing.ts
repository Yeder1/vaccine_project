import { Routes, RouterModule } from '@angular/router';
import { ReportCustomerResultComponent } from './report-customer-result/report-customer-result.component';
import { ReportInjectionResultComponent } from './report-injection-result/report-injection-result.component';
import { ReportVaccineResultComponent } from './report-vaccine-result/report-vaccine-result.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'reports',
    pathMatch: 'full',
  },
  {
    path: 'report-customer-result',
    component: ReportCustomerResultComponent,
  },
  {
    path: 'report-injection-result',
    component: ReportInjectionResultComponent,
  },
  {
    path: 'report-vaccine-result',
    component: ReportVaccineResultComponent,
  },
];

export const ReportRoute = RouterModule.forChild(routes);
