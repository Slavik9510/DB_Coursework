export interface Product {
    id: number;
    name: string;
    price: string;
    category: string;
    attributes: { [key: string]: any };
}