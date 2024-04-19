import { Review } from "./review.model";

export interface ProductDetails {
    id: number;
    name: string;
    price: number;
    category: string;
    description: string;
    attributes: { [key: string]: string };
    reviews: Review[];
}