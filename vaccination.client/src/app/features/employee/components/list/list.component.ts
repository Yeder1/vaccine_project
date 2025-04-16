import { Component, OnDestroy, OnInit } from '@angular/core';
import { EmployeeServiceService } from '../../services/employee.service.service';
import { Employee } from '../../models/employee';
import { Subscription } from 'rxjs';
import {
  ConfirmationService,
  MessageService,
  ConfirmEventType,
} from 'primeng/api';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent  implements OnInit, OnDestroy {
  selectedEmployee: Employee[] = [];
  employees: Employee[] = [];
  subscriptions: Subscription[] = [];
  keyword: string = '';
  isLoading: boolean = false;
  constructor(
    private employeeService: EmployeeServiceService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.getFunction();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  getFunction() {
    this.isLoading = true;
    this.subscriptions.push(
      this.employeeService.getAll().subscribe({
        next: (response) => {
          this.employees = response.data; // Truy cập vào mảng "data"
          console.log(this.employees); // In ra mảng nhân viên
        },
        complete: () => {
          this.isLoading = false;
        },
        error: (err) => {
          console.error('API error', err); // Thêm phần xử lý lỗi (nếu có)
          this.isLoading = false;
        }
      })
    );
  }

  search() {
    this.isLoading = true;
    if(this.keyword === "" || this.keyword === null) {
      this.getFunction();
    } else {
      this.subscriptions.push(
        this.employeeService.getByKeyWord(this.keyword).subscribe({
          next: (response) => {
            this.employees = response.data; // Truy cập vào mảng "data"
            console.log(this.employees); // In ra mảng nhân viên
          },
          complete: () => {
            this.isLoading = false;
          },
          error: (err) => {
            console.error('API error', err); // Thêm phần xử lý lỗi (nếu có)
            this.isLoading = false;
          }
        })
      );
    }
  }

  onSelectionChange(event: any) {
    this.selectedEmployee = event;
  }

  deleteItem(item: Employee) {
    this.confirmationService.confirm({
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.subscriptions.push(
          this.employeeService.delete([item]).subscribe({
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

  deleteSelectedEmployee() {
    if (this.selectedEmployee.length === 0) {
      return;
    }

    this.confirmationService.confirm({
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.employeeService.delete(this.selectedEmployee).subscribe({
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
