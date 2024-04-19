import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-order-deliver-info',
  templateUrl: './order-deliver-info.component.html',
  styleUrls: ['./order-deliver-info.component.css']
})
export class OrderDeliverInfoComponent implements OnInit {
  @Output() cancelOrder = new EventEmitter();
  @Output() deliveryDetails = new EventEmitter();

  deliveryInfo: any = { carrier: 'novaPost' };

  ngOnInit(): void {
    this.deliveryInfo.carrier = 'novaPost';
  }

  cancel() {
    this.cancelOrder.emit(false);
  }

  getInfo(carrier: string) {
    this.deliveryInfo.carrier = carrier;
    this.deliveryDetails.emit(this.deliveryInfo);
  }
}
