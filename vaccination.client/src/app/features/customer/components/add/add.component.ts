import { Component,OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddCustomerDTO } from 'src/app/models/customer/addCustomerDTO';
import {ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { MessageService } from 'primeng/api';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent {
  customerForm: FormGroup; 
  customerData: AddCustomerDTO; 
  isEditMode = false;  
  customerId: number | null = null;
  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService, 
    private messageService: MessageService,  
    private router: Router, 
    private route: ActivatedRoute,
    private datePipe: DatePipe, )
    {
    
      this.customerForm = this.fb.group({
        fullName: ['', [Validators.required, Validators.maxLength(100)]],  
        dateOfBirth: ['', Validators.required],  
        gender: ['', Validators.required],  
        identityCard: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],  
        address: ['', [Validators.required, Validators.maxLength(100)]],  
        username: ['', [Validators.required, Validators.maxLength(10)]],  
        password: ['', [Validators.required, Validators.maxLength(20)]],  
        confirmPassword: ['', [Validators.required, Validators.maxLength(20)]],  
        email: ['', [Validators.required, Validators.email, Validators.maxLength(25)]],  
        phone: ['', [Validators.required, Validators.pattern(/^\d{11}$/)]],  
      }, { validator: this.passwordMatchValidator });  
    this.customerData = {
      id: 0, 
      fullName: '',
      dateOfBirth: null,
      gender: true,
      identityCard: '',
      address: null,
      username: '',
      password: '',
      confirmPassword: '',
      email: '',
      phone: ''
    };
  }
  ngOnInit(): void {
    this.customerId = this.route.snapshot.paramMap.get('id') ? Number(this.route.snapshot.paramMap.get('id')) : null;
    if (this.customerId) {
      this.isEditMode = true;  
      this.loadCustomerData();  
    }
  }
  loadCustomerData() {
    this.customerService.getCustomerById(this.customerId!).subscribe({
      next: (response) => {
        const customer = response.data;
        const formattedDate = this.datePipe.transform(customer.dateOfBirth, 'yyyy-MM-dd');

      // Đặt giá trị vào form
      this.customerForm.patchValue({
        ...customer,
        dateOfBirth: formattedDate  // Định dạng lại ngày
      });
        this.customerForm.get('password')?.setValue('');  
        this.customerForm.get('confirmPassword')?.setValue('');  
      },
      error: (error) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load customer data.' });
      }
    });
  }

  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }
  isFieldInvalid(field: string) {
    return !this.customerForm.get(field)?.valid && this.customerForm.get(field)?.touched;
  }

  
  onSubmit() {
    if (this.customerForm.valid) {
      const customerData: AddCustomerDTO = this.customerForm.value;  // Lấy dữ liệu từ form
      if (this.isEditMode) {
        
        this.customerService.updateCustomer({ ...customerData, id: this.customerId! }).subscribe({
          next: (response) => {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Customer updated successfully!' });
            setTimeout(() => {
              this.router.navigate(['/customer/list']);
            }, 1000);
          },
          error: (error) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update customer.' });
          }
        });
      } else {
        // Nếu là thêm mới
        this.customerService.addCustomer(customerData).subscribe({
          next: (response) => {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Customer added successfully!' });
            setTimeout(() => {
              this.router.navigate(['/customer/list']);
            }, 1000);
          },
          error: (error) => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to add customer.' });
          }
        });
      }
    } else {
      this.customerForm.markAllAsTouched();  // Đánh dấu tất cả các trường để hiện lỗi nếu có
    }
  }

  // Reset form
  resetForm() {
    this.customerForm.reset();
  }

  // Hàm hủy form
  cancel() {
    this.router.navigate(['/customer/list']);
  }
  isPasswordMismatch() {
    return this.customerForm.errors?.['passwordMismatch'] && this.customerForm.get('confirmPassword')?.touched;
  }
  
}
