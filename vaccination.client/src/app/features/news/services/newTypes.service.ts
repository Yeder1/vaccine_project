import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NewType } from '../components/models/newType';
import { API_URL } from 'src/app/core/contants/url';

@Injectable({
  providedIn: 'root',
})
export class NewTypesService {
  apiUrl = API_URL + '/NewsType';
  constructor(private http: HttpClient) {}
  getAll(): Observable<NewType[]> {
    return this.http.get<NewType[]>(this.apiUrl);
  }
}
