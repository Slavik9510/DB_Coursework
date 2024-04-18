import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user.model';
import { ShoppingCartService } from './_services/shopping-cart.service';
import { CartItem } from './_models/cart-item.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'db-coursework';

  constructor(private accountService: AccountService, private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user: User = JSON.parse(userString);
      this.accountService.setCurrentUser(user);
    }

    const cartItemsString = localStorage.getItem('cart-items');
    if (cartItemsString) {
      const cartItems: CartItem[] = JSON.parse(cartItemsString);
      this.shoppingCartService.setItems(cartItems);
    }
  }
}
