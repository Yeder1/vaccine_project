import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { ListComponent } from './components/list/list.component';
import { NewsComponent } from './news.component';
import { NewsRoutes } from './news.routing';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { InputTextModule } from 'primeng/inputtext';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AddComponent } from './components/add/add.component';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ConfirmationService, MessageService } from 'primeng/api';
import { UpdateComponent } from './components/update/update.component';
@NgModule({
  imports: [
    CommonModule,
    NewsRoutes,
    SharedModule,
    TableModule,
    ButtonModule,
    ConfirmDialogModule,
    ToastModule,
    InputTextModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
  ],
  declarations: [NewsComponent, ListComponent, AddComponent, UpdateComponent],
  providers: [ConfirmationService, MessageService],
})
export class NewsModule {}
