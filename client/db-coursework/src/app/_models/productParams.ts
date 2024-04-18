export class ProductParams {
    category: string;
    pageNumber = 1;
    pageSize = 5;
    orderBy = 'price';
    minPrice: number | undefined;
    maxPrice: number | undefined;
    orderDescending = false;

    constructor(category: string) {
        this.category = category;
    }
}