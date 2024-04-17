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

  constructor(private productService: ProductService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.category = params['category'];
    });

    this.productParams = new ProductParams(this.category!);

    this.productService.getProducts(this.productParams).subscribe(response => {
      this.products = response;
    })
  }
}
