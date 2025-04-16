import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VaccineTypeDTO } from '../../../models/vaccineType/vacineTypeDTO'; // Adjust the import according to your file structure
import { API_URL } from 'src/app/core/contants/url';

@Injectable({
  providedIn: 'root'
})
export class VaccineTypeService {
  private baseUrl = `${API_URL}/vaccine-type`; // Update with your API endpoint

  constructor(private http: HttpClient) {}

  getVaccineTypes(): Observable<VaccineTypeDTO[]> {
    return this.http.get<VaccineTypeDTO[]>(this.baseUrl);
  }
}
