import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CartItem } from '../_models/cart-item.model';

@Component({
  selector: 'app-cart-item-card',
  templateUrl: './cart-item-card.component.html',
  styleUrls: ['./cart-item-card.component.css']
})
export class CartItemCardComponent {
  @Input() item: CartItem | undefined;
  @Output() removeItem = new EventEmitter();

  getPhoto() {
    if (!this.item) return ' ';

    switch (this.item.product.category) {
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

  incrementQuantity() {
    if (!this.item) return;

    this.item.quantity = Math.min(this.item.quantity + 1, 10);
  }

  decrementQuantity() {
    if (!this.item) return;

    this.item.quantity = Math.max(this.item.quantity - 1, 1);
  }

  removeProduct() {
    if (!this.item) return;

    this.removeItem.emit(this.item);
  }
}
