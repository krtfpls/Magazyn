export interface Product {
    id: string;
    name: string;
    serialNumber: string | null;
    priceNetto: number;
    minLimit: number | null;
    quantity: number;
    description: string | null;
    categoryName: string;
}