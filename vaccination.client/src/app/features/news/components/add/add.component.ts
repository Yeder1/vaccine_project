import { NewTypesService } from './../../services/newTypes.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { NewType } from '../models/newType';
import { AddNewRequest } from '../models/addRequest';
import { NewsService } from '../../services/news.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css'],
})
export class AddComponent implements OnInit, OnDestroy {
  addForm!: FormGroup;
  subscriptions: Subscription[] = [];
  listTypes: NewType[] = [];
  constructor(
    private fb: FormBuilder,
    private newTypesService: NewTypesService,
    private newService: NewsService,
    private messageService: MessageService,
    private router: Router
  ) {
    this.addForm = this.fb.group({
      title: ['', [Validators.required]],
      preview: ['', [Validators.required]],
      content: ['', [Validators.required]],
      newsTypeId: ['', [Validators.required]],
    });
  }

  ngOnInit() {
    this.subscriptions.push(
      this.newTypesService.getAll().subscribe({
        next: (data) => {
          this.listTypes = data;
        },
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  trackById(index: number, type: { id: number }): number {
    return type.id; // Return the unique id of the type
  }

  reset() {
    this.addForm.reset();
  }
  add() {
    let request: AddNewRequest = {
      ...this.addForm.value,
      postDate: new Date(Date.now()).toISOString(),
    };
    this.subscriptions.push(
      this.newService.add(request).subscribe({
        next: (data) => {
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
