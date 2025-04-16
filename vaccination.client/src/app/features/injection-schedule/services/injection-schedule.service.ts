import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InjectionSchedule } from '../models/injection-schedule.model'; // Adjust the path as necessary
import { API_URL } from 'src/app/core/contants/url';

@Injectable({
  providedIn: 'root'
})
export class InjectionScheduleService {
  private apiUrl = `${API_URL}/InjectionSchedule`;

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<InjectionSchedule[]> {
    return this.httpClient.get<InjectionSchedule[]>(this.apiUrl);
  }

  getById(id: number): Observable<InjectionSchedule> {
    return this.httpClient.get<InjectionSchedule>(`${this.apiUrl}/${id}`);
  }

  create(schedule: InjectionSchedule): Observable<InjectionSchedule> {
    return this.httpClient.post<InjectionSchedule>(this.apiUrl, schedule);
  }

  update(schedule: InjectionSchedule): Observable<InjectionSchedule> {
    return this.httpClient.put<InjectionSchedule>(`${this.apiUrl}/${schedule.id}`, schedule);
  }

  delete(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/${id}`);
  }
}
