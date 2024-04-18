import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderDto } from '../_models/orderDto';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private baseUrl = 'https://localhost:7272/api/';
  constructor(private http: HttpClient) { }

  placeOrder(order: OrderDto) {
    return this.http.post(this.baseUrl + 'orders', order);
  }
}
