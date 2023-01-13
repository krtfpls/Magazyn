export interface Product {
    id: string ;
    name: string;
    serialNumber: string ;
    priceNetto: number;
    minLimit: number ;
    quantity: number;
    description: string;
    categoryName: string;
}

export class ProductClass implements Product{
    id= '';
    name= '';
    serialNumber= '';
    priceNetto= 0.01;
    minLimit= 1;
    quantity= 0;
    description= '';
    categoryName= '';

}