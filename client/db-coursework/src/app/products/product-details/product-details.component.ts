import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { ProductDetails } from 'src/app/_models/product-details.model';
import { Product } from 'src/app/_models/product.model';
import { AccountService } from 'src/app/_services/account.service';
import { ProductService } from 'src/app/_services/product.service';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  product: ProductDetails | undefined;

  constructor(private route: ActivatedRoute, private productsService: ProductService,
    private shoppingCartService: ShoppingCartService, public accountService: AccountService) { }

  ngOnInit(): void {
    this.route.params.pipe(
      map(params => params['id'])
    ).subscribe(id => {
      this.productsService.getProductDetails(id).subscribe(response => {
        this.product = response;
      })
    });
  }

  getPhoto() {
    if (!this.product) return '';

    switch (this.product.category) {
      case 'Phones':
        return 'assets/images/phone.png';
      case 'Computers & Laptops':
        return 'assets/images/computer.png';
      case 'Home Appliances':
        return 'assets/images/homeAppliances.png';
      case 'Tablets':
        return 'assets/images/tablet.png';
      case 'Televisions':
        return 'assets/images/tv.png';
      default:
        return '';
    }
  }

  objectKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  addToCart() {
    if (!this.product) return;
    const product: Product = {
      id: this.product.id,
      name: this.product.name,
      price: this.product.price,
      category: this.product.category,
      rating: 0,
      amountOfComments: 0,
      photoUrl: undefined
    }
    this.shoppingCartService.addToCart(product);
  }
}
