import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main.component';
const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
      },
      {
        path: 'home',
        loadChildren: () =>
          import('./../../features/home/home.module').then((m) => m.HomeModule),
      },
      {
        path: 'news',
        loadChildren: () =>
          import('./../../features/news/news.module').then((m) => m.NewsModule),
      },
      {
        path: 'vaccination-result-management',
        loadChildren: () =>
          import('./../../features/vaccination-result-management/vaccination-result-management.module').then((m) => m.VaccinationResultManagementModule),
      },
      {
        path: 'injection-schedule',
        loadChildren: () =>
          import('./../../features/injection-schedule/injection-schedule.module').then((m) => m.InjectionScheduleModule),
      },
      {
        path: 'vaccines',
        loadChildren: () =>
          import('./../../features/vaccines/vaccines.module').then((m) => m.VaccinesModule),
      },
      {
        path: 'vaccine-type',
        loadChildren: () =>
          import('./../../features/vaccine-type/vaccine-type.module').then((m) => m.VaccineTypeModule),
      },
      {
        path: 'employee',
        loadChildren: () =>
          import('./../../features/employee/employee.module').then((m) => m.EmployeeModule),
      },
      {
        path: 'customer',
        loadChildren: () =>
          import('./../../features/customer/customer.module').then((m) => m.CustomerModule),
      },
      {
        path: 'reports',
        loadChildren: () =>
          import('./../../features/reports/reports.module').then((m) => m.ReportsModule),
      },
      {
        path: 'injection-schedule',
        loadChildren: () =>
          import('./../../features/injection-schedule/injection-schedule.module').then((m) => m.InjectionScheduleModule),
      },
    ],
  },
];

export const MainRoutes = RouterModule.forChild(routes);
