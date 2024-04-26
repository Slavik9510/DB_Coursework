import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { SharedModule } from './_modules/shared.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { HomeComponent } from './home/home.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { ProductCardComponent } from './products/product-card/product-card.component';
import { StarRatingComponent } from './star-rating/star-rating.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { CartItemCardComponent } from './cart-item-card/cart-item-card.component';
import { OrderDeliverInfoComponent } from './order-deliver-info/order-deliver-info.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { ProductDetailsComponent } from './products/product-details/product-details.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    RegisterComponent,
    TextInputComponent,
    HomeComponent,
    ProductListComponent,
    ProductCardComponent,
    StarRatingComponent,
    ShoppingCartComponent,
    CartItemCardComponent,
    OrderDeliverInfoComponent,
    ProductDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
