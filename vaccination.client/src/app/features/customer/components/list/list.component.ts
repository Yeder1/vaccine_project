import { Component, OnInit, ViewChild } from '@angular/core';
import { CustomerService } from '../../services/customer.service';
import { CustomerDTO } from 'src/app/models/customer/customerDTO';
import { Table } from 'primeng/table'; 
import { Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api'; 
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
  providers: [ConfirmationService]
})
export class ListComponent {
  @ViewChild('table', { static: false }) table!: Table;
  customers: CustomerDTO[] = [];
  selectedCustomers: CustomerDTO[] = [];
  rows: number = 10;  // Số lượng dòng hiển thị mặc định
  rowsPerPageOptions = [5, 10, 20];  // Các tùy chọn số dòng trên mỗi trang
  errorMessage: string = '';
  displayConfirmDialog: boolean = false; 
  constructor(private customerService: CustomerService,
     private router:Router,
     private confirmationService: ConfirmationService) { }

  
  ngOnInit(): void {
    this.getCustomers();
  }
  getCustomers(): void {
    this.customerService.getCustomers().subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          console.log(response);
          this.customers = response.data;
          this.errorMessage=response.message
        }
      },
      error: (err) => {
        this.errorMessage = 'Error fetching customers';
        console.error(err);
      }
    });
  }
  createCustomer(): void {
    this.router.navigate(['/customer/add']);
  }

  updateCustomer(): void {
    if (this.selectedCustomers.length === 1) {
      const customer = this.selectedCustomers[0];
      this.router.navigate([`/customer/update/${customer.id}`]);  // Điều hướng đến trang update
    }
  }

  
  confirmDeactivation(): void {
    if (this.selectedCustomers.length > 0) {
      this.displayConfirmDialog = true;  // Hiển thị dialog xác nhận
    }
  }

  // Thực hiện deactivate khi người dùng xác nhận
  deactivateSelected(): void {
    const ids = this.selectedCustomers.map(customer => customer.id);

    this.customerService.deleteCustomers(ids).subscribe({
      next: () => {
        console.log('Customers deactivated successfully');
        this.displayConfirmDialog = false;  // Ẩn dialog
        this.getCustomers();  // Tải lại danh sách sau khi deactivate
      },
      error: (err) => {
        console.error('Error deactivating customers', err);
      }
    });
  }

  applyFilterGlobal($event: Event, stringVal: string) {
    const input = ($event.target as HTMLInputElement).value;
    this.table.filterGlobal(input, stringVal);  // Lọc dữ liệu trong bảng
  }
}
