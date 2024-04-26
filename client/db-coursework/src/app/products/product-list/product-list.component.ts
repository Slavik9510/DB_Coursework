import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/_models/product.model';
import { ProductParams } from 'src/app/_models/productParams';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent {
  category: string | undefined;
  products: Product[] = [];
  productParams: ProductParams | undefined;
  totalItems: number | undefined;
  defaultPhoto: string | undefined;
  orderCriteria = 'price';

  constructor(private productService: ProductService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.category = params['category'];
    });

    switch (this.category) {
      case 'phones':
        this.category = 'Phones';
        break;
      case 'computers':
        this.category = 'Computers & Laptops';
        break;
      case 'home-appliances':
        this.category = 'Home Appliances';
        break;
      case 'tablets':
        this.category = 'Tablets';
        break;
      case 'tvs':
        this.category = 'Televisions';
        break;
    }

    this.productParams = new ProductParams(this.category!);
    this.productParams.pageSize = 15;
    this.productParams.orderBy = 'price';

    this.productService.getProducts(this.productParams).subscribe(response => {
      if (response.body)
        this.products = response.body;

      const paginationHeader = response.headers.get('Pagination');

      if (paginationHeader) {
        const paginationData = JSON.parse(paginationHeader);
        this.totalItems = paginationData.totalItems;
      }
    });
  }

  getTotalPages() {
    if (this.productParams && this.totalItems)
      return Math.ceil(this.totalItems / this.productParams?.pageSize);

    return null;
  }

  nextPage() {
    if (!this.productParams) return;

    this.productParams.pageNumber++;

    this.productService.getProducts(this.productParams).subscribe(response => {
      if (response.body)
        this.products = response.body;
    });
  }

  previousPage() {
    if (!this.productParams) return;

    this.productParams.pageNumber--;

    this.productService.getProducts(this.productParams).subscribe(response => {
      if (response.body)
        this.products = response.body;
    });
  }

  filterProducts() {
    if (!this.productParams) return;

    this.productParams.pageNumber = 1;
    switch (this.orderCriteria) {
      case 'price':
      case 'rating':
        this.productParams.orderBy = this.orderCriteria;
        this.productParams.orderDescending = false;
        break;
      case 'price-desc':
      case 'rating-desc':
        this.productParams.orderBy = this.orderCriteria.replace('-desc', '');
        this.productParams.orderDescending = true;
        break;
    }

    this.productService.getProducts(this.productParams).subscribe(response => {
      if (response.body)
        this.products = response.body;

      const paginationHeader = response.headers.get('Pagination');

      if (paginationHeader) {
        const paginationData = JSON.parse(paginationHeader);
        this.totalItems = paginationData.totalItems;
      }
    });
  }

  scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
