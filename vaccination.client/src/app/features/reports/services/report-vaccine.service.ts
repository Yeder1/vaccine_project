import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReportVaccineDTO } from '../models/ReportVaccineDTO';
import { ReportVaccineChartDTO } from '../models/ReportVaccineChartDTO';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReportVaccineService {
  private apiUrl = `${environment.BASE_API_URL}/api/Report`;

  constructor(private http: HttpClient) {}

  // API call for filtered table report
  getReportVaccine(
    injectionDateBegin?: string,
    injectionDateEnd?: string,
    vaccineType?: string,
    origin?: string
  ): Observable<ReportVaccineDTO[]> {
    let params = new HttpParams();
    if (injectionDateBegin) params = params.set('injectionDateBegin', injectionDateBegin);
    if (injectionDateEnd) params = params.set('injectionDateEnd', injectionDateEnd);
    if (vaccineType) params = params.set('vaccineType', vaccineType);
    if (origin) params = params.set('origin', origin);

    return this.http.get<ReportVaccineDTO[]>(`${this.apiUrl}/ReportVaccine`, { params });
  }

  // API call for chart report by year
  getVaccineReportByYear(year: number): Observable<ReportVaccineChartDTO[]> {
    return this.http.get<ReportVaccineChartDTO[]>(`${this.apiUrl}/GetVaccineReportByYear?year=${year}`);
  }
}
