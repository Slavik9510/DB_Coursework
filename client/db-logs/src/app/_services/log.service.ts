import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  private baseUrl = 'https://localhost:7272/api/';

  constructor(private http: HttpClient) { }

  getLogs(date: string): Observable<string[]> {
    return this.http.get<string[]>(this.baseUrl + `logs?date=${date}`);
  }
}
