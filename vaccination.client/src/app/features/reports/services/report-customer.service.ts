import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface ReportCustomerDTO {
    no: number;
    fullName: string | null;
    dob: Date | null;
    address: string | null;
    identityCard: string | null;
    numberOfInject: number;
  }
  
  export interface ReportCustomerChartDTO {
    month: number;
    numberOfCustomers: number;
  }
  

@Injectable({
  providedIn: 'root'
})
export class ReportCustomerService {
  private baseUrl = `${environment.BASE_API_URL}/api/Report`;

  constructor(private http: HttpClient) {}

  getReportCustomers(dobFrom: string, dobTo: string, fullName: string, address: string): Observable<ReportCustomerDTO[]> {
    const params = new HttpParams()
      .set('dobFrom', dobFrom)
      .set('dobTo', dobTo)
      .set('fullName', fullName)
      .set('address', address);
    return this.http.get<ReportCustomerDTO[]>(`${this.baseUrl}/ReportCustomer`, { params });
  }

  getCustomerReportByYear(year: number): Observable<ReportCustomerChartDTO[]> {
    return this.http.get<ReportCustomerChartDTO[]>(`${this.baseUrl}/ReportCustomerByYear?year=${year}`);
  }
}
