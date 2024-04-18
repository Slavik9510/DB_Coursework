import { Injectable } from '@angular/core';
import { Product } from '../_models/product.model';
import { CartItem } from '../_models/cart-item.model';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
  private items: CartItem[] = [];

  constructor() { }

  addToCart(product: Product): void {
    const currentItems = this.items;
    const foundItem = currentItems.find(item => item.product.id === product.id);
    if (foundItem) {
      foundItem.quantity++;
    } else {
      currentItems.push(new CartItem(product, 1));
    }
    localStorage.setItem('cart-items', JSON.stringify(this.items));
  }

  removeFromCart(product: Product): void {
    const currentItems = this.items;
    const index = currentItems.findIndex(item => item.product.id === product.id);
    if (index !== -1) {
      currentItems[index].quantity--;
      if (currentItems[index].quantity === 0) {
        currentItems.splice(index, 1);
      }
    }
    localStorage.setItem('cart-items', JSON.stringify(this.items));
  }

  removeAllInstancesFromCart(product: Product): void {
    const currentItems = this.items;
    const index = currentItems.findIndex(item => item.product.id === product.id);

    if (index !== -1) {
      currentItems.splice(index, 1);
    }
    localStorage.setItem('cart-items', JSON.stringify(this.items));
  }

  getItems(): CartItem[] {
    return this.items;
  }

  clearCart(): void {
    this.items = [];
    localStorage.setItem('cart-items', JSON.stringify(this.items));
  }

  setItems(items: CartItem[]): void {
    this.items = items;
  }
}