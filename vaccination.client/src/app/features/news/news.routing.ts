import { Routes, RouterModule } from '@angular/router';
import { ListComponent } from './components/list/list.component';
import { AddComponent } from './components/add/add.component';
import { UpdateComponent } from './components/update/update.component';

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
];

export const NewsRoutes = RouterModule.forChild(routes);
