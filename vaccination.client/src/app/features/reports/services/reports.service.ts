import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReportCustomerDTO } from '../models/ReportCustomerDTO';
import { ReportCustomerChartDTO } from '../models/ReportCustomerChartDTO';
import { API_URL } from 'src/app/core/contants/url';

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  private  baseUrl = `${API_URL}`;

  constructor(private http: HttpClient) {}

  // Lấy danh sách báo cáo khách hàng
  getReportCustomers(): Observable<ReportCustomerDTO[]> {
    return this.http.get<ReportCustomerDTO[]>(`${this.baseUrl}/ReportCustomer`);
  }

  // Lấy dữ liệu biểu đồ báo cáo khách hàng theo năm
  getReportCustomerByYear(year: number): Observable<ReportCustomerChartDTO[]> {
    const params = new HttpParams().set('year', year.toString());
    return this.http.get<ReportCustomerChartDTO[]>(
      `${this.baseUrl}/ReportCustomerByYear`,
      { params }
    );
  }
}
