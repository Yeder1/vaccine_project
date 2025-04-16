import { Component, OnDestroy, OnInit } from '@angular/core';
import { NewsService } from '../../services/news.service';
import { News } from '../models/new';
import { Subscription } from 'rxjs';
import {
  ConfirmationService,
  MessageService,
  ConfirmEventType,
} from 'primeng/api';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent implements OnInit, OnDestroy {
  selectedNews: News[] = [];
  news: News[] = [];
  subscriptions: Subscription[] = [];
  keyword: string = '';
  isLoading: boolean = false;
  constructor(
    private newsService: NewsService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) { }

  ngOnInit() {
    this.getFunction();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  getFunction() {
    this.isLoading = true;
    this.subscriptions.push(
      this.newsService.getAll(this.keyword).subscribe({
        next: (data) => {
          this.news = data;
        },
        complete: () => {
          this.isLoading = false;
        },
      })
    );
  }

  search() {
    this.getFunction();
  }

  onSelectionChange(event: any) {
    this.selectedNews = event;
  }

  deleteItem(item: News) {
    this.confirmationService.confirm({
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.subscriptions.push(
          this.newsService.delete([item]).subscribe({
            next: (data) => {
              this.messageService.add({
                severity: 'success',
                summary: 'Success',
                detail: 'Delete Success',
              });
              this.getFunction();
            },
          })
        );
      },
    });
  }

  deleteSelectedNews() {
    if (this.selectedNews.length === 0) {
      return;
    }

    this.confirmationService.confirm({
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.newsService.delete(this.selectedNews).subscribe({
          next: (data) => {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Delete Success',
            });
            this.getFunction();
          },
        });
      },
    });
  }
}
