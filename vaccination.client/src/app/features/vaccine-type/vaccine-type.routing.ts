import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VaccineTypeListComponent } from './components/list/list.component';
import { VaccineTypeAddComponent } from './components/add/add.component';
import { VaccineTypeUpdateComponent } from './components/update/update.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
    },
    {
        path: 'list',
        component: VaccineTypeListComponent
    },
    {
        path: 'add',
        component: VaccineTypeAddComponent
    },
    {
        path: 'update/:id',
        component: VaccineTypeUpdateComponent
    }
];

export const VaccineTypeRoutes = RouterModule.forChild(routes);

