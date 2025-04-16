import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { RouterModule } from '@angular/router';
import { MainRoutes } from './main.routing';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MainRoutes,
    MatSidenavModule,
    MatTreeModule,
    MatIconModule,
    MatButtonModule,
    MatProgressBarModule,
  ],
  declarations: [MainComponent, HeaderComponent, SidebarComponent],
})
export class MainModule {}
