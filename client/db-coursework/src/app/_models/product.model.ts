export interface Product {
    id: number;
    name: string;
    price: number;
    discount: number | undefined;
    category: string;
    rating: number;
    amountOfComments: number;
    photoUrl: string | undefined;
}