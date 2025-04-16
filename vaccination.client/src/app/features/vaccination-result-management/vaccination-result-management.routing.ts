import { Routes, RouterModule } from '@angular/router';
import { AddComponent } from './components/add/add.component';
import { UpdateComponent } from './components/update/update.component';
import { VaccinationResultList } from './components/list/list.component';
const routes: Routes = [
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full',
    },
    {
        path: 'list',
        component: VaccinationResultList,
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

export const VaccinationResultManagementRoutes = RouterModule.forChild(routes);
