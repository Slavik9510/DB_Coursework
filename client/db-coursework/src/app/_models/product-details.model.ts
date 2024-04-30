import { Review } from "./review.model";

export interface ProductDetails {
    id: number;
    name: string;
    price: number;
    discount: number | undefined;
    category: string;
    description: string;
    attributes: { [key: string]: string };
    reviews: Review[];
}