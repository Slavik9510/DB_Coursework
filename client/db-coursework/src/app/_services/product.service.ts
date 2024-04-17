import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductParams } from '../_models/productParams';
import { Product } from '../_models/product.model';
import { getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl = 'https://localhost:7272/api/';
  products: Product[] = [];
  constructor(private http: HttpClient) { }

  getProducts(productParams: ProductParams) {
    let params = getPaginationHeaders(productParams.pageNumber, productParams.pageSize);

    params = params.append('category', productParams.category)
    params = params.append('orderBy', productParams.orderBy)

    return this.http.get<Product[]>(this.baseUrl + 'products', { params: params });
  }
}
