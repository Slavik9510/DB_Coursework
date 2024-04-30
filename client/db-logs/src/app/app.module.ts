import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavComponent } from './nav/nav.component';
import { LogsViewerComponent } from './logs-viewer/logs-viewer.component';
import { NgChartsModule } from 'ng2-charts';
import { BarChartComponent } from './bar-chart/bar-chart.component';
import { StatisticsViewerComponent } from './statistics-viewer/statistics-viewer.component';
import { CustomCheckboxComponent } from './custom-checkbox/custom-checkbox.component';
import { LoginComponent } from './login/login.component';
import { JwtModule } from '@auth0/angular-jwt';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { InventoryComponent } from './inventory/inventory.component';
import { PhoneNumberPipe } from './_pipes/phone-number.pipe';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    LogsViewerComponent,
    NavComponent,
    BarChartComponent,
    StatisticsViewerComponent,
    CustomCheckboxComponent,
    LoginComponent,
    UnauthorizedComponent,
    InventoryComponent,
    PhoneNumberPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgChartsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7272"],
        disallowedRoutes: []
      }
    }),
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    })
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
