import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { checkLoginGuard } from './core/guards/check-login.guard';
import { NotFoundComponent } from './features/not-found/not-found.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('./core/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: '',
    loadChildren: () =>
      import('./core/main/main.module').then((m) => m.MainModule),
    canActivate: [checkLoginGuard],
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
