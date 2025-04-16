import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from 'src/app/core/contants/url';
import { News } from '../components/models/new';
import { AddNewRequest } from '../components/models/addRequest';

@Injectable({
  providedIn: 'root',
})
export class NewsService {
  apiUrl = API_URL + '/News';
  constructor(private httpClient: HttpClient) { }
  getAll(keyword: string): Observable<any> {
    return this.httpClient.get(`${this.apiUrl}?keyword=${keyword}`);
  }

  delete(list: News[]): Observable<any> {
    return this.httpClient.delete(`${this.apiUrl}/List`, {
      body: list,
    });
  }

  add(request: AddNewRequest): Observable<any> {
    return this.httpClient.post(`${this.apiUrl}`, request);
  }

  update(request: News): Observable<any> {
    return this.httpClient.put(`${this.apiUrl}`, request);
  }

  getDetail(id: string): Observable<News> {
    return this.httpClient.get<News>(`${this.apiUrl}/${id}`);
  }
}
