import { Routes, RouterModule } from '@angular/router';
import { VaccineListComponent } from './components/list/vaccine-list.component';
import { VaccineAddComponent } from './components/add/vaccine-add.component';
import { VaccineUpdateComponent } from './components/update/vaccine-update.component';
import { VaccineImportComponent } from './components/vaccine-import/vaccine-import.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full',
  },
  {
    path: 'list',
    component: VaccineListComponent,
  },
  {
    path: 'add',
    component: VaccineAddComponent,
  },
  {
    path: 'update/:id',
    component: VaccineUpdateComponent,
  },
  {
    path: 'import',
    component: VaccineImportComponent,
  },
];

export const VaccinesRoutes = RouterModule.forChild(routes);
