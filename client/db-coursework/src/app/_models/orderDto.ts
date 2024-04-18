import { DeliveryInfo } from "./delivery-info.model";
import { OrderItem } from "./order-item.model";

export class OrderDto {
    city: string;
    address: string;
    postalCode: string;
    cartItems: OrderItem[];

    constructor(deliveryInfo: DeliveryInfo, orderItems: OrderItem[]) {
        this.city = deliveryInfo.city;
        this.address = deliveryInfo.address;
        this.postalCode = deliveryInfo.postalCode;
        this.cartItems = orderItems;
    }
}