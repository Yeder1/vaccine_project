import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VaccineTypeDTO } from 'src/app/models/vaccineType/vacineTypeDTO';
import { API_URL } from 'src/app/core/contants/url';
import { VaccineTypeRequestDTO } from 'src/app/models/vaccineType/vacineTypeRequestDTO';

@Injectable({
  providedIn: 'root',
})
export class VaccineTypeService {
  private apiUrl = `${API_URL}/vaccine-type`;

  constructor(private http: HttpClient) {}

  getAllVaccineTypes(): Observable<VaccineTypeDTO[]> {
    return this.http.get<VaccineTypeDTO[]>(`${this.apiUrl}`);
  }

  getVaccineTypeById(id: number): Observable<VaccineTypeDTO> {
    return this.http.get<VaccineTypeDTO>(`${this.apiUrl}/${id}`);
  }

  addVaccineType(vaccine: FormData): Observable<FormData> {
    return this.http.post<FormData>(`${this.apiUrl}/add`, vaccine);
  }

  // updateVaccineType(vaccine: VaccineTypeDTO): Observable<VaccineTypeDTO> {
  //   return this.http.put<VaccineTypeDTO>(
  //     `${this.apiUrl}/update`,
  //     vaccine
  //   );
  // }

  // updateVaccineType(vaccine: VaccineTypeRequestDTO): Observable<VaccineTypeRequestDTO> {
  //   return this.http.put<VaccineTypeRequestDTO>(
  //     `${this.apiUrl}/update`,
  //     vaccine
  //   );
  // }

  updateVaccineType(vaccine: FormData): Observable<FormData> {
    return this.http.put<FormData>(
      `${this.apiUrl}/update`,
      vaccine
    );
  }

  deleteVaccineType(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/delete/${id}`);
  }

  deactivateVaccineType(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/deactivate/${id}`, null);
  }

  deactivateVaccineTypes(vaccineTypes: VaccineTypeDTO[]): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/deactivate-list`, vaccineTypes); // Assuming the API accepts a list of IDs.
  }

  importVaccineTypes(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.http.post<void>(`${this.apiUrl}/import`, formData);
  }
}
