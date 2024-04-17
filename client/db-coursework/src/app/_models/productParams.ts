import { Product } from "./product.model";

export class ProductParams {
    category: string;
    pageNumber = 1;
    pageSize = 5;
    orderBy = 'price';

    constructor(category: string) {
        this.category = category;
    }
}