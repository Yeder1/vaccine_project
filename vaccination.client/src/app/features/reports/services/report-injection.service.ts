import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface ReportInjectionDTO {
    no: number;
    vaccineName: string;
    prevention: string;
    customerName: string;
    injectDate: Date;
    numberOfInject: number;
  }
export interface ReportInjectionChartDTO {
    month: number;
    numberOfInjections: number;
  }
@Injectable({
  providedIn: 'root'
})

export class ReportInjectionService {
  private baseUrl = `${environment.BASE_API_URL}/api/Report`;

  constructor(private http: HttpClient) {}

  /**
   * Gọi API để lấy dữ liệu báo cáo theo các bộ lọc (fromDate, toDate, prevention, vaccineTypeId)
   * @param fromDate Ngày bắt đầu
   * @param toDate Ngày kết thúc
   * @param prevention Phòng ngừa
   * @param vaccineTypeId ID loại vaccine
   * @returns Observable<ReportInjectionDTO[]>
   */
  getReportInjections(fromDate: string, toDate: string, prevention: string, vaccineTypeId: string): Observable<ReportInjectionDTO[]> {
    let params = new HttpParams();
    if (fromDate) params = params.set('fromDate', fromDate);
    if (toDate) params = params.set('toDate', toDate);
    if (prevention) params = params.set('prevention', prevention);
    if (vaccineTypeId) params = params.set('vaccineTypeId', vaccineTypeId);

    return this.http.get<ReportInjectionDTO[]>(`${this.baseUrl}/ReportInjection`, { params });
  }

  /**
   * Gọi API để lấy dữ liệu báo cáo số lần tiêm chủng theo năm (dành cho biểu đồ)
   * @param year Năm cần lấy dữ liệu
   * @returns Observable<ReportInjectionChartDTO[]>
   */
  getInjectionReportByYear(year: number): Observable<ReportInjectionChartDTO[]> {
    const params = new HttpParams().set('year', year.toString());
    return this.http.get<ReportInjectionChartDTO[]>(`${this.baseUrl}/ReportInjectionByYear`, { params });
  }
}
