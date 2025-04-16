import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { EmployeeServiceService } from '../../services/employee.service.service';
import { Employee } from '../../models/employee';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { environment } from 'src/environments/environment';
import { ImageReference } from 'src/environments/image-reference';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss'],
})
export class UpdateComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];
  detail?: Employee;
  id: string | null = null;
  imageUrl: string | ArrayBuffer | null | undefined = null;
  imageFile: File | null = null;
  editForm!: FormGroup;
  constructor(
    private employeeService: EmployeeServiceService,
    private activedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private messageService: MessageService,
    private router: Router
  ) {}
  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe);
  }

  ngOnInit() {
    this.editForm = this.fb.group({
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
    this.subscriptions.push(
      this.activedRoute.paramMap.subscribe({
        next: (params) => {
          this.id = params.get('id');
          this.getDetail();
          console.log()
        },
      })
    );
  }

  getDetail() {
    this.subscriptions.push(
      this.employeeService.getDetail(this.id!).subscribe({
        next: (response) => {
          // Kiểm tra xem response có thành công không
          if (response.statusCode === 200) {
            // Lấy thông tin từ data
            const employeeData = response.data;
            this.detail = employeeData; // Lưu thông tin chi tiết
  
            // Thiết lập giá trị cho editForm
            this.editForm.setValue({
              employeeName: employeeData.employeeName,
              gender: employeeData.gender,
              dateOfBirth: employeeData.dateOfBirth,
              phone: employeeData.phone,
              address: employeeData.address, // Sửa lại để lấy đúng address
              email: employeeData.email, // Sửa lại để lấy đúng email
              workingPlace: employeeData.workingPlace, // Sửa lại để lấy đúng workingPlace
              position: employeeData.position, // Sửa lại để lấy đúng position
              image: employeeData.image // Sửa lại để lấy đúng image
            });
            this.imageUrl = `${environment.BASE_API_URL}/api/image/ref/${ImageReference.Employee}_${this.id}`;
          } else {
            // Xử lý trường hợp không thành công (nếu cần)
            console.error(response.message);
          }
        },
        error: (error) => {
          console.error('Error fetching employee details:', error);
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

  update() {
    // let request: Employee = {
    //   ...this.editForm.value,
    //   id: this.id,
    // };
    // this.subscriptions.push(
    //   this.employeeService.update(request).subscribe({
    //     next: () => {
    //       this.messageService.add({
    //         severity: 'success',
    //         summary: 'Success',
    //         detail: 'Delete Success',
    //       });
    //       this.router.navigate(['/employee/list']);
    //     },
    //   })
    // );

    const employee: Employee = {
      ...this.editForm.getRawValue(),
      id: this.id,
    };

    const editRequest = new FormData();
    if (employee) {
      editRequest.append('Employee', JSON.stringify(employee));
    }
    if (this.imageFile) {
      editRequest.append('Image', this.imageFile); // Append the actual file to FormData
    }
    console.log(employee);

    // Gọi service để thêm employee
    this.subscriptions.push(
      this.employeeService.update(editRequest).subscribe({
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
}
