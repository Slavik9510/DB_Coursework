import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductParams } from '../_models/productParams';
import { Product } from '../_models/product.model';
import { getPaginationHeaders } from './paginationHelper';
import { ProductDetails } from '../_models/product-details.model';
import { AddReviewDto } from '../_models/addReviewDto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = 'https://localhost:7272/api/';
  // products: Product[] = [];
  constructor(private http: HttpClient) { }

  getProducts(productParams: ProductParams) {
    let params = getPaginationHeaders(productParams.pageNumber, productParams.pageSize);

    params = params.append('category', productParams.category);
    params = params.append('orderBy', productParams.orderBy);
    params = params.append('orderDescending', productParams.orderDescending);

    if (productParams.minPrice)
      params = params.append('minPrice', productParams.minPrice);
    if (productParams.maxPrice)
      params = params.append('maxPrice', productParams.maxPrice);

    return this.http.get<Product[]>(this.baseUrl + 'products', { params: params, observe: 'response' });
  }

  getProductDetails(id: number) {
    return this.http.get<ProductDetails>(this.baseUrl + 'products/' + id);
  }

  addReview(addReview: AddReviewDto) {
    return this.http.post(this.baseUrl + 'products/add-review', addReview);
  }
}
