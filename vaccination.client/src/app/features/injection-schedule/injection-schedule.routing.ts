import { Routes, RouterModule } from '@angular/router';
import { ListComponent } from './components/list/list.component';
import { AddComponent } from './components/add/add.component';
import { UpdateComponent } from './components/update/update.component';
import { DetailsComponent } from './components/details/details.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: 'list',
    component: ListComponent,
  },
  {
    path: 'add',
    component: AddComponent,
  },
  {
    path: 'update/:id',
    component: UpdateComponent,
  },
  {
    path: 'details/:id',
    component: DetailsComponent,
  },
  {
    path: 'injection-schedule',
    loadChildren: () =>
      import('./injection-schedule.module').then((m) => m.InjectionScheduleModule),
  },
];

export const InjectionScheduleRoutes = RouterModule.forChild(routes);
