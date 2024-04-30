import { Component, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Product } from 'src/app/_models/product.model';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent {
  @Input() product: Product | undefined;

  constructor(private shoppingCartService: ShoppingCartService, private toastr: ToastrService) { }

  getPhoto() {
    if (!this.product) return ' ';

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

  addToCart() {
    if (this.product) {
      this.shoppingCartService.addToCart(this.product);
      this.toastr.success('Item was succesfully added to the cart');
    }
  }
}
