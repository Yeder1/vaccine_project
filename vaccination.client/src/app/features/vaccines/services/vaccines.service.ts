import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VaccineDTO } from '../models/vaccinceDTO';
import { API_URL } from 'src/app/core/contants/url';

@Injectable({
  providedIn: 'root',
})
export class VaccineService {
  private apiUrl = `${API_URL}/vaccine`;

  constructor(private http: HttpClient) {}

  getAllVaccines(): Observable<VaccineDTO[]> {
    return this.http.get<VaccineDTO[]>(`${this.apiUrl}`);
  }

  getVaccineById(id: number): Observable<VaccineDTO> {
    return this.http.get<VaccineDTO>(`${this.apiUrl}/${id}`);
  }

  addVaccine(vaccine: VaccineDTO): Observable<VaccineDTO> {
    return this.http.post<VaccineDTO>(`${this.apiUrl}/add`, vaccine);
  }

  updateVaccine(vaccine: VaccineDTO): Observable<VaccineDTO> {
    return this.http.put<VaccineDTO>(
      `${this.apiUrl}/update/${vaccine.id}`,
      vaccine
    );
  }

  deleteVaccine(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${id}`);
  }

  deactivateVaccine(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/deactivate/${id}`, null);
  }

  deactivateMultipleVaccines(ids: number[]): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/deactivate`, ids); // Assuming the API accepts a list of IDs.
  }

  importVaccines(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<void>(`${this.apiUrl}/import`, formData);
  }
}
