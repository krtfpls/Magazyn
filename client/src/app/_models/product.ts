export interface Product {
    id: string | undefined;
    name: string;
    serialNumber: string | undefined;
    priceNetto: number;
    minLimit: number | undefined;
    quantity: number;
    description: string | undefined;
    categoryName: string;
}

export class newProduct implements Product{
    id= undefined;
    name= '';
    serialNumber= '';
    priceNetto= 0.01;
    minLimit= 1;
    quantity= 1;
    description= '';
    categoryName= '';

}