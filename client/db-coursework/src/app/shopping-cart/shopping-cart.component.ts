import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../_services/shopping-cart.service';
import { Product } from '../_models/product.model';
import { CartItem } from '../_models/cart-item.model';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  cartItems: CartItem[] = [];

  constructor(private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
    const product: Product = {
      id: 1,
      name: 'Google CyberElite O3 Ultra',
      price: 10110.99,
      category: 'Phones',
      rating: 4.5,
      amountOfComments: 10,
      photoUrl: 'assets/images/phone.png'
    };
    this.cartItems.push(new CartItem(product, 1));
    this.cartItems.push(new CartItem(product, 1));
    //this.cartItems = this.shoppingCartService.getItems();
  }
  getTotalPrice(): number {
    let totalPrice = 0;

    for (const cartItem of this.cartItems) {
      const price = cartItem.product.price;
      const quantity = cartItem.quantity;

      totalPrice += price * quantity;
    }

    return totalPrice;
  }
}
