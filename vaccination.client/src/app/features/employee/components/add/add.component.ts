import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AddEmployeeRequest } from '../../models/addEmployeeRequest';
import { EmployeeServiceService } from '../../services/employee.service.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { Employee } from '../../models/employee';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit, OnDestroy {
  addForm!: FormGroup;
  subscriptions: Subscription[] = [];
  imageUrl: string | ArrayBuffer | null | undefined = null;
  imageFile: File | null = null;

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeServiceService,
    private messageService: MessageService,
    private router: Router
  ) 
  
  {
    this.addForm = this.fb.group({
      employeeName: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required]],
      phone: ['', [Validators.required]],
      address: ['', [Validators.required]],
      email: ['', [Validators.required]],
      workingPlace: [''],
      position: [''],
      image: ['']
    });
  }

  ngOnInit() {
   
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
    const employee: Employee = {
      ...this.addForm.getRawValue(),
      gender: this.addForm.value.gender === 'true',
      isDeleted: false
    };
    // let request: AddEmployeeRequest = {
    //   ...this.addForm.value,
    // };

    const addRequest = new FormData();
    if (employee) {
      addRequest.append('Employee', JSON.stringify(employee));
    }
    if (this.imageFile) {
      addRequest.append('Image', this.imageFile); // Append the actual file to FormData
    }
    console.log(employee);

    // Gọi service để thêm employee
    this.subscriptions.push(
      this.employeeService.add(addRequest).subscribe({
        next: (data) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Add Success',
          });
          this.router.navigate(['/employee']);
        },
        error: (error) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Add Failed',
          });
        }
      })
    );
  }  

  onFileSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.imageFile = fileInput.files[0];

      const reader = new FileReader();
      reader.onload = (e) => {
        if (e.target?.result) {
          this.imageUrl = e.target.result; // Assign only if result is defined
        }
      };
      reader.readAsDataURL(this.imageFile);
    }
  }
}
