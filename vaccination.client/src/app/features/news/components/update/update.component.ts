import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { NewsService } from '../../services/news.service';
import { News } from '../models/new';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NewTypesService } from '../../services/newTypes.service';
import { NewType } from '../models/newType';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css'],
})
export class UpdateComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];
  detail?: News;
  id: string | null = null;
  editForm!: FormGroup;
  listTypes: NewType[] = [];
  constructor(
    private newService: NewsService,
    private activedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private newTypesService: NewTypesService,
    private messageService: MessageService,
    private router: Router
  ) {}
  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe);
  }

  ngOnInit() {
    this.editForm = this.fb.group({
      title: ['', [Validators.required]],
      preview: ['', [Validators.required]],
      content: ['', [Validators.required]],
      newsTypeId: ['', [Validators.required]],
    });
    this.subscriptions.push(
      this.activedRoute.paramMap.subscribe({
        next: (params) => {
          this.id = params.get('id');
          this.getDetail();
        },
      })
    );

    this.subscriptions.push(
      this.newTypesService.getAll().subscribe({
        next: (data) => {
          this.listTypes = data;
        },
      })
    );
  }

  getDetail() {
    this.subscriptions.push(
      this.newService.getDetail(this.id!).subscribe({
        next: (data) => {
          this.detail = data;
          this.editForm.setValue({
            title: this.detail.title,
            content: this.detail.content,
            preview: this.detail.preview,
            newsTypeId: this.detail.newsTypeId,
          });
        },
      })
    );
  }

  update() {
    let request: News = {
      ...this.editForm.value,
      postDate: this.detail?.postDate,
      id: this.id,
    };
    this.subscriptions.push(
      this.newService.update(request).subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Delete Success',
          });
          this.router.navigate(['/news']);
        },
      })
    );
  }
}
