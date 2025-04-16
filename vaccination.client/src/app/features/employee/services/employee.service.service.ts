import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from 'src/app/core/contants/url';
import { Employee } from '../models/employee';
import { AddEmployeeRequest } from '../models/addEmployeeRequest';

@Injectable({
  providedIn: 'root'
})
export class EmployeeServiceService {
  apiUrl = API_URL + '/Employee';
  constructor(private httpClient: HttpClient) {}
  getByKeyWord(keyword: string): Observable<any> {
    return this.httpClient.get(`${this.apiUrl}/${keyword}`);
  }

  getAll(): Observable<any> {
    return this.httpClient.get(`${this.apiUrl}/getAll`);
  }

  delete(list: Employee[]): Observable<any> {
    return this.httpClient.delete(`${this.apiUrl}/List`, {
      body: list,
    });
  }

  add(request: FormData): Observable<any> {
    return this.httpClient.post(`${this.apiUrl}`, request);
  }

  update(request: FormData): Observable<any> {
    return this.httpClient.put(`${this.apiUrl}`, request);
  }

  getDetail(id: string): Observable<any> {
    return this.httpClient.get<any>(`${this.apiUrl}/GetEmployeeDetail/${id}`);
  }
}
