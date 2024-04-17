export interface Product {
    id: number;
    name: string;
    price: number;
    category: string;
    rating: number;
    amountOfComments: number;
    photoUrl: string | undefined;
}