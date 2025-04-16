import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { InjectionScheduleComponent } from './injection-schedule.component';
import { InjectionScheduleRoutes } from './injection-schedule.routing';
import { SharedModule } from 'src/app/shared/shared.module';
import { ListComponent } from './components/list/list.component';
import { AddComponent } from './components/add/add.component';
import { UpdateComponent } from './components/update/update.component';
import { DetailsComponent } from './components/details/details.component';

@NgModule({
  imports: [CommonModule, InjectionScheduleRoutes, SharedModule, ReactiveFormsModule],
  declarations: [InjectionScheduleComponent, ListComponent, AddComponent, UpdateComponent, DetailsComponent],
})
export class InjectionScheduleModule {}
