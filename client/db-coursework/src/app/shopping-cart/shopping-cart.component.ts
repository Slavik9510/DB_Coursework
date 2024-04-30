import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../_services/shopping-cart.service';
import { CartItem } from '../_models/cart-item.model';
import { DeliveryInfo } from '../_models/delivery-info.model';
import { OrderService } from '../_services/order.service';
import { OrderDto } from '../_models/orderDto';
import { OrderItem } from '../_models/order-item.model';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  cartItems: CartItem[] = [];
  orderDetailsMode = false;
  deliveryInfo: DeliveryInfo | undefined;

  constructor(private shoppingCartService: ShoppingCartService, private orderService: OrderService,
    public accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.cartItems = this.shoppingCartService.getItems();
  }
  getTotalPrice(): number {
    let totalPrice = 0;

    for (const cartItem of this.cartItems) {
      let price = cartItem.product.price;
      if (cartItem.product.discount)
        price -= cartItem.product.discount;
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
    this.toastr.success('Item was succesfully removed from the cart');
  }

  private placeOrder() {
    if (!this.deliveryInfo) return;

    const order = new OrderDto(this.deliveryInfo, this.cartItemsToOrderItems(this.cartItems));
    this.orderService.placeOrder(order).subscribe({
      next: () => {
        this.cartItems = [];
        this.shoppingCartService.clearCart();
        this.toastr.success('Order succesfully placed');
      },
      error: () => {
        this.toastr.error('Something went wrong');
      }
    });
  }

  private cartItemsToOrderItems(cartItems: CartItem[]): OrderItem[] {
    return cartItems.map(cartItem => ({
      productId: cartItem.product.id,
      quantity: cartItem.quantity
    }));
  }
}