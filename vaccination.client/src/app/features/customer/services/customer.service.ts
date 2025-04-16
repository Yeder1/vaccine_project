import { Injectable } from '@angular/core';
import { API_URL } from 'src/app/core/contants/url';
import { CustomerDTO } from 'src/app/models/customer/customerDTO';
import { ApiResponse } from 'src/app/models/apiResponse';
import { HttpClient, HttpHeaders,HttpParams  } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddCustomerDTO } from 'src/app/models/customer/addCustomerDTO';
@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = `${API_URL}/customer`;
  constructor(private http: HttpClient) { }
  getCustomers(keyword: string = ''): Observable<ApiResponse<CustomerDTO[]>> {
    let params = new HttpParams();

    if (keyword) {
      params = params.set('keyword', keyword.trim());
    }

    return this.http.get<ApiResponse<CustomerDTO[]>>(this.apiUrl, { params });
  }
  getCustomerById(id: number): Observable<ApiResponse<CustomerDTO>> {
    return this.http.get<ApiResponse<CustomerDTO>>(`${this.apiUrl}/${id}`);
  }
  addCustomer(customer: AddCustomerDTO): Observable<AddCustomerDTO> {
    return this.http.post<AddCustomerDTO>(`${this.apiUrl}`, customer);
  }
  deleteCustomer(id: number): Observable<ApiResponse<string>> {
    return this.http.delete<ApiResponse<string>>(`${this.apiUrl}/${id}`);
  }
  deleteCustomers(ids: number[]): Observable<ApiResponse<string>> {
    return this.http.request<ApiResponse<string>>('delete', `${this.apiUrl}`, {
      body: ids
    });
  }
  
  updateCustomer(customer: AddCustomerDTO): Observable<ApiResponse<AddCustomerDTO>> {
    return this.http.put<ApiResponse<AddCustomerDTO>>(`${this.apiUrl}`, customer);
  }
}
