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
    return this.http.get<ChartData<'bar'>>(this.baseUrl + `statistics/sales-per-month?startDate=${startDate}&endDate=${endDate}`);
  }

  getCityOrdersByMonth(startDate: string, endDate: string) {
    return this.http.get<ChartData<'bar'>>(this.baseUrl + `statistics/city-orders-per-month?startDate=${startDate}&endDate=${endDate}`);
  }

  getAverageOrderPriceByMonth(startDate: string, endDate: string) {
    return this.http.get<ChartData<'bar'>>(this.baseUrl + `statistics/avg-order-price-per-month?startDate=${startDate}&endDate=${endDate}`);
  }

  getTotalSalesByCategoryAndMonth(startDate: string, endDate: string) {
    return this.http.get<ChartData<'bar'>>(this.baseUrl + `statistics/category-sales-per-month?startDate=${startDate}&endDate=${endDate}`);
  }

  getAvgOrdersPerCustomerByMonth(startDate: string, endDate: string) {
    return this.http.get<ChartData<'bar'>>(this.baseUrl + `statistics/avg-customer-orders-per-month?startDate=${startDate}&endDate=${endDate}`);
  }

  getProductsReturnedByMonth(startDate: string, endDate: string) {
    return this.http.get<ChartData<'bar'>>(this.baseUrl + `statistics/products-returned-per-month?startDate=${startDate}&endDate=${endDate}`);
  }

  getTopReviewedProductsByMonth(startDate: string, endDate: string) {
    return this.http.get<ChartData<'bar'>>(this.baseUrl + `statistics/top-reviewed-products-per-month?startDate=${startDate}&endDate=${endDate}`);
  }
}
