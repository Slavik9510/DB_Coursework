import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginationParams } from '../_models/pagination-params';
import { InventoryItem } from '../_models/inventory-item.model';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

  private baseUrl = 'https://localhost:7272/api/';

  constructor(private http: HttpClient) { }

  getItemsToOrder(paginationParams: PaginationParams) {
    let params = this.getPaginationHeaders(paginationParams.pageNumber, paginationParams.pageSize);

    return this.http.get<InventoryItem[]>(this.baseUrl + 'inventory/items-to-order', { params: params, observe: 'response' });
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber);
    params = params.append('pageSize', pageSize);

    return params;
  }
}
