import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../_services/shopping-cart.service';
import { Product } from '../_models/product.model';
import { CartItem } from '../_models/cart-item.model';
import { DeliveryInfo } from '../_models/delivery-info.model';
import { OrderService } from '../_services/order.service';
import { OrderDto } from '../_models/orderDto';
import { OrderItem } from '../_models/order-item.model';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  cartItems: CartItem[] = [];
  orderDetailsMode = false;
  deliveryInfo: DeliveryInfo | undefined;

  constructor(private shoppingCartService: ShoppingCartService, private orderService: OrderService) { }

  ngOnInit(): void {
    this.cartItems = this.shoppingCartService.getItems();
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

  cancelOrder(event: boolean) {
    this.orderDetailsMode = event;
  }

  getDetails(event: DeliveryInfo) {
    this.deliveryInfo = event;
    this.orderDetailsMode = false;

    this.placeOrder();
  }

  removeItem(event: CartItem) {
    this.cartItems = this.cartItems.filter(item => item.product.id !== event.product.id);
    this.shoppingCartService.removeAllInstancesFromCart(event.product);
  }

  private placeOrder() {
    if (!this.deliveryInfo) return;

    const order = new OrderDto(this.deliveryInfo, this.cartItemsToOrderItems(this.cartItems));
    this.orderService.placeOrder(order).subscribe(response => {
      this.cartItems = [];
      this.shoppingCartService.clearCart();
    });
  }

  private cartItemsToOrderItems(cartItems: CartItem[]): OrderItem[] {
    return cartItems.map(cartItem => ({
      productId: cartItem.product.id,
      quantity: cartItem.quantity
    }));
  }
}



// const product: Product = {
//   id: 1,
//   name: 'Google CyberElite O3 Ultra',
//   price: 10110.99,
//   category: 'Phones',
//   rating: 4.5,
//   amountOfComments: 10,
//   photoUrl: 'assets/images/phone.png'
// };
// this.cartItems.push(new CartItem(product, 1));
// const product2: Product = {
//   id: 2,
//   name: 'Google CyberElite',
//   price: 10110.99,
//   category: 'Phones',
//   rating: 4.5,
//   amountOfComments: 10,
//   photoUrl: 'assets/images/phone.png'
// };
// this.cartItems.push(new CartItem(product2, 1));
