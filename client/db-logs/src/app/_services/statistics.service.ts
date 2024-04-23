import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ChartData } from 'chart.js';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  private baseUrl = 'https://localhost:7272/api/';

  constructor(private http: HttpClient) { }

  getSalesByMonth(startDate: string, endDate: string) {
    return this.http.get<ChartData>(this.baseUrl + `statistics/sales-by-period?startDate=${startDate}&endDate=${endDate}`);
  }
}
