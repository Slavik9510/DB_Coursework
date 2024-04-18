import { Component, EventEmitter, Output } from '@angular/core';
import { DeliveryInfo } from '../_models/delivery-info.model';

@Component({
  selector: 'app-order-deliver-info',
  templateUrl: './order-deliver-info.component.html',
  styleUrls: ['./order-deliver-info.component.css']
})
export class OrderDeliverInfoComponent {
  @Output() cancelOrder = new EventEmitter();
  @Output() deliveryDetails = new EventEmitter();

  deliveryInfo: any = {};

  cancel() {
    this.cancelOrder.emit(false);
  }

  getInfo() {
    this.deliveryDetails.emit(this.deliveryInfo);
  }
}
